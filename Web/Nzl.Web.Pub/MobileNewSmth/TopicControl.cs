namespace Nzl.Web.Pub.MobileNewSmth
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Nzl.Web.Util;

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
        public TopicControl(string topicUrl, string topicTitle, int count, string createDT, string createID, string lastDT, string lastID)
            : this()
        {
            if (topicTitle == null)
            {
                this.linklblTopic.Text = "Error - No Title was found!";
                this.linklblTopic.VisitedLinkColor = Color.Red;
                this.linklblTopic.LinkVisited = true;
            }
            else
            {
                this.linklblTopic.Text = CommonUtil.ReplaceSpecialChars(topicTitle) + " （" + count + "）";
                this.linklblTopic.Links.Add(0, topicTitle.Length, topicUrl);
            }
            
            this.lblCreateDT.Text = createDT;
            this.lblLastDT.Text = lastDT;

            if (createID == null)
            {
                this.linklblCreateID.Text = "异常";
                this.linklblCreateID.VisitedLinkColor = Color.Red;
                this.linklblCreateID.LinkVisited = true;
            }
            else
            {
                this.linklblCreateID.Text = createID;
                this.linklblCreateID.Links.Add(0, createID.Length, createID);
            }

            if (lastID == null)
            {
                this.linklblLastID.Text = "异常";
                this.linklblLastID.VisitedLinkColor = Color.Red;
                this.linklblLastID.LinkVisited = true;
            }
            else
            {
                this.linklblLastID.Text = lastID;
                this.linklblLastID.Links.Add(0, lastID.Length, lastID);
            }

            this.linklblTopic.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblTopic_LinkClicked);
            this.linklblCreateID.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblCreateID_LinkClicked);
            this.linklblLastID.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblLastID_LinkClicked);
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
