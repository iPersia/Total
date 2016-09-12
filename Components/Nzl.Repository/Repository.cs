namespace Nzl.Repository
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public static class Repository
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, object> _staticDictionary = new Dictionary<string, object>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetValue<T>(string key)
            where T : class
        {
            try
            {
                if (_staticDictionary.ContainsKey(key))
                {
                    return _staticDictionary[key] as T;
                }

                return default(T);
            }
            catch
            {
                return default(T);
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void Add<T>(string key, T value)
            where T : class
        {
            lock(_staticDictionary)
            {
                try
                {
                    if (_staticDictionary.ContainsKey(key) == false)
                    {
                        _staticDictionary.Add(key, value);
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Clear()
        {
            lock(_staticDictionary)
            {
                try
                {
                    _staticDictionary.Clear();
                }
                catch { }                
        }
    }
}
