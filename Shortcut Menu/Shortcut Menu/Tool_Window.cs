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
using System.Runtime.InteropServices;
using System.Collections;

namespace Shortcut_Menu
{
    public partial class Tool_Window : Form
    {
        [DllImport("shell32.dll")]
        static extern int ExtractIconEx(string lpszFile, int niconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, int nIcons);

        public Tool_Window()
        {
            InitializeComponent();
        }
        //图标框
        ArrayList Col_Picture = new ArrayList();
        //按钮
        ArrayList Col_Button = new ArrayList();
        //安全密码
        string SafePassword = "";
        //当前页数
        int Page = 1;
        //读取文件
        string[] InformationA = File.ReadAllLines(@"D:\Texts\Document\Uncommon\Title Path.txt");
        //最大页数
        int MaxPage;
        //更新图标
        private void UpdateIcon()
        {
            //Picture
            Col_Picture.Add(pictureBox1);
            Col_Picture.Add(pictureBox2);
            Col_Picture.Add(pictureBox3);
            Col_Picture.Add(pictureBox4);
            Col_Picture.Add(pictureBox5);
            Col_Picture.Add(pictureBox6);
            Col_Picture.Add(pictureBox7);
            Col_Picture.Add(pictureBox8);
            Col_Picture.Add(pictureBox9);
            Col_Picture.Add(pictureBox10);
            Col_Picture.Add(pictureBox11);
            Col_Picture.Add(pictureBox12);
            Col_Picture.Add(pictureBox13);
            Col_Picture.Add(pictureBox14);
            Col_Picture.Add(pictureBox15);
            Col_Picture.Add(pictureBox16);
            Col_Picture.Add(pictureBox17);
            Col_Picture.Add(pictureBox18);
            Col_Picture.Add(pictureBox19);
            Col_Picture.Add(pictureBox20);

            IntPtr[] Large = new IntPtr[1];//大图标
            IntPtr[] Small = new IntPtr[1];//小图标
            for (int i = (Page - 1) * 40 + 1, pos = 0; i < InformationA.Length && pos < 20; i += 2, pos++)
            {
                //取图标
                ExtractIconEx(InformationA[i], 0, Large, Small, 1);
                if ((int)Large[0] != 0)
                {
                    (Col_Picture[pos] as PictureBox).Image = Icon.FromHandle(Large[0]).ToBitmap();
                }
                else if ((int)Small[0] != 0)
                {
                    (Col_Picture[pos] as PictureBox).Image = Icon.FromHandle(Small[0]).ToBitmap();
                }
                else
                {
                    //清空图标
                    (Col_Picture[pos] as PictureBox).Image = null;
                }
                //Console.WriteLine("{0}:  L:{1}\tS:{2}", pos, Large[0], Small[0]);
            }


        }
        //更新标题
        private void UpdateTitle()
        {
            //Button
            Col_Button.Add(button1);
            Col_Button.Add(button2);
            Col_Button.Add(button3);
            Col_Button.Add(button4);
            Col_Button.Add(button5);
            Col_Button.Add(button6);
            Col_Button.Add(button7);
            Col_Button.Add(button8);
            Col_Button.Add(button9);
            Col_Button.Add(button10);
            Col_Button.Add(button11);
            Col_Button.Add(button12);
            Col_Button.Add(button13);
            Col_Button.Add(button14);
            Col_Button.Add(button15);
            Col_Button.Add(button16);
            Col_Button.Add(button17);
            Col_Button.Add(button18);
            Col_Button.Add(button19);
            Col_Button.Add(button20);

            for (int i = 0; i < 20; i++)
            {
                (Col_Button[i] as Button).Text = InformationA[(Page - 1) * 40 + i * 2];
            }
        }

        private void Tool_Window_Load(object sender, EventArgs e)
        {
            //控制位置 -7:屏幕最左边   1047:除去任务栏外的屏幕高度
            Location = new Point(-7, 1047 - Size.Height);

            //设置大小
            Size = new Size(400, 866);

            //设置页数
            Text = "Menu" + "(" + Page.ToString() + ")";

            //获取最大页数,/2:分为T和P, /20:一页20个
            MaxPage = InformationA.Length / 2 / 20;

            //Console.WriteLine(MaxPage);
            //处理文本
            StringDispose();
            Updata();
            //隐藏任务栏
            this.ShowInTaskbar = false;
            //避免两个此程序开启
            if (IsExist())
            {
                System.Environment.Exit(0);
            }
        }
        /// <summary>
        /// 判断此进程是否存在于计算机中
        /// </summary>
        /// <returns>存在返回真,不存在返回假<returns>
        private bool IsExist()
        {
            int count = 0;
            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process process in processList)
            {
                //转成大写判断
                if (process.ProcessName.ToUpper() == "SHORTCUT MENU")
                {
                    count++;
                }
            }
            if (count > 1)
            {
                return true;
            }
            return false;
        }
        //处理文本
        private string[] StringDispose()
        {
            for (int i = 0; i < InformationA.Length; i += 1)
            {
                InformationA[i] = InformationA[i].Substring(InformationA[i].IndexOf("=") + 1);
                //Console.WriteLine(InformationA[i]);
            }
            return InformationA;
        }
        //更新
        private void Updata()
        {
            UpdateTitle();
            UpdateIcon();
        }
        //窗口移动
        private void Tool_Window_Move(object sender, EventArgs e)
        {
            Location = new Point(-7, 1047 - Size.Height);
        }
        //按键映射
        private void Tool_Window_KeyPress(object sender, KeyPressEventArgs e)
        {
            //换页
            //Console.WriteLine("Key:" + e.KeyChar);
            if (e.KeyChar == '=' || e.KeyChar == '+')
            {
                Page++;
                //页数大于最大页数则不改变页数
                if (Page > MaxPage)
                {
                    Page--;
                }

                //更新信息
                Text = "Menu" + "(" + Page + ")";
                Updata();
                //Console.WriteLine("Page:" + Page);
            }
            else if (e.KeyChar == '-' || e.KeyChar == '_')
            {
                Page--;
                //页数小于1则不改变页数
                if (Page < 1)
                {
                    Page++;
                }

                //更新信息
                Text = "Menu" + "(" + Page + ")";
                Updata();
                //Console.WriteLine("Page:" + Page); 
            }

            //密码解锁
            else if (e.KeyChar == 'E')
            {
                SafePassword += "E";
                //输错重输
                if (SafePassword.Length > 3)
                {
                    SafePassword = "";
                }
            }
            else if (e.KeyChar == 'I')
            {

                SafePassword += "I";
                if (SafePassword.Length > 3)
                {
                    SafePassword = "";
                }
            }
            else if (e.KeyChar == 'A')
            {
                SafePassword += "A";
                //成功解锁
                if (SafePassword == "EIA")
                {
                    WebCollection Window = new WebCollection();
                    Window.Show();
                }
            }
            
        }
        //取消窗口关闭
        private void Tool_Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Hide();
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            e.Cancel = true;
        }
        //窗口样式改变
        private void Tool_Window_StyleChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                TuoPan.Visible = true;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                ShowInTaskbar = true;
            }
        }
        //托盘右键
        private void TuoPan_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TuoPanMenu.Show(MousePosition);
            }
        }
        //show
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }
        //quit
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //系统退出
            System.Environment.Exit(0);
        }
        //双击
        private void TuoPan_DoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }

        //渣渣代码
        private void button1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 1 * 2 - 1]);
            Process.Start(P);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 2 * 2 - 1]);
            Process.Start(P);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 3 * 2 - 1]);
            Process.Start(P);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 4 * 2 - 1]);
            Process.Start(P);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 5 * 2 - 1]);
            Process.Start(P);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 6 * 2 - 1]);
            Process.Start(P);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 7 * 2 - 1]);
            Process.Start(P);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 8 * 2 - 1]);
            Process.Start(P);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 9 * 2 - 1]);
            Process.Start(P);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 10 * 2 - 1]);
            Process.Start(P);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 11 * 2 - 1]);
            Process.Start(P);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 12 * 2 - 1]);
            Process.Start(P);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 13 * 2 - 1]);
            Process.Start(P);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 14 * 2 - 1]);
            Process.Start(P);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 15 * 2 - 1]);
            Process.Start(P);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 16 * 2 - 1]);
            Process.Start(P);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 17 * 2 - 1]);
            Process.Start(P);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 18 * 2 - 1]);
            Process.Start(P);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 19 * 2 - 1]);
            Process.Start(P);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            ProcessStartInfo P = new ProcessStartInfo(InformationA[(Page - 1) * 40 + 20 * 2 - 1]);
            Process.Start(P);
        }
    }
}