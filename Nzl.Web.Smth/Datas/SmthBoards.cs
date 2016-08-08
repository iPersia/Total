namespace Nzl.Web.Smth.Datas
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Common;
    using Page;
    using Utils;

    /// <summary>
    /// 
    /// </summary>
    public class SmthBoards
    {
        #region Singleton
        /// <summary>
        /// 
        /// </summary>
        public static readonly SmthBoards Instance = new SmthBoards();
        #endregion

        #region Variables
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, string> _dicBoards = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        private TreeNode _treenodeRoot = new TreeNode();
        #endregion

        #region Ctors.
        /// <summary>
        /// 
        /// </summary>
        SmthBoards()
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
                WebPage wp = pl.GetPage();
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
                    WebPage wp = pl.GetPage();
                    IList<BaseItem> bsList = SectionUtil.GetSectionsAndBoards(wp);
                    foreach (BaseItem bi in bsList)
                    {
                        ///Board
                        {
                            Board board = bi as Board;
                            if (board != null)
                            {
                                TreeNode tnBoard = new TreeNode();
                                tnBoard.Item = board;
                                tnBoard.Parent = args.Node;
                                args.Node.AddChild(board.Code, tnBoard);

                                this.AddBoard(board.Code, board.Name);
                            }
                        }

                        ///Section
                        {
                            Section section = bi as Section;
                            if (section != null)
                            {
                                TreeNode tnSection = new TreeNode();
                                tnSection.Item = section;
                                tnSection.Parent = args.Node;
                                args.Node.AddChild(section.Code, tnSection);

                                WorkerArgs newArgs = new WorkerArgs();
                                newArgs.SectionUrl = @"http://m.newsmth.net/section/" + section.Code;
                                newArgs.Node = tnSection;

                                PageLoader pageLoader = new PageLoader(newArgs.SectionUrl);
                                pageLoader.Tag = newArgs;
                                pageLoader.PageLoaded += PageLoader_PageLoaded;
                                PageDispatcher.Instance.Add(pageLoader);
                            }
                        }
                    }
                }

                e.Result = args;
            }
            catch (Exception exp)
            {
                MessageQueue.Enqueue(MessageFactory.CreateMessage(exp));
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
            //if (args != null && args.Node == this._treenodeRoot)
            //{
            //    Message msg = new Message();
            //    msg.DateTime = System.DateTime.Now;
            //    msg.Source = "Loading section information completed!";
            //    msg.Detail = "The root url is " + args.SectionUrl + "!";
            //    msg.Type = MessageType.Information;
            //    MessageQueue.Enqueue(msg);
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
                catch { };
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
            public TreeNode Node
            {
                get;
                set;
            }
        }
        #endregion
    }
}
