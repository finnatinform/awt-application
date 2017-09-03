using Android.App;
using Android.Content;
using Android.OS;
using AwtApplication.Droid.Services;
using AwtApplication.Services;

[assembly: Xamarin.Forms.Dependency(typeof(ServiceStarterAndroid))]
namespace AwtApplication.Droid.Services
{
    public class ServiceStarterAndroid : IServiceStarter
    {

        public static void StartBackgroundServiceAndroid( Context context )
        {
            Intent alarmIntent = new Intent(context.ApplicationContext, typeof(AlarmReceiver));
            var HBundle = new Bundle();
            HBundle.PutBoolean("IS_PUSH", false);
            alarmIntent.PutExtras(HBundle);
            var broadcast = PendingIntent.GetBroadcast(context.ApplicationContext, 0, alarmIntent, PendingIntentFlags.NoCreate);
            if (broadcast == null)
            {
                var pendingIntent = PendingIntent.GetBroadcast(context.ApplicationContext, 0, alarmIntent, 0);
                var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
                alarmManager.SetRepeating(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime(), Params.Constants.LoadDataInterval, pendingIntent);
            }
        }

        public void StartServices()
        {
            StartBackgroundServiceAndroid(Application.Context);
        }

        public void TriggerBackgroundRunManually()
        {
            // No Need
        }
    }
}