using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwtApplication.Params
{
    public static class Constants
    {
        public static string SERVER_URL = "192.168.0.59:9000"; // ""; //ec2-52-59-201-86.eu-central-1.compute.amazonaws.com:9000
        public static string STORAGE_KEY_CUSTOMER = "STORAGE_KEY_CUSTOMER" ;
        public static string STORAGE_KEY_USER = "STORAGE_KEY_USER";
        public static char UNLOCK_KEY_SEPARATOR = '@';

        public static string FEEDBACK_MAIL = "charlotte.schmetz@inform-software.com";
        public static string FEEDBACK_MAIL_SUBJECT = "Feedback%20zum%20Anwendertreffen%202017";
        public static string FEEDBACK_MAIL_BODY = "";

        public static int BackgroundLoadingInterval = 30000; // 10 Minuten Test 0,5 Minuten
        public static int BackgroundNotificationInterval = 60000; // 1 Minute genauigkeit für Notifications
        public static string ADRESS_EUROGRESS = "Eurogress Aachen"; // Monheimsallee 48, 52062 Aachen

        public static string STORAGE_KEY_LAST_LOADED = "STORAGE_KEY_LAST_LOADED";
        public static string STORAGE_KEY_NOTIFICATIONS = "STORAGE_KEY_NOTIFICATIONS";
        public static string STORAGE_KEY_USER_NOT_SEND = "STORAGE_KEY_USER_NOT_SEND";
    }
}
