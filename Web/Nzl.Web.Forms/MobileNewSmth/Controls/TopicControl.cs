namespace Nzl.Web.Forms.MobileNewSmth.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Forms.MobileNewSmth.Datas;

    /// <summary>
    /// 
    /// </summary>
    public partial class TopicControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTopicLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnCreateIDLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnLastIDLinkClicked;

        /// <summary>
        /// Ctor.
        /// </summary>
        public TopicControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public TopicControl(Topic topic)
            : this()
        {
            this.Tag = topic;
            if (topic != null)
            {
                this.linklblTopic.Text = CommonUtil.ReplaceSpecialChars(topic.Title) + " （" + topic.Replies + "）";
                this.linklblTopic.Links.Add(0, topic.Title.Length, topic.Uri);
                this.lblCreateDT.Text = topic.CreateDateTime;
                this.lblLastDT.Text = topic.LastThreadDateTime;
                this.linklblCreateID.Text = topic.CreateID;
                this.linklblCreateID.Links.Add(0, topic.CreateID.Length, topic.CreateID);
                this.linklblLastID.Text = topic.LastThreadID;
                this.linklblLastID.Links.Add(0, topic.LastThreadID.Length, topic.LastThreadID);
            }

            this.linklblTopic.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblTopic_LinkClicked);
            this.linklblCreateID.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblCreateID_LinkClicked);
            this.linklblLastID.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblLastID_LinkClicked);
        }

        /// <summary>
        /// 
        /// </summary>
        public override System.Drawing.Color ForeColor
        {
            set
            {
                this.linklblTopic.LinkColor = value;
            }
        }

        #region Event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblTopic_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnTopicLinkClicked != null)
            {
                OnTopicLinkClicked(sender, e);
                e.Link.Visited = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblCreateID_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnCreateIDLinkClicked != null)
            {
                OnCreateIDLinkClicked(sender, e);
                e.Link.Visited = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblLastID_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnLastIDLinkClicked != null)
            {
                OnLastIDLinkClicked(sender, e);
                e.Link.Visited = true;
            }
        }
        #endregion
    }
}
