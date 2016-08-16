namespace Nzl.Utils
{
    using System;
    using System.Reflection;

    /// <summary>
    /// 
    /// </summary>
    public static class ConfigurationUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static object GetConfigurationValue(string main, string key)
        {
            Assembly asm = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "\\Nzl.Configuration.dll");
            if (asm != null)
            {
                Type[] types = asm.GetExportedTypes();
                foreach (Type type in types)
                {
                    if (type.ToString() == "Nzl.Configuration.ConfigurationManager")
                    {
                        MethodInfo method = type.GetMethod("GetConfiguartionValue", new Type[] { typeof(string), typeof(string) });
                        if (method != null)
                        {
                            return method.Invoke(null, new object[] {main, key});
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static object GetConfigurationValue(Type type, string key)
        {            
            return type == null ? null : GetConfigurationValue(type.ToString(), key);
        }
    }
}
