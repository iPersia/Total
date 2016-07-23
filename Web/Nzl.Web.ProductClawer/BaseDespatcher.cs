namespace Nzl.Web.ProductClawer
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Remoting.Messaging;
    using System.Threading;
    using Nzl.Web.Interface;

    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseDespatcher : IDisposable
    {
        #region Variable
        /// <summary>
        /// 
        /// </summary>
        protected Queue<IWorkItem> mQueues = new Queue<IWorkItem>(1024);

        /// <summary>
        /// 
        /// </summary>
        protected bool mDispose = false;

        /// <summary>
        /// 
        /// </summary>
        protected UInt64 m_TotalCount = 0;

        /// <summary>
        /// 
        /// </summary>
        private object m_TotalCountLocker = new object();
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                return this.mQueues.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public UInt64 TotalCount
        {
            get
            {
                return this.m_TotalCount;
            }
        }        

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            lock (this)
            {
                if (!this.mDispose)
                {
                    this.mDispose = true;
                }
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public BaseDespatcher()
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(Run));
            thread.Name = "BaseDespatcherRunThread";
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(IWorkItem item)
        {
            lock (mQueues)
            {
                if (this.mQueues.Count < 1024)
                {
                    this.mQueues.Enqueue(item);
                    lock (this.m_TotalCountLocker)
                    {
                        this.m_TotalCount++;
                    }                    
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Reset()
        {
            lock (this.mQueues)
            {
                this.mQueues.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IWorkItem Get()
        {
            lock (this.mQueues)
            {
                if (this.mQueues.Count > 0)
                {
                    return this.mQueues.Dequeue();
                }

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnRun()
        {
            System.Threading.Thread.Sleep(50);
        }        

        /// <summary>
        /// 
        /// </summary>
        public virtual void Run()
        {
            while (!this.mDispose)
            {
                this.OnRun();
            }
        }
    }
}

