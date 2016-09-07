namespace Nzl.Smth.Controls.Complexes
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Nzl.Smth;
    using Nzl.Smth.Configs;
    using Nzl.Smth.Controls.Containers;

    /// <summary>
    /// 
    /// </summary>
    public partial class ReferControl : UserControl
    {
        #region event
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnDeleteLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnReferLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnUserLinkClicked;
        #endregion

        #region variable        
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        public ReferControl()
        {
            InitializeComponent();
            this.SizeChanged += ReferControl_SizeChanged;
            ///At
            {
                TabPage tp = new TabPage();
                tp.Name = "tpAt";
                tp.Text = "@me";
                this.tcRefer.TabPages.Add(tp);

                AtControlContainer acc = new AtControlContainer();
                acc.Dock = DockStyle.Fill;
                acc.CreateControl();
                acc.SetParent(tp);
                acc.OnDeleteLinkClicked += ReplyControlContainer_OnDeleteLinkClicked;
                acc.OnReplyLinkClicked += ReplyControlContainer_OnReplyLinkClicked;
                acc.OnUserLinkClicked += ReplyControlContainer_OnUserLinkClicked;
                tp.Controls.Add(acc);
            }

            ///Refer
            {
                TabPage tp = new TabPage();
                tp.Name = "tpRefer";
                tp.Text = "Refer";
                this.tcRefer.TabPages.Add(tp);

                ReplyControlContainer rcc = new ReplyControlContainer();
                rcc.Dock = DockStyle.Fill;
                rcc.CreateControl();
                rcc.SetParent(tp);
                rcc.OnDeleteLinkClicked += ReplyControlContainer_OnDeleteLinkClicked;
                rcc.OnReplyLinkClicked += ReplyControlContainer_OnReplyLinkClicked;
                rcc.OnUserLinkClicked += ReplyControlContainer_OnUserLinkClicked;
                tp.Controls.Add(rcc);
            }
        }
        #endregion

        #region eventhandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReplyControlContainer_OnDeleteLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnDeleteLinkClicked != null)
            {
                this.OnDeleteLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReplyControlContainer_OnReplyLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnReferLinkClicked != null)
            {
                this.OnReferLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReplyControlContainer_OnUserLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnUserLinkClicked != null)
            {
                this.OnUserLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferControl_SizeChanged(object sender, EventArgs e)
        {
            this.btnReadAll.Left = (this.panelMenu.Width - this.btnReadAll.Width) / 2;
        }
        #endregion
    }
}
