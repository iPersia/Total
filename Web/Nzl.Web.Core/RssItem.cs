namespace Nzl.Web.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.Serialization;

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class RssItem
    {
        /// <summary>
        /// 
        /// </summary>
        public Uri Uri 
        { 
            get; 
            internal set; 
        }

        /// <summary>
        /// 
        /// </summary>
        public string Guid
        {
            get;
            internal set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateTime
        {
            get;
            internal set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get;
            internal set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Vendor
        {
            get;
            internal set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get;
            internal set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, Image> Images
        {
            get;
            internal set;
        }

        /// <summary>
        /// 
        /// </summary>
        internal RssItem()
        {
        }
    }

    /// <summary>
    /// Rss item factory.
    /// </summary>
    public static class RssItemFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="guid"></param>
        /// <param name="datetime"></param>
        /// <param name="title"></param>
        /// <param name="vendor"></param>
        /// <param name="description"></param>
        /// <param name="images"></param>
        /// <returns></returns>
        public static RssItem Create(Uri uri,
                                     string guid,
                                     DateTime datetime,
                                     string title,
                                     string vendor,
                                     string description,
                                     Dictionary<string, Image> images)
        {
            try
            {
                RssItem item = new RssItem();
                item.Uri = uri;
                item.Guid = guid;
                item.DateTime = datetime;
                item.Title = title;
                item.Vendor = vendor;
                item.Description = description;
                item.Images = images;
                return item;
            }
            catch
            {
                return null;
            }
        }
    }
}
