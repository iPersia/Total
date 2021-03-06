﻿namespace Nzl.Smth.Controls.Complexes
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Windows.Forms;
    using Nzl.Smth;
    using Nzl.Smth.Configs;
    using Nzl.Smth.Loaders;
    using Nzl.Smth.Logger;
    using Nzl.Smth.Utils;
    using Nzl.Utils;
    using Nzl.Web.Page;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public partial class LoginControl : UserControl
    {
        #region event
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnLoginCompleted;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnLogoutCompleted;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MessageEventArgs> OnLoginFailed;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MessageEventArgs> OnLogoutFailed;
        #endregion

        #region variable
        /// <summary>
        /// 
        /// </summary>
        private string _filename = "userinfor.dat";
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        public LoginControl()
        {
            InitializeComponent();
            LogStatus.Instance.OnLoginStatusChanged += Instance_OnLoginStatusChanged;
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
            this.txtUserID.Left = this.panelContainer.Width / 2 - this.txtUserID.Width / 2;
            this.txtPassword.Left = this.panelContainer.Width / 2 - this.txtPassword.Width / 2;
            this.ckbSave.Left = this.panelContainer.Width / 2 - this.ckbSave.Width / 2;
            this.btnLogon.Left = this.panelContainer.Width / 2 - this.btnLogon.Width / 2;
            this.btnLogout.Left = this.panelUp.Width / 2 - this.btnLogout.Width / 2;
            this.lblNote.Left = this.panelUp.Width / 2 - this.lblNote.Width / 2;

            TextBoxUtil.SetWatermark(this.txtUserID, "Input username...");
            TextBoxUtil.SetWatermark(this.txtPassword, "Input password...");

            DeserializeUserInfor();
        }
        #endregion

        #region event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Instance_OnLoginStatusChanged(object sender, LogStatusEventArgs e)
        {
            this.SetLogStatus(e.IsLogin);
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetLogStatus(bool flag)
        {
            if (this.IsHandleCreated)
            {
                if (this.InvokeRequired)
                {
                    System.Threading.Thread.Sleep(0);
                    //this.Invoke(new OnLogStatusChangedCallBack(SetLogStatus), new object[] { flag });
                    this.Invoke(new MethodInvoker(delegate () {
                        this.SetLogStatus(flag);
                    }));
                    System.Threading.Thread.Sleep(0);
                }
                else
                {
                    lock (this.btnLogon)
                    {
                        this.txtUserID.Enabled = !flag;
                        this.txtPassword.Enabled = !flag;
                        this.ckbSave.Enabled = !flag;
                        this.btnLogon.Enabled = !flag;
                        this.btnLogout.Enabled = flag;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtUserID.Text) == false && string.IsNullOrEmpty(this.txtPassword.Text) == false)
            {
                WebPageFactory.RemoveCookie(Configuration.BaseUrl);
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
            WebPageFactory.RemoveCookie(Configuration.BaseUrl);
            LogOut();
        }
        #endregion

        #region Login & LogOut

        #region Login
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        private void LogIn(string userID, string password)
        {
            PageLoader pl = new PageLoader(Configuration.LoginUrl, @"id=" + userID + "&passwd=" + password + "&save=on");
            pl.PageLoaded += LoginPageLoader_PageLoaded;
            pl.PageFailed += PageLoader_PageFailed;
            PageDispatcher.Instance.Add(pl);
            this.SetControlEnabled(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginPageLoader_PageLoaded(object sender, EventArgs e)
        {
            PageLoader pl = sender as PageLoader;
            if (pl != null)
            {
                string html = pl.GetResult() as string;
                if (html != null)
                {
                    if (this.IsHandleCreated)
                    {
                        if (this.InvokeRequired)
                        {
                            System.Threading.Thread.Sleep(0);
                            ///this.Invoke(new LogPageLoadedCallback(LoginPageLoaded), new object[] { html });
                            this.Invoke(new MethodInvoker(delegate () {
                                this.LoginPageLoaded(html);
                            }));
                            System.Threading.Thread.Sleep(0);
                        }
                    }
                }
                else
                {
                    this.SetControlEnabled(true);
                    this.SetLogStatus(LogStatus.Instance.IsLogin);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageLoader_PageFailed(object sender, EventArgs e)
        {
            if (this.IsHandleCreated)
            {
                if (this.InvokeRequired)
                {
                    System.Threading.Thread.Sleep(0);
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        this.PageLoader_PageFailed(sender, e);
                    }));
                    System.Threading.Thread.Sleep(0);
                }
                else
                {
                    this.SetControlEnabled(true);
                    this.SetLogStatus(LogStatus.Instance.IsLogin);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlInfo"></param>
        private void LoginPageLoaded(string html)
        {
            BackgroundWorker bwLogin = new BackgroundWorker();
            bwLogin.DoWork += Login_DoWork;
            bwLogin.RunWorkerCompleted += Login_RunWorkerCompleted;
            bwLogin.RunWorkerAsync(html);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string html = e.Argument as string;
                LogStatus.Instance.UpdateStatus(html);
                if (LogStatus.Instance.IsLogin)
                {
                    if (this.ckbSave.Checked)
                    {
                        SerializeUserInfor();
                    }

                    e.Result = "Success";
                }
                else
                {
                    e.Result = CommonUtil.GetMatch(@"<div class=\Wsp hl f\W>(?'Information'[^<]+)</div>", html, "Information");
                }
            }
            catch (Exception exp)
            {
                e.Cancel = true;
                e.Result = exp;
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\t" + exp.StackTrace);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (this.OnLoginFailed != null)
                {
                    this.OnLoginFailed(this, new MessageEventArgs("Exception accurs: " + (e.Error != null ? e.Error.Message : "unknown") + "!"));
                }
            }
            else if (e.Cancelled)
            {
                if (this.OnLoginFailed != null)
                {
                    this.OnLoginFailed(this, new MessageEventArgs("Login is cancelled"));
                }
            }
            else
            {
                if (e.Result.ToString() == "Success")
                {
                    if (this.OnLoginCompleted != null)
                    {
                        this.OnLoginCompleted(this, new EventArgs());
                    }
                }
                else
                {
                    if (this.OnLoginFailed != null)
                    {
                        this.OnLoginFailed(this, new MessageEventArgs(e.Result == null ? "Login failed!" : e.Result.ToString()));
                    }
                }
            }

            this.SetControlEnabled(true);
            this.SetLogStatus(LogStatus.Instance.IsLogin);
        }
        #endregion

        #region Logout
        /// <summary>
        /// 
        /// </summary>
        private void LogOut()
        {
            PageLoader pl = new PageLoader(Configuration.LogoutUrl, "");
            pl.PageLoaded += LogoutPageLoader_PageLoaded;
            pl.PageFailed += PageLoader_PageFailed;
            PageDispatcher.Instance.Add(pl);
            this.SetControlEnabled(false);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutPageLoader_PageLoaded(object sender, EventArgs e)
        {
            PageLoader pl = sender as PageLoader;
            if (pl != null)
            {
                string html = pl.GetResult() as string;
                if (html != null)
                {
                    if (this.IsHandleCreated)
                    {
                        if (this.InvokeRequired)
                        {
                            System.Threading.Thread.Sleep(0);
                            ///this.Invoke(new LogPageLoadedCallback(LogoutPageLoaded), new object[] { html });
                            this.Invoke(new MethodInvoker(delegate () {
                                this.LogoutPageLoaded(html);
                            }));
                            System.Threading.Thread.Sleep(0);
                        }
                    }
                }
                else
                {
                    this.SetControlEnabled(true);
                    this.SetLogStatus(LogStatus.Instance.IsLogin);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlInfo"></param>
        private void LogoutPageLoaded(string html)
        {
            BackgroundWorker bwLogout = new BackgroundWorker();
            bwLogout.DoWork += Logout_DoWork;
            bwLogout.RunWorkerCompleted += Logout_RunWorkerCompleted;
            bwLogout.RunWorkerAsync(html);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logout_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string html = e.Argument as string;
                LogStatus.Instance.UpdateStatus(html);
                if (LogStatus.Instance.IsLogin == false)
                {
                    e.Result = "Success";
                }
                else
                {
                    e.Result = CommonUtil.GetMatch(@"<div class=\Wsp hl f\W>(?'Information'[^<]+)</div>", html, "Information");
                }
            }
            catch (Exception exp)
            {
                e.Cancel = true;
                e.Result = exp;
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\t" + exp.StackTrace);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logout_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (this.OnLogoutFailed != null)
                {
                    this.OnLogoutFailed(this, new MessageEventArgs("Exception accurs: " + (e.Error != null ? e.Error.Message : "unknown") + "!"));
                }
            }
            else if (e.Cancelled)
            {
                if (this.OnLogoutFailed != null)
                {
                    this.OnLogoutFailed(this, new MessageEventArgs("Login is cancelled"));
                }
            }
            else
            {
                if (e.Result.ToString() == "Success")
                {
                    if (this.OnLogoutCompleted != null)
                    {
                        this.OnLogoutCompleted(this, new EventArgs());
                    }
                }
                else
                {
                    if (this.OnLogoutFailed != null)
                    {
                        this.OnLogoutFailed(this, new MessageEventArgs(e.Result == null ? "Logout failed!" : e.Result.ToString()));
                    }
                }
            }

            this.SetControlEnabled(true);
            this.SetLogStatus(LogStatus.Instance.IsLogin);
        }
        #endregion        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        private void SetControlEnabled(bool flag)
        {
            if (this.IsHandleCreated)
            {
                if (this.InvokeRequired)
                {
                    System.Threading.Thread.Sleep(0);
                    this.Invoke(new MethodInvoker(delegate () {
                        this.SetControlEnabled(flag);
                    }));
                    System.Threading.Thread.Sleep(0);
                }
                else
                {
                    this.txtUserID.Enabled = flag;
                    this.txtPassword.Enabled = flag;
                    this.ckbSave.Enabled = flag;
                    this.btnLogon.Enabled = flag;
                    this.btnLogout.Enabled = flag;
                }
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
                string key = this.GetKey();
                UserInformation ui = new UserInformation();
                ui.UserName = Nzl.Utils.EncryptUtil.Encrypt(this.txtUserID.Text, key);
                ui.Password = Nzl.Utils.EncryptUtil.Encrypt(this.txtPassword.Text, key);
                byte[] datas = BufferHelper.Serialize(ui);
                byte[] eDatas = Nzl.Utils.EncryptUtil.Encrypt(datas, System.Text.Encoding.Default.GetBytes(this._filename));
                using (Stream fStream = new FileStream(this._filename, FileMode.Create, FileAccess.ReadWrite))
                {
                    fStream.Write(eDatas, 0, eDatas.Length);
                }
            }
            catch (Exception exp)
            {
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\n" + exp.StackTrace);
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
                if (File.Exists(this._filename))
                {
                    using (Stream fStream = new FileStream(this._filename, FileMode.Open, FileAccess.ReadWrite))
                    {
                        if (fStream != null && fStream.Length > 0)
                        {
                            string key = this.GetKey();
                            byte[] edatas = new byte[fStream.Length];
                            fStream.Read(edatas, 0, (int)fStream.Length);
                            byte[] datas = EncryptUtil.Decrypt(edatas, System.Text.Encoding.Default.GetBytes(this._filename));
                            UserInformation ui = (UserInformation)BufferHelper.Deserialize(datas, 0);
                            if (ui != null)
                            {
                                this.txtUserID.Text = EncryptUtil.Decrypt(ui.UserName, key);
                                this.txtPassword.Text = EncryptUtil.Decrypt(ui.Password, key);
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\n" + exp.StackTrace);
                }
#if (DEBUG)
                Nzl.Web.Util.CommonUtil.ShowMessage(exp.Message);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetKey()
        {
            return HardwareUtil.GetCpuID() +
                   "_" + 
                   HardwareUtil.GetMacAddress() +
                   "_" +
                   Application.StartupPath + @"\" + this._filename;
        }
        #endregion

        #region Serialize helper.
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
        #endregion
    }
}
