namespace Nzl.Smth.Forms
{
    using System;
    using System.Windows.Forms;
    using Nzl.Smth.Common;

    /// <summary>
    /// 
    /// </summary>
    public partial class TopicSettingsForm : Form
    {
        #region variable
        /// <summary>
        /// 
        /// </summary>
        private TopicSettingEventArgs _topicSetting = new TopicSettingEventArgs();
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        public TopicSettingsForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public TopicSettingEventArgs Settings
        {
            get
            {
                return _topicSetting;
            }
        }
        #endregion

        #region override
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.rbLatestReply.Checked = this._topicSetting.BrowserType == BrowserType.LastReply;
            this.rbFirstReply.Checked = this._topicSetting.BrowserType == BrowserType.FirstReply;
            this.ckbAutoUpdating.Checked = this._topicSetting.AutoUpdating;
            this.cmbInterval.Text = this._topicSetting.UpdatingInterval.ToString();
        }
        #endregion

        #region eventhandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this._topicSetting.AutoUpdating = this.ckbAutoUpdating.Checked;
            this._topicSetting.BrowserType = this.rbLatestReply.Checked ? BrowserType.LastReply : BrowserType.FirstReply;
            this._topicSetting.UpdatingInterval = Convert.ToInt32(this.cmbInterval.Text);
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
        #endregion
    }
}
