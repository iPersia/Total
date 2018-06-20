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
    using Nzl.UI;
    using Nzl.Utils;

    public partial class EncryptDecryptForm : Form
    {
        public EncryptDecryptForm()
        {
            InitializeComponent();
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            string resultMsg = string.Empty;
            this.txtResult.Text = resultMsg;
            try {
                resultMsg = CryptUtil.Decrypt(this.txtSrc.Text, this.txtKey.Text);
                this.txtResult.Text = resultMsg;
            }
            catch(Exception exc){
                MessageForm form = new MessageForm("Error", exc.Message);
                form.ShowDialog(this);
            }            
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string resultMsg = string.Empty;
            this.txtResult.Text = resultMsg;
            try
            {
                resultMsg = CryptUtil.Encrypt(this.txtSrc.Text, this.txtKey.Text);
                this.txtResult.Text = resultMsg;
            }
            catch (Exception exc)
            {
                MessageForm form = new MessageForm("Error", exc.Message);
                form.ShowDialog(this);
            }
        }
    }
}
