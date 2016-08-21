namespace Nzl.Smth.Common
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
        public bool IsNewMail
        {
            get;
            set;
        }
    }
}
