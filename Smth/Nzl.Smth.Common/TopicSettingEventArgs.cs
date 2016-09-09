namespace Nzl.Smth
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
            this.BrowserType = ThreadBrowserType.FirstReply;
            this.AutoUpdating = false;
            this.UpdatingInterval = 60;
        }

        /// <summary>
        /// 
        /// </summary>
        public ThreadBrowserType BrowserType
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

