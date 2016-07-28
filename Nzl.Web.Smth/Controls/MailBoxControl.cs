namespace Nzl.Web.Smth.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public partial class MailBoxControl : UserControl
    {
        #region event
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnMailLinkClick;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnUserLinkClick;
        #endregion

        #region variable
        /// <summary>
        /// 
        /// </summary>
        private string _inboxUrl = "http://m.newsmth.net/mail/inbox";

        /// <summary>
        /// 
        /// </summary>
        private string _sentUrl = "http://m.newsmth.net/mail/outbox";

        /// <summary>
        /// 
        /// </summary>
        private string _trashUrl = "http://m.newsmth.net/mail/deleted";

        /// <summary>
        /// 
        /// </summary>
        private Timer _timerLoadingTops = new Timer();

        /// <summary>
        /// 
        /// </summary>
        private int _sectionIndex = 1;
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        public MailBoxControl()
        {
            InitializeComponent();
        }
        #endregion

        #region override
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ///Inbox
            {
                TabPage tp = new TabPage();
                tp.Name = "tpInbox";
                tp.Text = "Inbox";
                XBoxControl xbc = new XBoxControl();
                xbc.Url = this._inboxUrl;
                xbc.SetParent(tp);
                xbc.OnMailLinkClick += Xbc_OnMailLinkClick;
                xbc.OnUserLinkClick += Xbc_OnUserLinkClick;
                tp.Controls.Add(xbc);
                this.tcMailBox.TabPages.Add(tp);
                this.Size = new Size(xbc.Width + 8, xbc.Height + 26);
            }


            this._timerLoadingTops.Interval = 8 * 1000;
            this._timerLoadingTops.Tick += new EventHandler(_timerLoadingTops_Tick);
            this._timerLoadingTops.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timerLoadingTops_Tick(object sender, EventArgs e)
        {
            if (this._sectionIndex > 2)
            {
                this._timerLoadingTops.Stop();
                return;
            }

            ///Outbox
            if (this._sectionIndex == 1)
            {
                TabPage tp = new TabPage();
                tp.Name = "tpOutbox";
                tp.Text = "Outbox";
                XBoxControl xbc = new XBoxControl();
                xbc.Url = this._sentUrl;
                xbc.SetParent(tp);
                xbc.OnMailLinkClick += Xbc_OnMailLinkClick;
                xbc.OnUserLinkClick += Xbc_OnUserLinkClick;
                tp.Controls.Add(xbc);
                this.tcMailBox.TabPages.Add(tp);
            }

            ///Trash
            if (this._sectionIndex == 2)
            {
                TabPage tp = new TabPage();
                tp.Name = "tpTrash";
                tp.Text = "Trash";
                XBoxControl xbc = new XBoxControl();
                xbc.Url = this._trashUrl;
                xbc.SetParent(tp);
                xbc.OnMailLinkClick += Xbc_OnMailLinkClick;
                xbc.OnUserLinkClick += Xbc_OnUserLinkClick;
                tp.Controls.Add(xbc);
                this.tcMailBox.TabPages.Add(tp);
            }

            this._sectionIndex++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Xbc_OnUserLinkClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnUserLinkClick != null)
            {
                this.OnUserLinkClick(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Xbc_OnMailLinkClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnMailLinkClick != null)
            {
                this.OnMailLinkClick(sender, e);
            }
        }
        #endregion
    }
}
