namespace Nzl.Smth
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        MessageEventArgs()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public MessageEventArgs(string msg)
            : this()
        {
            this.Message = msg;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            get;
            set;
        }
         
    }
}
