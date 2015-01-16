using Foundation;
using UIKit;
using Spelling;

namespace DutchSpelling
{
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        //App Defaults
        const string TargetLanguage = "Dutch";
        const string NativeLanguage = "English";

        UIWindow window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            window = new UIWindow(UIScreen.MainScreen.Bounds);

            Client.Initialize(TargetLanguage, NativeLanguage);

            var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
            window.RootViewController = (UIViewController) storyboard.InstantiateInitialViewController();
            window.MakeKeyAndVisible();

            return true;
        }
    }
}