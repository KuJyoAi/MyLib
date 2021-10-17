using System;
using System.Drawing;

namespace Minesweeper_Engine_outset
{
    class Pixel
    {
        IntPtr DC;
        IntPtr hwnd;

        int width;
        int heigh;
        Rect r = new Rect();
        public Pixel(IntPtr target,int width, int heigh)
        {
            hwnd = target;
            this.width = width;
            this.heigh = heigh;
            ScannerHwnd.GetWindowRect(hwnd, out r);
            DC = GetDC(target);
        }

        int StopTime = 20;//全局延时 初级8 高级18

        const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下
        const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下
        const int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起

        //获取值
        /*public int getValue(int x, int y)
        {
            //System.Threading.Thread.Sleep(5);
            //坐标转换公式:x * 16 + 3
            int X = x * 16 + 3;
            int Y = y * 16 + 3;

            Color temp = MyLibrary.WindowsAPI.GetPixelEx(hwnd, new Point(X + 9, Y + 12));
            if (temp.ToArgb() == Color.FromArgb(0, 0, 255).ToArgb())
            {
                return 1;
            }
            else if (temp.ToArgb() == Color.FromArgb(0, 128, 0).ToArgb())
            {
                return 2;
            }
            else if (temp.ToArgb() == Color.FromArgb(255, 0, 0).ToArgb())
            {
                return 3;
            }
            else if (temp.ToArgb() == Color.FromArgb(0, 0, 128).ToArgb())
            {
                return 4;
            }
            else if (temp.ToArgb() == Color.FromArgb(128,0,0).ToArgb())
            {
                return 5;
            }
            else if (temp.ToArgb() == Color.FromArgb(0,128,128).ToArgb())
            {
                return 6;
            }
            else if (temp.ToArgb() == Color.FromArgb(0,0,0).ToArgb())
            {
                temp = MyLibrary.WindowsAPI.GetPixelEx(hwnd, new Point(X + 1, Y + 1));
                if (temp.ToArgb() == Color.FromArgb(255, 255, 255).ToArgb())
                {
                    return -2;
                }
                else if (temp.ToArgb() == Color.FromArgb(255, 0, 0).ToArgb())
                {
                    return -3;
                }
                else
                {
                    return 7;
                }
            }
            else if (temp.ToArgb() == Color.FromArgb(128,128,128).ToArgb())
            {
                return 8;
            }
            else if (temp.ToArgb() == Color.FromArgb(192,192,192).ToArgb())
            {
                temp = MyLibrary.WindowsAPI.GetPixelEx(hwnd, new Point(X, Y));
                if (temp.ToArgb() == Color.FromArgb(255, 255, 255).ToArgb())
                {
                    return -1;
                }
                else return 0;
            }

            Console.WriteLine("GetValue ERROR");
            Console.WriteLine("({0},{1})", x, y);
            Console.WriteLine("X:{0},Y:{1}", X, Y);
            Console.WriteLine("hwnd:" + hwnd);
            temp = MyLibrary.WindowsAPI.GetPixelEx(hwnd, new Point(X + 9, Y + 12));
            Console.WriteLine("RGB:{0},{1},{2}", temp.R, temp.G, temp.G);
            temp = MyLibrary.WindowsAPI.GetPixelEx(hwnd, new Point(X + 1, Y + 1));
            Console.WriteLine("RGB:{0},{1},{2}", temp.R, temp.G, temp.G);
            temp = MyLibrary.WindowsAPI.GetPixelEx(hwnd, new Point(X, Y));
            Console.WriteLine("RGB:{0},{1},{2}", temp.R, temp.G, temp.G);

            return -250;
        }*/
        //值算法
        public int getValue(int x, int y)
        {
            //System.Threading.Thread.Sleep(5);
            //坐标转换公式:x * 16 + 3
            int X = x * 16 + 3;
            int Y = y * 16 + 3;

            int temp = GetPixel(DC, new Point(X + 9, Y + 12));
            if (temp == 16711680)
            {
                return 1;
            }
            else if (temp == 32768)
            {
                return 2;
            }
            else if (temp == 255)
            {
                return 3;
            }
            else if (temp == 8388608)
            {
                return 4;
            }
            else if (temp == 128)
            {
                return 5;
            }
            else if (temp == 8421376)
            {
                return 6;
            }
            else if (temp == 0)
            {
                temp = GetPixel(DC, new Point(X + 1, Y + 1));
                if (temp == 16777215)
                {
                    return -2;
                }
                else if (temp == 255)
                {
                    return -3;
                }
                else
                {
                    return 7;
                }
            }
            else if (temp == 8421504)
            {
                return 8;
            }
            else if (temp == 12632256)
            {
                temp = GetPixel(DC, new Point(X, Y));
                if (temp == 16777215)
                {
                    return -1;
                }
                else return 0;
            }

            Console.WriteLine("GetValue ERROR");
            Console.WriteLine("({0},{1})", x, y);
            Console.WriteLine("X:{0},Y:{1}", X, Y);
            Console.WriteLine("hwnd:" + hwnd);

            return -250;
        }
        //点击和插旗 注意不能移动窗口
        public void Click(Board board,int x, int y)
        {
            //绝对坐标 变换为窗口相对坐标 并且将之移动到中心
            //int X = x * 16 + 6 + 8;
            //int X = x * 16 + 14 + r.Left;
            //int Y = y * 16 + 14 + r.Top;
            SetCursorPos(x * 16 + 11 + r.Left, y * 16 + 11 + r.Top);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            //由于此处扫雷程序的反应时间问题 导致需要延时
            System.Threading.Thread.Sleep(StopTime);
            board.game[x, y].value = getValue(x, y);

            if (board.game[x,y].value == 0)
            {
                Updata(board);
            }
        }
        public void Flag(Board board, int x, int y)
        {
            //已经是旗帜了 防止误点(延时问题)
            if (getValue(x,y) == -2)
            {
                return;
            }
            board.game[x, y].value = -2;
            board.game[x, y].IsCleared = true;
            SetCursorPos(x * 16 + 11 + r.Left, y * 16 + 11 + r.Top);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }
        public void Updata(Board board)
        {
            for (int y = 0; y < heigh; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    board.game[x, y].value = getValue(x, y); 
                }
            }
        }

        //重新开始
        public void Restart()
        {
            SetCursorPos((r.Left + r.Right) / 2, r.Top - 27);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SetCursorPos(int x, int y);
        //取屏幕像素点
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern int GetPixel(IntPtr hdc, Point p);
        //取设备场景句柄
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);
    }
}
