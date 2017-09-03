using System;
using System.Collections.Generic;
using AwtApplication.Models;
using Xamarin.Forms;
using AwtApplication.Services;
using AwtApplication.Params;

namespace AwtApplication
{
	public partial class ProductItemViewPage : ContentPage
	{
        private int HiddenRating;

		public ProductItemViewPage( Event _Event, bool _IsModal )
		{
			InitializeComponent ();

            BindingContext = _Event;
            ShowRating(_Event.RATING);
 
        }

        private void SendFeedback(object sender, EventArgs e)
        {
            CommunicationService.SendFeedback(HiddenRating, (this.BindingContext as Models.Event).IDENT, new OnCommunicationSuccess(this.OnFeedbackAnswer));
        }

        private async void OnFeedbackAnswer( string _Answer )
        {
            if ( _Answer == "success" )
            {
                NotificationService.ShowAlert(Messages.THANK_YOU,Messages.FEEDBACK_THANKS,Messages.CLOSE);
                FeedbackButton.IsVisible = false;
                (BindingContext as Event).RATING = this.HiddenRating; // Jetzt kann es nicht mehr bewertet werden
                DependencyService.Get<IServiceStarter>().TriggerBackgroundRunManually();
                if (ViewService.InBreakoutSession)
                {
                    await Navigation.PopToRootAsync();
                    //ViewService.ShowHomepage();
                    ViewService.InBreakoutSession = false;
                }
            } else
            {
                NotificationService.ShowAlert(Messages.ERROR, Messages.FEEDBACK_ERROR, Messages.CLOSE);
            }
        }

        private void ReserveEvent(object sender, EventArgs e)
        {
            CommunicationService.ReserveEvent( ( BindingContext as Event ).IDENT , new OnCommunicationSuccess(OnReservationSucess) );
        }

        private void OnReservationSucess( string _Answer)
        {
            if ( _Answer == "success" )
            {
                NotificationService.ShowAlert(Messages.SUCCESS, Messages.SUCCESSFULL_RESERVED, Messages.OK);
                (BindingContext as Event).USER_HAS_RESERVED = true;
                ReserveButton.IsVisible = false;
                LabelReserved.IsVisible = true;
                ForceLayout();
            }
        }

        private void ShowRating( int _Rating )
        {
            if (_Rating!=-1)
            {
                Rating1.Value = Convert.ToInt32(_Rating > 0);
                Rating2.Value = Convert.ToInt32(_Rating > 1);
                Rating3.Value = Convert.ToInt32(_Rating > 2);
                Rating4.Value = Convert.ToInt32(_Rating > 3);
                Rating5.Value = Convert.ToInt32(_Rating > 4);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            // Not yet rated
            if ((this.BindingContext as Models.Event).RATING==-1)
            {
                if ( sender == Rating1 )
                {
                    HiddenRating = 1;
                } else if (sender == Rating2 )
                {
                    HiddenRating = 2;
                }
                else if (sender == Rating3)
                {
                    HiddenRating = 3;
                }
                else if (sender == Rating4)
                {
                    HiddenRating = 4;
                }
                else if (sender == Rating5)
                {
                    HiddenRating = 5;
                }

                if (HiddenRating != 0 && (this.BindingContext as Models.Event).GIVE_RATE_OPTION)
                {
                    ShowRating(HiddenRating);
                    // Show Button
                    FeedbackButton.IsVisible = true;
                }
            }

        }
    }
}

