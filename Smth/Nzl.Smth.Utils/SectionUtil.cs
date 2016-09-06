namespace Nzl.Smth.Utils
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Nzl.Recycling;
    using Nzl.Smth.Configs;
    using Nzl.Smth.Datas;
    using Nzl.Web.Page;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public class SectionUtil
    {
        /// <summary>
        /// 
        /// </summary>
        public static IList<Section> GetSections(WebPage page)
        {
            IList<Section> list = new List<Section>();
            if (page != null && page.IsGood)
            {
                if (page != null && page.IsGood)
                {
                    string pattern = @"(<li>|<li class=\Whl\W>)"
                                   + @"("
                                   + @"(版面\|<a href=\W(?'BoardUrl'/board/(?'BoardCode'[\w,  \., \-, %2E, %5F]+))\W>(?'BoardName'[^<]+)</a>(\|</a>)?)"
                                   + @"|"
                                   + @"(<font color=\W#f60\W>目录</font>\|<a href=\W(?'SectionUrl'/section/(?'SectionCode'[\w, \., \-, %2E, %5F]+))\W>(?'SectionName'[^<]+)</a>\|(<a href=\W/hot/\d+\W style=\Wcolor:#f00\W>热点</a>)?</a>)"
                                   + @")"
                                   + @"</li>";
                    MatchCollection mtCollection = CommonUtil.GetMatchCollection(pattern, page.Html);
                    if (mtCollection != null)
                    {
                        foreach (Match mt in mtCollection)
                        {
                            string url = Configuration.BaseUrl + mt.Groups[2].Value.ToString();
                            string title = mt.Groups[3].Value.ToString();

                            if (mt.Groups["BoardName"].Value != "")
                            {
                                //Section board = new Section();
                                Section board = RecycledQueues.GetRecycled<Section>();
                                if (board == null)
                                {
                                    board = new Section();
                                }

                                board.Name = mt.Groups["BoardName"].Value.ToString();
                                board.Code = mt.Groups["BoardCode"].Value.ToString();
                                board.IsBoard = true;
                                list.Add(board);
                            }

                            if (mt.Groups["SectionName"].Value != "")
                            {
                                //Section section = new Section();
                                Section section = RecycledQueues.GetRecycled<Section>();
                                if (section == null)
                                {
                                    section = new Section();
                                }

                                section.Name = mt.Groups["SectionName"].Value.ToString();
                                section.Code = mt.Groups["SectionCode"].Value.ToString();
                                section.IsBoard = false;
                                list.Add(section);
                            }
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IList<Board> GetFavorBoards(WebPage page)
        {
            IList<Board> list = new List<Board>();
            if (page != null && page.IsGood)
            {
                if (page != null && page.IsGood)
                {
                    string pattern = @"(<li>|<li class=\Whl\W>)"
                                   + @"("
                                   + @"(版面\|<a href=\W(?'BoardUrl'/board/(?'BoardCode'[\w,  \., \-, %2E, %5F]+))\W>(?'BoardName'[^<]+)</a>(\|</a>)?)"
                                   + @"|"
                                   + @"(<font color=\W#f60\W>目录</font>\|<a href=\W(?'SectionUrl'/section/(?'SectionCode'[\w, \., \-, %2E, %5F]+))\W>(?'SectionName'[^<]+)</a>\|(<a href=\W/hot/\d+\W style=\Wcolor:#f00\W>热点</a>)?</a>)"
                                   + @")"
                                   + @"</li>";
                    MatchCollection mtCollection = CommonUtil.GetMatchCollection(pattern, page.Html);
                    if (mtCollection != null)
                    {
                        foreach (Match mt in mtCollection)
                        {
                            string url = Configuration.BaseUrl + mt.Groups[2].Value.ToString();
                            string title = mt.Groups[3].Value.ToString();
                            if (mt.Groups["BoardName"].Value != "")
                            {
                                Board board = new Board();
                                board.Code = mt.Groups["BoardCode"].Value.ToString();
                                board.Name = mt.Groups["BoardName"].Value.ToString().Replace("(" + board.Code + ")", "");
                                list.Add(board);
                            }
                        }
                    }
                }
            }

            return list;
        }
    }
}
