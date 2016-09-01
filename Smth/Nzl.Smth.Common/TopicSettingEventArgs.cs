namespace Nzl.Smth.Common
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class TopicSettingEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public TopicSettingEventArgs()
        {
            this.BrowserType = BrowserType.FirstReply;
            this.AutoUpdating = false;
            this.UpdatingInterval = 60;
        }

        /// <summary>
        /// 
        /// </summary>
        public BrowserType BrowserType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AutoUpdating
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int UpdatingInterval
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
}

