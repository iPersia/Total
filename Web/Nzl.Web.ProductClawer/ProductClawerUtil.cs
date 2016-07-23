namespace Nzl.Web.ProductClawer
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class ProductClawerUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FindNumber(string str)
        {
            if (string.IsNullOrEmpty(str) == false)
            {
                str = str.Replace(",", "");
                Regex regex = new Regex(@"[1-9]\d*\.\d+|0\.\d*[1-9]\d*|[1-9]\d*", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                Match mc = regex.Match(str);
                if (mc.Success)
                {
                    return mc.Groups[0].Value;
                }
            }

            return string.Empty;
        }
    }
}
