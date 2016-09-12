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
                this.linklblExpand.Tag = refer.Subject != null ? refer.Subject.Replace("主题:Re: ", "") : "";
                this.linklblSubjectExpand.Tag = refer.Subject != null ? refer.Subject.Replace("主题:Re: ", "") : "";

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
                ControlUtil.AddContent(this.richtxtContent, refer.Data);
                //this.AddContent(refer.Data);
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
                (sender as LinkLabel).Tag = SmthUtil.GetReplyContent(this.Data.Author, this.Data.Content);
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
                (sender as LinkLabel).Tag = this.Data.Author + "<User>" + SmthUtil.GetReplyContent(this.Data.Author, this.Data.Content);
                this.OnMailClicked(sender, e);
                (sender as LinkLabel).Tag = null;
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
                (sender as LinkLabel).Tag = SmthUtil.GetReplyContent(this.Data.Author, this.Data.Content);
                this.OnEditClicked(sender, e);
                (sender as LinkLabel).Tag = null;
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
    }
}
