namespace Nzl.Web.Forms.ProductClawer
{
    using System;
    using System.Windows.Forms;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public partial class ProductDialog : Form
    {
        #region Variable

        /// <summary>
        /// 
        /// </summary>
        public string ProductName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Uri
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal TargetPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Interval
        {
            get;
            set;
        }
        #endregion

        public ProductDialog()
        {
            InitializeComponent();
        }

        private void txtOK_Click(object sender, EventArgs e)
        {
            if (this.txtProductName.Text == "" || this.txtURL.Text == "" || this.txtInterval.Text == "" || this.txtTargetPrice.Text == "")
            {
                MessageBox.Show(this, "请填写所有输入项！", "错误");
                return;
            }

            try
            {
                this.ProductName = this.txtProductName.Text.ToUpper();
                this.Uri = this.txtURL.Text;
                this.TargetPrice = System.Convert.ToDecimal(this.txtTargetPrice.Text);
                this.Interval = System.Convert.ToInt32(this.txtInterval.Text);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

#if (DEBUG)
                CommonUtil.ShowMessage(this, exp.Message);
#endif
                MessageBox.Show(this, "输入项转换错误，请重新输入！", "错误");
            }
        }
    }
}
