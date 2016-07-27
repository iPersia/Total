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
    using Controls;

    public partial class TestForm : BaseForm
    {
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
        public TestForm()
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
            ///Inbox
            {
                TabPage tp = new TabPage();
                tp.Name = "tpInbox";
                tp.Text = "Inbox";
                this.tcMail.TabPages.Add(tp);

                MailBoxControl mbc = new MailBoxControl();
                mbc.Width = tp.Width - 8;
                mbc.Top = 4;
                mbc.Left = 4;
                mbc.Url = this._inboxUrl;
                mbc.BorderStyle = BorderStyle.FixedSingle;
                tp.Controls.Add(mbc);                

                this.Height = mbc.Height + 65 + 8;
            }

            ///Outbox
            {
                TabPage tp = new TabPage();
                tp.Name = "tpOutbox";
                tp.Text = "Outbox";
                this.tcMail.TabPages.Add(tp);

                MailBoxControl mbc = new MailBoxControl();
                mbc.Url = this._sentUrl;
                mbc.Width = tp.Width - 10;
                mbc.Top = 4;
                mbc.Left = 4;
                tp.Controls.Add(mbc);                
            }

            ///Trash
            {
                TabPage tp = new TabPage();
                tp.Name = "tpTrash";
                tp.Text = "Trash";
                this.tcMail.TabPages.Add(tp);

                MailBoxControl mbc = new MailBoxControl();
                mbc.Url = this._trashUrl;
                mbc.Width = tp.Width - 10;
                mbc.Top = 4;
                mbc.Left = 4;
                tp.Controls.Add(mbc);                
            }
        }
    }
}
