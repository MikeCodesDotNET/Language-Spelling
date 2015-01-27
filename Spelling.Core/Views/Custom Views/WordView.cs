using System.Globalization;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Spelling.Core;
using System.Drawing;

namespace Spelling.Core
{
    [Register("WordView")]
    internal sealed class WordView : UIView
    {       
        #region Fields

        readonly UILabel _title;
        readonly UILabel _word;

        #endregion

        #region Public

        public WordView(RectangleF frame, string title, string value)
        {
            Init();
            Frame = frame;

            var textInfo = new CultureInfo("en-US",false).TextInfo;
           

            title = textInfo.ToTitleCase(title);
            value = textInfo.ToTitleCase(value);

            //Title label
            _title = new UILabel(new RectangleF(0, 45, frame.Width, 50))
            {
                TextAlignment = UITextAlignment.Center,
                    TextColor = UIColor.FromRGB(60,60,60),
                Font = UIFont.FromName("Raleway-SemiBold", 32),
                Text = title
            };
            Add(_title);

            //Word label
            _word = new UILabel(new RectangleF(0, 108, frame.Width, 59))
            {
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.FromRGB(60,60,60),
                Font = UIFont.FromName("Raleway-Regular", 32),
                Text = value
            };
            Add(_word);
        }

        public void Update(string value)
        {
            _word.Text = value;
            _word.Alpha = 1.0f;
        }

        public void GrowWord()
        {
            var transform = CGAffineTransform.MakeIdentity();
            transform.Scale(1.5f, 1.5f);            

            UIView.Animate(0.6, 0, UIViewAnimationOptions.TransitionCurlUp,
                () =>{
                _word.Transform = transform;
                    _title.TextColor = UIColor.FromRGB(100, 100, 101);
            },() =>{
                _word.Font = UIFont.FromName("Raleway-Regular", 48);
                var transform2 = CGAffineTransform.MakeIdentity();
                transform.Scale(1f, 1f);  
                _word.Transform = transform2;
            });
        }

        public void ShrinkWord()
        {
            var transform = CGAffineTransform.MakeIdentity();
            transform.Scale(1f, 1f);            

            UIView.Animate(0.6, 0, UIViewAnimationOptions.TransitionCurlUp,
                () =>{
                _word.Transform = transform; 
                    _title.TextColor = UIColor.FromRGB(60,60,60);
            },() =>{
                _word.Font = UIFont.FromName("Raleway-Regular", 32);
            });
        }

        #endregion
    }
}

