namespace Nzl.Smth.Datas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class BaseItem
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string ID
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool Updated
        {
            set;
            get;
        }
    }
}
