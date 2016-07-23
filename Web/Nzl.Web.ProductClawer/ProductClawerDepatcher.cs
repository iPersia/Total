namespace Nzl.Web.ProductClawer
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Remoting.Messaging;
    using System.Threading;
    using Nzl.Web.Util;
    using Nzl.Web.Page;
    using Nzl.Web.ProductClawer;
    using Nzl.Web.Interface;

    delegate bool AsyncExecuteItem(IWorkItem item);

    /// <summary>
    /// 
    /// </summary>
    public class ProductClawerDepatcher : BaseDespatcher
    {
        #region Variables
        /// <summary>
        /// Async invoker count.
        /// </summary>
        private int _asyncInvokeCount = 0;

        /// <summary>
        /// 
        /// </summary>
        private object _asyncInvokeCountLocker = new object();

        /// <summary>
        /// The totally excute count.
        /// </summary>
        private UInt64 _totalExcuteCount = 0;

        /// <summary>
        /// 
        /// </summary>
        private object _totalExcuteCountLocker = new object();

        /// <summary>
        /// 
        /// </summary>
        private int _maxAsyncInvokeCount = 64;

        /// <summary>
        /// 
        /// </summary>
        private object _maxAsyncInvokeCountLocker = new object();
        #endregion

        #region Properties
        /// <summary>
        /// Async invoker count.
        /// </summary>
        public int AsyncInvokeCount
        {
            get
            {
                lock (this._asyncInvokeCountLocker)
                {
                    return this._asyncInvokeCount;
                }
            }
        }

        /// <summary>
        /// Async invoker count.
        /// </summary>
        public UInt64 TotalExcuteCount
        {
            get
            {
                lock (_totalExcuteCountLocker)
                {
                    return this._totalExcuteCount;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MaxAsyncInvokeCount
        {
            get
            {
                lock (this._maxAsyncInvokeCountLocker)
                {
                    return this._maxAsyncInvokeCount;
                }
            }

            set
            {
                lock (this._maxAsyncInvokeCountLocker)
                {
                    this._maxAsyncInvokeCount = value;
                }
            }
        }
        #endregion

        /// <summary>
        /// Override the method.
        /// </summary>
        protected override void OnRun()
        {
            lock (this._maxAsyncInvokeCountLocker)
            {
                lock (this._asyncInvokeCountLocker)
                {
                    if (this._asyncInvokeCount < this._maxAsyncInvokeCount)
                    {
                        IWorkItem item = Get();
                        if (item != null)
                        {
                            AsyncExecuteItem caller = new AsyncExecuteItem(ExecuteItem);
                            caller.BeginInvoke(item, new AsyncCallback(ExecuteItemCallBack), caller);
                            this._asyncInvokeCount++;
                        }
                    }
                }
            }

            System.Threading.Thread.Sleep(5);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected void ExecuteItemThread(object obj)
        {
            try
            {
                IWorkItem item = obj as IWorkItem;
                if (obj != null)
                {
                    item.Execute();
                }
            }
            catch (Exception exp)
            {
#if (DEBUG)
                CommonUtil.ShowMessage(this, exp.Message);
#endif
            }
            finally
            {
                lock (_asyncInvokeCountLocker)
                {
                    this._asyncInvokeCount--;
                }
            }
        }

        #region Async invoke.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool ExecuteItem(IWorkItem item)
        {
            try
            {
                return item.Execute();
            }
            catch (Exception exp)                 
            {
#if (DEBUG)
                CommonUtil.ShowMessage(this, exp.Message);
#endif
                return false;
            }
            finally
            {
                lock (_asyncInvokeCountLocker)
                {
                    this._asyncInvokeCount--;
                }

                lock (_totalExcuteCountLocker)
                {
                    this._totalExcuteCount++;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
        private void ExecuteItemCallBack(IAsyncResult ar)
        {
            if (ar == null)
            {
                return;
            }

            AsyncResult result = (AsyncResult)ar;
            AsyncExecuteItem caller = (AsyncExecuteItem)result.AsyncDelegate;
            caller.EndInvoke(ar);
        }
        #endregion
    }
}
