using AwtApplication.iOS.Services;
using AwtApplication.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(ServiceStarterIOS))]
namespace AwtApplication.iOS.Services
{
    class ServiceStarterIOS : IServiceStarter
    {
        public void StartServices()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
                );

                UIApplication.SharedApplication.RegisterUserNotificationSettings(notificationSettings);
            }

            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(Params.Constants.CheckNotificationsInterval);
        }
        public void TriggerBackgroundRunManually(bool _AlsoOnAndroid = false)
        {
            BackgroundServiceIOS.DoWork( null);
        }
    }
}