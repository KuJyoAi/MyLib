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
using MyLibrary;
using System.Net;
using System.Threading;

namespace DLSite_Reader
{
    public partial class Reader : Form
    {
        //bool net = false;
        //string url = "https://eatasmr.com/subject/tong-ren-yin-sheng";
        List<int> thPos = new List<int>();//线程交互用pos,队列
        Thread th;
        ThreadStart ths;
        int clickPos = 0;//点击的picbox的标识
        int page = 1;
        int maxPage = 0;
        string path = @"E:\Everythings\Temporary Service\Voice";
        int widths = 6;
        int heights = 4;
        int wid = 0;
        int hei = 0;
        List<PictureBox> pbs = new List<PictureBox>();
        List<Image> img = new List<Image>();
        //Image def = Image.FromFile(@"E:\Everythings\Temporary Service\Voice\[RJ242035]就寝前の音玉てんこ盛り!01[全年龄向][损坏]\info\cover.jpg");
        Image def;
        List<string> picPos;//cover的位置
        List<string> dires = new List<string>();//目录的位置
        public Reader()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            fileLoad();
            initial();
            //refresh();
            //Console.WriteLine(pbs.Count);
            //初始化线程
            ths = new ThreadStart(copyFileDesktop);
            th = new Thread(ths);
        }
        //载入文件
        void fileLoad()
        {
            picPos = new List<string>();
            picPos.AddRange(Directory.GetFileSystemEntries(path));

            dires = new List<string>();
            dires.AddRange(picPos);

            img = new List<Image>();
            Stream s;
            for (int i = 0; i < picPos.Count; i++)
            {
                picPos[i] += @"\info\cover.jpg";

                //img.Add(Image.FromFile(picPos[i]));//fromFile不释放资源,导致移动文件时被占用,故改用文件流
                s = File.Open(picPos[i], FileMode.Open);
                img.Add(Image.FromStream(s));
                s.Close();

                //Console.WriteLine(picPos[i]);
            }
            s = File.Open(@"E:\Everythings\Temporary Service\Voice\[RJ242035]就寝前の音玉てんこ盛り!01[全年龄向][损坏]\info\cover.jpg", FileMode.Open);
            def = Image.FromStream(s);
            s.Close();

            //补全默认图像
            while (img.Count % (widths * heights) != 0)
            {
                img.Add(def);
            }
            //Console.WriteLine(img.Count);
            maxPage = picPos.Count / (widths * heights) + 1;
        }
        //添加图片框
        void initial()
        {
            //调整窗口大小
            Location = new Point(0, 0);
            wid = (Size.Width - 12) / widths;
            hei = (Size.Height - 36) / heights;
            //图片框对象化
            for (int i = 0; i < widths * heights; i++)
            {
                pbs.Add(new PictureBox());
            }
            int pos = 0;
            //Image ma = Image.FromFile(@"E:\Everythings\Temporary Service\Voice\[RJ263031]癒しの雨、祝福のあしおと[eatasmr.com]\info\cover.jpg");
            for (int y = 0; y < heights; y++)
            {
                for (int x = 0; x < widths; x++)
                {

                    pbs[pos].Name = pos.ToString();
                    pbs[pos].Size = new Size(wid, hei);
                    pbs[pos].Location = new Point(x * wid, y * hei);
                    //Console.WriteLine(x * wid);
                    //Console.WriteLine(y * hei);
                    //pbs[pos].BackgroundImage = ma;

                    //添加事件
                    pbs[pos].Click += new System.EventHandler(pb_click);
                    //添加右键菜单
                    pbs[pos].ContextMenuStrip = rightMain;

                    pbs[pos].BackgroundImageLayout = ImageLayout.Stretch;
                    Controls.Add(pbs[pos]);
                    pos++;
                }
            }
            refresh();
        }
        //以当前页码更新图片框内容
        void refresh()
        {
            Text = "Voice Cover Reader - " + page;
            for (int i = 0; i < widths * heights; i++)
            {
                //pbs[i].BackgroundImage = Image.FromFile(getName((page - 1) * (widths * heights) + i));
                pbs[i].BackgroundImage = img[(page - 1) * (widths * heights) + i];
            }
        }
        //换页
        private void Reader_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '=')
            {
                page++;

                if (page > maxPage)
                {
                    page--;
                }
                refresh();
            }
            else if (e.KeyChar == '-')
            {
                page--;
                if (page < 1)
                {
                    page++;
                }

                refresh();
            }
            else if (e.KeyChar == 'd')
            {
                path = @"D:\Everythings\E_tmp_Library\百合";
                img = new List<Image>();
                page = 1;

                fileLoad();
                //initial();
                refresh();
            }
            else if (e.KeyChar == 'v')
            {
                path = @"E:\Everythings\Temporary Service\Voice";
                img = new List<Image>();
                page = 1;

                fileLoad();
                //initial();
                refresh();
            }
            else if (e.KeyChar == 'n')
            {
                path = @"E:\Everythings\Temporary Service\Voice_test";
                img = new List<Image>();
                page = 1;

                fileLoad();
                //initial();
                refresh();
            }
            else if (e.KeyChar == 't')
            {
                path = @"E:\Everythings\Temporary Service\YOSEKAI";
                img = new List<Image>();
                page = 1;

                fileLoad();
                //initial();
                refresh();
            }

        }
        //点击时取到图片框
        int getpos(object sender)
        {
            PictureBox pbnew = sender as PictureBox;

            int pos = 0;
            for (int i = 0; i < pbs.Count; i++)
            {
                if (pbnew == pbs[i])
                {
                    pos = i;
                }
            }
            pos += (page - 1) * (widths * heights);
            return pos;
        }
        //点击图片框
        void pb_click(object sender, EventArgs e)
        {
            MouseEventArgs ms = (MouseEventArgs)e;
            if (ms.Button == MouseButtons.Left)
            {
                /*PictureBox pbnew = (PictureBox)sender;
                int pos = 0;
                for (int i = 0; i < pbs.Count; i++)
                {
                    if (pbnew == pbs[i])
                    {
                        pos = i;
                    }
                }
                pos += (page - 1) * (widths * heights);*/
                int pos = getpos(sender);

                //Console.WriteLine(pos);
                System.Diagnostics.Process.Start("explorer.exe", picPos[pos].Substring(0, picPos[pos].IndexOf(@"\info\")));
            }


        }

        private void rightMain_Opening(object sender, CancelEventArgs e)
        {
            //Console.WriteLine(558);
            clickPos = int.Parse((sender as ContextMenuStrip).SourceControl.Name);
        }

        private void voiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (path == @"E:\Everythings\Temporary Service\Voice")
            {
                return;
            }

            //E:\Everythings\Temporary Service\Voice

            int pos = (page - 1) * (widths * heights) + clickPos;
            //Console.WriteLine(pos);

            //Console.WriteLine(dires[pos]);

            //移动文件
            string name = dires[pos].Substring(dires[pos].LastIndexOf("\\"));
            //Console.WriteLine(name);
            Directory.Move(dires[pos], @"E:\Everythings\Temporary Service\Voice" + name);
            //Console.WriteLine(Directory.Exists(dires[pos]));

            //移除,取消文件使用
            img.Remove(img[pos]);
            img.Add(def);
            refresh();
            dires.Remove(dires[pos]);
            picPos.Remove(picPos[pos]);
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (path == @"E:\Everythings\Temporary Service\Voice_test")
            {
                return;
            }

            //E:\Everythings\Temporary Service\Voice_test

            int pos = (page - 1) * (widths * heights) + clickPos;
            //Console.WriteLine(pos);

            //Console.WriteLine(dires[pos]);


            

            //移动文件
            string name = dires[pos].Substring(dires[pos].LastIndexOf("\\"));
            //Console.WriteLine(name);
            Directory.Move(dires[pos], @"E:\Everythings\Temporary Service\Voice_test" + name);
            //Console.WriteLine(Directory.Exists(dires[pos]));
            //移除,取消文件使用
            img.Remove(img[pos]);
            img.Add(def);
            refresh();
            dires.Remove(dires[pos]);
            picPos.Remove(picPos[pos]);

            
        }

        //拷贝文件的线程托管函数
        void copyFileDesktop()
        {
            while (thPos.Count != 0)
            {
                string[] fileTmp = Directory.GetFiles(dires[thPos[0]]);
                Console.WriteLine(fileTmp.Length);
                foreach (var f in fileTmp)
                {
                    File.Copy(f, @"C:\Users\ACG\Desktop" + f.Substring(f.LastIndexOf("\\")));
                }
                thPos.RemoveAt(0);
            }
        }

        private void 移动到桌面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //C:\Users\ACG\Desktop

            int pos = (page - 1) * (widths * heights) + clickPos;
            //Console.WriteLine(pos);

            //Console.WriteLine(dires[pos]);
            //发送桌面

            thPos.Add(pos);

            if (!th.IsAlive)
            {
                th.Start();
            }


            //移动文件
            if (path != @"E:\Everythings\Temporary Service\Voice")
            {
                //取消文件使用
                img.Remove(img[pos]);
                refresh();
                
                string name = dires[pos].Substring(dires[pos].LastIndexOf("\\"));
                Directory.Move(dires[pos], @"C:\Users\ACG\Desktop" + name);
            }

            
        }

        private void deCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //E:\Everythings\Temporary Service\Voice_DeCollection
            //D:\Everythings\E_tmp_Library\Voice_DeCollection
            string target;
            if (path.Substring(0, 1) == "E")
            {
                target = @"E:\Everythings\Temporary Service\Voice_DeCollection";
            }
            else
            {
                target = @"D:\Everythings\E_tmp_Library\Voice_DeCollection";
            }

            int pos = (page - 1) * (widths * heights) + clickPos;

            

            //移动文件
            string name = dires[pos].Substring(dires[pos].LastIndexOf("\\"));
            Directory.Move(dires[pos], target + name);

            //移除,取消文件使用
            img.Remove(img[pos]);
            img.Add(def);
            refresh();
            dires.Remove(dires[pos]);
            picPos.Remove(picPos[pos]);

            Console.WriteLine(img.Count);
            Console.WriteLine(dires.Count);
            Console.WriteLine(picPos.Count);
            Console.WriteLine(11);
        }
        //网页模式
        /*void netMode()
        {
            //string source = File.ReadAllText(@"D:\1.txt");
            WebClient wc = new WebClient();
            Stream st = wc.OpenRead(url);
            StreamReader sr = new StreamReader(st);
            string source = sr.ReadToEnd();
            
            List<int> title = new List<int>();
            List<string> link = new List<string>();
            int pos = 0;
            do
            {
                link.Add(MyLibrary.Text.GetMiddle(source, "<a class=\"czr-title\" href=\"", "\" rel=\"bookmark\" title=\"", pos));
                pos = source.IndexOf("rel=\"bookmark\" title=\"", pos);

                string tmp = source.Substring(source.IndexOf("rel=\"bookmark\" title=\"", pos) + 22, 30);
                if (tmp.IndexOf("RJ") != -1)
                {
                    //无RJ号,移除中文音声
                    tmp = tmp.Substring(tmp.IndexOf("RJ") + 2, 6);
                    title.Add(int.Parse(tmp));
                }
                else
                {
                    link.RemoveAt(link.Count - 1);
                }

                pos = source.IndexOf("<a class=\"czr-title\" href=\"", pos) + 22;
            } while (source.IndexOf("<a class=\"czr-title\" href=\"", pos) != -1);

            
            loadPic(title);
            //for (int i = 0; i < title.Count; i++)
            //{
            //    Console.WriteLine(title[i]);
            //    Console.WriteLine(link[i]);
            //}
        }
        void loadPic(List<int> title)
        {
            img = new List<Image>();
            for (int i = 0; i < title.Count; i++)
            {
                //文件存在与否交给下载函数判断
                Download(title[i]);
                img.Add(Image.FromFile(@"D:\Texts\C# Source Code\DLSite\DLSite Reader\DLSite Reader\obj\Release\configure\eatasmr\Cache" + "\\" + title[i] + ".jpg"));
                
            }
            //补全默认图像
            while (img.Count % (widths * heights) != 0)
            {
                img.Add(def);
            }
            refresh();
        }
        static void Download(int rj)
        {
            string dirName = @"D:\Texts\C# Source Code\DLSite\DLSite Reader\DLSite Reader\obj\Release\configure\eatasmr\Cache";
            //图片路径格式:https://img.dlsite.jp/modpub/images2/work/doujin/RJ274000/RJ273917_img_main.jpg
            string url = "https://img.dlsite.jp/modpub/images2/work/doujin/RJ" + (((int)(rj / 1000) + 1) * 1000) + "/RJ" + rj + "_img_main.jpg";
            //Console.WriteLine(url);

            if (!File.Exists(dirName + "\\" + rj + ".jpg"))
            {
                WebClient wc = new WebClient();

                wc.DownloadFile(url, dirName + "\\" + rj + ".jpg");
            }
        }*/
    }
}
