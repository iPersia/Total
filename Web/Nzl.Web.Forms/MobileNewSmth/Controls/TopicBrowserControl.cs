namespace Nzl.Web.Forms.MobileNewSmth.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;    
    using Nzl.Web.Forms.MobileNewSmth.Datas;
    using Nzl.Web.Forms.MobileNewSmth.Forms;
    using Nzl.Web.Forms.MobileNewSmth.Interfaces;
    using Nzl.Web.Forms.MobileNewSmth.Utils;

    /// <summary>
    /// The topic form.
    /// </summary>
    public partial class TopicBrowserControl : BaseControl, IContainsThread
    {
        #region Variable
        /// <summary>
        /// 
        /// </summary>
        private string _topic;

        /// <summary>
        /// The url.
        /// </summary>
        private string _topicUrl;

        /// <summary>
        /// 
        /// </summary>
        private string _subject;

        /// <summary>
        /// 
        /// </summary>
        private string _postUrl;

        /// <summary>
        /// 
        /// </summary>
        private string _targetUserID;

        /// <summary>
        /// 
        /// </summary>
        private int _margin = 4;

        /// <summary>
        /// 
        /// </summary>
        private BrowserType _browserType = BrowserType.FirstReply;

        /// <summary>
        /// 
        /// </summary>
        private bool _autoUpdating = false;

        /// <summary>
        /// 
        /// </summary>
        private int _updatingInterval = 60;

        /// <summary>
        /// 
        /// </summary>
        private Timer _updatingTimer = new Timer();

        /// <summary>
        /// 
        /// </summary>
        private Thread _hostThread = null;

        /// <summary>
        /// 
        /// </summary>
        private UrlInfo _resultUrlInfo = new UrlInfo();

        /// <summary>
        /// 
        /// </summary>
        private SortedList<string, Thread> _sortedlistThread = new SortedList<string, Thread>();

        /// <summary>
        /// 
        /// </summary>
        private Control _parentControl = null;

        /// <summary>
        /// 
        /// </summary>
        private LoadingForm _loadingForm = new LoadingForm();
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public string TopicUrl
        {
            get
            {
                return this._topicUrl;
            }

            set
            {
                this._topicUrl = value;
                this.SetBaseUrl(this._topicUrl);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TargetUserID
        {
            get
            {
                return this._targetUserID;
            }

            set
            {
                this._targetUserID = value;
            }
        }
        #endregion

        #region Ctors.
        /// <summary>
        /// Ctor.
        /// </summary>
        public TopicBrowserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public TopicBrowserControl(string url, string subject)
            : base()
        {
            this.SetBaseUrl(url);
            this._subject = subject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        public void SetParent(Control ctl)
        {
            this._parentControl = ctl;
        }
        #endregion

        #region overrides
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.panel.MouseWheel -= new MouseEventHandler(TopicForm_MouseWheel);
            this.panel.MouseWheel += new MouseEventHandler(TopicForm_MouseWheel);
            this.SetUrlInfo(1, false);
            this.SetBtnEnabled(!this.FetchPage());
            System.Diagnostics.Debug.WriteLine(this.GetType().ToString() + this.GetHashCode() + " - " + this.Name + " - OnLoad");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Topic ";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        protected override void DoWork(UrlInfo info)
        {
            base.DoWork(info);
            IList<Thread> threads = ThreadFactory.CreateThreads(info.WebPage, this);
            IList<BaseItem> list = new List<BaseItem>();
            foreach (Thread thread in threads)
            {
                list.Add(thread);
            }

            info.Result = list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        protected override void WorkerCompleted(UrlInfo info)
        {
            base.WorkerCompleted(info);
            if (info.Status == PageStatus.Normal)
            {
                this.SaveThreads(info.Result as IList<BaseItem>);
                this.UpdateText(info.WebPage);
                this.GetTopicInfo(info.WebPage);
                this.lblPage1.Text = info.Index.ToString().PadLeft(3, '0') + "/" + info.Total.ToString().PadLeft(3, '0');
                this.lblPage2.Text = this.lblPage1.Text;
                this.btnReply.Visible = LoginForm.IsLogin;
                this._resultUrlInfo = info;
            }

            SetBtnEnabled(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Panel GetContainer()
        {
            return this.panel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override Control CreateControl(BaseItem item)
        {
            return this.CreateThreadControl(item as Thread);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thread"></param>
        /// <returns></returns>
        private ThreadControl CreateThreadControl(Thread thread)
        {
            int width = this.panel.Width - 4;
            ThreadControl tc = new ThreadControl(width);
            tc.Thread = thread;
            tc.OnIDLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnIDClicked);
            tc.OnQueryTypeLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnQueryTypeLinkClicked);
            tc.OnEditLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnEditLinkClicked);
            tc.OnDeleteLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnDeleteLinkClicked);
            tc.OnReplyLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnReplyLinkClicked);
            tc.OnMailLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnMailLinkClicked);
            tc.OnTransferLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnTransferLinkClicked);
            tc.OnTextBoxMouseWheel += new MouseEventHandler(TopicForm_MouseWheel);
            return tc;
        }
        #endregion

        #region Interface.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public Thread GetSavedThread(string tid)
        {
            if (this._sortedlistThread.ContainsKey(tid))
            {
                return this._sortedlistThread[tid];
            }

            return null;
        }
        #endregion

        #region Get information
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        private void GetTopicInfo(object state)
        {
            WebPage wp = state as WebPage;
            if (wp == null) return;
            string html = wp.Html;
            if (string.IsNullOrEmpty(html)) return;

            this._subject = this._topic;//CommonUtil.GetMatch(@"<input type=\Whidden\W name=\Wsubject\W value=\W(?'subject'Re[0-9A-Z,%,~,-]+)\W\s/>", html, 1);
            this._postUrl = CommonUtil.GetMatch(@"<form action=\W(?'post'/article/[\w, %2E, %5F]+/post/\d+)\W method=\Wpost\W>", html, 1);
            if (string.IsNullOrEmpty(this._postUrl) == false)
            {
                this._postUrl.Replace("%2E", ".");
                this._postUrl.Replace("%5F", "_");
                this._postUrl = @"http://m.newsmth.net" + this._postUrl;
            }
        }
        
        #endregion

         #region Event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopicForm_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                int panelContainerHeight = this.splitContainer2.Panel1.Height; //panel容器高度
#if (DEBUG)
                System.Diagnostics.Debug.WriteLine("---------------------***TopicForm_MouseWheel***---------------------");
                System.Diagnostics.Debug.WriteLine("Sender is:" + sender.GetType().ToString());
                System.Diagnostics.Debug.WriteLine("panelContainerHeight:" + panelContainerHeight);
#endif
                if (this.panel.Height > panelContainerHeight)
                {
#if (DEBUG)
                    System.Diagnostics.Debug.WriteLine("oldYPos:" + this.panel.Location.Y);
                    System.Diagnostics.Debug.WriteLine("Delta  :" + e.Delta);
#endif
                    int newYPos = this.panel.Location.Y + e.Delta;
                    newYPos = newYPos > this._margin ? this._margin : newYPos;
                    newYPos = newYPos < panelContainerHeight - this.panel.Height - this._margin
                         ? panelContainerHeight - this.panel.Height - this._margin : newYPos;
#if (DEBUG)
                    System.Diagnostics.Debug.WriteLine("newYPos:" + newYPos);
#endif
                    this.panel.Location = new Point(this.panel.Location.X, newYPos);
                    if (newYPos == panelContainerHeight - this.panel.Height - this._margin)
                    {
                        this.SetUrlInfo(true);
                        if (this._browserType == BrowserType.FirstReply)
                        {
                            this.SetBtnEnabled(!this.FetchNextPage());
                        }
                        //else
                        //{
                        //    this.SetBtnEnabled(!this.FetchPrevPage());
                        //}
#if (DEBUG)
                        System.Diagnostics.Debug.WriteLine("FetchNextPage - newYPos is " + newYPos);
#endif
                    }
                }
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_Click(object sender, EventArgs e)
        {            
            this.SetUrlInfo(false);
            if (this._browserType == BrowserType.FirstReply)
            {
                this.SetUrlInfo(1, false);
                this.SetBtnEnabled(!this.FetchPage());
            }
            else
            {
                this.SetBtnEnabled(!this.FetchLastPage());
            }   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            if (this._browserType == BrowserType.LastReply)
            {
                this.SetBtnEnabled(!this.FetchNextPage());
            }
            else
            {
                this.SetBtnEnabled(!this.FetchPrevPage());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            if (this._browserType == BrowserType.FirstReply)
            {
                this.SetBtnEnabled(!this.FetchNextPage());
            }
            else
            {
                this.SetBtnEnabled(!this.FetchPrevPage());
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            if (this._browserType == BrowserType.LastReply)
            {
                this.SetUrlInfo(1, false);
                this.SetBtnEnabled(!this.FetchPage());
            }
            else
            {
                this.SetBtnEnabled(!this.FetchLastPage());
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSettings_Click(object sender, EventArgs e)
        {
            BrowserType srcBrowseType = this._browserType;
            TopicBrowserSettingsForm form = new TopicBrowserSettingsForm();
            form.AutoUpdating = this._autoUpdating;
            form.BrowserType = this._browserType;
            form.UpdatingInterval = this._updatingInterval;
            form.StartPosition = FormStartPosition.CenterParent;
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this._autoUpdating = form.AutoUpdating;
                this._browserType = form.BrowserType;
                this._updatingInterval = form.UpdatingInterval;
            }

            this._updatingTimer.Stop();
            if (this._browserType == BrowserType.LastReply && this._autoUpdating)
            {
                this._updatingTimer.Interval = this._updatingInterval * 1000;
                this._updatingTimer.Tick -= new EventHandler(_updatingTimer_Tick);
                this._updatingTimer.Tick += new EventHandler(_updatingTimer_Tick);
                this._updatingTimer.Start();
            }
            else
            {
                this._updatingTimer.Stop();
                this.SetBtnEnabled(!this.FetchPage());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _updatingTimer_Tick(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            this.SetBtnEnabled(!this.FetchLastPage());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                int pageIndex = Int32.MaxValue;
                if (btn.Name == "btnGo1")
                {
                    if (string.IsNullOrEmpty(this.txtGoTo1.Text) == false)
                    {
                        pageIndex = System.Convert.ToInt32(this.txtGoTo1.Text);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.txtGoTo2.Text) == false)
                    {
                        pageIndex = System.Convert.ToInt32(this.txtGoTo2.Text);
                    }
                }


                if (this._browserType == BrowserType.FirstReply)
                {
                    this.SetUrlInfo(pageIndex, false);
                }
                else
                {
                    this.SetUrlInfo(this._resultUrlInfo.Total - pageIndex + 1, false);
                }

                this.SetBtnEnabled(!this.FetchPage());
                this.txtGoTo1.Text = this.txtGoTo2.Text = "";
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

#if (DEBUG)
                CommonUtil.ShowMessage(typeof(TopicBrowserControl), exp.Message);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReply_Click(object sender, EventArgs e)
        {
            NewThreadForm threadForm = new NewThreadForm("回复 - " + this.Text, this._postUrl, "Re: " + this._subject);
            threadForm.StartPosition = FormStartPosition.CenterParent;
            if (DialogResult.OK == threadForm.ShowDialog(this))
            {
                this.SetUrlInfo(false);
                this.FetchLastPage();
            }
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnIDClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                UserForm userForm = new UserForm(e.Link.LinkData.ToString());
                userForm.StartPosition = FormStartPosition.CenterParent;
                userForm.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnQueryTypeLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                string userID = Nzl.Web.Util.CommonUtil.GetMatch(@"au=(?'ID'[a-zA-z][a-zA-Z0-9]{1,11})", e.Link.LinkData.ToString(), 1);
                TopicForm topicForm = new TopicForm(Nzl.Web.Util.CommonUtil.GetUrlBase(e.Link.LinkData.ToString()), userID);
                topicForm.StartPosition = FormStartPosition.CenterParent;
                topicForm.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnReplyLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                Thread thread = linkLabel.Tag as Thread;
                if (thread != null)
                {
                    NewThreadForm newThreadForm = new NewThreadForm(this._topic, thread.ReplyUrl, "Re: " + this._topic, SmthUtil.GetReplyContent(thread), true);
                    newThreadForm.StartPosition = FormStartPosition.CenterParent;
                    if (DialogResult.OK == newThreadForm.ShowDialog(this))
                    {
                        this.SetUrlInfo(false);
                        this.FetchLastPage();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnMailLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                Thread thread = linkLabel.Tag as Thread;
                if (thread != null)
                {
                    NewMailForm newMailForm = new NewMailForm(thread.ID, "Re: " + this._topic, SmthUtil.GetReplyContent(thread));
                    newMailForm.StartPosition = FormStartPosition.CenterParent;
                    newMailForm.ShowDialog(this);
                }
            }
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnTransferLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnEditLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                Thread thread = linkLabel.Tag as Thread;
                if (thread != null)
                {
                    Regex regex = new Regex(@"\s*FROM\s[\d, \., \*]+");
                    string content = regex.Replace(thread.Tag.ToString(), "");
                    content = CommonUtil.ReplaceSpecialChars(content);
                    content = SmthUtil.TrimUrls(content);
                    NewThreadForm newThreadForm = new NewThreadForm(this._topic, thread.EditUrl, "Re: " + this._topic, content, false);
                    newThreadForm.StartPosition = FormStartPosition.CenterParent;
                    if (DialogResult.OK == newThreadForm.ShowDialog(this))
                    {
                        this.SetUrlInfo(false);
                        this.FetchLastPage();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnDeleteLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                Thread thread = linkLabel.Tag as Thread;
                if (thread != null)
                {
                    Nzl.Web.Forms.Common.MessageForm confirmForm = new Nzl.Web.Forms.Common.MessageForm("提示", "确认删除此信息？");
                    confirmForm.StartPosition = FormStartPosition.CenterParent;
                    DialogResult dlgResult = confirmForm.ShowDialog(this);
                    if (dlgResult == DialogResult.OK)
                    {
                        WebPage page = WebPageFactory.CreateWebPage(thread.DeleteUrl);
                        string result = CommonUtil.GetMatch(@"<div id=\Wm_main\W><div class=\Wsp hl f\W>(?'Result'\w+)</div>", page.Html, "Result");
                        if (result != null && result.Contains("成功"))
                        {
                            this.FetchPage();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenInBrowser_Click(object sender, EventArgs e)
        {
            CommonUtil.OpenUrl(this.GetCurrentUrl());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string GetCurrentUrl()
        {
            return base.GetCurrentUrl() + (string.IsNullOrEmpty(this._targetUserID) ? "" : "&au=" + this._targetUserID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string GetUrl(UrlInfo info)
        {
            return base.GetUrl(info) + (string.IsNullOrEmpty(this._targetUserID) ? "" : "&au=" + this._targetUserID);
        }
        #endregion

        #region Updating
        /// <summary>
        /// 
        /// </summary>
        private void UpdateText(object state)
        {
            WebPage wp = state as WebPage;
            if (wp != null)
            {
                this._topic = SmthUtil.GetTopic(wp);
                this.Text = this._topic + " - " + SmthUtil.GetBoard(wp);
                if (string.IsNullOrEmpty(this._targetUserID) == false)
                {
                    this.Text = "只看" + this._targetUserID + " - " + this.Text;
                }

                if (this._parentControl != null)
                {
                    this._parentControl.Text = this.Text;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        private void SetBtnEnabled(bool flag)
        {
            this.panel.Enabled = flag;

            this.btnFirst1.Enabled = flag;
            this.btnGo1.Enabled = flag;
            this.btnLast1.Enabled = flag;
            this.btnNext1.Enabled = flag;
            this.btnPrev1.Enabled = flag;
            
            this.btnFirst2.Enabled = flag;
            this.btnGo2.Enabled = flag;
            this.btnLast2.Enabled = flag;
            this.btnNext2.Enabled = flag;
            this.btnPrev2.Enabled = flag;

            this.txtGoTo1.Enabled = flag;
            this.txtGoTo2.Enabled = flag;
            
            this.btnReply.Enabled = flag;
            this.btnSettings.Enabled = flag;
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="threadList"></param>
        private void SaveThreads(IList<BaseItem> items)
        {
            foreach (BaseItem item in items)
            {
                Thread thread = item as Thread;
                if (thread != null)
                {
                    if (this._sortedlistThread.ContainsKey(thread.ID))
                    {
                        this._sortedlistThread[thread.ID] = thread;
                    }
                    else
                    {
                        this._sortedlistThread.Add(thread.ID, thread);
                    }
                }
            }
        }              
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listThreadControl"></param>
        private void UpdateView(IList<ThreadControl> listThreadControl, bool isAppend)
        {
            lock (this.panel)
            {
                /////Show the loading form.
                //this._loadingForm.StartPosition = FormStartPosition.CenterParent;                
                //this._loadingForm.Show(this);
         
                if (this._browserType == BrowserType.FirstReply)
                {
                    bool flag = false;
                    int accumulateHeight = 0;
                    if (isAppend == false)
                    {
                        this.panel.Controls.Clear();
                    }
                    else
                    {
                        accumulateHeight = this.panel.Height - 3;
                    }

                    foreach (ThreadControl tc in listThreadControl)
                    {
                        tc.Top = accumulateHeight + 1;
                        tc.Left = 1;
                        if (flag)
                        {
                            tc.BackColor = Color.White;
                        }

                        flag = !flag;
                        this.panel.Controls.Add(tc);
                        accumulateHeight += tc.Height + 1;
                    }

                    System.Diagnostics.Debug.WriteLine("accumulateHeight:" + accumulateHeight);
                    this.panel.Height = accumulateHeight + 3;
                }

                if (this._browserType == BrowserType.LastReply)
                {
                    //Add the host thread.
                    listThreadControl.Add(this.GetSavedControl(this._hostThread) as ThreadControl);

                    //Check whether need updating.
                    {
                        if (this.panel.Controls.Count == listThreadControl.Count)
                        {
                            bool isUpdated = false;
                            foreach (ThreadControl tcNew in listThreadControl)
                            {
                                if (this.panel.Controls.ContainsKey(tcNew.Name))
                                {
                                    ThreadControl tc = this.panel.Controls[tcNew.Name] as ThreadControl;
                                    Thread thread = tc.Tag as Thread;
                                    Thread threadNew = tcNew.Tag as Thread;
                                    if (threadNew.ID == thread.ID)
                                    {
                                        if (thread.Content == threadNew.Content)
                                        {
                                            continue;
                                        }
                                    }
                                }

                                isUpdated = true;
                                break;
                            }

                            if (isUpdated == false)
                            {
                                return;
                            }
                        }
                    }

                    bool flag = false;
                    int accumulateHeight = 0;
                    this.panel.Controls.Clear();                    
                    for (int i = listThreadControl.Count - 1; i >= 0; i--)
                    {
                        ThreadControl tc = listThreadControl[i];
                        tc.Top = accumulateHeight + 1;
                        tc.Left = 1;
                        if (flag)
                        {
                            tc.BackColor = Color.White;
                        }

                        flag = !flag;
                        this.panel.Controls.Add(tc);
                        accumulateHeight += tc.Height + 1;
                    }

                    System.Diagnostics.Debug.WriteLine("accumulateHeight:" + accumulateHeight);
                    this.panel.Height = accumulateHeight + 3;
                }
                
                if (isAppend == false)
                {
                    this.panel.Location = new Point(this.panel.Location.X, this._margin);
                }

                /////Hide the loading form.
                //this._loadingForm.Hide();
            }
        }
        #endregion
    }
}