using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwtApplication.Services
{
    public interface IPushService
    {
        void ShowPushNotification(Models.Notification _Notification);
    }
}
