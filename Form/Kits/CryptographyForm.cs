namespace Nzl.Forms.Kits
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Nzl.Utils;

    /// <summary>
    /// 
    /// </summary>
    public partial class CryptographyForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public CryptographyForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtDecrypted.Text) == false &&
                string.IsNullOrEmpty(this.txtKey.Text) == false)
                try
                {
                    this.txtDecrypted.Text = CryptUtil.Decrypt(this.txtEncrypted.Text, this.txtKey.Text);
                }
                catch
                {

                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtDecrypted.Text = "";
            this.txtEncrypted.Text = "";
            this.txtKey.Text = "";
        }
    }
}
