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
                    NUMBER = "024122222",
                    ADDRESS = "Tempelhofer Str.16"
                }
            );
            this.TaxiList.Add(
                new Taxi()
                {
                    CAPTION = "CityFalke",
                    NUMBER = "024133311",
                    ADDRESS = "Wurmbenden 27"
                }
            );
            this.TaxiList.Add(
                new Taxi()
                {
                    CAPTION = "TAAV",
                    NUMBER = "024166666",
                    ADDRESS = "Bendelstr. 28-30"
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
