using NUnit.Framework;
using System;
using Xamarin.UITest;
using System.IO;
using System.Reflection;

namespace TestCloud
{
    [TestFixture()]
    public class UITest 
    {
        IApp _app;

        Xamarin.UITest.iOS.iOSApp app = null;

        public string PathToIPA { get; set; }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            string currentFile = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            FileInfo fi = new FileInfo(currentFile);
            string dir = fi.Directory.Parent.Parent.Parent.FullName;
            PathToIPA = Path.Combine(dir, "DutchSpelling", "bin", "iPhoneSimulator", "Debug", "DutchSpelling.app");
        }

        [SetUp]
        public void SetUp()
        {
            // an API key is required to publish on Xamarin Test Cloud for remote, multi-device testing
            // this works fine for local simulator testing though
           
            app = ConfigureApp.iOS.AppBundle(PathToIPA).Debug().EnableLocalScreenshots().StartApp();
        }

        [Test()]
        public void WillValidateCorrectAnswer()
        {
            app.WaitForElement(x => x.Text("Colours"));
            app.Screenshot("Given I'm on the vocab list"); 

            app.Tap(x => x.Text("Colours"));
            app.Screenshot("Then I select Colours");

            app.WaitForElement(x => x.Class("WordView"));
            app.Screenshot("And then I memorise the new word");

            app.WaitForElement(x => x.Text("Skip"));
            app.Screenshot("An then I start the start");

            //Find the current word value! 
            app.Tap(x => x.Text("k"));
            app.Tap(x => x.Text("l"));
            app.Tap(x => x.Text("e"));
            app.Tap(x => x.Text("u"));
            app.Tap(x => x.Text("r"));

            app.Screenshot("And then I enter the text values");

            app.WaitForElement(x => x.Text("Next"));
            app.Screenshot("And then I click Next");

            app.Tap(x => x.Text("Back"));
            app.Screenshot("And then I go back to the vocab list");
        }


        [Test()]
        public void RemoveCharFromAnswer()
        {
            app.WaitForElement(x => x.Text("Colours"));
            app.Screenshot("Given I'm on the vocab list"); 

            app.Tap(x => x.Text("Colours"));
            app.Screenshot("Then I select Colours");

            app.WaitForElement(x => x.Class("WordView"));
            app.Screenshot("And then I memorise the new word");

            app.WaitForElement(x => x.Class("WordView").Child().Text("English"));

            app.Tap(x => x.Text("k"));

            app.Screenshot("And then I start entering the answer");

            app.Tap(x => x.Text("<"));

            app.Screenshot("And then I remove the letter");

        }

        [Test()]
        public void SwipeToReturnToList()
        {
            app.WaitForElement(x => x.Text("Colours"));
            app.Screenshot("Given I'm on the vocab list");

            app.Tap(x => x.Text("Colours"));
            app.Screenshot("Then I select Colours");

            app.WaitForElement(x => x.Class("WordView").Child().Text("English"));
            app.Screenshot("And then I'm ready to start the test");

            app.DragCoordinates(100, 100, 100, 500);

            app.WaitForElement(x => x.Text("Colours"));
            app.Screenshot("Then I return to the vocab list");
        }
    }
}

