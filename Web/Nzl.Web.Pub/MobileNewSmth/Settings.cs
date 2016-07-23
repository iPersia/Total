namespace Nzl.Web.Pub.MobileNewSmth
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    static class Settings
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, string> _dicSettings = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        static Settings()
        {
            Deserialize();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Serialize()
        {
            try
            {
                string fileName = Application.ExecutablePath + ".settings";//文件名称与路径
                Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
                binFormat.Serialize(fStream, _dicSettings);
                fStream.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Deserialize()
        {
            try
            {
                string fileName = Application.ExecutablePath + ".settings";//文件名称与路径
                Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
                _dicSettings = (Dictionary<string, string>)binFormat.Deserialize(fStream);
                fStream.Close();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static bool Contains(string setting)
        {
            return _dicSettings.ContainsKey(setting);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="value"></param>
        public static void Set(string setting, string value)
        {
            if (Contains(setting))
            {
                _dicSettings[setting] = value;
            }
            else
            {
                _dicSettings.Add(setting, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static string Get(string setting)
        {
            if (_dicSettings.ContainsKey(setting))
            {
                return _dicSettings[setting];
            }

            return null;
        }
    } 
}
