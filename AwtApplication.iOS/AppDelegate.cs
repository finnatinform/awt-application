using AwtApplication.iOS.Services;
using AwtApplication.Services;
using FFImageLoading.Forms.Touch;
using Foundation;
using Lottie.Forms.iOS.Renderers;
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

            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
                );

                app.RegisterUserNotificationSettings(notificationSettings);
            }

            if (options != null)
            {
                // check for a local notification
                if (options.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey))
                {
                    var localNotification = options[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
                    if (localNotification != null)
                    {
                        // TODO
                    }
                }
            }

            return base.FinishedLaunching (app, options);
		}

        // WTF: [BlockProxy(typeof(NIDActionArity1V137))] Action<...
        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> _CompletionHandler)
        {
            BackgroundServiceIOS.DoWork( _CompletionHandler );
        }
        public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
        {
            NSObject HByEvent;
            NSObject HIdent;
            if (notification.UserInfo.TryGetValue(new NSString("BY_EVENT"), out HByEvent))
            {
                bool HFromEvent = (bool) (HByEvent as NSNumber);
                if ( HFromEvent )
                {
                    HIdent = notification.UserInfo.ValueForKey(new NSString("EVENT_IDENT"));
                    ViewService.ShowEventDetailByIdent((int) ( HIdent as NSNumber));
                } else
                {
                    HIdent = notification.UserInfo.ValueForKey(new NSString("START_DATE"));
                    ViewService.ShowBreakoutSession((HIdent as NSString));
                }
            }
        }
    }
}