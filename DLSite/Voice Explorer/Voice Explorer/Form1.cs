using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibrary;
using System.Threading;

namespace Voice_Explorer
{
    public partial class form1 : Form
    {
        string netSource = "";
        ThreadStart NetReadTs;

        public form1()
        {
            InitializeComponent();
        }

        private void form1_Load(object sender, EventArgs e)
        {
            initial();
        }
        //初始化函数
        private void initial()
        {
            comboBox1.SelectedItem = "eatasmr";
            NetReadTs = new ThreadStart(readNet);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string X = (string)comboBox1.SelectedItem;
            if (X == "DLSite")
            {
                ads.Text = "";
            }
            else if (X == "eatasmr")
            {
                ads.Text = @"https://eatasmr.com/subject/tong-ren-yin-sheng";
            }
            else if (X == "夜世界")
            {
                ads.Text = @"https://www.gscsds.com/category/tryy";
            }
        }

        private void ads_TextChanged(object sender, EventArgs e)
        {
            
            //Console.WriteLine(netSource);
            
        }
        void readNet()
        {
            Console.WriteLine("read");
            
            netSource = IntNet.getNetSource(ads.Text);
            Console.WriteLine("finish");
            string[] objs = MyLibrary.Text.BatchGetMiddle(netSource, "<a class=\"czr-title\" href", "<div class=\"entry-media__wrapper czr__r-i\">");
            Console.WriteLine("finish");
            Console.WriteLine(objs.Length);
            foreach (var i in objs)
            {
                Console.WriteLine(objs);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Thread th = new Thread(NetReadTs);
            th.Start();

        }
    }
}
