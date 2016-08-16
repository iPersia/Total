namespace Nzl.Test.Hook
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using Nzl.Hook;

    public class UserActivityLogger
    {
        #region Variable
        /// <summary>
        /// User activity hook.
        /// </summary>
        private UserActivityHook _uaHook;

        /// <summary>
        /// User activity log file name.
        /// </summary>
        private string _logFileName;

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
        private Dictionary<string, double> _dicProcessesTimeSummary;

        /// <summary>
        /// 
        /// </summary>
        private string _lastProcessName = ProcessSet.Idle;

        /// <summary>
        /// File log timer.
        /// </summary>
        private System.Timers.Timer _timer = new System.Timers.Timer();

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
        /// 
        /// </summary>
        private string _strMargin = "  ";

        /// <summary>
        /// 
        /// </summary>
        private DateTime _lastWriteLogTime = DateTime.Now;

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
        private static int[] _colWidth = new int[3] { 35, 5, 25 };

        /// <summary>
        /// 
        /// </summary>
        private static UserActivityLogger _instance = new UserActivityLogger();
        #endregion

        #region Properties
        public static UserActivityLogger Instance
        {
            get
            {
                return _instance;
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
        public string LogFileName
        {
            get
            {
                return this._logFileName;
            }
        }

        private double TotalLoggedTime
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
        private Dictionary<string, double> ProcessesTimeSummary
        {
            get
            {
                return this._dicProcessesTimeSummary;
            }

            set
            {
                lock (this._dicProcessesTimeSummary)
                {
                    this._dicProcessesTimeSummary = value;
                }
            }
        }
        #endregion

        #region Ctors.
        /// <summary>
        /// Ctor.
        /// </summary>
        UserActivityLogger()
            : this(true, true)
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="logMouse"></param>
        /// <param name="logKeyBoard"></param>
        UserActivityLogger(bool logMouse, bool logKeyBoard)
            : this(logMouse, logKeyBoard, @"UserActivityLog.txt")
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="logMouse"></param>
        /// <param name="logKeyBoard"></param>
        UserActivityLogger(bool logMouse, bool logKeyBoard, string logFileName)
            : this(logMouse, logKeyBoard, logFileName, 600000.0)    ////timer's interval is default to five minutes.
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logMouse"></param>
        /// <param name="logKeyBoard"></param>
        UserActivityLogger(bool logMouse, bool logKeyBoard, string logFileName, double interval)
        {
            this._uaHook = new UserActivityHook(logMouse, logKeyBoard);
            this._logFileName = logFileName;
            this._timer.Interval = interval;
            this.Initialize();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            int totalWidth = _colWidth[0] + _colWidth[1] + _colWidth[2];
            string datetimeFormatStr = "yyyy-MM-dd HH:mm:ss";
            string processCountInfor = "Logged Process Count - 00/00";
            string infor = "*".PadLeft(totalWidth, '*') + "\n";
            infor += this.StartTime.ToString(datetimeFormatStr).PadLeft((totalWidth + this.StartTime.ToString(datetimeFormatStr).Length) / 2) + "\n";
            infor += DateTime.Now.ToString(datetimeFormatStr).PadLeft((totalWidth + DateTime.Now.ToString(datetimeFormatStr).Length) / 2) + "\n";
            infor += processCountInfor.PadLeft((totalWidth + processCountInfor.Length) / 2) + "\n";
            infor += "-".PadLeft(totalWidth, '-') + "\n";
            double dtActual = (DateTime.Now - this.StartTime).TotalMilliseconds;
            int counter = 1;
            foreach (KeyValuePair<string, string> kp in ProcessSet.Processes)
            {
                if (this.ProcessesTimeSummary[kp.Key] > 0.0)
                {
                    infor += this._strMargin + counter++.ToString().PadLeft(2) + this._strMargin + this.FormatProcessTime(kp.Key) + "\n";
                }
            }

            infor = infor.Replace(processCountInfor, "Logged Process Count - " + (counter - 1).ToString().PadLeft(2) + "/" + ProcessSet.Processes.Count);
            infor += "-".PadLeft(totalWidth, '-') + "\n";
            infor += this.FormatInforTime("Total time interval", dtActual) + "\n";
            infor += this.FormatInforTime("Total time logged", this.TotalLoggedTime) + "\n";
            infor += this.FormatInforTime("Total time d-value", dtActual - this.TotalLoggedTime) + "\n";
            infor += "*".PadLeft(totalWidth, '*');
            return infor;
        }

        #region Log information.
        /// <summary>
        /// Log time infor.
        /// </summary>
        public void Log()
        {
            if (Nzl.Utils.FileUtil.WriteText(this._logFileName, this.ToString() + "\n").Substring(0, 1) == "S")
            {
                this._lastWriteLogTime = DateTime.Now;
            }
        }
        #endregion

        /// <summary>
        /// Initialize.
        /// </summary>
        private void Initialize()
        {
            if (this._uaHook != null)
            {
                this._uaHook.MouseActivity += new EventHandler<MouseExEventArgs>(this._uaHook_MouseActivity);
                this._uaHook.KeyPress += new EventHandler<KeyPressExEventArgs>(this._uaHook_KeyPress);
                this._uaHook.KeyDown += new EventHandler<KeyExEventArgs>(this._uaHook_KeyDown);
                this._uaHook.KeyUp += new EventHandler<KeyExEventArgs>(_uaHook_KeyUp);

                this._dicProcessesTimeSummary = this.GetTimeSummaryDic();

                this._timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
                this._timer.Start();

                this._dtStart = DateTime.Now;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
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
                    UpdateProcessSummaryTime(ProcessSet.Idle, this.LastProcessName, dtNow);
                    return;
                }
            }
            catch
            {
            }
        }

        #region User Activity Hook Handler
        private void _uaHook_MouseActivity(object sender, MouseExEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.None || e.Delta > 0)
            {
                Update(UserActivityType.Mouse);
            }
        }

        private void _uaHook_KeyPress(object sender, KeyPressExEventArgs e)
        {
            Update(UserActivityType.KeyPress);
        }

        private void _uaHook_KeyDown(object sender, KeyExEventArgs e)
        {
            Update(UserActivityType.KeyDown);
        }

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
                    return prs.ProcessName;
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
                return ProcessSet.Idle;
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
                if (ProcessSet.Processes.ContainsKey(processName) == false)
                {
                    ProcessSet.Processes.Add(processName, processName);
                }

                if (this.ProcessesTimeSummary.ContainsKey(processName) == false)
                {
                    this.ProcessesTimeSummary.Add(processName, 0.0);
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
            if (this.LastProcessName == ProcessSet.Idle)
            {
                this.ProcessesTimeSummary[ProcessSet.Idle] += ts.TotalMilliseconds;
            }
            else
            {
                this.ProcessesTimeSummary[processName] += ts.TotalMilliseconds / 2.0;
                this.ProcessesTimeSummary[this.LastProcessName] += ts.TotalMilliseconds / 2.0;
            }

            this.TotalLoggedTime += ts.TotalMilliseconds;
            this.LastProcessName = processName;
            this.LastActivityTime = dtNow;
        }
        #endregion

        #region Private methods.
        /// <summary>
        /// Get a dictionay of processes with which total time is recorded corresponding to the process.
        /// </summary>
        /// <returns>A dictionary.</returns>
        private Dictionary<string, double> GetTimeSummaryDic()
        {
            Dictionary<string, double> dic = new Dictionary<string, double>();
            foreach (KeyValuePair<string, string> kp in ProcessSet.Processes)
            {
                dic.Add(kp.Key, 0.0);
            }

            return dic;
        }

        /// <summary>
        /// Check whether the status can be updated.
        /// </summary>
        /// <returns>A boolean flag indicates whether the status can be updated.</returns>
        private bool IntervalIsTooLong(TimeSpan ts)
        {
            return ts.TotalMilliseconds > _longIntervalThreshold;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IntervalIsTooShort(TimeSpan ts)
        {
            return ts.TotalMilliseconds < _shortIntervalThreshold;
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

        private string FormatProcessTimePersentage(string process)
        {
            if (this.TotalLoggedTime > 0.0)
            {
                return (this.ProcessesTimeSummary[process] * 100.0 / this.TotalLoggedTime).ToString("0.0");
            }

            return "0.0";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        /// <param name="totalmiliseconds"></param>
        /// <returns></returns>
        private string FormatProcessTime(string process)
        {
            string processName = ProcessSet.Processes[process];
            if (processName.Length > _colWidth[0] - this._strMargin.Length * 3)
            {
                processName = processName.Substring(0, _colWidth[0] - this._strMargin.Length * 3 - 2) + "..";
            }

            return processName.PadRight(_colWidth[0] - this._strMargin.Length * 3)
                   + this.FormatProcessTimePersentage(process).PadLeft(_colWidth[1])
                   + this.FormatTime(this.ProcessesTimeSummary[process]).PadLeft(_colWidth[2]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        /// <param name="totalmiliseconds"></param>
        /// <returns></returns>
        private string FormatInforTime(string infor, double totalmiliseconds)
        {
            return _strMargin
                   + infor.PadRight(_colWidth[0] + _colWidth[1] - this._strMargin.Length)
                   + this.FormatTime(totalmiliseconds).PadLeft(_colWidth[2]);
        }
        #endregion
    }
}
