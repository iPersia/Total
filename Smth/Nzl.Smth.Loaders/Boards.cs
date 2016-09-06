namespace Nzl.Smth.Loaders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Nzl.Messaging;
    using Nzl.Recycling;
    using Nzl.Smth.Datas;
    using Nzl.Smth.Logger;
    using Nzl.Smth.Utils;
    using Nzl.Web.Page;

    /// <summary>
    /// 
    /// </summary>
    public class Boards
    {
        #region Singleton
        /// <summary>
        /// 
        /// </summary>
        public static readonly Boards Instance = new Boards();
        #endregion

        #region Variables
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, string> _dicBoards = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        private BoardNode _treenodeRoot = new BoardNode();
        #endregion

        #region Ctors.
        /// <summary>
        /// 
        /// </summary>
        Boards()
        { }
        #endregion

        #region Public
        /// <summary>
        /// 
        /// </summary>
        public void Initilize()
        {
            WorkerArgs e = new WorkerArgs();
            e.SectionUrl = @"http://m.newsmth.net/section";
            e.Node = this._treenodeRoot;

            PageLoader pl = new PageLoader(e.SectionUrl);
            pl.Tag = e;
            pl.PageLoaded += PageLoader_PageLoaded;
            PageDispatcher.Instance.Add(pl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageLoader_PageLoaded(object sender, EventArgs e)
        {
            PageLoader pl = sender as PageLoader;
            if (pl != null)
            {
                WebPage wp = pl.GetResult() as WebPage;
                WorkerArgs workArgs = pl.Tag as WorkerArgs;

                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += Bw_DoWork;
                bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
                bw.RunWorkerAsync(pl);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                PageLoader pl = e.Argument as PageLoader;
                WorkerArgs args = pl.Tag as WorkerArgs;
                if (args != null)
                {
                    WebPage wp = pl.GetResult() as WebPage;
                    IList<Section> secList = SectionUtil.GetSections(wp);
                    foreach (Section sec in secList)
                    {
                        ///Board
                        if (sec.IsBoard)
                        {
                            BoardNode tnBoard = new BoardNode();
                            tnBoard.Parent = args.Node;
                            args.Node.AddChild(sec.Code, tnBoard);

                            this.AddBoard(sec.Code, sec.Name);                            
                        }
                        else
                        ///Section
                        {
                            BoardNode tnSection = new BoardNode();
                            tnSection.Parent = args.Node;
                            args.Node.AddChild(sec.Code, tnSection);

                            WorkerArgs newArgs = new WorkerArgs();
                            newArgs.SectionUrl = @"http://m.newsmth.net/section/" + sec.Code;
                            newArgs.Node = tnSection;

                            PageLoader pageLoader = new PageLoader(newArgs.SectionUrl);
                            pageLoader.Tag = newArgs;
                            pageLoader.PageLoaded += PageLoader_PageLoaded;
                            PageDispatcher.Instance.Add(pageLoader);
                        }

                        RecycledQueues.AddRecycled<Section>(sec);
                    }
                }

                e.Result = args;
            }
            catch (Exception exp)
            {
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\n" + exp.StackTrace);
                }
#if (DEBUG)
                MessageQueue.Enqueue(MessageFactory.CreateMessage(exp));
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //WorkerArgs args = e.Result as WorkerArgs;
            //if (args != null)
            //{

            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engName"></param>
        /// <returns></returns>
        public string GetBoardName(string engName)
        {
            if (this._dicBoards.ContainsKey(engName))
            {
                return this._dicBoards[engName];
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engName"></param>
        /// <returns></returns>
        private void AddBoard(string engName, string chnName)
        {
            if (this._dicBoards.ContainsKey(engName))
            {
                this._dicBoards[engName] = chnName;
            }
            else
            {
                try
                {
                    this._dicBoards.Add(engName, chnName);
                }
                catch (Exception exp)
                {
                    if (Logger.Enabled)
                    {
                        Logger.Instance.Error(exp.Message + "\n" + exp.StackTrace);
                    }
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class WorkerArgs
        {
            /// <summary>
            /// 
            /// </summary>
            public string SectionUrl
            {
                get;
                set;
            }

            /// <summary>
            /// 
            /// </summary>
            public BoardNode Node
            {
                get;
                set;
            }
        }
        #endregion
    }
}
