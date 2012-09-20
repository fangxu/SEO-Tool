using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SEO_Tool.Utils
{
    class Baidu
    {
        /// <summary>  
        /// 获取google和百度收录数量，为百时同时返回快找时间  
        /// </summary>  

        /// <summary>  
        /// 获取数据  
        /// </summary>  
        /// <param name="u"></param>  
        /// <param name="isGG"></param>  
        /// <param name="isJson">是否返回json格式的数据，否则返回用|分隔的数据</param>  
        /// <returns></returns>  
        public static string Index(string u)
        {
            string rst = "", html = "";
            u = @"http://www.baidu.com/s?wd=site:" + u;
            html = NetUtils.GetHtml(u, Encoding.UTF8);
            if (html != null)
            {//找到约 4,210 条结果  
                Regex r = new Regex(@"找到相关结果数([\d,]+)个", RegexOptions.Compiled);
                Match m = r.Match(html);
                if (m.Success)
                {
                    rst = m.Groups[1].Value;
                    return rst;
                }
            }

            return "-1";
        }
        //http://www.baidu.com/s?wd=domain%3Ayouku.com
        public static String Link(String u)
        {
            string rst = "", html = "";
            u = @"http://www.baidu.com/s?wd=domain%3A" + u;
            html = NetUtils.GetHtml(u, Encoding.UTF8);
            if (html != null)
            {//找到约 4,210 条结果  
                Regex r = new Regex(@"相关结果[约]*([\d,]+)个", RegexOptions.Compiled);
                Match m = r.Match(html);
                if (m.Success)
                {
                    rst = m.Groups[1].Value;
                    return rst;
                }
            }

            return "-1";
        }

        /// <summary>  
        /// 获取百度权重  
        /// </summary>  

        /// <summary>  
        /// 获取权重  
        /// </summary>  
        /// <param name="u"></param>  
        /// <returns></returns>  
        public static string Weight(string u)
        {
            string w = "n";
            if (!string.IsNullOrEmpty(u))
            {
                Uri uri = NetUtils.CreatUri(u);
                if (uri != null)
                {
                    string html = NetUtils.GetHtml("http://www.aizhan.com/getbr.php?url=" + uri.Host.Replace("www.", "") + "&style=1");
                    if (html != null)
                        w = Regex.Match(html, @">([n\d])</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled).Groups[1].Value;
                    if (w == "") w = "n";
                }
            }
            return w;
        }

    }
}
