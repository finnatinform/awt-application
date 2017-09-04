using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwtApplication.Models
{
    public delegate void OnNotificationAccept();

    public class Notification
    {
        public string DESCRIPTION;
        public string CAPTION;

        public Notification(string _Description, string _Caption)
        {
            this.DESCRIPTION = _Description;
            this.CAPTION = _Caption;
        }

        public string START_DATE;
        public string LAST_EDITED;
        public int EVENT_IDENT;
        public bool BY_EVENT;
        public int IDENT;
        public string EVENT_DATE;
    }
}
