namespace Nzl.Web.Smth.Controls
{
    using System;
    using System.Windows.Forms;
    using Nzl.Web.Smth.Datas;

    /// <summary>
    /// 
    /// </summary>
    public partial class TopControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTopLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        TopControl()
        {
            InitializeComponent();
            this.Height = TopControlHeight;
            this.linklblTop.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblTop_LinkClicked);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public TopControl(Topic topic)
            : this()
        {
            this.Tag = topic;
            this.lblIndex.Text = topic.TopSeq.ToString("00");
            this.linklblTop.Text = topic.Title;
            this.linklblTop.Links.Add(0, this.linklblTop.Text.Length, topic.Uri);
            if (topic.Replies > 0)
            {
                this.lblReplies.Visible = true;
                this.lblReplies.Text = "(" + topic.Replies + ")";
                this.lblReplies.Left = this.linklblTop.Left + this.linklblTop.Width + 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int TopControlHeight
        {
            get
            {
                return 40;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblTop_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnTopLinkClicked != null)
            {
                this.OnTopLinkClicked(sender, e);
                e.Link.Visited = true;
            }
        }
    }
}
