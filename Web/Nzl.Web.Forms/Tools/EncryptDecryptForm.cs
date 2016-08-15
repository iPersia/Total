using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nzl.Utils;

namespace Nzl.Web.Forms.Tools
{
    public partial class EncryptDecryptForm : Form
    {
        public EncryptDecryptForm()
        {
            InitializeComponent();
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            this.txtResult.Text = EncryptUtil.Decrypt(this.txtSrc.Text, this.txtKey.Text);
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            this.txtResult.Text = EncryptUtil.Encrypt(this.txtSrc.Text, this.txtKey.Text);
        }
    }
}
