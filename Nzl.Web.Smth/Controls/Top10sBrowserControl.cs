namespace Nzl.Web.Smth.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Nzl.Web.Smth.Controls;

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
                SectionTopsControl tbc = new SectionTopsControl("http://m.newsmth.net/hot");                
                tbc.SetParent(tp);
                tbc.OnTopLinkClicked += new LinkLabelLinkClickedEventHandler(tbc_OnTopLinkClicked);
                tp.Controls.Add(tbc);
                this.tcTop10s.TabPages.Add(tp);
                this.Size = new Size(tbc.Width + 8, tbc.Height + 26);
            }

            this._timerLoadingTops.Interval = 30 * 1000;
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

            SectionTopsControl tbc = new SectionTopsControl("http://m.newsmth.net/hot/" + this._sectionIndex++);
            TabPage tp = new TabPage();
            tbc.SetParent(tp);
            tbc.OnTopLinkClicked += new LinkLabelLinkClickedEventHandler(tbc_OnTopLinkClicked);
            tp.Controls.Add(tbc);
            this.tcTop10s.TabPages.Add(tp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbc_OnTopLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linklbl = sender as LinkLabel;
            if (linklbl != null && this.OnTopLinkClicked != null)
            {
                this.OnTopLinkClicked(sender, e);
            }
        }
    }
}
