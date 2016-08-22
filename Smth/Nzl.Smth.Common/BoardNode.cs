namespace Nzl.Smth.Common
{
    using System;
    using System.Collections.Generic;
    using Nzl.Smth.Logger;

    /// <summary>
    /// 
    /// </summary>
    internal class BoardNode
    {
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, BoardNode> _dicChilds = new Dictionary<string, BoardNode>();


        /// <summary>
        /// 
        /// </summary>
        public BoardNode Parent
        {
            get;
            set;
        }        

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, BoardNode> Childs
        {
            get
            {
                return this._dicChilds;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="node"></param>
        public void AddChild(string key, BoardNode node)
        {
            try
            {
                this._dicChilds.Add(key, node);
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
}
