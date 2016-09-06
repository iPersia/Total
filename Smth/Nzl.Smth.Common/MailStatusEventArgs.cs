namespace Nzl.Smth
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class MailStatusEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public bool NewArrived
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int NewCount
        {
            get;
            set;
        }
    }
}
