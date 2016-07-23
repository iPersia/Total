namespace Nzl.Web.Forms.ProductClawer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;
    using Nzl.Web.Core;
    using Nzl.Web.Core.EventArgs;
    using Nzl.Web.Interface;
    using Nzl.Web.Page;
    using Nzl.Web.ProductClawer;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public partial class ProductClawerFom : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private bool _isClawing = false;

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, ProductClawerParameter> _dicClawerParam = new Dictionary<string, ProductClawerParameter>();

        /// <summary>
        /// 
        /// </summary>
        private int _maxAsyncInvokeCount = 16;

        /// <summary>
        /// 
        /// </summary>
        private int _threadDelayTime = 1000;

        /// <summary>
        /// 
        /// </summary>
        private ProductClawerScheduler _scheduler;

        /// <summary>
        /// 
        /// </summary>
        private System.Threading.SynchronizationContext _uiContext = WindowsFormsSynchronizationContext.Current;

        /// <summary>
        /// 
        /// </summary>
        public ProductClawerFom()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.dgvPrice.AutoGenerateColumns = false;
            this.LoadProductFromXml(@"products.xml");
            this._scheduler = new ProductClawerScheduler(this._threadDelayTime, this._maxAsyncInvokeCount);
        }

        private void AddClawersToScheduler(ProductClawerScheduler scheduler)
        {
            if (scheduler != null)
            {
                foreach (KeyValuePair<string, ProductClawerParameter> kp in this._dicClawerParam)
                {
                    scheduler.AddProductClawer(CreateClawer(kp.Value));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        private void LoadProductFromXml(string filename)
        {
            if (System.IO.File.Exists(filename))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(filename);
                    XmlNode root = doc.SelectSingleNode("infor");
                    XmlNode config = root.SelectSingleNode("configuration");
                    this._maxAsyncInvokeCount = System.Convert.ToInt32(config.SelectSingleNode("MaxAsyncInvokeCount").InnerText);
                    this._threadDelayTime = System.Convert.ToInt32(config.SelectSingleNode("ThreadDelayTime").InnerText);
                    XmlNode pistNode = root.SelectSingleNode("productlist");
                    XmlNodeList productList = pistNode.ChildNodes;
                    foreach (XmlNode node in productList)
                    {
                        this._dicClawerParam.Add(node.SelectSingleNode("url").InnerText,
                                                 new ProductClawerParameter(node.SelectSingleNode("name").InnerText.ToUpper(),
                                                                            node.SelectSingleNode("url").InnerText,
                                                                            System.Convert.ToInt32(node.SelectSingleNode("interval").InnerText),
                                                                            System.Convert.ToDecimal(node.SelectSingleNode("targetprice").InnerText)));
                    }
                }
                catch (Exception exp)
                {
                    if (Program.LoggerEnabled)
                    {
                        Program.Logger.Error(exp.Message);
                    }

#if (DEBUG)
                    CommonUtil.ShowMessage(this, exp.Message);
#endif
                    MessageBox.Show("加载配置文件异常！\n" + exp.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private BaseProductClawer CreateClawer(ProductClawerParameter param)
        {
            return CreateClawer(param.Name, param.Uri, param.Interval, param.TargetPrice);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private BaseProductClawer CreateClawer(string name, string url, int interval, decimal targetPrice)
        {
            BaseProductClawer clawer = ProductClawerFactory.CreateProductClawer(name, url, interval, targetPrice);
            if (clawer != null)
            {
                clawer.PriceChanged += new EventHandler<PriceClawingEventArgs>(PriceChanged);
                clawer.PriceClawed += new EventHandler<PriceClawingEventArgs>(PriceClawed);
                clawer.TargetPriceAccur += new EventHandler<PriceClawingEventArgs>(TargetPriceAccur);
            }

            return clawer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        //private void UpdatePriceInfo(PriceClawingEventArgs e)
        private void UpdatePriceInfo(object obj)
        {
            try
            {
                if (this._uiContext != null)
                {
                    this._uiContext.Post(this.UpdateSchedulerInfor, obj);
                    this._uiContext.Post(this.UpdatePriceInfor, obj);
                }
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

#if (DEBUG)
                CommonUtil.ShowMessage(this, "Exception accured: " + exp.Message);
#endif
            }
        }

        /// <summary>
        /// Set scheduler information.
        /// </summary>
        /// <returns></returns>
        private void UpdateSchedulerInfor(object state)
        {
            PriceClawingEventArgs e = state as PriceClawingEventArgs;
            if (e != null)
            {
                if (e.Flag && e.IsUpdated)
                {
                    this.lblStatus.Text = e.Product.Vendor + " - " + e.Product.Title;
                }
                else
                {
                    this.lblStatus.Text = "！ - " + e.Message;
                }

                this.lblSchedulerInfor.Text = "·产品数量：" + string.Format("{0}", this.dgvPrice.Rows.Count)
                                            + "·线程数量：" + string.Format("{0}", this._scheduler.RunningThreadCount)
                                            + "·队列数量：" + string.Format("{0}", this._scheduler.QueueCount)
                                            + "·执行次数：" + string.Format("{0}", this._scheduler.TotalExcuteCount)
                                            + "·网络流量：" + FormatNetworkFlow(this._scheduler.NetworkFlow);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void UpdatePriceInfor(object state)
        {
            PriceClawingEventArgs e = state as PriceClawingEventArgs;
            if (e != null)
            {
                lock (this.dgvPrice)
                {
                    if (e.Flag && e.IsUpdated)
                    {
                        foreach (DataGridViewRow row in this.dgvPrice.Rows)
                        {
                            if (row.Cells["Vendor"].Value.ToString().ToUpper() == e.Product.Vendor.ToUpper()
                                && row.Cells["Title"].Value.ToString().ToUpper() == e.Product.Title.ToUpper())
                            {
                                row.Cells["Price"].Value = e.Product.Price;
                                row.Cells["Stock"].Value = e.Product.IsInStock ? "有货" : "无货";
                                row.Selected = true;
                                return;
                            }
                        }

                        int index = this.dgvPrice.Rows.Add();
                        this.dgvPrice.Rows[index].Cells["URL"].Value = e.Product.Uri;
                        this.dgvPrice.Rows[index].Cells["Vendor"].Value = e.Product.Vendor;
                        this.dgvPrice.Rows[index].Cells["ProductName"].Value = e.Product.Name;
                        this.dgvPrice.Rows[index].Cells["Title"].Value = e.Product.Title;
                        this.dgvPrice.Rows[index].Cells["Price"].Value = e.Product.Price;
                        this.dgvPrice.Rows[index].Cells["Stock"].Value = e.Product.IsInStock ? "有货" : "无货";
                        this.dgvPrice.Rows[index].Selected = true;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flowSize"></param>
        /// <returns></returns>
        private string FormatNetworkFlow(decimal flowSize)
        {
            int[] bs = new int[5] { 0, 0, 0, 0, 0 };
            string[] ut = new string[5] { "B", "K", "M", "G", "T" };
            int ith = 0;
            while (flowSize > 0)
            {
                bs[ith] = (int)(flowSize % 1024);
                flowSize -= bs[ith];
                flowSize /= 1024;
                ith++;
            }

            string formatStringResult = "";
            for (int i = 4; i >= 0; i--)
            {
                if (bs[i] != 0)
                {
                    formatStringResult += string.Format("{0,4}", bs[i]) + ut[i];
                }
            }

            return formatStringResult;
        }

        #region Clawer event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PriceChanged(object sender, PriceClawingEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PriceClawed(object sender, PriceClawingEventArgs e)
        {
            if (this.IsDisposed == false)
            {
                Thread thread = new Thread(UpdatePriceInfo);
                thread.Name = "UpdatePriceInforThread";
                thread.IsBackground = true;
                thread.Start(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetPriceAccur(object sender, PriceClawingEventArgs e)
        {
            if (e.Product.Price > 0)
            {
                IPrice price = sender as IPrice;
                if (price != null)
                {
                    price.StopClaw();
                    this._scheduler.RemoveProductClawer(e.Product.Uri);
                }
            }

            if (this._uiContext != null)
            {
                this._uiContext.Post(this.TargetPriceAccured, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void TargetPriceAccured(object state)
        {
            PriceClawingEventArgs e = state as PriceClawingEventArgs;
            if (e != null)
            {
                try
                {
                    CommonUtil.OpenUrl(string.IsNullOrEmpty(e.Product.QuickUri) ? e.Product.Uri : e.Product.QuickUri);
                    ProductMessageDialog msgForm = new ProductMessageDialog("降价信息", e.Product);
                    msgForm.StartPosition = FormStartPosition.CenterParent;
                    msgForm.ShowDialog(this);
                }
                catch (Exception exp)
                {
                    if (Program.LoggerEnabled)
                    {
                        Program.Logger.Error(exp.Message);
                    }
#if (DEBUG)
                    CommonUtil.ShowMessage(this, exp.Message);
#endif
                    MessageBox.Show(exp.Message);
                }
            }
        }
        #endregion

        #region Control event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTriger_Click(object sender, EventArgs e)
        {
            this._isClawing = !this._isClawing;
            this.SetButtonEnabled(false);
            if (this._isClawing)
            {
                this.btnTriger.Text = "Stop?";
                this.AddClawersToScheduler(_scheduler);
                this._scheduler.Start();
                this.SetButtonEnabled(true);
            }
            else
            {
                this.btnTriger.Text = "Start?";
                this._scheduler.Stop();
                this._scheduler.ClearProductClawer();
                this._scheduler.ClearQueue();
                Thread thread = new Thread(WaitForStop);
                thread.Name = "WaitForStopThread";
                thread.IsBackground = true;
                thread.Start();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        private void SetButtonEnabled(bool flag)
        {
            this.btnAdd.Enabled = flag;
            this.btnClearQueue.Enabled = flag;
            this.btnTriger.Enabled = flag;
            this.btnReset.Enabled = flag;
        }

        /// <summary>
        /// 
        /// </summary>
        private void WaitForStop()
        {
            while (true)
            {
                if (this._scheduler.RunningThreadCount <= 0)
                {
#if (DEBUG)
                    CommonUtil.ShowMessage(this, "Scheduler - Running Thread Count:\t" + this._scheduler.RunningThreadCount);
#endif
                    break;
                }

                Thread.Sleep(500);
#if (DEBUG)
                CommonUtil.ShowMessage(this, "Scheduler - Running Thread Count:\t" + this._scheduler.RunningThreadCount);
#endif
            }

            if (this.IsDisposed == false)
            {
                this.BeginInvoke((EventHandler)delegate
                {
                    this.SetButtonEnabled(true);
                });
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearQueue_Click(object sender, EventArgs e)
        {
            this._scheduler.ClearQueue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductDialog dlg = new ProductDialog();
            dlg.StartPosition = FormStartPosition.CenterParent;
            DialogResult resut = dlg.ShowDialog(this);
            if (resut == System.Windows.Forms.DialogResult.OK)
            {
                if (_dicClawerParam.ContainsKey(dlg.Uri) == false)
                {
                    this._dicClawerParam.Add(dlg.Uri, new ProductClawerParameter(dlg.ProductName, dlg.Uri, dlg.Interval, dlg.TargetPrice));
                    this._scheduler.AddProductClawer(CreateClawer(new ProductClawerParameter(dlg.ProductName, dlg.Uri, dlg.Interval, dlg.TargetPrice)));
                }
            }
        }
        #endregion

        #region override method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }
        #endregion

        /// <summary>
        /// Cell content click event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPrice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPrice.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex > -1)
            {
                string target = dgvPrice.Rows[e.RowIndex].Cells["URL"].Value.ToString();
                try
                {
                    System.Diagnostics.Process.Start(target);
                }
                catch (System.ComponentModel.Win32Exception noBrowser)
                {
                    if (Program.LoggerEnabled)
                    {
                        Program.Logger.Error(noBrowser.Message);
                    }

#if (DEBUG)
                    CommonUtil.ShowMessage(this, noBrowser.Message);
#endif
                    if (noBrowser.ErrorCode == int.MinValue)
                    {
                        MessageBox.Show(noBrowser.Message);
                    }
                }
                catch (System.Exception other)
                {
                    if (Program.LoggerEnabled)
                    {
                        Program.Logger.Error(other.Message);
                    }

#if (DEBUG)
                    CommonUtil.ShowMessage(this, other.Message);
#endif
                    MessageBox.Show(other.Message);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.SetButtonEnabled(false);
            this._scheduler.ClearClawer();
            this._scheduler.ClearQueue();
            this.AddClawersToScheduler(this._scheduler);
            this.dgvPrice.Rows.Clear();
            this._scheduler.Start();
            this._isClawing = true;
            this.btnTriger.Text = "Stop?";
            this.SetButtonEnabled(true);
        }
    }
}
