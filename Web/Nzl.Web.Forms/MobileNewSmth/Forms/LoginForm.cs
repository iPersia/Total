namespace Nzl.Web.Forms.MobileNewSmth.Forms
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
    using Nzl.Web.Forms.MobileNewSmth.Utils;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LoginStatusChangedHandler(object sender, LoginEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    public partial class LoginForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly LoginForm Instance = new LoginForm();

        /// <summary>
        /// 
        /// </summary>
        private static object _isLoginLocker = new object();

        /// <summary>
        /// 
        /// </summary>
        private static bool _isLogin = false;

        /// <summary>
        /// 
        /// </summary>
        private static string _userID = "NOT LOGIN";

        /// <summary>
        /// 
        /// </summary>
        private static string _userPassword = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        private string _filename = "userinfor.dat";

        /// <summary>
        /// 
        /// </summary>
        public static event LoginStatusChangedHandler LoginStatusChanged;

        /// <summary>
        /// 
        /// </summary>
        LoginForm()
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
            this.Deactivate += new EventHandler(LoginForm_Deactivate);
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
                WebPage.RemoveCookie(@"http://m.newsmth.net");
                LogIn(this.txtUserID.Text, this.txtPassword.Text);
                if (this.ckbAutoLogon.Enabled)
                {
                    _userID = this.txtUserID.Text;
                    _userPassword = this.txtPassword.Text;
                }
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
            WebPage.RemoveCookie(@"http://m.newsmth.net");
            LogOut();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Deactivate(object sender, EventArgs e)
        {
            this.Visible = false;
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
            paraList.Add(@"http://m.newsmth.net");
            paraList.Add(@"http://m.newsmth.net/user/login");
            paraList.Add(@"id=" + userID + "&passwd=" + password + "&save=on");
            this.bwFetchPage.RunWorkerAsync(paraList);
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
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
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
                if (wp.IsGood)
                {
                    LoginForm.UpdateLoginStatus(wp);
                }

                if (LoginForm.IsLogin && this.ckbAutoLogon.Checked)
                {
                    SerializeUserInfor();
                }
            }

            this.Enabled = true;
            this.Visible = false;
        }
        #endregion

        #region Static updating login status
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        public static void UpdateLoginStatus(WebPage page)
        {
            lock (_isLoginLocker)
            {
                _isLogin = SmthUtil.GetLogInStatus(page);
                _userID = SmthUtil.GetLogInUserID(page);
                LoginForm.OnLoginStatusChanged(_isLogin);
            }
        }

        protected static void OnLoginStatusChanged(bool isLogin)
        {
            if (LoginForm.LoginStatusChanged != null)
            {
                LoginEventArgs e = new LoginEventArgs();
                e.IsLogin = isLogin;
                LoginStatusChanged(LoginForm.ActiveForm, e);
            }
        }
        #endregion

        #region Get login status
        /// <summary>
        /// 
        /// </summary>
        public static bool IsLogin
        {
            get
            {
                lock (_isLoginLocker)
                {
                    return _isLogin;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string UserID
        {
            get
            {
                lock (_userID)
                {
                    return _userID;
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
                    UserInformation ui = (UserInformation)BufferHelper.Deserialize(datas,0);
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

    /// <summary>
    /// 
    /// </summary>
    public class LoginEventArgs : EventArgs
    {
        public bool IsLogin
        {
            get;
            set;
        }
    }
}
