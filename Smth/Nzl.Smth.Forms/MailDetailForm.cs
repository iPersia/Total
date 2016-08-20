namespace Nzl.Smth.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;
    using Nzl.Smth.Datas;
    using Nzl.Smth.Containers;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class MailDetailForm : Form
    {
        #region variable
        /// <summary>
        /// 
        /// </summary>
        private string _url = null;

        /// <summary>
        /// 
        /// </summary>
        private MailDetailContainerControl _mccContainer = null;
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor.
        /// </summary>
        public MailDetailForm()
        {
            InitializeComponent();
            this._mccContainer = new MailDetailContainerControl();
            this._mccContainer.Left = 1;
            this._mccContainer.Top = 1;
            this._mccContainer.OnMailAuthorLinkClicked += Mdcc_OnMailAuthorLinkClicked;
            this._mccContainer.OnMailDeleteLinkClicked += Mdcc_OnMailDeleteLinkClicked;
            this._mccContainer.OnMailReplyLinkClicked += Mdcc_OnMailReplyLinkClicked;
            this._mccContainer.OnMailTransferLinkClicked += Mdcc_OnMailTransferLinkClicked;
            this._mccContainer.SetParentControl(this);
            this._mccContainer.BorderStyle = BorderStyle.FixedSingle;
            this.panelContainer.Controls.Add(this._mccContainer);
            this.Size = new Size(this._mccContainer.Width + this.Width - this.panelContainer.Width + 2, 
                                 this._mccContainer.Height + this.Height - this.panelContainer.Height + 2);
        }
        #endregion

        #region properties
        /// <summary>
        /// 
        /// </summary>
        public string Url
        {
            set
            {
                this._mccContainer.Url = value;
            }
        }
        #endregion

        #region eventhandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mdcc_OnMailTransferLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mdcc_OnMailReplyLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                Mail mail = linkLabel.Tag as Mail;
                if (mail != null)
                {
                    string content = GetReplyContent(mail.Title, mail.Content);
                    NewMailForm newMailForm = new NewMailForm(mail.Author, mail.Title, content);
                    newMailForm.StartPosition = FormStartPosition.CenterParent;
                    if (newMailForm.ShowDialog(this) == DialogResult.Yes)
                    {
                        e.Link.Visited = true;
                    }
                }
            }
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mdcc_OnMailDeleteLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                if (string.IsNullOrEmpty(e.Link.LinkData.ToString()) == false)
                {
                    WebPage page = WebPageFactory.CreateWebPage(e.Link.LinkData.ToString());
                    string result = CommonUtil.GetMatch(@"<div id=\Wm_main\W><div class=\Wsp hl f\W>(?'Result'\w+)</div>", page.Html, "Result");
                    if (result != null && result.Contains("成功"))
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                        this.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mdcc_OnMailAuthorLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                UserForm userForm = new UserForm(e.Link.LinkData.ToString());
                userForm.StartPosition = FormStartPosition.CenterParent;
                userForm.ShowDialog(this);
            }
        }
        #endregion

        #region private
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string GetReplyContent(string id, string content)
        {
            if (string.IsNullOrEmpty(content) == false)
            {
                Regex regex = new Regex(@"\s*--\sFROM\s[\d, \., \*]+");
                content = regex.Replace(content, "");

                //删除上层回复
                regex = new Regex(@"【\s在\s.+\s的大作中提到\: 】");
                content = new Regex(@"(?m)<a[^>]*>(\w|\W)*?</a[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(content, "");
                MatchCollection mtCollection = regex.Matches(content);
                if (mtCollection != null && mtCollection.Count > 0)
                {
                    content = content.Substring(0, content.IndexOf(mtCollection[0].Groups[0].Value.ToString()));
                }

                //替换多次换行
                content = new Regex(@"[\n]+", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(content, "\n");
                content = new Regex(@"[\r]+", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(content, "\n");

                //删除尾部换行
                content = content.TrimEnd('\n');

                //保留一定的回复行数 （saveLastReplyLineCount + 1）
                mtCollection = new Regex(@"[\n]+", RegexOptions.Multiline | RegexOptions.IgnoreCase).Matches(content);
                int saveLastReplyLineCount = 5;
                string tail = string.Empty;
                string head = "\n\n【 在 " + id + " 的大作中提到: 】\n: ";
                if (mtCollection != null && mtCollection.Count > saveLastReplyLineCount)
                {
                    for (int i = mtCollection.Count; i > saveLastReplyLineCount; i--)
                    {
                        content = content.Substring(0, content.LastIndexOf("\n"));
                    }

                    tail += "\n: ...................";
                }

                //添加回复头和尾
                content = head + content.Replace("\n", "\n: ") + tail;
                return content;
            }

            return null;
        }
        #endregion
    }
}
