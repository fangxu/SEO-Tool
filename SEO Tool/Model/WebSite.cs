using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEO_Tool.Utils;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SEO_Tool.Model
{
    class WebSite
    {
        private Form1 form;
        private int currentStep = 0;
        private const String FAILED = "查询失败";
        private String name;
        private String url;
        private int pr;
        private int sogouRank;
        private int sogouIndex;
        private int googleIndex;
        private int googleLink;
        private int baiduIndex;
        private int baiduLink;
        private AlexaDetail alexa = null;
        private int sosoIndex;
        private int yahooIndex;

        public WebSite(String url, Form1 form)
        {
            Match m = Regex.Match(url, @"^http://(\S*)/*$");
            if (m.Success)
            {
                url = m.Groups[1].Value;
            }
            this.url = url;
            alexa = new AlexaDetail();
            this.form = form;
        }

        void sendP()
        {
            form.BeginInvoke(new Form1.MessageHandler(form.MessageP),
                new object[] { ++currentStep });
        }

        public int Steps
        {
            get
            {
                return 11;
            }
        }
        public void updateSite()
        {
            currentStep = 0;
            this.pr = int.Parse(Google.GetPageRank(url));
            sendP();
            this.googleIndex = int.Parse(trim(Google.Index(url)));
            sendP();
            this.googleLink = int.Parse(trim(Google.Link(url)));
            sendP();
            this.baiduIndex = int.Parse(trim(Baidu.Index(url)));
            sendP();
            this.baiduLink = int.Parse(trim(Baidu.Link(url)));
            sendP();
            this.sogouRank = int.Parse(Sogou.Rank(url));
            sendP();
            this.sogouIndex = int.Parse(trim(Sogou.Index(url)));
            sendP();
            this.sosoIndex = int.Parse(trim(Soso.Index(url)));
            sendP();
            this.yahooIndex = int.Parse(trim(Yahoo.Index(url)));
            sendP();
            Dictionary<String, String> data = Utils.Alexa.getAlexa(url);
            sendP();
            String temp = null;
            if (data.TryGetValue("addr", out temp))
            {
                alexa.Addr = temp;
            }
            if (data.TryGetValue("owner", out temp))
            {
                alexa.Owner = temp;
            }
            if (data.TryGetValue("email", out temp))
            {
                alexa.Email = temp;
            }
            if (data.TryGetValue("linksin", out temp))
            {
                alexa.Linksin = uint.Parse(temp);
            }
            if (data.TryGetValue("addr", out temp))
            {
                alexa.Addr = temp;
            }
            if (data.TryGetValue("title", out temp))
            {
                alexa.Title = temp;
            }
            if (data.TryGetValue("desc", out temp))
            {
                alexa.Desc = temp;
            }
            if (data.TryGetValue("rank", out temp))
            {
                alexa.Rank = uint.Parse(temp);
            }
            if (data.TryGetValue("reach", out temp))
            {
                alexa.Reach = uint.Parse(temp);
            }
            if (data.TryGetValue("delta", out temp))
            {
                alexa.Delta = int.Parse(temp);
            }
            if (data.TryGetValue("country", out temp))
            {
                alexa.Country = temp;
            }
            if (data.TryGetValue("countryRank", out temp))
            {
                alexa.CountryRank = uint.Parse(temp);
            }
            sendP();
        }

        private String trim(String s)
        {
            if (s == null)
            {
                return s;
            }
            Regex r = new Regex(@",");
            return r.Replace(s, "");
        }

        public String YahooIndex
        {
            get
            {
                if (yahooIndex == -1)
                {
                    return FAILED;
                }
                return yahooIndex.ToString();
            }

        }
        public String SosoIndex
        {
            get
            {
                if (sosoIndex == -1)
                {
                    return FAILED;
                }
                return sosoIndex.ToString();
            }
        }

        public String SogouIndex
        {
            get
            {
                if (sogouIndex == -1)
                {
                    return FAILED;
                }
                return sogouIndex.ToString();
            }
        }
        public String BaiduLink
        {
            get
            {
                if (baiduLink == -1)
                {
                    return FAILED;
                }
                return baiduLink.ToString();
            }
        }
        public String GoogleLink
        {
            get
            {
                if (googleLink == -1)
                {
                    return FAILED;
                }
                return googleLink.ToString();
            }
        }
        public SEO_Tool.Model.WebSite.AlexaDetail Alexa
        {
            get { return alexa; }
        }

        public String BaiduIndex
        {
            get
            {
                if (baiduIndex == -1)
                {
                    return FAILED;
                }
                return baiduIndex.ToString();
            }
        }
        public String GoogleIndex
        {
            get
            {
                if (googleIndex == -1)
                {
                    return FAILED;
                }
                return googleIndex.ToString();
            }
        }

        public int SogouRank
        {
            get
            {
                return sogouRank;
            }
        }
        public int Pr
        {
            get
            {
                return pr;
            }
        }
        public System.String Url
        {
            get { return url; }
            set { url = value; }
        }
        public System.String Name
        {
            get { return name; }
            set { name = value; }
        }



        public class AlexaDetail
        {
            private String addr;
            private String owner;
            private String email;
            private uint linksin;
            private String title;
            private String desc;
            private uint rank;
            private uint reach;
            private int delta;
            private String country;
            private uint countryRank;

            public uint CountryRank
            {
                get { return countryRank; }
                set { countryRank = value; }
            }
            public System.String Country
            {
                get { return country; }
                set { country = value; }
            }
            public int Delta
            {
                get { return delta; }
                set { delta = value; }
            }
            public uint Reach
            {
                get { return reach; }
                set { reach = value; }
            }
            public uint Rank
            {
                get { return rank; }
                set { rank = value; }
            }
            public System.String Desc
            {
                get { return desc; }
                set { desc = value; }
            }
            public System.String Title
            {
                get { return title; }
                set { title = value; }
            }
            public uint Linksin
            {
                get { return linksin; }
                set { linksin = value; }
            }
            public System.String Email
            {
                get { return email; }
                set { email = value; }
            }
            public System.String Owner
            {
                get { return owner; }
                set { owner = value; }
            }
            public System.String Addr
            {
                get { return addr; }
                set { addr = value; }
            }
        }
    }
}
