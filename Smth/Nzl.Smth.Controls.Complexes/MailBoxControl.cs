namespace Nzl.Smth.Controls.Complexes
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Nzl.Smth.Common;
    using Nzl.Smth.Configurations;
    using Nzl.Smth.Controls.Containers;

    /// <summary>
    /// 
    /// </summary>
    public partial class MailBoxControl : UserControl
    {
        #region event
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnMailLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnUserLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnNewMailClicked;
        #endregion

        #region variable        
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        public MailBoxControl()
        {
            InitializeComponent();

            ///Inbox
            {
                TabPage tp = new TabPage();
                tp.Name = "tpInbox";
                tp.Text = "Inbox";
                MailControlContainer xbc = new MailControlContainer(MailBoxType.Inbox);
                xbc.Url = Configuration.InboxUrl;
                xbc.SetParent(tp);
                xbc.OnMailLinkClicked += Xbc_OnMailLinkClicked;
                xbc.OnUserLinkClicked += Xbc_OnUserLinkClicked;
                xbc.OnNewMailClicked += Xbc_OnNewMailClicked;
                tp.Controls.Add(xbc);
                this.tcMailBox.TabPages.Add(tp);
                this.Size = new Size(xbc.Width + 8, xbc.Height + 26);
            }

            ///Outbox
            {
                TabPage tp = new TabPage();
                tp.Name = "tpOutbox";
                tp.Text = "Outbox";
                MailControlContainer xbc = new MailControlContainer(MailBoxType.Outbox);
                xbc.Url = Configuration.OutboxUrl;
                xbc.SetParent(tp);
                xbc.OnMailLinkClicked += Xbc_OnMailLinkClicked;
                xbc.OnUserLinkClicked += Xbc_OnUserLinkClicked;
                xbc.OnNewMailClicked += Xbc_OnNewMailClicked;
                tp.Controls.Add(xbc);
                this.tcMailBox.TabPages.Add(tp);
            }

            ///Trash
            {
                TabPage tp = new TabPage();
                tp.Name = "tpTrash";
                tp.Text = "Trash";
                MailControlContainer xbc = new MailControlContainer(MailBoxType.Trash);
                xbc.Url = Configuration.TrashUrl;
                xbc.SetParent(tp);
                xbc.OnMailLinkClicked += Xbc_OnMailLinkClicked;
                xbc.OnUserLinkClicked += Xbc_OnUserLinkClicked;
                xbc.OnNewMailClicked += Xbc_OnNewMailClicked;
                tp.Controls.Add(xbc);
                this.tcMailBox.TabPages.Add(tp);
            }


            //this._timerLoadingTops.Interval = 8 * 1000;
            //this._timerLoadingTops.Tick += new EventHandler(_timerLoadingTops_Tick);
            //this._timerLoadingTops.Start();
        }
        #endregion

        #region eventhandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Xbc_OnUserLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnUserLinkClicked != null)
            {
                this.OnUserLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Xbc_OnMailLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnMailLinkClicked != null)
            {
                this.OnMailLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Xbc_OnNewMailClicked(object sender, EventArgs e)
        {
            if (this.OnNewMailClicked != null)
            {
                this.OnNewMailClicked(sender, e);
            }
        }
        #endregion
    }
}
