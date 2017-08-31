using System;
using System.Collections.Generic;
using AwtApplication.Models;
using AwtApplication.Services;
using System.Threading.Tasks;
using AwtApplication.Views;

namespace AwtApplication
{
	public class DocumentTimelineViewModel
	{
        private bool IsPersonalTimeline;
        public DocumentTimelineViewModel( bool _IsPersonalTimeline )
		{
            this.IsPersonalTimeline = _IsPersonalTimeline;
            DocumentTimelineList = new List<Event>();
		}

        internal void LoadData()
        {
            ViewService.ToggleUI();
            if ( IsPersonalTimeline )
            {
                Task.Factory.StartNew(async () => await CommunicationService.LoadPersonalTimeline(new OnLoadEventsSuccess(OnLoadEventsSuccess)));
            } else
            {
                Task.Factory.StartNew(async () => await CommunicationService.LoadTimeline(new OnLoadEventsSuccess(OnLoadEventsSuccess)));
            }
        }

        private void OnLoadEventsSuccess(List<Event> _Result)
        {
            DocumentTimelineList = _Result;
            ViewService.ToggleUI();
            ViewService.TriggerUIBindings();
        }

        public List<Event> DocumentTimelineList { get; set; }
	}
}
