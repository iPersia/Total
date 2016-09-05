//#define DESIGNMODE
namespace Nzl.Smth.Controls.Containers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Nzl.Recycling;
    using Nzl.Smth.Common;
    using Nzl.Smth.Configurations;
    using Nzl.Smth.Controls.Base;
    using Nzl.Smth.Controls.Elements;
    using Nzl.Smth.Datas;
    using Nzl.Smth.Logger;
    using Nzl.Smth.Utils;
    using Nzl.Web.Util;
    using Nzl.Web.Page;
    
    /// <summary>
    /// The topic form.
    /// </summary>
#if (DESIGNMODE)
    public partial class ThreadControlContainer : UserControl
#else
    public partial class ThreadControlContainer : BaseControlContainer<ThreadControl, Thread>
#endif
    {
#if (DESIGNMODE)
#else
        #region events.
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnThreadUserLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnThreadQueryTypeLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnThreadReplyLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTopicReplyLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<TopicSettingEventArgs> OnTopicSettingsClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnBoardLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnThreadMailLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnThreadTransferLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnThreadEditLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnThreadDeleteLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkClickedEventHandler OnThreadContentLinkClicked;
        #endregion

        #region variable
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
        private TopicSettingEventArgs _Settings = null;

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
        private UrlInfo<ThreadControl, Thread> _resultUrlInfo = new UrlInfo<ThreadControl, Thread>();

        /// <summary>
        /// 
        /// </summary>
        private Control _parentControl = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public string Url
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
        public ThreadControlContainer()
        {
            InitializeComponent();
            this._updatingTimer.Tick += _updatingTimer_Tick;
            LogStatus.Instance.OnLoginStatusChanged += LogStatus_OnLoginStatusChanged;
            this.Text = "Topic";
            {
                this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
                this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
                this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
                this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
                this.btnGo.Click += new System.EventHandler(this.btnGo_Click);

                this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
                this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
                this.btnOpenInBrowser.Click += new System.EventHandler(this.btnOpenInBrowser_Click);
                this.linklblReply.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblReply_LinkClicked);
                this.linklblBoard.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblBoard_LinkClicked);
            }

            ///Initialize settings.
            this._Settings = new TopicSettingEventArgs();
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
        /// <returns></returns>
        public override string ToString()
        {
            return "Topic ";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        protected override void DoWork(UrlInfo<ThreadControl, Thread> info)
        {
            base.DoWork(info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        protected override void WorkCompleted(UrlInfo<ThreadControl, Thread> info)
        {
            base.WorkCompleted(info);
            if (info.Status == PageStatus.Normal)
            {
                this.UpdateInfor(info.WebPage);
                this.lblPage.Text = info.Index.ToString().PadLeft(3, '0') + "/" + info.Total.ToString().PadLeft(3, '0');
                this._resultUrlInfo = info;


                ///Save the host thread.
                if (info.Index == 1 &&                     
                    info.Result.Count > 0 &&
                    info.Result[0].Floor == "楼主" &&
                    this._hostThread == null)
                {
                    this._hostThread = info.Result[0];
                }

                ///Fetch next page when the container is not full.
                if (this.GetPanel().Height < this.panelContainer.Height)
                {
                    if (this._Settings.BrowserType == BrowserType.FirstReply)
                    {
                        this.SetUrlInfo(true);
                        this.FetchNextPage();
                    }
                    else
                    {
                        this.SetUrlInfo(true);
                        this.FetchPrevPage();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Panel GetPanel()
        {
            return this.panel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Panel GetPanelContainer()
        {
            return this.panelContainer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        protected override IList<Thread> GetItems(WebPage wp)
        {
            IList<Thread> threads = ThreadFactory.CreateThreads(wp);
            if (this._Settings.BrowserType == BrowserType.LastReply)
            {
                IList<Thread> reversedThreads = new List<Thread>();
                if (threads != null)
                {
                    if (this._hostThread != null)
                    {
                        reversedThreads.Insert(0, this._hostThread);
                    }

                    for (int i = threads.Count - 1; i >= 0; i--)
                    {
                        reversedThreads.Add(threads[i]);
                    }

                    return reversedThreads;
                }
            }

            return threads;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="item"></param>
        protected override void InitializeControl(ThreadControl ctl, Thread item)
        {
            base.InitializeControl(ctl, item);
            if (ctl != null && item != null)
            {
                ctl.Name = "tc" + item.ID;
                ctl.OnUserLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnUserClicked);
                ctl.OnQueryTypeLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnQueryTypeLinkClicked);
                ctl.OnEditLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnEditLinkClicked);
                ctl.OnDeleteLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnDeleteLinkClicked);
                ctl.OnReplyLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnReplyLinkClicked);
                ctl.OnMailLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnMailLinkClicked);
                ctl.OnTransferLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnTransferLinkClicked);
                ctl.OnTextBoxLinkClicked += ThreadControl_OnTextBoxLinkClicked;
                ctl.OnTextBoxMouseWheel += this.Container_MouseWheel;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        protected override void RecylingControl(ThreadControl ctl)
        {
            base.RecylingControl(ctl);
            if (ctl != null)
            {
                ctl.OnUserLinkClicked -= new LinkLabelLinkClickedEventHandler(ThreadControl_OnUserClicked);
                ctl.OnQueryTypeLinkClicked -= new LinkLabelLinkClickedEventHandler(ThreadControl_OnQueryTypeLinkClicked);
                ctl.OnEditLinkClicked -= new LinkLabelLinkClickedEventHandler(ThreadControl_OnEditLinkClicked);
                ctl.OnDeleteLinkClicked -= new LinkLabelLinkClickedEventHandler(ThreadControl_OnDeleteLinkClicked);
                ctl.OnReplyLinkClicked -= new LinkLabelLinkClickedEventHandler(ThreadControl_OnReplyLinkClicked);
                ctl.OnMailLinkClicked -= new LinkLabelLinkClickedEventHandler(ThreadControl_OnMailLinkClicked);
                ctl.OnTransferLinkClicked -= new LinkLabelLinkClickedEventHandler(ThreadControl_OnTransferLinkClicked);
                ctl.OnTextBoxLinkClicked -= ThreadControl_OnTextBoxLinkClicked;
                ctl.OnTextBoxMouseWheel -= this.Container_MouseWheel;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        protected override void RecyclingItem(Thread thread)
        {
            if (this._hostThread != thread)
            {
                base.RecyclingItem(thread);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        protected override bool CheckAddingControl(ThreadControl ctl)
        {
            if (this._Settings.BrowserType == BrowserType.LastReply &&
                this._Settings.AutoUpdating == false &&
                ctl.Name != null)
            {
                return this.GetPanel().Controls.ContainsKey(ctl.Name) == false;
            }

            return base.CheckAddingControl(ctl);
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
        protected override string GetUrl(UrlInfo<ThreadControl, Thread> info)
        {
            return base.GetUrl(info) + (string.IsNullOrEmpty(this._targetUserID) ? "" : "&au=" + this._targetUserID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        protected override void SetControlEnabled(bool flag)
        {
            base.SetControlEnabled(flag);          
            this.btnRefresh.Enabled = true;
        }

        protected override void UpdateProgress(int proc)
        {
            this.btnFirst.Text = (proc * 11111).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void FetchPageOnMouseWheel()
        {
            this.SetUrlInfo(true);
            if (this._Settings.BrowserType == BrowserType.FirstReply)
            {
                this.FetchNextPage();
            }
            else if (this._Settings.AutoUpdating == false)
            {
                this.FetchPrevPage();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        public override void Recycling()
        {
            base.Recycling();

            ///Recyling the host thread.
            if (this._hostThread != null)
            {
                RecycledQueues.AddRecycled<Thread>(this._hostThread);
                this._hostThread = null;
            }

            ///Initialize the settings.
            this._Settings = new TopicSettingEventArgs();
            this._updatingTimer.Stop();
            this._topic = null;
            this._topicUrl = null;
            this._subject = null;
            this._postUrl = null;
            this._targetUserID = null;
            this._parentControl = null;
        }
        #endregion

        #region Get information       
        #endregion

        #region Event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogStatus_OnLoginStatusChanged(object sender, LogStatusEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate () {
                    this.linklblReply.Visible = e.IsLogin && string.IsNullOrEmpty(this._postUrl) == false;
                }));
            }
            else
            {
                this.linklblReply.Visible = e.IsLogin && string.IsNullOrEmpty(this._postUrl) == false;
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
            this.FetchLastPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_Click(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            if (this._Settings.BrowserType == BrowserType.FirstReply)
            {
                this.SetUrlInfo(1, false);
                this.FetchPage();
            }
            else
            {
                this.FetchLastPage();
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
            if (this._Settings.BrowserType == BrowserType.LastReply)
            {
                this.FetchNextPage();
            }
            else
            {
                this.FetchPrevPage();
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
            if (this._Settings.BrowserType == BrowserType.FirstReply)
            {
                this.FetchNextPage();
            }
            else
            {
                this.FetchPrevPage();
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
            if (this._Settings.BrowserType == BrowserType.LastReply)
            {
                this.SetUrlInfo(1, false);
                this.FetchPage();
            }
            else
            {
                this.FetchLastPage();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (this.OnTopicSettingsClicked != null)
            {
                TopicSettingEventArgs tsEventArgs = new TopicSettingEventArgs();
                tsEventArgs.AutoUpdating = this._Settings.AutoUpdating;
                tsEventArgs.BrowserType = this._Settings.BrowserType;
                tsEventArgs.UpdatingInterval = this._Settings.UpdatingInterval;
                this.OnTopicSettingsClicked(sender, tsEventArgs);
                if (tsEventArgs.Tag != null && tsEventArgs.Tag.ToString() == "Updated")
                {
                    this._Settings.AutoUpdating = tsEventArgs.AutoUpdating;
                    this._Settings.BrowserType = tsEventArgs.BrowserType;
                    this._Settings.UpdatingInterval = tsEventArgs.UpdatingInterval;
                    ApplyTopicSetting();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ApplyTopicSetting()
        {
            if (this._Settings.BrowserType == BrowserType.FirstReply)
            {
                this._updatingTimer.Stop();
                this.SetUrlInfo(1, false);
                this.FetchPage();
            }

            if (this._Settings.BrowserType == BrowserType.LastReply)
            {
                this._updatingTimer.Stop();
                if (this._Settings.AutoUpdating)
                {
                    this._updatingTimer.Interval = this._Settings.UpdatingInterval * 1000;
                    this._updatingTimer.Start();
                }

                this.SetUrlInfo(false);
                this.FetchLastPage();
            }
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
                if (string.IsNullOrEmpty(this.txtGoTo.Text) == false)
                {
                    pageIndex = System.Convert.ToInt32(this.txtGoTo.Text);
                }

                if (this._Settings.BrowserType == BrowserType.FirstReply)
                {
                    this.SetUrlInfo(pageIndex, false);
                }
                else
                {
                    this.SetUrlInfo(this._resultUrlInfo.Total - pageIndex + 1, false);
                }

                this.FetchPage();
            }
            catch (Exception exp)
            {
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\n" + exp.StackTrace);
                }

#if (DEBUG)
                CommonUtil.ShowMessage(typeof(ThreadControlContainer), exp.Message);
#endif
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            this.FetchPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblReply_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnTopicReplyLinkClicked != null)
            {
                this.OnTopicReplyLinkClicked(sender, e);
                if (e.Link.Tag != null)
                {
                    string postString = e.Link.Tag.ToString();
                    e.Link.Tag = null;
                    if (string.IsNullOrEmpty(postString) == false)
                    {
                        PageLoader pl = new PageLoader(this._postUrl, postString);
                        pl.PageLoaded += ThreadReply_PageLoaded;
                        pl.PageFailed += ThreadReply_PageFailed;
                        PageDispatcher.Instance.Add(pl);
                    }
                }

                e.Link.Tag = null;
            }
        }

        #region ThreadReply - PageLoaded & PageFailed
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadReply_PageLoaded(object sender, EventArgs e)
        {
            PageLoader pl = sender as PageLoader;
            if (pl != null)
            {
                string html = pl.GetResult() as string;
                string result = CommonUtil.GetMatch(@"<div id=\Wm_main\W><div class=\Wsp hl f\W>(?'Result'\w+)</div>", html, "Result");
                if (result != null && result.Contains("成功"))
                {
                    this.SetUrlInfo(false);
                    this.FetchLastPage();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadReply_PageFailed(object sender, EventArgs e)
        {
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblBoard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnBoardLinkClicked != null)
            {
                this.OnBoardLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnUserClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnThreadUserLinkClicked != null)
            {
                this.OnThreadUserLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnQueryTypeLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnThreadQueryTypeLinkClicked != null)
            {
                this.OnThreadQueryTypeLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnReplyLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnThreadReplyLinkClicked != null)
            {
                this.OnThreadReplyLinkClicked(sender, e);
                if (e.Link.Tag != null)
                {
                    string postString = e.Link.Tag.ToString();                    
                    if (string.IsNullOrEmpty(postString) == false)
                    {
                        PageLoader pl = new PageLoader(e.Link.LinkData.ToString(), postString);
                        pl.PageLoaded += ThreadReply_PageLoaded;
                        pl.PageFailed += ThreadReply_PageFailed;
                        PageDispatcher.Instance.Add(pl);
                    }
                }

                e.Link.Tag = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnMailLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnThreadMailLinkClicked != null)
            {
                this.OnThreadMailLinkClicked(sender, e);
                if (e.Link.Tag != null)
                {
                    string postString = e.Link.Tag as string;
                    if (string.IsNullOrEmpty(postString) == false)
                    {
                        PageLoader pl = new PageLoader(Configuration.SendMailUrl, postString);
                        pl.PageLoaded += ThreadMail_PageLoaded;
                        pl.PageFailed += ThreadMail_PageFailed;
                        PageDispatcher.Instance.Add(pl);
                    }                    
                }

                e.Link.Tag = null;
            }
        }

        #region ThreadMail - PageLoaded & PageFailed
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadMail_PageLoaded(object sender, EventArgs e)
        {
            PageLoader pl = sender as PageLoader;
            if (pl != null)
            {
                string html = pl.GetResult() as string;
                string result = CommonUtil.GetMatch(@"<div id=\Wm_main\W><div class=\Wsp hl f\W>(?'Result'\w+)</div>", html, "Result");
                if (result != null && result.Contains("成功"))
                {
                    //this.SetUrlInfo(false);
                    //this.FetchLastPage();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadMail_PageFailed(object sender, EventArgs e)
        {
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnTransferLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnThreadTransferLinkClicked != null)
            {
                this.OnThreadTransferLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnEditLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnThreadEditLinkClicked != null)
            {
                this.OnThreadEditLinkClicked(sender, e);
                if (e.Link.Tag != null)
                {
                    string postString = e.Link.Tag.ToString();                    
                    if (string.IsNullOrEmpty(postString) == false)
                    {
                        PageLoader pl = new PageLoader(e.Link.LinkData.ToString(), postString);
                        pl.PageLoaded += ThreadEdit_PageLoaded;
                        pl.PageFailed += ThreadEdit_PageFailed;
                        PageDispatcher.Instance.Add(pl);
                    }
                }

                e.Link.Tag = null;
            }
        }

        #region ThreadEdit - PageLoaded & PageFailed
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadEdit_PageLoaded(object sender, EventArgs e)
        {
            PageLoader pl = sender as PageLoader;
            if (pl != null)
            {
                string html = pl.GetResult() as string;
                string result = CommonUtil.GetMatch(@"<div id=\Wm_main\W><div class=\Wsp hl f\W>(?'Result'\w+)</div>", html, "Result");
                if (result != null && result.Contains("成功"))
                {
                    this.ShowInformation("Edit completed!");
                   // this.SetUrlInfo(false);
                    //this.FetchLastPage();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadEdit_PageFailed(object sender, EventArgs e)
        {
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnDeleteLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnThreadDeleteLinkClicked != null)
            {
                this.OnThreadDeleteLinkClicked(sender, e);
                if (e.Link.Tag != null && e.Link.Tag.ToString() == "Yes")
                {                    
                    PageLoader pl = new PageLoader(e.Link.LinkData.ToString());
                    pl.PageLoaded += ThreadDelete_PageLoaded;
                    pl.PageFailed += THreadDelete_PageFailed;
                    PageDispatcher.Instance.Add(pl);
                }

                e.Link.Tag = null;
            }
        }

        #region ThreadDelete - PageLoaded & PageFailed
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadDelete_PageLoaded(object sender, EventArgs e)
        {
            PageLoader pl = sender as PageLoader;
            if (pl != null)
            {
                WebPage wp = pl.GetResult() as WebPage;
                if (wp != null && wp.IsGood)
                {
                    string result = CommonUtil.GetMatch(@"<div id=\Wm_main\W><div class=\Wsp hl f\W>(?'Result'\w+)</div>", wp.Html, "Result");
                    if (result != null && result.Contains("成功"))
                    {
                        this.SetUrlInfo(false);
                        this.FetchPage();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void THreadDelete_PageFailed(object sender, EventArgs e)
        {
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnTextBoxLinkClicked(object sender, LinkClickedEventArgs e)
        {
            if (this.OnThreadContentLinkClicked != null)
            {
                this.OnThreadContentLinkClicked(sender, e);
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
        #endregion

        #region Updating
        /// <summary>
        /// 
        /// </summary>
        private void UpdateInfor(WebPage wp)
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

            this._subject = this._topic;//CommonUtil.GetMatch(@"<input type=\Whidden\W name=\Wsubject\W value=\W(?'subject'Re[0-9A-Z,%,~,-]+)\W\s/>", html, 1);
            this._postUrl = CommonUtil.GetMatch(@"<form action=\W(?'post'/article/[\w, %2E, %5F, \.]+/post/\d+)\W method=\Wpost\W>", wp.Html, 1);
            this.linklblReply.Visible = false;
            if (string.IsNullOrEmpty(this._postUrl) == false)
            {
                this._postUrl.Replace("%2E", ".");
                this._postUrl.Replace("%5F", "_");
                this._postUrl = Configuration.BaseUrl + this._postUrl;

                this.linklblReply.Visible = true;
                this.linklblReply.Links.Clear();
                this.linklblReply.Links.Add(0, 5, this._postUrl);
            }

            string board = SmthUtil.GetBoard(wp);
            if (board != null)
            {
                string engBoardName = CommonUtil.GetMatch(@"\((?'Board'.+)\)", board, "Board");
                string chnBoardName = board.Replace("(" + engBoardName + ")", "");
                this.linklblBoard.Visible = true;
                this.linklblBoard.Text = chnBoardName;
                this.linklblBoard.Links.Clear();
                this.linklblBoard.Links.Add(0, board.Length, "http://m.newsmth.net/board/" + engBoardName);
            }

            this.linklblReply.Visible = LogStatus.Instance.IsLogin;
        }
        #endregion
#endif
    }
}
