using System;
using MonoTouch.UIKit;

namespace Spelling.Core
{
    static class Extensions
    {
        public static int RandomNumberBetween(double minimum, double maximum)
        {
            var random = new Random();
            return Convert.ToInt16(random.NextDouble()*(maximum - minimum) + minimum);
        }
    }
}