namespace Nzl.Web.Forms.MobileNewSmth.Controls
{
    using System;
    using System.Windows.Forms;
    using Nzl.Web.Forms.MobileNewSmth.Datas;

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
                this.linklblTop.Text = this.linklblTop.Text + " (" + topic.Replies + ") ";
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
