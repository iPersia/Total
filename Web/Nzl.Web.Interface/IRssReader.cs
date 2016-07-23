namespace Nzl.Web.Interface
{
    using System;
    using System.Collections.Generic;
    using Nzl.Web.Core;
    using Nzl.Web.Core.EventArgs;

    /// <summary>
    /// 
    /// </summary>
    public interface IRssReader
    {
        /// <summary>
        /// A boolean indicates whether the rss reader can be loaded automatically.
        /// </summary>
        bool AutoLoad { get; }

        /// <summary>
        /// The unique id identifying rss reader.
        /// </summary>
        string UniqueID { get; }

        /// <summary>
        /// The rss's vendor.
        /// </summary>
        string Vendor { get; }

        /// <summary>
        /// The rss's uri.
        /// </summary>
        Uri Uri { get; }

        /// <summary>
        /// The guid token in the xml file.
        /// </summary>
        string GuidXmlToken { get; set; }

        /// <summary>
        /// The rss reader's updating interval.
        /// </summary>
        int UpdatingInterval { get; set; }

        /// <summary>
        /// Get the rss item list.
        /// </summary>
        /// <param name="count">The number.</param>
        /// <returns>The rss item list.</returns>
        IList<RssItem> GetLatestRssItems(int count);

        /// <summary>
        /// Get the rss item list.
        /// </summary>
        /// <param name="after">The date time.</param>
        /// <returns>The rss item list.</returns>
        IList<RssItem> GetRssItemsAfter(DateTime after);

        /// <summary>
        /// Start updating.
        /// </summary>
        void Start();

        /// <summary>
        /// Stop updating.
        /// </summary>
        void Stop();

        /// <summary>
        /// New item captured eventhandler.
        /// </summary>
        event EventHandler<NewItemsCapturedEnventArgs> NewItemsCaptured;
    }
}
