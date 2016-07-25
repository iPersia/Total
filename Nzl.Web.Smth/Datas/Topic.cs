namespace Nzl.Web.Smth.Datas
{
    using System;

    /// <summary>
    /// The topic class.
    /// </summary>
    public class Topic : BaseItem
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public Topic()
        {

        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public Topic(Topic topic)
        {
            if (topic != null)
            {
                this.CreateDateTime = topic.CreateDateTime;
                this.CreateID = topic.CreateID;
                this.Index = topic.Index;
                this.TopSeq = topic.TopSeq;
                this.LastThreadDateTime = topic.LastThreadDateTime;
                this.LastThreadID = topic.LastThreadID;
                this.Replies = topic.Replies;
                this.Title = topic.Title;
                this.Uri = topic.Uri;
                this.Board = topic.Board;
            }
        }

        public override string ID
        {
            get
            {
                return this.Board + "-" + this.Index;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Uri
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Board
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Index
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
        public int TopSeq
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Replies
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string CreateDateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string CreateID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string LastThreadDateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string LastThreadID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsTop
        {
            get;
            set;
        }
    }
}