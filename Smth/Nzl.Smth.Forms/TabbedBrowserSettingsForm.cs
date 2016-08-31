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
        #region Singleton
        /// <summary>
        /// 
        /// </summary>
        public static readonly TabbedBrowserSettingsForm Instance = new TabbedBrowserSettingsForm();
        #endregion

        /// <summary>
        /// 
        /// </summary>
        TabbedBrowserSettingsForm()
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
                Configuration.SetNewMailCheckingInterval(Convert.ToInt32(this.cmbNewMailCheckingInterval.Text) * 1000);
            }

            if (string.IsNullOrEmpty(this.cmbSectionTopUpdatingInterval.Text) == false)
            {
                Configuration.SetSectionTopsUpdatingInterval(Convert.ToInt32(this.cmbSectionTopUpdatingInterval.Text) * 1000);
            }

            if (string.IsNullOrEmpty(this.cmbTop10sLoadingInterval.Text) == false)
            {
                Configuration.SetTop10sLoadingInterval(Convert.ToInt32(this.cmbTop10sLoadingInterval.Text) * 1000);
            }

            this.SaveSettings();
            this.Close();
        }

        #region private
        /// <summary>
        /// 
        /// </summary>
        public void LoadSettings()
        {
            System.Configuration.Configuration cfg = 
              System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            if (cfg != null)
            {
                if (cfg.AppSettings.Settings["SectionTopsUpdatingInterval"] != null &&
                    cfg.AppSettings.Settings["SectionTopsUpdatingInterval"].Value != null)
                {
                    try
                    {
                        Configuration.SetSectionTopsUpdatingInterval(1000 * Convert.ToInt32(cfg.AppSettings.Settings["SectionTopsUpdatingInterval"].Value));
                    }
                    catch { }
                }

                if (cfg.AppSettings.Settings["Top10sLoadingInterval"] != null &&
                    cfg.AppSettings.Settings["Top10sLoadingInterval"].Value != null)
                {
                    try
                    {
                        Configuration.SetTop10sLoadingInterval(1000 * Convert.ToInt32(cfg.AppSettings.Settings["Top10sLoadingInterval"].Value));
                    }
                    catch { }
                }

                if (cfg.AppSettings.Settings["NewMailCheckingInterval"] != null &&
                    cfg.AppSettings.Settings["NewMailCheckingInterval"].Value != null)
                {
                    try
                    {
                        Configuration.SetNewMailCheckingInterval(1000 * Convert.ToInt32(cfg.AppSettings.Settings["NewMailCheckingInterval"].Value));
                    }
                    catch { }
                }

                if (cfg.AppSettings.Settings["BaseControlContainerLocationMargin"] != null &&
                    cfg.AppSettings.Settings["BaseControlContainerLocationMargin"].Value != null &&
                    cfg.AppSettings.Settings["BaseControlLocationMargin"] != null &&
                    cfg.AppSettings.Settings["BaseControlLocationMargin"].Value != null)
                {
                    try
                    {
                        Configuration.SetLocationMargin(Convert.ToInt32(cfg.AppSettings.Settings["BaseControlContainerLocationMargin"].Value),
                                                        Convert.ToInt32(cfg.AppSettings.Settings["BaseControlLocationMargin"].Value));


                        this.cmbBaseControlContainerLocationMargin.Text = cfg.AppSettings.Settings["BaseControlContainerLocationMargin"].Value;
                        this.cmbBaseControlLocationMargin.Text = cfg.AppSettings.Settings["BaseControlLocationMargin"].Value;
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveSettings()
        {
            System.Configuration.Configuration cfg = 
                System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            if (cfg != null)
            {
                if (cfg.AppSettings.Settings["SectionTopsUpdatingInterval"] != null)
                {
                    try
                    {
                        cfg.AppSettings.Settings["SectionTopsUpdatingInterval"].Value =
                            (Configuration.SectionTopsUpdatingInterval / 1000).ToString();
                    }
                    catch { }
                }

                if (cfg.AppSettings.Settings["Top10sLoadingInterval"] != null)
                {
                    try
                    {
                        cfg.AppSettings.Settings["Top10sLoadingInterval"].Value =
                            (Configuration.Top10sLoadingInterval / 1000).ToString();
                    }
                    catch { }
                }

                if (cfg.AppSettings.Settings["NewMailCheckingInterval"] != null)
                {
                    try
                    {
                        cfg.AppSettings.Settings["NewMailCheckingInterval"].Value =
                            (Configuration.NewMailCheckingInterval / 1000).ToString();
                    }
                    catch { }
                }

                if (cfg.AppSettings.Settings["BaseControlContainerLocationMargin"] != null)
                {
                    try
                    {
                        cfg.AppSettings.Settings["BaseControlContainerLocationMargin"].Value = 
                            this.cmbBaseControlContainerLocationMargin.Text;
                    }
                    catch { }
                }

                if (cfg.AppSettings.Settings["BaseControlLocationMargin"] != null)
                {
                    try
                    {
                        cfg.AppSettings.Settings["BaseControlLocationMargin"].Value =
                            this.cmbBaseControlLocationMargin.Text;
                    }
                    catch { }
                }

                cfg.Save();
            }
        }
        #endregion
    }
}