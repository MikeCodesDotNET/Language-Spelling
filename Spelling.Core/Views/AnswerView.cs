using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.Collections.Generic;

namespace Spelling.Core.Views
{
    public class AnswerView : UIView
    {
        UIButton _button1;
        UIButton _button2;
        UIButton _button3;
        UIButton _button4;
        UIButton _button5;
        UIButton _button6;
        UIButton _button7;
        UIButton _button8;
        UIButton _button9;
        UIButton _button10;
        UIButton _button11;
        UIButton _button12;

        UILabel _label;
        UIButton _btnRemoveLastChar;

        int _buttonCount =1;
        string _answer = "";
        List<UIButton> _buttonsUsedInAnswer;
        List<RectangleF> _defaultPositions;


        public AnswerView(RectangleF rect)
        {
            this.Frame = rect;
            Init();
            this.BackgroundColor = UIColor.Clear;

            _label = new UILabel(new RectangleF(25, 10, rect.Width - 50, 40));
            _label.BackgroundColor = UIColor.Clear;
            _label.Font = UIFont.FromName("Raleway-Regular", 32); 
            _label.TextColor = MicJames.ExtensionMethods.ToUIColor("434343");
            this.Add(_label);

            _btnRemoveLastChar = new UIButton(UIButtonType.RoundedRect);
            _btnRemoveLastChar.SetTitle("<", UIControlState.Normal);
            _btnRemoveLastChar.Frame = new RectangleF(rect.Width - 50, 10, 40, 40);
            _btnRemoveLastChar.TouchUpInside += delegate
            {    
                if(_buttonsUsedInAnswer.Count != 0)
                {
                    var button = _buttonsUsedInAnswer[_buttonsUsedInAnswer.Count -1]; //Gets last button
                    _buttonsUsedInAnswer.Remove(button);
                    RemoveButtonFromAnswer(button);
                    BuildAnswer();
                }
            };
            this.Add(_btnRemoveLastChar);

            _button1 = new UIButton(UIButtonType.RoundedRect);
            this.Add(ButtonBuilder(ref _button1));

            _button2 = new UIButton(UIButtonType.RoundedRect);
            this.Add(ButtonBuilder(ref _button2));

            _button3 = new UIButton(UIButtonType.RoundedRect);
            this.Add(ButtonBuilder(ref _button3));

            _button4 = new UIButton(UIButtonType.RoundedRect);
            this.Add(ButtonBuilder(ref _button4));

            _button5 = new UIButton(UIButtonType.RoundedRect);
            this.Add(ButtonBuilder(ref _button5));

            _button6 = new UIButton(UIButtonType.RoundedRect);
            this.Add(ButtonBuilder(ref _button6));

            _button7 = new UIButton(UIButtonType.RoundedRect);
            this.Add(ButtonBuilder(ref _button7));

            _button8 = new UIButton(UIButtonType.RoundedRect);
            this.Add(ButtonBuilder(ref _button8));

            _button9 = new UIButton(UIButtonType.RoundedRect);
            this.Add(ButtonBuilder(ref _button9));

            _button10 = new UIButton(UIButtonType.RoundedRect);
            this.Add(ButtonBuilder(ref _button10));

            _button11 = new UIButton(UIButtonType.RoundedRect);
            this.Add(ButtonBuilder(ref _button11));

            _button12 = new UIButton(UIButtonType.RoundedRect);
            this.Add(ButtonBuilder(ref _button12));

            _defaultPositions = new List<RectangleF>();
           
            _defaultPositions.Add(new RectangleF(12, 100, 45, 45));
            _defaultPositions.Add(new RectangleF(62, 100, 45, 45));
            _defaultPositions.Add(new RectangleF(112, 100, 45, 45));
            _defaultPositions.Add(new RectangleF(162, 100, 45, 45));
            _defaultPositions.Add(new RectangleF(212, 100, 45, 45));
            _defaultPositions.Add(new RectangleF(262, 100, 45, 45));

            _defaultPositions.Add(new RectangleF(12, 160, 45, 45));
            _defaultPositions.Add(new RectangleF(62, 160, 45, 45));
            _defaultPositions.Add(new RectangleF(112, 160, 45, 45));
            _defaultPositions.Add(new RectangleF(162, 160, 45, 45));
            _defaultPositions.Add(new RectangleF(212, 160, 45, 45));
            _defaultPositions.Add(new RectangleF(262, 160, 45, 45));
        }

        public override void Draw(System.Drawing.RectangleF rect)
        {
            base.Draw(rect);
            UIColor color = MicJames.ExtensionMethods.ToUIColor("3C3C3C");
            var rectanglePath = UIBezierPath.FromRect(new RectangleF(25, 50, rect.Width - 50, 0));
            UIColor.White.SetFill();
            rectanglePath.Fill();
            color.SetStroke();
            rectanglePath.LineWidth = 1.2f;
            rectanglePath.Stroke();

            var squareColour = MicJames.ExtensionMethods.ToUIColor("9F9F9F");
            var button1SquarePath = UIBezierPath.FromRect(_defaultPositions[0]);
            squareColour.SetStroke();
            button1SquarePath.LineWidth = 1;
            button1SquarePath.Stroke();

            var button2SquarePath = UIBezierPath.FromRect(_defaultPositions[1]);
            squareColour.SetFill();
            button2SquarePath.LineWidth = 1;
            button2SquarePath.Stroke();

            var button3SquarePath = UIBezierPath.FromRect(_defaultPositions[2]);
            squareColour.SetFill();
            button3SquarePath.LineWidth = 1;
            button3SquarePath.Stroke();

            var button4SquarePath = UIBezierPath.FromRect(_defaultPositions[3]);
            squareColour.SetFill();
            button4SquarePath.LineWidth = 1;
            button4SquarePath.Stroke();

            var button5SquarePath = UIBezierPath.FromRect(_defaultPositions[4]);
            squareColour.SetFill();
            button5SquarePath.LineWidth = 1;
            button5SquarePath.Stroke();

            var button6SquarePath = UIBezierPath.FromRect(_defaultPositions[5]);
            squareColour.SetFill();
            button6SquarePath.LineWidth = 1;
            button6SquarePath.Stroke();

            var button7SquarePath = UIBezierPath.FromRect(_defaultPositions[6]);
            squareColour.SetFill();
            button7SquarePath.LineWidth = 1;
            button7SquarePath.Stroke();

            var button8SquarePath = UIBezierPath.FromRect(_defaultPositions[7]);
            squareColour.SetFill();
            button8SquarePath.LineWidth = 1;
            button8SquarePath.Stroke();

            var button9SquarePath = UIBezierPath.FromRect(_defaultPositions[8]);
            squareColour.SetFill();
            button9SquarePath.LineWidth = 1;
            button9SquarePath.Stroke();

            var button10SquarePath = UIBezierPath.FromRect(_defaultPositions[9]);
            squareColour.SetFill();
            button10SquarePath.LineWidth = 1;
            button10SquarePath.Stroke();

            var button11SquarePath = UIBezierPath.FromRect(_defaultPositions[10]);
            squareColour.SetFill();
            button11SquarePath.LineWidth = 1;
            button11SquarePath.Stroke();

            var button12SquarePath = UIBezierPath.FromRect(_defaultPositions[11]);
            squareColour.SetFill();
            button12SquarePath.LineWidth = 1;
            button12SquarePath.Stroke();
        }

        public void Reset()
        {
            if (_buttonsUsedInAnswer != null)
            {
                foreach (var b in _buttonsUsedInAnswer)
                {
                    b.BackgroundColor = MicJames.ExtensionMethods.ToUIColor("F4F4F4");
                    b.SetTitleColor(MicJames.ExtensionMethods.ToUIColor("434343"), UIControlState.Normal);
                    RemoveButtonFromAnswer(b);
                }
                _buttonsUsedInAnswer.Clear();
                _answer = "";
            }
        }

        void HandleButtonTouchUpInside (object sender, EventArgs e)
        {
            if (_buttonsUsedInAnswer == null)
                _buttonsUsedInAnswer = new List<UIButton>();

            var button = (UIButton)sender;

            if (_buttonsUsedInAnswer.Contains(button) == true)
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
                    UIView.Animate(0.2, 0, UIViewAnimationOptions.TransitionCurlUp,
                        () =>
                        {
                        button.Frame = CalculatePosition(_buttonsUsedInAnswer.IndexOf(button));
                        button.Alpha = 0.0f;                       
                        }, () =>
                    {
                    });
                }

                if (AnswerIsValid() == true)
                {                   
                    ValidAnswer();
                }
            }
        }

        void RemoveButtonFromAnswer(UIButton button)
        {
            RectangleF frame;
            frame = _defaultPositions[button.Tag - 1];
            UIView.Animate(0.2, 0, UIViewAnimationOptions.TransitionCurlUp,
                () =>{
                button.Frame = frame;  
                button.Alpha = 1f;
                button.BackgroundColor = MicJames.ExtensionMethods.ToUIColor("E6E5E7");
                button.SetTitleColor(MicJames.ExtensionMethods.ToUIColor("434343"), UIControlState.Normal);
            },() =>{});
            BuildAnswer();
            AdjustPositions();
        }
       
        UIButton ButtonBuilder(ref UIButton button)
        {
            button.Tag = _buttonCount;
            button.BackgroundColor = MicJames.ExtensionMethods.ToUIColor("E6E5E7");
            button.Font = UIFont.FromName("Raleway-Light", 18);
            button.SetTitleColor(MicJames.ExtensionMethods.ToUIColor("434343"), UIControlState.Normal);
            button.SetTitle("a", UIControlState.Normal);
            button.TouchUpInside += HandleButtonTouchUpInside;
            _buttonCount++;
            return button;
        }

        void BuildAnswer()
        {
            _answer = "";
            foreach (var b in _buttonsUsedInAnswer)
            {
                _answer = _answer + b.Title(UIControlState.Normal);
            }
            _label.Text = _answer;
        }

        void AdjustPositions()
        {
            foreach (var b in _buttonsUsedInAnswer)
            {
                UIView.Animate(0.2, 0, UIViewAnimationOptions.TransitionCurlUp,
                () =>{
                    b.Frame = CalculatePosition(_buttonsUsedInAnswer.IndexOf(b));
                },() =>{});
               
            }
        }

        public void ForceButtonsToStartPosition()
        {
            _button1.Frame = _defaultPositions[0];
            _button2.Frame = _defaultPositions[1];
            _button3.Frame = _defaultPositions[2];
            _button4.Frame = _defaultPositions[3];
            _button5.Frame = _defaultPositions[4];
            _button6.Frame = _defaultPositions[5];

            _button7.Frame = _defaultPositions[6];
            _button8.Frame = _defaultPositions[7];
            _button9.Frame = _defaultPositions[8];
            _button10.Frame = _defaultPositions[9];
            _button11.Frame = _defaultPositions[10];
            _button12.Frame = _defaultPositions[11];
        }

        public override bool Hidden
        {
            get
            {
                return base.Hidden;
            }
            set
            {
                base.Hidden = value;
                _label.Text = "";
                ForceButtonsToStartPosition();
            }
        }

        bool AnswerIsValid()
        {
            if (_answer.Length == 8)
                return true;
            return false;
        }

        RectangleF CalculatePosition(int buttonPosition)
        {
            //var i = _answer.Length;
            var a = 18 + (18 * (buttonPosition));
            return new RectangleF(a, 15, 28, 28);
        }

        public delegate void ValidAnswerEventHandler();
        public event ValidAnswerEventHandler ValidAnswer;

    }
}

