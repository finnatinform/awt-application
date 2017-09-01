using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AwtApplication.Droid.Services;
using AwtApplication.Params;
using AwtApplication.Services;

[assembly: Xamarin.Forms.Dependency(typeof(ServiceStarterAndroid))]
namespace AwtApplication.Droid.Services
{
    public class ServiceStarterAndroid : IServiceStarter
    {
        public void StartBackgroundService()
        {
            //var HBackgroundIntent = new Intent(Application.Context, typeof(Services.BackgroundService));
            //Application.Context.StartService(HBackgroundIntent);


            var alarmIntent = new Intent(Application.Context, typeof(AlarmReceiver));
            var broadcast = PendingIntent.GetBroadcast(Application.Context, 0, alarmIntent, PendingIntentFlags.NoCreate);
            if (broadcast == null)
            {
                var pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, alarmIntent, 0);
                var alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);
                alarmManager.SetRepeating(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime(), Constants.BackgroundLoadingInterval, pendingIntent);
            }

        }
    }
}