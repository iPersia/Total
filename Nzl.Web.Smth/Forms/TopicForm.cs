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
    using Nzl.Web.Smth.Controls;
    using Nzl.Web.Smth.Datas;

    /// <summary>
    /// The topic form.
    /// </summary>
    public partial class TopicForm : BaseForm
    {
        #region Ctors.
        /// <summary>
        /// Ctor.
        /// </summary>
        public TopicForm()
        {
            InitializeComponent();
            this.HideWhenDeactivate = false;
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
