namespace Nzl.Smth.Controls.Base
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.ComponentModel;
    using Nzl.Messaging;
    using Nzl.Recycling;
    using Nzl.Smth;
    using Nzl.Smth.Configs;
    using Nzl.Smth.Datas;
    using Nzl.Smth.Loaders;
    using Nzl.Smth.Logger;
    using Nzl.Utils;
    using Nzl.Web.Page;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public class BaseControlContainer<TBaseControl, TBaseData> : UserControl, IRecycled
        where TBaseControl : BaseControl<TBaseData>, new()
        where TBaseData : BaseData
    {
        #region event
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MessageEventArgs> OnWorkerFailed;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MessageEventArgs> OnWorkerCancelled;
        #endregion

        #region variable
        /// <summary>
        /// 
        /// </summary>
        private UrlInfo<TBaseControl, TBaseData> _urlInfo = new UrlInfo<TBaseControl, TBaseData>();

        /// <summary>
        /// 
        /// </summary>
        private System.ComponentModel.BackgroundWorker bwFetchPage;

        /// <summary>
        /// 
        /// </summary>
        private Label _lblMessage = new Label();
        #endregion

        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        public BaseControlContainer()
            : base()
        {
            this.IsWorking = false;
            this.IsRecycled = false;
            this.Status = RecycledStatus.Using;
            Configuration.OnLocationMarginChanged += Configuration_OnLocationMarginChanged;
        }
        #endregion

        #region properties
        /// <summary>
        /// 
        /// </summary>
        protected bool IsResponingMouseWheel
        {
            set
            {
                this.GetPanelContainer().MouseWheel -= Container_MouseWheel;
                if (value)
                {
                    this.GetPanelContainer().MouseWheel += Container_MouseWheel;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected bool IsWorking
        {
            get;
            private set;
        }
        #endregion

        #region public
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        public void RefreshingOnSizeChanged(bool flag)
        {
            this.SizeChanged -= BaseContainer_SizeChanged;
            if (flag)
            {
                this.SizeChanged += BaseContainer_SizeChanged;
            }
        }
        #endregion

        #region IRecycled
        /// <summary>
        /// 
        /// </summary>
        public bool IsRecycled
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public RecycledStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Reusing()
        {
            this.Status = RecycledStatus.Using;
            if (this.IsRecycled)
            {
                this.SetUrlInfo(1, false);
                this.FetchPage();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Recycling()
        {
            if (this.IsWorking == false)
            {
                Panel baseControlContainer = this.GetPanel();
                if (baseControlContainer != null)
                {
                    foreach (Control ctl in baseControlContainer.Controls)
                    {
                        this.RecylingControl(ctl as TBaseControl);
                    }

                    baseControlContainer.Controls.Clear();
                    baseControlContainer.Height = 100;
                }
            }

            this.IsRecycled = true;
            this.SetUrlInfo(1, false);
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
            if (this.GetPanelContainer() == null || this.GetPanel() == null)
            {
                if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                {
                    return;
                }
                else
                {
                    throw new Exception("The panel or the panel container is null!");
                }
            }

            ///Set BorderStyle.
            this.BorderStyle = BorderStyle.None;
            this.GetPanelContainer().BorderStyle = BorderStyle.FixedSingle;
            this.GetPanel().BorderStyle = BorderStyle.FixedSingle;

            ///Set location.
            this.GetPanel().Location = new Point(Configuration.BaseControlContainerLocationMargin,
                                                 Configuration.BaseControlContainerLocationMargin);

            ///Set width.
            this.GetPanel().Width = this.GetPanelContainer().Width
                                  - Configuration.BaseControlContainerLocationMargin * 2
                                  - this.GetPanelContainerBoarderMargin();

            ///Add message label.
            this._lblMessage.BackColor = Color.Transparent;
            this._lblMessage.ForeColor = Color.OrangeRed;
            this._lblMessage.Hide();
            this.GetPanelContainer().Controls.Remove(this.GetPanel());
            this.GetPanelContainer().Controls.Add(this._lblMessage);
            this.GetPanelContainer().Controls.Add(this.GetPanel());

            ///Response to mouse wheeling.
            this.IsResponingMouseWheel = true;            

            ///Fetch first page.
            this.SetUrlInfo(false);
            this.FetchPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Panel baseControlContainer = this.GetPanel();
            if (baseControlContainer != null)
            {
                baseControlContainer.Width = this.Width
                                           - Configuration.BaseControlContainerLocationMargin * 2
                                           - this.GetPanelContainerBoarderMargin();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseContainer_SizeChanged(object sender, EventArgs e)
        {
            Panel baseControlContainer = this.GetPanel();
            if (baseControlContainer != null)
            {
#if (DEBUG)
                System.Diagnostics.Debug.WriteLine("BaseContainer - BaseContainer_SizeChanged - FetchPage");
#endif
                baseControlContainer.Width = this.Width
                                           - Configuration.BaseControlContainerLocationMargin * 2
                                           - this.GetPanelContainerBoarderMargin();
                this.SetUrlInfo(false);
                this.FetchPage();
            }
        }
        #endregion

        #region virtual
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUrl"></param>
        protected virtual Panel GetPanel()
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUrl"></param>
        protected virtual Panel GetPanelContainer()
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual string GetCurrentUrl()
        {
            return this._urlInfo.BaseUrl + "?p=" + this._urlInfo.Index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual string GetUrl(UrlInfo<TBaseControl, TBaseData> info)
        {
            return info.BaseUrl + "?p=" + info.Index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected virtual void DoWork(UrlInfo<TBaseControl, TBaseData> info)
        {
            this.UpdatePageInfo(info.WebPage, info);
            info.Result = this.GetItems(info.WebPage);
            info.Controls = this.PrepareControls(info.Result);
#if (X)
            System.Diagnostics.Debug.WriteLine("BaseContainer - DoWork - " + info.BaseUrl);
            System.Diagnostics.Debug.WriteLine("BaseContainer - DoWork - this.IsHandleCreated - " + this.IsHandleCreated);
#endif
            if (info.Controls != null && info.Controls.Count > 0)
            {
                if (this.IsHandleCreated)
                {
                    if (this.InvokeRequired)
                    {
                        System.Threading.Thread.Sleep(0);
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            this.InitializeContainer(info.IsAppend);
                        }));
                        System.Threading.Thread.Sleep(0);
                    }
                }

                this.UpdateView(info.Controls);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctls"></param>
        protected void UpdateView(IList<TBaseControl> ctls)
        {
            foreach (TBaseControl ctl in ctls)
            {
                if (this.IsHandleCreated)
                {
                    if (this.InvokeRequired)
                    {
                        System.Threading.Thread.Sleep(0);
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            this.AddControl(ctl);
                        }));
                        System.Threading.Thread.Sleep(0);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void InitializeContainer(bool isAppend)
        {
            Panel baseControlContainer = GetPanel();
            if (baseControlContainer != null)
            {
                lock (baseControlContainer)
                {
                    if (isAppend == false)
                    {
                        foreach (Control ctl in baseControlContainer.Controls)
                        {
                            TBaseControl bc = ctl as TBaseControl;
                            if (bc != null)
                            {
                                this.RecylingControl(bc);
                            }
                        }

                        baseControlContainer.Location = new Point(Configuration.BaseControlContainerLocationMargin, Configuration.BaseControlContainerLocationMargin);
                        baseControlContainer.Height = Configuration.BaseControlLocationMargin + this.GetPanelContainerBoarderMargin();
                        baseControlContainer.Controls.Clear();
                        GC.Collect();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual TBaseControl GetRecycledControl()
        {
            return RecycledQueues.GetRecycled<TBaseControl>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bc"></param>
        protected virtual void RecylingControl(TBaseControl ctl)
        {
            if (ctl != null)
            {
                RecycledQueues.AddRecycled<TBaseControl>(ctl);
                this.RecyclingItem(ctl.Data);
                ctl.Data = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        protected virtual void RecyclingItem(TBaseData data)
        {
            if (data != null)
            {
                RecycledQueues.AddRecycled<TBaseData>(data);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        protected virtual bool CheckAddingControl(TBaseControl ctl)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        protected virtual void AddControl(TBaseControl ctl)
        {
            Panel baseControlContainer = GetPanel();
            if (baseControlContainer != null)
            {
                lock (baseControlContainer)
                {
                    if (CheckAddingControl(ctl))
                    {
                        int accumulateHeight = baseControlContainer.Height - Configuration.BaseControlLocationMargin - this.GetPanelContainerBoarderMargin();
                        ctl.Top = accumulateHeight + Configuration.BaseControlLocationMargin;
                        ctl.Left = Configuration.BaseControlLocationMargin;
                        this.SetControl(ctl, baseControlContainer.Controls.Count % 2 == 0);
                        baseControlContainer.Controls.Add(ctl);
                        accumulateHeight += ctl.Height + Configuration.BaseControlLocationMargin;
                        baseControlContainer.Height = accumulateHeight + Configuration.BaseControlLocationMargin + this.GetPanelContainerBoarderMargin();
#if (X)
                        System.Diagnostics.Debug.WriteLine("BaseContainer - AddControl - container.Controls.Count is " + container.Controls.Count);
#endif

#if (X)
                        Nzl.Web.Util.CommonUtil.ShowMessage(this, "\tBaseContainer - AddControl\n" +
                                                                  "\t\t" + _urlInfo.BaseUrl + " - accumulateHeight:" + accumulateHeight + "\n" +
                                                                  "\t\t" + _urlInfo.BaseUrl + " - ctl name:" + ctl.Name);
#endif
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        protected virtual IList<TBaseData> GetItems(WebPage wp)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        protected virtual bool CheckPage(WebPage wp, UrlInfo<TBaseControl, TBaseData> info)
        {
            if (wp == null)
            {
                info.Status = PageStatus.TimeOut;
                return false;
            }

            if (wp != null && wp.IsGood)
            {
                info.Status = PageStatus.Normal;
                if (wp.Html.Contains("<div class=\"sp hl f\">您无权阅读此版面</div>"))
                {
                    info.Status = PageStatus.AccessRestricted;
                    return false;
                }

                if (wp.Html.Contains("<div class=\"sp hl f\">指定的版面不存在</div>") ||
                    wp.Html.Contains("<div class=\"sp hl f\">指定的文章不存在或链接错误</div>"))
                {
                    info.Status = PageStatus.NotFound;
                    return false;
                }

                return true;
            }

            info.Status = PageStatus.UnKnown;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected virtual void WorkCompleted(UrlInfo<TBaseControl, TBaseData> info)
        {
            if (info != null)
            {
                if (info.Status == PageStatus.Normal)
                {
                    LogStatus.Instance.UpdateStatus(info.WebPage);
#if (DEBUG)
                    this.ShowInformation("Work is completed!");
#endif
                }
                else
                {
                    ShowInformation(MiscUtil.GetEnumDescription(info.Status));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        protected virtual void SetControlEnabled(bool flag)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    this.SetControlEnabled(flag);
                }));
            }
            else
            {
                this.GetPanelContainer().Enabled = flag;                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thread"></param>
        /// <returns></returns>
        protected virtual TBaseControl CreateBaseControl()
        {
            return new TBaseControl();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="item"></param>
        protected virtual void InitializeControl(TBaseControl ctl, TBaseData data)
        {
            if (ctl != null)
            {
                Panel baseControlContainer = this.GetPanel();
                if (baseControlContainer != null)
                {
                    int width = baseControlContainer.Width
                              - Configuration.BaseControlLocationMargin * 2
                              - this.GetControlContainerBoarderMargin();
                    ctl.SetWidth(width);
                }

                if (data != null)
                {
                    ctl.Name = "ctl" + data.ID;
                }

                ctl.Initialize(data);
#if (DEBUG)
                ctl.BorderStyle = BorderStyle.FixedSingle;
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        protected virtual void SetControl(TBaseControl ctl, bool oeFlag)
        {
            if (oeFlag)
            {
                ctl.BackColor = Color.White;
            }
            else
            {
                ctl.BackColor = System.Drawing.SystemColors.Control;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proc"></param>
        protected virtual void UpdateProgress(int proc)
        {
            ///Noting to do.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Container_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                Panel panelContainer = this.GetPanelContainer();
                Panel baseControlContainer = this.GetPanel();
                int panelContainerHeight = panelContainer.Height; //panel容器高度
#if (X)
                System.Diagnostics.Debug.WriteLine("---------------------***TopicForm_MouseWheel***---------------------");
                System.Diagnostics.Debug.WriteLine("Sender is:" + sender.GetType().ToString());
                System.Diagnostics.Debug.WriteLine("panelContainerHeight:" + panelContainerHeight);
#endif
                if (this.GetPanel().Height > panelContainerHeight)
                {
#if (X)
                    System.Diagnostics.Debug.WriteLine("oldYPos:" + this.panel.Location.Y);
                    System.Diagnostics.Debug.WriteLine("Delta  :" + e.Delta);
#endif
                    int boardMargin = this.GetPanelContainerBoarderMargin();
                    int maxHeight = Configuration.BaseControlContainerLocationMargin;
                    int minHeight = panelContainerHeight
                                  - baseControlContainer.Height
                                  - Configuration.BaseControlContainerLocationMargin
                                  - boardMargin;
                    int newYPos = baseControlContainer.Location.Y + e.Delta;
                    newYPos = newYPos > maxHeight ? maxHeight : newYPos;
                    newYPos = newYPos < minHeight ? minHeight : newYPos;
#if (X)
                    System.Diagnostics.Debug.WriteLine("newYPos:" + newYPos);
#endif
                    baseControlContainer.Location = new Point(Configuration.BaseControlContainerLocationMargin, newYPos);
                    if (newYPos == minHeight)
                    {
                        this.FetchPageOnMouseWheel();
#if (X)
                        System.Diagnostics.Debug.WriteLine("FetchNextPage - newYPos is " + newYPos);
#endif
                    }
                }
            }
            catch (Exception exp)
            {
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\n" + exp.StackTrace);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void FetchPageOnMouseWheel()
        {
            this.SetUrlInfo(true);
            this.FetchNextPage();
            //if (this._settingBrowserType == BrowserType.FirstReply)
            //{
            //    this.FetchNextPage();
            //}
            //else if (this._settingAutoUpdating == false)
            //{
            //    this.FetchPrevPage();
            //}
        }
        #endregion

        #region protected
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUrl"></param>
        protected void SetBaseUrl(string baseUrl)
        {
            this._urlInfo.BaseUrl = baseUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void SetUrlInfo(int index, bool isAppend)
        {
            this._urlInfo.Index = index;
            this._urlInfo.IsAppend = isAppend;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void SetUrlInfo(bool isAppend)
        {
            this._urlInfo.IsAppend = isAppend;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        private IList<TBaseData> PrepareInfos(UrlInfo<TBaseControl, TBaseData> info)
        {
            return info.Result as IList<TBaseData>;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private void UpdatePageInfo(WebPage wp, UrlInfo<TBaseControl, TBaseData> info)
        {
            if (wp != null)
            {
                MatchCollection mtCollection = CommonUtil.GetMatchCollection(@"<a class=\Wplant\W>(?'Current'\d+)/(?'Total'\d+)</a>", wp.Html);
                if (mtCollection.Count == 2)
                {
                    this._urlInfo.Index = System.Convert.ToInt32(mtCollection[0].Groups[1].Value);
                    this._urlInfo.Total = System.Convert.ToInt32(mtCollection[0].Groups[2].Value);

                    info.Index = System.Convert.ToInt32(mtCollection[0].Groups[1].Value);
                    info.Total = System.Convert.ToInt32(mtCollection[0].Groups[2].Value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listThread"></param>
        private IList<TBaseControl> PrepareControls(IList<TBaseData> list)
        {
            IList<TBaseControl> listThreacControl = new List<TBaseControl>();
            foreach (TBaseData data in list)
            {
                TBaseControl ctl = this.GetControl(data);
                if (ctl != null)
                {
                    listThreacControl.Add(ctl);
                }
            }
#if (DEBUG)
            System.Diagnostics.Debug.WriteLine("BaseContainer - PrepareControls - TBaseControl count is " + list.Count);
#endif
            return listThreacControl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected TBaseControl GetControl(TBaseData data)
        {
            if (this.IsHandleCreated)
            {
                if (this.InvokeRequired)
                {
                    TBaseControl ctl = this.GetRecycledControl();
                    if (ctl == null || ctl.IsDisposed)
                    {
#if (DEBUG)
                        System.Diagnostics.Debug.WriteLine("BaseContainer - GetControl - GetRecycledControl failed, type is " + typeof(TBaseControl).ToString());
#endif
                        System.Threading.Thread.Sleep(0);
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            ctl = this.CreateBaseControl();
                        }));
                        System.Threading.Thread.Sleep(0);
                    }

                    if (ctl != null && data != null)
                    {
                        if (this.IsHandleCreated)
                        {
                            if (this.InvokeRequired)
                            {
                                System.Threading.Thread.Sleep(0);
                                this.Invoke(new MethodInvoker(delegate ()
                                {
                                    this.InitializeControl(ctl, data);
                                }));
                                System.Threading.Thread.Sleep(0);
                            }
                        }

                        return ctl;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void ShowInformationInPanel(string text)
        {
            Panel baseControlContainer = this.GetPanel();
            if (baseControlContainer != null)
            {
                Label lbl = new Label();
                lbl.AutoSize = true;
                lbl.Text = text;
                baseControlContainer.Controls.Add(lbl);
                lbl.Top = 30;
                lbl.Left = (baseControlContainer.Width - lbl.Width) / 2;
                baseControlContainer.Height = 60 + lbl.Height;
                baseControlContainer.Top = Configuration.BaseControlContainerLocationMargin;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        protected void ShowInformation(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    this.ShowInformation(text);
                }));
            }
            else
            {
                Panel panelContainer = this.GetPanelContainer();
                if (panelContainer != null)
                {
                    this._lblMessage.Hide();
                    this._lblMessage.AutoSize = true;
                    this._lblMessage.Text = text;
                    this._lblMessage.Top = panelContainer.Height * 8 / 10;
                    this._lblMessage.Left = (panelContainer.Width - this._lblMessage.Width) / 2;
                    this._lblMessage.Show();
                }

                ///Clear timer
                Timer showTextTimer = new Timer();
                showTextTimer.Tick += ShowTextTimer_Tick;
                showTextTimer.Interval += 1000;
                showTextTimer.Start();                
            }
        }

        private void ShowTextTimer_Tick(object sender, EventArgs e)
        {
            Timer timer = sender as Timer;
            if (timer != null)
            {
                timer.Stop();
                this._lblMessage.Hide();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected int GetPanelContainerBoarderMargin()
        {
            return this.GetPanelContainer().BorderStyle == BorderStyle.FixedSingle ? 2 : 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected int GetControlContainerBoarderMargin()
        {
            return this.GetPanel().BorderStyle == BorderStyle.FixedSingle ? 2 : 0;
        }
        #endregion

        #region FetchPage
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
                UrlInfo<TBaseControl, TBaseData> urlInfo = e.Argument as UrlInfo<TBaseControl, TBaseData>;
                if (CheckPage(urlInfo.WebPage, urlInfo))
                {
                    e.Result = urlInfo;
                    DoWorkBase(e);
                }

                MessageQueue.Enqueue(MessageFactory.CreateMessage(this.Text == null ? this.GetType().ToString() : this.Text,
                                                                    Nzl.Utils.MiscUtil.GetEnumDescription(urlInfo.Status) + " The page is '" + this.GetUrl(urlInfo) + "'!"));
                e.Result = urlInfo;
            }
            catch (Exception exp)
            {
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\n" + exp.StackTrace);
                }

                (e.Argument as UrlInfo<TBaseControl, TBaseData>).Status = PageStatus.UnKnown;
                MessageQueue.Enqueue(MessageFactory.CreateMessage(exp));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state">State is DoWorkEventArgs!</param>
        protected void DoWorkBase(object state)
        {
            DoWorkEventArgs e = state as DoWorkEventArgs;
            try
            {
                UrlInfo<TBaseControl, TBaseData> urlInfo = e.Result as UrlInfo<TBaseControl, TBaseData>;
                if (urlInfo != null)
                {
                    DoWork(urlInfo);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception exp)
            {
                e.Cancel = true;
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(this.ToString() + "\t" + exp.Message + "\n" + exp.StackTrace);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    WorkFailedBase(e);
                }
                else if (e.Cancelled)
                {
                    WorkCancelledBase(e);
                }
                else
                {
                    WorkCompletedBase(e);
                }

                this.IsWorking = false;
                this.SetControlEnabled(true);
            }
            catch (Exception exp)
            {
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\n" + exp.StackTrace);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state">State is RunWorkerCompletedEventArgs!</param>
        protected void WorkCompletedBase(RunWorkerCompletedEventArgs e)
        {
            UrlInfo<TBaseControl, TBaseData> urlInfo = e.Result as UrlInfo<TBaseControl, TBaseData>;
            if (urlInfo != null)
            {
                WorkCompleted(urlInfo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state">State is RunWorkerCompletedEventArgs!</param>
        protected void WorkCancelledBase(RunWorkerCompletedEventArgs e)
        {
            this.WorkCancelled(e.Error != null ? e.Error.Message : "Work is cancelled!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state">State is RunWorkerCompletedEventArgs!</param>
        protected void WorkFailedBase(RunWorkerCompletedEventArgs e)
        {
            this.WorkCancelled(e.Error != null ? e.Error.Message : "Work is failed!");
        }

        /// <summary>
        /// 
        /// </summary>
        protected bool FetchPage()
        {
            UrlInfo<TBaseControl, TBaseData> info = new UrlInfo<TBaseControl, TBaseData>(this._urlInfo);
            return FetchPage(info);
        }

        /// <summary>
        /// 
        /// </summary>
        protected bool FetchPrevPage()
        {
            UrlInfo<TBaseControl, TBaseData> info = new UrlInfo<TBaseControl, TBaseData>(this._urlInfo);
            info.Index = this._urlInfo.Index - 1;
            return FetchPage(info);
        }

        /// <summary>
        /// 
        /// </summary>
        protected bool FetchNextPage()
        {
            UrlInfo<TBaseControl, TBaseData> info = new UrlInfo<TBaseControl, TBaseData>(this._urlInfo);
            info.Index = this._urlInfo.Index + 1;
            return FetchPage(info);
        }

        /// <summary>
        /// 
        /// </summary>
        protected bool FetchLastPage()
        {
            UrlInfo<TBaseControl, TBaseData> info = new UrlInfo<TBaseControl, TBaseData>(this._urlInfo);
            info.Index = this._urlInfo.Total;
            return FetchPage(info);
        }

        /// <summary>
        /// 
        /// </summary>
        protected bool FetchPage(UrlInfo<TBaseControl, TBaseData> urlInfo)
        {
            lock (this)
            {
                if (urlInfo.Index > 0 &&
                    urlInfo.Index <= urlInfo.Total &&
                    string.IsNullOrEmpty(urlInfo.BaseUrl) == false &&
                    this.IsWorking == false &&
                    this.Status == RecycledStatus.Using)
                {
                    this.IsWorking = true;
                    PageLoader pl = new PageLoader(this.GetUrl(urlInfo));
                    pl.Tag = urlInfo;
                    pl.PageLoaded += new EventHandler(PageLoader_PageLoaded);
                    pl.PageFailed += new EventHandler(PageLoader_PageFailed);
                    PageDispatcher.Instance.Add(pl);
#if (DEBUG)
                    Nzl.Web.Util.CommonUtil.ShowMessage(this, "BaseContainer - FetchPage(UrlInfo's index is equal to " + urlInfo.Index + ")!");
#endif

#if (DEBUG)
                    this.ShowInformation("Starting getting page!");
#endif
                    SetControlEnabled(false);
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageLoader_PageLoaded(object sender, EventArgs e)
        {
            PageLoader pl = sender as PageLoader;
            if (pl != null)
            {
                WebPage wp = pl.GetResult() as WebPage;
                if (wp != null && wp.IsGood)
                {
                    UrlInfo<TBaseControl, TBaseData> info = pl.Tag as UrlInfo<TBaseControl, TBaseData>;
                    info.WebPage = wp;
                    if (this.IsHandleCreated)
                    {
                        if (this.InvokeRequired)
                        {
                            System.Threading.Thread.Sleep(0);
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                this.PageLoaded(info);
                            }));
                            System.Threading.Thread.Sleep(0);
                        }
                    }
                }
                else
                {
                    this.ShowInformation("The page getted is bad!");
                    this.IsWorking = false;
                    SetControlEnabled(true);
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
            lock (this)
            {
                this.IsWorking = false;
                SetControlEnabled(true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        private void PageLoaded(object pageInfor)
        {
            UrlInfo<TBaseControl, TBaseData> info = pageInfor as UrlInfo<TBaseControl, TBaseData>;
            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            this.bwFetchPage.DoWork += new System.ComponentModel.DoWorkEventHandler(bwFetchPage_DoWork);
            this.bwFetchPage.ProgressChanged += BwFetchPage_ProgressChanged;
            this.bwFetchPage.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bwFetchPage_RunWorkerCompleted);
            this.bwFetchPage.WorkerReportsProgress = true;
            this.bwFetchPage.RunWorkerAsync(info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BwFetchPage_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateProgress(e.ProgressPercentage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        private void WorkFailed(string msg)
        {
            if (this.OnWorkerFailed != null)
            {
                this.OnWorkerFailed(this, new MessageEventArgs(msg));
                ShowInformation("Loading failed!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        private void WorkCancelled(string msg)
        {
            if (this.OnWorkerCancelled != null)
            {
                this.OnWorkerCancelled(this, new MessageEventArgs(msg));
                ShowInformation("Loading cancelled!");
            }
        }
        #endregion

        #region eventhandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Configuration_OnLocationMarginChanged(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            this.GetPanel().Width = this.GetPanelContainer().Width
                                  - Configuration.BaseControlContainerLocationMargin * 2
                                  - this.GetPanelContainerBoarderMargin();
            this.FetchPage();
        }
        #endregion
    }
}