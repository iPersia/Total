namespace Nzl.Smth.Containers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Windows.Forms;
    using Nzl.Web.Page;
    using Nzl.Web.Util;
    using Nzl.Smth.Datas;
    using Nzl.Smth.Utils;
    using Nzl.Smth.Log;

    /// <summary>
    /// 
    /// </summary>
    public partial class LoginControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnLogCompleted;

        /// <summary>
        /// 
        /// </summary>
        private string _filename = "userinfor.dat";

        /// <summary>
        /// 
        /// </summary>
        public LoginControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);            
            this.txtUserID.Left = this.panelContainer.Width / 2 - this.txtUserID.Width / 2;
            this.txtPassword.Left = this.panelContainer.Width / 2 - this.txtPassword.Width / 2;
            this.ckbAutoLogon.Left = this.panelContainer.Width / 2 - this.ckbAutoLogon.Width / 2;
            this.btnLogon.Left = this.panelContainer.Width / 2 - this.btnLogon.Width / 2;
            this.btnLogout.Left = this.panelUp.Width / 2 - this.btnLogout.Width / 2;
            this.lblNote.Left = this.panelUp.Width / 2 - this.lblNote.Width / 2;            

            TextBoxUtil.SetWatermark(this.txtUserID, "Input username...");
            TextBoxUtil.SetWatermark(this.txtPassword, "Input password...");

            DeserializeUserInfor();
        }

        #region event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtUserID.Text) == false && string.IsNullOrEmpty(this.txtPassword.Text) == false)
            {
                this.Enabled = false;
                WebPage.RemoveCookie(Configurations.BaseUrl);
                LogIn(this.txtUserID.Text, this.txtPassword.Text);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            WebPage.RemoveCookie(Configurations.BaseUrl);
            LogOut();
        }        
        #endregion

        #region Login & LogOut
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        private void LogIn(string userID, string password)
        {
            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            this.bwFetchPage.DoWork += new System.ComponentModel.DoWorkEventHandler(bwFetchPage_LogInOut_DoWork);
            this.bwFetchPage.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwFetchPage_LogInOut_RunWorkerCompleted);
            IList<string> paraList = new List<string>();
            paraList.Add(Configurations.BaseUrl);
            paraList.Add(@"http://m.newsmth.net/user/login");
            paraList.Add(@"id=" + userID + "&passwd=" + password + "&save=on");
            this.bwFetchPage.RunWorkerAsync(paraList);
            this.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LogOut()
        {
            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            this.bwFetchPage.DoWork += new System.ComponentModel.DoWorkEventHandler(bwFetchPage_LogInOut_DoWork);
            this.bwFetchPage.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwFetchPage_LogInOut_RunWorkerCompleted);
            IList<string> paraList = new List<string>();
            paraList.Add(@"http://m.newsmth.net/user/logout");
            this.bwFetchPage.RunWorkerAsync(paraList);
            this.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_LogInOut_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                IList<string> paraList = e.Argument as IList<string>;
                if (paraList != null)
                {
                    WebPage wp = null;
                    //Log In.
                    if (paraList.Count == 3)
                    {
                        wp = WebPageFactory.CreateWebPage(paraList[0], paraList[1], paraList[2]);
                    }

                    //Log Out.
                    if (paraList.Count == 1)
                    {
                        wp = WebPageFactory.CreateWebPage(paraList[0]);
                    }

                    e.Result = wp;
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (Exception exp)
            {
                if (TheLogger.LoggerEnabled)
                {
                    TheLogger.Logger.Error(exp.Message + "\n" + exp.StackTrace);
                }

                e.Cancel = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_LogInOut_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("Log In or Out is Canceled");
            }
            else
            {
                //Check login status.
                WebPage wp = e.Result as WebPage;
                if (wp != null && wp.IsGood)
                {
                    LogStatus.Instance.UpdateLoginStatus(wp);
                }

                if (LogStatus.Instance.IsLogin && this.ckbAutoLogon.Checked)
                {
                    SerializeUserInfor();
                }
            }

            this.Enabled = true;

            if (this.OnLogCompleted != null)
            {
                this.OnLogCompleted(this, new EventArgs());
            }
        }
        #endregion

        #region Serialize
        /// <summary>
        /// 
        /// </summary>
        private void SerializeUserInfor()
        {
            try
            {
                UserInformation ui = new UserInformation();
                ui.UserName = Nzl.Util.EncryptUtil.Encrypt(this.txtUserID.Text, this._filename);
                ui.Password = Nzl.Util.EncryptUtil.Encrypt(this.txtPassword.Text, this._filename);
                byte[] datas = BufferHelper.Serialize(ui);
                byte[] eDatas = Nzl.Util.EncryptUtil.Encrypt(datas, System.Text.Encoding.Default.GetBytes(this._filename));
                Stream fStream = new FileStream(this._filename, FileMode.Create, FileAccess.ReadWrite);
                fStream.Write(eDatas, 0, eDatas.Length);
                fStream.Close();
            }
            catch (Exception exp)
            {
                if (TheLogger.LoggerEnabled)
                {
                    TheLogger.Logger.Error(exp.Message + "\n" + exp.StackTrace);
                }
#if (DEBUG)
                Nzl.Web.Util.CommonUtil.ShowMessage(exp.Message);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DeserializeUserInfor()
        {
            try
            {
                Stream fStream = new FileStream(this._filename, FileMode.Open, FileAccess.ReadWrite);
                if (fStream != null && fStream.Length > 0)
                {
                    byte[] edatas = new byte[fStream.Length];
                    fStream.Read(edatas, 0, (int)fStream.Length);
                    byte[] datas = Nzl.Util.EncryptUtil.Decrypt(edatas, System.Text.Encoding.Default.GetBytes(this._filename));
                    UserInformation ui = (UserInformation)BufferHelper.Deserialize(datas, 0);
                    if (ui != null)
                    {
                        this.txtUserID.Text = Nzl.Util.EncryptUtil.Decrypt(ui.UserName, this._filename);
                        this.txtPassword.Text = Nzl.Util.EncryptUtil.Decrypt(ui.Password, this._filename);
                    }
                }

                fStream.Close();
            }
            catch (Exception exp)
            {
                if (TheLogger.LoggerEnabled)
                {
                    TheLogger.Logger.Error(exp.Message + "\n" + exp.StackTrace);
                }
#if (DEBUG)
                Nzl.Web.Util.CommonUtil.ShowMessage(exp.Message);
#endif
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private class BufferHelper
        {
            public static byte[] Serialize(object obj)
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();
                bf.Serialize(stream, obj);
                byte[] datas = stream.ToArray();
                stream.Dispose();
                return datas;
            }

            public static object Deserialize(byte[] datas, int index)
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream stream = new MemoryStream(datas, index, datas.Length - index);
                object obj = bf.Deserialize(stream);
                stream.Dispose();
                return obj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Serializable]
        private class UserInformation
        {
            /// <summary>
            /// 
            /// </summary>
            private string _userID;

            /// <summary>
            /// 
            /// </summary>
            private string _password;

            /// <summary>
            /// 
            /// </summary>
            public string UserName
            {
                get
                {
                    return this._userID;
                }
                set
                {
                    this._userID = value;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public string Password
            {
                get
                {
                    return this._password;
                }

                set
                {
                    this._password = value;
                }
            }
        }
    }
}
