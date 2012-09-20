using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SEO_Tool.Utils
{
    class Sogou
    {
        public static String Index(String url)
        {
            String index = null;
            String u = @"http://www.sogou.com/web?query=site%3A" + url;
            String html = NetUtils.GetHtml(u, Encoding.GetEncoding("GB2312"));
            Match m = Regex.Match(html, @"scd_num"">([\s\S]*?)<");
            if (m.Success)
            {
                index = m.Groups[1].Value;
                return index;
            }
            return "-1";
        }

        public static String Rank(String url)
        {
            //http://rank.ie.sogou.com/sogourank.php?ur=http%3A%2F%2Fwuyuans.com%2F
            String sr = null;
            String u = @"http://rank.ie.sogou.com/sogourank.php?ur=http://" + url+"/";
            String html = NetUtils.GetHtml(u, Encoding.GetEncoding("GB2312"));
            Match m = Regex.Match(html, @"sogourank=(\d*)");
            if (m.Success)
            {
                sr = m.Groups[1].Value;
                return sr;
            }
            return "-1";
        }
    }
}
