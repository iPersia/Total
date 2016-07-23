namespace Nzl.Web.Smth.Forms
{
    using System;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;

    /// <summary>
    /// 
    /// </summary>
    public partial class UserForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private string _userID;

        /// <summary>
        /// 
        /// </summary>
        public UserForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public UserForm(string userID)
            : this()
        {
            this._userID = userID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Text = "Query User - " + this._userID;
            LoadUserInfor();

            {
                int rowCount = txtUser.GetLineFromCharIndex(txtUser.SelectionStart) + 2;
                this.Height = this.txtUser.Font.Height * rowCount + (this.Height - this.txtUser.Height);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadUserInfor()
        {
            if (string.IsNullOrEmpty(this._userID))
            {
                this.txtUser.AppendText("\n\t没有指定用户ID！");
                return;
            }

            WebPage userInforPage = WebPageFactory.CreateWebPage("http://m.newsmth.net/user/query/" + this._userID);
            if (userInforPage != null && userInforPage.IsGood)
            {
                string userInfor = CommonUtil.GetMatch(@"<li>[\w, \W]+</li>", userInforPage.Html);
                if (string.IsNullOrEmpty(userInfor) == false)
                {
                    userInfor = userInfor.Replace(@"<li>", "\t");
                    Regex objReg = new System.Text.RegularExpressions.Regex(@"[\n]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                    userInfor = objReg.Replace(userInfor, "");
                    userInfor = userInfor.Replace(@"</li>", "\n\n");
                    objReg = new System.Text.RegularExpressions.Regex("(<[^>]+?>)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                    userInfor = objReg.Replace(userInfor, "");
                    userInfor = CommonUtil.ReplaceSpecialChars(userInfor);
                    userInfor.TrimEnd('\n');
                    this.txtUser.AppendText("\n");
                    this.txtUser.AppendText(userInfor);
                    this.txtUser.AppendText("\n");
                    return;
                }
            }

            this.txtUser.AppendText("\n\t没有查询到用户'" + this._userID + "'的信息！");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendMail_Click(object sender, EventArgs e)
        {
            NewMailForm newMailForm = new NewMailForm(this._userID);
            newMailForm.StartPosition = FormStartPosition.CenterParent;
            newMailForm.ShowDialog(this);
        }
    }
}
