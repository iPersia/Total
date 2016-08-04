namespace Nzl.Web.Smth.Forms
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

        /// <summary>
        /// 
        /// </summary>
        public MailBoxForm()
        {
            InitializeComponent();
            this.mbcMailBox.OnMailLinkClicked += MbcMailBox_OnMailLinkClicked;
            this.mbcMailBox.OnUserLinkClicked += MbcMailBox_OnUserLinkClicked;
            this.mbcMailBox.OnNewMailClicked += MbcMailBox_OnNewMailClicked;

            this.HideWhenDeactivate = false;     
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
                UserForm userForm = new UserForm(e.Link.LinkData.ToString());
                userForm.StartPosition = FormStartPosition.CenterParent;
                userForm.ShowDialog(this);
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
                MailDetailForm mailDetailForm = new MailDetailForm(e.Link.LinkData.ToString());
                mailDetailForm.StartPosition = FormStartPosition.CenterParent;
                this.HideWhenDeactivate = false;
                if (mailDetailForm.ShowDialog(this) == System.Windows.Forms.DialogResult.Yes)
                {
                    e.Link.Tag = "Success";
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MbcMailBox_OnNewMailClicked(object sender, EventArgs e)
        {
            ShowFormAsDialog(new NewMailForm());
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        private void ShowFormOnCenterParent(Form form)
        {
            if (form != null && form.IsDisposed == false)
            {
                form.StartPosition = FormStartPosition.Manual;
                int centerX = this.Location.X + this.Size.Width / 2;
                int centerY = this.Location.Y + this.Size.Height / 2;
                form.Location = new System.Drawing.Point(centerX - form.Size.Width / 2, centerY - form.Size.Height / 2);
                form.Show();
                form.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        private void ShowFormAsDialog(Form form)
        {
            if (form != null && form.IsDisposed == false)
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog(this);
            }
        }
    }
}
