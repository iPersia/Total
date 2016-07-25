namespace Nzl.Web.Smth.Datas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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
        public void AddBoard(string engName, string chnName)
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
