namespace Nzl.Util
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Query performance.
    /// </summary>
    public class QueryPerformance
    {
        /// <summary>
        /// The import method - QueryPerformanceCounter.
        /// </summary>
        /// <param name="performanceCount">The performance count.</param>
        /// <returns>Operation flag.</returns>
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long performanceCount);

        /// <summary>
        /// The import method - QueryPerformanceFrequency.
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        /// <returns>Operation flag.</returns>
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long frequency);

        /// <summary>
        /// The starting time.
        /// </summary>
        private long beginTime = 0;

        /// <summary>
        /// The ending time.
        /// </summary>
        private long endTime = 0;

        /// <summary>
        /// The frequence of the CPU.
        /// </summary>
        private long frequency = 0;//处理器频率

        /// <summary>
        /// The beginning time.
        /// </summary>
        public long BeginTime
        {
            get { return beginTime; }
        }

        /// <summary>
        /// The ending time.
        /// </summary>
        public long EndTime
        {
            get { return endTime; }
        }

        /// <summary>
        /// The frequence of the CPU.
        /// </summary>
        public long Frequency
        {
            get { return frequency; }
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public QueryPerformance()
        {
            QueryPerformanceFrequency(out frequency);//获取频率
        }

        /// <summary>
        /// Start.
        /// </summary>
        public void Start()
        {
            QueryPerformanceCounter(out beginTime);
        }

        /// <summary>
        /// Stop.
        /// </summary>
        public void Stop()
        {
            QueryPerformanceCounter(out endTime);            
        }

        /// <summary>
        /// The time spent.
        /// </summary>
        public double TastTime//花费时间：单位ns
        {
            get
            {
                if (frequency > 0)
                    return (double)(endTime - beginTime) * 1000000 / frequency;
                else
                    return 0;
            }
        }
    }
}
