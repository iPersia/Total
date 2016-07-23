using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nzl.Util;
using Nzl.Web.Core;
using Nzl.Web.Core.EventArgs;
using Nzl.Web.Interface;
using Nzl.Web.Util;

namespace Nzl.Web.Forms.Rss
{
    /// <summary>
    /// The rss monitor form.
    /// </summary>
    public partial class RssMonitorForm : Form
    {
        #region vars.
        /// <summary>
        /// 
        /// </summary>
        public readonly static string DefaultFlag = " ";

#if (DEBUG)
        /// <summary>
        /// 
        /// </summary>
        private static int _totalCount = 32;
#else
        /// <summary>
        /// 
        /// </summary>
        private static int _totalCount = 64;
#endif

        /// <summary>
        /// 
        /// </summary>
        private List<IRssReader> _lstReaders = new List<IRssReader>();

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, TabPage> _dictTabPages = new Dictionary<string, TabPage>();

        /// <summary>
        /// 
        /// </summary>
        private Panel _activeRssItemPanel = null;

        /// <summary>
        /// 
        /// </summary>
        private string _closeFlag = null;

        /// <summary>
        /// 
        /// </summary>
        private string _newItemFlag = "?";

        /// <summary>
        /// 
        /// </summary>
        private int _defaultTabPageStringCount = 12;

        /// <summary>
        /// 
        /// </summary>
        private int _balloonTipElapsedTime = 500;

        /// <summary>
        /// 
        /// </summary>
        private bool _isRefreshingContent = false;
        #endregion

        #region delegate.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        private delegate void InvokeUpdateNewItemsDelegate(IRssReader reader, NewItemsCapturedEnventArgs e);
        #endregion

        #region ctor.
        /// <summary>
        /// Ctor.
        /// </summary>
        public RssMonitorForm()
        {
            InitializeComponent();
        }
        #endregion

        #region form event.
        /// <summary>
        /// Form load event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RssMonitorForm_Load(object sender, EventArgs e)
        {
            ///Default window size.
            this.Width = 800;
            this.Height = 600;

            ///Mouse wheel event handler.
            this.MouseWheel += new MouseEventHandler(RssMonitorForm_MouseWheel);

            ///
            this.ntyIcon.Visible = false;

            ///
            IEnumerable<IRssReader> ie =
                AssemblyUtil.GetImplementedObjectsByDirectory<IRssReader>(Application.StartupPath + "\\RssReaders");
            if (ie != null)
            {
                ie = ie.OrderBy(x => x.Vendor).ToArray();
                foreach (IRssReader reader in ie)
                {
                    if (reader.AutoLoad)
                    {
                        this._lstReaders.Add(reader);
                    }
                }
            }

            ///
            ApplyReaders(this._lstReaders);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RssMonitorForm_Shown(object sender, EventArgs e)
        {
            if (this._activeRssItemPanel == null && this.tcRss.TabPages.Count > 0)
            {
                this._activeRssItemPanel = this.tcRss.TabPages[0];
            }

#if (DEBUG)
            this.Text = this.Text + " - DEBUG";
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RssMonitorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._closeFlag != "NotifyIcon")
            {
                e.Cancel = true;

                this.ShowInTaskbar = false;
                this.Hide();
                this.ntyIcon.Visible = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RssMonitorForm_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (this._activeRssItemPanel != null)
                {
                    int yPos = this._activeRssItemPanel.Location.Y + e.Delta;
                    yPos = yPos > 3 ? 3 : yPos;
                    yPos = yPos < this.tcRss.Height - this._activeRssItemPanel.Height - 3 ? this.tcRss.Height - this._activeRssItemPanel.Height - 3 : yPos;

                    ////Smooth rollong.
                    int size = yPos - this._activeRssItemPanel.Location.Y;
                    int loop = 25;
                    int step = size / loop;
                    for (int i = 0; i < loop; i++)
                    {
                        this._activeRssItemPanel.Location = new Point(this._activeRssItemPanel.Location.X, this._activeRssItemPanel.Location.Y + step
                            );
                    }

                    this._activeRssItemPanel.Location = new Point(this._activeRssItemPanel.Location.X, this._activeRssItemPanel.Location.Y + size - loop * step);
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
        #endregion

        #region main.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendor"></param>
        /// <param name="margin"></param>
        /// <returns></returns>
        private string GetTabPageTitle(string vendor, string replacement)
        {
            int marginCount = (this._defaultTabPageStringCount - vendor.Length + 1) / 2;
            string title = vendor.PadLeft(vendor.Length + marginCount, replacement[0]);
            return title.PadRight(title.Length + marginCount, replacement[0]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private TabPage GetOrCreateTabPageByRssReader(IRssReader reader)
        {
            if (this._dictTabPages.ContainsKey(reader.UniqueID))
            {
                return this._dictTabPages[reader.UniqueID];
            }

            //TabPage resultTp = new TabPage(DefaultFlag + reader.Vendor + DefaultFlag);
            TabPage resultTp = new TabPage(this.GetTabPageTitle(reader.Vendor, DefaultFlag));
            {
                resultTp.Name = "tp" + reader.Vendor;

                Panel pc = new Panel();
                pc.Name = "pc" + reader.Vendor;
                pc.Dock = DockStyle.Fill;
                pc.BorderStyle = BorderStyle.FixedSingle;
                resultTp.Controls.Add(pc);

                Panel panel = new Panel();
                panel.Left = 3;
                panel.Top = 3;
                panel.Width = this.tcRss.Width - 14;
                panel.Tag = reader;
                resultTp.Tag = panel;
                resultTp.Controls[0].Controls.Add(panel);

                this._dictTabPages.Add(reader.UniqueID, resultTp);
            }

            return resultTp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readers"></param>
        private void ApplyReaders(IList<IRssReader> readers)
        {
            foreach (TabPage tp in this._dictTabPages.Values)
            {
                ((tp.Tag as Panel).Tag as IRssReader).Stop();
            }

            this.tcRss.TabPages.Clear();
            foreach (IRssReader reader in readers)
            {
                TabPage tp = this.GetOrCreateTabPageByRssReader(reader);
                if (tp != null)
                {
                    this.tcRss.TabPages.Add(tp);
                    IRssReader runningReader = ((tp.Tag as Panel).Tag as IRssReader);
                    if (runningReader != null)
                    {
                        runningReader.Stop();
                        runningReader.NewItemsCaptured -= new EventHandler<NewItemsCapturedEnventArgs>(reader_NewItemsCaptured);
                        runningReader.NewItemsCaptured += new EventHandler<NewItemsCapturedEnventArgs>(reader_NewItemsCaptured);
                        IException ie = runningReader as IException;
                        if (ie != null)
                        {
                            ie.NewExceptionAccured -= new EventHandler<ExceptionEventArgs>(reader_NewExceptionAcurred);
                            ie.NewExceptionAccured += new EventHandler<ExceptionEventArgs>(reader_NewExceptionAcurred);
                        }

                        runningReader.Start();
                    }
                }
            }

            this.tcRss.SelectedIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="e"></param>
        private void UpdateNewItems(IRssReader reader, NewItemsCapturedEnventArgs e)
        {
            if (reader != null)
            {
                TabPage tp = this.GetOrCreateTabPageByRssReader(reader);
                if (tp != null)
                {
                    Panel panel = tp.Tag as Panel;
                    if (panel != null)
                    {
                        while (this._isRefreshingContent) ;

                        lock (panel)
                        {
                            IList<RssItem> tmpLst = e.Items;
                            if (tmpLst.Count > 0)
                            {
                                this.tcRss.Enabled = false;
                                tmpLst = tmpLst.OrderBy(x => x.DateTime).ToList();
                                foreach (RssItem ri in tmpLst)
                                {
                                    AddItemToPanel(panel, ri);
                                }

                                panel.Top = 3;
                                this.tcRss.Enabled = true;                                
                                tp.Text = tp.Text.Substring(0, 1).Replace(DefaultFlag, this._newItemFlag)
                                        + tp.Text.Substring(1, tp.Text.Length - 2)
                                        + tp.Text.Substring(tp.Text.Length - 1).Replace(DefaultFlag, this._newItemFlag);                                

                                ////Show balloon tip.
                                if (this.ntyIcon.Visible)
                                {
                                    IList<string> ballonTips = new List<string>();
                                    StringBuilder sb = new StringBuilder();
                                    foreach (RssItem ri in tmpLst)
                                    {
                                        sb.AppendLine("@" + ri.Title);
                                        if (sb.Length > 128)
                                        {
                                            ballonTips.Add(sb.ToString());
                                            sb.Clear();
                                        }
                                    }

                                    if (sb.Length > 0)
                                    {
                                        ballonTips.Add(sb.ToString());
                                    }

                                    foreach (string tip in ballonTips)
                                    {
                                        this.ntyIcon.ShowBalloonTip(this._balloonTipElapsedTime,
                                                                    tmpLst[0].Vendor,
                                                                    tip,
                                                                    ToolTipIcon.Info);
                                    }
                                }
                            }
                        }
                    }
                }

                this.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ri"></param>
        private void AddItemToPanel(Panel panel, RssItem ri)
        {
            try
            {
                if (ri != null)
                {
                    lock (panel)
                    {
                        ///Remove item.
                        for (int i = 0; i <= panel.Controls.Count - _totalCount; i++)
                        {
                            panel.Height -= panel.Controls[0].Height + 1;
                            (panel.Controls[0] as RssItemControl).Dispose();
                            panel.Controls.RemoveAt(0);
                        }

                        ///Add new item.
                        int accHeight = 1;
                        RssItemControl newRic = new RssItemControl(ri);
                        newRic.OnTitleLinkClicked += new LinkLabelLinkClickedEventHandler(ric_OnTitleLinkClicked);
                        newRic.Top = accHeight;
                        newRic.Left = 1;
                        newRic.AllWidth = panel.Width - 2;

                        string description = ri.Description;
                        if (description != null)
                        {
                            foreach (KeyValuePair<string, Image> kp in ri.Images)
                            {
                                ///Before Image.
                                int index = description.IndexOf(kp.Key);
                                newRic.TextBox.AppendText(description.Substring(0, index));

                                ///Insert Image.
                                if (kp.Value != null)
                                {
                                    int maxImageWidth = newRic.TextBox.Width - 10;
                                    Image imageToPaste = kp.Value;
                                    if (imageToPaste.Width > maxImageWidth)
                                    {
                                        imageToPaste = new Bitmap(imageToPaste,
                                                                  new Size(maxImageWidth, maxImageWidth * imageToPaste.Height / maxImageWidth));
                                    }

                                    Clipboard.SetDataObject(imageToPaste);
                                    newRic.TextBox.Paste(DataFormats.GetFormat(DataFormats.Bitmap));
                                    Clipboard.Clear();
                                }

                                ///After Image.
                                description = description.Substring(index + kp.Key.Length);
                            }

                            newRic.TextBox.AppendText(description.TrimEnd('\n'));
                        }

                        ///Auto fixing height.
                        newRic.AutoFixHeight();

                        ///Get index & change position of the existing RssItemControl.
                        int maxInx = 0;
                        foreach (Control ctr in panel.Controls)
                        {
                            RssItemControl riCtrl = ctr as RssItemControl;
                            if (riCtrl != null)
                            {
                                riCtrl.Top += newRic.Height + 1;
                                accHeight += riCtrl.Height + 1;

                                int inx = (int)riCtrl.Tag;
                                maxInx = inx > maxInx ? inx : maxInx;
                            }
                        }

                        newRic.Tag = maxInx + 1;
                        newRic.Index = (maxInx + 1).ToString("000");
                        if ((maxInx + 1) % 2 == 0)
                        {
                            newRic.BackGroundColor = System.Drawing.Color.White;
                        }

                        newRic.TextBox.Tag = ri;
                        newRic.TextBox.ReadOnly = true;
                        panel.Controls.Add(newRic);
                        accHeight += newRic.Height + 1;
                        panel.Height = accHeight + 27;
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
                CommonUtil.ShowMessage(this, exp.Message + "\n" + exp.StackTrace);
#endif
            }
        }

        /// <summary>
        /// Refresh all the ui elements.
        /// </summary>
        private void ChangeSize(int width, int height)
        {
            try
            {
                if (width < 100 || height < 100)
                {
                    return;
                }

                this.Width = width > Screen.PrimaryScreen.Bounds.Width - 100 ? Screen.PrimaryScreen.Bounds.Width - 100 : width;
                this.Height = height > Screen.PrimaryScreen.Bounds.Height - 100 ? Screen.PrimaryScreen.Bounds.Height - 100 : height;

                ///Unused rss reader tabpage.
                this._isRefreshingContent = true;
                IList<TabPage> tpUnused = new List<TabPage>();
                foreach (TabPage tp in this._dictTabPages.Values)
                {
                    if (this.tcRss.TabPages.Contains(tp) == false)
                    {
                        tpUnused.Add(tp);
                        this.tcRss.TabPages.Add(tp);
                    }
                }

                ///Collect all rss items, then clear and re-add.
                IList<RssItem> riLst = new List<RssItem>();
                foreach (TabPage tp in this.tcRss.TabPages)
                {
                    Panel container = tp.Tag as Panel;
                    if (container != null)
                    {
                        container.Width = this.tcRss.Width - 14;
                        riLst.Clear();
                        foreach (Control ctrl in container.Controls)
                        {
                            RssItemControl ric = ctrl as RssItemControl;
                            if (ric != null)
                            {
                                RssItem ri = ric.TextBox.Tag as RssItem;
                                if (ri != null)
                                {
                                    riLst.Add(ri);
                                }
                            }
                        }

                        container.Controls.Clear();
                        foreach (RssItem ri in riLst)
                        {
                            AddItemToPanel(container, ri);
                        }
                    }
                }

                ///Clear unused tabpage.
                foreach (TabPage tp in tpUnused)
                {
                    this.tcRss.TabPages.Remove(tp);
                }

                ///Sort.
                IList<TabPage> tpInuse = new List<TabPage>();
                foreach (TabPage tp in this.tcRss.TabPages)
                {
                    tpInuse.Add(tp);
                }

                tpInuse = tpInuse.OrderBy(x => x.Name).ToArray();
                this.tcRss.TabPages.Clear();
                foreach (TabPage tp in tpInuse)
                {
                    this.tcRss.TabPages.Add(tp);
                }
            }
            catch { }
            finally
            {
                this._isRefreshingContent = false;
            }
        }
        #endregion

        #region rss reader eventhandler.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reader_NewItemsCaptured(object sender, NewItemsCapturedEnventArgs e)
        {
            IRssReader reader = sender as IRssReader;
            if (reader != null)
            {
                this.BeginInvoke(new InvokeUpdateNewItemsDelegate(UpdateNewItems), reader, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reader_NewExceptionAcurred(object sender, ExceptionEventArgs e)
        {
            if (e.Exception != null && e.From != null)
            {
                this.ntyIcon.ShowBalloonTip(this._balloonTipElapsedTime,
                                            e.Exception.Message,
                                            e.From.ToString(),
                                            ToolTipIcon.Info);
            }
        }
        #endregion

        #region control eventhandler.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcRss_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tcRss.SelectedTab != null)
            {
                this._activeRssItemPanel = this.tcRss.SelectedTab.Tag as Panel;
                this.tcRss.SelectedTab.Text = this.tcRss.SelectedTab.Text.Replace(this._newItemFlag, DefaultFlag);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ric_OnTitleLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linklbl = sender as LinkLabel;
            if (linklbl != null)
            {
                e.Link.Visited = true;
                ///Reset the tabpage's title.
                this.tcRss.SelectedTab.Text = this.tcRss.SelectedTab.Text.Replace(this._newItemFlag, DefaultFlag);
                CommonUtil.OpenUrl(e.Link.LinkData.ToString());
            }
        }
        #endregion

        #region notify icon eventhandler.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSettings_Click(object sender, EventArgs e)
        {
            RssMonitorSettingsDlg dlg = new RssMonitorSettingsDlg();//this._lstReaders, _totalCount);
            dlg.StartPosition = FormStartPosition.CenterParent;
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                _totalCount = dlg.TotalCount;
                this._lstReaders.Clear();
                foreach (IRssReader reader in dlg.ActiveRssReaders)
                {
                    this._lstReaders.Add(reader);
                }

                ///
                ApplyReaders(this._lstReaders);

                ///
                Size size = dlg.GetWindowSize();
                if (size.Width > 0 && size.Height > 0)
                {
                    ChangeSize(size.Width, size.Height);
                    if (this.tcRss.TabPages.Count > 0)
                    {
                        this.tcRss.SelectedIndex = 0;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this._closeFlag = "NotifyIcon";
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ntyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.ShowInTaskbar = true;
            this.ntyIcon.Visible = false;
        }
        #endregion
    }
}
