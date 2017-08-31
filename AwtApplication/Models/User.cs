using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.DeviceInfo.Abstractions;

namespace AwtApplication.Models
{
    public class User
    {
        public string IDENT
        {
            get;
            set;
        }
        public string COMPANY
        {
            get;
            set;
        }
        public string MODEL
        {
            get;
            set;
        }
        public string VERSION
        {
            get;
            set;
        }
        public string PLATFORM
        {
            get;
            set;
        }
    }
}
