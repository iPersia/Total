using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nzl.Web.Forms.MobileNewSmth.Datas
{
    /// <summary>
    /// 
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 
        /// </summary>
        public MessageType Type
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Source
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Detail
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 
        /// </summary>
        Information = 1,

        /// <summary>
        /// 
        /// </summary>
        Log = 2,

        /// <summary>
        /// 
        /// </summary>
        Error = 3,

        /// <summary>
        /// 
        /// </summary>
        Exception = 4
    }    
}
