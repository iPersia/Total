namespace Nzl.Web.ProductClawer
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Nzl.Web.Core;
    using Nzl.Web.Core.EventArgs;
    using Nzl.Web.Util;

    public class ProductClawerScheduler
    {
        #region Variables
        /// <summary>
        /// 
        /// </summary>
        private List<BaseProductClawer> _productClawerList = new List<BaseProductClawer>();

        /// <summary>
        /// 
        /// </summary>
        private object _productClawerListLocker = new object();
        
        /// <summary>
        /// 
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// 
        /// </summary>
        private int _clawInterval = 1000;

        /// <summary>
        /// 
        /// </summary>
        private ProductClawerDepatcher _productClawerDespatcher = new ProductClawerDepatcher();

        /// <summary>
        /// 
        /// </summary>
        private decimal _networkFlow = decimal.Zero;

        /// <summary>
        /// 
        /// </summary>
        private object _networkFlowLocker = new object();
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal NetworkFlow
        {
            get
            {
                return this._networkFlow;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int QueueCount
        {
            get
            {
                return this._productClawerDespatcher.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int RunningThreadCount
        {
            get
            {
                return this._productClawerDespatcher.AsyncInvokeCount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public UInt64 TotalQueueCount
        {
            get
            {
                return this._productClawerDespatcher.TotalCount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public UInt64 TotalExcuteCount
        {
            get
            {
                return this._productClawerDespatcher.TotalExcuteCount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ClawInterval
        {
            get
            {
                return this._clawInterval;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MaxThreadCount
        {
            get
            {
                return this._productClawerDespatcher.MaxAsyncInvokeCount;
            }

            set
            {
                this._productClawerDespatcher.MaxAsyncInvokeCount = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public ProductClawerScheduler()
        {
            this.Init();
        }

        /// <summary>
        /// 
        /// </summary>
        public ProductClawerScheduler(int interval)
            : this()
        {
            if (interval > 0)
            {
                this._clawInterval = interval;
            }
        }        

        /// <summary>
        /// 
        /// </summary>
        public ProductClawerScheduler(int interval, int maxAsyncCount)
            : this(interval)
        {
            if (maxAsyncCount > 0)
            {
                this._productClawerDespatcher.MaxAsyncInvokeCount = maxAsyncCount;
            }
        } 

        /// <summary>
        /// 
        /// </summary>
        protected void Init()
        {
            this._timer = new Timer(new TimerCallback(_timer_Elapsed), null, 0, Timeout.Infinite);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            this._timer.Change(0, _clawInterval);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start(int interval)
        {
            this._timer.Change(0, interval);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            this._timer.Change(0, Timeout.Infinite);
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        protected void _timer_Elapsed(object sender)
        {
            Random rdm = new Random();
            lock (this._productClawerListLocker)
            {
                if (this._productClawerList.Count > 0)
                {
                    try
                    {
                        int index = rdm.Next(0, this._productClawerList.Count);
                        this._productClawerDespatcher.Add(this._productClawerList[index]);
                    }
                    catch (Exception exp)
                    {
#if (DEBUG)
                        CommonUtil.ShowMessage(this, exp.Message);
#endif
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        public void AddProductClawer(BaseProductClawer item)
        {
            if (item != null)
            {
                item.PriceClawed += new EventHandler<PriceClawingEventArgs>(item_PriceClawed);
                lock (this._productClawerListLocker)
                {
                    this._productClawerList.Add(item);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        public void RemoveProductClawer(string url)
        {
            if (string.IsNullOrEmpty(url) == false)
            {
                lock (this._productClawerListLocker)
                {
                    for (int i = 0; i < this._productClawerList.Count; i++)
                    {
                        if (url == this._productClawerList[i].ClawerParam.Uri)
                        {
                            this._productClawerList.RemoveAt(i);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void item_PriceClawed(object sender, PriceClawingEventArgs e)
        {
            if (e.IsUpdated)
            {
                UpdateNetworkFlow(e.NetworkFlow);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearProductClawer()
        {
            lock (this._productClawerListLocker)
            {
                this._productClawerList.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearQueue()
        {
            lock (this._productClawerDespatcher)
            {
                this._productClawerDespatcher.Reset();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void ClearClawer()
        {
            lock (this._productClawerList)
            {
                this._productClawerList.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flowSize"></param>
        public void UpdateNetworkFlow(decimal flowSize)
        {
            lock (this._networkFlowLocker)
            {
                this._networkFlow += flowSize;
            }
        }        
    }
}
