namespace Nzl.Smth.Forms
{
    using System;
    using System.Windows.Forms;

    public partial class MailBoxForm : BaseForm
    {
        #region Singleton
        /// <summary>
        /// 
        /// </summary>
        public static readonly MailBoxForm Instance = new MailBoxForm();
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private Form _parentForm = null;

        private Nzl.Smth.Controls.Complexes.MailBoxControl _mbcMails = new Smth.Controls.Complexes.MailBoxControl();

        /// <summary>
        /// 
        /// </summary>
        private MailDetailForm _mailDetailForm = new MailDetailForm();

        /// <summary>
        /// 
        /// </summary>
        public MailBoxForm()
        {
            InitializeComponent();
            this._mbcMails.OnMailLinkClicked += MbcMailBox_OnMailLinkClicked;
            this._mbcMails.OnUserLinkClicked += MbcMailBox_OnUserLinkClicked;
            this._mbcMails.OnNewMailClicked += MbcMailBox_OnNewMailClicked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this._mbcMails.Top = 1;
            this._mbcMails.Left = 1;
            this.panelContainer.Controls.Clear();
            this.panelContainer.Controls.Add(this._mbcMails);
            this.Size = new System.Drawing.Size(this._mbcMails.Width + 2 + this.Width - this.panelContainer.Width,
                                                this._mbcMails.Height + 2 + this.Height - this.panelContainer.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        public void SetParent(Form parent)
        {
            this._parentForm = parent;
        }       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MbcMailBox_OnUserLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                UserForm form  = new UserForm(e.Link.LinkData.ToString());
                form.StartPosition = FormStartPosition.CenterParent;
                this.HideWhenDeactivate = false;
                form.ShowDialog(this._parentForm);
                this.Focus();
                this.HideWhenDeactivate = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MbcMailBox_OnMailLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                this._mailDetailForm.StartPosition = FormStartPosition.CenterParent;
                this._mailDetailForm.Url = e.Link.LinkData.ToString();
                this.HideWhenDeactivate = false;
                if (this._mailDetailForm.ShowDialog(this._parentForm) == System.Windows.Forms.DialogResult.Yes)
                {
                    e.Link.Tag = "Success";
                }

                this.Focus();
                this.HideWhenDeactivate = true;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MbcMailBox_OnNewMailClicked(object sender, EventArgs e)
        {
            NewMailForm form = new NewMailForm();
            form.StartPosition = FormStartPosition.CenterParent;
            this.HideWhenDeactivate = false;
            form.ShowDialog(this._parentForm);
            this.Focus();
            this.HideWhenDeactivate = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MailBoxForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            if (this._parentForm != null)
            {
                this._parentForm.Focus();
            }
        }
    }
}
