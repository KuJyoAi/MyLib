using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

namespace MyLibrary
{
    public class WindowsAPI
    {
        /// <summary>
        /// 设置父窗口
        /// </summary>
        /// <param name="hWndChild">需要置父的窗口</param>
        /// <param name="hWndNewParent">父窗口</param>
        /// <returns>函数成功返回子窗口的原父窗口的句柄;函数失败返回NULL</returns>
        [DllImport("user32.dll", EntryPoint = "SetParent")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        /// <summary>
        /// 寻找窗口句柄
        /// </summary>
        /// <param name="ipClassName">窗口类名,为空则搜索所有</param>
        /// <param name="ipWindowName">窗口标题,为空则搜索所有</param>
        /// <returns>成功返回找到的窗口句柄;失败返回NULL</returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string ipClassName, string ipWindowName);

        /// <summary>
        /// 取指定坐标颜色
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="p">坐标</param>
        /// <returns>RGB颜色值</returns>
        public static System.Drawing.Color GetPixelEx(IntPtr hWnd, System.Drawing.Point p)
        {
            int a = GetPixel(GetDC(hWnd), p);
            //int r = (a & 0xFF);
            //int g = (a & 0xFF00) / 256;
            //int b = (a & 0xFF0000) / 65536;
            //转成ARGB颜色值
            return Color.FromArgb(a & 0xFF, (a & 0xFF00) / 256, (a & 0xFF0000) / 65536);
        }
        //取屏幕像素点
        [DllImport("gdi32.dll")]
        private static extern int GetPixel(IntPtr hdc, System.Drawing.Point p);
        //取设备场景句柄
        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        //取窗口类名
        [DllImport("user32.dll")]
        private static extern void GetClassName(IntPtr hWnd, StringBuilder s, int maxCount);
        //取窗口标题
        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder s, int maxCount);
        /// <summary>
        /// 取窗口类名
        /// </summary>
        /// <param name="HWND">窗口句柄</param>
        /// <param name="MaxCount">窗口类名最大长度,默认20</param>
        /// <returns>返回窗口类名</returns>
        public static string GetClassNameEx(IntPtr HWND,int MaxCount = 20)
        {
            StringBuilder SB = new StringBuilder();
            GetClassName(HWND, SB, MaxCount);

            return SB.ToString();
        }
        /// <summary>
        /// 取窗口标题
        /// </summary>
        /// <param name="HWND">窗口句柄</param>
        /// <param name="MaxCount">窗口标题最大长度,默认20</param>
        /// <returns>返回窗口标题</returns>
        public static string GetWindowTextEx(IntPtr HWND, int MaxCount = 20)
        {
            StringBuilder SB = new StringBuilder();
            GetWindowText(HWND, SB, MaxCount);
            return SB.ToString();
        }
    }
}
