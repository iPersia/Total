namespace Nzl.Core.Interface
{
    using System;

    /// <summary>
    /// Get configruation.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainConfig"></param>
        /// <param name="detailConfig"></param>
        /// <returns></returns>
        object GetConfigruationValue(string mainConfig, string detailConfig);
    }
}
