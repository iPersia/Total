namespace Nzl.Configuration
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The static class for configuration.
    /// </summary>
    public static class ConfigurationManager
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly Dictionary<string, Dictionary<string, object>> s_mainDict;

        /// <summary>
        /// 
        /// </summary>
        static ConfigurationManager()
        {
            s_mainDict = new Dictionary<string, Dictionary<string, object>>();

            /// For class: Nzl.Utils.EncryptUtil
            {
                string key = "Nzl.Utils.EncryptUtil";
                s_mainDict.Add(key, new Dictionary<string, object>());
                s_mainDict[key].Add("Key", "_CAS_DEV_GROUP_");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainConfig"></param>
        /// <param name="detailConfig"></param>
        /// <returns></returns>
        public static object GetConfiguartionValue(string mainConfig, string detailConfig)
        {
            if (s_mainDict.ContainsKey(mainConfig) && s_mainDict[mainConfig].ContainsKey(detailConfig))
            {
                return s_mainDict[mainConfig][detailConfig];
            }

            return null;
        }
    }
}
