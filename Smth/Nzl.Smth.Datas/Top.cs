namespace Nzl.Smth.Datas
{
    using System;

    /// <summary>
    /// The topic class.
    /// </summary>
    public class Top : BaseData
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public Top()
        {

        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public Top(Top top)
        {
            if (top != null)
            {
                this.Index = top.Index;
                this.Sequence = top.Sequence;
                this.Replies = top.Replies;
                this.Title = top.Title;
                this.Uri = top.Uri;
                this.Board = top.Board;
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
        public int Sequence
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
    }
}