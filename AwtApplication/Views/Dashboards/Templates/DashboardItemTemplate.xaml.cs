using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AwtApplication
{
	public partial class DashboardItemTemplate : DashboardItemTemplateBase
	{
		public DashboardItemTemplate ( )
		{
			InitializeComponent();
		}

        public async void OnWidgetTapped(object sender, EventArgs args)
        {
            //ViewService.ShowReferentList();
            await Application.Current.MainPage.DisplayAlert("Tile Tapped!", "You have tapped a \r\n DashboardItemTemplateBase", "OK");
        }
    }
}