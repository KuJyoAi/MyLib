using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MyLibrary;
using System.IO;

namespace Dynamic_Wallpaper
{
    public partial class Window : Form
    {
        static IntPtr Desktop;//桌面句柄
        //委托
        public delegate bool Del(IntPtr hWnd, int lParam);
        [DllImport("user32.dll", EntryPoint = "EnumWindows")]
        //委托类型即函数
        static extern bool EnumWindow(Del del, int lParam);
        //回调函数
        static bool CallBack(IntPtr hWnd,int lParam)
        {
            //窗口类名
            if (WindowAPI.GetClassNameEx(hWnd) == "WorkerW")
            {
                //窗口标题
                if (WindowAPI.GetWindowTextEx(hWnd) == "")
                {
                    Desktop = hWnd;//赋值将到最后一个
                }
            }
            return true;
        }

        public Window()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RandomMode();
        }
        void RandomMode()
        {
            //隐藏主窗口
            Visible = false;
            Hide();
            WindowState = FormWindowState.Minimized;
            Size = new Size(0, 0);

            ShowWindow.stretchToFit = true;//自动缩放视频大小
            ShowWindow.Size = new Size(1920, 1080);
            ShowWindow.Location = new Point(0, 0);

            //寻找桌面窗口句柄
            Del del = new Del(CallBack);
            EnumWindow(del, 0);

            //设置到桌面
            WindowAPI.SetParent(ShowWindow.Handle, Desktop);

            //随机文件播放
            string[] Files = Directory.GetFiles(@"D:\Music\Music Video", "*.*");
            //Console.WriteLine(Files[0]);
            Random R = new Random();

            //能取0 但不能取Files.Length
            ShowWindow.URL = Files[R.Next(0, Files.Length)];
        }
    }
}
