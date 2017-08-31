
using AwtApplication.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AwtApplication.Services
{
    class DialService
    {
        public static void CallNumber(string _PhoneNumber)
        {
            Device.OpenUri(new Uri("tel://"+_PhoneNumber));
        }
        private static void SendMail( string _Mail, string _Subject )
        {
            Device.OpenUri(new Uri("mailto:"+_Mail+"?subject="+_Subject));
        }
        public static void OpenContactMail ()
        {
            SendMail(Constants.FEEDBACK_MAIL, Constants.FEEDBACK_MAIL_SUBJECT);
        }
    }
}
