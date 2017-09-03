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
            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(Params.Constants.CheckNotificationsInterval);
        }
        public void TriggerBackgroundRunManually()
        {
            BackgroundServiceIOS.DoWork( null);
        }
    }
}