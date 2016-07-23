namespace Nzl.Web.Smth.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nzl.Web.Smth.Datas;

    /// <summary>
    /// 
    /// </summary>
    public interface IContainsThread
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        Thread GetSavedThread(string tid);
    }
}
