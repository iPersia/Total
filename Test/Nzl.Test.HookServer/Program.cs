using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace Nzl.Test.HookServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (IsAlreadyRuning("P") || IsAlreadyRuning("M"))
            {
                MessageBox.Show("Load failed!\n\nThe user activity supervisor server is already running!",
                                            "Warning",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Stop);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UserActivitySupervisorServerForm());
        }

        static bool IsAlreadyRuning(string methodFlag)
        {
            switch (methodFlag)
            {
                case "P":
                    {
                        return RunningInstance() != null;
                    }
                case "M":
                    {
                        bool isFirst;
                        System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName + Application.ProductVersion, out isFirst);
                        return !isFirst;
                    }
                default:
                    return false;
            }
        }

        static bool IsAlreadyRuning()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (process.ProcessName == current.ProcessName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //查找相同名称的进程
            foreach (Process process in processes)
            {
                //忽略当前进程
                if (process.Id != current.Id)
                {
                    //确认相同进程的程序运行位置是否一样.
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        //Return the other process instance.
                        return process;
                    }
                }
            }
            //No other instance was found, return null.
            return null;
        }
    }
}
