namespace Nzl.Web.ProductClawer
{
    using System;
    using System.Net;
    using System.Timers;
    using Nzl.Web.Core;
    using Nzl.Web.Core.EventArgs;
    using Nzl.Web.Interface;
    using Nzl.Web.Page;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseProductClawer : IPrice, IWorkItem, IException
    {
        #region Variables
        /// <summary>
        /// 
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// 
        /// </summary>
        protected ProductClawerParameter _clawerParam;

        /// <summary>
        /// 
        /// </summary>
        private DateTime _prevRequestDateTime = DateTime.MinValue;

        /// <summary>
        /// 
        /// </summary>
        private bool _needWebPage = true;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        protected string Verdor
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        protected bool NeedWebPage
        {
            get
            {
                return this._needWebPage;
            }

            set
            {
                this._needWebPage = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ProductClawerParameter ClawerParam
        {
            get
            {
                if (this._clawerParam == null)
                {
                    this._clawerParam = new ProductClawerParameter();
                }

                return this._clawerParam;
            }

            set
            {
                this._clawerParam = value;
            }
        }

        /// <summary>
        /// The currency.
        /// </summary>
        public Currency Currency
        {
            get;
            set;
        }

        #endregion

        #region virtual.
        /// <summary>
        /// Get price information.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected virtual string GetPriceInformation(WebPage page, PriceClawingEventArgs e)
        {
            return string.Empty;
        }

        /// <summary>
        /// Get stock information. 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected virtual string GetStockInformation(WebPage page, PriceClawingEventArgs e)
        {
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnPriceClawing(object sender, PriceClawingEventArgs e)
        {
            if (e.Product != null)
            {
                e.Product.Name = this._clawerParam.Name;
                e.Product.Uri = this._clawerParam.Uri;
            }

            this.PriceClawed(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnPriceClawed(object sender, PriceClawingEventArgs e)
        {
            if (e.Product != null)
            {
                if (e.Product.Price < e.Product.MarketPrice)
                {
                    this.PriceChanged(sender, e);
                }

                if (e.Product.Price <= this.ClawerParam.TargetPrice)
                {
                    this.TargetPriceAccur(sender, e);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnPriceChanged(object sender, PriceClawingEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnTargetPriceAccur(object sender, PriceClawingEventArgs e)
        {
        }
        #endregion

        #region ctor.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        public BaseProductClawer(ProductClawerParameter param)
        {
            this.ClawerParam = param;
            this.Currency = Currency.RMB;
            this.Init();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="interval"></param>
        /// <param name="targetPrice"></param>
        public BaseProductClawer(string name, string url, int interval, decimal targetPrice)
        {
            this.ClawerParam.Name = name;
            this.ClawerParam.Uri = url;
            this.ClawerParam.Interval = interval;
            this.ClawerParam.TargetPrice = targetPrice;
            this.Init();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this._timer = new Timer();
            this._timer.Interval = this.ClawerParam.Interval;
            this._timer.Elapsed += new ElapsedEventHandler(Elapsed);
            this.PriceClawing += new EventHandler<PriceClawingEventArgs>(OnPriceClawing);
            this.PriceClawed += new EventHandler<PriceClawingEventArgs>(OnPriceClawed);
            this.PriceChanged += new EventHandler<PriceClawingEventArgs>(OnPriceChanged);
            this.TargetPriceAccur += new EventHandler<PriceClawingEventArgs>(OnTargetPriceAccur);
        }
        #endregion

        #region main.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.PriceClawing != null)
            {
                this.PriceClawing(sender, new PriceClawingEventArgs());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="from"></param>
        protected void OnNewExceptionAccured(Exception exp, object from)
        {
            if (this.NewExceptionAccured != null)
            {
                this.NewExceptionAccured(this, new ExceptionEventArgs(exp, from));
            }
        }
        #endregion        

        #region Excute the IPrice interface.
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<PriceClawingEventArgs> PriceChanged;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<PriceClawingEventArgs> PriceClawing;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<PriceClawingEventArgs> PriceClawed;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<PriceClawingEventArgs> TargetPriceAccur;

        /// <summary>
        /// 
        /// </summary>
        public virtual void StartClaw()
        {
            this._timer.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void StopClaw()
        {
            this._timer.Enabled = false;
        }
        #endregion

        #region Excute the IPrice interface.
        /// <summary>
        /// 
        /// </summary>
        public bool Execute()
        {
            PriceClawingEventArgs e = new PriceClawingEventArgs();
            try
            {
                WebPage webPage = this.NeedWebPage ? WebPageFactory.CreateWebPage(this.ClawerParam.Uri) : null;
                if (this.NeedWebPage == false || (webPage != null && webPage.IsGood))
                {
                    e.NetworkFlow = webPage == null ? 0 : webPage.PageSize;
                    e.Product = e.Product == null ? new Product() : e.Product;
                    e.Product.Vendor = this.Verdor;
                    e.Product.Title = webPage == null ? this.ClawerParam.Name : webPage.Title;
                    string stockError = GetStockInformation(webPage, e);
                    if (string.IsNullOrEmpty(stockError) == false)
                    {
                        throw new Exception(this.Verdor + " - " + this.ClawerParam.Name + " - 提取“是否有货”信息失败！");
                    }

                    string priceError = GetPriceInformation(webPage, e);
                    if (string.IsNullOrEmpty(priceError) == false)
                    {
                        throw new Exception(this.Verdor + " - " + this.ClawerParam.Name + " - 提取“价格”信息失败！");
                    }

                    e.Flag = true;
                    e.IsUpdated = true;
                }
                else
                {
                    e.Message = this.Verdor + " - " + this.ClawerParam.Name + " - 抓取页面失败！";
                }


                this.OnPriceClawing(this, e);
                return e.Flag;
            }
            catch (Exception exp)
            {
                e.Flag = false;
                e.Message = exp.Message;
#if (DEBUG)
                CommonUtil.ShowMessage(this, exp.Message);
#endif
                OnNewExceptionAccured(exp, this);
                return e.Flag;
            }
        }
        #endregion

        #region Excute the IException interface.
        /// <summary>
        /// New exception accured eventhandler.
        /// </summary>
        public event EventHandler<ExceptionEventArgs> NewExceptionAccured;
        #endregion
    }
}
