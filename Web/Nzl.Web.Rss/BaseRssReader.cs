namespace Nzl.Web.Rss
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Nzl.Web.Core;
    using Nzl.Web.Core.EventArgs;
    using Nzl.Web.Interface;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseRssReader : IRssReader, IException
    {
        #region vars.
        /// <summary>
        /// The rss's vendor variable.
        /// </summary>
        private string _vendor = null;

        /// <summary>
        /// The rss's uri variable.
        /// </summary>
        private Uri _uri = null;

        /// <summary>
        /// The rss reader's updating interval, default to 1 minute.
        /// </summary>
        private int _updatingInterval = 60 * 1000;

        /// <summary>
        /// 
        /// </summary>
        private System.Timers.Timer _updatingTimer = new System.Timers.Timer();

        /// <summary>
        /// 
        /// </summary>
        private bool _isRuningUpdating = false;

        /// <summary>
        /// 
        /// </summary>
        private object _isRuningUpdatingLocker = new object();

        /// <summary>
        /// The auto loading flag.
        /// </summary>
        protected bool _autoLoad = true;

        /// <summary>
        /// 
        /// </summary>
        protected string _guidXmlToken = "guid";

        /// <summary>
        /// 
        /// </summary>
        protected Dictionary<string, RssItem> _dictRssItemDict = new Dictionary<string, RssItem>();

        /// <summary>
        /// 
        /// </summary>
        protected string _uniqueID = null;

        /// <summary>
        /// 
        /// </summary>
        protected string _guidEnterToken = Guid.NewGuid().ToString();
        #endregion

        #region ctors.
        /// <summary>
        /// 
        /// </summary>
        BaseRssReader()
        {
            ///
            this._updatingTimer.Elapsed += new System.Timers.ElapsedEventHandler(UpdatingTimer_Elapsed);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendor"></param>
        /// <param name="uri"></param>
        public BaseRssReader(string vendor, Uri uri)
            : this()
        {
            this._vendor = vendor;
            this._uri = uri;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendor"></param>
        /// <param name="uri"></param>
        /// <param name="interval"></param>
        public BaseRssReader(string vendor, Uri uri, int interval)
            : this(vendor, uri)
        {
            this._updatingInterval = interval;
        }
        #endregion

        #region main.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdatingTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (this._isRuningUpdatingLocker)
            {
                if (this._isRuningUpdating) { return; }

                Update();
                this._isRuningUpdating = false;
            }
        }

        /// <summary>
        /// Upate rss from server.
        /// </summary>
        private void Update()
        {
            try
            {
                this._isRuningUpdating = true;
                System.Net.WebClient wc = new System.Net.WebClient();
                byte[] bytes = wc.DownloadData(this._uri.AbsoluteUri);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes, 0, bytes.Length);
                string xml = new System.IO.StreamReader(ms, this.GetEncoding(bytes)).ReadToEnd();
                if (string.IsNullOrEmpty(xml) == false)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xml);
                    XmlNodeList xnLst = xmlDoc.SelectNodes("//rss/channel/item");
                    lock (this._dictRssItemDict)
                    {
                        IList<RssItem> newItems = new List<RssItem>();
                        foreach (XmlNode xn in xnLst)
                        {
                            string key = xn[this._guidXmlToken].InnerText;
                            if (string.IsNullOrEmpty(key) == false)
                            {
                                if (this._dictRssItemDict.ContainsKey(key) == false)
                                {
                                    RssItem ri = GetItem(xn);
                                    if (ri != null)
                                    {
                                        this._dictRssItemDict.Add(key, ri);
                                        newItems.Add(ri);
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }

                        this.OnNewItemsCaptured(newItems);
                    }
                }
            }
            catch (Exception e)
            {
                OnNewExceptionAccured(e, this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void OnNewItemsCaptured(IList<RssItem> lst)
        {
            if (lst.Count > 0 && this.NewItemsCaptured != null)
            {
                this.NewItemsCaptured(this, new NewItemsCapturedEnventArgs(lst));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="from"></param>
        protected void OnNewExceptionAccured(Exception exp, object from)
        {
            if (this.NewExceptionAccured != null)
            {
                this.NewExceptionAccured(this, new ExceptionEventArgs(exp, from));
            }
        }
        #endregion

        #region virtual.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        protected virtual System.Text.Encoding GetEncoding(byte[] bytes)
        {
            if (bytes != null)
            {
                string content = System.Text.Encoding.Default.GetString(bytes);
                if (content != null)
                {
                    int index = content.IndexOf("encoding=\"");
                    if (index != -1)
                    {
                        try
                        {
                            content = content.Substring(index + "encoding=\"".Length);
                            string enc = content.Substring(0, content.IndexOf("\""));
                            return System.Text.Encoding.GetEncoding(enc);
                        }
                        catch { }
                    }
                }
            }

            return System.Text.Encoding.UTF8;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        protected virtual void GetImageAndDescription(string html, ref string description, Dictionary<string, Image> images)
        {
            try
            {
                if (string.IsNullOrEmpty(html) == false)
                {
                    ///Get images.                    
                    string pattern = "<img[^>]+src=\"(?'ImageUrl'[^\"]+\\.[a-z\\d]{3,4})\"[^>]+>";
                    MatchCollection mc = CommonUtil.GetMatchCollection(pattern, html);
                    string guid = null;
                    foreach (Match mt in mc)
                    {
                        ///New guid.
                        guid = Guid.NewGuid().ToString();

                        ///Get Image.                        
                        images.Add(guid, CommonUtil.GetWebImage(mt.Groups["ImageUrl"].ToString()));

                        ///Replace img by guid.
                        html = html.Replace(mt.Groups[0].Value.ToString(), guid);
                    }
                    
                    ///Replace html token by enter.
                    html = html.Replace("<p>", "  ");
                    html = html.Replace("</p>", this._guidEnterToken);
                    html = html.Replace("<br >", this._guidEnterToken);
                    html = html.Replace("<br>", this._guidEnterToken);
                    html = html.Replace("<br/>", this._guidEnterToken);
                    html = html.Replace("<br />", this._guidEnterToken);
                    description = CommonUtil.TrimHtml(html).Replace(this._guidEnterToken, "\n").TrimEnd('\n');

                    ///Check whether the last content is image or not.
                    if (guid != null && description.Substring(description.Length-guid.Length) == guid)
                    {
                        description += "\n\t  \t";
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        protected virtual void PreProcessing(ref string xml)
        {
            ///Nothing to do.
        }

        /// <summary>
        /// Default rss item capture method.
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        protected virtual RssItem GetItem(XmlNode xmlNode)
        {
            if (xmlNode != null)
            {
                string description = null;
                Dictionary<string, Image> images = new Dictionary<string, Image>();
                this.GetImageAndDescription(CommonUtil.ReplaceSpecialChars(xmlNode["description"].InnerText.Trim()), ref description, images);
                return RssItemFactory.Create(this.GetItemUri(xmlNode),
                                             this.GetItemGuid(xmlNode),
                                             this.GetItemDateTime(xmlNode),
                                             this.GetItemTitle(xmlNode),
                                             this.GetItemVendor(xmlNode),
                                             description,
                                             images);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        protected virtual string GetItemVendor(XmlNode xmlNode)
        {
            return this.Vendor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        protected virtual Uri GetItemUri(XmlNode xmlNode)
        {
            try
            {
                return new System.Uri(xmlNode["link"].InnerText.Trim());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        protected virtual string GetItemGuid(XmlNode xmlNode)
        {
            try
            {
                return xmlNode[this._guidXmlToken].InnerText.Trim();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        protected virtual DateTime GetItemDateTime(XmlNode xmlNode)
        {
            try
            {
                return DateTime.Parse(xmlNode["pubDate"].InnerText.Trim());
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        protected virtual string GetItemTitle(XmlNode xmlNode)
        {
            try
            {
                return xmlNode["title"].InnerText.Trim();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region override.
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Rss Reader - " + this._vendor + " - " + this._dictRssItemDict.Count;
        }
        #endregion

        #region implements IRssReader.
        /// <summary>
        /// Implements IRssReader.
        /// </summary>
        public event EventHandler<NewItemsCapturedEnventArgs> NewItemsCaptured;

        /// <summary>
        /// Auto loading.
        /// </summary>
        public bool AutoLoad
        {
            get
            {
                return this._autoLoad;
            }
        }


        /// <summary>
        /// The vendor.
        /// </summary>
        public string Vendor
        {
            get
            {
                return this._vendor;
            }
        }

        /// <summary>
        /// The uri.
        /// </summary>
        public Uri Uri
        {
            get
            {
                return this._uri;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int UpdatingInterval
        {
            get
            {
                return this._updatingInterval;
            }

            set
            {
                this._updatingInterval = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UniqueID
        {
            get
            {
                return this._vendor.ToUpper();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string GuidXmlToken
        {
            get
            {
                return this._guidXmlToken;
            }

            set
            {
                this._guidXmlToken = value;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual IList<RssItem> GetLatestRssItems(int count)
        {
            lock (this._dictRssItemDict)
            {
                int itemCount = this._dictRssItemDict.Count;
                int start = itemCount - count;
                int counter = 0;
                IList<RssItem> list = new List<RssItem>();
                this._dictRssItemDict = this._dictRssItemDict.OrderBy(x => x.Value.DateTime).ToDictionary(x => x.Key, x => x.Value);
                foreach (KeyValuePair<string, RssItem> kp in this._dictRssItemDict)
                {
                    counter++;
                    if (counter > start)
                    {
                        list.Add(kp.Value);
                    }
                }

                return list;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual IList<RssItem> GetRssItemsAfter(DateTime after)
        {
            lock (this._dictRssItemDict)
            {
                IList<RssItem> list = new List<RssItem>();
                foreach (KeyValuePair<string, RssItem> kp in this._dictRssItemDict)
                {
                    if (kp.Value.DateTime > after)
                    {
                        list.Add(kp.Value);
                    }
                }

                return list;
            }
        }

        /// <summary>
        /// Start updating.
        /// </summary>
        public void Start()
        {
            ///Stop firstly.
            this.Stop();

            ///Then start.
            this._updatingTimer.Interval = this._updatingInterval;
            this._updatingTimer.Start();
        }

        /// <summary>
        /// Stop updating.
        /// </summary>
        public void Stop()
        {
            this._updatingTimer.Stop();
        }
        #endregion

        #region implements IException.
        /// <summary>
        /// Implements IException.
        /// </summary>
        public event EventHandler<ExceptionEventArgs> NewExceptionAccured;
        #endregion
    }
}
