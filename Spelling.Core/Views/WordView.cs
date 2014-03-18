using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using Spelling.Core.Models;

namespace Spelling.Core.Views
{
    public class WordView : UIView
    {       
        public WordView(RectangleF frame, Vocabulary vocab)
        {
            this.Init();
            this.Frame = frame;

            _vocab = vocab;

            //Title label
            _title = new UILabel(new RectangleF(0, 45, frame.Width, 50));
            _title.TextAlignment = UITextAlignment.Center;
            _title.TextColor = MicJames.ExtensionMethods.ToUIColor("3C3C3C");
            _title.Font = UIFont.FromName("Raleway-SemiBold", 32);
            _title.Text = _vocab.Language;
            this.Add(_title);

            //Word label
            _word = new UILabel(new RectangleF(0, 108, frame.Width, 59));
            _word.TextAlignment = UITextAlignment.Center;
            _word.TextColor = MicJames.ExtensionMethods.ToUIColor("3C3C3C");
            _word.Font = UIFont.FromName("Raleway-Regular", 32);
            _word.Text = _vocab.Word;
            this.Add(_word);
        }

        public void Update(Vocabulary vocab)
        {
            _word.Text = vocab.Word;
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
        Vocabulary _vocab;
    }
}

