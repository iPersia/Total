namespace Nzl.Web.Pub.MobileNewSmth
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public enum SettingItems
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("Display tail information when posting a new thread!")]
        NewThreadTail,

        /// <summary>
        /// 
        /// </summary>
        [Description("User password!")]
        Password,

        /// <summary>
        /// 
        /// </summary>
        [Description("The top 10 boards' updating interval in minute!")]
        UpdateInterval,

        /// <summary>
        /// 
        /// </summary>
        [Description("The user name!")]
        UserName        
    }
}
