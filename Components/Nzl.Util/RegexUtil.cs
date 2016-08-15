using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Nzl.Utils
{
    /// <summary>
    /// Util for regular expression.
    /// </summary>
    public static class RegexUtil
    {
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
                    return false;
                }
            }

            return false;
        }
    }
}
