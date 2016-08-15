using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Nzl.Hook;
using Nzl.Utils;

namespace Nzl.Test.Hook
{
    public partial class UserActivitySupervisorClientForm : Form
    {
        #region Variables.
        /// <summary>
        /// 
        /// </summary>
        private bool _closeWindow = false;

        /// <summary>
        /// The text line height of the RichTextBox, default to 16.
        /// </summary>
        private int _txtCharWidth = 16;

        /// <summary>
        /// The text line height of the RichTextBox, default to 16.
        /// </summary>        
        private int _txtLineHeight = 16;

        /// <summary>
        /// The d-value of width between form and RichTextBox, default to 26.
        /// </summary>
        private int _dValueOfWidthBetFormAndRichTextBox = 6;

        /// <summary>
        /// The d-value of height between form and RichTextBox, default to 26.
        /// </summary>
        private int _dValueOfHeightBetFormAndRichTextBox = 26;

        /// <summary>
        /// 
        /// </summary>
        private string _uasWindowName = "User Activity Supervisor";

        /// <summary>
        /// Current process id.
        /// </summary>
        private uint _currentProcessID;

        /// <summary>
        /// The user activity logger server handler.
        /// </summary>
        private IntPtr _uasServerHandler = IntPtr.Zero;

        /// <summary>
        /// The user activity logger server process id.
        /// </summary>
        private uint _uasServerProcessID;

        /// <summary>
        /// User activity log file name.
        /// </summary>
        private string _logFileName = @"UALog.log";

        /// <summary>
        /// Show config.
        /// </summary>
        private bool _showConfig = false;

        /// <summary>
        /// Sort type.
        /// </summary>
        private SortType _sortType = SortType.None;

        /// <summary>
        /// Order style.
        /// </summary>
        private OrderStyle _orderStyle = OrderStyle.Ascending;

        /// <summary>
        /// Interval of updating user activity, defualt to 1s.
        /// </summary>
        private int _uiTimerInterval = 1000;

        /// <summary>
        /// Interval of logging, default to 30 minute.
        /// </summary>
        private int _logTimerInterval = 30 * 60 * 1000;

        /// <summary>
        /// Interval of logging, default to 5s.
        /// </summary>
        private int _checkServerTimerInterval = 5 * 1000;

        /// <summary>
        /// 
        /// </summary>
        private string _marginString = "   ";

        /// <summary>
        /// 
        /// </summary>
        private int _lineCharCount = 100;

        /// <summary>
        /// 
        /// </summary>
        private int _minPersLen = 5;

        /// <summary>
        /// 
        /// </summary>
        private int _minTimeLen = 21;

        /// <summary>
        /// The shared memory.
        /// </summary>        
        private SharedMemory _sharedMemory;

        /// <summary>
        /// 
        /// </summary>
        private bool _isTopMost = true;

        /// <summary>
        /// 
        /// </summary>
        private string _communicationType = "SharedMemory";

        /// <summary>
        /// 
        /// </summary>
        private string _serverIP = null;

        /// <summary>
        /// 
        /// </summary>
        private int _serverPort = -1;
        #endregion

        #region Ctor & Form Event
        /// <summary>
        /// 
        /// </summary>
        public UserActivitySupervisorClientForm()
        {
            try
            {
                InitializeComponent();
                Initialize();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Accur: " + e.Message);
            }
        }

        /// <summary>
        /// Initialize.
        /// </summary>
        private void Initialize()
        {            
            this.ReadAppConfig();
            this.ApplyAppConfig();
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
                ////Get config of log file name.
                if (false == string.IsNullOrEmpty(GetConfigValue("logFileName")))
                {
                    this._logFileName = GetConfigValue("logFileName");
                }

                ////Get config of interval of ui updating.
                if (false == string.IsNullOrEmpty(GetConfigValue("updateInterval")))
                {
                    this._uiTimerInterval = System.Convert.ToInt32(GetConfigValue("updateInterval")) * 1000;
                }

                ////Get config of interval of logging.
                if (false == string.IsNullOrEmpty(GetConfigValue("logInterval")))
                {
                    this._logTimerInterval = System.Convert.ToInt32(GetConfigValue("logInterval")) * 60 * 1000;
                }

                ////Get config of interval of checking user activity supervisor server.
                if (false == string.IsNullOrEmpty(GetConfigValue("checkServerInterval")))
                {
                    this._checkServerTimerInterval = System.Convert.ToInt32(GetConfigValue("checkServerInterval")) * 1000;
                }

                ////Get config of char count per line.
                if (false == string.IsNullOrEmpty(GetConfigValue("lineCharCount")))
                {
                    this._lineCharCount = System.Convert.ToInt32(GetConfigValue("lineCharCount"));
                }

                ////Get config of margin string.
                if (false == string.IsNullOrEmpty(GetConfigValue("marginString")))
                {
                    this._marginString = GetConfigValue("marginString");
                }

                ////Get config of sort type.
                if (false == string.IsNullOrEmpty(GetConfigValue("sortType")))
                {
                    switch (GetConfigValue("sortType"))
                    {
                        case "ProcessName":
                            {
                                this._sortType = SortType.ProcessName;
                            }
                            break;
                        case "Time":
                            {
                                this._sortType = SortType.Time;
                            }
                            break;
                        case "None":
                        default:
                            {
                                this._sortType = SortType.None;
                            }
                            break;
                    }
                }

                ////Get config of order style.
                if (false == string.IsNullOrEmpty(GetConfigValue("orderStyle")))
                {
                    switch (GetConfigValue("orderStyle"))
                    {
                        case "Descending":
                            {
                                this._orderStyle = OrderStyle.Descending;
                            }
                            break;
                        case "Ascending":
                        default:
                            {
                                this._orderStyle = OrderStyle.Ascending;
                            }
                            break;
                    }
                }

                ////Get config of whether show the configuaration.
                this._showConfig = GetConfigValue("showConfig") == "Yes";

                ////Get config of whether wether window is top-most.
                this._isTopMost = GetConfigValue("isTopMost") == "Yes";

                ////Get communication type;
                this._communicationType = GetConfigValue("communicationType") == null ? this._communicationType : GetConfigValue("communicationType");
                if (this._communicationType == "Socket")
                {
                    this._serverIP = GetConfigValue("serverIP");
                    this._serverPort = Convert.ToInt32(GetConfigValue("serverPort"));
                }
            }
            catch { }
        }

        /// <summary>
        /// Apply app config.
        /// </summary>
        private void ApplyAppConfig()
        {
            this.uiTimer.Stop();
            this.logTimer.Stop();
            this.checkServerTimer.Stop();
            this.uiTimer.Interval = this._uiTimerInterval;
            this.logTimer.Interval = this._logTimerInterval;
            this.checkServerTimer.Interval = this._checkServerTimerInterval;
            this.uiTimer.Start();
            this.logTimer.Start();
            this.checkServerTimer.Start();

            ////Top-most.
            this.TopMost = this._isTopMost;

            ////Change form width.
            this.Width = (this._lineCharCount + 1) * this._txtCharWidth
                       + 1 //Starting postion.
                       + this._dValueOfWidthBetFormAndRichTextBox;

            CommunicationTypeChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CommunicationTypeChanged()
        {
            this._uasServerHandler = IntPtr.Zero;
            if (this._communicationType == "SharedMemory")
            {
                this._uasServerHandler = Win32API.FindWindow(null, this._uasWindowName);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserActivitySupervisorClientForm_Shown(object sender, EventArgs e)
        {
            Version ver = System.Environment.OSVersion.Version;
            if (ver.Major > 5)
            {
                Win32API.ChangeWindowMessageFilter(Win32API.WM_COPYDATA, 1);
            }

            this.txtBox.AppendText("  \n  \n  ");
            System.Drawing.Point ptLine1 = this.txtBox.GetPositionFromCharIndex(this.txtBox.GetFirstCharIndexFromLine(0));
            System.Drawing.Point ptLine2 = this.txtBox.GetPositionFromCharIndex(this.txtBox.GetFirstCharIndexFromLine(1));
            System.Drawing.Point ptChar1 = this.txtBox.GetPositionFromCharIndex(0);
            System.Drawing.Point ptChar2 = this.txtBox.GetPositionFromCharIndex(1);
            this._txtCharWidth = (ptChar2.X - ptChar1.X);
            this._txtLineHeight = (ptLine2.Y - ptLine1.Y);
            this._dValueOfWidthBetFormAndRichTextBox = this.Width - this.txtBox.Width;
            this._dValueOfHeightBetFormAndRichTextBox = this.Height - this.txtBox.Height;

            this.Width = (this._lineCharCount + 1) * this._txtCharWidth
                       + 1 //Starting postion.
                       + this._dValueOfWidthBetFormAndRichTextBox;

            Win32API.GetWindowThreadProcessId(this.Handle, out this._currentProcessID);
            this._uasServerHandler = Win32API.FindWindow(null, this._uasWindowName);
            if (this._uasServerHandler != IntPtr.Zero)
            {
                Win32API.SendMessage(this._uasServerHandler, Win32API.WM_GET_SHARED_MEMORY_INFO, this.Handle, IntPtr.Zero);
                Win32API.GetWindowThreadProcessId(this._uasServerHandler, out this._uasServerProcessID);
                this.uiTimer.Start();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserActivitySupervisorClientForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                ShowWindow(false);
            }
        }

        private void UserActivitySupervisorClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._closeWindow == false)
            {
                e.Cancel = true;
                ShowWindow(false);
            }
            else
            {
                this.Log();
            }
        }
        #endregion

        #region Override DefWndProc
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32API.WM_COPYDATA:
                    {
                        string logInfor = GetStringByMsg(m);
                        if (logInfor != null)
                        {
                            string smName = RegexUtil.GetMatch(@"<SharedMemoryName>(?'SharedMemoryName'.+)</SharedMemoryName>", logInfor, "SharedMemoryName");
                            string smSize = RegexUtil.GetMatch(@"<SharedMemorySize>(?'SharedMemorySize'\d+)</SharedMemorySize>", logInfor, "SharedMemorySize");
                            if (smName != null && smSize != null)
                            {
                                if ((this._sharedMemory == null) || this._sharedMemory.Name != smName || this._sharedMemory.Size != Convert.ToInt32(smSize))
                                {
                                    if (this._sharedMemory != null)
                                    {
                                        this._sharedMemory.UnMapping();
                                        this._sharedMemory = null;
                                    }

                                    this._sharedMemory = SharedMemoryFactory.OpenSharedMemory(Win32API.FILE_MAP_ALL_ACCESS, smName, Convert.ToInt32(smSize));
                                }
                            }

                            m.Result = (IntPtr)int.MaxValue;
                        }
                        else
                        {
                            m.Result = (IntPtr)(int.MinValue);
                        }
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
        /// <param name="msg"></param>
        /// <returns></returns>
        private string GetStringByMsg(Message msg)
        {
            try
            {
                COPYDATASTRUCT mystr = (COPYDATASTRUCT)msg.GetLParam(typeof(COPYDATASTRUCT));
                if (mystr.lpData != null)
                {
                    return mystr.lpData.Substring(0, mystr.cbData).TrimEnd('\n');
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Timers' Tick event handler.
        /// <summary>
        /// 
        /// </summary>
        private byte[] _rcvServerBytes = new byte[10240];

        private long _count = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this._communicationType == "SharedMemory")
                {
                    HookMessage smm = HookMessageExchanger.Read(this._sharedMemory);
                    if (smm != null)
                    {
                        if (smm.Type == HookMessageType.Data)
                        {
                            Update(smm.Message);
                        }
                        else
                        {
                            UpdateText("*".PadLeft(this._lineCharCount, '*') + "\n" + smm.Message + "\n" + "*".PadLeft(this._lineCharCount, '*'));
                        }
                    }                    
                }
                else
                {
                    this.bwRequestMessage = new System.ComponentModel.BackgroundWorker();
                    this.bwRequestMessage.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwRequestMessage_DoWork);
                    this.bwRequestMessage.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwRequestMessage_ProgressChanged);
                    this.bwRequestMessage.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwRequestMessage_RunWorkerCompleted);
                    this.bwRequestMessage.RunWorkerAsync(this._count++);
                    /*
                    try
                    {
                        IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(this._serverIP), this._serverPort);
                        Socket sc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        sc.Connect(ipe);
                        int bytes = sc.Receive(this._rcvServerBytes, this._rcvServerBytes.Length, 0);
                        Update(Encoding.UTF8.GetString(this._rcvServerBytes, 0, bytes));
                        sc.Close();
                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine("argumentNullException: {0}", ane);
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("SocketException:{0}", se);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("Exception:{0}", exp);
                    }
                    */
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        private string QueryMessage()
        {
            try
            {
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(this._serverIP), this._serverPort);
                Socket sc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sc.Connect(ipe);
                int bytes = sc.Receive(this._rcvServerBytes, this._rcvServerBytes.Length, 0);
                sc.Close();

                string msg = Encoding.UTF8.GetString(this._rcvServerBytes, 0, bytes);
                Update(msg);
                return msg;
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("argumentNullException: {0}", ane);
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException:{0}", se);
            }
            catch (Exception exp)
            {
                Console.WriteLine("Exception:{0}", exp);
            }

            return null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logTimer_Tick(object sender, EventArgs e)
        {
            this.Log();
        }

        /// <summary>
        /// 
        /// </summary>u
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkServerTimer_Tick(object sender, EventArgs e)
        {
            if (this._communicationType == "SharedMemory")
            {
                this._uasServerHandler = Win32API.FindWindow(null, _uasWindowName);
                if (this._uasServerHandler == IntPtr.Zero)
                {
                    this._uasServerProcessID = 0;
                    this.UpdateText("Cannot find the user activity supervisor server!\n");
                    this._sharedMemory = null;
                    this.uiTimer.Stop();
                }
                else
                {
                    Win32API.SendMessage(this._uasServerHandler, Win32API.WM_GET_SHARED_MEMORY_INFO, this.Handle, IntPtr.Zero);
                    Win32API.GetWindowThreadProcessId(this._uasServerHandler, out this._uasServerProcessID);
                    this.uiTimer.Start();
                }
            }
            else
            { 
            }
        }
        #endregion

        #region Update log infor.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private DateTime GetDateTimeFromXmlNode(XmlNode node)
        {
            return new DateTime(System.Convert.ToInt32(node.Attributes["Year"].Value),
                                System.Convert.ToInt32(node.Attributes["Month"].Value),
                                System.Convert.ToInt32(node.Attributes["Day"].Value),
                                System.Convert.ToInt32(node.Attributes["Hour"].Value),
                                System.Convert.ToInt32(node.Attributes["Minute"].Value),
                                System.Convert.ToInt32(node.Attributes["Second"].Value),
                                System.Convert.ToInt32(node.Attributes["Millisecond"].Value));
        }

        /// <summary>
        /// Format strings
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="lens"></param>
        /// <param name="lrAlg"></param>
        /// <returns></returns>
        public string FormatStrings(string[] strs, int[] lens, LeftRightAlignment[] lrAlgs)
        {
            return FormatStrings(strs, lens, lrAlgs, ' ');
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="lens"></param>
        /// <param name="lrAlg"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public string FormatStrings(string[] strs, int[] lens, LeftRightAlignment[] lrAlgs, char replacement)
        {
            if (strs.Length == lens.Length && strs.Length == lrAlgs.Length)
            {
                string result = string.Empty;
                for (int i = 0; i < strs.Length; i++)
                {
                    if (lrAlgs[i] == LeftRightAlignment.Left)
                    {
                        result += strs[i].PadRight(lens[i]-NumberOfChineseCharacters(strs[i]), replacement);
                    }
                    else
                    {
                        result += strs[i].PadLeft(lens[i] - NumberOfChineseCharacters(strs[i]), replacement);
                    }
                }

                return result;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void Update(string logXml)
        {
            try
            {
                string result = string.Empty;
                string proInfor = "Supervised Process Information";
                string sumInfor = "Summary Information";
                string subMargin = this._marginString.Substring(0, this._marginString.Length / 2);
                string infor = string.Empty;
                ////Starting with *
                infor += "*".PadLeft(this._lineCharCount, '*') + "\n";

                ////Total time information.
                infor += this.GetFormattedTotalTimeInfor(logXml);

                ////Show supervised process information.                
                infor += FormatStrings(new string[] { subMargin, "", proInfor, "", subMargin },
                                       new int[] { subMargin.Length, 
                                                   (this._lineCharCount - subMargin.Length * 2 - proInfor.Length) / 2, 
                                                   proInfor.Length, 
                                                   this._lineCharCount - subMargin.Length * 2 - (this._lineCharCount - subMargin.Length * 2 - proInfor.Length) / 2 - proInfor.Length, 
                                                   subMargin.Length },
                                       new LeftRightAlignment[] { LeftRightAlignment.Left, LeftRightAlignment.Left, LeftRightAlignment.Left, LeftRightAlignment.Left, LeftRightAlignment.Left },
                                       '-') + "\n";
                infor += this.GetFormattedProcessTimeInfor(logXml);

                ////Process summary information.                
                infor += FormatStrings(new string[] { subMargin, "", sumInfor, "", subMargin },
                                       new int[] { subMargin.Length, 
                                                   (this._lineCharCount - subMargin.Length * 2 - sumInfor.Length) / 2, 
                                                   sumInfor.Length, 
                                                   this._lineCharCount - subMargin.Length * 2 - (this._lineCharCount - subMargin.Length * 2 - sumInfor.Length) / 2 - sumInfor.Length, 
                                                   subMargin.Length, },
                                       new LeftRightAlignment[] { LeftRightAlignment.Left, LeftRightAlignment.Left, LeftRightAlignment.Left, LeftRightAlignment.Left, LeftRightAlignment.Left },
                                       '-') + "\n";
                infor += this.GetFormattedSummaryInfor(logXml);

                ////Show configuaration.
                if (this._showConfig)
                {
                    string conInfor = "Configuarations";
                    infor += FormatStrings(new string[] { subMargin, "", conInfor, "", subMargin },
                                           new int[] { subMargin.Length, 
                                                       (this._lineCharCount - subMargin.Length * 2 - conInfor.Length) / 2, 
                                                       conInfor.Length, 
                                                       this._lineCharCount - subMargin.Length * 2 - (this._lineCharCount - subMargin.Length * 2 - conInfor.Length) / 2 - conInfor.Length, 
                                                       subMargin.Length },
                                           new LeftRightAlignment[] { LeftRightAlignment.Left, LeftRightAlignment.Left, LeftRightAlignment.Left, LeftRightAlignment.Left, LeftRightAlignment.Left },
                                           '-') + "\n";
                    infor += this.GetFormattedConfigInfor();
                }


                ////Ending with *.
                infor += "*".PadLeft(this._lineCharCount, '*');
                this.UpdateText(infor);
            }
            catch (Exception e)
            {
                this.UpdateText(e.Message);
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logXml"></param>
        /// <returns></returns>
        private string GetFormattedTotalTimeInfor(string logXml)
        {
            string result = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(logXml);
            XmlNode root = xmlDoc.FirstChild
                                 .NextSibling
                //.SelectSingleNode("Supervisor")
                                 .SelectSingleNode("UserActivitySupervisor")
                                 .SelectSingleNode("Summary");
            if (root != null)
            {
                string datetimeFormatStr = "yyyy-MM-dd HH:mm:ss";
                XmlNode node = root.SelectSingleNode("StartTime");
                if (node != null)
                {
                    DateTime startTime = this.GetDateTimeFromXmlNode(node);
                    result += startTime.ToString(datetimeFormatStr).PadLeft((this._lineCharCount + startTime.ToString(datetimeFormatStr).Length) / 2) + "\n";
                }

                node = root.SelectSingleNode("CurrentTime");
                if (node != null)
                {
                    DateTime currentTime = this.GetDateTimeFromXmlNode(node);
                    result += currentTime.ToString(datetimeFormatStr).PadLeft((this._lineCharCount + currentTime.ToString(datetimeFormatStr).Length) / 2) + "\n";
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logXml"></param>
        /// <returns></returns>
        private Dictionary<string, double> GetProcessTimes(string logXml)
        {
            Dictionary<string, double> dic = new Dictionary<string, double>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(logXml);
            XmlNode root = xmlDoc.FirstChild
                                 .NextSibling
                                 .SelectSingleNode("UserActivitySupervisor");
            if (root != null)
            {
                XmlNode xe = root.SelectSingleNode("Processes");
                if (xe != null)
                {
                    if (xe.HasChildNodes)
                    {
                        xe = xe.SelectSingleNode("Process");
                        while (xe != null)
                        {
                            dic.Add(xe.Attributes["Name"].Value.ToString(), System.Convert.ToDouble(xe.Attributes["Time"].Value.ToString()));
                            xe = xe.NextSibling;
                        }
                    }

                    ////Sort the dictionary.
                    if (dic.Count > 0 && this._sortType != SortType.None)
                    {
                        List<KeyValuePair<string, double>> myList = new List<KeyValuePair<string, double>>(dic);
                        switch (this._sortType)
                        {
                            case SortType.ProcessName:
                                {
                                    myList.Sort(delegate(KeyValuePair<string, double> s1, KeyValuePair<string, double> s2)
                                    {
                                        if (this._orderStyle == OrderStyle.Ascending)
                                        {
                                            return s1.Key.CompareTo(s2.Key);
                                        }
                                        else
                                        {
                                            return s2.Key.CompareTo(s1.Key);
                                        }
                                    });
                                }
                                break;
                            case SortType.Time:
                                {
                                    myList.Sort(delegate(KeyValuePair<string, double> s1, KeyValuePair<string, double> s2)
                                    {
                                        if (this._orderStyle == OrderStyle.Ascending)
                                        {
                                            return s1.Value.CompareTo(s2.Value);
                                        }
                                        else
                                        {
                                            return s2.Value.CompareTo(s1.Value);
                                        }
                                    });
                                }
                                break;
                            default:
                                break;
                        }

                        dic.Clear();
                        foreach (KeyValuePair<string, double> pair in myList)
                        {
                            dic.Add(pair.Key, pair.Value);
                        }
                    }
                }
            }

            return dic;
        }

        /// <summary>
        /// Get process summary information.
        /// </summary>
        /// <param name="logXml"></param>
        /// <returns></returns>
        private string GetFormattedProcessTimeInfor(string logXml)
        {
            string result = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(logXml);
            XmlNode root = xmlDoc.FirstChild;
            if (root != null)
            {
                XmlNode node = root.NextSibling
                                   .SelectSingleNode("UserActivitySupervisor")
                                   .SelectSingleNode("Summary")
                                   .SelectSingleNode("TotalSupervisedTime");
                if (node != null)
                {
                    double totalSupervisedTime = System.Convert.ToDouble(node.InnerText);
                    int counter = 1;
                    Dictionary<string, double> dic = this.GetProcessTimes(logXml);
                    if (dic.Count > 0)
                    {
                        int counLen = 4;
                        int persLen = this._minPersLen;
                        int timeLen = this._minTimeLen;
                        int nameLen = this._lineCharCount - this._marginString.Length * 5 - 4 - persLen - timeLen;
                        char replace = '_';

                        result += this.FormatStrings(new string[] { this._marginString, 
                                                                    replace + "No" + replace, 
                                                                    this._marginString, 
                                                                    "Name".PadLeft(nameLen / 2 + 2, replace), 
                                                                    this._marginString, 
                                                                    "%".PadLeft(persLen / 2 + 1, replace), 
                                                                    this._marginString, 
                                                                    "Time".PadLeft(timeLen / 2 + 2, replace),
                                                                    this._marginString},
                                                     new int[] { this._marginString.Length,
                                                                 counLen, 
                                                                 this._marginString.Length,
                                                                 nameLen, 
                                                                 this._marginString.Length,
                                                                 persLen, 
                                                                 this._marginString.Length,
                                                                 timeLen,
                                                                 this._marginString.Length},
                                                    new LeftRightAlignment[] { LeftRightAlignment.Left, 
                                                                               LeftRightAlignment.Left,
                                                                               LeftRightAlignment.Left,
                                                                               LeftRightAlignment.Left,
                                                                               LeftRightAlignment.Left,
                                                                               LeftRightAlignment.Left,
                                                                               LeftRightAlignment.Left,
                                                                               LeftRightAlignment.Left,
                                                                               LeftRightAlignment.Left},
                                                   replace)
                                                   + "\n";
                        foreach (KeyValuePair<string, double> kp in dic)
                        {
                            result += this.FormatStrings(new string[] { this._marginString, 
                                                                        "[" + counter++.ToString().PadLeft(2) + "]", 
                                                                        this._marginString, 
                                                                        (kp.Key.Length > nameLen ? kp.Key.Substring(0, nameLen-2)+".." : kp.Key) , 
                                                                        this._marginString, 
                                                                        this.FormatProcessTimePersentage(kp.Value, totalSupervisedTime, 0), 
                                                                        this._marginString, 
                                                                        this.FormatTime(kp.Value),
                                                                        this._marginString},
                                                         new int[] { this._marginString.Length,
                                                                     counLen, 
                                                                     this._marginString.Length,
                                                                     nameLen, 
                                                                     this._marginString.Length,
                                                                     persLen, 
                                                                     this._marginString.Length,
                                                                     timeLen,
                                                                     this._marginString.Length},
                                                        new LeftRightAlignment[] { LeftRightAlignment.Left, 
                                                                                   LeftRightAlignment.Left,
                                                                                   LeftRightAlignment.Left,
                                                                                   LeftRightAlignment.Left,
                                                                                   LeftRightAlignment.Left,
                                                                                   LeftRightAlignment.Right,
                                                                                   LeftRightAlignment.Left,
                                                                                   LeftRightAlignment.Right,
                                                                                   LeftRightAlignment.Left}
                                                        ) + "\n";
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logXml"></param>
        /// <returns></returns>
        private string GetFormattedSummaryInfor(string logXml)
        {
            string result = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(logXml);
            XmlNode root = xmlDoc.FirstChild
                                 .NextSibling
                                 .SelectSingleNode("UserActivitySupervisor")
                                 .SelectSingleNode("Summary");
            if (root != null)
            {
                double totalSupervisedTime = 0.0;
                DateTime startTime = DateTime.Now;
                DateTime currentTime = DateTime.Now;
                int supervisedProccessCount = 0;
                int registedProcessCount = 0;
                XmlNode node = root.SelectSingleNode("StartTime");
                if (node != null)
                {
                    startTime = this.GetDateTimeFromXmlNode(node);
                }

                node = root.SelectSingleNode("CurrentTime");
                if (node != null)
                {
                    currentTime = this.GetDateTimeFromXmlNode(node);
                }

                node = root.SelectSingleNode("TotalSupervisedTime");
                if (node != null)
                {
                    totalSupervisedTime = System.Convert.ToDouble(node.InnerText);
                }

                node = root.SelectSingleNode("SupervisedProccessCount");
                if (node != null)
                {
                    supervisedProccessCount = System.Convert.ToInt32(node.InnerText);
                }

                node = root.SelectSingleNode("RegistedProcessCount");
                if (node != null)
                {
                    registedProcessCount = System.Convert.ToInt32(node.InnerText);
                }

                double dtActual = (currentTime - startTime).TotalMilliseconds;
                result += this._marginString + this.FormatStrings(new string[] { "Process count", supervisedProccessCount.ToString().PadLeft(2) + "/" + registedProcessCount.ToString().PadLeft(2) },
                                                                  new int[] { (this._lineCharCount - this._marginString.Length * 2) / 2, this._lineCharCount - this._marginString.Length * 2 - (this._lineCharCount - this._marginString.Length * 2) / 2 },
                                                                  new LeftRightAlignment[] { LeftRightAlignment.Left, LeftRightAlignment.Right }) + this._marginString + "\n";
                result += this._marginString + this.FormatInforTime("Total time interval", dtActual, this._lineCharCount - this._marginString.Length * 2) + this._marginString + "\n";
                result += this._marginString + this.FormatInforTime("Total time logged", totalSupervisedTime, this._lineCharCount - this._marginString.Length * 2) + this._marginString + "\n";
                result += this._marginString + this.FormatInforTime("Total time d-value", dtActual - totalSupervisedTime, this._lineCharCount - this._marginString.Length * 2) + this._marginString + "\n";
            }

            return result;
        }

        /// <summary>
        /// Get configuaration string.
        /// </summary>
        /// <returns></returns>
        private string GetFormattedConfigInfor()
        {
            string result = string.Empty;

            ////Update interval.
            result += this._marginString
                    + FormatStrings(new string[] { "Update interval:", (this._uiTimerInterval / 1000).ToString() + "s" },
                                    new int[] { (this._lineCharCount - this._marginString.Length * 2) / 2, this._lineCharCount - this._marginString.Length * 2 - (this._lineCharCount - this._marginString.Length * 2) / 2 },
                                    new LeftRightAlignment[] { LeftRightAlignment.Left, LeftRightAlignment.Right }) + "\n";

            ////Check server interval
            result += this._marginString
                    + FormatStrings(new string[] { "Check server interval:", (this._checkServerTimerInterval / 1000).ToString() + "s" },
                                    new int[] { (this._lineCharCount - this._marginString.Length * 2) / 2, this._lineCharCount - this._marginString.Length * 2 - (this._lineCharCount - this._marginString.Length * 2) / 2 },
                                    new LeftRightAlignment[] { LeftRightAlignment.Left, LeftRightAlignment.Right }) + "\n";

            ////Log interval
            result += this._marginString
                    + FormatStrings(new string[] { "Log interval:", (this._logTimerInterval / 60000).ToString() + "m" },
                                    new int[] { (this._lineCharCount - this._marginString.Length * 2) / 2, this._lineCharCount - this._marginString.Length * 2 - (this._lineCharCount - this._marginString.Length * 2) / 2 },
                                    new LeftRightAlignment[] { LeftRightAlignment.Left, LeftRightAlignment.Right }) + "\n";

            ////Char count of per-line.
            result += this._marginString
                    + FormatStrings(new string[] { "Char count of per-line:", this._lineCharCount.ToString() },
                                    new int[] { (this._lineCharCount - this._marginString.Length * 2) / 2, this._lineCharCount - this._marginString.Length * 2 - (this._lineCharCount - this._marginString.Length * 2) / 2 },
                                    new LeftRightAlignment[] { LeftRightAlignment.Left, LeftRightAlignment.Right }) + "\n";

            ////Margin string.
            result += this._marginString
                    + FormatStrings(new string[] { "Margin string:", "\"" + this._marginString + "\"" },
                                    new int[] { (this._lineCharCount - this._marginString.Length * 2) / 2, this._lineCharCount - this._marginString.Length * 2 - (this._lineCharCount - this._marginString.Length * 2) / 2 },
                                    new LeftRightAlignment[] { LeftRightAlignment.Left, LeftRightAlignment.Right }) + "\n";

            ////Log file name.
            result += this._marginString
                    + FormatStrings(new string[] { "Log file name:", this._logFileName },
                                    new int[] { (this._lineCharCount - this._marginString.Length * 2) / 2, this._lineCharCount - this._marginString.Length * 2 - (this._lineCharCount - this._marginString.Length * 2) / 2 },
                                    new LeftRightAlignment[] { LeftRightAlignment.Left, LeftRightAlignment.Right }) + "\n";

            ////Sort type.
            result += this._marginString
                    + FormatStrings(new string[] { "Sort type:", MiscUtil.GetEnumDescription(this._sortType) },
                                    new int[] { (this._lineCharCount - this._marginString.Length * 2) / 2, this._lineCharCount - this._marginString.Length * 2 - (this._lineCharCount - this._marginString.Length * 2) / 2 },
                                    new LeftRightAlignment[] { LeftRightAlignment.Left, LeftRightAlignment.Right }) + "\n";

            ////Order style.
            if (this._sortType != SortType.None)
            {
                result += this._marginString + FormatStrings(new string[] { "Order styple:", MiscUtil.GetEnumDescription(this._orderStyle) },
                                                             new int[] { (this._lineCharCount - this._marginString.Length * 2) / 2, this._lineCharCount - this._marginString.Length * 2 - (this._lineCharCount - this._marginString.Length * 2) / 2 },
                                                             new LeftRightAlignment[] { LeftRightAlignment.Left, LeftRightAlignment.Right }) + "\n";
            }

            ////Order style.
            if (this._sharedMemory != null)
            {
                result += this._marginString + FormatStrings(new string[] { "Shared memory information:" },
                                                             new int[] { this._lineCharCount - this._marginString.Length },
                                                             new LeftRightAlignment[] { LeftRightAlignment.Left }) + "\n";

                int namesizeLen = 15;
                result += this._marginString
                        + this._marginString
                        + FormatStrings(new string[] { "Name:", this._sharedMemory.Name },
                                        new int[] { namesizeLen, this._lineCharCount - this._marginString.Length * 3 - namesizeLen },
                                        new LeftRightAlignment[] { LeftRightAlignment.Left, LeftRightAlignment.Right }) + "\n";

                result += this._marginString
                        + this._marginString
                        + FormatStrings(new string[] { "Size:", this._sharedMemory.Size.ToString() + "Byte" },
                                        new int[] { namesizeLen, this._lineCharCount - this._marginString.Length * 3 - namesizeLen },
                                        new LeftRightAlignment[] { LeftRightAlignment.Left, LeftRightAlignment.Right }) + "\n";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalmiliseconds"></param>
        /// <returns></returns>
        private string FormatTime(double totalmiliseconds)
        {
            int tms = (int)totalmiliseconds;
            int[] radixs = new int[] { 1000, 60, 60, 24 };
            string[] unit = new string[] { "ms", "s", "m", "h", "d" };
            int[] unitPadCount = new int[] { 3, 2, 2, 2, 2 };
            int counter = 0;
            string result = ((int)(tms % radixs[counter])).ToString().PadLeft(unitPadCount[counter]) + unit[counter];
            tms /= radixs[counter++];
            while (tms > 0 && counter < radixs.Length)
            {
                int tempBin = (int)(tms % radixs[counter]);
                tms /= radixs[counter];
                if (tms > 0 || tempBin > 0)
                {
                    result = tempBin.ToString().PadLeft(unitPadCount[counter]) + unit[counter] + "-" + result;
                }

                counter++;
            }

            return (tms > 0 ? tms.ToString().PadLeft(unitPadCount[counter]) + unit[counter] + "-" : "") + result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="processTime"></param>
        /// <param name="totalLoggedTime"></param>
        /// <param name="charCount"></param>
        /// <returns></returns>
        private string FormatProcessTimePersentage(double processTime, double totalLoggedTime, int charCount)
        {
            return (processTime * 100.0 / totalLoggedTime).ToString("0.0");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        /// <param name="totalmiliseconds"></param>
        /// <returns></returns>
        private string FormatInforTime(string infor, double totalmiliseconds, int charCount)
        {
            return infor.PadRight(charCount - this._minTimeLen)
                   + this.FormatTime(totalmiliseconds).PadLeft(this._minTimeLen);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void UpdateText(string text)
        {
            this.txtBox.Clear();
            this.txtBox.AppendText(text);
            this.UpdateFormSize();
            this.txtBox.SelectionStart = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateFormSize()
        {
            if (this.txtBox != null)
            {
                int rowCount = this.txtBox.GetLineFromCharIndex(this.txtBox.SelectionStart) + 1;
                this.Height = rowCount * this._txtLineHeight
                            + this.txtBox.Margin.Top //+ this.txtBox.Margin.Bottom
                            + 1
                            + this._dValueOfHeightBetFormAndRichTextBox;
            }
        }

        private int NumberOfChineseCharacters(string str)
        {
            int count=0;
            char[] c = str.ToCharArray();
            for (int i = 0; i < c.Length; i++)
                if (c[i] >= 0x4e00 && c[i] <= 0x9fbb)
                    count++;

            return count;                 
        }
        #endregion

        #region Notify envent handler.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowWindow(!this.Visible);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>                
        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this,
                            "Name:\t"
                            + Application.ProductName
                            + "\nVersion:\t" + Application.ProductVersion
                            + "\n\n\tCreate by Nesus\n\tningzhlei@gmail.com",
                            "Information",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiLog_Click(object sender, EventArgs e)
        {
            this.Log();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this._closeWindow = true;
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        private void ShowWindow(bool flag)
        {
            this.Visible = flag;
            this.ShowInTaskbar = flag;
            if (flag)
            {
                this.Activate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Log()
        {
            Nzl.Utils.FileUtil.WriteText(this._logFileName, this.txtBox.Text.TrimEnd('\n') + "\n");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditSupervisorConfig_Click(object sender, EventArgs e)
        {
            if (this._uasServerProcessID != 0)
            {
                System.Diagnostics.Process uasProcess = System.Diagnostics.Process.GetProcessById((int)this._uasServerProcessID);
                if (uasProcess != null)
                {
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.StandardInput.WriteLine("notepad " + uasProcess.MainModule.FileName + ".config"); //10秒后重启（C#中可不好做哦） 
                    p.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditMonitorConfig_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine("notepad " + Application.ExecutablePath + ".config"); //10秒后重启（C#中可不好做哦） 
            p.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiUpdateClientConfig_Click(object sender, EventArgs e)
        {
            try
            {
                this.ReadAppConfig();
                this.ApplyAppConfig();
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiUpdateServerConfig_Click(object sender, EventArgs e)
        {
            if (this._uasServerHandler != IntPtr.Zero)
            {
                Win32API.SendMessage(this._uasServerHandler, Win32API.WM_UPDATE_CONFIG, (IntPtr)this._currentProcessID, IntPtr.Zero);
            }
        }
        #endregion

        #region Private enum.
        private enum SortType
        {
            /// <summary>
            /// No sort.
            /// </summary>
            [Description("Default order.")]
            None = 0,

            /// <summary>
            /// Process name.
            /// </summary>
            [Description("Process name.")]
            ProcessName = 1,

            /// <summary>
            /// Active time.
            /// </summary>
            [Description("Active time.")]
            Time = 2,
        }

        /// <summary>
        /// Sort order enum.
        /// </summary>
        private enum OrderStyle
        {
            /// <summary>
            /// 
            /// </summary>
            [Description("Ascending.")]
            Ascending = 0,

            /// <summary>
            /// 
            /// </summary>
            [Description("Descending.")]
            Descending = 1
        }
        #endregion

        #region BackgroudWorker
        private void bwRequestMessage_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = "?";
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(this._serverIP), this._serverPort);
                Socket sc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sc.Connect(ipe);
                int bytes = sc.Receive(this._rcvServerBytes, this._rcvServerBytes.Length, 0);
                sc.Shutdown(SocketShutdown.Both);
                sc.Close();

                e.Result = "0" + Encoding.UTF8.GetString(this._rcvServerBytes, 0, bytes);
            }
            catch (Exception exp)
            {
                e.Result = "1" + exp.ToString();
            }
        }

        private void bwRequestMessage_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void bwRequestMessage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                string result = e.Result.ToString();
                if (result.Substring(0, 1) == "0")
                {
                    Update(result.Substring(1));
                }
                else
                {
                    UpdateText(result.Substring(1));
                }
            }
        }
        #endregion        
    }
}
