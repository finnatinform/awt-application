namespace AwtApplication.Models
{
    public class Stand
    {
        public string CAPTION
        {
            get;
            set;
        }
        public string LOGO
        {
            get;
            set;
        }
        public int RANKING
        {
            get;
            set;
        }
        public string DESCRIPTION
        {
            get;
            set;
        }
        public string GRIAL_ICON
        {
            get
            {
                switch (LOGO)
                {
                    case "School": return GrialShapesFont.School;
                    case "Dashboard": return GrialShapesFont.Dashboard;
                    case "Build": return GrialShapesFont.Build;
                    case "ChatBubble": return GrialShapesFont.ChatBubble;
                    case "Code": return GrialShapesFont.Code;
                    case "DesktopMac": return GrialShapesFont.DesktopMac;
                    case "DesktopWindows": return GrialShapesFont.DesktopWindows;
                    case "FlashOn": return GrialShapesFont.FlashOn;
                    case "Group": return GrialShapesFont.Group;
                    case "Notifications": return GrialShapesFont.Notifications;
                    case "PhoneAndroid": return GrialShapesFont.PhoneAndroid;
                    case "PhoneIphone": return GrialShapesFont.PhoneIphone;
                    case "Place": return GrialShapesFont.Place;
                    case "Settings": return GrialShapesFont.Settings;
                    case "ShoppingCart": return GrialShapesFont.ShoppingCart;
                    case "WebAsset": return GrialShapesFont.WebAsset;
                    default: return "";
                }
            }
        }
    }
}
