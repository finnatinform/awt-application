using AwtApplication.Models;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AwtApplication.Services
{
    public class NotificationService
    {
        public static List<Models.Notification> _Notifications = new List<Models.Notification>();
        public static bool AppIsInForeground;
        public static async void ShowNotification( Notification _Notification)
        {
            //CrossLocalNotifications.Current.Show(_Notification.CAPTION,_Notification.DESCRIPTION);
            if ( AppIsInForeground )
            {
                bool HOK = await ShowAlert(_Notification.CAPTION, _Notification.DESCRIPTION, "Feedback abgeben", "Abbrechen");
                if (HOK)
                {
                    _Notification.ON_SUCCESS?.Invoke();
                }
            } else
            {
                // TODO
                //DependencyService.Get<IPushService>().ShowPushNotification(_Notification);
            }           
        }

        public static async Task<bool> ShowAlert(string _Title, string _Text, string _ButtonText, string _Button2Text = "")
        {
            if (_Button2Text == "" )
            {
                Application.Current.MainPage.DisplayAlert(_Title, _Text, _ButtonText);
                return true;
            } else
            {
                bool HResult = await Application.Current.MainPage.DisplayAlert(_Title, _Text, _ButtonText, _Button2Text);
                return HResult;
            }
        }
    }
}
