namespace Nzl.Web.Core.EventArgs
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class ExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="readerType"></param>
        public ExceptionEventArgs(Exception exp, object from)
        {
            this.Exception = exp;
            this.From = from;
        }

        /// <summary>
        /// 
        /// </summary>
        public Exception Exception
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public object From
        {
            get;
            set;
        }
    }
}
