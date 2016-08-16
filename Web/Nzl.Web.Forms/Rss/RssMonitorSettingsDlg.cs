using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Nzl.Web.Core;
using Nzl.Web.Interface;
using Nzl.Utils;

namespace Nzl.Web.Forms.Rss
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RssMonitorSettingsDlg : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private IList<IRssReader> _allRssReaderList = new List<IRssReader>();

        /// <summary>
        /// 
        /// </summary>
        private IList<IRssReader> _activeRssReaderList = new List<IRssReader>();

        /// <summary>
        /// 
        /// </summary>
        private int _totalCount = 100;

        /// <summary>
        /// 
        /// </summary>
        public IList<IRssReader> ActiveRssReaders
        {
            get
            {
                return _activeRssReaderList;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IList<IRssReader> RegisteredRssReaders
        {
            get
            {
                return _allRssReaderList;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TotalCount
        {
            get
            {
                return this._totalCount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public RssMonitorSettingsDlg()
        {
            InitializeComponent();
            IEnumerable<IRssReader> ie = AssemblyUtil.GetImplementedObjectsByDirectory<IRssReader>(Application.StartupPath + "\\RssReaders");
            foreach (IRssReader reader in ie)
            {
                _allRssReaderList.Add(reader);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RssMonitorSettingsDlg_Load(object sender, EventArgs e)
        {
            ///
            this.cmbTotalCount.Items.Add(40);
            this.cmbTotalCount.Items.Add(60);
            this.cmbTotalCount.Items.Add(80);
            this.cmbTotalCount.Items.Add(100);
            this.cmbTotalCount.SelectedIndex = (this._totalCount - 40) / 20;

            ///
            this.cmbWindowSize.Items.Add(new Size(0, 0));
            this.cmbWindowSize.Items.Add(new Size(640, 480));
            this.cmbWindowSize.Items.Add(new Size(800, 600));
            this.cmbWindowSize.Items.Add(new Size(1024, 768));
            this.cmbWindowSize.Items.Add(new Size(1280, 800));
            this.cmbWindowSize.Items.Add(new Size(1440, 900));
            this.cmbWindowSize.Items.Add(new Size(1600, 900));
            this.cmbWindowSize.SelectedIndex = 0;

            ///
            foreach (IRssReader reader in _allRssReaderList)
            {
                this.clbRssReaders.Items.Add(reader, reader.AutoLoad);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                _activeRssReaderList.Clear();
                int count = this.clbRssReaders.Items.Count;
                for (int i=0; i<count; i++)
                {
                    if (this.clbRssReaders.GetItemChecked(i))
                    {
                        _activeRssReaderList.Add(this.clbRssReaders.Items[i] as IRssReader);
                    }                    
                }

                if (_activeRssReaderList.Count < 1)
                {
                    MessageBox.Show("Please Select At Least 1 Rss Provider", "Caution", MessageBoxButtons.OK);
                    return;
                }

                this._totalCount = System.Convert.ToInt32(this.cmbTotalCount.SelectedItem.ToString());
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

                (new Common.MessageForm("Error", exp.Message)).ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Size GetWindowSize()
        {
            return (Size)this.cmbWindowSize.SelectedItem;
        }
    }
}
