namespace Nzl.Web.Smth.Controls
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class BoardControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnLinkClicked;

        /// <summary>
        /// Ctor.
        /// </summary>
        public BoardControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public BoardControl(string type, string url, string title)
            : this()
        {
            this.lblType.Text = type;
            this.linklblBorS.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblBorS_LinkClicked);
            this.linklblBorS.Text = title;
            this.linklblBorS.Tag = type;
            LinkLabel.Link link = new LinkLabel.Link(0, this.linklblBorS.Text.Length, url);
            link.Tag = type;
            this.linklblBorS.Links.Add(link);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblBorS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnLinkClicked != null)
            {
                OnLinkClicked(sender, e);
                e.Link.Visited = true;
            }
        }
    }
}
