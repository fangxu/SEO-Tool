using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SEO_Tool.Utils
{
    class Alexa
    {
        /*
 * 
 * 接口一：
http://data.alexa.com/data/+wQ411en8000lA?cli=10&dat=snba&ver=7.0&cdt=alx_vw=20&wid=12206&act=00000000000&ss=1680x1050&bw=964&t=0&ttl=35371&vis=1&rq=4&url=http://www.baidu.com

接口二：
http://data.alexa.com/data/TCaX/0+qO000fV?cli=10&dat=snba&ver=7.0&cdt=alx_vw=20&wid=31472&act=00000000000&ss=1024x768&bw=639&t=0&ttl=4907&vis=1&rq=23&url=http://www.baidu.com

接口三：
http://data.alexa.com/data/ezdy01DOo100QI?cli=10&dat=snba&ver=7.0&cdt=alx_vw=20&wid=16865&act=00000000000&ss=1024x768&bw=775&t=0&ttl=1125&vis=1&rq=2&url=http://www.baidu.com

接口四：
http://data.alexa.com/data/+wQ411en8000lA?cli=10&dat=snba&ver=7.0&cdt=alx_vw=20&wid=12206&act=00000000000&ss=1680x1050&bw=964&t=0&ttl=35371&vis=1&rq=4&url=http://www.baidu.com
         */
        public static Dictionary<String, String> getAlexa(String url)
        {
            Dictionary<String, String> data = new Dictionary<String, String>();
            String html = NetUtils.GetHtml(@"http://data.alexa.com/data/+wQ411en8000lA?cli=10&dat=snba&cdt=alx_vw=20&wid=12206&act=00000000001&ss=1680x1050&bw=964&t=0&ttl=35371&vis=1&rq=4&url="
                + url, Encoding.UTF8);
            //<ADDR STREET="hangzhou dianzi university" CITY="hangzhou" STATE="zhejiang" ZIP="" COUNTRY="china"/>
            Match m = Regex.Match(html, @"<ADDR STREET=""([\s\S]*?)"" CITY=""([\s\S]*?)"" STATE=""([\s\S]*?)"" ZIP=""([\s\S]*?)"" COUNTRY=""([\s\S]*?)"" />");
            if (m.Success)
            {
                String addr = m.Groups[1].Value + "," +
                    m.Groups[2].Value + " " +
                    m.Groups[3].Value + "，" +
                    m.Groups[4].Value + "," +
                    m.Groups[5].Value;
                data.Add("addr", addr);
                //data.Add("city", m.Groups[2].Value);
                //data.Add("state", m.Groups[3].Value);
                //data.Add("zip", m.Groups[4].Value);
                //data.Add("country", m.Groups[5].Value);
            }
            m = Regex.Match(html, @"<OWNER NAME=""([\s\S]*?)""/>");
            if (m.Success)
            {
                data.Add("owner", m.Groups[1].Value);
            }
            //<EMAIL ADDR="wuyuans@126.com"/>
            m = Regex.Match(html, @"<EMAIL ADDR=""([\s\S]*?)""/>");
            if (m.Success)
            {
                data.Add("email", m.Groups[1].Value);
            }
            //<LINKSIN NUM="11"/>
            m = Regex.Match(html, @"<LINKSIN NUM=""([\s\S]*?)""/>");
            if (m.Success)
            {
                data.Add("linksin", m.Groups[1].Value);
            }
            //TITLE="Wuyuan's Blog" DESC="个人博客，记录自己学习的过程，分享生活中的快乐。"
            m = Regex.Match(html, @"<SITE[\s\S]*?TITLE=""([\s\S]*?)"" DESC=""([\s\S]*?)""");
            if (m.Success)
            {
                data.Add("title", m.Groups[1].Value);
                data.Add("desc", m.Groups[2].Value);
            }
            //<POPULARITY URL="wuyuans.com/" TEXT="3590147" SOURCE="panel"/>
            m = Regex.Match(html, @"<POPULARITY[\s\S]*?TEXT=""([\s\S]*?)""");
            if (m.Success)
            {
                data.Add("rank", m.Groups[1].Value);
            }
            //<REACH RANK="2938193"/>
            m = Regex.Match(html, @"<REACH RANK=""([\s\S]*?)""");
            if (m.Success)
            {
                data.Add("reach", m.Groups[1].Value);
            }
            //<RANK DELTA="-1510597"/>
            m = Regex.Match(html, @"<RANK DELTA=""([\s\S]*?)""");
            if (m.Success)
            {
                data.Add("delta", m.Groups[1].Value);
            }
            //<COUNTRY CODE="CN" NAME="China" RANK="181721"/>
            m = Regex.Match(html, @"<COUNTRY[\s\S]*?NAME=""([\s\S]*?)"" RANK=""([\s\S]*?)""");
            if (m.Success)
            {
                data.Add("country", m.Groups[1].Value);
                data.Add("countryRank", m.Groups[2].Value);
            }
            return data;
        }
    }
}
