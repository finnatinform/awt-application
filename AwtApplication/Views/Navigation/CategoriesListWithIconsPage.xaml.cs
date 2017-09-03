using AwtApplication.ViewModels;
using AwtApplication.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AwtApplication.Services;
using AwtApplication.Views;

namespace AwtApplication
{
	public partial class CategoriesListWithIconsPage : ContentPage, ILoadingPage
    {
        private bool InLoading;
        private CategoriesViewModel Controller
        {
            get
            {
                return BindingContext as CategoriesViewModel;
            }
        }

        public CategoriesListWithIconsPage ( string _DateTime )
		{
			InitializeComponent ();

			BindingContext = new CategoriesViewModel( _DateTime );
            NavigationPage.SetHasBackButton(this, false);
        }

		private async void OnItemTapped(Object sender, ItemTappedEventArgs e)
		{
            Event HSelectedEvent = (sender as ListView).SelectedItem as Event;
            if (HSelectedEvent.HAS_FEEDBACK || HSelectedEvent.CAN_BE_RESERVED)
            {
                ViewService.BreakoutPage = this;
                ViewService.ShowEventDetail(HSelectedEvent,true);
            }
		}

        protected override void OnAppearing()
        {
            ViewService.FuturePage = this;
            Controller.LoadData();
        }
        public void ToggleLoading()
        {
            InLoading = !InLoading;
            PageLoading.IsVisible = InLoading;
            PageContent.IsVisible = !InLoading;
        }
        public void TriggerBindingManually()
        {
            EventListView.ItemsSource = Controller.EventList;
        }

        protected override bool OnBackButtonPressed()
        {
            CommunicationService.CancelAllRequests();
            return base.OnBackButtonPressed();
        }
    }
}

