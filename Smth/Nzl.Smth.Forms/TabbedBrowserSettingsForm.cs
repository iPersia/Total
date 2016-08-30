namespace Nzl.Smth.Forms
{
    using System;
    using System.Windows.Forms;
    using Nzl.Smth.Configurations;

    /// <summary>
    /// 
    /// </summary>
    public partial class TabbedBrowserSettingsForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public TabbedBrowserSettingsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.cmbNewMailCheckingInterval.Text = (Configuration.NewMailCheckingInterval / 1000).ToString();
            this.cmbSectionTopUpdatingInterval.Text = (Configuration.SectionTopsUpdatingInterval / 1000).ToString();
            this.cmbTop10sLoadingInterval.Text = (Configuration.Top10sLoadingInterval / 1000).ToString();
            this.cmbBaseControlContainerLocationMargin.Text = Configuration.BaseControlContainerLocationMargin.ToString();
            this.cmbBaseControlLocationMargin.Text = Configuration.BaseControlLocationMargin.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cmbNewMailCheckingInterval.Text) == false)
            {
                Configuration.SetNewMailUpdatingInterval(Convert.ToInt32(this.cmbNewMailCheckingInterval.Text) * 1000);
            }

            if (string.IsNullOrEmpty(this.cmbSectionTopUpdatingInterval.Text) == false)
            {
                Configuration.SetSectionTopsUpdatingInterval(Convert.ToInt32(this.cmbSectionTopUpdatingInterval.Text) * 1000);
            }

            if (string.IsNullOrEmpty(this.cmbTop10sLoadingInterval.Text) == false)
            {
                Configuration.SetTop10sLoadingInterval(Convert.ToInt32(this.cmbTop10sLoadingInterval.Text) * 1000);
            }

            if (string.IsNullOrEmpty(this.cmbBaseControlContainerLocationMargin.Text) == false &&
                string.IsNullOrEmpty(this.cmbBaseControlLocationMargin.Text) == false)
            {
                Configuration.SetLocationMargin(Convert.ToInt32(this.cmbBaseControlContainerLocationMargin.Text),
                                                Convert.ToInt32(this.cmbBaseControlLocationMargin.Text));
            }            

            this.Close();
        }
    }
}