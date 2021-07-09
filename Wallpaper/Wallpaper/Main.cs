using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;

//使用时记得设置拉伸

namespace Wallpaper
{
    public partial class MainWindow : Form
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        int HowManyFile = 0;//文件数
        String FilePath = @"D:\Texts\Everythings of Desktop\Wallpaper";//文件路径
        /*private void CreateReg()
        {
            try
            {
                //string regPath = @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                //string softwarePath = @"D:\Texts\C# Source Code\Programs\Wallpaper.exe";
                //RegistryKey Key = Registry.LocalMachine;
                //Key.CreateSubKey();
                //RegistryKey SoftWare = Key.OpenSubKey(regPath, true);
                //SoftWare.SetValue("Wallpaper", softwarePath, RegistryValueKind.String);
                //Key.Close();

                RegistryKey Key = Registry.LocalMachine;
                RegistryKey software = Key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true); //该项必须已存在
                software.SetValue("Wallpaper", @"D:\Texts\C# Source Code\Programs\Wallpaper.exe");
                //在HKEY_LOCAL_MACHINE\SOFTWARE\test下创建一个名为“test”，值为“博客园”的键值。如果该键值原本已经存在，则会修改替换原来的键值，如果不存在则是创建该键值。
                // 注意：SetValue()还有第三个参数，主要是用于设置键值的类型，如：字符串，二进制，Dword等等~~默认是字符串。如：
                // software.SetValue("test", "0", RegistryValueKind.DWord); //二进制信息
                Key.Close();
            }
            catch
            {
                
            }
            //return true;
            //RegistryKey runKey = Registry.LocalMachine.OpenSubKey(regPath,true);
            //runKey.SetValue(@"Wallpaper",softwarePath);
            //runKey.Close();
        }*/

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if (!GetPath())
            {
                MessageBox.Show("一张壁纸也没有找到的说QwQ","提示");
            }
            //初始化代码
            Random_RB.Checked = true;
            Random_RB_CheckedChanged(this,null);
            this.WindowState = FormWindowState.Minimized;
            
        }
        private bool GetPath()
        {
            for (int x = 1; true; x++)
            {
                if (!System.IO.File.Exists(FilePath + @"\" + x + ".png"))
                {
                    HowManyFile = x - 1;
                    Console.WriteLine(HowManyFile);
                    break;
                }   
            }
            //如果没有找到文件,则返回false
            if (HowManyFile==0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void Sequence_RB_CheckedChanged(object sender, EventArgs e)
        {
               
        }

        private void Random_RB_CheckedChanged(object sender, EventArgs e)
        {
            if (Random_RB.Checked)
            {
                //Console.WriteLine(GetRandomNumber());
                Timer_Random.Interval = 60000;
            }
        }
        //取随机数
        private string GetRandomNumber()
        {
            int RandomKey = 1;
            Random R = new Random();
            RandomKey = R.Next(1, HowManyFile);
            return RandomKey.ToString();
        }

        private void Timer_Random_Tick(object sender, EventArgs e)
        {
            string path = FilePath + "\\" + GetRandomNumber() + ".png";
            Console.WriteLine(path);
            //设置壁纸,但不可设置样式,只能使用当前默认样式
            SystemParametersInfo(20,1,path,1);
        }

        //设置桌面背景
        [DllImport("User32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(int UAction,int UParam,string IpvParam,int fuWinIni);

        //实现托盘功能
        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                ICO.Visible = true;
                ShowInTaskbar = false;
            }
        }
        //窗口将要关闭时
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {

            //取消关闭窗口
            e.Cancel = true;
            Hide();
            ICO.Visible = true;
            ShowInTaskbar = false;

        }
        //菜单栏事件
        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("程序结束");
            System.Environment.Exit(0);  
        }

        private void ICO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }


        //图片拉伸
        //private void SetPictureSize()
        //{
        //}
    }
}
