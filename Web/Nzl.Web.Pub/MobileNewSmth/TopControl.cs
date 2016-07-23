namespace Nzl.Web.Pub.MobileNewSmth
{
    using System;
    using System.Windows.Forms;

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
        public TopControl()
        {
            InitializeComponent();
            this.linklblTop.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblTop_LinkClicked);
        }

        /// <summary>
        /// 
        /// </summary>
        public TopControl(int index, string title, string url)
            : this()
        {
            this.lblIndex.Text = index.ToString("00");
            this.linklblTop.Text = title;
            this.linklblTop.Links.Add(0, this.linklblTop.Text.Length, url);
        }

        /// <summary>
        /// 
        /// </summary>
        public TopControl(int index, string title, string url, int replies)
            : this(index, title, url)
        {
            if (replies > 0)
            {
                this.linklblTop.Text = this.linklblTop.Text + " (" + replies + ") ";
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
