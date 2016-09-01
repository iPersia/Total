namespace Nzl.Smth.Forms
{
    using System;
    using System.Windows.Forms;
    using Nzl.Smth.Common;

    /// <summary>
    /// 
    /// </summary>
    public partial class BoardSettingsForm : Form
    {
        #region variable
        /// <summary>
        /// 
        /// </summary>
        private BoardSettingEventArgs _boardSetting = new BoardSettingEventArgs();
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        public BoardSettingsForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public BoardSettingEventArgs Settings
        {
            get
            {
                return _boardSetting;
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
            this.rbShowTops.Checked = this._boardSetting.IsShowTop;
            this.rbNoTops.Checked = !this._boardSetting.IsShowTop;
            this.ckbAutoUpdating.Checked = this._boardSetting.AutoUpdating;
            this.cmbInterval.Text = this._boardSetting.UpdatingInterval.ToString();
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
            this._boardSetting.IsShowTop = this.rbShowTops.Checked;
            this._boardSetting.AutoUpdating = this.ckbAutoUpdating.Checked;
            this._boardSetting.UpdatingInterval = Convert.ToInt32(this.cmbInterval.Text);
            this.Close();
        }        
        #endregion
    }
}
