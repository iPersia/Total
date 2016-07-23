namespace Nzl.Web.Page
{
    using System;

    /// <summary>
    /// 链接类
    /// </summary>
    public class HyperLink
    {
        /// <summary>
        /// 链接网址
        /// </summary>
        private string _url;

        /// <summary>
        /// 链接文字
        /// </summary>
        private string _text;

        /// <summary>
        /// 链接网址
        /// </summary>
        public string Url
        {
            get
            {
                return this._url;
            }

            set
            {
                this._url = value;
            }
        }

        /// <summary>
        /// 链接文字
        /// </summary>
        public string Text
        {
            get
            {
                return this._text;
            }

            set
            {
                this._text = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_url"></param>
        /// <param name="_text"></param>
        public HyperLink(string url, string text)
        {
            this._url = url;
            this._text = text;
        }
    }
}
