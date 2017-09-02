
using Android.App;
using Android.Content;

namespace AwtApplication.Droid.Services
{
    [BroadcastReceiver]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    public class BootBroadcast : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            // Call Android directly
            ServiceStarterAndroid.StartBackgroundServiceAndroid( context );
        }
    }
}