
using Android.App;
using Android.Content;
using Android.OS;
using AwtApplication.Params;
using AwtApplication.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace AwtApplication.Droid.Services
{
    [Service]
    class BackgroundService : Service
    {
        private const string Tag = "[BackgroundService]";

        private bool _isRunning;
        private Context _context;
        private Task _task;

        #region overrides

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            _context = this;
            _isRunning = false;
            _task = new Task(DoWork);
        }

        public override void OnDestroy()
        {
            _isRunning = false;

            if (_task != null)
            {
                _task.Dispose();
            }
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _task.Start();
            }
            return StartCommandResult.Sticky;
        }

        #endregion

        private void DoWork()
        {
            string HLastTimeLoaded;
            if ( FileService.DoesStorageEntryExist(Constants.STORAGE_KEY_LAST_LOADED) )
            {
                HLastTimeLoaded = FileService.GetStorageValue(Constants.STORAGE_KEY_LAST_LOADED) as string;
            } else
            {
                HLastTimeLoaded = new DateTime(2015, 1, 1).ToString("dd.MM.yyyy HH:mm");
            }

            AwtApplication.Services.CommunicationService.LoadNotifications(HLastTimeLoaded, new OnLoadNotificationsSuccess(this.OnLoadSuccess));
            FileService.SetStorageEntry(Constants.STORAGE_KEY_LAST_LOADED, HLastTimeLoaded);
        }
        private void OnLoadSuccess(List<Models.Notification> _Answer)
        {
            // Just for Breakpoint
            int HTest = 5;
        }

    }

        //private void ShouldNotificateTimerTick( object _Sender, EventArgs _Event )
        //{
        //    TimeSpan HSpan;
        //    foreach ( Models.Notification HNotification in this.NotificationList)
        //    {
        //        HSpan = DateTime.Parse(HNotification.START_DATE) - DateTime.Now;
        //        if ( HSpan.Days == 0 && 
        //             HSpan.Hours == 0 &&
        //             HSpan.Minutes == 0)
        //        {
        //            // TODO : if active then nur alert
        //            AwtApplication.Services.NotificationService.ShowNotification(HNotification);
        //            this.NotificationList.Remove(HNotification);
        //        }
        //    }
        //}
    
}