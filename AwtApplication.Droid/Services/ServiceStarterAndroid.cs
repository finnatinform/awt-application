using Android.App;
using Android.Content;
using Android.Widget;
using AwtApplication.Droid.Services;
using AwtApplication.Services;

[assembly: Xamarin.Forms.Dependency(typeof(ServiceStarterAndroid))]
namespace AwtApplication.Droid.Services
{
    public class ServiceStarterAndroid : IServiceStarter
    {
        public void StartBackgroundService()
        {
            var HBackgroundIntent = new Intent(Application.Context, typeof(Services.BackgroundService));
            Application.Context.StartService(HBackgroundIntent);
        }
    }
}