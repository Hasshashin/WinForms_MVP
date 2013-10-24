using System.ComponentModel;
using System.Windows.Forms;
using WinFormsMvp.Binder;

namespace WinFormsMvp.Forms
{
    public class MvpForm<TModel> : Form, IView<TModel>
        where TModel : class
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        public MvpForm()
        {
            InitializeComponent();
            ThrowExceptionIfNoPresenterBound = true;
        }

           /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Implementation of IView<TModel>

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TModel Model { get; set; }

        #endregion

        #region Implementation of IView

        public bool ThrowExceptionIfNoPresenterBound { get; private set; }

        #endregion
    }
}
