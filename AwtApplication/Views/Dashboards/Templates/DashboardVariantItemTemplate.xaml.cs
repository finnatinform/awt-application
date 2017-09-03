using AwtApplication.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AwtApplication
{
	public partial class DashboardVariantItemTemplate : DashboardItemTemplateBase
	{
		
		public DashboardVariantItemTemplate ()
		{
			InitializeComponent ();
		}

        public async void OnTileTapped(object sender, EventArgs args)
        {
            base.OnWidgetTapped(sender,args);
        }
    }

}
