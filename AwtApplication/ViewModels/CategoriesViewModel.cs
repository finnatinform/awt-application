using AwtApplication.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwtApplication.ViewModels
{
    class CategoriesViewModel
    {
        private string FDateTime;
        public CategoriesViewModel( string _DateTime )
        {
            EventList = new List<Models.Event>();
            FDateTime = _DateTime;
        }

        internal void LoadData()
        {
            ViewService.ToggleUI();
            Task.Factory.StartNew(async () => await CommunicationService.LoadBreakoutSession( FDateTime, new OnLoadEventsSuccess(OnLoadSuccess)));
        }

        private void OnLoadSuccess(List<Models.Event> _Result)
        {
            EventList = _Result;
            ViewService.ToggleUI();
            ViewService.TriggerUIBindings();
        }

        public List<Models.Event> EventList
        {
            get;
            set;
        }
    }
}
