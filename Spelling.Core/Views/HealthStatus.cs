using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace Spelling.Core.Views
{
    public class HealthStatus : UIView
    {       

        UIImageView _heart1;
        UIImageView _heart2;
        UIImageView _heart3;
        UIImageView _heart4;
        UIImageView _heart5;
        UIImageView _heart6;

        public HealthStatus(RectangleF rect)
        {
            this.Frame = rect;
            Init();
            this.BackgroundColor = UIColor.Clear;

            _heart1 = new UIImageView(new RectangleF(rect.Width - 35, 0, 20, 17));
            _heart1.Image = UIImage.FromFile("heart.png");
            _heart1.Alpha = 0.2f;
            this.Add(_heart1);

            _heart2 = new UIImageView(new RectangleF(rect.Width - 60, 0, 20, 17));
            _heart2.Image = UIImage.FromFile("heart.png");
            _heart2.Alpha = 0.2f;
            this.Add(_heart2);

            _heart3 = new UIImageView(new RectangleF(rect.Width - 85, 0, 20, 17));
            _heart3.Image = UIImage.FromFile("heart.png");
            _heart3.Alpha = 0.2f;
            this.Add(_heart3);

            //Falling Heart
            _heart4 = new UIImageView(new RectangleF(rect.Width - 35, 0, 20, 17));
            _heart4.Image = UIImage.FromFile("heart.png");
            this.Add(_heart4);

            _heart5 = new UIImageView(new RectangleF(rect.Width - 60, 0, 20, 17));
            _heart5.Image = UIImage.FromFile("heart.png");
            this.Add(_heart5);

            _heart6 = new UIImageView(new RectangleF(rect.Width - 85, 0, 20, 17));
            _heart6.Image = UIImage.FromFile("heart.png");
            this.Add(_heart6);


        }

        public int Health
        {          
            get
            {
                return _currentLivesHeld;
            }
        }

        public void RemoveLife()
        {
            if (_currentLivesHeld >= 0)
            {
                if (_currentLivesHeld == 3)
                {
                    var frame = _heart6.Frame;
                    var newFrame = new RectangleF(frame.X, frame.Y + 50, frame.Width, frame.Height);
                    UIView.Animate(0.5, 0, UIViewAnimationOptions.TransitionCurlUp,
                        () =>{
                        _heart6.Frame = newFrame;
                        _heart6.Alpha = 0.0f;         
                    },() =>{});                   
                }

                if (_currentLivesHeld == 2)
                {
                    var frame = _heart5.Frame;
                    var newFrame = new RectangleF(frame.X, frame.Y + 50, frame.Width, frame.Height);
                    UIView.Animate(0.5, 0, UIViewAnimationOptions.TransitionCurlUp,
                        () =>{
                        _heart5.Frame = newFrame;
                        _heart5.Alpha = 0.0f;         
                    },() =>{});            
                }          

                if (_currentLivesHeld == 1)
                {
                    var frame = _heart4.Frame;
                    var newFrame = new RectangleF(frame.X, frame.Y + 50, frame.Width, frame.Height);
                    UIView.Animate(0.5, 0, UIViewAnimationOptions.TransitionCurlUp,
                        () =>{
                        _heart4.Frame = newFrame;
                        _heart4.Alpha = 0.0f;         
                    },() =>{});                     
                }

                _currentLivesHeld--;
            }
        }

        public void Reset()
        {
            _currentLivesHeld = 3;
        }



        int _totalLivesAllowed = 3;
        int _currentLivesHeld = 3;
    }
}

