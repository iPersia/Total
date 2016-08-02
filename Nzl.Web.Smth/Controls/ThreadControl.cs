namespace Nzl.Web.Smth.Controls
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Smth.Datas;
    using Nzl.Web.Smth.Utils;

    /// <summary>
    /// Thread control.
    /// </summary>
    public partial class ThreadControl : UserControl
    {
        #region events.
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnUserLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnQueryTypeLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnReplyLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnMailLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTransferLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnEditLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnDeleteLinkClicked;

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
        ThreadControl()
        {
            InitializeComponent();
            this.linklblID.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblID_LinkClicked);
            this.linklblQuryType.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblQuryType_LinkClicked);
            this.linklblReply.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblReply_LinkClicked);
            this.linklblMail.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblMail_LinkClicked);
            this.linklblTransfer.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblTransfer_LinkClicked);
            this.linklblEdit.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblEdit_LinkClicked);
            this.linklblDelete.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblDelete_LinkClicked);
            this.richtxtContent.MouseWheel += new MouseEventHandler(richtxtContent_MouseWheel);

            System.Drawing.Font currentFont = this.richtxtContent.SelectionFont;
            this.richtxtContent.Font = new Font(currentFont.FontFamily, 11, FontStyle.Regular);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        public ThreadControl(int width)
            : this()
        {
            this.Width = width;
            this.panelLine.Width = this.Width - 20;
            this.richtxtContent.Width = this.panelLine.Width - 8;
            this.richtxtContent.WordWrap = true;
            this.richtxtContent.ScrollBars = RichTextBoxScrollBars.None;

            ///Need to be optimized.
            this.richtxtContent.ContentsResized += new ContentsResizedEventHandler(richtxtContent_ContentsResized);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richtxtContent_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("richtxtContent_ContentsResized - "
                                              + "Url - " + (this.Tag as Thread).Url + "\t"
                                              + "Floor -" + (this.Tag as Thread).Floor + "\t"
                                              + "NewSize - " + e.NewRectangle.Size);
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
        public void Initialize(Thread thread)
        {
            if (thread != null)
            {
                ///Tag
                this.Tag = thread;

                ///Floor
                this.lblFloor.Visible = true;
                if (thread.Floor == "楼主")
                {
                    this.lblFloor.Text = thread.Floor.PadLeft(4);
                }
                else
                {
                    this.lblFloor.Text = thread.Floor.PadLeft(5);
                }

                ///User
                this.linklblID.Visible = true;
                this.linklblID.Text = thread.User;
                if (thread.User != null)
                {
                    this.linklblID.Links.Add(0, this.linklblID.Text.Length, thread.User);
                }

                ///DateTime
                this.lblDateTime.Visible = true;
                this.lblDateTime.Text = thread.DateTime;

                ///Query type
                if (thread.QueryType == "只看此ID")
                {
                    this.linklblQuryType.Text = "Related";
                }
                else
                {
                    this.linklblQuryType.Text = "Spreads";
                }

                this.linklblQuryType.Links.Add(0, this.linklblQuryType.Text.Length, thread.QueryUrl);

                ///Reply link
                if (string.IsNullOrEmpty(thread.ReplyUrl) == false)
                {
                    this.linklblReply.Visible = true;
                    this.linklblReply.Links.Add(0, this.linklblReply.Text.Length, thread.ReplyUrl);
                    this.linklblReply.Tag = thread;
                }

                ///Mail url
                if (string.IsNullOrEmpty(thread.MailUrl) == false)
                {
                    this.linklblMail.Visible = true;
                    this.linklblMail.Links.Add(0, this.linklblMail.Text.Length, thread.MailUrl);
                    this.linklblMail.Tag = thread;
                }

                ///Transfer url
                if (string.IsNullOrEmpty(thread.TransferUrl) == false)
                {
                    this.linklblTransfer.Visible = true;
                    this.linklblTransfer.Links.Add(0, this.linklblTransfer.Text.Length, thread.TransferUrl);
                    this.linklblTransfer.Tag = thread;
                }

                ///Edit url
                if (string.IsNullOrEmpty(thread.EditUrl) == false)
                {
                    this.linklblEdit.Visible = true;
                    this.linklblEdit.Links.Add(0, this.linklblEdit.Text.Length, thread.EditUrl);
                    this.linklblEdit.Tag = thread;
                }

                ///Delete url
                if (string.IsNullOrEmpty(thread.DeleteUrl) == false)
                {
                    this.linklblDelete.Visible = true;
                    this.linklblDelete.Links.Add(0, this.linklblDelete.Text.Length, thread.DeleteUrl);
                    this.linklblDelete.Tag = thread;
                }

                ///Add content.
                this.Name = "tc" + thread.ID;
                this.AddContent(thread);
                this.Height = this.richtxtContent.Height + 48;
                this.richtxtContent.ReadOnly = true;
                this.richtxtContent.ShortcutsEnabled = false;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Thread Thread
        {
            set
            {
                this.Initialize(value);
            }
        }

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

        /// <summary>
        /// 
        /// </summary>
        public bool IsPlainView
        {
            set
            {
                if (value)
                {
                    this.linklblDelete.Visible = false;
                    this.linklblEdit.Visible = false;
                    this.linklblMail.Visible = false;
                    this.linklblReply.Visible = false;
                    this.linklblQuryType.Visible = false;
                    this.linklblTransfer.Visible = false;
                    this.linklblID.Links.Clear();
                }
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
            if (this.OnUserLinkClicked != null)
            {
                this.OnUserLinkClicked(sender, e);
                ///e.Link.Visited = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblQuryType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnQueryTypeLinkClicked != null)
            {
                this.OnQueryTypeLinkClicked(sender, e);
                ///e.Link.Visited = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblReply_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnReplyLinkClicked != null)
            {
                this.OnReplyLinkClicked(sender, e);
                ///e.Link.Visited = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblMail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnMailLinkClicked != null)
            {
                this.OnMailLinkClicked(sender, e);
                ///e.Link.Visited = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblTransfer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnTransferLinkClicked != null)
            {
                this.OnTransferLinkClicked(sender, e);
                ///e.Link.Visited = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnEditLinkClicked != null)
            {
                this.OnEditLinkClicked(sender, e);
                ///e.Link.Visited = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnDeleteLinkClicked != null)
            {
                this.OnDeleteLinkClicked(sender, e);
                ///e.Link.Visited = true;
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
            this.Focus();
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
                                string url = thread.ImageList[imageCounter].Tag.ToString();
                                url = url.Replace("/middle", "/large");
                                this.richtxtContent.InsertLink(ToRtfCode(thread.ImageList[imageCounter++]),
                                                               url,
                                                               this.richtxtContent.Text.Length);
                                //this.richtxtContent.InsertImage(thread.ImageList[imageCounter++]);
                                //Clipboard.SetDataObject(thread.ImageList[imageCounter]);
                                //this.richtxtContent.Paste(DataFormats.GetFormat(DataFormats.Bitmap));
                            }

                            //Icon
                            if (mt.Groups["Type"].Value.ToString() == ThreadFactory.IconToken)
                            {
                                this.richtxtContent.InsertImage(thread.IconList[iconCounter++]);
                                //Clipboard.SetDataObject(thread.IconList[iconCounter]);
                                //this.richtxtContent.Paste(DataFormats.GetFormat(DataFormats.Bitmap));
                            }

                            //Anchor
                            if (mt.Groups["Type"].Value.ToString() == ThreadFactory.AnchorToken)
                            {
                                this.richtxtContent.InsertLink(ToRtfCode(thread.AnchorList[anchorCounter].Text),
                                                               thread.AnchorList[anchorCounter++].Url,
                                                               this.richtxtContent.Text.Length);
                                //System.Diagnostics.Debug.WriteLine(ToRtfCode(thread.AnchorList[anchorCounter - 1].Text));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="richTextBox"></param>
        private void ResetRichTextBoxHeight(RichTextBox richTextBox)
        {
            if (richTextBox != null)
            {
                int rowCount = richTextBox.GetLineFromCharIndex(richTextBox.SelectionStart) + 1;
                richTextBox.Height = (richTextBox.Font.Height + 2) * rowCount + 4;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private RichTextBoxEx _convertTxtBox = new RichTextBoxEx();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string ToRtfCode(string info)
        {
            this._convertTxtBox.Clear();
            this._convertTxtBox.AppendText(info);
            string rtfValue = this._convertTxtBox.Rtf;

            string startStr = @"\viewkind4\uc1\pard\lang2052";
            rtfValue = rtfValue.Substring(rtfValue.IndexOf(startStr) + startStr.Length);
            return rtfValue = rtfValue.Substring(0, rtfValue.LastIndexOf(@"\par"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string ToRtfCode(Image image)
        {
            this._convertTxtBox.Clear();
            this._convertTxtBox.InsertImage(image);
            string rtfValue = this._convertTxtBox.Rtf;

            string startStr = @"\viewkind4\uc1\pard\lang2052";
            rtfValue = rtfValue.Substring(rtfValue.IndexOf(startStr) + startStr.Length);
            return rtfValue = rtfValue.Substring(0, rtfValue.LastIndexOf(@"\par"));
        }
        #endregion
    }
}
