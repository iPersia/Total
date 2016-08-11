namespace Nzl.Web.Smth.Common
{
    using System;
    using System.Runtime.Remoting.Messaging;
    using Nzl.Dispatcher;

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
                //caller.Invoke(item);
                caller.BeginInvoke(item, new AsyncCallback(ExecuteItemCallBack), caller);
                System.Threading.Thread.Sleep(0);
#if (DEBUG)
                Utils.MessageQueue.Enqueue(Utils.MessageFactory.CreateMessage("Page dispatcher", "the queue size is " + this.mQueues.Count + "!"));
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
            catch
            {
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
