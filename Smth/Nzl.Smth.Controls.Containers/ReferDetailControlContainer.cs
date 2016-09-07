//#define DESIGNMODE
namespace Nzl.Smth.Controls.Containers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Nzl.Recycling;
    using Nzl.Smth.Datas;
    using Nzl.Smth.Controls.Base;
    using Nzl.Smth.Controls.Elements;
    using Nzl.Smth.Utils;
    using Nzl.Web.Page;
    /// <summary>
    /// 
    /// </summary>
#if (DESIGNMODE)
    public partial class ReferDetailControlContainer : UserControl
#else
    public partial class ReferDetailControlContainer : BaseControlContainer<ReferDetailControl, Refer>
#endif
    {
#if (DESIGNMODE)
#else
        #region events
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
        public event LinkLabelLinkClickedEventHandler OnUserClicked;
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
        public ReferDetailControlContainer()
        {
            InitializeComponent();
            this.Text = "Refer Detail";
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
        protected override string GetUrl(UrlInfo<ReferDetailControl, Refer> info)
        {
            return info.BaseUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        protected override IList<Refer> GetItems(WebPage wp)
        {
            return ReferFactory.CreateRefers(wp);
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
        protected override void InitializeControl(ReferDetailControl ctl, Refer item)
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
        protected override void RecylingControl(ReferDetailControl ctl)
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

        protected override void RecyclingItem(Refer data)
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
        protected override void WorkCompleted(UrlInfo<ReferDetailControl, Refer> info)
        {
            base.WorkCompleted(info);
            if (this._parentControl != null)
            {
                if (info.Status == PageStatus.Normal && info.Result != null && info.Result.Count > 0)
                {
                    this._parentControl.Text = (info.Result[0] as Refer).Subject;
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
            }
        }

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
            }
        }

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
            }
        }

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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferDetailControl_OnMailClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

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