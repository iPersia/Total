namespace Nzl.Web.Smth.Controls
{
    using System.Windows.Forms;
    using Datas;
    using Utils;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class SectionControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnLinkClicked;

        /// <summary>
        /// Ctor.
        /// </summary>
        public SectionControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public SectionControl(Section section)
            : this()
        {
            if (section != null)
            {
                this.linklblSection.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblBorS_LinkClicked);
                this.linklblSection.Text = section.Name;
                LinkLabel.Link link = new LinkLabel.Link(0, this.linklblSection.Text.Length, SmthUtil.GetSectionUrl(section.Code));
                link.Tag = "Section";
                this.linklblSection.Links.Add(link);
            }
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
            }
        }
    }
}
