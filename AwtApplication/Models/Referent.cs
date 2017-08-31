using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwtApplication.Models
{
    public class Referent
    {
        public string DESCRIPTION
        {
            get;
            set;
        }
        public string FORE_NAME
        {
            get;
            set;
        }
        public string SURE_NAME
        {
            get;
            set;
        }
        public int RANKING
        {
            get;
            set;
        }

        public string ImageName {
            get{
                string HName = FORE_NAME.Replace(' ','_') + "_" + SURE_NAME;
                HName = HName.Replace('ö','_');
                HName = HName.Replace('ü','_');
                HName = HName.Replace('ß', '_');
                return HName + ".jpg";
            }
        }
        public string Name
        {
            get
            {
                return FORE_NAME + " " + SURE_NAME;
            }
        }
    }
}
