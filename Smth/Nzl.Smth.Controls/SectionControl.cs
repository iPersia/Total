namespace Nzl.Smth.Controls
{
    using System.Windows.Forms;
    using Datas;
    using Utils;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class SectionControl : BaseControl<Section>
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

        public override void Initialize(Section section)
        {
            base.Initialize(section);
            if (section != null)
            {
                this.linklblSection.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblBorS_LinkClicked);
                this.linklblSection.Text = section.Name;
                LinkLabel.Link link = new LinkLabel.Link(0, this.linklblSection.Text.Length, SmthUtil.GetSectionUrl(section.Code));
                if (section.IsBoard)
                {
                    this.lblType.ForeColor = System.Drawing.Color.Black;
                    this.lblType.Text = "版面";
                    link.Tag = "Board";
                }
                else
                {
                    this.lblType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
                    this.lblType.Text = "目录";
                    link.Tag = "Section";
                }

                this.linklblSection.Links.Clear();
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
