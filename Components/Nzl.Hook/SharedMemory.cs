using System;
using System.Runtime.InteropServices;

namespace Nzl.Hook
{
    public class SharedMemory
    {
        #region Variables.
        /// <summary>
        /// 
        /// </summary>
        private IntPtr m_hSharedMemoryFile = IntPtr.Zero;

        /// <summary>
        /// 
        /// </summary>
        private IntPtr m_pwData = IntPtr.Zero;

        /// <summary>
        /// 
        /// </summary>
        private long m_nMemSize = 0;

        /// <summary>
        /// 
        /// </summary>
        private bool m_bInit = false;

        /// <summary>
        /// 
        /// </summary>
        private string _name = null;

        /// <summary>
        /// 
        /// </summary>
        private int _size = Int32.MinValue;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Size
        {
            get
            {
                return this._size;
            }
        }
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor.
        /// </summary>
        public SharedMemory()
        {
        }
        #endregion

        #region Initialize
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hSharedMemoryFile"></param>
        /// <param name="memSize"></param>
        public bool Init(IntPtr hSharedMemoryFile, string memName, int memSize)
        {            
            if (hSharedMemoryFile != IntPtr.Zero)
            {
                this.m_hSharedMemoryFile = hSharedMemoryFile;
                this.m_nMemSize = memSize;
                this.m_pwData = Win32API.MapViewOfFile(hSharedMemoryFile, Win32API.FILE_MAP_WRITE, 0, 0, (uint)memSize);
                this.m_bInit = this.m_pwData != IntPtr.Zero && this.m_hSharedMemoryFile != IntPtr.Zero;
            }

            if (m_bInit)
            {
                this._name = memName;
                this._size = memSize;
            }
            
            return m_bInit;
        }
        #endregion

        #region override ToString
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.m_hSharedMemoryFile.ToString() + "\n"
                   + this.m_pwData + "\n"
                   + this.m_nMemSize + "\n";
        }
        #endregion

        #region Close
        /// <summary>
        /// 关闭共享内存
        /// </summary>
        public void Close()
        {
            if (m_bInit)
            {                
                if (m_pwData != IntPtr.Zero)
                {
                    if (Win32API.UnmapViewOfFile(m_pwData))
                    {
                        m_pwData = IntPtr.Zero;
                    }
                }

                if (m_hSharedMemoryFile != IntPtr.Zero)
                {
                    if (Win32API.CloseHandle(m_hSharedMemoryFile))
                    {
                        m_hSharedMemoryFile = IntPtr.Zero;
                    }
                }
            }
        }
        #endregion

        #region Unmapping
        /// <summary>
        /// 
        /// </summary>
        public void UnMapping()
        {
            if (m_bInit && m_pwData != IntPtr.Zero)
            {
                if (Win32API.UnmapViewOfFile(m_pwData))
                {
                    m_pwData = IntPtr.Zero;
                }
            }
        }
        #endregion

        #region Read & Write
        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="bytData">数据</param>
        /// <param name="lngAddr">起始地址</param>
        /// <param name="lngSize">个数</param>
        /// <returns></returns>
        public int Read(ref char[] bytData, int lngAddr, int lngSize)
        {
            if (lngAddr + lngSize > m_nMemSize)
            {
                return 2; //超出数据区
            }

            if (m_bInit)
            {
                Marshal.Copy(m_pwData, bytData, lngAddr, lngSize);
            }
            else
            {
                return 1; //共享内存未初始化
            }

            return 0;     //读成功
        }

        /// <summary>
        /// 写数据
        /// </summary>
        /// <param name="bytData">数据</param>
        /// <param name="lngAddr">起始地址</param>
        /// <param name="lngSize">个数</param>
        /// <returns></returns>
        public int Write(char[] bytData, int lngAddr, int lngSize)
        {
            if (lngAddr + lngSize > m_nMemSize)
            {
                return 2; //超出数据区
            }

            if (m_bInit)
            {
                Marshal.Copy(bytData, lngAddr, m_pwData, lngSize);
            }
            else
            {
                return 1; //共享内存未初始化
            }

            return 0;     //写成功
        }
        #endregion
    }
}
