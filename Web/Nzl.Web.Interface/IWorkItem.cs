namespace Nzl.Web.Interface
{
    using System;

    /// <summary>
    /// The work item interface.
    /// </summary>
    public interface IWorkItem
    {
        /// <summary>
        /// The execute method.
        /// </summary>
        /// <returns>A boolean flag indicates whether the operation is executed successfully!</returns>
        bool Execute();
    }
}