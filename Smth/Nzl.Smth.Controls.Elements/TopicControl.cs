﻿namespace Nzl.Smth.Controls.Elements
{
    using System.Windows.Forms;
    using Nzl.Smth.Controls.Base;
    using Nzl.Smth.Datas;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public partial class TopicControl : BaseControl<Topic>
    {
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnPostLinkClicked;

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
            this.linklblTopic.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblTopic_LinkClicked);
            this.linklblCreateID.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblCreateID_LinkClicked);
            this.linklblLastID.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblLastID_LinkClicked);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        public override void Initialize(Topic topic)
        {
            base.Initialize(topic);
            if (topic != null)
            {
                
                if (topic.Type == TopicBrowserType.Classic)
                {
                    this.lblReplies.Visible = false;
                    this.linklblCreateID.Visible = false;
                    this.lblLastDT.Visible = false;

                    this.InitializeLabel(this.lblCreateDT, topic.CreateDateTime.Replace(" ", "      "));
                    this.InitializeLinkLabel(this.linklblTopic, CommonUtil.ReplaceSpecialChars(topic.Title), topic.Uri);
                    this.InitializeLinkLabel(this.linklblLastID, topic.CreateID, topic.CreateID);
                }
                else
                {
                    this.lblReplies.Visible = true;
                    this.linklblCreateID.Visible = true;
                    this.lblLastDT.Visible = true;

                    this.InitializeLinkLabel(this.linklblTopic, CommonUtil.ReplaceSpecialChars(topic.Title), topic.Uri);
                    this.InitializeLabel(this.lblReplies, "（" + topic.Replies + "）");
                    this.lblReplies.Left = this.linklblTopic.Left + this.linklblTopic.Width + 1;
                    this.InitializeLabel(this.lblCreateDT, topic.CreateDateTime);
                    this.InitializeLabel(this.lblLastDT, topic.LastThreadDateTime);
                    this.InitializeLinkLabel(this.linklblCreateID, topic.CreateID, topic.CreateID);
                    this.InitializeLinkLabel(this.linklblLastID, topic.LastThreadID, topic.LastThreadID);
                }                
            }
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
            if (this.Data.Type == TopicBrowserType.Subject)
            {
                if (this.OnTopicLinkClicked != null)
                {
                    this.OnTopicLinkClicked(sender, e);
                }
            }
            else
            {                
                if (this.OnPostLinkClicked != null)
                {
                    this.OnPostLinkClicked(sender, e);
                }
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
                this.OnCreateIDLinkClicked(sender, e);
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
               this. OnLastIDLinkClicked(sender, e);
            }
        }
        #endregion
    }
}
