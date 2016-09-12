namespace Nzl.Smth
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class ReplyStatusEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public bool HasNewArrived
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int NewArrivedCount
        {
            get;
            set;
        }
    }
}
