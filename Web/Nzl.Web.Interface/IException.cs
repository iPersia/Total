namespace Nzl.Web.Interface
{
    using System;
    using Nzl.Web.Core;
    using Nzl.Web.Core.EventArgs;

    /// <summary>
    /// 
    /// </summary>
    public interface IException
    {
        /// <summary>
        /// New exception accured eventhandler.
        /// </summary>
        event EventHandler<ExceptionEventArgs> NewExceptionAccured;
    }
}
