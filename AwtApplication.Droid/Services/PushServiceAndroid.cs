using AwtApplication.Services;
using AwtApplication.Droid.Services;
using Android.App;
using Android.Content;

[assembly: Xamarin.Forms.Dependency(typeof(PushServiceAndroid))]
namespace AwtApplication.Droid.Services
{
    class PushServiceAndroid : IPushService
    {
        public void ShowPushNotification( Models.Notification _Notification )
        {
            Intent HClickIntent = new Intent(Application.Context,(typeof(MainActivity)));
            HClickIntent.PutExtra("BY_EVENT",_Notification.BY_EVENT);

            // Wir werden nicht mehr als 10000 events haben
            int HPendingIntentIdPre = _Notification.IDENT + 10000;
            PendingIntent HPendingIntent = PendingIntent.GetActivity(Application.Context, HPendingIntentIdPre, HClickIntent,PendingIntentFlags.OneShot);


            Notification.Builder HBuilder = new Notification.Builder(Application.Context)
                .SetContentIntent(HPendingIntent)
                .SetContentTitle(_Notification.CAPTION)
                .SetContentText(_Notification.DESCRIPTION)
                .SetDefaults(NotificationDefaults.Vibrate)
                .SetSmallIcon(Resource.Drawable.icon);

            Notification HNotification = HBuilder.Build();

            NotificationManager HNotificationManager = Application.Context.GetSystemService(MainActivity.NotificationService) as NotificationManager;

            HNotificationManager.Notify( _Notification.IDENT, HNotification );
        }
    }
}