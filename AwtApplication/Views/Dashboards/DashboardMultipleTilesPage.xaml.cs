using AwtApplication.Services;
using Xamarin.Forms;

namespace AwtApplication
{
	public partial class DashboardMultipleTilesPage : ContentPage
	{
		public DashboardMultipleTilesPage ()
		{			
			InitializeComponent();

            DependencyService.Get<IServiceStarter>().StartServices();
        }

        protected override void OnAppearing()
        {
            BindingContext = new DashboardMultipleTilesViewModel();
        }
	}
}