namespace Nzl.Smth.Datas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public static class Configurations
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
        #endregion
        
        #region variable
        /// <summary>
        /// The base url of smth.
        /// </summary>
        private static string staticBaseUrl = @"http://m.newsmth.net";

        /// <summary>
        /// The base url of smth.
        /// </summary>
        public static string staticLoginUrl = @"http://m.newsmth.net/user/login";

        /// <summary>
        /// The base url of smth.
        /// </summary>
        public static string staticLogoutUrl = @"http://m.newsmth.net/user/logout";

        /// <summary>
        /// The interval to update the section tops in SectionTopControl.
        /// </summary>
        public static int staticSectionTopsUpdatingInterval = 5 *60 * 1000;
        
        /// <summary>
        /// The interval to load the SectionTopControls.
        /// </summary>
        public static int staticTop10sLoadingInterval = 30 * 1000;
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
                    OnSectionTopsUpdatingIntervalChanged(typeof(Configurations), new EventArgs());
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
                    OnTop10sLoadingIntervalChanged(typeof(Configurations), new EventArgs());
                }
            }
        }
        #endregion
    }
}
