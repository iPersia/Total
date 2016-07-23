namespace Nzl.Web.Smth.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;
    using Nzl.Web.Smth.Utils;
    using Nzl.Web.Smth.Datas;
    using Nzl.Web.Smth.Controls;


    /// <summary>
    /// The new smth form.
    /// </summary>
    public partial class SmthForm : Form
    {
        #region Variable
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, object> _dicWindows = new Dictionary<string, object>();
        #endregion

        #region Ctors.
        /// <summary>
        /// Ctor.
        /// </summary>
        public SmthForm()
        {
            InitializeComponent();
            LoginForm.LoginStatusChanged += new LoginStatusChangedHandler(LoginForm_LoginStatusChanged);

            //线程间操作无效: 从不是创建控件的线程访问它
            //Panel.CheckForIllegalCrossThreadCalls = false;            
        }
        #endregion

        #region overdide
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            Top10sBrowserControl tbc = new Top10sBrowserControl();
            tbc.Name = "tbcTop10s";
            tbc.Top = 1;
            tbc.Left = 1;
            this.panelContainer.Controls.Add(tbc);
            this.Width = tbc.Width + 2 + this.Width - this.panelContainer.Width;
            this.Height = tbc.Height + 2 + this.Height - this.panelContainer.Height;
            tbc.OnTopLinkClicked += new LinkLabelLinkClickedEventHandler(tbc_OnTopLinkClicked);

            this.MouseDown += new MouseEventHandler(SmthForm_MouseDown);
            this.MouseMove += new MouseEventHandler(SmthForm_MouseMove);
            this.MouseUp += new MouseEventHandler(SmthForm_MouseUp);

            ///TabbedBrowserForm
            TabbedBrowserForm.Instance.SetParent(this);
        }
        #endregion

        #region Properties
        #endregion

        #region Event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_LoginStatusChanged(object sender, LoginEventArgs e)
        {
            SetButtonVisibleByLogInStatus(e.IsLogin);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            LoginForm.Instance.StartPosition = FormStartPosition.CenterParent;
            LoginForm.Instance.ShowDialog(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBoardNavi_Click(object sender, EventArgs e)
        {
            BoardNavigatorForm form = this.GetRegisteredForm<BoardNavigatorForm>();
            if (form != null)
            {
                form.StartPosition = FormStartPosition.Manual;
                int centerX = this.Location.X + this.Size.Width / 2;
                int centerY = this.Location.Y + this.Size.Height / 2;
                form.Location = new System.Drawing.Point(centerX - form.Size.Width / 2, centerY - form.Size.Height / 2);
                form.Show();
                form.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFavor_Click(object sender, EventArgs e)
        {
            FavorForm form = this.GetRegisteredForm<FavorForm>();
            if (form != null)
            {
                form.StartPosition = FormStartPosition.Manual;
                int centerX = this.Location.X + this.Size.Width / 2;
                int centerY = this.Location.Y + this.Size.Height / 2;
                form.Location = new System.Drawing.Point(centerX - form.Size.Width / 2, centerY - form.Size.Height / 2);
                form.Show();
                form.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMail_Click(object sender, EventArgs e)
        {
            MailBoxForm form = this.GetRegisteredForm<MailBoxForm>();
            if (form != null)
            {
                form.StartPosition = FormStartPosition.Manual;
                int centerX = this.Location.X + this.Size.Width / 2;
                int centerY = this.Location.Y + this.Size.Height / 2;
                form.Location = new System.Drawing.Point(centerX - form.Size.Width / 2, centerY - form.Size.Height / 2);
                form.Show();
                form.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMessge_Click(object sender, EventArgs e)
        {
            ShowFormOnCenterParent(MessageCenterForm.Instance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        private void ShowFormOnCenterParent(Form form)
        {
            if (form != null && form.IsDisposed == false)
            {
                form.StartPosition = FormStartPosition.Manual;
                int centerX = this.Location.X + this.Size.Width / 2;
                int centerY = this.Location.Y + this.Size.Height / 2;
                form.Location = new System.Drawing.Point(centerX - form.Size.Width / 2, centerY - form.Size.Height / 2);
                form.Show();
                form.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbc_OnTopLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linklbl = sender as LinkLabel;
            if (linklbl != null)
            {
                TabbedBrowserForm.Instance.AddTopic(e.Link.LinkData.ToString(), linklbl.Text);
                //SmthForm.Browser.AddTopic(e.Link.LinkData.ToString(), linklbl.Text);
                TabbedBrowserForm.Instance.Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetButtonVisibleByLogInStatus(bool flag)
        {
            if (this.IsDisposed == false)
            {
                lock (this.linklblUserID)
                {
                    if (flag)
                    {
#if (DEBUG)
                        Nzl.Web.Util.CommonUtil.ShowMessage("this.linklblUserID.Links count:" + this.linklblUserID.Links.Count);
#endif

                        string welcomeStr = "Welcome ";
                        this.linklblUserID.Text = welcomeStr + LoginForm.UserID + "!";
                        this.linklblUserID.Links.Clear();
                        this.linklblUserID.Links.Add(welcomeStr.Length, LoginForm.UserID.Length, LoginForm.UserID);

                        this.linklblUserID.LinkClicked -= new LinkLabelLinkClickedEventHandler(linklblUserID_LinkClicked);
                        this.linklblUserID.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblUserID_LinkClicked);
                        this.btnLogon.Text = "Log Out";
                    }
                    else
                    {
                        this.linklblUserID.Text = "Welcome!";
                        this.linklblUserID.Links.Clear();
                        this.btnLogon.Text = "Log In";
                    }
                }

                this.btnFavor.Visible = flag;
                this.btnMail.Visible = flag;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblUserID_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lock (this.linklblUserID)
            {
                LinkLabel linkLabel = sender as LinkLabel;
                if (linkLabel != null)
                {
                    UserForm userForm = new UserForm(e.Link.LinkData.ToString());
                    userForm.StartPosition = FormStartPosition.CenterParent;
                    userForm.ShowDialog(this);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        private T GetRegisteredForm<T>()
            where T : class, new()
        {
            string key = typeof(T).ToString();
            if (this._dicWindows.ContainsKey(key))
            {
                Form form = this._dicWindows[key] as Form;
                if (form.IsDisposed == false)
                {
                    return this._dicWindows[key] as T;
                }
            }

            T t = new T();
            this._dicWindows.Remove(key);
            this._dicWindows.Add(key, t);
            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        private T GetRegisteredForm<T>(string key)
            where T : class, new()
        {
            if (this._dicWindows.ContainsKey(key))
            {
                Form form = this._dicWindows[key] as Form;
                if (form.IsDisposed == false)
                {
                    return this._dicWindows[key] as T;
                }
            }

            T t = new T();
            this._dicWindows.Remove(key);
            this._dicWindows.Add(key, t);
            return t;
        }
        #endregion

        #region Drag & Drop
        //[DllImport("user32.dll")]
        //public static extern bool ReleaseCapture();

        //[DllImport("user32.dll")]
        //public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void SmthForm_MouseDown(object sender, MouseEventArgs e)
        //{
        //    ReleaseCapture();
        //    SendMessage(this.Handle, 0x0112, 0xF012, 0);
        //} 


        /// <summary>
        /// 鼠标移动位置变量  
        /// </summary>
        Point mouseOff;

        /// <summary>
        /// 标签是否为左键
        /// </summary>
        bool leftFlag;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SmthForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值  
                leftFlag = true;                  //点击左键按下时标注为true;  
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SmthForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置  
                Location = mouseSet;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SmthForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;  
            }
        }
        #endregion
    }
}
