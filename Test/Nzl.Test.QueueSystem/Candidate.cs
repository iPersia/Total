namespace Nzl.Test.QueueSystem
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class Candidate : IComparable
    {
        /// <summary>
        /// 
        /// </summary>
        private int _index;

        /// <summary>
        /// 
        /// </summary>
        private string _id;

        public Candidate()
        {
        }

        public Candidate(int index, string id)
        {
            this._index = index;
            this._id = id;            
        }

        public int Index
        {
            get
            {
                return this._index;
            }
        }

        public string Id
        {
            get
            {
                return this._id;
            }
        }

        public int CompareTo(object obj)
        {
            return 1;
        }
    }
}
