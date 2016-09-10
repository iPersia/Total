//#define DESIGNMODE
namespace Nzl.Smth.Controls.Containers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Nzl.Recycling;
    using Nzl.Smth.Configs;
    using Nzl.Smth.Controls.Base;
    using Nzl.Smth.Controls.Elements;
    using Nzl.Smth.Datas;
    using Nzl.Smth.Loaders;
    using Nzl.Smth.Utils;
    using Nzl.Web.Page;
    /// <summary>
    /// 
    /// </summary>
#if (DESIGNMODE)
    public partial class ReferDetailControlContainer : UserControl
#else
    public partial class PostControlContainer : BaseControlContainer<PostControl, Post>
#endif
    {
#if (DESIGNMODE)
#else
        #region events
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnExpandClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnSubjectExpandClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnEditClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnReplyClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTransferClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnBoardClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnDeleteClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnNewClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnMailClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnUserClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkClickedEventHandler OnContentLinkClicked;
        #endregion

        #region variable
        /// <summary>
        /// 
        /// </summary>
        private Control _parentControl = null;
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        public PostControlContainer()
        {
            InitializeComponent();
            this.Text = "Post";
            this.SetUrlInfo(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        public void SetParentControl(Control ctl)
        {
            this._parentControl = ctl;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public string Url
        {
            set
            {
                this.SetBaseUrl(value);
            }
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override string GetUrl(UrlInfo<PostControl, Post> info)
        {
            return info.BaseUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        protected override IList<Post> GetItems(WebPage wp)
        {
            return PostFactory.CreatePosts(wp);
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
        /// <param name="ctl"></param>
        /// <param name="item"></param>
        protected override void InitializeControl(PostControl ctl, Post item)
        {
            base.InitializeControl(ctl, item);
            if (ctl != null && item != null)
            {
                ctl.Name = "rdc";
                ctl.OnBoardClicked += ReferDetailControl_OnBoardClicked;
                ctl.OnDeleteClicked += ReferDetailControl_OnDeleteClicked;
                ctl.OnEditClicked += ReferDetailControl_OnEditClicked;
                ctl.OnExpandClicked += ReferDetailControl_OnExpandClicked;
                ctl.OnHostClicked += ReferDetailControl_OnHostClicked;
                ctl.OnLastClicked += ReferDetailControl_OnLastClicked;
                ctl.OnMailClicked += ReferDetailControl_OnMailClicked;
                ctl.OnNewClicked += ReferDetailControl_OnNewClicked;
                ctl.OnNextClicked += ReferDetailControl_OnNextClicked;
                ctl.OnReplyClicked += ReferDetailControl_OnReplyClicked;
                ctl.OnSourceClicked += ReferDetailControl_OnSourceClicked;
                ctl.OnSubjectExpandClicked += ReferDetailControl_OnSubjectExpandClicked;
                ctl.OnSubjectLastClicked += ReferDetailControl_OnSubjectLastClicked;
                ctl.OnSubjectNextClicked += ReferDetailControl_OnSubjectNextClicked;
                ctl.OnTextBoxLinkClicked += ReferDetailControl_OnTextBoxLinkClicked;
                ctl.OnTextBoxMouseWheel += this.Container_MouseWheel;
                ctl.OnTransferClicked += ReferDetailControl_OnTransferClicked;
                ctl.OnUserClicked += ReferDetailControl_OnUserClicked;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ctl"></param>
        protected override void RecylingControl(PostControl ctl)
        {
            base.RecylingControl(ctl);
            if (ctl != null)
            {
                ctl.OnBoardClicked -= ReferDetailControl_OnBoardClicked;
                ctl.OnExpandClicked -= ReferDetailControl_OnExpandClicked;
                ctl.OnHostClicked -= ReferDetailControl_OnHostClicked;
                ctl.OnLastClicked -= ReferDetailControl_OnLastClicked;
                ctl.OnMailClicked -= ReferDetailControl_OnMailClicked;
                ctl.OnNewClicked -= ReferDetailControl_OnNewClicked;
                ctl.OnNextClicked -= ReferDetailControl_OnNextClicked;
                ctl.OnReplyClicked -= ReferDetailControl_OnReplyClicked;
                ctl.OnSourceClicked -= ReferDetailControl_OnSourceClicked;
                ctl.OnSubjectExpandClicked -= ReferDetailControl_OnSubjectExpandClicked;
                ctl.OnSubjectLastClicked -= ReferDetailControl_OnSubjectLastClicked;
                ctl.OnSubjectNextClicked -= ReferDetailControl_OnSubjectNextClicked;
                ctl.OnTextBoxLinkClicked -= ReferDetailControl_OnTextBoxLinkClicked;
                ctl.OnTextBoxMouseWheel -= this.Container_MouseWheel;
                ctl.OnTransferClicked -= ReferDetailControl_OnTransferClicked;
                ctl.OnUserClicked -= ReferDetailControl_OnUserClicked;
            }
        }

        protected override void RecyclingItem(Post data)
        {
            base.RecyclingItem(data);
            if (data != null && data.Data != null)
            {
                RecycledQueues.AddRecycled<Thread>(data.Data);
                data.Data = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected override void WorkCompleted(UrlInfo<PostControl, Post> info)
        {
            base.WorkCompleted(info);
            if (this._parentControl != null)
            {
                if (info.Status == PageStatus.Normal && info.Result != null && info.Result.Count > 0)
                {
                    this._parentControl.Text = (info.Result[0] as Post).Subject;
                }
            }
        }
        #endregion

        #region eventhandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnEditClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnEditClicked != null)
            {
                this.OnEditClicked(sender, e);
                if (e.Link.Tag != null)
                {
                    string postString = e.Link.Tag.ToString();
                    if (string.IsNullOrEmpty(postString) == false)
                    {
                        PostLoader pl = new PostLoader(e.Link.LinkData.ToString(), postString);
                        pl.Succeeded += ThreadEdit_Succeeded;
                        pl.Failed += ThreadEdit_Failed;
                        pl.Start();
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
        private void ThreadEdit_Succeeded(object sender, EventArgs e)
        {
            this.ShowInformation("Editting the thread is completed, the page will be refreshed!");
            this.SetUrlInfo(false);
            this.FetchLastPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadEdit_Failed(object sender, EventArgs e)
        {
            this.ShowInformation("Editting the thread failed!");
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnDeleteClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnDeleteClicked != null)
            {
                this.OnDeleteClicked(sender, e);
                if (e.Link.Tag != null && e.Link.Tag.ToString() == "Yes")
                {
                    PostLoader pl = new PostLoader(e.Link.LinkData.ToString());
                    pl.Succeeded += ThreadDelete_Succeeded;
                    pl.Failed += ThreadDelete_Failed;
                    pl.Start();
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
        private void ThreadDelete_Succeeded(object sender, EventArgs e)
        {
            this.ShowInformation("Deleting the thread is completed, the page will be refreshed!");
            this.SetUrlInfo(false);
            this.FetchPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadDelete_Failed(object sender, EventArgs e)
        {
            this.ShowInformation("Deleting the thread failed!");
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnUserClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnUserClicked != null)
            {
                this.OnUserClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnTransferClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnTransferClicked != null)
            {
                this.OnTransferClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnTextBoxLinkClicked(object sender, LinkClickedEventArgs e)
        {
            if (this.OnContentLinkClicked != null)
            {
                this.OnContentLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnSubjectNextClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SetUrlInfo(false);
            this.SetBaseUrl(e.Link.LinkData.ToString());
            this.FetchPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnSubjectLastClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SetUrlInfo(false);
            this.SetBaseUrl(e.Link.LinkData.ToString());
            this.FetchPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnSubjectExpandClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnSubjectExpandClicked != null)
            {
                this.OnSubjectExpandClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnSourceClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SetUrlInfo(false);
            this.SetBaseUrl(e.Link.LinkData.ToString());
            this.FetchPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnReplyClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnReplyClicked != null)
            {
                this.OnReplyClicked(sender, e);
                if (e.Link.Tag != null)
                {
                    string postString = e.Link.Tag.ToString();
                    if (string.IsNullOrEmpty(postString) == false)
                    {
                        PostLoader pl = new PostLoader(e.Link.LinkData.ToString(), postString);
                        pl.Succeeded += ThreadReply_Succeeded;
                        pl.Failed += ThreadReply_Failed;
                        pl.Start();
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
        private void ThreadReply_Succeeded(object sender, EventArgs e)
        {
            this.ShowInformation("Replying the thread is completed, the page will be refreshed!");
            this.SetUrlInfo(false);
            this.FetchLastPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadReply_Failed(object sender, EventArgs e)
        {
            this.ShowInformation("Replying the thread failed!");
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnNextClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SetUrlInfo(false);
            this.SetBaseUrl(e.Link.LinkData.ToString());
            this.FetchPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnNewClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnNewClicked != null)
            {
                this.OnNewClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnMailClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnMailClicked != null)
            {
                this.OnMailClicked(sender, e);
                if (e.Link.Tag != null)
                {
                    string postString = e.Link.Tag as string;
                    if (string.IsNullOrEmpty(postString) == false)
                    {
                        PostLoader pl = new PostLoader(Configuration.SendMailUrl, postString);
                        pl.Succeeded += ThreadMail_Succeeded;
                        pl.Failed += ThreadMail_Failed;
                        pl.Start();
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
        private void ThreadMail_Succeeded(object sender, EventArgs e)
        {
            this.ShowInformation("Sending mail is completed!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadMail_Failed(object sender, EventArgs e)
        {
            this.ShowInformation("Sending mail failed!");
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnLastClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SetUrlInfo(false);
            this.SetBaseUrl(e.Link.LinkData.ToString());
            this.FetchPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnHostClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SetUrlInfo(false);
            this.SetBaseUrl(e.Link.LinkData.ToString());
            this.FetchPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnExpandClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnExpandClicked != null)
            {
                this.OnExpandClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnBoardClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnBoardClicked != null)
            {
                this.OnBoardClicked(sender, e);
            }
        }
        #endregion
#endif
    }
}