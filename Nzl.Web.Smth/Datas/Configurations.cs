namespace Nzl.Web.Smth.Datas
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
        /// <summary>
        /// The base url of smth.
        /// </summary>
        public readonly static string BaseUrl = @"http://m.newsmth.net";

        /// <summary>
        /// The interval to update the section tops in SectionTopControl.
        /// </summary>
        public readonly static int SectionTopsUpdatingInterval = 180 * 1000;//3 * 60 * 1000;
        
        /// <summary>
        /// The interval to load the SectionTopControls.
        /// </summary>
        public readonly static int Top10sLoadingInterval = 30 * 1000;
    }
}
