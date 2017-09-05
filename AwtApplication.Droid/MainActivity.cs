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
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            if (intent.HasExtra("BY_EVENT"))
            {
                bool HFromEvent = intent.GetBooleanExtra("BY_EVENT", false);
                if (HFromEvent)
                {
                    int HEventIdent = intent.GetIntExtra("EVENT_IDENT", -1);
                    ViewService.ShowEventDetailByIdent(HEventIdent);
                }
                else
                {
                    if ( intent.HasExtra("START_DATE") )
                    {
                        // Dann haben wir ein Breakoutsession Start-Datum
                        string HBreakoutStartDate = intent.GetStringExtra("START_DATE");
                        ViewService.ShowBreakoutSession(HBreakoutStartDate);
                    }
                }
            }
        }
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
                    int HEventIdent = this.Intent.GetIntExtra("EVENT_IDENT",-1);
                    ViewService.ShowEventDetailByIdent(HEventIdent);
                } else
                {
                    if (this.Intent.HasExtra("START_DATE"))
                    {
                        // Dann haben wir ein Breakoutsession Start-Datum
                        string HBreakoutStartDate = this.Intent.GetStringExtra("START_DATE");
                        ViewService.ShowBreakoutSession(HBreakoutStartDate);
                    }
                }
            }
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
		{
			base.OnConfigurationChanged(newConfig);

			DeviceOrientationLocator.NotifyOrientationChanged();
		}

    }

}

