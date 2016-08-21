namespace Nzl.Smth.Datas
{
    using System;

    /// <summary>
    /// Class.
    /// </summary>
    public class Mail : BaseData
    {
        /// <summary>
        /// 
        /// </summary>
        public Mail()
        {

        }

        public override string ID
        {
            get
            {
                return this.Url;
            }
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

