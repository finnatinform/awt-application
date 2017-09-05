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

            if (options != null)
            {
                // check for a local notification
                if (options.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey))
                {
                    var localNotification = options[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
                    if (localNotification != null)
                    {
                        NSObject HByEvent;
                        NSObject HIdent;
                        if (localNotification.UserInfo.TryGetValue(new NSString("BY_EVENT"), out HByEvent))
                        {
                            bool HFromEvent = (bool)(HByEvent as NSNumber);
                            if (HFromEvent)
                            {
                                HIdent = localNotification.UserInfo.ValueForKey(new NSString("EVENT_IDENT"));
                                ViewService.ShowEventDetailByIdent((int)(HIdent as NSNumber));
                            }
                            else
                            {
                                if (localNotification.UserInfo.TryGetValue(new NSString("START_DATE"), out HIdent))
                                {
                                    ViewService.ShowBreakoutSession((HIdent as NSString));
                                }
                            }
                        }
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
            if ( application.ApplicationState==UIApplicationState.Active )
            {
                NSObject HByEvent;
                if (notification.UserInfo!=null)
                {
                    if (notification.UserInfo.TryGetValue(new NSString("BY_EVENT"), out HByEvent))
                    {
                        //Create Alert
                        var okCancelAlertController = UIAlertController.Create(notification.AlertTitle, notification.AlertBody, UIAlertControllerStyle.Alert);

                        //Add Actions
                        okCancelAlertController.AddAction(UIAlertAction.Create(Params.Messages.OK, UIAlertActionStyle.Default, alert => Alertok(notification)));
                        okCancelAlertController.AddAction(UIAlertAction.Create(Params.Messages.CLOSE, UIAlertActionStyle.Cancel, alert => { }));

                        // Present Alert
                        Window.RootViewController.PresentViewController(okCancelAlertController, true, null);
                    }
                }
            }
        }

        private void Alertok(UILocalNotification notification)
        {
            NSObject HByEvent;
            NSObject HIdent;
            if (notification.UserInfo.TryGetValue(new NSString("BY_EVENT"), out HByEvent))
            {
                bool HFromEvent = (bool)(HByEvent as NSNumber);
                if (HFromEvent)
                {
                    HIdent = notification.UserInfo.ValueForKey(new NSString("EVENT_IDENT"));
                    ViewService.ShowEventDetailByIdent((int)(HIdent as NSNumber));
                }
                else
                {
                    if (notification.UserInfo.TryGetValue(new NSString("START_DATE"), out HIdent))
                    {
                        ViewService.ShowBreakoutSession((HIdent as NSString));
                    }
                }
            }
        }
    }
}