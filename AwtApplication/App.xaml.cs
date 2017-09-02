using AwtApplication.Params;
using AwtApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AwtApplication
{
	//[XamlCompilation (XamlCompilationOptions.Skip)]
	public partial class App : Application
	{
		public static MasterDetailPage MasterDetailPage;

		public App()
		{
			InitializeComponent();

            MainPage = ViewService.GetFirstPage();

			MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.White);
            // AWT App
            ViewService.NavigationService = MainPage.Navigation;
            if ( FileService.DoesStorageEntryExist(Constants.STORAGE_KEY_USER_NOT_SEND) )
            {
                FileService.GainUserData();
            }
		}

        protected override void OnStart()
        {
            base.OnStart();
            NotificationService.AppIsInForeground = true;
        }
        protected override void OnSleep()
        {
            base.OnStart();
            NotificationService.AppIsInForeground = false;
        }
        protected override void OnResume()
        {
            base.OnStart();
            NotificationService.AppIsInForeground = true;
        }
    }
}
