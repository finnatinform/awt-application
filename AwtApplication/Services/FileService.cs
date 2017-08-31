using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwtApplication.Models;
using Xamarin.Forms;
using Plugin.DeviceInfo;
using AwtApplication.Params;

namespace AwtApplication.Services
{
    public class FileService
    {
        public static bool DoesStorageEntryExist(string _Key)
        {
            if (Application.Current.Properties.TryGetValue(_Key, out object HValue))
            {
                return !String.IsNullOrEmpty(HValue as string);
            }
            return false;
        }

        public static object GetStorageValue(string _Key)
        {
            if (Application.Current.Properties.TryGetValue(_Key, out object HValue))
            {
                return HValue;
            } else
            {
                throw new KeyNotFoundException();
            }       
        }

        public static void SetStorageEntry(string _Key, object _Value)
        {
            Application.Current.Properties.Add(_Key,_Value);
        }
        internal static User GetUserData()
        {
            return new User
            {
                IDENT = CrossDeviceInfo.Current.Id,
                MODEL = CrossDeviceInfo.Current.Model,
                VERSION = CrossDeviceInfo.Current.Version,
                PLATFORM = CrossDeviceInfo.Current.Platform.ToString(),
                COMPANY = (GetStorageValue(Constants.STORAGE_KEY_CUSTOMER) as string)
            };
        }
        internal static void GainUserData()
        {
            User HThisUser = GetUserData();
            SetStorageEntry(Constants.STORAGE_KEY_USER, HThisUser.IDENT);
            Task.Factory.StartNew(async () => await CommunicationService.SaveUser(HThisUser));
        }
        internal static void RemoveStorageEntry( string _Key )
        {
            Application.Current.Properties.Remove(_Key);
        }
    }
}
