using AwtApplication.Services;
using AwtApplication.Views;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AwtApplication
{
	public partial class TimelinePage : ContentPage, ILoadingPage
	{
        private bool InLoading;
        public TimelinePage()
		{
            InitializeComponent();
            this.BindingContext = new TimelineViewModel();
        }

        private TimelineViewModel Controller
        {
            get
            {
                return BindingContext as TimelineViewModel;
            }
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
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
            ReferentListView.ItemsSource = Controller.ReferentList;
        }
    }
}

