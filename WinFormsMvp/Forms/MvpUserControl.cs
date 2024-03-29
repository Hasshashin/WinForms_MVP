﻿using System.Windows.Forms;
using WinFormsMvp.Binder;

namespace WinFormsMvp.Forms
{
    public class MvpUserControl<TModel> : UserControl, IView<TModel>
        where TModel : class
    {
        private TModel model;
        private readonly PresenterBinder presenterBinder = new PresenterBinder();

        public MvpUserControl()
        {
            presenterBinder.PerformBinding(this);
        }

        #region Implementation of IView<TModel>

        public TModel Model
        {
            get { return model; }
            set { model = value; }
        }

        #endregion

        #region Implementation of IView

        public bool ThrowExceptionIfNoPresenterBound
        {
            get { return false; } // todo: need to consider the use of this in this new environment!!!
        }

        #endregion
    }
}
