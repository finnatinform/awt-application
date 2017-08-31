using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwtApplication.Views
{
    public interface ILoadingPage
    {
        void ToggleLoading();
        void TriggerBindingManually();
    }
}
