namespace Nzl.Web.Pub.MobileNewSmth
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using Nzl.Util;


    public partial class SettingsForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private static string _preEncryptKey = "_N_Z_L_";

        /// <summary>
        /// 
        /// </summary>
        private static string _sufEncryptKey = "_Y_Y_J_";

        /// <summary>
        /// 
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();
            InitializeSettings();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetPassword(string userName)
        {
            return string.IsNullOrEmpty(userName) ? null : EncryptUtil.Decrypt(Settings.Get(SettingItems.Password.ToString()), _preEncryptKey + userName + _sufEncryptKey);
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeSettings()
        {
            this.txtID.Text = Settings.Get(SettingItems.UserName.ToString());
            if (string.IsNullOrEmpty(this.txtID.Text) == false)
            {
                this.txtPasswd.Text = EncryptUtil.Decrypt(Settings.Get(SettingItems.Password.ToString()), _preEncryptKey + this.txtID.Text + _sufEncryptKey);
            }

            if (Settings.Contains(SettingItems.UpdateInterval.ToString()))
            {
                this.nudUpdateInterval.Text = Settings.Get(SettingItems.UpdateInterval.ToString());
            }

            foreach (SettingItems si in Enum.GetValues(typeof(SettingItems)))
            {
                if (si != SettingItems.UserName &&
                    si != SettingItems.Password &&
                    si != SettingItems.UpdateInterval)
                {
                    this.cmbItems.Items.Add(si);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTp1Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtID.Text.Trim()) == false)
            {
                Settings.Set(SettingItems.UserName.ToString(), this.txtID.Text.Trim());
            }

            if (string.IsNullOrEmpty(this.txtPasswd.Text.Trim()) == false && this.ckbSavedPassword.Checked)
            {
                Settings.Set(SettingItems.Password.ToString(), EncryptUtil.Encrypt(this.txtPasswd.Text, _preEncryptKey + this.txtID.Text + _sufEncryptKey));
            }

            Settings.Set(SettingItems.UpdateInterval.ToString(), this.nudUpdateInterval.Text);
            Settings.Serialize();
        }

        private void btnTp2Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.rtxtItemValue.Text) == false && this.cmbItems.SelectedIndex > -1)
            {
                Settings.Set(this.cmbItems.Text, this.rtxtItemValue.Text);
                Settings.Serialize();
            }
        }

        private void cmbItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.rtxtItemValue.Text = Settings.Get(this.cmbItems.Text);
            this.txtHint.Text = MiscUtil.GetEnumDescription(this.cmbItems.SelectedItem);
        }
    }
}