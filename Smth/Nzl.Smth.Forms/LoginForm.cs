namespace Nzl.Smth.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Windows.Forms;
    using Nzl.Web.Page;
    using Nzl.Web.Util;
    using Nzl.Smth.Datas;
    using Nzl.Smth.Utils;

    /// <summary>
    /// 
    /// </summary>
    public partial class LoginForm : BaseForm
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly LoginForm Instance = new LoginForm();

        /// <summary>
        /// 
        /// </summary>
        LoginForm()
        {
            InitializeComponent();
            this.lcLog.OnLogCompleted += LcLog_LogCompleted;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LcLog_LogCompleted(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
