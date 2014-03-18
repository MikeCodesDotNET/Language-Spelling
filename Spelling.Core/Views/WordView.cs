using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using Spelling.Core.Models;
using System.Globalization;

namespace Spelling.Core.Views
{
    public class WordView : UIView
    {       
        public WordView(RectangleF frame, string title, string value)
        {
            this.Init();
            this.Frame = frame;

            TextInfo textInfo = new CultureInfo("en-US",false).TextInfo;

            title = textInfo.ToTitleCase(title);
            value = textInfo.ToTitleCase(value);

            //Title label
            _title = new UILabel(new RectangleF(0, 45, frame.Width, 50));
            _title.TextAlignment = UITextAlignment.Center;
            _title.TextColor = MicJames.ExtensionMethods.ToUIColor("3C3C3C");
            _title.Font = UIFont.FromName("Raleway-SemiBold", 32);
            _title.Text = title;
            this.Add(_title);

            //Word label
            _word = new UILabel(new RectangleF(0, 108, frame.Width, 59));
            _word.TextAlignment = UITextAlignment.Center;
            _word.TextColor = MicJames.ExtensionMethods.ToUIColor("3C3C3C");
            _word.Font = UIFont.FromName("Raleway-Regular", 32);
            _word.Text = value;
            this.Add(_word);
        }

        public void Update(string value)
        {
            _word.Text = value;
            _word.Alpha = 1.0f;
        }

        public void GrowWord()
        {
            CGAffineTransform transform = CGAffineTransform.MakeIdentity();
            transform.Scale(1.5f, 1.5f);            

            UIView.Animate(0.6, 0, UIViewAnimationOptions.TransitionCurlUp,
                () =>{
                _word.Transform = transform;
                _title.TextColor = MicJames.ExtensionMethods.ToUIColor("646465");
            },() =>{
                _word.Font = UIFont.FromName("Raleway-Regular", 48);
                CGAffineTransform transform2 = CGAffineTransform.MakeIdentity();
                transform.Scale(1f, 1f);  
                _word.Transform = transform2;
            });
        }

        public void ShrinkWord()
        {
            CGAffineTransform transform = CGAffineTransform.MakeIdentity();
            transform.Scale(1f, 1f);            

            UIView.Animate(0.6, 0, UIViewAnimationOptions.TransitionCurlUp,
                () =>{
                _word.Transform = transform; 
                _title.TextColor = MicJames.ExtensionMethods.ToUIColor("3C3C3C");
            },() =>{
                _word.Font = UIFont.FromName("Raleway-Regular", 32);
            });
        }

        UILabel _title;
        UILabel _word;
    }
}

