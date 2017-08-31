
using Android.App;
using Android.Content;
using Android.OS;
using AwtApplication.Params;
using AwtApplication.Services;
using System;
using System.Collections.Generic;
using System.Timers;

namespace AwtApplication.Droid.Services
{
    [Service]
    public class BackgroundService : Service
    {
        private Timer DataLoadTimer;
        private Timer ShouldNotificateTimer;

        private DateTime LastTimeLoaded;

        private List<Models.Notification> NotificationList;

        // Is periodic service
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
        {
            this.DataLoadTimer = new Timer();
            this.DataLoadTimer.Elapsed += new ElapsedEventHandler( DataLoadTimerTick );
            this.DataLoadTimer.Interval = Constants.BackgroundLoadingInterval;
            this.DataLoadTimer.Start();

            // Init Last Loaded
            if (FileService.DoesStorageEntryExist(Constants.STORAGE_KEY_LAST_LOADED))
                this.LastTimeLoaded = DateTime.Parse(FileService.GetStorageValue(Constants.STORAGE_KEY_LAST_LOADED) as string);
            else
                this.LastTimeLoaded = new DateTime(2015, 1, 1);

            if (FileService.DoesStorageEntryExist(Constants.STORAGE_KEY_NOTIFICATIONS))
                this.NotificationList = FileService.GetStorageValue(Constants.STORAGE_KEY_NOTIFICATIONS) as List<Models.Notification>;
            else
                this.NotificationList = new List<Models.Notification>();

            this.ShouldNotificateTimer = new Timer();
            this.ShouldNotificateTimer.Elapsed += new ElapsedEventHandler( ShouldNotificateTimerTick );
            this.ShouldNotificateTimer.Interval = Constants.BackgroundNotificationInterval;
            this.ShouldNotificateTimer.Start();

            return StartCommandResult.Sticky;
        }


        private void DataLoadTimerTick( object _Sender, EventArgs _Event )
        {
            //CommunicationService.LoadNotifications();
            AwtApplication.Services.CommunicationService.LoadNotifications( new OnLoadNotificationsSuccess(this.OnLoadSuccess) );

            // Now set to this
            this.LastTimeLoaded = DateTime.Now;

            FileService.SetStorageEntry(Constants.STORAGE_KEY_LAST_LOADED,this.LastTimeLoaded);
            FileService.SetStorageEntry(Constants.STORAGE_KEY_NOTIFICATIONS,this.NotificationList);
        }

        private void OnLoadSuccess(List<Models.Notification> _Answer)
        {
            // TODO_ filtern auf last loaded
            this.NotificationList.AddRange(_Answer);
        }

        private void ShouldNotificateTimerTick( object _Sender, EventArgs _Event )
        {
            TimeSpan HSpan;
            foreach ( Models.Notification HNotification in this.NotificationList)
            {
                HSpan = DateTime.Parse(HNotification.START_DATE) - DateTime.Now;
                if ( HSpan.Days == 0 && 
                     HSpan.Hours == 0 &&
                     HSpan.Minutes == 0)
                {
                    // TODO : if active then nur alert
                    AwtApplication.Services.NotificationService.ShowNotification(HNotification);
                    this.NotificationList.Remove(HNotification);
                }
            }
        }
    }
}