namespace Nzl.Web.Forms.MobileNewSmth.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;
    using Nzl.Web.Forms.MobileNewSmth.Utils;
    using Nzl.Web.Forms.MobileNewSmth.Datas;
    using Nzl.Web.Forms.MobileNewSmth.Controls;
    

    /// <summary>
    /// The new smth form.
    /// </summary>
    public partial class MobileNewSmthForm : Form
    {
        #region Variable
        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Timer _reloadSectionsTimer = new System.Windows.Forms.Timer();

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, string> _dicTopUrl = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, object> _dicWindows = new Dictionary<string, object>();

        /// <summary>
        /// 
        /// </summary>
        private DateTime _lastUpdateTime = DateTime.Now;
        
        /// <summary>
        /// 
        /// </summary>
        private int _locationShiftX = 50;
        
        /// <summary>
        /// 
        /// </summary>
        private int _locationShiftY = 50;

        /// <summary>
        /// 
        /// </summary>
        private LoginForm _loginForm = null;

        /// <summary>
        /// 
        /// </summary>
        private BrowserForm _browserForm = null;

        /// <summary>
        /// 
        /// </summary>
        private static TabbedBrowserForm _tabbedBrowserForm = null;

        /// <summary>
        /// 
        /// </summary>
        private MessageCenterForm _msgCenterForm = null;
        #endregion

        #region Ctors.
        /// <summary>
        /// Ctor.
        /// </summary>
        public MobileNewSmthForm()
        {
            InitializeComponent();
            this._reloadSectionsTimer.Interval = 5 * 60 * 1000; // 5 minutes
            this._reloadSectionsTimer.Tick += new EventHandler(ReloadSectionsTimer_Tick);
            this._reloadSectionsTimer.Start();
            this._dicTopUrl.Add("hot", @"http://m.newsmth.net/hot");
            for (int i = 1; i < 10; i++)
            {
                this._dicTopUrl.Add(@"hot/" + i.ToString(), @"http://m.newsmth.net/hot/" + i.ToString());
            }

            LoginForm.LoginStatusChanged += new LoginStatusChangedHandler(LoginForm_LoginStatusChanged);
            
            //线程间操作无效: 从不是创建控件的线程访问它
            //Panel.CheckForIllegalCrossThreadCalls = false;

            //this._browserForm = new BrowserForm(this);
            //this._msgCenterForm = new MessageCenterForm(this);
            //MobileNewSmthForm._tabbedBrowserForm = new TabbedBrowserForm(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MobileNewSmthForm_Activated(object sender, EventArgs e)
        {
            TimeSpan ts = DateTime.Now - this._lastUpdateTime;
            if (ts.TotalSeconds > 3 * 60)
            {
                foreach (KeyValuePair<string, string> kp in this._dicTopUrl)
                {
                    FetchPage(kp.Value);
                }

#if (DEBUG)
                Nzl.Web.Util.CommonUtil.ShowMessage("Reload sections...");
#endif
                this._lastUpdateTime = this._lastUpdateTime.Add(ts);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MobileNewSmthForm_Shown(object sender, EventArgs e)
        {
            this._msgCenterForm.Show();
            //(new Top10sForm()).Show();
            FetchPage(@"http://m.newsmth.net/hot");
        }
        #endregion

        #region Properties
        private static TabbedBrowserForm Browser
        {
            get
            {
                return _tabbedBrowserForm;
            }
        }
        #endregion

        #region Fetch page
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private void FetchPage(string url)
        {
            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            this.bwFetchPage.WorkerReportsProgress = true;
            this.bwFetchPage.DoWork += new System.ComponentModel.DoWorkEventHandler(bwFetchPage_DoWork);
            this.bwFetchPage.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwFetchPage_RunWorkerCompleted);
            this.bwFetchPage.RunWorkerAsync(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker bw = sender as BackgroundWorker;
                WebPage wp = WebPageFactory.CreateWebPage(e.Argument as string);
                if (wp != null && wp.IsGood)
                {
                    bool isLogIn = SmthUtil.GetLogInStatus(wp);
                    bw.ReportProgress(isLogIn ? 1 : 0);
                    string section = this.GetSectionTitle(wp);
                    if (string.IsNullOrEmpty(section) == false)
                    {
                        IList<Topic> topicList = Nzl.Web.Forms.MobileNewSmth.Utils.TopicFactory.GetTop10Topics(wp);
                        if (topicList != null)
                        {
                            Topic topic = new Topic();
                            topic.Title = section;
                            topicList.Add(topic);
                        }

                        e.Result = topicList;
                    }                    
                }
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

#if (DEBUG)
                Nzl.Web.Util.CommonUtil.ShowMessage(typeof(MobileNewSmthForm), exp.Message);
#endif
                e.Result = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("Canceled");
            }
            else
            {
                IList<Topic> topicList = e.Result as IList<Topic>;
                if (topicList != null)
                {
                    string title = topicList[topicList.Count - 1].Title;
                    topicList.RemoveAt(topicList.Count - 1);
                    CreateOrUpdateTabPage(topicList, title);
                }
            }
        }
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
            ShowFormOnCenterParent(this._loginForm);
            //this._loginForm.Enabled = true;
            //this._loginForm.StartPosition = FormStartPosition.CenterParent;
            //this._loginForm.ShowDialog(this);
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
                form.Location = new System.Drawing.Point(centerX - form.Size.Width/2, centerY - form.Size.Height/2);
                form.Show();
                form.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadTop_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, string> kp in this._dicTopUrl)
            {
                FetchPage(kp.Value);
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
            this.ShowFormOnCenterParent(this._msgCenterForm);
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
        private void TopControl_OnTopLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linklbl = sender as LinkLabel;
            if (linklbl != null)
            {
                MobileNewSmthForm.Browser.AddTopic(e.Link.LinkData.ToString(), linklbl.Text);
                MobileNewSmthForm.Browser.Show();
                //this.ShowFormOnCenterParent(this._tabbedBrowserForm);
                //this._browserForm.AddTopic(e.Link.LinkData.ToString(), linklbl.Text);
                //this.ShowFormOnCenterParent(this._browserForm);
                //this.Focus();
                //TopicForm form = this.GetRegisteredForm<TopicForm>(e.Link.LinkData.ToString());                
                //if (form != null)
                //{
                //    e.Link.Visited = true;
                //    form.TopicUrl = e.Link.LinkData.ToString();
                //    form.StartPosition = FormStartPosition.Manual;
                //    int centerX = this.Location.X + this.Size.Width / 2;
                //    int centerY = this.Location.Y + this.Size.Height / 2;
                //    form.Location = new System.Drawing.Point(centerX - form.Size.Width / 2, centerY - form.Size.Height / 2);
                //    form.Show();
                //    form.Focus();
                //}
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReloadSectionsTimer_Tick(object sender, EventArgs e)
        {
            if (this.IsDisposed == false && this.Focused == false)
            {
                foreach (KeyValuePair<string, string> kp in this._dicTopUrl)
                {
                    FetchPage(kp.Value);
                }

                this._lastUpdateTime = DateTime.Now;
            }
            else
            {
                this._reloadSectionsTimer.Stop();
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

        #region Get topic information
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        private string GetSectionTitle(WebPage wp)
        {
            try
            {
                return CommonUtil.GetMatch("<li class=\"f\">(?'Title'\\w+)</li>", wp.Html, 1).Replace("热门话题", "");
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private TabPage GetTabPage(string title)
        {
            foreach (TabPage tb in this.tabctrlPages.TabPages)
            {
                if (tb.Name == title)
                {
                    return tb;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldList"></param>
        /// <param name="newList"></param>
        /// <returns></returns>
        private bool IsUpdated(IList<Topic> oldList, IList<Topic> newList)
        {
            if (oldList != null && newList != null && oldList.Count == newList.Count)
            {
                for (int i = 0; i < newList.Count; i++)
                {
                    if (oldList[i].Uri.ToString() != newList[i].Uri.ToString())
                    {
                        return true;
                    }
                }

                return false;
            }

            return true;
        }    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicList"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        private void CreateOrUpdateTabPage(IList<Topic> topicList, string title)
        {
            if (string.IsNullOrEmpty(title) == false)
            {
                TabPage tp = GetTabPage(title);
                if (tp == null)
                {
                    tp = new TabPage(title);
                    tp.Name = title;
                    Panel newPanel = new Panel();
                    newPanel.Dock = DockStyle.Fill;
                    tp.Controls.Add(newPanel);
                    this.tabctrlPages.TabPages.Add(tp);
                }

                if (tp.Controls.Count > 0)
                {
                    Panel tabPanel = tp.Controls[0] as Panel;
                    if (IsUpdated(tabPanel.Tag as IList<Topic>, topicList))
                    {
                        foreach (Control ctrl in tabPanel.Controls)
                        {
                            TopControl tc = ctrl as TopControl;
                            if (tc != null)
                            {
                                tc.Dispose();
                            }
                        }

                        tabPanel.Controls.Clear();
                        tabPanel.Visible = false;
                        bool flag = false;
                        int accumulateHeight = 0;
                        foreach (Topic topic in topicList)
                        {
                            TopControl tc = new TopControl(topic);
                            tc.OnTopLinkClicked += new LinkLabelLinkClickedEventHandler(TopControl_OnTopLinkClicked);
                            tc.Width = tabctrlPages.Width - 9;
                            tc.Top = accumulateHeight;
                            tc.Left = 0;
                            if (flag)
                            {
                                tc.BackColor = System.Drawing.Color.White;
                            }

                            flag = !flag;
                            accumulateHeight += tc.Height;
                            tabPanel.Controls.Add(tc);
                        }

                        tabPanel.Visible = true;
                        tabPanel.Tag = topicList;
                        this.tabctrlPages.Height += accumulateHeight - tabPanel.Height;
                        this.Height += accumulateHeight - tabPanel.Height;
                    }
                }
            }
        }
        #endregion
    }
}
