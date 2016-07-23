namespace Nzl.Web.Core.EventArgs
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
     
    /// <summary>
    /// 
    /// </summary>
    public class NewItemsCapturedEnventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="readerType"></param>
        public NewItemsCapturedEnventArgs(IList<RssItem> items)
        {
            this.Items = items;
        }

        /// <summary>
        /// 
        /// </summary>
        public IList<RssItem> Items
        {
            get;
            set;
        }
    }
}
