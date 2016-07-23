namespace Nzl.Web.Pub.MobileNewSmth
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// The thread class.
    /// </summary>
    public class Thread
    {
        /// <summary>
        /// 
        /// </summary>
        public Thread()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public Thread(Thread thread)
            : this()
        {
            if (thread != null)
            {
                this.Content = thread.Content;
                this.DateTime = thread.DateTime;
                this.Floor = thread.Floor;
                this.ID = thread.ID;
                this.Url = thread.Url;
                this.ReplyUrl = thread.ReplyUrl;
                this.MailUrl = thread.MailUrl;
                this.TransferUrl = thread.TransferUrl;
                this.QueryType = thread.QueryType;
                this.QueryUrl = thread.QueryUrl;
                this.EditUrl = thread.EditUrl;
                this.DeleteUrl = thread.DeleteUrl;
                this.ImageList = thread.ImageList;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IList<Image> ImageList
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Floor
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Url
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string QueryType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ReplyUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string MailUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string TransferUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string QueryUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string EditUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DeleteUrl
        {
            get;
            set;
        }

        

        /// <summary>
        /// 
        /// </summary>
        public string Content
        {
            get;
            set;
        }
    }
}
