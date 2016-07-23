namespace Nzl.Hook
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public static class SharedMemoryFactory
    {
        /// <summary>
        /// The static shared memory dictionary.
        /// </summary>
        private static Dictionary<string, SharedMemory> _dicSharedMemory = new Dictionary<string, SharedMemory>();

        /// <summary>
        /// Create shared memory with specified name, the size is default to 1MB.
        /// </summary>
        /// <param name="sharedMemoryName">Shared memory name</param>
        /// <returns>Shared memory.</returns>
        public static SharedMemory CreateSharedMemory(string sharedMemoryName)
        {            
            return CreateSharedMemory(sharedMemoryName, 1024000);
        }

        /// <summary>
        /// Create shared memory with specified name and size.
        /// </summary>
        /// <param name="sharedMemoryName">Shared memory name.</param>
        /// <param name="sharedMemorySize">Shared memory size.</param>
        /// <returns>Shared memory.</returns>
        public static SharedMemory CreateSharedMemory(string sharedMemoryName, int sharedMemorySize)
        {            
            IntPtr hSharedMemoryFile = Win32API.CreateFileMapping(Win32API.INVALID_HANDLE_VALUE, IntPtr.Zero, (uint)Win32API.PAGE_READWRITE, 0, (uint)sharedMemorySize, sharedMemoryName);
            SharedMemory sm = new SharedMemory();
            if (sm.Init(hSharedMemoryFile, sharedMemoryName, sharedMemorySize))
            {
                return sm;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sharedMemoryName"></param>
        /// <returns></returns>
        public static SharedMemory OpenSharedMemory(int dwDesiredAccess, string sharedMemoryName, int sharedMemorySize)
        {
            IntPtr hSharedMemoryFile = Win32API.OpenFileMapping(dwDesiredAccess, false, sharedMemoryName);
            SharedMemory sm = new SharedMemory();
            if (sm.Init(hSharedMemoryFile, sharedMemoryName, sharedMemorySize))
            {
                return sm;
            }

            return null;
        }
    }
}
