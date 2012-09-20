using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace SEO_Tool.Utils
{
    public class NetUtils
    {
        /// <summary>  
        /// 创建URI  
        /// </summary>  
        /// <param name="u"></param>  
        /// <returns></returns>  
        public static Uri CreatUri(string u)
        {
            if (string.IsNullOrEmpty(u)) return null;
            else
            {
                u = u.Trim('/', ' ').ToLower();
                if (!u.StartsWith("http://") && !u.StartsWith("https://")) u = "http://" + u;
                try { return new Uri(u); }
                catch { return null; }
            }
        }
        /// <summary>  
        /// 使用webclient  
        /// </summary>  
        /// <param name="url"></param>  
        /// <returns></returns>  
        public static string GetHtml(string url)
        {
            string html = null;
            WebClient wc = new WebClient();
            try { html = wc.DownloadString(url); }
            catch { }
            wc.Dispose();
            return html;
        }
        /// <summary>  
        /// 使用HttpWebRequest对象  
        /// </summary>  
        /// <param name="url"></param>  
        /// <param name="encoding">编码</param>  
        /// <returns></returns>  
        public static string GetHtml(string url, Encoding encoding)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Timeout = 100 * 1000;
            request.Method = "GET";
            request.MaximumAutomaticRedirections = 1000;
            request.ContentType = "text/html";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,* / *;q=0.8";
            request.AllowAutoRedirect = true;
            request.KeepAlive = true;
            request.ProtocolVersion = HttpVersion.Version10;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:15.0) Gecko/20100101 Firefox/15.0.1";
            request.UseDefaultCredentials = true;
            string html = null;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader srd = new StreamReader(response.GetResponseStream(), encoding))
                    {
                        html = srd.ReadToEnd();
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Data);
            }
            finally
            {
                request.Abort();
            }
            return html;
            /*string html = null;
            WebClient wc = new WebClient();
            wc.Encoding = encoding;
            try { html = wc.DownloadString(url); }
            catch { }
            wc.Dispose();
            return html;*/
        }
    }

}
