namespace Nzl.Smth.Controls.Elements
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
                this.linklblTopic.Text = CommonUtil.ReplaceSpecialChars(topic.Title);
                this.lblReplies.Text = "（" + topic.Replies + "）";
                this.lblReplies.Left = this.linklblTopic.Left + this.linklblTopic.Width + 1;
                this.linklblTopic.Links.Clear();
                this.linklblTopic.Links.Add(0, topic.Title.Length, topic.Uri);
                this.lblCreateDT.Text = topic.CreateDateTime;
                this.lblLastDT.Text = topic.LastThreadDateTime;
                this.linklblCreateID.Text = topic.CreateID;
                this.linklblCreateID.Links.Clear();
                this.linklblCreateID.Links.Add(0, topic.CreateID.Length, topic.CreateID);
                this.linklblLastID.Text = topic.LastThreadID;
                this.linklblLastID.Links.Clear();
                this.linklblLastID.Links.Add(0, topic.LastThreadID.Length, topic.LastThreadID);
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
            if (this.OnTopicLinkClicked != null)
            {
                OnTopicLinkClicked(sender, e);
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
            }
        }
        #endregion
    }
}
