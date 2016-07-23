namespace Nzl.Web.Forms.MobileNewSmth.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;
    using Nzl.Web.Forms.MobileNewSmth.Controls;
    using Nzl.Web.Forms.MobileNewSmth.Datas;

    /// <summary>
    /// The topic form.
    /// </summary>
    public partial class TopicForm : Form
    {
        #region Ctors.
        /// <summary>
        /// Ctor.
        /// </summary>
        public TopicForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        public TopicForm(string uri)
            : this()
        {
            this.tbcTopic.SetParent(this);
            this.tbcTopic.TopicUrl = uri;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        public TopicForm(string uri, string userID)
            : this(uri)
        {
            this.tbcTopic.TargetUserID = userID;
        }
        #endregion
    }
}
