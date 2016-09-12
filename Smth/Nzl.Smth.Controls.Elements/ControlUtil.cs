namespace Nzl.Smth.Controls.Elements
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Nzl.Controls;
    using Nzl.Smth.Datas;
    using Nzl.Smth.Utils;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public static class ControlUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="thread"></param>
        public static void AddContent(RichTextBoxEx richtxtContent, Thread thread)
        {
            if (thread != null && richtxtContent != null)
            {
                Font boldFont = new Font(richtxtContent.Font.FontFamily, richtxtContent.Font.Size, FontStyle.Bold);
                string content = CommonUtil.ReplaceSpecialChars(thread.Content);
                {
                    string tokenPattern = ThreadFactory.TokenPrefix + "(?'Type'[A-Z]+)" + ThreadFactory.TokenSuffix;
                    tokenPattern += "|<b>[^<]*</b>";
                    MatchCollection mtCollection = CommonUtil.GetMatchCollection(tokenPattern, thread.Content);
                    int iconCounter = 0;
                    int imageCounter = 0;
                    int anchorCounter = 0;
                    if (thread.ImageUrls != null || thread.IconUrls != null || thread.Anchors != null)
                    {
                        foreach (Match mt in mtCollection)
                        {
                            string token = mt.Groups[0].Value.ToString();
                            int pos = content.IndexOf(token);
                            string tempContent = content.Substring(0, pos);
                            {
                                //Trim html tag.
                                tempContent = new Regex(@"(?m)<script[^>]*>(\w|\W)*?</script[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(tempContent, "");
                                tempContent = new Regex(@"(?m)<style[^>]*>(\w|\W)*?</style[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(tempContent, "");
                                tempContent = new Regex(@"(?m)<select[^>]*>(\w|\W)*?</select[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(tempContent, "");
                                Regex objReg = new System.Text.RegularExpressions.Regex("(<[.^>]+?>)|&nbsp;", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                                tempContent = objReg.Replace(tempContent, "");
                            }

                            ///Add plain text.
                            {
                                int index = richtxtContent.Text.Length;
                                richtxtContent.AppendText(tempContent);
                                richtxtContent.Select(index, tempContent.Length);
                                richtxtContent.SelectionFont = richtxtContent.Font;
                                richtxtContent.DeselectAll();
                            }

                            ///Cut the content.
                            content = content.Substring(pos + token.Length);

                            ///<b></b>
                            if (mt.Value.Contains("<b>"))
                            {
                                string tVal = mt.Value.Replace("<b>", "").Replace("</b>", "");
                                int index = richtxtContent.Text.Length;
                                richtxtContent.AppendText(tVal);
                                richtxtContent.Select(index, tVal.Length);
                                richtxtContent.SelectionFont = boldFont;
                                richtxtContent.DeselectAll();
                            }

                            //Image
                            if (mt.Groups["Type"].Value.ToString() == ThreadFactory.ImageToken)
                            {
                                string url = thread.ImageUrls[imageCounter++];
#if (DEBUG)
                                System.Diagnostics.Debug.WriteLine(url);
#endif
                                if (thread.Images.ContainsKey(url))
                                {
                                    string data = thread.Images[url].Tag.ToString();
                                    richtxtContent.InsertLink(data, url, richtxtContent.Text.Length);
                                }
                                else
                                {
                                    richtxtContent.InsertLink(RtfUtil.GetRtfCode("图片下载失败"),
                                                              url,
                                                              richtxtContent.Text.Length);
                                }

                            }

                            //Icon
                            if (mt.Groups["Type"].Value.ToString() == ThreadFactory.IconToken)
                            {
                                string url = thread.IconUrls[iconCounter++];
#if (DEBUG)
                                System.Diagnostics.Debug.WriteLine(url);
#endif
                                if (thread.Images.ContainsKey(url))
                                {
                                    richtxtContent.InsertImage(thread.Icons[url]);
                                }
                                else
                                {
                                    richtxtContent.InsertLink(RtfUtil.GetRtfCode("图片下载失败"),
                                                              url,
                                                              richtxtContent.Text.Length);
                                }
                            }

                            //Anchor
                            if (mt.Groups["Type"].Value.ToString() == ThreadFactory.AnchorToken)
                            {
                                richtxtContent.InsertLink(RtfUtil.GetRtfCode(thread.Anchors[anchorCounter].Text),
                                                          thread.Anchors[anchorCounter++].Url,
                                                          richtxtContent.Text.Length);
                            }
                        }
                    }

                    ///Add plain text.
                    {
                        int index = richtxtContent.Text.Length;
                        richtxtContent.AppendText(content);
                        richtxtContent.Select(index, content.Length);
                        richtxtContent.SelectionFont = richtxtContent.Font;
                        richtxtContent.DeselectAll();
                    }

                    ///Colored the replied thread content.
                    {
                        string text = richtxtContent.Text;
                        string replayPattern = @"【 在 [a-zA-z][a-zA-Z0-9]{1,11} (\((.+)?\) )?的大作中提到: 】[^\r^\n]*[\r\n]+(\:.*[\r\n]*)*";
                        MatchCollection mc = CommonUtil.GetMatchCollection(replayPattern, text);
                        if (mc != null && mc.Count > 0)
                        {
                            foreach (Match mt in mc)
                            {
                                string from = mt.Groups[0].Value;
                                int index = richtxtContent.Text.IndexOf(from);
                                if (index >= 0)
                                {
                                    richtxtContent.Select(index, from.Length);
                                    richtxtContent.SelectionColor = Color.FromArgb(96, 96, 96);
                                    richtxtContent.SelectionFont = new Font(richtxtContent.Font.FontFamily, 9, FontStyle.Regular);
                                    richtxtContent.DeselectAll();
                                }
                            }
                        }
                    }

                    ///Colored the From IP.
                    {
                        string text = richtxtContent.Text;
                        string ipPattern = @"--[\r\n]+(修改:[a-zA-z][a-zA-Z0-9]{1,11} FROM (\d+\.){3}(\*|\d+)[\r\n]+)?FROM (\d+\.){3}(\*|\d+)";
                        MatchCollection mc = CommonUtil.GetMatchCollection(ipPattern, text);
                        if (mc != null && mc.Count > 0)
                        {
                            string from = mc[mc.Count - 1].Groups[0].Value;
                            int index = richtxtContent.Text.LastIndexOf(from);
                            if (index >= 0)
                            {
                                richtxtContent.Select(index, from.Length);
                                richtxtContent.SelectionColor = Color.FromArgb(160, 160, 160);
                                richtxtContent.SelectionFont = new Font(richtxtContent.Font.FontFamily, 9, FontStyle.Regular);
                                richtxtContent.DeselectAll();
                            }
                        }
                    }

                    ///Colored the reply tail.
                    {
                        string text = richtxtContent.Text;
                        string repleyContent = SmthUtil.GetReplyText();
                        int index = text.IndexOf(repleyContent);
                        if (index >= 0)
                        {
                            richtxtContent.Select(index, repleyContent.Length);
                            richtxtContent.SelectionFont = new Font(richtxtContent.SelectionFont.FontFamily, 9, FontStyle.Regular);
                            richtxtContent.DeselectAll();
                        }
                    }
                }
            }
        }
    }
}
