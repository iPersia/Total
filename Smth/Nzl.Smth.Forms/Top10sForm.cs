namespace Nzl.Smth.Forms
{
    using System;
    using System.Windows.Forms;
    using Nzl.Smth.Controls.Complexes;

    /// <summary>
    /// 
    /// </summary>
    public partial class Top10sForm : BaseForm
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly Top10sForm Instance = new Top10sForm();

        #region Event
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTopLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTopBoardLinkClicked;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        Top10sForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Top10sBrowserControl tbc = new Top10sBrowserControl();
            tbc.Name = "tbcTop10s";
            tbc.Top = 1;
            tbc.Left = 1;
            tbc.OnTopBoardLinkClicked += Tbc_OnTopBoardLinkClicked;
            tbc.OnTopLinkClicked += Tbc_OnTopLinkClicked;
            this.panelContainer.Controls.Add(tbc);
            this.Width = tbc.Width + 2 + this.Width - this.panelContainer.Width;
            this.Height = tbc.Height + 2 + this.Height - this.panelContainer.Height;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbc_OnTopBoardLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnTopBoardLinkClicked != null)
            {
                this.OnTopBoardLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbc_OnTopLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnTopLinkClicked != null)
            {
                this.OnTopLinkClicked(sender, e);
            }
        }
    }
}
