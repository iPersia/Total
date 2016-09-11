namespace Nzl.Smth.Configs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public static class Configuration
    {
        #region event
        /// <summary>
        /// 
        /// </summary>
        public static EventHandler OnSectionTopsUpdatingIntervalChanged;

        /// <summary>
        /// 
        /// </summary>
        public static EventHandler OnTop10sLoadingIntervalChanged;

        /// <summary>
        /// 
        /// </summary>
        public static EventHandler OnNewMailUpdatingIntervalChanged;

        /// <summary>
        /// 
        /// </summary>
        public static EventHandler OnLocationMarginChanged;
        #endregion

        #region variable
        /// <summary>
        /// The base url of smth.
        /// </summary>
        private static string staticBaseUrl = @"http://m.newsmth.net";

        /// <summary>
        /// The base url of smth.
        /// </summary>
        private static string staticLoginUrl = @"http://m.newsmth.net/user/login";

        /// <summary>
        /// The base url of smth.
        /// </summary>
        private static string staticLogoutUrl = @"http://m.newsmth.net/user/logout";

        /// <summary>
        /// The base url of smth.
        /// </summary>
        private static string staticInboxUrl = @"http://m.newsmth.net/mail/inbox";

        /// <summary>
        /// The base url of smth.
        /// </summary>
        private static string staticOutboxUrl = @"http://m.newsmth.net/mail/outbox";

        /// <summary>
        /// The base url of smth.
        /// </summary>
        private static string staticTrashBaseUrl = @"http://m.newsmth.net/mail/deleted";

        /// <summary>
        /// The base url of smth.
        /// </summary>
        private static string staticAttachmentBaseUrl = @"http://att.newsmth.net/nForum";

        /// <summary>
        /// The base url of smth.
        /// </summary>
        private static string staticSendMailUrl = @"http://m.newsmth.net/mail/send";

        /// <summary>
        /// 
        /// </summary>
        private static string staticReferUrl = @"http://m.newsmth.net/refer/reply";

        /// <summary>
        /// 
        /// </summary>
        private static string staticAtUrl = @"http://m.newsmth.net/refer/at";

        /// <summary>
        /// 
        /// </summary>
        private static string staticReadAllReferUrl = @"http://m.newsmth.net/refer/reply/read?index=all";
#if (DEBUG)
        /// <summary>
        /// The interval to update the section tops in SectionTopControl.
        /// </summary>
        private static int staticSectionTopsUpdatingInterval = 30 * 1000;

        /// <summary>
        /// The interval to load the SectionTopControls.
        /// </summary>
        private static int staticTop10sLoadingInterval = 30 * 1000;

        /// <summary>
        /// The interval to load the SectionTopControls.
        /// </summary>
        private static int staticNewMailCheckingInterval = 30 * 1000;
#else
        /// <summary>
        /// The interval to update the section tops in SectionTopControl.
        /// </summary>
        private static int staticSectionTopsUpdatingInterval = 5 * 60 * 1000;

        /// <summary>
        /// The interval to load the SectionTopControls.
        /// </summary>
        private static int staticTop10sLoadingInterval = 5 * 60 * 1000;

        /// <summary>
        /// The interval to load the SectionTopControls.
        /// </summary>
        private static int staticNewMailCheckingInterval = 5 * 60 * 1000;
#endif
        /// <summary>
        /// The count of sections.
        /// </summary>
        private static int staticSectionCount = 9;

#if (DEBUG)
        /// <summary>
        /// 
        /// </summary>
        private static int staticBaseControlContainerLocationMargin = 8;

        /// <summary>
        /// 
        /// </summary>
        private static int staticBaseControlLocationMargin = 6;
#else
                /// <summary>
        /// 
        /// </summary>
        private static int staticBaseControlContainerLocationMargin = 4;

        /// <summary>
        /// 
        /// </summary>
        private static int staticBaseControlLocationMargin = 2;
#endif

        /// <summary>
        /// 
        /// </summary>
        private static bool staticPageDispatcherSynchronous = false;

        /// <summary>
        /// 
        /// </summary>
        private static int staticTitleWordCount = 6;        
        #endregion


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public static string BaseUrl
        {
            get
            {
                return staticBaseUrl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string AttachmentBaseUrl
        {
            get
            {
                return staticAttachmentBaseUrl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string LoginUrl
        {
            get
            {
                return staticLoginUrl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string LogoutUrl
        {
            get
            {
                return staticLogoutUrl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string InboxUrl
        {
            get
            {
                return staticInboxUrl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string OutboxUrl
        {
            get
            {
                return staticOutboxUrl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string TrashUrl
        {
            get
            {
                return staticTrashBaseUrl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string SendMailUrl
        {
            get
            {
                return staticSendMailUrl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ReferUrl
        {
            get
            {
                return staticReferUrl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string AtUrl
        {
            get
            {
                return staticAtUrl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int SectionTopsUpdatingInterval
        {
            get
            {
                return staticSectionTopsUpdatingInterval;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int Top10sLoadingInterval
        {
            get
            {
                return staticTop10sLoadingInterval;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int NewMailCheckingInterval
        {
            get
            {
                return staticNewMailCheckingInterval;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int BaseControlContainerLocationMargin
        {
            get
            {
                return staticBaseControlContainerLocationMargin;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int BaseControlLocationMargin
        {
            get
            {
                return staticBaseControlLocationMargin;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int SectionCount
        {
            get
            {
                return staticSectionCount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool PageDispatcherSynchronous
        {
            get
            {
                return staticPageDispatcherSynchronous;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int TitleWordCount
        {
            get
            {
                return staticTitleWordCount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ReadAllReferUrl
        {
            get
            {
                return staticReadAllReferUrl;
            }
        }        
        #endregion

        #region Public methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static void SetSectionTopsUpdatingInterval(int value)
        {
            if (value != staticSectionTopsUpdatingInterval)
            {
                staticSectionTopsUpdatingInterval = value;
                if (OnSectionTopsUpdatingIntervalChanged != null)
                {
                    OnSectionTopsUpdatingIntervalChanged(typeof(Configuration), new EventArgs());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static void SetTop10sLoadingInterval(int value)
        {
            if (value != staticTop10sLoadingInterval)
            {
                staticTop10sLoadingInterval = value;
                if (OnTop10sLoadingIntervalChanged != null)
                {
                    OnTop10sLoadingIntervalChanged(typeof(Configuration), new EventArgs());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static void SetNewMailCheckingInterval(int value)
        {
            if (value != staticNewMailCheckingInterval)
            {
                staticNewMailCheckingInterval = value;
                if (OnNewMailUpdatingIntervalChanged != null)
                {
                    OnNewMailUpdatingIntervalChanged(typeof(Configuration), new EventArgs());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static void SetLocationMargin(int baseControlContainerLocationMargin, int baseControlLocationMargin)
        {
            if (baseControlLocationMargin != staticBaseControlLocationMargin ||
                baseControlContainerLocationMargin != staticBaseControlContainerLocationMargin)
            {
                staticBaseControlLocationMargin = baseControlLocationMargin;
                staticBaseControlContainerLocationMargin = baseControlContainerLocationMargin;
                if (OnLocationMarginChanged != null)
                {
                    OnLocationMarginChanged(typeof(Configuration), new EventArgs());
                }
            }
        }
        #endregion
    }
}
