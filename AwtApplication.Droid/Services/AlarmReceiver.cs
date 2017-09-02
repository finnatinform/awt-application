using Android.Content;

namespace AwtApplication.Droid.Services
{
    [BroadcastReceiver]
    class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var HIntent = new Intent(context, typeof(BackgroundServiceAndroid));
            
            context.StartService(HIntent);
        }
    }
}