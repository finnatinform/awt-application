using AwtApplication.iOS.Services;
using AwtApplication.Services;

[assembly: Xamarin.Forms.Dependency(typeof(ServiceStarterIOS))]
namespace AwtApplication.iOS.Services
{
    class ServiceStarterIOS : IServiceStarter
    {
        public void StartBackgroundService()
        {
            // TODO
        }
    }
}