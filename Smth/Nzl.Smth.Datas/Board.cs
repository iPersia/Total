namespace Nzl.Smth.Datas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class Board : BaseData
    {  
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public override string ID
        {
            get
            {
                return this.Code;
            }
        }
    }
}
