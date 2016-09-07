namespace Nzl.Smth.Datas
{
    using System;

    /// <summary>
    /// Class.
    /// </summary>
    public class Reply : BaseData
    {
        /// <summary>
        /// 
        /// </summary>
        public Reply()
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
    }
}