namespace Nzl.Web.Forms.MobileNewSmth.Datas
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;     
    using System.Linq;
    using System.Text;
    using Nzl.Web.Page;

    /// <summary>
    /// 
    /// </summary>
    public class UrlInfo
    {
        /// <summary>
        /// 
        /// </summary>
        private int _index = 1;

        /// <summary>
        /// 
        /// </summary>
        private int _total = 1;

        /// <summary>
        /// 
        /// </summary>
        private bool _isAppend = true;

        /// <summary>
        /// 
        /// </summary>
        public UrlInfo() 
        { 
        }

        /// <summary>
        /// 
        /// </summary>
        public UrlInfo(UrlInfo srcInfo)
        {
            this.BaseUrl = srcInfo.BaseUrl;
            this.Subject = srcInfo.Subject;
            this.Index = srcInfo.Index;
            this.Total = srcInfo.Total;
            this.IsAppend = srcInfo.IsAppend;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string BaseUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Index
        {
            get
            {
                return this._index;
            }
            set
            {
                this._index = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Total
        {
            get
            {
                return this._total;
            }
            set
            {
                this._total = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAppend
        {
            get
            {
                return this._isAppend;
            }
            set
            {
                this._isAppend = value;
            }
        }

        public PageStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public WebPage WebPage
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public IList<BaseData> Result
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public object Tag
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum PageStatus
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("The page is not found!")]
        NotFound=1,

        /// <summary>
        /// 
        /// </summary>
        [Description("Gets the page time out!")]
        TimeOut,

        /// <summary>
        /// 
        /// </summary>
        [Description("Gets the page succeedes!")]
        Normal,

        /// <summary>
        /// 
        /// </summary>
        [Description("The page is permission denied!")]
        AccessRestricted,

        /// <summary>
        /// 
        /// </summary>
        [Description("Unknown exception occurs!")]
        UnKnown
    }
}
