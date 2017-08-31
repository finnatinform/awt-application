using System;
using System.Collections.Generic;
using AwtApplication.ViewModels;
using Xamarin.Forms;
using AwtApplication.Services;
using AwtApplication.Params;

namespace AwtApplication
{
	public partial class LockScreen : ContentPage
	{
        private LockScreenViewModel Controller
        {
            get
            {
                return BindingContext as LockScreenViewModel;
            }
        }
		public LockScreen()
		{
			InitializeComponent ();
            BindingContext = new LockScreenViewModel();
            
            // Modal Page
            NavigationPage.SetHasNavigationBar(this, false);
            Controller.RemoveCallback = new RemoveFromStack(this.RemoveFromStack);
            Controller.ResetViewCallback = new ResetView(this.ResetView);

            UnlockButton.IsEnabled = false;
        }
        public void OnTryToUnlock(object sender, EventArgs e)
        {
            if (!Controller.InCheckKey)
            {
                UnlockButton.Text = Messages.UNLOCK_KEY_CHECKING;
                Controller.TryToUnlock(EntryUnlockApp.Text);
            }       
        }
        private void RemoveFromStack()
        {
            Navigation.RemovePage(this);
        }

        private void ResetView()
        {
            UnlockButton.Text = Messages.UNLOCK_APP;
        }

        private void OnReadAGB(object sender, EventArgs e)
        {
            Controller.ShowAGB();
        }

        private void EntryCodeChanged(object sender, TextChangedEventArgs e)
        {
            UnlockButton.IsEnabled = !String.IsNullOrEmpty(EntryUnlockApp.Text);
        }
    }
}