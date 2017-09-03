using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Plugin.DeviceInfo;
using System.Net.Http;

namespace AwtApplication.iOS.Services
{
    class BackgroundServiceIOS
    {
        public static async void DoWork(Action<UIBackgroundFetchResult> _CompletionHandler)
        {
            try
            {
                // First get last loaded
                string HLastLoaded = GetLastLoaded();
                // synchronized
                await LoadData(HLastLoaded);

                CheckNotifications();
                _CompletionHandler(UIBackgroundFetchResult.NewData);
            }
            catch (Exception e)
            {
                // Clear Loaded Notifications
                AwtApplication.Services.NotificationService._Notifications.Clear();
                _CompletionHandler(UIBackgroundFetchResult.Failed);
            }
        }
        private static async Task LoadData(string _LastLoaded)
        {
            try
            {
                string HData = JsonConvert.SerializeObject(new Models.PersonalTimelineObject { IDENT = CrossDeviceInfo.Current.Id, LAST_EDITED = _LastLoaded });
                HttpClient HClient = new HttpClient();
                HClient.BaseAddress = new Uri("http://" + Params.Constants.SERVER_URL + '/');
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
            catch (Exception e)
            {
                // TODO entsprechende UI Handler aufrufen als Callbackmethode
            }
        }

        private static void HandleAnswer(string _Answer)
        {
            List<Models.Notification> HNotifications = new List<Models.Notification>();
            HNotifications = JsonConvert.DeserializeObject<List<Models.Notification>>(_Answer);
            CheckDuplicates(HNotifications);
            // TEMP
            AwtApplication.Services.NotificationService._Notifications.AddRange(HNotifications);
        }
        private static void CheckDuplicates(List<Models.Notification> _NewItems)
        {
            foreach (Models.Notification HNotification in _NewItems)
            {
                // Delete Duplicates
                AwtApplication.Services.NotificationService._Notifications.RemoveAll(n => n.IDENT.Equals(HNotification.IDENT));
            }
        }

        private static string GetLastLoaded()
        {
            DateTime HLast = DateTime.ParseExact(Params.Constants.LastLoadedInitial, Params.Constants.TIME_FORMAT, null);
            DateTime HTemp;
            foreach (Models.Notification HNotification in AwtApplication.Services.NotificationService._Notifications)
            {
                HTemp = DateTime.ParseExact(HNotification.LAST_EDITED, Params.Constants.TIME_FORMAT, null);
                if (HTemp > HLast)
                {
                    HLast = HTemp;
                }
            }
            return HLast.ToString(Params.Constants.TIME_FORMAT);
        }

        private static void CheckNotifications()
        {
            TimeSpan HSpan;
            foreach (Models.Notification HNotification in AwtApplication.Services.NotificationService._Notifications)
            {
                HSpan = DateTime.ParseExact(HNotification.START_DATE, Params.Constants.TIME_FORMAT, null) - DateTime.Now;
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

        public static void ShowNotification(Models.Notification _Notification)
        {
            var HUINotification = new UILocalNotification();
            NSDateFormatter HFormatter = new NSDateFormatter();
            HFormatter.DateFormat = Params.Constants.TIME_FORMAT;
            HUINotification.FireDate = HFormatter.Parse(_Notification.START_DATE);
            HUINotification.AlertTitle = _Notification.CAPTION;
            HUINotification.AlertBody = _Notification.DESCRIPTION;
            HUINotification.AlertAction = "ViewAlert";

            UIApplication.SharedApplication.ScheduleLocalNotification(HUINotification);
        }
    }
}