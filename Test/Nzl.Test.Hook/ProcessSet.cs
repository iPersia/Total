namespace Nzl.Test.Hook
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// 
    /// </summary>
    public static class ProcessSet
    {

        /// <summary>
        /// 
        /// </summary>
        private static string _processXmlFileName = "Processes.xml";

        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, string> _dic;

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, string> Processes
        {
            get
            {
                return _dic;
            }
        }

        public static string Idle
        {
            get
            {
                return "Idle";
            }
        }

        public static string Unkonwn
        {
            get
            {
                return "Unkonwn";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        static ProcessSet()
        {
            _dic = new Dictionary<string, string>();
            _dic.Add(Idle, Idle);
            _dic.Add(Unkonwn, Unkonwn);
            LoadProcessesFromXml();
        }

        static void LoadProcessesFromXml()
        {
            try
            {
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.Load(_processXmlFileName);
                XmlNode root = xmlDoc.SelectSingleNode("Processes");
                foreach (XmlNode xe in root.ChildNodes)
                {
                    if (Processes.ContainsKey(xe.SelectSingleNode("ID").InnerText) == false)
                    {
                        _dic.Add(xe.SelectSingleNode("ID").InnerText, xe.SelectSingleNode("Name").InnerText);
                    }
                }                
            }
            catch
            {

            }
        }

        static void SaveProcessesToXml()
        {
            try
            {
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.Load(_processXmlFileName);
                XmlNode root = xmlDoc.SelectSingleNode("Processes");
                foreach (KeyValuePair<string, string> kp in _dic)
                {
                    XmlElement xePrs = xmlDoc.CreateElement("Process");
                    XmlElement xeID = xmlDoc.CreateElement("ID");
                    XmlElement xeName = xmlDoc.CreateElement("Name");
                    xeID.InnerText = kp.Key;
                    xeName.InnerText = kp.Value;
                    xePrs.AppendChild(xeID);
                    xePrs.AppendChild(xeName);
                    root.AppendChild(xePrs);
                }

                xmlDoc.Save(_processXmlFileName);
            }
            catch
            {
            }
        }
    }
}