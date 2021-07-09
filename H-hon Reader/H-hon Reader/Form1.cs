using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace H_hon_Reader
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        static int Page = 1;
        static int MaxPage = 1;
        static List<PictureBox> Pb = new List<PictureBox>();
        static List<TextBox> Tb = new List<TextBox>();
        static List<string> DirectoryName = new List<string>();//目录名
        static List<Image> Pic = new List<Image>();//图片
        private void Main_Load(object sender, EventArgs e)
        {
            //加入控件
            Pb.Add(pictureBox1);
            Pb.Add(pictureBox2);
            Pb.Add(pictureBox3);
            Pb.Add(pictureBox4);
            Pb.Add(pictureBox5);
            Pb.Add(pictureBox6);
            Tb.Add(textBox1);
            Tb.Add(textBox2);
            Tb.Add(textBox3);
            Tb.Add(textBox4);
            Tb.Add(textBox5);
            Tb.Add(textBox6);

            //找到程序运行路径
            string Path = Process.GetCurrentProcess().MainModule.FileName;
            Path = Path.Substring(0, Path.LastIndexOf('\\'));

            //寻找路径
            Path = @"D:\Everythings\New folder\妹控汉化本合集（多数实妹，93本，3.14G，附预览+目录）\第二弹（73）";
            DirectoryName = Directory.GetDirectories(Path).ToList();
            //寻找图片文件名
            string[] PicFiles;
            for (int i = 0; i < DirectoryName.Count; i++)
            {
                //Console.WriteLine(i + ":" + DirectoryName[i]);
                //防止文件夹里有文件夹 或 没有文件 的情况导致数组越界
                PicFiles = Directory.GetFiles(DirectoryName[i]);
                if (PicFiles.Length != 0)
                {
                    Pic.Add(Image.FromFile(PicFiles[0]));
                }
                else
                {
                    Pic.Add(null);
                }
            }
            MaxPage = DirectoryName.Count / 6 + 1;

            //Console.WriteLine(MaxPage);
            //Console.WriteLine(DirectoryName.Count);
            //更新一次
            UpData();
        }

        //刷新
        void UpData()
        {
            //起始下标,0开始
            int StartIndex = (Page - 1) * 6;
            for (int i = StartIndex; i < StartIndex + 6 && i < DirectoryName.Count; i++)
            {
                //写入标题
                if (DirectoryName[i] != "")
                {
                    Tb[i - StartIndex].Text = DirectoryName[i].Substring(DirectoryName[i].LastIndexOf('\\') + 1);
                    Pb[i - StartIndex].BackgroundImage = Pic[i];
                }
            }
            //更新标题
            this.Text = "Hon Reader" + " - " + Page;
        }

        private void Main_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '=' || e.KeyChar == '+')
            {
                Page++;
                if (Page > MaxPage)
                {
                    Page--;
                }
            }
            else if (e.KeyChar == '-' || e.KeyChar == '_')
            {
                Page--;
                if (Page == 0)
                {
                    Page++;
                }
            }
            UpData();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Main_KeyPress(null, e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            File.Open(Directory.GetFiles(DirectoryName[Page * 6])[0], FileMode.Open);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
    }
}
