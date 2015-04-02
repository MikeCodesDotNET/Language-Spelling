using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;
using Spelling.Core;

namespace Spelling.Core 
{
    [Register("AnswerView")]
    internal class AnswerView : UIView
    {
        #region Fields
        static readonly Random random = new Random();
        readonly List<CGRect> _defaultPositions;
        readonly UILabel _label;

        string _answer = string.Empty;
        string _targetWord = string.Empty;

        UIButton _btnRemoveLastChar;
        List<UIButton> buttons = new List<UIButton>();
        List<UIButton> _buttonsUsedInAnswer;

        #endregion

        #region Public
        public AnswerView(CGRect rect)
        {
            var i = 0;
            while(i < 12)
            {
                var button = new UIButton(UIButtonType.RoundedRect);
                ButtonBuilder(ref button);
                buttons.Add(button);
                Add(button);
                i++;
            }

            Frame = rect;
            Init();
            BackgroundColor = UIColor.Clear;

            _label = new UILabel(new CGRect(25, 10, rect.Width - 50, 40));
            _label.BackgroundColor = UIColor.Clear;
            _label.Font = UIFont.FromName("Raleway-Regular", 32);
            _label.TextColor = "434343".ToUIColor();
            Add(_label);

            _btnRemoveLastChar = new UIButton(UIButtonType.RoundedRect);
            _btnRemoveLastChar.SetTitle("<", UIControlState.Normal);
            _btnRemoveLastChar.Frame = new CGRect(rect.Width - 50, 10, 40, 40);
            _btnRemoveLastChar.TouchUpInside += delegate
            {
                if (_buttonsUsedInAnswer.Count != 0)
                {
                    UIButton button = _buttonsUsedInAnswer[_buttonsUsedInAnswer.Count - 1]; //Gets last button
                    _buttonsUsedInAnswer.Remove(button);
                    RemoveButtonFromAnswer(button);
                    BuildAnswer();
                }
            };
            Add(_btnRemoveLastChar);

            _defaultPositions = new List<CGRect>();

            _defaultPositions.Add(new CGRect(12, 100, 45, 45));
            _defaultPositions.Add(new CGRect(62, 100, 45, 45));
            _defaultPositions.Add(new CGRect(112, 100, 45, 45));
            _defaultPositions.Add(new CGRect(162, 100, 45, 45));
            _defaultPositions.Add(new CGRect(212, 100, 45, 45));
            _defaultPositions.Add(new CGRect(262, 100, 45, 45));

            _defaultPositions.Add(new CGRect(12, 160, 45, 45));
            _defaultPositions.Add(new CGRect(62, 160, 45, 45));
            _defaultPositions.Add(new CGRect(112, 160, 45, 45));
            _defaultPositions.Add(new CGRect(162, 160, 45, 45));
            _defaultPositions.Add(new CGRect(212, 160, 45, 45));
            _defaultPositions.Add(new CGRect(262, 160, 45, 45));
        }

        public string Text
        {
            get
            {
                return _answer;
            }
        }

        public override bool Hidden
        {
            get { return base.Hidden; }
            set
            {
                base.Hidden = value;
                _label.Text = "";
                ForceButtonsToStartPosition();
            }
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            UIColor color = "3C3C3C".ToUIColor();
            UIBezierPath rectanglePath = UIBezierPath.FromRect(new CGRect(25, 50, rect.Width - 50, 0));
            UIColor.White.SetFill();
            rectanglePath.Fill();
            color.SetStroke();
            rectanglePath.LineWidth = 1.2f;
            rectanglePath.Stroke();

            UIColor squareColour = "9F9F9F".ToUIColor();

            var i = 0;
            while(i < 12)
            {
                UIBezierPath button1SquarePath = UIBezierPath.FromRect(_defaultPositions[i]);
                squareColour.SetStroke();
                button1SquarePath.LineWidth = 1;
                button1SquarePath.Stroke();
                i++;
                Console.WriteLine(i);
            }
        }

        public void Reset()
        {
            if (_buttonsUsedInAnswer != null)
            {
                foreach (UIButton b in _buttonsUsedInAnswer)
                {
                    b.BackgroundColor = "F4F4F4".ToUIColor();
                    b.SetTitleColor("434343".ToUIColor(), UIControlState.Normal);
                    RemoveButtonFromAnswer(b);
                }
                _buttonsUsedInAnswer.Clear();
                _answer = "";
            }
        }

        public void SetTargetWord(string targetWord)
        {
            _targetWord = targetWord;
            int targetLength = targetWord.Length;
            int remainingLength = 11 - targetLength;

            char[] letters = ScrambleWord(targetWord.ToLower()).ToCharArray();

            while (remainingLength >= 1)
            {
                int i = letters.Length;
                Array.Resize(ref letters, i + 1);
                letters[i] = GetLetter();
                var word = new string(letters);
                string words = ScrambleWord(word);
                letters = words.ToCharArray();
                remainingLength = 12 - letters.Length;
            }
            UpdateButtonTitles(letters);
        }

        public void ForceButtonsToStartPosition()
        {
            var i = 0;
            while(i < buttons.Count)
            {
                buttons[i].Frame = _defaultPositions[i];
                i++;
            }
        }

        #endregion

        #region Private
        static char GetLetter()
        {
            int num = random.Next(0, 26);
            var letter = (char) ('a' + num);
            return letter;
        }

        static string ScrambleWord(string word)
        {
            string scambledWord = word.ToLower();
            var rnd = new Random();
            int rndPosition;
            int wordLength = scambledWord.Length;

            string scrambledWord = "";
            var letter = new string[wordLength];

            for (int i = 0; i <= (wordLength - 1); i++)
            {
                repeat:
                rndPosition = Convert.ToInt16((wordLength)*rnd.NextDouble());

                if (rndPosition == wordLength || letter[rndPosition] != null)
                {
                    goto repeat;
                }
                letter[rndPosition] = word.Substring(i, 1);
            }
            for (int j = 0; j <= (wordLength - 1); j++)
            {
                scrambledWord += letter[j];
            }
            return scrambledWord;
        }

        void HandleButtonTouchUpInside(object sender, EventArgs e)
        {
            if (_buttonsUsedInAnswer == null)
                _buttonsUsedInAnswer = new List<UIButton>();

            var button = (UIButton) sender;

            if (_buttonsUsedInAnswer.Contains(button))
            {
                _buttonsUsedInAnswer.Remove(button);
                RemoveButtonFromAnswer(button);
            }
            else
            {
                _buttonsUsedInAnswer.Add(button);
                BuildAnswer();
                if (_answer.Length <= 30)
                {
                    Animate(0.2, 0, UIViewAnimationOptions.TransitionCurlUp,
                        () =>
                        {
                            button.Frame = CalculatePosition(_buttonsUsedInAnswer.IndexOf(button));
                            button.Alpha = 0.0f;
                        }, () => { });
                }
            }
        }

        void RemoveButtonFromAnswer(UIButton button)
        {
            CGRect frame;
            frame = _defaultPositions[Convert.ToInt16(button.Tag) - 1];
            Animate(0.2, 0, UIViewAnimationOptions.TransitionCurlUp,
                () =>
                {
                    button.Frame = frame;
                    button.Alpha = 1f;
                    button.BackgroundColor = "E6E5E7".ToUIColor();
                    button.SetTitleColor("434343".ToUIColor(), UIControlState.Normal);
                }, () => { });
            BuildAnswer();
            AdjustPositions();
        }

        void UpdateButtonTitles(char[] value)
        {
            //TODO when the char[] is less than 11 (12) items then fill it with extra chars.

            var i = 0;
            while(i < buttons.Count)
            {
                buttons[i].SetTitle(value[i].ToString(), UIControlState.Normal);
                i++;
            }
        }

        UIButton ButtonBuilder(ref UIButton button)
        {
            button.Tag = buttons.Count + 1;
            button.BackgroundColor = "E6E5E7".ToUIColor();
            button.Font = UIFont.FromName("Raleway-Light", 18);
            button.SetTitleColor("434343".ToUIColor(), UIControlState.Normal);
            button.SetTitle("a", UIControlState.Normal);
            button.TouchUpInside += HandleButtonTouchUpInside;
            return button;
        }

        void BuildAnswer()
        {
            _answer = "";
            foreach (UIButton button in _buttonsUsedInAnswer)
            {
                _answer = _answer + button.Title(UIControlState.Normal);
            }
            _label.Text = _answer;

            if (AnswerIsValid)
            {
                ValidAnswer(true);
            }
            else
            {
                ValidAnswer(false);
            }
        }

        void AdjustPositions()
        {
            foreach (UIButton button in _buttonsUsedInAnswer)
            {
                Animate(0.2, 0, UIViewAnimationOptions.TransitionCurlUp,
                    () => { button.Frame = CalculatePosition(_buttonsUsedInAnswer.IndexOf(button)); }, () => { });
            }
        }

        bool AnswerIsValid
        {
            get
            {
                return _answer.ToLower() == _targetWord.ToLower();
            }
        }

        CGRect CalculatePosition(int buttonPosition)
        {
            //var i = _answer.Length;
            int a = 18 + (18*(buttonPosition));
            return new CGRect(a, 15, 28, 28);
        }

        #endregion

        #region Events
        public delegate void ValidAnswerEventHandler(bool value);
        public event ValidAnswerEventHandler ValidAnswer;
        #endregion 
    }
}