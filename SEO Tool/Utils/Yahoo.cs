using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SEO_Tool.Utils
{
    class Yahoo
    {
        //http://www.yahoo.cn/s?q=site%3Atudou.com
        public static String Index(String url)
        {
            String index = null;
            String u = @"http://www.yahoo.cn/s?q=site%3A" + url;
            String html = NetUtils.GetHtml(u, Encoding.UTF8);
            Match m = Regex.Match(html, @"<div class=""s_info"">找到相关网页约([\s\S]*?)条");
            if (m.Success)
            {
                index = m.Groups[1].Value;
                return index;
            }
            return "-1";
        }
    }
}
