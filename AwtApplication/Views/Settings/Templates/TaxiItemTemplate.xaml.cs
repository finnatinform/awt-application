using AwtApplication.Models;
using AwtApplication.Services;
using System;

using Xamarin.Forms;

namespace AwtApplication
{
    public partial class TaxiItemTemplate : ContentView
	{
		public TaxiItemTemplate()
		{
			InitializeComponent();
		}

        private void OnCallTaxi(object sender, EventArgs e)
        {
            DialService.CallNumber((this.BindingContext as Taxi).NUMBER);
        }
    }
}

