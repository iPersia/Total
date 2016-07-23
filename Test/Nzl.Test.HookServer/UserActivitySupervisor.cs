namespace Nzl.Test.HookServer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;
    using Nzl.Hook;

    public sealed class UserActivitySupervisor
    {
        #region Variable
        /// <summary>
        /// User activity hook.
        /// </summary>
        private UserActivityHook _uaHook;

        /// <summary>
        /// Last mouse event date time.
        /// </summary>
        private DateTime _lastActivityDateTime = DateTime.Now;

        /// <summary>
        /// Last mouse event date time.
        /// </summary>
        private object _lastActivityDateTimeLocker = new object();

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, double> _dicProcessesTimeSummary = new Dictionary<string, double>();

        /// <summary>
        /// 
        /// </summary>
        private string _lastProcessName = ProcessSet.Instance.Idle;

        /// <summary>
        /// Check idle timer.
        /// </summary>
        private System.Timers.Timer _idleTimer = new System.Timers.Timer();

        /// <summary>
        /// The starting time.
        /// </summary>
        private DateTime _dtStart = DateTime.Now;

        /// <summary>
        /// Long interval threshold in ms.
        /// </summary>
        private double _longIntervalThreshold = 300000.0;

        /// <summary>
        /// Short interval threshold in ms.
        /// </summary>
        private double _shortIntervalThreshold = 2000.0;

        /// <summary>
        /// Total logged time in milisecond.
        /// </summary>
        private double _totalLoggedTime = 0.0;

        /// <summary>
        /// Locker for total logged time.
        /// </summary>
        private object _totalLoggedTimeLocker = new object();

        /// <summary>
        /// 
        /// </summary>
        private List<char> _inputChars = new List<char>();

        /// <summary>
        /// Singleton.
        /// </summary>
        public readonly static UserActivitySupervisor Instance = new UserActivitySupervisor();
        #endregion

        #region UserInputSupervised event
        public event EventHandler<UserInputEventArgs> UserInputSupervised;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string ProcessXmlFileName
        {
            set
            {
                ProcessSet.Instance.ProcessXmlFileName = value;
                this.InitializeSupervisedTimes();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return this._dtStart;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ShortInterval
        {
            get
            {
                return (int)this._shortIntervalThreshold;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int LongInterval
        {
            get
            {
                return (int)this._longIntervalThreshold;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private double TotalSupervisedTime
        {
            get
            {
                return this._totalLoggedTime;
            }

            set
            {
                lock (this._totalLoggedTimeLocker)
                {
                    this._totalLoggedTime = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private DateTime LastActivityTime
        {
            get
            {
                return this._lastActivityDateTime;
            }

            set
            {
                lock (this._lastActivityDateTimeLocker)
                {
                    this._lastActivityDateTime = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string LastProcessName
        {
            get
            {
                return this._lastProcessName;
            }

            set
            {
                lock (this._lastProcessName)
                {
                    this._lastProcessName = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, double> SupervisedTimes
        {
            get
            {
                return this._dicProcessesTimeSummary;
            }
        }
        #endregion

        #region Ctors & Initialize.
        /// <summary>
        /// Ctor.
        /// </summary>
        UserActivitySupervisor()
            : this(true, true)
        { }

        /// </summary>
        /// <param name="logMouse"></param>
        /// <param name="logKeyBoard"></param>
        UserActivitySupervisor(bool logMouse, bool logKeyBoard)
        {
            this._uaHook = new UserActivityHook(logMouse, logKeyBoard);
            this.Initialize();
        }

        /// <summary>
        /// Initialize.
        /// </summary>
        private void Initialize()
        {
            if (this._uaHook != null)
            {
                this._dtStart = DateTime.Now;
                this.InitializeSupervisedTimes();
                this._uaHook.MouseActivity += new EventHandler<MouseExEventArgs>(this._uaHook_MouseActivity);
                this._uaHook.KeyPress += new EventHandler<KeyPressExEventArgs>(this._uaHook_KeyPress);
                this._uaHook.KeyDown += new EventHandler<KeyExEventArgs>(this._uaHook_KeyDown);
                this._uaHook.KeyUp += new EventHandler<KeyExEventArgs>(_uaHook_KeyUp);
                this._idleTimer.Elapsed += new System.Timers.ElapsedEventHandler(_idleTimer_Elapsed);
                this.ReadConfig();
                this.ApplyConfig();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ReadConfig()
        {
            try
            {
                if (false == string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["shortInterval"]))
                {
                    this._shortIntervalThreshold = 1000 * System.Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["shortInterval"]);
                }

                if (false == string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["longInterval"]))
                {
                    this._longIntervalThreshold = 1000 * System.Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["longInterval"]);
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ApplyConfig()
        {
            this._idleTimer.Stop();
            this._idleTimer.Interval = this._longIntervalThreshold;
            this._idleTimer.Start();
        }
        #endregion

        #region Read Config & Apply.
        /// <summary>
        /// Re-config supervisor.
        /// </summary>
        public void ReConfig()
        {
            this.ReadConfig();
            this.ApplyConfig();
        }
        #endregion

        #region Get Xml.
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetXml()
        {
            string xml = "<UserActivitySupervisor>";
            {
                xml += "<Summary>";
                xml += "<StartTime" + this.GetXml(this.StartTime) + "></StartTime>";
                xml += "<CurrentTime></CurrentTime>";
                xml += "<SupervisedProccessCount></SupervisedProccessCount>";
                xml += "<RegistedProcessCount>" + ProcessSet.Instance.Processes.Count + "</RegistedProcessCount>";
                xml += "<TotalSupervisedTime></TotalSupervisedTime>";
                xml += "</Summary>";
                xml += "<Processes>";
                double totalLoggedTime = 0.0;
                int supervisedProccessCount = 0;
                foreach (KeyValuePair<string, string> kp in ProcessSet.Instance.Processes)
                {
                    if (this.SupervisedTimes[kp.Key] > 0.0)
                    {
                        xml += "<Process"
                            + " ID=\"" + kp.Key
                            + "\" Name=\"" + ProcessSet.Instance.Processes[kp.Key]
                            + "\" Time=\"" + this.SupervisedTimes[kp.Key].ToString()
                            + "\"></Process>";

                        totalLoggedTime += this.SupervisedTimes[kp.Key];
                        supervisedProccessCount++;
                    }
                }

                xml += "</Processes>";
                xml += "</UserActivitySupervisor>";
                xml = xml.Replace("<CurrentTime></CurrentTime>", "<CurrentTime" + this.GetXml(DateTime.Now) + "></CurrentTime>");
                xml = xml.Replace("<SupervisedProccessCount></SupervisedProccessCount>", "<SupervisedProccessCount>" + supervisedProccessCount + "</SupervisedProccessCount>");
                xml = xml.Replace("<TotalSupervisedTime></TotalSupervisedTime>", "<TotalSupervisedTime>" + totalLoggedTime + "</TotalSupervisedTime>");
            }

            return xml;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        private string GetXml(DateTime datetime)
        {
            return " Year=\"" + datetime.Year + "\""
                 + " Month=\"" + datetime.Month + "\""
                 + " Day=\"" + datetime.Day + "\""
                 + " Hour=\"" + datetime.Hour + "\""
                 + " Minute=\"" + datetime.Minute + "\""
                 + " Second=\"" + datetime.Second + "\""
                 + " Millisecond=\"" + datetime.Millisecond + "\"";
        }
        #endregion

        #region Time elapsed event handler.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _idleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                DateTime dtNow = DateTime.Now;
                TimeSpan ts = dtNow - this.LastActivityTime;
                if (IntervalIsTooShort(ts))
                {
                    return;
                }

                if (IntervalIsTooLong(ts))
                {
                    UpdateProcessSummaryTime(ProcessSet.Instance.Idle, this.LastProcessName, dtNow);
                    return;
                }
            }
            catch
            {
            }
        }
        #endregion

        #region User Activity Hook Handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _uaHook_MouseActivity(object sender, MouseExEventArgs e)
        {
            //if (e.Button != System.Windows.Forms.MouseButtons.None || e.Delta > 0)
            //{
            Update(UserActivityType.Mouse);
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _uaHook_KeyPress(object sender, KeyPressExEventArgs e)
        {
            Update(UserActivityType.KeyPress);
            if (this.UserInputSupervised != null)
            {
                this._inputChars.Add(e.KeyChar);
                if (this._inputChars.Count > 256)
                {
                    UserInputEventArgs uiEventArgs = new UserInputEventArgs();
                    uiEventArgs.Input = new string(GetChars(this._inputChars));
                    this._inputChars.Clear();                                       
                    this.UserInputSupervised(this, uiEventArgs);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listChars"></param>
        /// <returns></returns>
        private char[] GetChars(List<char> listChars)
        {
            if (listChars == null)
            {
                return null;
            }
            
            char[] tmpChars = new char[this._inputChars.Count];
            this._inputChars.CopyTo(tmpChars);
            this._inputChars.Clear();
            return tmpChars;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _uaHook_KeyDown(object sender, KeyExEventArgs e)
        {
            Update(UserActivityType.KeyDown);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _uaHook_KeyUp(object sender, KeyExEventArgs e)
        {
            Update(UserActivityType.KeyUp);
        }
        #endregion

        #region Update Status
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetForegroundWindowThreadProcessName()
        {
            try
            {
                UInt32 processID;
                Win32API.GetWindowThreadProcessId(Win32API.GetForegroundWindow(), out processID);
                System.Diagnostics.Process prs = System.Diagnostics.Process.GetProcessById((int)processID);
                if (prs != null)
                {
                    if (prs.MainModule.FileVersionInfo.FileDescription.ToUpper().Contains(".EXE")==false)
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

        private string GetActiveProcessName(TimeSpan ts)
        {
            //Short interval.
            if (this.IntervalIsTooShort(ts))
            {
                return null;
            }
            //Long interval.
            else if (this.IntervalIsTooLong(ts))
            {
                return ProcessSet.Instance.Idle;
            }
            //Middle interval.
            else
            {
                return GetForegroundWindowThreadProcessName();
            }
        }

        private void Update(UserActivityType uaType)
        {
            DateTime dtNow = DateTime.Now;
            string processName = this.GetActiveProcessName(dtNow - this.LastActivityTime);
            if (processName != null)
            {
                if (ProcessSet.Instance.Processes.ContainsKey(processName) == false)
                {
                    ProcessSet.Instance.Processes.Add(processName, processName);
                }

                if (this.SupervisedTimes.ContainsKey(processName) == false)
                {
                    this.SupervisedTimes.Add(processName, 0.0);
                }

                UpdateProcessSummaryTime(processName, this.LastProcessName, dtNow);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="seconds"></param>
        private void UpdateProcessSummaryTime(string processName, string lastProcessName, DateTime dtNow)
        {
            TimeSpan ts = dtNow - this.LastActivityTime;
            if (this.LastProcessName == ProcessSet.Instance.Idle)
            {
                this.SupervisedTimes[ProcessSet.Instance.Idle] += ts.TotalMilliseconds;
            }
            else
            {
                this.SupervisedTimes[processName] += ts.TotalMilliseconds / 2.0;
                this.SupervisedTimes[this.LastProcessName] += ts.TotalMilliseconds / 2.0;
            }

            this.TotalSupervisedTime += ts.TotalMilliseconds;
            this.LastProcessName = processName;
            this.LastActivityTime = dtNow;
        }
        #endregion

        #region Private methods.
        /// <summary>
        /// Get a dictionay of processes with which total time is recorded corresponding to the process.
        /// </summary>
        /// <returns>A dictionary.</returns>
        private void InitializeSupervisedTimes()
        {
            foreach (KeyValuePair<string, string> kp in ProcessSet.Instance.Processes)
            {
                if (this.SupervisedTimes.ContainsKey(kp.Key) == false)
                {
                    this.SupervisedTimes.Add(kp.Key, 0.0);
                }
            }
        }

        /// <summary>
        /// Check whether the status can be updated.
        /// </summary>
        /// <returns>A boolean flag indicates whether the status can be updated.</returns>
        private bool IntervalIsTooLong(TimeSpan ts)
        {
            return ts.TotalMilliseconds > _longIntervalThreshold / 2.0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IntervalIsTooShort(TimeSpan ts)
        {
            return ts.TotalMilliseconds < _shortIntervalThreshold;
        }
        #endregion        
    }
}
