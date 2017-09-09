using AwtApplication.Models;
using AwtApplication.Params;
using AwtApplication.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;

namespace AwtApplication.Services
{

    public delegate void ShowView() ;
    public class ViewService
    {
        public static bool InBreakoutSession ;
        public static INavigation NavigationService;
        public static void ShowEventDetail( Event _Event, bool _IsModal = false )
        {
            NavigationService.PushAsync(new ProductItemViewPage(_Event, _IsModal));
            SetPageTitle(_Event.CAPTION);
            CommunicationService.CancelAllRequests();
        }
        public async static void ShowReferentList()
        {
            await NavigationService.PushAsync(new TimelinePage());
        }
        public async static void ShowStandList()
        {
            await NavigationService.PushAsync(new StandsList());
        }
        public async static void ShowEventList()
        {
            await NavigationService.PushAsync(new DocumentTimelinePage());
            SetPageTitle(Messages.PAGE_TITLE_TIMELINE_LONG);
        }
        public static void ShowMap()
        {

            switch (Device.OS)
            {
                case TargetPlatform.iOS:
                    Device.OpenUri(
                      new Uri(string.Format("http://maps.apple.com/?q={0}", WebUtility.UrlEncode(Constants.ADRESS_EUROGRESS))));
                    break;
                case TargetPlatform.Android:
                    Device.OpenUri(
                      new Uri(string.Format("geo:0,0?q={0}", WebUtility.UrlEncode(Constants.ADRESS_EUROGRESS))));
                    break;
            }
        }
        public async static void ShowTaxi()
        {
            await NavigationService.PushAsync(new GrialDemoSettings());
        }
        public async static void ShowHomepage()
        {
            await NavigationService.PushAsync(new DashboardMultipleTilesPage());
        }

        public static Page GetFirstPage()
        {
            Page HContent;

            if (AppIsUnlocked())
                HContent = new DashboardMultipleTilesPage();
            else
                HContent = new LockScreen();

            return new NavigationPage(HContent);
        }

        protected static void LoadSingleEventSucess( string _Answer )
        {
            if ( _Answer == "error" )
            {
                NotificationService.ShowAlert(Messages.ERROR,Messages.ERROR_AT_LOADING,Messages.CLOSE);
            } else
            {
                List<Event> HEvent = JsonConvert.DeserializeObject<List<Event>>(_Answer);
                HEvent[0].GenerateReferent();
                ShowEventDetail(HEvent[0]);
            }
        }

        public static void ShowEventDetailByIdent(int _EventIdent)
        {
            CommunicationService.LoadSingleEvent(_EventIdent, new OnCommunicationSuccess(LoadSingleEventSucess) );
        }

        internal async static void ShowAGB()
        {
            await NavigationService.PushAsync(new GenericAboutPage());
        }

        public async static void ShowBreakoutSession( string _DateTime )
        {
            InBreakoutSession = true;
            DateTime HTime = DateTime.ParseExact(_DateTime, Constants.TIME_FORMAT, null);
            await NavigationService.PushAsync(new CategoriesListWithIconsPage( _DateTime ));
            SetPageTitle(Messages.BREAKOUT_FEEDBACK + HTime.ToString("HH:mm") );
        }

        public async static void ShowPersonalEventList()
        {
            await NavigationService.PushAsync(new DocumentTimelinePage(true));
            SetPageTitle(Messages.PAGE_TITLE_MY_TIMELINE_LONG);
        }

        private static bool AppIsUnlocked()
        {
            return FileService.DoesStorageEntryExist(Constants.STORAGE_KEY_CUSTOMER);
        }
        private static void SetPageTitle( string _Title )
        {
            (Application.Current.MainPage as NavigationPage).CurrentPage.Title = _Title;
        }

        public async static void ToggleUI()
        {
            if (!((Application.Current.MainPage as NavigationPage).CurrentPage is ILoadingPage))
            {
                FuturePage.ToggleLoading();
            } else
            {
                ((Application.Current.MainPage as NavigationPage).CurrentPage as ILoadingPage).ToggleLoading();
            }
        }
        public async static void TriggerUIBindings()
        {
            if (!((Application.Current.MainPage as NavigationPage).CurrentPage is ILoadingPage))
            {
                FuturePage.TriggerBindingManually();
            }
            else
            {
                ((Application.Current.MainPage as NavigationPage).CurrentPage as ILoadingPage).TriggerBindingManually();
            }
        }

        public static ILoadingPage FuturePage;
        public static Page BreakoutPage;

        public async static void HandleLoadingError()
        {
            CommunicationService.CancelAllRequests();
            NotificationService.ShowAlert(Messages.ERROR,Messages.LOADING_ERROR,Messages.CLOSE);
            // TODO BACK TO START SCREEN
        }
    }
}
