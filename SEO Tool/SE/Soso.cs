using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SEO_Tool.Utils
{
    class Soso
    {
        //http://www.soso.com/q?w=site%3Awuyuans.com
        public static String Index(String url)
        {
            String index = null;
            String u = @"http://www.soso.com/q?w=site%3A" + url;
            String html = NetUtils.GetHtml(u, Encoding.GetEncoding("GB2312"));
            Match m = Regex.Match(html, @"搜索到约([\s\S]*?)项结果");
            if (m.Success)
            {
                index = m.Groups[1].Value;
                return index;
            }
            return "-1";
        }
    }
}
