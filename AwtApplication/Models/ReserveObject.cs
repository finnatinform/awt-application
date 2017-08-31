using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwtApplication.Models
{
    class ReserveObject
    {
        public int EVENT_IDENT;
        public string USER_IDENT;

        public ReserveObject( int _Event , string _User)
        {
            EVENT_IDENT = _Event;
            USER_IDENT = _User;
        }
    }
}
