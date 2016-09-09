namespace Nzl.Smth.Controls.Elements
{
    using System;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Smth.Controls.Base;    
    using Nzl.Smth.Datas;
    using Nzl.Smth.Utils;
    using Nzl.Web.Util;

    /// <summary>
    /// Thread control.
    /// </summary>
    public partial class PostControl : BaseControl<Post>
    {
        #region events.
        /// <summary>
        ///
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnNewClicked;

        /// <summary>
        ///
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnReplyClicked;

        /// <summary>
        ///
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnExpandClicked;

        /// <summary>
        ///
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnHostClicked;

        /// <summary>
        ///
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnSubjectExpandClicked;

        /// <summary>
        ///
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnSourceClicked;

        /// <summary>
        ///
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnBoardClicked;

        /// <summary>
        ///
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnLastClicked;

        /// <summary>
        ///
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnNextClicked;

        /// <summary>
        ///
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnSubjectLastClicked;

        /// <summary>
        ///
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnSubjectNextClicked;

        /// <summary>
        ///
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnMailClicked;

        /// <summary>
        ///
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTransferClicked;

        /// <summary>
        ///
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnUserClicked;

        /// <summary>
        ///
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnEditClicked;

        /// <summary>
        ///
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnDeleteClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkClickedEventHandler OnTextBoxLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event MouseEventHandler OnTextBoxMouseWheel;
        #endregion

        #region Ctors.
        /// <summary>
        /// Ctor.
        /// </summary>
        public PostControl()
        {
            InitializeComponent();

            System.Drawing.Font currentFont = this.richtxtContent.SelectionFont;
            this.richtxtContent.Font = new Font(currentFont.FontFamily, 11, FontStyle.Regular);
            this.richtxtContent.GotFocus += RichtxtContent_GotFocus;
            this.richtxtContent.MouseWheel += new MouseEventHandler(richtxtContent_MouseWheel);

            ///Need to be optimized.
            this.richtxtContent.WordWrap = true;
            this.richtxtContent.ScrollBars = RichTextBoxScrollBars.None;
            this.richtxtContent.ContentsResized += new ContentsResizedEventHandler(richtxtContent_ContentsResized);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RichtxtContent_GotFocus(object sender, EventArgs e)
        {
            this.panel.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richtxtContent_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
#if (X)
            System.Diagnostics.Debug.WriteLine("richtxtContent_ContentsResized - "
                                              + "Url - " + (this.Tag as Thread).Url + "\t"
                                              + "Floor -" + (this.Tag as Thread).Floor + "\t"
                                              + "NewSize - " + e.NewRectangle.Size);
#endif
            RichTextBox rtb = sender as RichTextBox;
            if (rtb != null)
            {
                rtb.Size = e.NewRectangle.Size;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thread"></param>
        public override void Initialize(Post refer)
        {
            base.Initialize(refer);
            if (refer != null)
            {
                ///DateTime
                this.lblDateTime.Visible = true;
                this.lblDateTime.Text = refer.DateTime;

                ///Subject
                this.lblSubject.Text = refer.Subject;
                this.lblSubject.Visible = true;

                ///ID
                this.linklblID.Text = refer.Author;

                ///Board
                this.linklblBoard.Text = refer.Board;
                
                ///Urls
                this.InitializeLinkLabel(this.linklblBoard, refer.Board);
                this.InitializeLinkLabel(this.linklblDelete, refer.DeleteUrl);
                this.InitializeLinkLabel(this.linklblEdit, refer.EditUrl);
                this.InitializeLinkLabel(this.linklblExpand, refer.ExpandUrl);
                this.InitializeLinkLabel(this.linklblHost, refer.HostUrl);
                this.InitializeLinkLabel(this.linklblID, refer.Author);
                this.InitializeLinkLabel(this.linklblLast, refer.LastUrl);
                this.InitializeLinkLabel(this.linklblMail, refer.MailUrl);
                this.InitializeLinkLabel(this.linklblNew, refer.NewUrl);
                this.InitializeLinkLabel(this.linklblNext, refer.NextUrl);
                this.InitializeLinkLabel(this.linklblReply, refer.ReplyUrl);
                this.InitializeLinkLabel(this.linklblSource, refer.SourceUrl);
                this.InitializeLinkLabel(this.linklblSubjectExpand, refer.SubjectExpandUrl);
                this.InitializeLinkLabel(this.linklblSubjectLast, refer.SubjectLastUrl);
                this.InitializeLinkLabel(this.linklblSubjectNext, refer.SubjectNextUrl);
                this.InitializeLinkLabel(this.linklblTransfer, refer.TransferUrl);

                ///Add content.
                this.richtxtContent.Clear();
                this.AddContent(refer.Data);
                this.Height = this.richtxtContent.Height + 100;
                this.richtxtContent.ReadOnly = true;
                this.richtxtContent.ShortcutsEnabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        public override void SetWidth(int width)
        {
            base.SetWidth(width);
            this.Width = width;
            this.panelTitle.Width = this.Width - 10;
            this.richtxtContent.Width = this.panelTitle.Width - 8;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public override Color BackColor
        {
            set
            {
                this.panel.BackColor = value;
                this.richtxtContent.BackColor = value;
            }
        }
        #endregion

        #region Event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblID_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnUserClicked != null)
            {
                this.OnUserClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblReply_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnReplyClicked != null)
            {
                (sender as LinkLabel).Tag = this.Data;
                this.OnReplyClicked(sender, e);
                (sender as LinkLabel).Tag = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblTransfer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnTransferClicked != null)
            {
                this.OnTransferClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblNew_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnNewClicked!=null)
            {
                this.OnNewClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblExpand_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnExpandClicked != null)
            {
                this.OnExpandClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblHost_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnHostClicked != null)
            {
                this.OnHostClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblSubjectExpand_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnSubjectExpandClicked != null)
            {
                this.OnSubjectExpandClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblSource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnSourceClicked != null)
            {
                this.OnSourceClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblBoard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnBoardClicked != null)
            {
                this.OnBoardClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblLast_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnLastClicked != null)
            {
                this.OnLastClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnNextClicked != null)
            {
                this.OnNextClicked(sender, e);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblSubjectLast_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnSubjectLastClicked != null)
            {
                this.OnSubjectLastClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblSubjectNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnSubjectNextClicked != null)
            {
                this.OnSubjectNextClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblMail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnMailClicked != null)
            {
                this.OnMailClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnEditClicked != null)
            {
                this.OnEditClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnDeleteClicked != null)
            {
                this.OnDeleteClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richtxtContent_MouseWheel(object sender, MouseEventArgs e)
        {
            if (this.OnTextBoxMouseWheel != null)
            {
                this.OnTextBoxMouseWheel(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richtxtContent_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            if (this.OnTextBoxLinkClicked != null)
            {
                this.OnTextBoxLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richtxtContent_Enter(object sender, EventArgs e)
        {
            this.panel.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                this.richtxtContent.SelectAll();
                Clipboard.SetData(DataFormats.Rtf, this.richtxtContent.SelectedRtf);
                this.richtxtContent.DeselectAll();
                e.Link.Visited = true;
            }
        }
        #endregion

        #region Privates
        /// <summary>
        /// 
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="thread"></param>
        private void AddContent(Thread thread)
        {
            if (thread != null)
            {
                string content = CommonUtil.ReplaceSpecialChars(thread.Content);
                {
                    string tokenPattern = ThreadFactory.TokenPrefix + "(?'Type'[A-Z]+)" + ThreadFactory.TokenSuffix;
                    MatchCollection mtCollection = CommonUtil.GetMatchCollection(tokenPattern, thread.Content);
                    int iconCounter = 0;
                    int imageCounter = 0;
                    int anchorCounter = 0;
                    if (thread.ImageList != null || thread.IconList != null || thread.AnchorList != null)
                    {
                        foreach (Match mt in mtCollection)
                        {
                            string token = mt.Groups[0].Value.ToString();
                            int pos = content.IndexOf(token);
                            string tempContent = content.Substring(0, pos);
                            {
                                //去除HTTP标签
                                tempContent = new Regex(@"(?m)<script[^>]*>(\w|\W)*?</script[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(tempContent, "");
                                tempContent = new Regex(@"(?m)<style[^>]*>(\w|\W)*?</style[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(tempContent, "");
                                tempContent = new Regex(@"(?m)<select[^>]*>(\w|\W)*?</select[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(tempContent, "");
                                Regex objReg = new System.Text.RegularExpressions.Regex("(<[.^>]+?>)|&nbsp;", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                                tempContent = objReg.Replace(tempContent, "");
                            }

                            this.richtxtContent.AppendText(tempContent);
                            content = content.Substring(pos + token.Length);

                            //Image
                            if (mt.Groups["Type"].Value.ToString() == ThreadFactory.ImageToken)
                            {
                                string data = thread.ImageList[imageCounter++].Tag.ToString();
                                int indexOfUrl = data.IndexOf(token);
                                string url = data.Substring(0, indexOfUrl);
                                System.Diagnostics.Debug.WriteLine(url);
                                string rtfCode = data.Substring(indexOfUrl + token.Length);
                                this.richtxtContent.InsertLink(rtfCode, url, this.richtxtContent.Text.Length);                                
                            }

                            //Icon
                            if (mt.Groups["Type"].Value.ToString() == ThreadFactory.IconToken)
                            {
                                this.richtxtContent.InsertImage(thread.IconList[iconCounter++]);
                            }

                            //Anchor
                            if (mt.Groups["Type"].Value.ToString() == ThreadFactory.AnchorToken)
                            {
                                this.richtxtContent.InsertLink(RtfUtil.GetRtfCode(thread.AnchorList[anchorCounter].Text),
                                                               thread.AnchorList[anchorCounter++].Url,
                                                               this.richtxtContent.Text.Length);
                            }
                        }
                    }

                    this.richtxtContent.AppendText(content);

                    ///Colored the replied thread content.
                    {
                        string text = this.richtxtContent.Text;
                        string replayPattern = @"【 在 [a-zA-z][a-zA-Z0-9]{1,11} (\((.+)?\) )?的大作中提到: 】[^\r^\n]*[\r\n]+(\:.*[\r\n]*)*";
                        MatchCollection mc = CommonUtil.GetMatchCollection(replayPattern, text);
                        if (mc != null && mc.Count > 0)
                        {
                            foreach (Match mt in mc)
                            {
                                string from = mt.Groups[0].Value;
                                int index = this.richtxtContent.Text.IndexOf(from);
                                if (index >= 0)
                                {
                                    this.richtxtContent.Select(index, from.Length);
                                    this.richtxtContent.SelectionColor = Color.FromArgb(96, 96, 96);
                                    this.richtxtContent.SelectionFont = new Font(this.richtxtContent.Font.FontFamily, 9, FontStyle.Regular);
                                    this.richtxtContent.DeselectAll();
                                }
                            }
                        }
                    }

                    ///Colored the From IP.
                    {
                        string text = this.richtxtContent.Text;
                        string ipPattern = @"--[\r\n]+(修改:[a-zA-z][a-zA-Z0-9]{1,11} FROM (\d+\.){3}(\*|\d+)[\r\n]+)?FROM (\d+\.){3}(\*|\d+)";
                        MatchCollection mc = CommonUtil.GetMatchCollection(ipPattern, text);
                        if (mc != null && mc.Count > 0)
                        {
                            string from = mc[mc.Count - 1].Groups[0].Value;
                            int index = this.richtxtContent.Text.LastIndexOf(from);
                            if (index >= 0)
                            {
                                this.richtxtContent.Select(index, from.Length);
                                this.richtxtContent.SelectionColor = Color.FromArgb(160, 160, 160);
                                this.richtxtContent.SelectionFont = new Font(this.richtxtContent.Font.FontFamily, 9, FontStyle.Regular);
                                this.richtxtContent.DeselectAll();
                            }
                        }
                    }

                    ///Colored the reply tail.
                    {
                        string text = this.richtxtContent.Text;
                        string repleyContent = SmthUtil.GetReplyText();
                        int index = text.IndexOf(repleyContent);
                        if (index >= 0)
                        {
                            this.richtxtContent.Select(index, repleyContent.Length);
                            this.richtxtContent.SelectionFont = new Font(this.richtxtContent.SelectionFont.FontFamily, 9, FontStyle.Regular);
                            this.richtxtContent.DeselectAll();
                        }
                    }
                }
            }
        }

        #region For test.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="thread"></param>
        private void AddText(string content)
        {
            if (content != null)
            {
                {
                    content = content.Replace("<br />", "\n");
                    content = content.Replace("<br/>", "\n");
                    content = content.Replace("<br>", "\n");
                    content = content.Replace("<div class=\"sp\">", "");
                    content = content.Replace("&nbsp;", " ");
                    content = content.Replace("</div>", "");
                    content = CommonUtil.ReplaceSpecialChars(content);
                }
 
                this.richtxtContent.AppendText(content);
                ///Colored the replied thread content.
                {
                    string text = this.richtxtContent.Text;
                    string replayPattern = @"【 在 [a-zA-z][a-zA-Z0-9]{1,11} (\((.+)?\) )?的大作中提到: 】[^\r^\n]*[\r\n]+(\:.*[\r\n]*)*";
                    MatchCollection mc = CommonUtil.GetMatchCollection(replayPattern, text);
                    if (mc != null && mc.Count > 0)
                    {
                        foreach (Match mt in mc)
                        {
                            string from = mt.Groups[0].Value;
                            int index = this.richtxtContent.Text.IndexOf(from);
                            if (index >= 0)
                            {
                                this.richtxtContent.Select(index, from.Length);
                                this.richtxtContent.SelectionColor = Color.FromArgb(96, 96, 96);
                                this.richtxtContent.SelectionFont = new Font(this.richtxtContent.Font.FontFamily, 9, FontStyle.Regular);
                                this.richtxtContent.DeselectAll();
                            }
                        }
                    }
                }

                ///Colored the From IP.
                {
                    string text = this.richtxtContent.Text;
                    string ipPattern = @"--[\r\n]+(修改:[a-zA-z][a-zA-Z0-9]{1,11} FROM (\d+\.){3}(\*|\d+)[\r\n]+)?FROM (\d+\.){3}(\*|\d+)";
                    MatchCollection mc = CommonUtil.GetMatchCollection(ipPattern, text);
                    if (mc != null && mc.Count > 0)
                    {
                        string from = mc[mc.Count - 1].Groups[0].Value;
                        int index = this.richtxtContent.Text.LastIndexOf(from);
                        if (index >= 0)
                        {
                            this.richtxtContent.Select(index, from.Length);
                            this.richtxtContent.SelectionColor = Color.FromArgb(160, 160, 160);
                            this.richtxtContent.SelectionFont = new Font(this.richtxtContent.Font.FontFamily, 9, FontStyle.Regular);
                            this.richtxtContent.DeselectAll();
                        }
                    }
                }

                ///Colored the reply tail.
                {
                    string text = this.richtxtContent.Text;
                    string repleyContent = SmthUtil.GetReplyText();
                    int index = text.IndexOf(repleyContent);
                    if (index >= 0)
                    {
                        this.richtxtContent.Select(index, repleyContent.Length);
                        this.richtxtContent.SelectionFont = new Font(this.richtxtContent.SelectionFont.FontFamily, 9, FontStyle.Regular);
                        this.richtxtContent.DeselectAll();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="thread"></param>
        private void AddText(Thread thread)
        {
            if (thread != null)
            {
                string content = CommonUtil.ReplaceSpecialChars(thread.Content);
                {
                    string tokenPattern = ThreadFactory.TokenPrefix + ThreadFactory.AnchorToken + ThreadFactory.TokenSuffix;
                    MatchCollection mtCollection = CommonUtil.GetMatchCollection(tokenPattern, thread.Content);
                    int anchorCounter = 0;
                    if (thread.ImageList != null || thread.IconList != null || thread.AnchorList != null)
                    {
                        foreach (Match mt in mtCollection)
                        {
                            string token = mt.Groups[0].Value.ToString();
                            int pos = content.IndexOf(token);
                            string tempContent = content.Substring(0, pos);
                            {
                                //去除HTTP标签
                                tempContent = new Regex(@"(?m)<script[^>]*>(\w|\W)*?</script[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(tempContent, "");
                                tempContent = new Regex(@"(?m)<style[^>]*>(\w|\W)*?</style[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(tempContent, "");
                                tempContent = new Regex(@"(?m)<select[^>]*>(\w|\W)*?</select[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(tempContent, "");
                                Regex objReg = new System.Text.RegularExpressions.Regex("(<[.^>]+?>)|&nbsp;", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                                tempContent = objReg.Replace(tempContent, "");
                            }

                            this.richtxtContent.AppendText(tempContent);
                            content = content.Substring(pos + token.Length);

                            this.richtxtContent.InsertLink(RtfUtil.GetRtfCode(thread.AnchorList[anchorCounter].Text),
                                                           thread.AnchorList[anchorCounter++].Url,
                                                           this.richtxtContent.Text.Length);
                            //System.Diagnostics.Debug.WriteLine(RichTextBoxHelper.ToRtfCode(thread.AnchorList[anchorCounter - 1].Text));
                        }
                    }

                    this.richtxtContent.AppendText(content);

                    ///Colored the replied thread content.
                    {
                        string text = this.richtxtContent.Text;
                        string replayPattern = @"【 在 [a-zA-z][a-zA-Z0-9]{1,11} (\((.+)?\) )?的大作中提到: 】[^\r^\n]*[\r\n]+(\:.*[\r\n]*)*";
                        MatchCollection mc = CommonUtil.GetMatchCollection(replayPattern, text);
                        if (mc != null && mc.Count > 0)
                        {
                            foreach (Match mt in mc)
                            {
                                string from = mt.Groups[0].Value;
                                int index = this.richtxtContent.Text.IndexOf(from);
                                if (index >= 0)
                                {
                                    this.richtxtContent.Select(index, from.Length);
                                    this.richtxtContent.SelectionColor = Color.FromArgb(96, 96, 96);
                                    this.richtxtContent.SelectionFont = new Font(this.richtxtContent.Font.FontFamily, 9, FontStyle.Regular);
                                    this.richtxtContent.DeselectAll();
                                }
                            }
                        }
                    }

                    ///Colored the From IP.
                    {
                        string text = this.richtxtContent.Text;
                        string ipPattern = @"--[\r\n]+(修改:[a-zA-z][a-zA-Z0-9]{1,11} FROM (\d+\.){3}(\*|\d+)[\r\n]+)?FROM (\d+\.){3}(\*|\d+)";
                        MatchCollection mc = CommonUtil.GetMatchCollection(ipPattern, text);
                        if (mc != null && mc.Count > 0)
                        {
                            string from = mc[mc.Count - 1].Groups[0].Value;
                            int index = this.richtxtContent.Text.LastIndexOf(from);
                            if (index >= 0)
                            {
                                this.richtxtContent.Select(index, from.Length);
                                this.richtxtContent.SelectionColor = Color.FromArgb(160, 160, 160);
                                this.richtxtContent.SelectionFont = new Font(this.richtxtContent.Font.FontFamily, 9, FontStyle.Regular);
                                this.richtxtContent.DeselectAll();
                            }
                        }
                    }

                    ///Colored the reply tail.
                    {
                        string text = this.richtxtContent.Text;
                        string repleyContent = SmthUtil.GetReplyText();
                        int index = text.IndexOf(repleyContent);
                        if (index >= 0)
                        {
                            this.richtxtContent.Select(index, repleyContent.Length);
                            this.richtxtContent.SelectionFont = new Font(this.richtxtContent.SelectionFont.FontFamily, 9, FontStyle.Regular);
                            this.richtxtContent.DeselectAll();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="thread"></param>
        private void AddImages(Thread thread)
        {
            if (thread != null && thread.ImageList != null)
            {
                foreach (Image image in thread.ImageList)
                {
                    this.AddImage(image);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="thread"></param>
        private void AddImage(Image image)
        {
            if (image != null)
            {
                int searchStartPosition = 0;// this.richtxtContent.SelectionStart;
                string token = ThreadFactory.TokenPrefix + ThreadFactory.ImageToken + ThreadFactory.TokenSuffix;
                int indexOfText = this.richtxtContent.Find(token, searchStartPosition, RichTextBoxFinds.None);
                if (indexOfText > 0)
                {
                    this.richtxtContent.Select(indexOfText, token.Length);
                    this.richtxtContent.SelectedText = "";//.Replace(token, "");
                    string data = image.Tag.ToString();
                    int indexOfUrl = data.IndexOf(token);
                    string url = data.Substring(0, indexOfUrl);
                    System.Diagnostics.Debug.WriteLine(url);
                    string rtfCode = data.Substring(indexOfUrl + token.Length);
                    this.richtxtContent.InsertLink(rtfCode, url, indexOfText);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="thread"></param>
        private void AddIcons(Thread thread)
        {
            if (thread != null && thread.IconList != null)
            {
                foreach (Image icon in thread.IconList)
                {
                    this.AddIcon(icon);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="icon"></param>
        private void AddIcon(Image icon)
        {
            if (icon != null)
            {
                int searchStartPosition = 0;// this.richtxtContent.SelectionStart;
                string token = ThreadFactory.TokenPrefix + ThreadFactory.IconToken + ThreadFactory.TokenSuffix;
                int indexOfText = this.richtxtContent.Find(token, searchStartPosition, RichTextBoxFinds.None);
                if (indexOfText > 0)
                {
                    this.richtxtContent.Select(indexOfText, token.Length);
                    this.richtxtContent.SelectedText = "";//.Replace(token, "");
                    this.richtxtContent.InsertImage(icon);
                }
            }
        }
        #endregion

        #endregion
    }
}
