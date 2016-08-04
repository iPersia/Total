namespace Nzl.Web.Smth.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;
    using Nzl.Web.Smth.Datas;
    using Nzl.Web.Smth.Controls;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class TestForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private string _url = null;
        
        #region Ctor
        /// <summary>
        /// Ctor.
        /// </summary>
        public TestForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public TestForm(string url)
            : this()
        {
            this._url = url;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            MailDetailControl mdc = new MailDetailControl(this._url);
            mdc.Left = 1;
            mdc.Top = 1;
            mdc.SetParentControl(this);
            this.Size = new Size(mdc.Width + 2, mdc.Height + 2);
            this.panelContainer.Controls.Add(mdc);
        }        
        #endregion
    }
}
