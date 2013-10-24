using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PropertyManagemnet.UI.Models;
using WinFormsMvp.Forms;
using WinFormsMvp;
using PropertyManagemnet.UI.Presenters;
using PropertyManagemnet.UI.Views.Base;

namespace PropertyManagemnet.UI.Views
{
    //[PresenterBinding(typeof(PolicyManagementPresenter))]
    public partial class PolicyManagementForm : PolicyManagementBaseForm, IPolicyManagementView
    {
        private Button button1;
    
        public PolicyManagementForm()
        {
            ContainerBootstrapper.Resolve<PolicyManagementPresenter>(new { view = this }); 
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializeComponent();
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 207);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PolicyManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button1);
            this.Name = "PolicyManagementForm";
            this.Text = "PolicyManagementForm";
            this.ResumeLayout(false);

        }

        public event EventHandler ButtonClicked;

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonClicked(sender,e);
        }
    }
}
