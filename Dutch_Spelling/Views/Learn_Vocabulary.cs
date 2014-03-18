using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.CoreGraphics;
using Spelling.Core.Models;
using Spelling.Core.Views;
using Spelling.Core;

namespace Dutch_Spelling
{
    public partial class Learn_Vocabulary : UIViewController
    {
        public Learn_Vocabulary() : base("Learn_Vocabulary", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			           
            _countdownTimer = new System.Timers.Timer();
            _countdownTimer.Interval = 1000;
            _countdownTimer.Elapsed += HandleCountDownElapsed;           

            View.BackgroundColor = MicJames.ExtensionMethods.ToUIColor("F4F4F4");
            btnSkipTimer.BackgroundColor = MicJames.ExtensionMethods.ToUIColor("53D769");
            btnSkipTimer.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnSkipTimer.Font = UIFont.FromName("Raleway-SemiBold", 32);

            btnSkipTimer.TouchUpInside += HandleBottomButtonTouchUpInside;

            GetNextVocabulary();

            if (_answerView == null)
                _answerView = new AnswerView(new RectangleF(0, 180, this.View.Bounds.Width, 220));
            this.View.Add(_answerView);
            _answerView.Hidden = true;
            _answerView.ValidAnswer += HandleValidAnswer;

            if (_healthStatus == null)
                _healthStatus = new HealthStatus(new RectangleF(this.View.Bounds.Width - 120, 30, 120, 35));
            this.Add(_healthStatus);
        }

        void HandleValidAnswer ()
        {
            btnSkipTimer.SetTitle("Next", UIControlState.Normal);
            btnSkipTimer.BackgroundColor = MicJames.ExtensionMethods.ToUIColor("53D769");
        }

        void ShowAnswerView()
        {
            _answerView.Hidden = false;
        }

        void HandleBottomButtonTouchUpInside (object sender, EventArgs e)
        {
            if ((btnSkipTimer.Title(UIControlState.Normal) == "Skip") || (btnSkipTimer.Title(UIControlState.Normal) == "Next"))
            {
                GetNextVocabulary(); 

                _answerView.Reset();
                _answerView.Hidden = true;

                if (_testNative == true)
                {
                    RestoreViewFromTestAgainstNative();
                }
                else
                {
                    RestoreViewFromTestAgainTarget();                  
                }
            }           
        
            if(btnSkipTimer.Title(UIControlState.Normal) == "Skip")
                _healthStatus.RemoveLife();
        }

        void HandleCountDownElapsed (object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_countDownValue == 0)
            {
                _countdownTimer.Stop();
                if (RandomNumberBetween(0, 1) == 1)
                {
                    TestAgainstNative();
                }
                else
                {
                    TestAgainstTarget();
                }
            }
            else
                _countDownValue--;
            InvokeOnMainThread(delegate
            {
                btnSkipTimer.SetTitle(_countDownValue.ToString(), UIControlState.Normal);
            });
        }

        void GetNextVocabulary()
        {
            if (_wordList == null)
                _wordList = VocabManager.ImportWords();
            var index = RandomNumberBetween(0, _wordList.Count);
            var native = new Vocabulary("English", _wordList[index].English);
            var target = new Vocabulary("Dutch", _wordList[index].Target);
            RefreshWordViews(native, target);
        }

        void RefreshWordViews(Vocabulary native, Vocabulary target)
        {
            if (_nativeWord == null)
            {
                _nativeWord = new WordView(new RectangleF(0, 0, 320, 30), native);
                this.View.Add(_nativeWord);
            }
      
            if (_targetWord == null)
            {
                _targetWord = new WordView(new RectangleF(0, 160, 320, 30), target);            
                this.View.Add(_targetWord);
            }
                      
            _targetWord.Update(target);
            _nativeWord.Update(native);

            _countDownValue = 5;
            _countdownTimer.Start();
        }

        /// <summary>
        /// See if the user can remember the Dutch spelling of the English word
        /// </summary>
        void TestAgainstNative()
        {
            _testNative = true;

            InvokeOnMainThread(delegate
            {
                _nativeWord.GrowWord();
                CGAffineTransform transform = CGAffineTransform.MakeIdentity();
                transform.Scale(0f, 0f);     

                UIView.Animate(0.6, 0, UIViewAnimationOptions.TransitionCurlUp,
                    () =>
                    {
                        _nativeWord.Frame = new RectangleF(0, 0, 320, 30);
                        _targetWord.Transform = transform; 
                   
                    }, () =>
                {
                    btnSkipTimer.SetTitle("Skip", UIControlState.Normal);
                    btnSkipTimer.BackgroundColor = MicJames.ExtensionMethods.ToUIColor("FECB2F");
                    ShowAnswerView();
                });
            });
        }

        void RestoreViewFromTestAgainstNative()
        {
            InvokeOnMainThread(delegate
            {
                _nativeWord.ShrinkWord();
                CGAffineTransform transform = CGAffineTransform.MakeIdentity();
                transform.Scale(1f, 1f);     

                UIView.Animate(0.5, 0, UIViewAnimationOptions.TransitionCurlUp,
                    () =>
                    {
                        _nativeWord.Frame = new RectangleF(0, 0, 320, 30);
                        _targetWord.Transform = transform; 

                    }, () =>
                {
                    btnSkipTimer.SetTitle("", UIControlState.Normal);
                    btnSkipTimer.BackgroundColor = MicJames.ExtensionMethods.ToUIColor("53D769");
                });
            });
        }

        /// <summary>
        /// See if the user can remember the English spelling of the Dutch word
        /// </summary>
        void TestAgainstTarget()
        {
            _testNative = false;
            InvokeOnMainThread(delegate
            {
                _targetWord.GrowWord();
                UIView.Animate(0.6, 0, UIViewAnimationOptions.TransitionCurlUp,
                    () =>{
                    _nativeWord.Frame = new RectangleF(0, -250, 320, 30);
                    _targetWord.Frame = new RectangleF(0, 0, 320, 30);
                },() =>{
                    btnSkipTimer.SetTitle("Skip", UIControlState.Normal);
                    btnSkipTimer.BackgroundColor = MicJames.ExtensionMethods.ToUIColor("FECB2F");
                    ShowAnswerView();
                });
            });
        }

        /// <summary>
        /// Restores the view from test again target.
        /// </summary>
        void RestoreViewFromTestAgainTarget()
        {
            InvokeOnMainThread(delegate
            {
                _targetWord.ShrinkWord();
                UIView.Animate(0.5, 0, UIViewAnimationOptions.TransitionCurlUp,
                    () =>
                    {
                        _nativeWord.Frame = new RectangleF(0, 0, 320, 30);
                        _targetWord.Frame = new RectangleF(0, 160, 320, 30);
                    }, () =>
                {
                    btnSkipTimer.SetTitle("5", UIControlState.Normal);
                    btnSkipTimer.BackgroundColor = MicJames.ExtensionMethods.ToUIColor("53D769");
                });
            });
        }

        int RandomNumberBetween(double minimum, double maximum)
        { 
            Random random = new Random();
            return Convert.ToInt16(random.NextDouble() * (maximum - minimum) + minimum);
        }

        //Views
        WordView _nativeWord;
        WordView _targetWord;
        AnswerView _answerView;
        HealthStatus _healthStatus;

        System.Timers.Timer _countdownTimer;
        int _countDownValue = 5;
        List<Word> _wordList;
        bool _testNative = false;
    }
}

