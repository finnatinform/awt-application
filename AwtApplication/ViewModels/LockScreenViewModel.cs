using Android.Widget;
using AwtApplication.Params;
using AwtApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AwtApplication.ViewModels
{
    public delegate void RemoveFromStack();
    public delegate void ResetView();

    class LockScreenViewModel
    {
        public bool InCheckKey
        {
            get;
            set;
        }
        public RemoveFromStack RemoveCallback;
        public ResetView ResetViewCallback;
        public void TryToUnlock(string _UnlockCode )
        {
            if (String.IsNullOrEmpty(_UnlockCode)||!_UnlockCode.Contains(Constants.UNLOCK_KEY_SEPARATOR+""))
            {
                NotificationService.ShowAlert(Messages.UNLOCK_KEY_ERROR_TITLE, Messages.UNLOCK_KEY_ERROR_TEXT , Messages.OK);
                this.ResetViewCallback?.Invoke();
            } else
            {
                InCheckKey = true;
                String HCustomer = _UnlockCode.Split(Constants.UNLOCK_KEY_SEPARATOR)[0].ToUpper();
                Task.Factory.StartNew(async () => await CommunicationService.CheckUnlockKey(HCustomer , new OnHandleCheckCustomerKey(HandleUnlock)));
            }
        }

        internal void ShowAGB()
        {
            ViewService.ShowAGB();
        }

        private void HandleUnlock( string _CustomerKey )
        {
            switch (_CustomerKey)
            {
                case "error":
                    InCheckKey = false;
                    this.ResetViewCallback?.Invoke();
                    NotificationService.ShowAlert(Messages.ERROR, Messages.UNLOCK_KEY_ERROR, Messages.CLOSE);
                    break;
                case "false":
                    InCheckKey = false;
                    this.ResetViewCallback?.Invoke();
                    NotificationService.ShowAlert(Messages.UNLOCK_KEY_ERROR_TITLE, Messages.UNLOCK_KEY_ERROR_TEXT, Messages.CLOSE);
                    break;
                default:
                    FileService.SetStorageEntry(Constants.STORAGE_KEY_CUSTOMER, _CustomerKey);

                    InCheckKey = false;
                    // If this doesnt work out, it is now that bad
                    FileService.GainUserData();
                    ViewService.ShowHomepage();
                    this.RemoveCallback?.Invoke();
                    break;
            }
        }
    }
}
