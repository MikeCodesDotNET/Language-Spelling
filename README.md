#Language Spelling 
![](http://i1.wp.com/micjames.co.uk/wp-content/uploads/2014/09/screen22.png) 

###Introduction
Language spelling is an open-source(MIT licensed) Xamarin.iOS project created to help you learn a learn new language. I originally developed it to help in my learning of Dutch, its architected in a way that allows it to be easily extended to new languages if you wish. 

This is technically the third version of the project having completely redesigned the user interface with each big release. This release contains lots of animations and is mostly created entirely with C# rather than Storyboards or Xibs. Much of the original code can still be found in the solution so you might find areas that need a little refactoring (it will be on one of my lists somewhere). You are of course welcome to make changes and fix areas that you need attention. 

###Installation
To get started with Language Spelling you're going to need a Xamarin.iOS license. If you've already got an Indie license or higher then you're good to start exploring this solution. Alternatively you can signup to a free 30 day trial [here](www.xamarin.com/download)

###Implementation
The solution is broken into two distinct projects in order to keep a high degree of separation between a particular language variety and the game  implementation. This approach makes it extremely fast to produce a new version of the app in a different language. 

The project by default uses Xamarin.Insights to collect uncaught exceptions. It does not at this time track or identify any user information. If you wish to continue using Xamarin.Insights then you will need to generate your own unique key as I've removed this from the public repository. 

####Create a variant with another language  
If you're looking to create a French version, you will need to do the following: 

* Create a new Xamarin.iOS Project within the solution [Named Spelling.French]
*  Add a reference to **Spelling.Core.iOS** 
*  Add the following to the AppDelegate of the new project:
 ```csharp
	   const string TargetLanguage = "French";
        const string NativeLanguage = "English";
```
* Add the following to the **FinishedLaunching** override: 
 ```csharp
 Client.Initialize(TargetLanguage, NativeLanguage);
            var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
            window.RootViewController = storyboard.InstantiateInitialViewController();
```
	     
* Add a new folder named **vocabulary** to the resources folder
*  Add translations to the new vocabulary directory. 

You should now have enough in place to try your new language spelling App. You will of course also need to create the unique artwork and also ensure the provisioning profiles are setup correctly. However It should be noted that you will never need to edit the Core.iOS project unless you want to customise the Apps gameplay or look.

### Visual Studio Migration Tips
If you're attempting to migrate the classic-api branch to the Unified API using Visual Studio, you may find that it doesn't convert 100% of the API changes for you. The main error you'll see is RectangleF to CGRect which is very easy to resolve. 

### Related resources 
* More info [here](http://micjames.co.uk/dutch-spelling/)
* About Xamarin [here](www.xamarin.com)