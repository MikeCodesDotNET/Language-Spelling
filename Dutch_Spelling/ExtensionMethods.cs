using System;
using MonoTouch.UIKit;

namespace Dutch_Spelling
{
    public static class FontExensions
    {
        public static UIFont FontSemiBold
        {
            get { return UIFont.FromName("Raleway-SemiBold", 32); }
        }

        public static UIFont FontLight
        {
            get { return UIFont.FromName("Raleway-Light", 18); }
        }

        public static UIFont FontRegular
        {
            get { return UIFont.FromName("Raleway-Regular", 32); }
        }

    }
}