using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Nzl.Test.HookServer
{
    /// <summary>
    /// 
    /// </summary>
    sealed class ProcessSet
    {
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, string> _dic = new Dictionary<string, string>();

        /// <summary>
        /// Singleton.
        /// </summary>
        public readonly static ProcessSet Instance = new ProcessSet();

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Processes
        {
            get
            {
                return _dic;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Idle
        {
            get
            {
                return "System Idle";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ProcessXmlFileName
        {
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                {
                    this.LoadProcessesFromXml(value);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        ProcessSet()
        {
            _dic.Add(Idle, Idle);
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadProcessesFromXml(string procXmlFileName)
        {
            try
            {
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.Load(procXmlFileName);
                XmlNode root = xmlDoc.SelectSingleNode("Processes");
                foreach (XmlNode xe in root.ChildNodes)
                {
                    if (Processes.ContainsKey(xe.SelectSingleNode("ID").InnerText) == false)
                    {
                        _dic.Add(xe.SelectSingleNode("ID").InnerText, xe.SelectSingleNode("Name").InnerText);
                    }
                    else
                    {
                        _dic[xe.SelectSingleNode("ID").InnerText] = xe.SelectSingleNode("Name").InnerText;
                    }
                }
            }
            catch { }
        }

        void SaveProcessesToXml(string procXmlFileName)
        {
            try
            {
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.Load(procXmlFileName);
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

                xmlDoc.Save(procXmlFileName);
            }
            catch
            {
            }
        }
    }
}
