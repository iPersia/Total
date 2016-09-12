namespace Nzl.Web.Util
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text.RegularExpressions;
    using Nzl.Repository;

    /// <summary>
    /// Util class.
    /// </summary>
    public static class CommonUtil
    {
#if (DEBUG)
        /// <summary>
        /// Show error message.
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowMessage(string msg)
        {
            System.Diagnostics.Debug.WriteLine("************************Diagonostic Message Start************************");
            System.Diagnostics.Debug.WriteLine(msg);
            System.Diagnostics.Debug.WriteLine("*************************Diagonostic Message End*************************");
        }


        /// <summary>
        /// Show error message.
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowMessage(object obj, string msg)
        {
            System.Diagnostics.Debug.WriteLine("************************Diagonostic Message Start************************");
            if (obj != null)
            {
                string name = obj.GetType().ToString();
                System.Diagnostics.Debug.WriteLine("DateTime: " + DateTime.Now.TimeOfDay.ToString());
                System.Diagnostics.Debug.WriteLine("Class: " + name.Substring(name.LastIndexOf(".") + 1));
                System.Diagnostics.Debug.WriteLine("\tFull name:" + name + "\n\t\tHashCode:" + obj.GetHashCode().ToString());
            }

            System.Diagnostics.Debug.WriteLine(msg);
            System.Diagnostics.Debug.WriteLine("*************************Diagonostic Message End*************************");
        }
#endif
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patter"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetMatch(string pattern, string content)
        {
            return GetMatch(pattern, content, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patter"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetMatch(string pattern, string content, string group)
        {
            try
            {
                if (string.IsNullOrEmpty(pattern) == false && string.IsNullOrEmpty(content) == false)
                {
                    Regex regex = new Regex(pattern);
                    Match mt = regex.Match(content);
                    if (mt.Success)
                    {
                        return mt.Groups[group].Value.ToString();
                    }
                }

                return null;
            }
            catch (Exception e)
            {
#if (DEBUG)
                ShowMessage(typeof(CommonUtil), e.Message);
#endif
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patter"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetMatch(string pattern, string content, int groupIndex)
        {
            try
            {
                if (string.IsNullOrEmpty(pattern) == false && string.IsNullOrEmpty(content) == false)
                {
                    Regex regex = new Regex(pattern);
                    Match mt = regex.Match(content);
                    if (mt.Success)
                    {
                        return mt.Groups[groupIndex].Value.ToString();
                    }
                }

                return null;
            }
            catch (Exception e)
            {
#if (DEBUG)
                ShowMessage(typeof(CommonUtil), e.Message);
#endif
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patter"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static MatchCollection GetMatchCollection(string pattern, string content)
        {
            try
            {
                if (string.IsNullOrEmpty(pattern) == false && string.IsNullOrEmpty(content) == false)
                {
                    Regex regex = new Regex(pattern);
                    return regex.Matches(content);
                }

                return null;
            }
            catch (Exception e)
            {
#if (DEBUG)
                ShowMessage(typeof(CommonUtil), e.Message);
#endif
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="patter"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static IList<string> GetMatchList(string pattern, string content)
        {
            return GetMatchList(pattern, content, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patter"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static IList<string> GetMatchList(string pattern, string content, int groupIndex)
        {
            try
            {
                if (string.IsNullOrEmpty(pattern) == false && string.IsNullOrEmpty(content) == false)
                {
                    Regex regex = new Regex(pattern);
                    MatchCollection mtCollection = regex.Matches(content);
                    if (mtCollection != null)
                    {
                        IList<string> mtList = new List<string>();
                        foreach (Match mt in mtCollection)
                        {
                            mtList.Add(mt.Groups[groupIndex].Value.ToString());
                        }

                        return mtList;
                    }
                }

                return null;
            }
            catch (Exception e)
            {
#if (DEBUG)
                ShowMessage(typeof(CommonUtil), e.Message);
#endif
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool IsMatch(string pattern, string content)
        {
            if (string.IsNullOrEmpty(pattern) == false && string.IsNullOrEmpty(content) == false)
            {
                try
                {
                    Regex regex = new Regex(pattern);
                    Match mt = regex.Match(content);
                    return mt.Success;
                }
                catch (Exception e)
                {
#if (DEBUG)
                    ShowMessage(typeof(CommonUtil), e.Message);
#endif
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetUrlBase(string url)
        {
            if (string.IsNullOrEmpty(url) == false)
            {
                url = url.Replace("%2E", ".");
                url = url.Replace("%5F", "_");
                int pos = url.IndexOf("?");
                if (pos > 0)
                {
                    return url.Substring(0, pos);
                }

                return url;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public static void OpenUrl(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (System.Exception e)
            {
#if (DEBUG)
                ShowMessage(typeof(CommonUtil), e.Message);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ReplaceSpecialChars(string content)
        {
            string[] dst = new string[]{"!", 
                                        "\"",
                                        "%",
                                        "&",
                                        "&",
                                        "\"", 
                                        "(", 
                                        ")", 
                                        "*", 
                                        "+", 
                                        ",", 
                                        "-", 
                                        ".", 
                                        "/", 
                                        ":", 
                                        ";", 
                                        "<", 
                                        "=", 
                                        ">", 
                                        "?", 
                                        "@", 
                                        "[", 
                                        "\\", 
                                        "]", 
                                        "^", 
                                        "_", 
                                        "`", 
                                        "｛", 
                                        "|", 
                                        "｝", 
                                        "~",
                                        ">",
                                        "&",
                                        "<",
                                        " ",
                                        "“",
                                        "”",
                                        " ",
                                        "\""};

            string[] src = new string[]{"&#33;",
                                        "&#34;",
                                        "&#37;",
                                        "&#38;",
                                        "&#038;",
                                        "&#39;",
                                        "&#40;",
                                        "&#41;",
                                        "&#42;",
                                        "&#43;",
                                        "&#44;",
                                        "&#45;",
                                        "&#46;",
                                        "&#47;",
                                        "&#58;",
                                        "&#59;",
                                        "&#60;",
                                        "&#61;",
                                        "&#62;",
                                        "&#63;",
                                        "&#64;",
                                        "&#91;",
                                        "&#92;",
                                        "&#93;",
                                        "&#94;",
                                        "&#95;",
                                        "&#96;",
                                        "&#123;",
                                        "&#124;",
                                        "&#125;",
                                        "&#126;",
                                        "&gt;", 
                                        "&amp;",
                                        "&lt;",
                                        "&nbsp;",
                                        "&ldquo;",
                                        "&rdquo;",
                                        "&#160;",
                                        "&quot;"};

            if (content != null)
            {
                for (int i = 0; i < src.Length; i++)
                {
                    content = content.Replace(src[i], dst[i]);
                }

                return content;
            }

            return null;
        }

        public delegate void VoidDelegate();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string TryCatchExcute(VoidDelegate del)
        {
            try
            {
                del();
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 此私有方法从一段HTML文本中提取纯文本
        /// </summary>
        /// <param name="instr">HTML代码</param>
        /// <returns>纯文本</returns>
        public static string TrimHtml(string html)
        {
            if (html != null)
            {
                html = new Regex(@"(?m)<script[^>]*>(\w|\W)*?</script[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(html, "");
                html = new Regex(@"(?m)<style[^>]*>(\w|\W)*?</style[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(html, "");
                html = new Regex(@"(?m)<select[^>]*>(\w|\W)*?</select[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(html, "");
                //html = new Regex(@"(?m)<a[^>]*>(\w|\W)*?</a[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(html, "");
                html = new Regex(@"<[^<>]+>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(html, "");
                Regex objReg = new System.Text.RegularExpressions.Regex("(<[^>]+?>)|&nbsp;", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                html = objReg.Replace(html, "");
                Regex objReg2 = new System.Text.RegularExpressions.Regex("(\\s)+", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                html = objReg2.Replace(html, " ");
            }

            return html;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object Clone(object obj)
        {
            try
            {
                object clonedObj = null;
                if (obj != null)
                {
                    BinaryFormatter Formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
                    MemoryStream stream = new MemoryStream();
                    Formatter.Serialize(stream, obj);
                    stream.Position = 0;
                    clonedObj = Formatter.Deserialize(stream);
                    stream.Close();
                }

                return clonedObj;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get web image.
        /// </summary>
        /// <param name="url">The image url.</param>
        /// <returns>The image.</returns>
        public static Image GetWebImage(string url)
        {
            try
            {
                Image image = Repository.GetValue<Image>(url);
                if (image == null)
                {
                    using (WebDownload wd = new WebDownload())
                    {
                        byte[] bytes = wd.DownloadData(url);
                        if (bytes != null)
                        {
                            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes, 0, bytes.Length);
                            image = Image.FromStream(ms);
                            if (image != null)
                            {
                                Repository.Add<Image>(url, image);
                            }
                        }                        
                    }
                }

                return image;
            }
            catch
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class WebDownload : WebClient
    {
        private int _timeout;
        /// <summary>
        /// 超时时间(毫秒)
        /// </summary>
        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }

        public WebDownload()
        {
            this._timeout = 60000;
        }

        public WebDownload(int timeout)
        {
            this._timeout = timeout;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var result = base.GetWebRequest(address);
            result.Timeout = this._timeout;
            return result;
        }
    }
}
