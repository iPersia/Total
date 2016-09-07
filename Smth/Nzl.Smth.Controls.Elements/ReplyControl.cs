namespace Nzl.Smth.Controls.Elements
{
    using System;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Smth.Controls.Base;
    using Nzl.Smth.Datas;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class ReplyControl : BaseControl<Reply>
    {
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnReplyLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnUserLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnDeleteLinkClicked;

        /// <summary>
        /// Ctor.
        /// </summary>
        public ReplyControl()
        {
            InitializeComponent();
            this.linklblAuthor.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblAuthor_LinkClicked);
            this.linklblTitle.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblTitle_LinkClicked);
        }

        public override void Initialize(Reply refer)
        {
            base.Initialize(refer);
            if (refer != null)
            {
                this.linklblTitle.Text = CommonUtil.ReplaceSpecialChars(refer.Title);
                this.linklblTitle.Links.Clear();
                this.linklblTitle.Links.Add(0, this.linklblTitle.Text.Length, refer.Url);
                this.linklblAuthor.Text = refer.Author;
                this.linklblAuthor.Links.Clear();
                this.linklblAuthor.Links.Add(0, refer.Author.Length, refer.Author);
                this.lblDT.Text = refer.DateTime;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override System.Drawing.Color ForeColor
        {
            set
            {
                this.linklblTitle.LinkColor = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblAuthor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnUserLinkClicked != null)
            {
                this.OnUserLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnReplyLinkClicked != null)
            {
                this.OnReplyLinkClicked(sender, e);
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
            }
        }
    }
}
