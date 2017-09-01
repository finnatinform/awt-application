using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwtApplication.Models
{
    public class Event
    {
        public Event clone()
        {
            return this.MemberwiseClone() as Event;
        }
        public int IDENT
        {
            get;
            set;
        }
        public string CAPTION
        {
            get;
            set;
        }
        public string PLACE
        {
            get;
            set;
        }
        public string DESCRIPTION
        {
            get;
            set;
        }
        public string REFERENT_NAME
        {
            get
            {
                if (HAS_REFERENT)
                {
                    return Referent.FORE_NAME + " " + Referent.SURE_NAME;
                }
                return "";
            }
        }
        public string ICON
        {
            get;
            set;
        }

        public int RATING
        {
            get;
            set;
        }

        public string GRIAL_ICON
        {
            get
            {
                switch ( ICON )
                {
                    case "School": return GrialShapesFont.School;
                    case "Event": return GrialShapesFont.Event;
                    case "Heart": return GrialShapesFont.FavoriteBorder;
                    case "Forum": return GrialShapesFont.Forum;
                    case "Group": return GrialShapesFont.Group;
                    case "Help": return GrialShapesFont.Help;
                    case "Place": return GrialShapesFont.Place;

                    default: return "";
                }
            }
        }

        public string START_DATE
        {
            get;
            set;
        }

        public DateTime START_AS_DATE
        {
            get
            {
                return DateTime.ParseExact(START_DATE,"dd.MM.yyyy HH:mm", null);
            }
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

        public bool IsInbound
        {
            get;
            set;
        }
        public bool CAN_BE_RESERVED
        {
            get;
            set;
        }
        public bool HAS_FEEDBACK
        {
            get;
            set;
        }
        public bool USER_HAS_RESERVED
        {
            get;
            set;
        }
        public bool IS_IN_PAST
        {
            get
            {
                return DateTime.ParseExact(START_DATE, "dd.MM.yyyy HH:mm", null) < DateTime.Now;
            }
        }

        public int DURATION
        {
            get;
            set;
        }

        public bool HAS_REFERENT
        {
            get
            {
                return Referent != null;
            }
        }
        public Referent Referent
        {
            get;
            set;
        }

        public string REFERENT_IMAGE
        {
            get
            {
                if (HAS_REFERENT)
                {
                    return Referent.ImageName;
                }
                return "";           
            }
        }

        public bool GIVE_RATE_OPTION
        {
            get
            {
                return HAS_FEEDBACK && (DateTime.ParseExact(START_DATE, "dd.MM.yyyy HH:mm", null).AddMinutes(this.DURATION) < DateTime.Now);
            }
        }

        public bool GIVE_RESERVE_OPTION
        {
            get
            {
                return CAN_BE_RESERVED && !USER_HAS_RESERVED && !IS_IN_PAST;
            }
        }

        public void GenerateReferent()
        {
            this.Referent = new Referent { FORE_NAME = this.FORE_NAME, SURE_NAME = this.SURE_NAME, DESCRIPTION = "", RANKING = 100 };
        }
    }
}
