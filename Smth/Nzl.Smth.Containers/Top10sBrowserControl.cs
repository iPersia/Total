namespace Nzl.Smth.Containers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Nzl.Smth.Datas;

    /// <summary>
    /// 
    /// </summary>
    public partial class Top10sBrowserControl : UserControl
    {
        #region Event
        /// <summary>
        /// 
        /// </summary>
        public static event LinkLabelLinkClickedEventHandler OnTopLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public static event LinkLabelLinkClickedEventHandler OnTopBoardLinkClicked;
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
        static Top10sBrowserControl()
        {
            SectionTopsControl.OnTopBoardLinkClicked += SectionTopsControl_OnTopBoardLinkClicked;
            SectionTopsControl.OnTopLinkClicked += SectionTopsControl_OnTopLinkClicked;
        }

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
                SectionTopsControl tbc = new SectionTopsControl("http://m.newsmth.net/hot");
                tbc.Name = "tbcHot";
                tbc.SetParent(tp);
                tbc.CreateControl();
                tp.Controls.Add(tbc);
                this.tcTop10s.TabPages.Add(tp);
                this.Size = new Size(tbc.Width + 8, tbc.Height + 26);
            }

            this._timerLoadingTops.Interval = Configurations.Top10sLoadingInterval;
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
            if (this._sectionIndex > 9)
            {
                this._timerLoadingTops.Stop();
                return;
            }

            TabPage tp = new TabPage();
            tp.Name = "tbcHot" + this._sectionIndex;
            SectionTopsControl tbc = new SectionTopsControl("http://m.newsmth.net/hot/" + this._sectionIndex);
            tbc.Name = "tbcHot" + this._sectionIndex++;
            tbc.SetParent(tp);
            tbc.CreateControl();
            tp.Controls.Add(tbc);
            this.tcTop10s.TabPages.Add(tp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SectionTopsControl_OnTopLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linklbl = sender as LinkLabel;
            if (linklbl != null && OnTopLinkClicked != null)
            {
                OnTopLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SectionTopsControl_OnTopBoardLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linklbl = sender as LinkLabel;
            if (linklbl != null && OnTopBoardLinkClicked != null)
            {
                OnTopBoardLinkClicked(sender, e);
            }
        }        
    }
}
