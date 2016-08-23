namespace Nzl.Smth.Controls.Complexes
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Nzl.Smth.Configurations;
    using Nzl.Smth.Controls.Containers;

    /// <summary>
    /// 
    /// </summary>
    public partial class Top10sBrowserControl : UserControl
    {
        #region Event
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTopLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTopBoardLinkClicked;
        #endregion

        #region Variable
        /// <summary>
        /// 
        /// </summary>
        private Timer _timerLoadingTops = new Timer();

        /// <summary>
        /// 
        /// </summary>
        private int _sectionIndex = 1;
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public Top10sBrowserControl()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            {
                TabPage tp = new TabPage();
                tp.Name = "tpHot";
                TopContainer tbc = new TopContainer("http://m.newsmth.net/hot");
                tbc.Name = "tbcHot";
                tbc.SetParent(tp);
                tbc.CreateControl();
                tbc.OnTopBoardLinkClicked += SectionTopsControl_OnTopBoardLinkClicked;
                tbc.OnTopLinkClicked += SectionTopsControl_OnTopLinkClicked;
                tp.Controls.Add(tbc);
                this.tcTop10s.TabPages.Add(tp);
                this.Size = new Size(tbc.Width + 8, tbc.Height + 26);
            }

            this._timerLoadingTops.Interval = Configuration.Top10sLoadingInterval;
            this._timerLoadingTops.Tick += new EventHandler(_timerLoadingTops_Tick);
            this._timerLoadingTops.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timerLoadingTops_Tick(object sender, EventArgs e)
        {
            if (this._sectionIndex > Configuration.SectionCount)
            {
                this._timerLoadingTops.Stop();
                return;
            }

            TabPage tp = new TabPage();
            tp.Name = "tbcHot" + this._sectionIndex;
            TopContainer tbc = new TopContainer("http://m.newsmth.net/hot/" + this._sectionIndex);
            tbc.Name = "tbcHot" + this._sectionIndex++;
            tbc.SetParent(tp);
            tbc.CreateControl();
            tbc.OnTopBoardLinkClicked += SectionTopsControl_OnTopBoardLinkClicked;
            tbc.OnTopLinkClicked += SectionTopsControl_OnTopLinkClicked;
            tp.Controls.Add(tbc);
            this.tcTop10s.TabPages.Add(tp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionTopsControl_OnTopLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linklbl = sender as LinkLabel;
            if (linklbl != null && this.OnTopLinkClicked != null)
            {
                this.OnTopLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionTopsControl_OnTopBoardLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linklbl = sender as LinkLabel;
            if (linklbl != null && this.OnTopBoardLinkClicked != null)
            {
                this.OnTopBoardLinkClicked(sender, e);
            }
        }        
    }
}
