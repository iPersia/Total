namespace Nzl.Smth.Forms
{
    using System;
    using System.Windows.Forms;
    using Nzl.Smth.Common;

    /// <summary>
    /// 
    /// </summary>
    public partial class TopicBrowserSettingsForm : Form
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public BrowserType BrowserType
        {
            get
            {
                return this.rbLatestReply.Checked ? BrowserType.LastReply : BrowserType.FirstReply;
            }

            set
            {
                this.rbFirstReply.Checked = value == BrowserType.FirstReply;
                this.rbLatestReply.Checked = value == BrowserType.LastReply;
                this.gpAutoUpdating.Enabled = this.rbLatestReply.Checked;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AutoUpdating
        {
            get
            {
                return this.ckbAutoUpdating.Checked;
            }

            set
            {
                this.ckbAutoUpdating.Checked = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int UpdatingInterval
        {
            get
            {
                return Convert.ToInt32(this.cmbInterval.Text);
            }

            set
            {
                this.cmbInterval.Text = value.ToString();
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public TopicBrowserSettingsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbLatestReply_CheckedChanged(object sender, EventArgs e)
        {
            this.gpAutoUpdating.Enabled = this.rbLatestReply.Checked;
        }
    }
}
