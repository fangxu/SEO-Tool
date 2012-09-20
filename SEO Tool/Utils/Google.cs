using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace SEO_Tool.Utils
{
    public class Google
    {
        #region google pr
        private static string tryMore(string strUrl)
        {
            //string sURL = "http://toolbarqueries.google.com/search?client=navclient-auto&ch=" + GetPR(strUrl) + "&ie=UTF-8&oe=UTF-8&features=Rank&q=info:" + System.Web.HttpUtility.UrlEncode(strUrl);  
            string sURL = "http://toolbarqueries.google.com/tbr?client=navclient-auto&features=Rank&ch="
                + GetPR(strUrl) + "&q=info:"
                + System.Web.HttpUtility.UrlEncode(strUrl);
            string result = NetUtils.GetHtml(sURL);
            if (result != null)
            {
                result = result.Trim();
                if (result.IndexOf(':') > 0)
                {
                    string[] pr = result.Split(':');
                    if (pr.Length == 3) return pr[2].ToString();
                }
            }
            return "0";
        }
        public static string GetPageRank(string strUrl)
        {
            string pr = "0";
            if (!string.IsNullOrEmpty(strUrl))
            {
                Uri u = NetUtils.CreatUri(strUrl);
                if (u != null)
                {
                    string host = u.Host, path = u.PathAndQuery;
                    if (path == "/")
                    {
                        //查询主域名，非路径.其他2级域名只进行一次查询.  
                        //如果是顶级域名或者www2级域名，得到的值为0时，查询www2级域名或者顶级域名，然后再试http://  
                        string[] arr = host.Split('.');
                        if (arr[0] == "www" || arr.Length == 2)
                        {
                            pr = tryMore(host);
                            if (pr == "0")
                            {
                                pr = tryMore(arr.Length == 2 ? "www." + host : host.Replace("www.", ""));
                                if (pr == "0")
                                {
                                    pr = tryMore("http://" + host);
                                    if (pr == "0") pr = tryMore("http://" + (arr.Length == 2 ? "www." + host : host.Replace("www.", "")));
                                }
                            }
                        }
                        else
                        {
                            pr = tryMore(u.Host);
                            if (pr == "0") pr = tryMore("http://" + u.Host);
                        }
                    }
                    else//查询路径  
                    {
                        pr = tryMore(u.Host + path);
                        if (pr == "0") pr = tryMore("http://" + u.Host + path);
                    }
                }
            }
            return pr;
        }
        private static string GetPR(string url)
        {
            url = "info:" + url;
            string ch = GoogleCH(str_asc(url)).ToString();
            ch = "6" + ch;
            return ch;
        }
        private static int[] str_asc(string str)
        {
            if (str == null || str == string.Empty) return null;
            int[] result = new int[str.Length];
            for (int i = 0; i < str.Length; i++) result[i] = (int)str[i];
            return result;
        }
        private static long yiweitwo(long a, long b)
        {
            long z = 0x80000000;
            if ((z & a) != 0)
            {
                a = (a >> 1);
                a &= (~z);
                a |= 0x40000000;
                a = ((int)a >> (int)(b - 1));
            }
            else
            {
                a = ((int)a >> (int)b);
            }
            return a;
        }
        private static int[] yiwei(long a, long b, long c)
        {
            a -= b; a -= c; a ^= (yiweitwo(c, 13));
            b -= c; b -= a; b ^= (a << 8);
            c -= a; c -= b; c ^= (yiweitwo(b, 13));
            a -= b; a -= c; a ^= (yiweitwo(c, 12));
            b -= c; b -= a; b ^= (a << 16);
            c -= a; c -= b; c ^= (yiweitwo(b, 5));
            a -= b; a -= c; a ^= (yiweitwo(c, 3));
            b -= c; b -= a; b ^= (a << 10);
            c -= a; c -= b; c ^= (yiweitwo(b, 15));
            return new int[] { (int)a, (int)b, (int)c };

        }
        private static int GoogleCH(int[] url)
        {
            int length = url.Length;
            long a = 0x9E3779B9;
            long b = 0x9E3779B9;
            long c = 0xE6359A60;
            int k = 0;
            int len = length;
            int[] mid;
            while (len >= 12)
            {
                a += (url[k + 0] + (url[k + 1] << 8) + (url[k + 2] << 16) + (url[k + 3] << 24));
                b += (url[k + 4] + (url[k + 5] << 8) + (url[k + 6] << 16) + (url[k + 7] << 24));
                c += (url[k + 8] + (url[k + 9] << 8) + (url[k + 10] << 16) + (url[k + 11] << 24));
                mid = yiwei(a, b, c);
                a = mid[0]; b = mid[1]; c = mid[2];
                k += 12;
                len -= 12;
            }
            c += length;
            switch (len)
            {
                case 11:
                    {
                        c += (url[k + 10] << 24);
                        c += (url[k + 9] << 16);
                        c += (url[k + 8] << 8);
                        b += (url[k + 7] << 24);
                        b += (url[k + 6] << 16);
                        b += (url[k + 5] << 8);
                        b += (url[k + 4]);
                        a += (url[k + 3] << 24);
                        a += (url[k + 2] << 16);
                        a += (url[k + 1] << 8);
                        a += (url[k + 0]);
                        break;
                    }
                case 10:
                    {
                        c += (url[k + 9] << 16);
                        c += (url[k + 8] << 8);
                        b += (url[k + 7] << 24);
                        b += (url[k + 6] << 16);
                        b += (url[k + 5] << 8);
                        b += (url[k + 4]);
                        a += (url[k + 3] << 24);
                        a += (url[k + 2] << 16);
                        a += (url[k + 1] << 8);
                        a += (url[k + 0]);
                        break;
                    }

                case 9:
                    {
                        c += (url[k + 8] << 8);
                        b += (url[k + 7] << 24);
                        b += (url[k + 6] << 16);
                        b += (url[k + 5] << 8);
                        b += (url[k + 4]);
                        a += (url[k + 3] << 24);
                        a += (url[k + 2] << 16);
                        a += (url[k + 1] << 8);
                        a += (url[k + 0]);
                        break;

                    }

                case 8:
                    {
                        b += (url[k + 7] << 24);
                        b += (url[k + 6] << 16);
                        b += (url[k + 5] << 8);
                        b += (url[k + 4]);
                        a += (url[k + 3] << 24);
                        a += (url[k + 2] << 16);
                        a += (url[k + 1] << 8);
                        a += (url[k + 0]);
                        break;

                    }

                case 7:
                    {

                        b += (url[k + 6] << 16);
                        b += (url[k + 5] << 8);
                        b += (url[k + 4]);
                        a += (url[k + 3] << 24);
                        a += (url[k + 2] << 16);
                        a += (url[k + 1] << 8);
                        a += (url[k + 0]);
                        break;

                    }

                case 6:
                    {
                        b += (url[k + 5] << 8);
                        b += (url[k + 4]);
                        a += (url[k + 3] << 24);
                        a += (url[k + 2] << 16);
                        a += (url[k + 1] << 8);
                        a += (url[k + 0]);
                        break;
                    }

                case 5:
                    {
                        b += (url[k + 4]);
                        a += (url[k + 3] << 24);
                        a += (url[k + 2] << 16);
                        a += (url[k + 1] << 8);
                        a += (url[k + 0]);
                        break;

                    }

                case 4:
                    {
                        a += (url[k + 3] << 24);
                        a += (url[k + 2] << 16);
                        a += (url[k + 1] << 8);
                        a += (url[k + 0]);
                        break;

                    }

                case 3:
                    {
                        a += (url[k + 2] << 16);
                        a += (url[k + 1] << 8);
                        a += (url[k + 0]);
                        break;
                    }
                case 2:
                    {
                        a += (url[k + 1] << 8);
                        a += (url[k + 0]);
                        break;
                    }

                case 1:
                    {
                        a += (url[k + 0]);
                        break;
                    }

            }
            mid = yiwei(a, b, c);
            return mid[2];
        }
        #endregion
        #region google index
        public static string Index(string u)
        {//http://www.google.com/search?hl=zh-CN&q=site%3Awww.coodir.com
            //http://www.google.com.hk/search?hl=zh-CN&q=site:wuyuans.com&fp=27b5666de1cb797a&tch=5&ech=1&psi=x6tZULfSN4WQiQeImYCADw.1348053956837.5
            string rst = "", html = "";
            u = @"http://www.google.com.hk/search?q=site%3A" + u + @"&ie=utf-8&oe=utf-8&aq=t";
            html = NetUtils.GetHtml(u, Encoding.UTF8);
            if (html != null)
            {//找到约 4,210 条结果  
                Regex r = new Regex(@"約有 ([\d|,]*?) 項結果", RegexOptions.Compiled);
                Match m = r.Match(html);
                if (m.Success)
                {
                    rst = m.Groups[1].Value;
                    return rst;
                }
            }
            return "-1";
        }
        #endregion

        #region google link
        public static string Link(string u)
        {//http://www.google.com/search?hl=zh-CN&q=site%3Awww.coodir.com
            string rst = "", html = "";
            u = @"http://www.google.com.hk/search?q=link%3A" + u + @"&ie=utf-8&oe=utf-8&aq=t";
            html = NetUtils.GetHtml(u, Encoding.UTF8);
            if (html != null)
            {//找到约 4,210 条结果  
                Regex r = new Regex(@"約有 ([\d|,]*?) 項結果", RegexOptions.Compiled);
                Match m = r.Match(html);
                if (m.Success)
                {
                    rst = m.Groups[1].Value;
                    return rst;
                }                
            }
            
            return "-1";
        }
        #endregion
    }

}
