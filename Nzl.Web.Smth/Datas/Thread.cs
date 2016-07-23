namespace Nzl.Web.Smth.Datas
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// The thread class.
    /// </summary>
    public class Thread : BaseItem
    {
        /// <summary>
        /// 
        /// </summary>
        private bool _updated = true;

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
                this.User = thread.User;
                this.Url = thread.Url;
                this.ReplyUrl = thread.ReplyUrl;
                this.MailUrl = thread.MailUrl;
                this.TransferUrl = thread.TransferUrl;
                this.QueryType = thread.QueryType;
                this.QueryUrl = thread.QueryUrl;
                this.EditUrl = thread.EditUrl;
                this.DeleteUrl = thread.DeleteUrl;
                this.ImageList = thread.ImageList;                
                this.Tag = thread.Tag;
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
        public IList<Image> IconList
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public IList<Anchor> AnchorList
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
        public string User
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
        /// This indicates the thread content whose html has been trimed！
        /// </summary>
        public string Content
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
        
        /// <summary>
        /// 
        /// </summary>
        public override bool Updated
        {
            get
            {
                return this._updated;
            }

            set
            {
                this._updated = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "[<" + this.ID + ">]{" + this.Content + "}";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Anchor
    {
        /// <summary>
        /// 
        /// </summary>
        public string Text
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
    }
}
