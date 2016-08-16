namespace Nzl.Utils
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Xml;

    /// <summary>
    /// Util for miscellaneous usage.
    /// </summary>
    public static class MiscUtil
    {
        /// <summary>
        /// 根据枚举类型返回类型中的所有值，文本及描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns>返回三列数组，第0列为Description,第1列为Value，第2列为Text</returns>
        public static List<string[]> GetEnumInfor(Type type)
        {
            List<string[]> Strs = new List<string[]>();
            FieldInfo[] fields = type.GetFields();
            for (int i = 1, count = fields.Length; i < count; i++)
            {
                string[] strEnum = new string[3];
                FieldInfo field = fields[i];
                //值列
                strEnum[1] = ((int)Enum.Parse(type, field.Name)).ToString();
                //文本列赋值
                strEnum[2] = field.Name;

                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length == 0)
                {
                    strEnum[0] = field.Name;
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    strEnum[0] = da.Description;
                }

                Strs.Add(strEnum);
            }

            return Strs;
        }

        //// <summary>
        /// 获取枚举类子项描述信息
        /// </summary>
        /// <param name="enumSubitem">枚举类子项</param>        
        public static string GetEnumDescription(object enumSubitem)
        {
            enumSubitem = (Enum)enumSubitem;
            string strValue = enumSubitem.ToString();

            FieldInfo fieldinfo = enumSubitem.GetType().GetField(strValue);

            if (fieldinfo != null)
            {

                Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (objs == null || objs.Length == 0)
                {
                    return strValue;
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    return da.Description;
                }
            }
            else
            {
                return enumSubitem.ToString();
            }
        }

        /// <summary>
        /// 读取.exe.config的值
        /// </summary>
        /// <param name="path">.exe.config文件的路径</param>
        /// <param name="appKey">"key"的值</param>
        /// <returns>"value"的值</returns>
        public static string GetConfigValue(string path, string appKey)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path);
                XmlNode xNode = xDoc.SelectSingleNode("//appSettings"); ;
                XmlElement xElem = (XmlElement)xNode.SelectSingleNode("//add[@key=\"" + appKey + "\"]");
                if (xElem != null)
                {
                    return xElem.GetAttribute("value");
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        #region 将程序添加到启动项
        /// <summary>
        /// 注册表操作，将程序添加到启动项
        /// </summary>
        public static void SetRegistryApp(string productName, string excutePath)
        {
            try
            {
                Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (reg == null)
                {
                    reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                }

                reg.SetValue(productName, excutePath);
            }
            catch
            {

            }
        }
        #endregion

        #region 将程序从启动项中删除
        /// <summary>
        /// 注册表操作，删除注册表中启动项
        /// </summary>
        public static bool DeleteRegisterApp(string productName)
        {
            try
            {
                Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (reg == null)
                {
                    reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                }

                reg.DeleteValue(productName, false);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 检查当前程序是否在启动项中
        /// <summary>
        /// 检查当前程序是否在启动项中
        /// </summary>
        /// <returns></returns>
        public static bool CheckExistRegisterApp(string productName)
        {
            try
            {
                Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (reg == null)
                {
                    reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                }

                foreach (string s in reg.GetValueNames())
                {
                    if (s.Equals(productName))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
