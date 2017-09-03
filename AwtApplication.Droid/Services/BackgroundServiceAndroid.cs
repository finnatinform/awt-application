using Android.App;
using Android.Content;
using Android.OS;
using AwtApplication.Params;
using AwtApplication.Services;
using Newtonsoft.Json;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AwtApplication.Droid.Services
{
    [Service]
    class BackgroundServiceAndroid : Service
    {
        private const string Tag = "[BackgroundServiceAndroid]";

        private bool _isRunning;
        private Context _context;
        private Task _task;
        private static int Count ;

        #region overrides

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            try
            {
                _context = this;
                _isRunning = false;
                _task = new Task(DoWork);
            }
            catch ( Exception e )
            {
                // Should not show
            }
        }

        public override void OnDestroy()
        {
            try
            {
                _isRunning = false;

                if (_task != null)
                {
                    _task.Dispose();
                }
            }
            catch ( Exception e )
            {
                // Should not show
            }
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            try
            {
                if (!_isRunning)
                {
                    _isRunning = true;
                    _task.Start();
                }
            }
            catch ( Exception _E )
            {
                StopSelf();
            }

            return StartCommandResult.Sticky;
        }

        #endregion

        private async Task LoadData( string _LastLoaded )
        {
            try
            {
                
                string HData = JsonConvert.SerializeObject(new Models.PersonalTimelineObject { IDENT = CrossDeviceInfo.Current.Id, LAST_EDITED = _LastLoaded });
                HttpClient HClient = new HttpClient();
                HClient.BaseAddress = new Uri("http://" + Constants.SERVER_URL + '/');
                HClient.DefaultRequestHeaders.Accept.Clear();
                HClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HClient.Timeout = TimeSpan.FromSeconds(30);

                var HPostContent = new StringContent(HData, Encoding.UTF8, "application/json");
                HttpResponseMessage HResponse = HClient.PostAsync("notifications/list", HPostContent).Result;
                string HContent = await HResponse.Content.ReadAsStringAsync();
                if (HContent == "error")
                {
                    throw new Exception();
                }
                else
                {
                    HandleAnswer(HContent);
                }
            }
            catch ( Exception e )
            {
                StopSelf();
            }
        }

        private void HandleAnswer( string _Answer )
        {
            List<Models.Notification> HNotifications = new List<Models.Notification>();
            HNotifications = JsonConvert.DeserializeObject<List<Models.Notification>>(_Answer);
            CheckDuplicates(HNotifications);
            // TEMP
            AwtApplication.Services.NotificationService._Notifications.AddRange(HNotifications);
        }
        private void CheckDuplicates( List<Models.Notification> _NewItems )
        {
            foreach ( Models.Notification HNotification in _NewItems )
            {
                // Delete Duplicates
                AwtApplication.Services.NotificationService._Notifications.RemoveAll( n => n.IDENT.Equals(HNotification.IDENT) );
            }
        }

        private string GetLastLoaded()
        {
            DateTime HLast = DateTime.ParseExact(Constants.LastLoadedInitial, Constants.TIME_FORMAT, null);
            DateTime HTemp;
            foreach ( Models.Notification HNotification in AwtApplication.Services.NotificationService._Notifications)
            {
                HTemp = DateTime.ParseExact(HNotification.LAST_EDITED, Constants.TIME_FORMAT, null);
                if ( HTemp > HLast )
                {
                    HLast = HTemp;
                }
            }
            return HLast.ToString(Constants.TIME_FORMAT);
        }

        private bool ShouldLoadData
        {
            get
            {
                return Count == 1;
            }
        }

        private void UpdateCount()
        {
            Count++;
            if ( UpdateCount > 5 )
            {
                UpdateCount = 1;
            }
        }

        private async void DoWork()
        {
            try
            {
                UpdateCount();
                // First get last loaded
                string HLastLoaded = GetLastLoaded();
                // synchronized
                await LoadData( HLastLoaded );

                CheckNotifications();
            }
            catch (Exception e)
            {
                // Clear Loaded Notifications
                AwtApplication.Services.NotificationService._Notifications.Clear();
            }
            finally
            {
                StopSelf();
            }
        }
        private void CheckNotifications()
        {
            TimeSpan HSpan;
            foreach (Models.Notification HNotification in AwtApplication.Services.NotificationService._Notifications)
            {
                HSpan = DateTime.ParseExact(HNotification.START_DATE, Constants.TIME_FORMAT, null) - DateTime.Now;
                if (HSpan.Days == 0 &&
                     HSpan.Hours == 0 &&
                     HSpan.Minutes == 0 &&
                     HSpan.Seconds <= 0)
                {
                    ShowNotification(HNotification);
                    // Not important,
                    AwtApplication.Services.NotificationService._Notifications.Remove(HNotification);
                }
            }
        }

        private void ShowNotification(Models.Notification _Notification)
        {
            Intent HClickIntent = new Intent(_context, (typeof(MainActivity)));
            HClickIntent.PutExtra("BY_EVENT", _Notification.BY_EVENT);
            if (_Notification.BY_EVENT)
            {
                HClickIntent.PutExtra("EVENT_IDENT",_Notification.EVENT_IDENT);
            } else
            {
                HClickIntent.PutExtra("EVENT_IDENT", _Notification.EVENT_IDENT);
            }

            // Wir werden nicht mehr als 10000 notifications haben
            int HPendingIntentIdPre = _Notification.IDENT + 10000;
            PendingIntent HPendingIntent = PendingIntent.GetActivity(_context, HPendingIntentIdPre, HClickIntent, PendingIntentFlags.OneShot);


            Android.App.Notification.Builder HBuilder = new Android.App.Notification.Builder(_context)
                .SetContentIntent(HPendingIntent)
                .SetContentTitle(_Notification.CAPTION)
                .SetContentText(_Notification.DESCRIPTION)
                .SetDefaults(NotificationDefaults.Vibrate)
                .SetSmallIcon(Resource.Drawable.icon);

            Android.App.Notification HNotification = HBuilder.Build();

            NotificationManager HNotificationManager = _context.GetSystemService(MainActivity.NotificationService) as NotificationManager;

            HNotificationManager.Notify(_Notification.IDENT, HNotification);
        }
    }
}