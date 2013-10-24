using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PropertyManagemnet.UI.Views;
using WinFormsMvp;
using Castle.Core.Logging;

namespace PropertyManagemnet.UI.Presenters
{
    public class PolicyManagementPresenter : Presenter<IPolicyManagementView>
    {
        public PolicyManagementPresenter(IPolicyManagementView view, ILogger logger)
            : base(view, logger)
        {
            View.ButtonClicked += new EventHandler(View_ButtonClicked);
        }

        void View_ButtonClicked(object sender, EventArgs e)
        {
            Logger.Info("Works !");
        }
    }
}
