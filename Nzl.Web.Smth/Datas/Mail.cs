namespace Nzl.Web.Smth.Datas
{
    using System;

    /// <summary>
    /// Class.
    /// </summary>
    public class Mail
    {
        /// <summary>
        /// 
        /// </summary>
        public Mail()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public Mail(int index, string url, string title, string author, string datetime)
            : this()
        {
            this.IsNew = false;
            this.Index = index;
            this.Url = url;
            this.Title = title;
            this.Author = author;
            this.DateTime = datetime;
        }

        /// <summary>
        /// 
        /// </summary>
        public Mail(int index, string url, string title, string author, string datetime, string content)
            : this(index, url, title, author, datetime)
        {
            this.Content = content;
        }

        /// <summary>
        /// 
        /// </summary>
        public Mail(int index, string url, string title, string author, string datetime, string content, string replyUrl, string transferUrl, string deleteUrl)
            : this(index, url, title, author, datetime)
        {
            this.Content = content;
            this.ReplyUrl = replyUrl;
            this.TransferUrl = transferUrl;
            this.DeleteUrl = deleteUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsNew
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Index
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
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Author
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
        public string Content
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
        public string DeleteUrl
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
    }
}

