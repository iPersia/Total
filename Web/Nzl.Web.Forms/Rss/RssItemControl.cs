namespace Nzl.Web.Forms.Rss
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.ComponentModel;    
    using Nzl.Web.Core;
    using Nzl.Web.Interface;
    using Nzl.Web.Util;

    /// <summary>
    /// Thread control.
    /// </summary>
    public partial class RssItemControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTitleLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public string Index
        {
            set
            {
                this.lblIndex.Text = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRead
        {
            get
            {
                return this.lblIndex.ForeColor == System.Drawing.Color.Red;
            }

            set
            {
                if (value)
                {
                    this.lblIndex.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.lblIndex.ForeColor = System.Drawing.Color.Black;
                }
            }
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public RssItemControl()
        {
            InitializeComponent();

#if (DEBUG)
            this.contentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thread"></param>
        public RssItemControl(RssItem item)
            : this()
        {
            this.lblVendor.Text = item.Vendor;
            this.lblDateTime.Text = item.DateTime.ToString("yyyy-MM-dd HH:mm:ss");

            this.lnklblTitle.Text = item.Title;
            this.lnklblTitle.Links.Add(0, this.lnklblTitle.Text.Length, item.Uri.AbsoluteUri);
        }

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public int AllWidth
        {
            set
            {
                this.Width = value;
                //this.richtxtContent.Width = this.Width - 30;
                this.contentPanel.Width = this.Width - 30;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color BackGroundColor
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
        public RichTextBox TextBox
        {
            get
            {
                return this.richtxtContent;
            }
        }
        #endregion

        #region Event handler

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richtxtContent_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            CommonUtil.OpenUrl(e.LinkText);
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
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RssItemControl_Load(object sender, EventArgs e)
        {
            this.lnklblTitle.LinkClicked += new LinkLabelLinkClickedEventHandler(lnklblTitle_LinkClicked);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnklblTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {            
            if (this.OnTitleLinkClicked != null)
            {
                this.OnTitleLinkClicked(sender, e);
                e.Link.Visited = true;
                this.IsRead = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="height"></param>
        public void AutoFixHeight()
        {
            this.contentPanel.Height = this.GetActualHeight(this.richtxtContent);
            this.Height = this.contentPanel.Height + 80;
        }

        /// <summary>
        /// Get the actual height of the RichTextBox.
        /// The height is of the full content of the RichTextBox control.
        /// </summary>
        /// <param name="txtBox">The RichTextBox.</param>
        private int GetActualHeight(RichTextBox txtBox)
        {
            if (txtBox != null)
            {
                ///获取RichTextBox的内容行数
                int rowCount = txtBox.GetLineFromCharIndex(txtBox.SelectionStart);
                ///获取RichTextBox最后一行第一个字符的坐标
                Point ptLine2 = txtBox.GetPositionFromCharIndex(txtBox.GetFirstCharIndexFromLine(rowCount));
                ///返回实际高度
                return ptLine2.Y + txtBox.Font.Height + txtBox.Margin.Top + +txtBox.Margin.Bottom;
            }

            return -1;
        }
    }
}
