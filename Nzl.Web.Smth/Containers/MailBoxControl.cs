namespace Nzl.Web.Smth.Containers
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

            ///Inbox
            {
                TabPage tp = new TabPage();
                tp.Name = "tpInbox";
                tp.Text = "Inbox";
                XBoxControl xbc = new XBoxControl();
                xbc.Url = this._inboxUrl;
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
                XBoxControl xbc = new XBoxControl();
                xbc.Url = this._sentUrl;
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
                XBoxControl xbc = new XBoxControl();
                xbc.Url = this._trashUrl;
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

        #region override
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            /////Inbox
            //{
            //    TabPage tp = new TabPage();
            //    tp.Name = "tpInbox";
            //    tp.Text = "Inbox";
            //    XBoxControl xbc = new XBoxControl();
            //    xbc.Url = this._inboxUrl;
            //    xbc.SetParent(tp);
            //    xbc.OnMailLinkClicked += Xbc_OnMailLinkClicked;
            //    xbc.OnUserLinkClicked += Xbc_OnUserLinkClicked;
            //    xbc.OnNewMailClicked += Xbc_OnNewMailClicked;
            //    tp.Controls.Add(xbc);
            //    this.tcMailBox.TabPages.Add(tp);
            //    this.Size = new Size(xbc.Width + 8, xbc.Height + 26);
            //}


            //this._timerLoadingTops.Interval = 8 * 1000;
            //this._timerLoadingTops.Tick += new EventHandler(_timerLoadingTops_Tick);
            //this._timerLoadingTops.Start();
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
                xbc.OnMailLinkClicked += Xbc_OnMailLinkClicked;
                xbc.OnUserLinkClicked += Xbc_OnUserLinkClicked;
                xbc.OnNewMailClicked += Xbc_OnNewMailClicked;
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
                xbc.OnMailLinkClicked += Xbc_OnMailLinkClicked;
                xbc.OnUserLinkClicked += Xbc_OnUserLinkClicked;
                xbc.OnNewMailClicked += Xbc_OnNewMailClicked;
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
