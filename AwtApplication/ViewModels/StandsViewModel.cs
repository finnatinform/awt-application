using AwtApplication.Models;
using AwtApplication.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwtApplication
{
    public class StandsViewModel
    {
        public StandsViewModel()
        {
            StandList = new List<Stand>();
        }

        internal void LoadData()
        {
            ViewService.ToggleUI();
            Task.Factory.StartNew(async () => await CommunicationService.LoadStands(new OnLoadStandsSuccess(OnLoadStandsSuccess)));
        }

        private void OnLoadStandsSuccess(List<Stand> _Result)
        {
            StandList = _Result;
            ViewService.ToggleUI();
            ViewService.TriggerUIBindings();
        }

        public List<Stand> StandList
        {
            get;
            set;
        }
    }

}

