namespace Nzl.Hook
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public static class HookUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetForegroundWindowThreadProcessName()
        {
            try
            {
                UInt32 processID;
                Win32API.GetWindowThreadProcessId(Win32API.GetForegroundWindow(), out processID);
                System.Diagnostics.Process prs = System.Diagnostics.Process.GetProcessById((int)processID);
                if (prs != null)
                {
                    if (prs.MainModule.FileVersionInfo.FileDescription.ToUpper().Contains(".EXE") == false)
                    {
                        return prs.MainModule.FileVersionInfo.FileDescription;
                    }

                    return prs.MainModule.FileVersionInfo.ProductName;//.FileDescription;
                    //return prs.ProcessName;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
