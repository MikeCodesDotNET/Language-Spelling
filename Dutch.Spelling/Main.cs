using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Mindscape.Raygun4Net;

namespace Spelling.Dutch
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            RaygunClient.Attach("WpAnopmQri3MxfhbGvkDLw==");
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
