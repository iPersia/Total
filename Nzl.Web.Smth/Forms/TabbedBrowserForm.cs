namespace Nzl.Web.Smth.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Nzl.Hook;
    using Nzl.Web.Smth.Controls;

    /// <summary>
    /// 
    /// </summary>
    public partial class TabbedBrowserForm : Form
    {
        #region Singleton
        /// <summary>
        /// 
        /// </summary>
        public static readonly TabbedBrowserForm Instance = new TabbedBrowserForm();
        #endregion

        #region Variable
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, object> _dicWindows = new Dictionary<string, object>();        

        /// <summary>
        /// 
        /// </summary>
        private Form _parentForm = null;

        /// <summary>
        /// 
        /// </summary>
        private const int CLOSE_SIZE = 16;

        /// <summary>
        /// 
        /// </summary>
        private const int PADDING_SIZE = 4;

        /// <summary>
        /// 
        /// </summary>
        private UserActivityHook _uahKey = new UserActivityHook(false, true);
        #endregion

        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        TabbedBrowserForm()
        {
            InitializeComponent();
            LoginForm.LoginStatusChanged += new LoginStatusChangedHandler(LoginForm_LoginStatusChanged);
            _uahKey.KeyUp += new EventHandler<KeyExEventArgs>(Global_KeyUp);
            _uahKey.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        TabbedBrowserForm(Form parent)
            : this()
        {
            this._parentForm = parent;            
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        public void SetParent(Form parent)
        {
            this._parentForm = parent;
        }
        #endregion

        #region Topic
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public void AddTopic(string url, string subject)
        {
            TabPage tp = GetTabPage(url, subject);
            if (tp != null)
            {
                System.Diagnostics.Debug.WriteLine(this.tcTopics.GetType().ToString() + " before select tabpage - " + tp.Name);
                this.tcTopics.SelectedTab = tp;
                System.Diagnostics.Debug.WriteLine(this.tcTopics.GetType().ToString() + " after select tabpage - " + tp.Name);
                this.Text = subject;
                System.Diagnostics.Debug.WriteLine(tp.Name + "'s Size is- " + tp.Size);
            }
        }
        #endregion

        #region Board
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="title"></param>
        public void AddBoard(string url, string title)
        {
            TabPage tp = new TabPage();
            tp.Name = "tp" + url; ;
            tp.Text = "[" + title + "]";
            tp.ToolTipText = tp.Text;
            this.tcTopics.TabPages.Add(tp);
            this.tcTopics.SelectedTab = tp;
                

            BoardBrowserControl bbc = new BoardBrowserControl(url);
            bbc.OnTopicLinkClicked += new LinkLabelLinkClickedEventHandler(BoardBrowserControl_OnTopicLinkClicked);
            bbc.OnTopicCreateIDLinkClicked += new LinkLabelLinkClickedEventHandler(TBF_IDLinkClicked);
            bbc.OnTopicLastIDLinkClicked += new LinkLabelLinkClickedEventHandler(TBF_IDLinkClicked);
            bbc.Dock = DockStyle.Fill;
            tp.Controls.Add(bbc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoardBrowserControl_OnTopicLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                this.AddTopic(e.Link.LinkData.ToString(), linkLabel.Text);
            }
        }
        #endregion

        #region TabPages
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private TabPage GetTabPage(string url, string subject)
        {
            lock (this.tcTopics)
            {
                string key = "tp" + url;
                if (this.tcTopics.TabPages.ContainsKey(key))
                {
                    return this.tcTopics.TabPages[key];
                }

                TabPage tp = new TabPage();
                tp.Name = key;
                //tp.Text = subject==null ? "Unknown" : subject.Length > 8 ? subject.Substring(0, 8) + ".." : "" + subject;
                tp.Text = subject;
                tp.ToolTipText = subject;      
                this.tcTopics.TabPages.Add(tp);
                
                System.Diagnostics.Debug.WriteLine(this.tcTopics.GetType().ToString() + " add tabpage - " + tp.Name);
                System.Diagnostics.Debug.WriteLine(tp.Name + "'s Size is- " + tp.Size);

                TopicBrowserControl tbc = new TopicBrowserControl();                
                tbc.Name = "tbc" +　url;
                System.Diagnostics.Debug.WriteLine(tbc.GetType().ToString() + " - " + tbc.Name + " - Created");
                tbc.TopicUrl = url;                
                System.Diagnostics.Debug.WriteLine(tbc.GetType().ToString() + " - " + tbc.Name + " - is ADDED TO tp");
                tp.Controls.Add(tbc);
                tbc.Dock = DockStyle.Fill;
                return tp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcTopics_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = (this.tcTopics.SelectedTab == null) ? "Browser" : this.tcTopics.SelectedTab.ToolTipText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabbedTopicBrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            this._parentForm.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcTopics_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.tcTopics.SelectedIndex > -1)
            {
                TabPage tp = this.tcTopics.TabPages[this.tcTopics.SelectedIndex];
                this.tcTopics.TabPages.Remove(tp);
                tp.Dispose();
                GC.Collect();
            }
        }
        #endregion

        #region Event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void Global_KeyUp(object sender, KeyExEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Global_KeyUp - " + HookUtil.GetForegroundWindowThreadProcessName());
            System.Diagnostics.Debug.WriteLine("Global_KeyUp - " +
                                               "Control.ModifierKeys " + Control.ModifierKeys + "\t" +
                                               "Modifiers " + e.Modifiers + "\t" +
                                               "Alt " + e.Alt + "\t" +
                                               "Control " + e.Control + "\t" +
                                               "Shift " + e.Shift + "\t" +
                                               e.KeyCode + "\t" + e.KeyValue + "\t" + e.KeyData);
            if (HookUtil.GetForegroundWindowThreadProcessName() == "Nzl.Web.Smth")
            {
                if ((e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey) && Control.ModifierKeys == Keys.Control)
                {
                    ShowTop10s();
                }
            }
        }

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
        private void TBF_IDLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                UserForm userForm = new UserForm(e.Link.LinkData.ToString());
                userForm.StartPosition = FormStartPosition.CenterScreen;
                userForm.ShowDialog(this);
            }
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
                form.OnFavorBoardLinkLableClicked -= FavorForm_OnFavorBoardLinkLableClicked;
                form.OnFavorBoardLinkLableClicked += FavorForm_OnFavorBoardLinkLableClicked;
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
        private void FavorForm_OnFavorBoardLinkLableClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                TabbedBrowserForm.Instance.AddBoard(e.Link.LinkData.ToString(), linkLabel.Text);
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (TabPage tp in this.tcTopics.TabPages)
            {
                tp.Dispose();
            }

            this.tcTopics.TabPages.Clear();
            GC.Collect();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHome_Click(object sender, EventArgs e)
        {
            if (this._parentForm != null)
            {
                this._parentForm.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadTop_Click(object sender, EventArgs e)
        {
            ShowTop10s();
        }
        #endregion

        #region Private
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

                        this.linklblUserID.LinkClicked -= new LinkLabelLinkClickedEventHandler(TBF_IDLinkClicked);
                        this.linklblUserID.LinkClicked += new LinkLabelLinkClickedEventHandler(TBF_IDLinkClicked);
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
        private void ShowTop10s()
        {
            Form form = Top10sForm.Instance;
            if (form != null && form.IsDisposed == false)
            {
                form.StartPosition = FormStartPosition.Manual;
                int centerX = this.Location.X + this.Size.Width / 2;
                int centerY = this.Location.Y + this.Size.Height / 2;
                form.Location = new System.Drawing.Point(centerX - form.Size.Width / 2, centerY - form.Size.Height / 2);
                form.Visible = !form.Visible;
            }
        }
        #endregion        
    }
}
