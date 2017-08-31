using AwtApplication.Services;
using AwtApplication.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using AwtApplication.Views;

namespace AwtApplication
{
	public partial class DocumentTimelinePage : ContentPage,ILoadingPage
	{
        private bool InLoading;
        private bool IsPersonalTimeline;
        private DocumentTimelineViewModel Controller
        {
            get
            {
                return BindingContext as DocumentTimelineViewModel;
            }
        }

        public DocumentTimelinePage( bool _IsPersonalTimeline = false )
		{
            this.IsPersonalTimeline = _IsPersonalTimeline;
			InitializeComponent ();
			BindingContext = new DocumentTimelineViewModel( _IsPersonalTimeline );
		}

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Event HSelectedEvent = (sender as ListView).SelectedItem as Event;
            if ( HSelectedEvent.HAS_FEEDBACK || HSelectedEvent.CAN_BE_RESERVED )
            {
                ViewService.ShowEventDetail(HSelectedEvent);
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
            EventListView.ItemsSource = Controller.DocumentTimelineList;
        }

        protected override bool OnBackButtonPressed()
        {
            CommunicationService.CancelAllRequests();
            return base.OnBackButtonPressed();
        }
    }
}

