namespace Nzl.Web.Pub.MobileNewSmth
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;

    /// <summary>
    /// The new smth form.
    /// </summary>
    public partial class MobileNewSmthForm : Form
    {
        #region Variable
        /// <summary>
        /// 
        /// </summary>
        private bool _isLogIn = false;

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
        #endregion

        /// <summary>
        /// Ctor.
        /// </summary>
        public MobileNewSmthForm()
        {
            InitializeComponent();
            this._reloadSectionsTimer.Interval = 5 * 60 * 1000; // 5 minutes
            if (Settings.Contains(SettingItems.UpdateInterval.ToString()))
            {
                this._reloadSectionsTimer.Interval = Convert.ToInt32(Settings.Get(SettingItems.UpdateInterval.ToString())) * 60 * 1000;
            }

            this._reloadSectionsTimer.Tick += new EventHandler(ReloadSectionsTimer_Tick);
            this._reloadSectionsTimer.Start();
            this._dicTopUrl.Add("hot", @"http://m.newsmth.net/hot");
            for (int i = 1; i < 10; i++)
            {
                this._dicTopUrl.Add(@"hot/" + i.ToString(), @"http://m.newsmth.net/hot/" + i.ToString());
            }
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
            FetchPage(@"http://m.newsmth.net/hot");
        }

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
            this.bwFetchPage.ProgressChanged += new ProgressChangedEventHandler(bwFetchPage_ProgressChanged);
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
                    bool isLogIn = GetLogInStatus(wp);
                    bw.ReportProgress(isLogIn ? 1 : 0);
                    string section = this.GetSectionTitle(wp);
                    if (string.IsNullOrEmpty(section) == false)
                    {
                        IList<Topic> topicList = GetTop10Topics(wp.Html);
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
                Nzl.Web.Util.CommonUtil.ShowMessage(typeof(TopicForm), exp.Message);
#endif
                e.Result = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.btnLogIn.Enabled = true;
            this._isLogIn = e.ProgressPercentage > 0;
            SetButtonVisibleByLogInStatus(this._isLogIn);
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
                    //Log In.
                    if (paraList.Count == 3)
                    {
                        WebPage wp = WebPageFactory.CreateWebPage(paraList[0], paraList[1], paraList[2]);
                        e.Result = GetLogInStatus(wp).ToString();
                    }

                    //Log Out.
                    if (paraList.Count == 1)
                    {
                        WebPage wp = WebPageFactory.CreateWebPage(paraList[0]);
                        e.Result = GetLogInStatus(wp).ToString();
                    }
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
                SetButtonVisibleByLogInStatus(false);
                if (e.Result.ToString() == "True")
                {
                    SetButtonVisibleByLogInStatus(true);
                }

                FetchPage(this._dicTopUrl["hot"]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True - Loged In, False - Loged Out</returns>
        private bool GetLogInStatus(WebPage wp)
        {
            return !string.IsNullOrEmpty(CommonUtil.GetMatch(@"<a href=\W/user/logout\W accesskey=\W9\W>注销\([a-zA-z][a-zA-Z0-9]{1,11}\)</a>", wp.Html));
        }
        #endregion

        #region Event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            this.btnLogIn.Enabled = false;
            if (this.btnLogIn.Text == "Log In")
            {  
                string userName = Settings.Get(SettingItems.UserName.ToString());
                string password = SettingsForm.GetPassword(userName);
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    Common.MessageForm form = new Common.MessageForm("Attention", "Please go to 'Settings' to set the username & password!");
                    form.StartPosition = FormStartPosition.CenterParent;
                    form.ShowDialog(this);
                    this.btnLogIn.Enabled = true;
                }
                else
                {
                    WebPageFactory.RemoveCookie(@"http://m.newsmth.net");
                    LogIn(userName, password);
                }
            }
            else
            {
                WebPageFactory.RemoveCookie(@"http://m.newsmth.net");
                LogOut();
            }
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
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog(this);
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
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog(this);
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
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetting_Click(object sender, EventArgs e)
        {
            SettingsForm sForm = new SettingsForm();
            if (sForm != null)
            {
                sForm.StartPosition = FormStartPosition.CenterParent;
                sForm.ShowDialog(this);                
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
                TopicForm form = this.GetRegisteredForm<TopicForm>(e.Link.LinkData.ToString());
                if (form != null)
                {
                    e.Link.Visited = true;
                    form.TopicUrl = e.Link.LinkData.ToString();
                    form.StartPosition = FormStartPosition.CenterParent;
                    form.ShowDialog(this);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetButtonVisibleByLogInStatus(bool flag)
        {
            if (this.IsDisposed == false)
            {
                if (flag)
                {
                    this.btnLogIn.Text = "Log Out";
                    this.lblWelcome.Text = "Welcome, " + Settings.Get(SettingItems.UserName.ToString()) + "!";
                }
                else
                {
                    this.btnLogIn.Text = "Log In";
                    this.lblWelcome.Text = "Welcome!";
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
            TabPage tp = null;
            foreach (TabPage tb in this.tabctrlPages.TabPages)
            {
                if (tb.Name == title)
                {
                    tp = tb;
                    break;
                }
            }

            if (tp == null)
            {
                tp = new TabPage(title);
                tp.Name = title;
                Panel newPanel = new Panel();
                newPanel.Dock = DockStyle.Fill;
                tp.Controls.Add(newPanel);
                this.tabctrlPages.TabPages.Add(tp);
            }

            return tp;
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
                if (tp !=null && tp.Controls.Count > 0)
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
                            TopControl tc = new TopControl(topic.Index, topic.Title, topic.Uri, topic.Replies);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private IList<Topic> GetTop10Topics(WebPage page)
        {
            if (page != null && page.IsGood)
            {
                return this.GetTop10Topics(page.Html);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private IList<Topic> GetTop10Topics(string html)
        {
            MatchCollection mtCollection = CommonUtil.GetMatchCollection(@"(?'Index'\d{1,2})\|<a href=\W(?'Url'/article/[\w, %2E, %5F]+/\d+)\W>(?'Title'[^<]+)(\(<span\s+style=\Wcolor:red\W>(?'Replies'\d+)</span>\))?</a></li>", html);
            IList<Topic> topicList = new List<Topic>();
            foreach (Match mt in mtCollection)
            {
                if (mt.Success)
                {
                    Topic topic = new Topic();
                    topic.Index = System.Convert.ToInt32(mt.Groups["Index"].Value);
                    topic.Uri = @"http://m.newsmth.net" + mt.Groups["Url"].ToString();
                    topic.Title = CommonUtil.ReplaceSpecialChars(mt.Groups["Title"].ToString());
                    topic.Replies = mt.Groups["Replies"].Value.ToString() == "" ? 0 : System.Convert.ToInt32(mt.Groups["Replies"].Value);
                    topicList.Add(topic);
                }
            }

            return topicList;
        }
        #endregion
    }
}
