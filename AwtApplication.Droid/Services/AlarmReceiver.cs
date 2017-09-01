using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AwtApplication.Droid.Services
{
    [BroadcastReceiver]
    class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var backgroundServiceIntent = new Intent(context, typeof(BackgroundService));
            context.StartService(backgroundServiceIntent);
        }
    }
}