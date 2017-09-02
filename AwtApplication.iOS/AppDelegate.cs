using FFImageLoading.Forms.Touch;
using Foundation;
using Lottie.Forms.iOS.Renderers;
using ObjCRuntime;
using System;
using UIKit;
using UXDivers.Artina.Shared;

namespace AwtApplication.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register ("AppDelegate")]
	public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();
			CachedImageRenderer.Init(); // Initializing FFImageLoading
			AnimationViewRenderer.Init(); // Initializing Lottie

			UXDivers.Artina.Shared.GrialKit.Init(new ThemeColors(), "AwtApplication.iOS.GrialLicense");

			// Code for starting up the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			#endif

			FormsHelper.ForceLoadingAssemblyContainingType(typeof(UXDivers.Effects.Effects));
			FormsHelper.ForceLoadingAssemblyContainingType<UXDivers.Effects.iOS.CircleEffect>();

            LoadApplication (new App ());

            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(Params.Constants.CheckNotificationsInterval);

			return base.FinishedLaunching (app, options);
		}

        // WTF: [BlockProxy(typeof(NIDActionArity1V137))] 
        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
        {
            base.PerformFetch(application, completionHandler);

            // Entsprechend einbauen statt stop?
            completionHandler(UIBackgroundFetchResult.NewData);
        }
    }
}