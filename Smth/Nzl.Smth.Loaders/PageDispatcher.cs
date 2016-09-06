namespace Nzl.Smth.Loaders
{
    using System;
    using System.Runtime.Remoting.Messaging;
    using Nzl.Dispatcher;    
    using Nzl.Messaging;
    using Nzl.Smth.Logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    delegate bool AsyncExecuteItem(IExecute item);

    /// <summary>
    /// 
    /// </summary>
    public class PageDispatcher : Dispatcher
    {
        #region Singleton
        /// <summary>
        /// 
        /// </summary>
        public static readonly PageDispatcher Instance = new PageDispatcher();
        #endregion

        /// <summary>
        /// 
        /// </summary>
        PageDispatcher()
        {
            this.Synchronous = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Synchronous
        {
            get;
            set;
        }

        /// <summary>
        /// Override the method.
        /// </summary>
        protected override void OnRun()
        {
            IExecute item = Get();
            if (item != null)
            {
                AsyncExecuteItem caller = new AsyncExecuteItem(ExecuteItem);
                if (Synchronous)
                {
                    caller.Invoke(item);
                }
                else
                {
                    caller.BeginInvoke(item, new AsyncCallback(ExecuteItemCallBack), caller);
                }

                System.Threading.Thread.Sleep(0);
#if (DEBUG)
                MessageQueue.Enqueue(MessageFactory.CreateMessage("Page dispatcher", "the queue size is " + this.mQueues.Count + "!"));
#endif
                return;
            }

            System.Threading.Thread.Sleep(100);
        }


        #region Async invoke.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool ExecuteItem(IExecute item)
        {
            try
            {
                return item.Execute();
            }
            catch (Exception e)
            {
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(e.Message + "\n" + e.StackTrace);
                }

                return false;
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
