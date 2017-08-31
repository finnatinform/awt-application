using System;
using AwtApplication.ViewModels;
using Xamarin.Forms;

namespace AwtApplication
{
    public partial class GrialDemoSettings : ContentPage
	{
		public GrialDemoSettings()
		{
			InitializeComponent();
            this.BindingContext = new GrialDemoSettingsViewModel();
		}
        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }
    }
}
