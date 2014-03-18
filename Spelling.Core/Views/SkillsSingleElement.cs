using System;
using MonoTouch.Dialog;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Collections.Generic;

namespace Spelling.Core.Views
{
    public class SkillsElement : Element
    {
        public SkillsElement (Models.Skill skill) : base(UITableViewCellStyle.Default, "sampleOwnerDrawnElement")
        {
            Skill = skill;
            Text = skill.Title;
            Image = UIImage.FromFile(skill.ImageURl);

            title = new UILabel();
            title.Font =  UIFont.FromName("Raleway-Regular", 32);
            title.TextColor = MicJames.ExtensionMethods.ToUIColor("434343");
            title.Text = Text;


        }

        Models.Skill Skill;

        private UILabel title;

        public string Text { get; set; }     
        public UIImage Image { get; set; }
       

        public override void Draw (RectangleF bounds, CGContext context, UIView view)
        {
            UIColor.White.SetFill();
            context.FillRect(bounds);           
        }

        public override float Height (RectangleF bounds)
        {
            return 130f;
        }

    }
}

