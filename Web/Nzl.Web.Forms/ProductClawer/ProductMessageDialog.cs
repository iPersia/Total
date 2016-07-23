namespace Nzl.Web.Forms.ProductClawer
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Nzl.Web.Core;

    /// <summary>
    /// Message form.
    /// </summary>
    public partial class ProductMessageDialog : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public ProductMessageDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="text"></param>
        public ProductMessageDialog(string caption, Product product)
            : this()
        {
            this.Text = caption;
            this.richtxtInfor.AppendText("\n\t" + product.Vendor + "\n\n");
            this.richtxtInfor.AppendText("\t" + product.Title + "\n\n");
            this.richtxtInfor.AppendText("\t  价格：\t" + product.Price + "\n");
            this.richtxtInfor.AppendText("\t  存货：\t" + (product.IsInStock ? "有货" : "无货") + "\n\n");
            this.richtxtInfor.AppendText("\t时间：\t" + DateTime.Now);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
