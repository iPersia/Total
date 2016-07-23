namespace Nzl.Hook
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// The process communication type enum.
    /// </summary>
    public enum ProcessCommunicationType
    {
        /// <summary>
        /// Anonymous pipe.
        /// </summary>
        [Description("Anonymous pipe.")]
        AnonymousPipe = 0,

        /// <summary>
        /// Named pipe.
        /// </summary>
        [Description("Named pipe.")]
        NamedPipe = 1,

        /// <summary>
        /// Send message by WM_COPYDATA.
        /// </summary>
        [Description("Send message by Win32 API.")]
        SendMessage = 2,

        /// <summary>
        /// Shared memory.
        /// </summary>
        [Description("Shared memory.")]
        SharedMemory = 3,        
    }
}
