using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nzl.Web.Smth.Forms
{
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
                TabbedBrowserForm.Instance.Focus();
            }            
        }
        #endregion
    }
}
