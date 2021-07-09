using System;
using System.Runtime.InteropServices;

namespace Minesweeper_Engine_outset
{
    static class ScannerHwnd
    {
        static IntPtr Final;
        static int count;

        //寻找窗口句柄
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string ipClassName, string ipWindowName);

        //遍历子窗口
        [DllImport("user32.dll")]
        private static extern int EnumChildWindows(IntPtr hWndParent, Callback lpfn);

        public static IntPtr GetTarget(out int width, out int heigh)
        {
            IntPtr Father = FindWindow("TMain", "Minesweeper Arbiter ");
            //Console.WriteLine(Father);

            Callback c = new Callback(Check);
            EnumChildWindows(Father, c);

            //赋值高宽
            Rect r = new Rect();
            GetWindowRect(Final,out r);
            width = (r.Right - r.Left - 6) / 16;
            heigh = (r.Bottom - r.Top - 6) / 16;

            return Final;
        }

        //委托
        private static bool Check(IntPtr hwnd)
        {
            Final = hwnd;
            //Console.WriteLine(Final);
            count++;
            //经实验证明 第三个为目标窗口
            if (count == 3)
            {
                return false;
            }
            return true;    
        }
        private delegate bool Callback(IntPtr hwnd);

        //取窗口高宽
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, out Rect lpRect);
    }

    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}
