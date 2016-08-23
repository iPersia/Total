namespace Nzl.Smth.Forms
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Nzl.Hook;
    using Nzl.Smth.Common;
    using Nzl.Smth.Logger;

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
            //Boards.Instance.Initilize();

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
                return;
            }

            e.Cancel = MessageBox.Show(this,
                                      "There exists some window active, do you want close the form?",
                                      "Warning",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes;

            if (e.Cancel == true)
            {
                return;
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
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\n" + exp.StackTrace);
                }
            };
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
                TabbedBrowserForm.Instance.Activate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAbout_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this._closeFlag = "NotifyIcon";
            this.Close();
        }
        #endregion
    }
}
