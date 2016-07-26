namespace Nzl.Web.Smth.Datas
{
    using System.Collections.Generic;
    using System.ComponentModel;
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
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
            bw.RunWorkerAsync(@"http://m.newsmth.net/section");
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Message msg = new Message();
                msg.DateTime = System.DateTime.Now;
                msg.Source = this.ToString();
                msg.Detail = e.Argument as string;
                msg.Type = MessageType.Information;
                MessageQueue.Enqueue(msg);

                WebPage wp = new WebPage(e.Argument as string);
                IList<BaseItem> bsList = SectionUtil.GetSectionsAndBoards(wp);
                foreach (BaseItem bi in bsList)
                {
                    Board board = bi as Board;
                    if (board != null)
                    {
                        this.AddBoard(board.Code, board.Name);
                    }

                    Section section = bi as Section;
                    if (section != null)
                    {
                        BackgroundWorker bw = new BackgroundWorker();
                        bw.DoWork += Bw_DoWork;
                        bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
                        bw.RunWorkerAsync(@"http://m.newsmth.net/section/" + section.Code);
                    }
                }
            }
            catch
            {
            }
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
                try {
                    this._dicBoards.Add(engName, chnName);
                }catch { };
            }
        }
        #endregion
    }
}
