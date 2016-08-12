namespace Nzl.Web.Smth.Forms
{
    using System;
    using System.Windows.Forms;
    using Datas;

    public partial class MainForm : Form
    {
        #region Variable
        /// <summary>
        /// 
        /// </summary>
        private string _closeFlag = null;
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        public MainForm()
        {
            InitializeComponent();            
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

            this.Hide();
            this.ShowInTaskbar = false;
            this.nfiMain.Visible = true;

            ///Load board's infor.
            SmthBoards.Instance.Initilize();

            TabbedBrowserForm.Instance.SetParent(this);
            TabbedBrowserForm.Instance.Show();
            TabbedBrowserForm.Instance.Focus();
        }
        #endregion

        #region eventhandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._closeFlag != "NotifyIcon")
            {
                e.Cancel = true;
                this.ShowInTaskbar = false;
                this.Hide();
                this.nfiMain.Visible = true;
            }

            try
            {
                TabbedBrowserForm.Instance.Hide();
                TabbedBrowserForm.Instance.Clear();
                TabbedBrowserForm.Instance.Dispose();
                GC.Collect();

                LoginForm.Instance.Hide();
                LoginForm.Instance.Dispose();
                GC.Collect();

                Top10sForm.Instance.Hide();
                Top10sForm.Instance.Dispose();
                GC.Collect();

                BoardNavigatorForm.Instance.Hide();
                BoardNavigatorForm.Instance.Dispose();
                GC.Collect();

                MessageCenterForm.Instance.Hide();
                MessageCenterForm.Instance.Dispose();
                GC.Collect();

                MailBoxForm.Instance.Hide();
                MailBoxForm.Instance.Dispose();
                GC.Collect();

                FavorForm.Instance.Hide();
                FavorForm.Instance.Dispose();
                GC.Collect();
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message + "\n" + exp.StackTrace);
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name.Substring(4))
            {
                case "Exit":
                    {
                        this._closeFlag = "NotifyIcon";
                        this.Close();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nfiMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TabbedBrowserForm.Instance.Visible = !TabbedBrowserForm.Instance.Visible;
            if (TabbedBrowserForm.Instance.Visible)
            {
                TabbedBrowserForm.Instance.Show();
                TabbedBrowserForm.Instance.Focus();
            }            
        }
        #endregion
    }
}
