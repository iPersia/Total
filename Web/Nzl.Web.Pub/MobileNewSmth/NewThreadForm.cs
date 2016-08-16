namespace Nzl.Web.Pub.MobileNewSmth
{
    using System;
    using System.Windows.Forms;
    using Nzl.Web.Page;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public partial class NewThreadForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private string _subject;

        /// <summary>
        /// 
        /// </summary>
        private string _postUrl;

        /// <summary>
        /// 
        /// </summary>
        private string _topic;

        /// <summary>
        /// 
        /// </summary>
        public NewThreadForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postUrl"></param>  
        /// <param name="subject"></param>
        public NewThreadForm(string topic, string postUrl, string subject)
            : this()
        {
            this._topic = topic;
            this._postUrl = postUrl;
            this._subject = subject;
            this.Text = this._topic;
            this.txtTitle.Text = subject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postUrl"></param>  
        /// <param name="subject"></param>
        public NewThreadForm(string topic, string postUrl, string subject, string content, bool isShowSendMail)
            : this(topic, postUrl, subject)
        {
            this.ckbSendMail.Visible = isShowSendMail;
            this.txtContent.Text = content;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.txtContent.Focus();
        }

        /// <summary>
        /// Thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtContent.Text) == false)
            {
                string postStr = "content=" + this.txtContent.Text;
                if (Settings.Contains(SettingItems.NewThreadTail.ToString()))
                {
                    postStr += "\n\n--------------------------\n" + Settings.Get(SettingItems.NewThreadTail.ToString()) + "\n--*Powered by iPersia.Inc!";
                }
                     
                postStr += "&subject=" + this._subject;
                if (this.ckbSendMail.Checked)
                {
                    postStr += "?email=on";
                }

                this.txtContent.ReadOnly = true;
                this.btnSubmit.Enabled = true;
                string html = WebPageFactory.Post(this._postUrl, postStr);
                string result = CommonUtil.GetMatch(@"<div id=\Wm_main\W><div class=\Wsp hl f\W>(?'Result'\w+)</div>", html, "Result");
                if (result != null && result.Contains("成功"))
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }

                this.Close();
            }
        }
    }
}
