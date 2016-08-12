namespace Nzl.Web.Smth.Datas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, TreeNode> _dicChilds = new Dictionary<string, TreeNode>();
        /// <summary>
        /// 
        /// </summary>
        public BaseItem Item
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public TreeNode Parent
        {
            get;
            set;
        }        

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, TreeNode> Childs
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
        public void AddChild(string key, TreeNode node)
        {
            try
            {
                this._dicChilds.Add(key, node);
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message + "\n" + exp.StackTrace);
                }
            };
        }
    }
}
