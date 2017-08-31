using AwtApplication.Models;
using AwtApplication.Services;
using AwtApplication.Views;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwtApplication
{
	public class TimelineViewModel 
    {
		public TimelineViewModel()
		{
            ReferentList = new List<Referent>();
		}

        internal void LoadData()
        {
            ViewService.ToggleUI();
            Task.Factory.StartNew(async () => await CommunicationService.LoadReferents( new OnLoadReferentsSuccess(OnLoadReferentsSuccess) ));
        }
        
        private void OnLoadReferentsSuccess( List<Referent> _Result )
        {
            ReferentList = _Result;
            ViewService.ToggleUI();
            ViewService.TriggerUIBindings();
        }

        public List<Referent> ReferentList {
            get;
            set;
        }
	}

}

