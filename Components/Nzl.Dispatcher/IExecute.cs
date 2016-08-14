namespace Nzl.Dispatcher
{
    using System;

    /// <summary>
    /// The work item interface.
    /// </summary>
    public interface IExecute
    {
        /// <summary>
        /// The execute method.
        /// </summary>
        /// <returns>A boolean flag indicates whether the operation is executed successfully!</returns>
        bool Execute();
    }
}