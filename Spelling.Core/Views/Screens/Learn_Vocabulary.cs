using System;
using CoreGraphics;
using System.Timers;
using UIKit;
using Spelling.Core;
using Spelling.Core.Models;
using System.Collections.Generic;

namespace Spelling.Core.Views.Screens
{
    partial class Learn_Vocabulary : UIViewController
    {
        public Learn_Vocabulary() : base("Learn_Vocabulary", null)
        {
        }

        public LanguagePack SelectedLanguagePack {get; set;}

        #region "Fields"
        AnswerView _answerView;
        int _countDownValue = 5;
        Timer _countdownTimer;
        Word _currentWord;
        WordView _nativeWord;
        WordView _targetWord;
        bool _testNative;
        #endregion

        #region "Overrides"
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if(_answerView == null)
                _answerView = new AnswerView(new CGRect(0, 180, View.Bounds.Width, 220)) {Hidden = true};

            _answerView.Reset();
            _answerView.Hidden = true;

            if (_testNative)
                RestoreViewFromTestAgainstNative();
            else
                RestoreViewFromTestAgainTarget();

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _countdownTimer = new Timer {Interval = 1000};
            _countdownTimer.Elapsed += HandleCountDownElapsed;

            View.BackgroundColor = "F4F4F4".ToUIColor();
            btnSkipTimer.BackgroundColor = "1ECE6D".ToUIColor();
            btnSkipTimer.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnSkipTimer.Font = UIFont.FromName("Raleway-SemiBold", 32);

            btnSkipTimer.TouchUpInside += HandleBottomButtonTouchUpInside;

            GetNextVocabulary();

            var backButton = new UIButton(UIButtonType.RoundedRect) {Font = UIFont.FromName("Raleway-SemiBold", 18)};
            backButton.SetTitleColor("646465".ToUIColor(), UIControlState.Normal);
            backButton.SetTitle("Back", UIControlState.Normal);
            backButton.Frame = new CGRect(0, 20, 80, 40);
            backButton.TouchUpInside += delegate { DismissViewController(true, null); };
            Add(backButton);

            View.AddGestureRecognizer(new UISwipeGestureRecognizer(gesture =>
                DismissViewController(true, null)) {Direction = UISwipeGestureRecognizerDirection.Down,});
        }

        public override void DismissViewController(bool animated, Action completionHandler)
        {
            _answerView.Alpha = 0;
            _targetWord.Alpha = 0;
            _nativeWord.Alpha = 0;
            RestoreViewFromTestAgainTarget();
            base.DismissViewController(animated, completionHandler);
        }
       

        #endregion

        #region "Public Methods"
        public void Reset()
        {
            if (_answerView == null)
            {
                BuildViews();
            }
            else
            {
                _answerView.Alpha = 1;
                _targetWord.Alpha = 1;
                _nativeWord.Alpha = 1;
                GetNextVocabulary();

                _answerView.Reset();
                _answerView.Hidden = true;
            }

            if (_testNative)
                RestoreViewFromTestAgainstNative();
            else
                RestoreViewFromTestAgainTarget();

        }

        #endregion

        #region "Private Methods"
        void HandleValidAnswer(bool value)
        {
            if (value)
            {
                btnSkipTimer.SetTitle("Next", UIControlState.Normal);
                btnSkipTimer.BackgroundColor = "1ECE6D".ToUIColor();
            }
            else
            {
                btnSkipTimer.SetTitle("Skip", UIControlState.Normal);
                btnSkipTimer.BackgroundColor = "F2C500".ToUIColor();
            }
        }

        void ShowAnswerView()
        {
            _answerView.Hidden = false;
        }

        void HandleBottomButtonTouchUpInside(object sender, EventArgs e)
        {
            if ((btnSkipTimer.Title(UIControlState.Normal) != "Skip") &&
                (btnSkipTimer.Title(UIControlState.Normal) != "Next")) return;

            if (btnSkipTimer.Title(UIControlState.Normal) == "Skip")
            {
                Xamarin.Insights.Track("Skipped Test", new Dictionary<string, string> {
                    {"Native", _currentWord.Native },
                    {"Target", _currentWord.Target},
                    {"Response", _answerView.Text}
                });
            }

            GetNextVocabulary();

            _answerView.Reset();
            _answerView.Hidden = true;

            if (_testNative)
                RestoreViewFromTestAgainstNative();
            else
                RestoreViewFromTestAgainTarget();

        }

        void HandleCountDownElapsed(object sender, ElapsedEventArgs e)
        {

            #if DEBUG
            if(_countDownValue == 0)
            {
                _countdownTimer.Stop();
                TestAgainstNative();
                InvokeOnMainThread(delegate { btnSkipTimer.SetTitle(_countDownValue.ToString(), UIControlState.Normal); });
                return;
            }
            #endif

            if (_countDownValue == 0)
            {
                _countdownTimer.Stop();
                if (Extensions.RandomNumberBetween(0, 1) == 1)
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
            InvokeOnMainThread(delegate { btnSkipTimer.SetTitle(_countDownValue.ToString(), UIControlState.Normal); });
        }

        void GetNextVocabulary()
        {
            var i = Extensions.RandomNumberBetween(0, SelectedLanguagePack.Words.Count);

            #if DEBUG
            i = 0;
            #endif

            if (i <= SelectedLanguagePack.Words.Count - 1)
            {
                SelectedLanguagePack.SelectWord(i);
                RefreshWordViews(SelectedLanguagePack);
            }
            else
            {
                GetNextVocabulary();
            }
        }

        void RefreshWordViews(LanguagePack languagePack)
        {
            _currentWord = languagePack.SelectedWord;
            //Native probably being english
            if (_nativeWord == null)
            {
                if (languagePack.NativeLanguage == null)
                    languagePack.NativeLanguage = Client.NativeLanguage;
                _nativeWord = new WordView(new CGRect(0, 0, View.Bounds.Width, 30), languagePack.NativeLanguage,
                    languagePack.SelectedWord.Native);
                View.Add(_nativeWord);
            }

            //Target being Dutch, French etc..
            if (_targetWord == null)
            {
                if (languagePack.TargetLanguage == null)
                    languagePack.TargetLanguage = Client.TargetLanguage;

                _targetWord = new WordView(new CGRect(0, 160, View.Frame.Width, 30), languagePack.TargetLanguage,
                    languagePack.SelectedWord.Target);
                View.Add(_targetWord);
            }

            _targetWord.Update(languagePack.SelectedWord.Target);
            _nativeWord.Update(languagePack.SelectedWord.Native);

            _countDownValue = 5;
            _countdownTimer.Start();
        }

        /// <summary>
        ///     See if the user can remember the Dutch spelling of the English word
        /// </summary>
        void TestAgainstNative()
        {
            _testNative = true;

            InvokeOnMainThread(delegate
            {
                _nativeWord.GrowWord();
                CGAffineTransform transform = CGAffineTransform.MakeIdentity();
                transform.Scale(0f, 0f);

                UIView.Animate(0.6, 0, UIViewAnimationOptions.CurveEaseIn,
                    () =>
                    {
                        _nativeWord.Frame = new CGRect(0, 0, View.Frame.Width, 30);
                        _targetWord.Transform = transform;
                    }, () =>
                    {
                        btnSkipTimer.SetTitle("Skip", UIControlState.Normal);
                        btnSkipTimer.BackgroundColor = "F2C500".ToUIColor();
                        _answerView.SetTargetWord(_currentWord.Target);
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
                        _nativeWord.Frame = new CGRect(0, 0, 320, 30);
                        _targetWord.Transform = transform;
                    }, () =>
                    {
                        btnSkipTimer.SetTitle("", UIControlState.Normal);
                        btnSkipTimer.BackgroundColor = "1ECE6D".ToUIColor();
                    });
            });
        }

        /// <summary>
        ///     See if the user can remember the English spelling of the Dutch word
        /// </summary>
        void TestAgainstTarget()
        {
            _testNative = false;
            InvokeOnMainThread(delegate
            {
                _targetWord.GrowWord();
                UIView.Animate(0.6, 0, UIViewAnimationOptions.TransitionCurlUp,
                    () =>
                    {
                        _nativeWord.Frame = new CGRect(0, -250, View.Frame.Width, 30);
                        _targetWord.Frame = new CGRect(0, 0, View.Frame.Width, 30);
                    }, () =>
                    {
                        btnSkipTimer.SetTitle("Skip", UIControlState.Normal);
                        btnSkipTimer.BackgroundColor = "F2C500".ToUIColor();
                        _answerView.SetTargetWord(_currentWord.Native);
                        ShowAnswerView();
                    });
            });
        }

        /// <summary>
        ///     Restores the view from test again target.
        /// </summary>
        void RestoreViewFromTestAgainTarget()
        {
            InvokeOnMainThread(delegate
            {
                _targetWord.ShrinkWord();
                UIView.Animate(0.5, 0, UIViewAnimationOptions.TransitionCurlUp,
                    () =>
                    {
                            _nativeWord.Frame = new CGRect(0, 0, View.Frame.Width, 30);
                            _targetWord.Frame = new CGRect(0, 160, View.Frame.Width, 30);
                    }, () =>
                    {
                        btnSkipTimer.SetTitle("5", UIControlState.Normal);
                        btnSkipTimer.BackgroundColor = "1ECE6D".ToUIColor();
                    });
            });
        }
            
        void BuildViews()
        {
            if (_answerView == null)
                _answerView = new AnswerView(new CGRect(0, 180, View.Bounds.Width, 220)) {Hidden = true};
            _answerView.ValidAnswer += HandleValidAnswer;
            View.Add(_answerView);
        }

        #endregion
    }
}