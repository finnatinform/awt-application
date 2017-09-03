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

        public static void ShowAlert(string _Title, string _Text, string _ButtonText)
        {
            Application.Current.MainPage.DisplayAlert(_Title, _Text, _ButtonText);
        }
    }
}
