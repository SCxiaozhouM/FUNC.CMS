using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace HS.Data.Extensions
{
    public static class WebHelper
    {

        /// <summary>打包返回地址</summary>
        /// <param name="url"></param>
        /// <param name="returnUrl"></param>
        /// <param name="returnKey"></param>
        /// <returns></returns>
        public static String AppendReturn(this String url, String returnUrl, String returnKey = null)
        {
            if (url.IsNullOrEmpty() || returnUrl.IsNullOrEmpty()) return url;

            if (returnKey.IsNullOrEmpty()) returnKey = "r";

            // 如果协议和主机相同，则削减为只要路径查询部分
            if (url.StartsWithIgnoreCase("http") && returnUrl.StartsWithIgnoreCase("http"))
            {
                var uri = new Uri(url);
                var ruri = new Uri(returnUrl);
                if (ruri.Scheme.EqualIgnoreCase(uri.Scheme) && ruri.Host.EqualIgnoreCase(uri.Host)) returnUrl = ruri.PathAndQuery;
            }

            if (url.Contains("?"))
                url += "&";
            else
                url += "?";
            //url += returnKey + "=" + returnUrl;
            url += returnKey + "=" + HttpUtility.UrlEncode(returnUrl);

            return url;
        }

        /// <summary>
        /// 判断是否位null或者空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsNullOrEmpty(this string str)
        {
            return String.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 忽略大小写判断是否以任意一个字符串开头
        /// </summary>
        /// <param name="url"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool StartsWithIgnoreCase(this string url,string str)
        {
            url = url.ToLower();
            str = url.ToLower();
            return url.LastIndexOf(str) == 0;
        }
        /// <summary>
        /// 忽略大小写判断是否跟任意一个字符串相等
        /// </summary>
        /// <param name="url"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool EqualIgnoreCase(this string url, string str)
        {
            url = url.ToLower();
            str = url.ToLower();
            return url== str;
        }
    }
}
