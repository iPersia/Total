using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nzl.Web.Forms.Common
{
    public partial class NavigatorForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private Uri _uri = null;

        /// <summary>
        /// 
        /// </summary>
        public NavigatorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public NavigatorForm(Uri uri, string title)
            : this()
        {
            this.Text = title;
            this._uri = uri;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigatorForm_Load(object sender, EventArgs e)
        {
            this.wbNav.Navigate(this._uri);
        }
    }
}
