namespace Nzl.Smth.Forms
{
    using System;
    using System.Windows.Forms;
    using Nzl.Web.Page;
    using Nzl.Web.Util;
    using Utils;

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
        private string _topic;

        /// <summary>
        /// 
        /// </summary>
        NewThreadForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postUrl"></param>  
        /// <param name="subject"></param>
        public NewThreadForm(string topic, string subject)
            : this()
        {
            this._topic = topic;
            this._subject = subject;
            this.Text = this._topic;
            this.txtTitle.Text = subject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postUrl"></param>  
        /// <param name="subject"></param>
        public NewThreadForm(string topic, string subject, string content, bool isShowSendMail)
            : this(topic, subject)
        {
            this.ckbSendMail.Visible = isShowSendMail;
            this.txtContent.Text = content;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetPostString()
        {
            if (string.IsNullOrEmpty(this.txtContent.Text) == false)
            {
                string postStr = "content=" + this.txtContent.Text;

#if (true)
                postStr += SmthUtil.GetReplyTail();
#endif

                postStr += "&subject=" + this.txtTitle.Text;

                if (this.ckbSendMail.Checked)
                {
                    postStr += "?email=on";
                }

                return postStr;
            }

            return null;
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
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtContent.Text) == false && 
                string.IsNullOrEmpty(this.txtTitle.Text) == false)
            {
                string postStr = "subject=" + this.txtTitle.Text;
                postStr += "&content=" + this.txtContent.Text;

#if (true)
                postStr += SmthUtil.GetReplyTail();
#endif

                if (this.ckbSendMail.Checked)
                {
                    postStr += "?email=on";
                }

                this.txtContent.ReadOnly = true;
                this.btnSubmit.Enabled = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
