using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwtApplication.Models
{
    class UnlockObject
    {
        public UnlockObject(string _Key)
        {
            this.UNLOCK_KEY = _Key;
        }
        public string UNLOCK_KEY;
    }
}
