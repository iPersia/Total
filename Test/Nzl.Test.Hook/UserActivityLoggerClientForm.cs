using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;

namespace Nzl.Test.Hook
{
    public partial class UserActivityLoggerClientForm : Form
    {
        /// <summary>
        /// Boolean flag indicates whether the window will be closed.
        /// </summary>
        private bool _closeWindow = false;

        /// <summary>
        /// 
        /// </summary>
        private UserActivityLogger _uaLogger;

        /// <summary>
        /// The text line height of the RichTextBox, default to 16.
        /// </summary>
        private int _txtCharWidth = 16;

        /// <summary>
        /// The text line height of the RichTextBox, default to 16.
        /// </summary>        
        private int _txtLineHeight = 16;

        /// <summary>
        /// The d-value of width between form and RichTextBox, default to 26.
        /// </summary>
        private int _dValueOfWidthBetFormAndRichTextBox = 6;
        
        /// <summary>
        /// The d-value of height between form and RichTextBox, default to 26.
        /// </summary>
        private int _dValueOfHeightBetFormAndRichTextBox = 26;        

        public UserActivityLoggerClientForm()
        {
            InitializeComponent();
            try
            {
                string logFileName = @"UALog.log";//Default ot UALog.log file name.
                if (false == string.IsNullOrEmpty(ConfigurationManager.AppSettings["logFileName"]))
                {
                    logFileName = ConfigurationManager.AppSettings["logFileName"];
                }

                this._uaLogger = UserActivityLogger.Instance;//new UserActivityLogger(true, true, logFileName);

                this.uiTimer.Interval = 5000;// Default to 5 second.
                if (false == string.IsNullOrEmpty(ConfigurationManager.AppSettings["updateInterval"]))
                {
                    this.uiTimer.Interval = System.Convert.ToInt32(ConfigurationManager.AppSettings["updateInterval"]) * 1000;
                }
                
                this.logTimer.Interval = 30 * 60 * 1000; //Default to 30 minutes.
                if (false == string.IsNullOrEmpty(ConfigurationManager.AppSettings["logInterval"]))
                {
                    this.logTimer.Interval = System.Convert.ToInt32(ConfigurationManager.AppSettings["logInterval"]) * 60 * 1000;
                }

                this.uiTimer.Start();
                this.logTimer.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Accur: " + e.Message);
            }
        }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
            UpdateText();
        }


        private void logTimer_Tick(object sender, EventArgs e)
        {
            this._uaLogger.Log();
        }

        private void UpdateText()
        {
            this.txtBox.Clear();
            this.txtBox.AppendText(this._uaLogger.ToString());
            this.UpdateFormSize();
            this.txtBox.SelectionStart = 0;
        }

        private void UpdateFormSize()
        {
            if (this.txtBox != null)
            {
                int rowCount = this.txtBox.GetLineFromCharIndex(this.txtBox.SelectionStart) + 1;
                this.Height = rowCount * this._txtLineHeight
                            + this.txtBox.Margin.Top
                            //+ this.txtBox.Margin.Bottom
                            + 1
                            + this._dValueOfHeightBetFormAndRichTextBox;                

                this.Width = (this.txtBox.Text.IndexOf('\n') + 1) * this._txtCharWidth
                           + 1 //Starting postion.
                           + this._dValueOfWidthBetFormAndRichTextBox;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserActivityLoggerForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                ShowWindow(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowWindow(!this.Visible);
        }

        private void UserActivityLoggerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._closeWindow == false)
            {
                e.Cancel = true;
                this.ShowWindow(false);
            }

            if (e.Cancel == false)
            {
                Nzl.Utils.FileUtil.WriteText(this._uaLogger.LogFileName, "User activity logger exit for the reason " + e.CloseReason.ToString() + "!\n");
                this._uaLogger.Log();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsNotifyIcon_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "tsmiExit")
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure to exit?", "Warning", MessageBoxButtons.YesNo))
                {
                    this._closeWindow = true;
                    this.Close();
                }
            }

            if (e.ClickedItem.Name == "tsmiLog")
            {
                this._uaLogger.Log();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        private void ShowWindow(bool flag)
        {
            this.Visible = flag;
            this.ShowInTaskbar = flag;
            if (flag)
            {
                this.Activate();
            }
        }


        #region 将程序添加到启动项
        /// <summary>
        /// 注册表操作，将程序添加到启动项
        /// </summary>
        private static void SetRegistryApp()
        {
            try
            {
                Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (reg == null)
                {
                    reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                }

                reg.SetValue(Application.ProductName, Application.ExecutablePath);
            }
            catch
            {

            }
        }
        #endregion

        #region 将程序从启动项中删除
        /// <summary>
        /// 注册表操作，删除注册表中启动项
        /// </summary>
        private static bool DeleteRegisterApp()
        {            
            try
            {
                Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (reg == null)
                {
                    reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                }

                reg.DeleteValue(Application.ProductName, false);
                return true;
            }
            catch
            {
                return false;
            }           
        }
        #endregion

        #region 检查当前程序是否在启动项中
        /// <summary>
        /// 检查当前程序是否在启动项中
        /// </summary>
        /// <returns></returns>
        private static bool CheckExistRegisterApp()
        {
            try
            {
                Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (reg == null)
                {
                    reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                }

                foreach (string s in reg.GetValueNames())
                {
                    if (s.Equals(Application.ProductName))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteRegisterApp();
            SetRegistryApp();
            MessageBox.Show(this,
                            "Turn start on system startup on " + (CheckExistRegisterApp() ? "Succeeded!" : "Failed!"),
                            "Information",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteRegisterApp();
            MessageBox.Show(this,
                            "Turn start on system startup off " + (CheckExistRegisterApp() ? "Failed!" : "Succeeded!"),
                            "Information",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void UserActivityLoggerForm_Shown(object sender, EventArgs e)
        {
            this.txtBox.AppendText("  \n  \n  ");
            System.Drawing.Point ptLine1 = this.txtBox.GetPositionFromCharIndex(this.txtBox.GetFirstCharIndexFromLine(0));
            System.Drawing.Point ptLine2 = this.txtBox.GetPositionFromCharIndex(this.txtBox.GetFirstCharIndexFromLine(1));
            System.Drawing.Point ptChar1 = this.txtBox.GetPositionFromCharIndex(0);
            System.Drawing.Point ptChar2 = this.txtBox.GetPositionFromCharIndex(1);
            this._txtCharWidth = (ptChar2.X - ptChar1.X);
            this._txtLineHeight = (ptLine2.Y - ptLine1.Y);
            this._dValueOfWidthBetFormAndRichTextBox = this.Width - this.txtBox.Width;
            this._dValueOfHeightBetFormAndRichTextBox = this.Height - this.txtBox.Height;            
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this,
                            "Name:\t" 
                            + Application.ProductName 
                            + "\nVersion:\t" + Application.ProductVersion 
                            + "\n\n\tCreate by Nesus\n\tningzhlei@gmail.com",
                            "Information",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }
    }
}
