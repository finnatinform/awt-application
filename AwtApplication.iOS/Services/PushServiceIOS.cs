using AwtApplication.iOS.Services;
using AwtApplication.Models;
using AwtApplication.Services;

[assembly: Xamarin.Forms.Dependency(typeof(PushServiceIOS))]
namespace AwtApplication.iOS.Services
{
    class PushServiceIOS : IPushService
    {
        public void ShowPushNotification(Notification _Notification)
        {
            // TODO
        }
    }
}
