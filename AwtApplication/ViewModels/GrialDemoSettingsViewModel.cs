using AwtApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwtApplication.ViewModels
{
    class GrialDemoSettingsViewModel
    {
        public GrialDemoSettingsViewModel()
        {
            this.TaxiList = new List<Taxi>();

            this.TaxiList.Add(
                new Taxi() {
                    CAPTION = "Alfataxi",
                    NUMBER = "024122222"
                }
            );
            this.TaxiList.Add(
                new Taxi()
                {
                    CAPTION = "CityFalke",
                    NUMBER = "024133311"
                }
            );
            this.TaxiList.Add(
                new Taxi()
                {
                    CAPTION = "TAAV",
                    NUMBER = "024166666"
                }
            );
        }
        public List<Taxi> TaxiList
        {
            get;
            set;
        }
    }
}
