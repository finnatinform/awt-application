using AwtApplication.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwtApplication.ViewModels
{
    class CategoriesViewModel
    {
        public CategoriesViewModel()
        {
            EventList = new List<Models.Event>();
        }

        internal void LoadData()
        {
            ViewService.ToggleUI();
            Task.Factory.StartNew(async () => await CommunicationService.LoadTimeline(new OnLoadEventsSuccess(OnLoadSuccess)));
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
