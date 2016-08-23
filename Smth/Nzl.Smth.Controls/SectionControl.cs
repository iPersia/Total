namespace Nzl.Smth.Controls.Elements
{
    using System.Windows.Forms;
    using Nzl.Smth.Controls.Base;
    using Nzl.Smth.Datas;
    using Nzl.Smth.Utils;

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
            this.linklblSection.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblBorS_LinkClicked);
        }

        public override void Initialize(Section section)
        {
            base.Initialize(section);
            if (section != null)
            {                
                this.linklblSection.Text = section.Name;
                LinkLabel.Link link = null;
                if (section.IsBoard)
                {
                    link = new LinkLabel.Link(0, this.linklblSection.Text.Length, SmthUtil.GetBoardUrl(section.Code));
                    this.lblType.ForeColor = System.Drawing.Color.Black;
                    this.lblType.Text = "版面";
                    this.Tag = "Board";
                }
                else
                {
                    link = new LinkLabel.Link(0, this.linklblSection.Text.Length, SmthUtil.GetSectionUrl(section.Code));
                    this.lblType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
                    this.lblType.Text = "目录";
                    this.Tag = "Section";
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
