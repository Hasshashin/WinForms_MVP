using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinFormsMvp;

namespace PropertyManagemnet.UI.Views
{
    public interface IPolicyManagementView : IView
    {
        event EventHandler ButtonClicked;
    }
}
