using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nzl.Web.Forms.MobileNewSmth.Forms
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public MainForm()
        {
            InitializeComponent();            
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            TabbedBrowserForm.Instance.SetParent(this);
            TabbedBrowserForm.Instance.Show();
            TabbedBrowserForm.Instance.Focus();
        }        
    }
}
