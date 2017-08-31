using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwtApplication.Models;

namespace AwtApplication.ViewModels
{
    class ProductItemViewModel
    {
        private Event FEvent;

        public ProductItemViewModel(Event _Event)
        {
            FEvent = _Event;
        }
    }
}
