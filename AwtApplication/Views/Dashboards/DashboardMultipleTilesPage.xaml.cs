using AwtApplication.Services;
using Xamarin.Forms;

namespace AwtApplication
{
	public partial class DashboardMultipleTilesPage : ContentPage
	{
		public DashboardMultipleTilesPage ()
		{			
			InitializeComponent();

			BindingContext = new DashboardMultipleTilesViewModel ();

            DependencyService.Get<IServiceStarter>().StartServices();
        }
	}
}