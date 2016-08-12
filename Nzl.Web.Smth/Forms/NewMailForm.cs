namespace Nzl.Web.Smth.Forms
{
    using System;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;
    using Nzl.Web.Smth.Common;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class NewMailForm : BaseForm
    {
        /// <summary>
        /// 
        /// </summary>
        private string _postUrl = "http://m.newsmth.net/mail/send";

        /// <summary>
        /// Ctor.
        /// </summary>
        public NewMailForm()
        {
            InitializeComponent();
            this.HideWhenDeactivate = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        public NewMailForm(string userID)
            : this()
        {
            this.txtSendTo.Text = userID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        public NewMailForm(string userID, string title)
            : this(userID)
        {
            this.txtTitle.Text = title;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        public NewMailForm(string userID, string title, string content)
            : this(userID, title)
        {
            this.richtxtContent.Text = content;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                SetCtrlsEnabled(false);
                string errorStr = string.Empty;
                if (CommonUtil.IsMatch("[a-zA-z][a-zA-Z0-9]{1,11}", this.txtSendTo.Text) == false)
                {
                    errorStr += "用户名录入错误！\n";
                }

                if (string.IsNullOrEmpty(this.txtTitle.Text))
                {
                    errorStr += "Title不能为空！\n";
                }

                if (string.IsNullOrEmpty(this.richtxtContent.Text))
                {
                    errorStr += "内容不能为空！\n";
                }

                if (errorStr != string.Empty)
                {
                    MessageForm msgForm = new MessageForm(errorStr);
                    msgForm.StartPosition = FormStartPosition.CenterParent;
                    msgForm.ShowDialog(this);
                    SetCtrlsEnabled(true);
                    return;
                }

                string postData = "id=" + this.txtSendTo.Text
                                + "&title=" + this.txtTitle.Text
                                + "&content=" + this.richtxtContent.Text;
                if (this.ckbBackup.Checked)
                {
                    postData += "&backup=on";
                }

                string result = WebPage.Post(this._postUrl, postData);
                if (result != null)
                {
                    result = CommonUtil.GetMatch(@"<div class=\Wsp hl f\W>(?'Result'\w+)</div>", result, "Result");
                }
                else
                {
                    result = "邮件发送失败！";                    
                }

                MessageForm msgForm2 = new MessageForm(result);
                msgForm2.StartPosition = FormStartPosition.CenterParent;
                msgForm2.ShowDialog(this);
                
                if (result.Contains("成功"))
                {
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }

                SetCtrlsEnabled(true);
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message + "\n" + exp.StackTrace);
                }

#if (DEBUG)
                CommonUtil.ShowMessage(this, exp.Message);
#endif
                this.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        private void SetCtrlsEnabled(bool flag)
        {
            this.txtSendTo.Enabled = flag;
            this.txtTitle.Enabled = flag;
            this.richtxtContent.Enabled = flag;
            this.ckbBackup.Enabled = flag;
            this.btnSend.Enabled = flag;
        }
    }
}
