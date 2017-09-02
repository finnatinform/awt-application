using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics.Drawables;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using System.Threading.Tasks;

using UXDivers.Artina.Shared;
using UXDivers.Artina.Shared.Droid;

using AwtApplication.Services;

using FFImageLoading.Forms.Droid;
using AwtApplication.Droid.Services;

namespace AwtApplication.Droid
{
	//https://developer.android.com/guide/topics/manifest/activity-element.html
	[Activity(
		Label = "FELIOS Anwendertreffen",
		Icon = "@drawable/icon",
		Theme = "@style/Theme.Splash",
	 	MainLauncher = true,
		LaunchMode = LaunchMode.SingleTask,
		ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize
		)
	]
	public class MainActivity : FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
            // Changing to App's theme since we are OnCreate and we are ready to 
            // "hide" the splash
            base.Window.RequestFeature(WindowFeatures.ActionBar);
			base.SetTheme(Resource.Style.AppTheme);


			FormsAppCompatActivity.ToolbarResource = Resource.Layout.Toolbar;
			FormsAppCompatActivity.TabLayoutResource = Resource.Layout.Tabs;

			base.OnCreate(bundle);

			//Initializing FFImageLoading
			CachedImageRenderer.Init();

			global::Xamarin.Forms.Forms.Init(this, bundle);
			UXDivers.Artina.Shared.GrialKit.Init(this, "AwtApplication.Droid.GrialLicense");

			FormsHelper.ForceLoadingAssemblyContainingType(typeof(UXDivers.Effects.Effects));

            LoadApplication(new App());

            if (this.Intent.HasExtra("BY_EVENT"))
            {
                bool HFromEvent = this.Intent.GetBooleanExtra("BY_EVENT", false);
                if ( HFromEvent )
                {
                    // Dann haben wir einen Event Ident

                } else
                {
                    // Dann haben wir ein Breakoutsession Start-Datum

                }

                // TODO SHOW BREAKOUT_SESSION or Event
            }
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
		{
			base.OnConfigurationChanged(newConfig);

			DeviceOrientationLocator.NotifyOrientationChanged();
		}

    }

}

