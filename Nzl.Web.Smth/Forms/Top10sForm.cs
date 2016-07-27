namespace Nzl.Web.Smth.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Nzl.Web.Smth.Controls;

    /// <summary>
    /// 
    /// </summary>
    public partial class Top10sForm : Form
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
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Deactivate += new EventHandler(Top10sForm_Deactivate);

            Top10sBrowserControl tbc = new Top10sBrowserControl();
            tbc.Name = "tbcTop10s";
            tbc.Top = 1;
            tbc.Left = 1;
            this.panelContainer.Controls.Add(tbc);
            this.Width = tbc.Width + 2 + this.Width - this.panelContainer.Width;
            this.Height = tbc.Height + 2 + this.Height - this.panelContainer.Height;
            tbc.OnTopLinkClicked += new LinkLabelLinkClickedEventHandler(tbc_OnTopLinkClicked);
            tbc.OnTopBoardLinkClicked += Tbc_OnTopBoardLinkClicked;
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
        private void tbc_OnTopLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnTopLinkClicked != null)
            {
                this.OnTopLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Top10sForm_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
