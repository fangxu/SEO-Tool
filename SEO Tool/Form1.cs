using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SEO_Tool.Utils;
using SEO_Tool.Model;
using System.Threading;

namespace SEO_Tool
{
    public partial class Form1 : Form
    {
        private WebSite webSite = null;
        public const int USER = 0x0400;
        public const int WEBSITE_UPDATE = USER + 101;
        Image[] images;
        public delegate void MessageHandler(int p);
        //public MessageHandler updateProc;
        public void MessageP(int p)
        {
            Console.WriteLine(p);
            progressBar1.PerformStep();
        }

        public Form1()
        {
            InitializeComponent();
            this.textBoxDomain.GotFocus += new System.EventHandler(this.textboxDomain_GotFocus);
            progressBar1.Visible = false;
            //updateProc = MessageP;
            images = new Image[] {
            Properties.Resources.pagerank0,
            Properties.Resources.pagerank1,
            Properties.Resources.pagerank2,
            Properties.Resources.pagerank3,
            Properties.Resources.pagerank4,
            Properties.Resources.pagerank5,
            Properties.Resources.pagerank6,
            Properties.Resources.pagerank7,
            Properties.Resources.pagerank8,
            Properties.Resources.pagerank9,
            Properties.Resources.pagerank10,};
        }

        private void textboxDomain_GotFocus(object sender, EventArgs e)
        {
            textBoxDomain.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (buttonQuery.Enabled)
            {
                buttonQuery.Enabled = false;
            }
            webSite = new WebSite(textBoxDomain.Text, this);
            progressBar1.Visible = true;
            progressBar1.Maximum = webSite.Steps;
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            Thread t = new Thread(() =>
            {
                webSite.updateSite();
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    updateResult();
                    buttonQuery.Enabled = true;
                    progressBar1.Visible = false;
                }));
            });
            t.IsBackground = true;
            t.Start();
        }

        private void updateResult()
        {
            int rank = webSite.Pr;
            if (rank == -1)
            {
                textBoxPR.Text = "失败";
                pictureBoxPR.Image = images[0];
            }
            else
            {
                textBoxPR.Text = rank.ToString();
                pictureBoxPR.Image = images[rank];
            }
            rank = webSite.SogouRank;
            if (rank == -1)
            {
                textBoxSogouRank.Text = "失败";
                pictureBoxSR.Image = images[0];
            }
            else
            {
                textBoxSogouRank.Text = rank.ToString();
                pictureBoxSR.Image = images[rank];
            }
            textBoxSogouIndex.Text = webSite.SogouIndex;
            textBoxGoogleIndex.Text = webSite.GoogleIndex;
            textBoxGoogleLink.Text = webSite.GoogleLink;
            textBoxBaiduIndex.Text = webSite.BaiduIndex;
            textBoxBaiduLink.Text = webSite.BaiduLink;
            textBoxSoSoIndex.Text = webSite.SosoIndex;
            textBoxYoudaoIndex.Text = webSite.YoudaoIndex;
            updateAlexa();
        }

        private void updateAlexa()
        {
            textBoxTitle.Text = webSite.Alexa.Title;
            textBoxOwner.Text = webSite.Alexa.Owner;
            textBoxEmail.Text = webSite.Alexa.Email;
            textBoxRank.Text = webSite.Alexa.Rank.ToString();
            textBoxCountryRank.Text = webSite.Alexa.CountryRank.ToString();
            textBoxLink.Text = webSite.Alexa.Linksin.ToString();
            textBoxDesc.Text = webSite.Alexa.Desc;
            textBoxCountry.Text = webSite.Alexa.Country;
            textBoxDelta.Text = webSite.Alexa.Delta.ToString();
            textBoxAdder.Text = webSite.Alexa.Addr;
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }

        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WEBSITE_UPDATE:
                    Console.WriteLine(m.WParam);
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }
    }
}
