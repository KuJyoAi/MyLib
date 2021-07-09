using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary;
using System.Diagnostics;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Monomial m = new Monomial(1.22);
            string source = File.ReadAllText(@"D:\1.txt");
            List<string> title = new List<string>();
            List<string> link = new List<string>();
            int pos = 0;
            do
            {
                link.Add(MyLibrary.Text.GetMiddle(source, "<a class=\"czr-title\" href=\"", "rel=\"bookmark\" title=\"", pos));
                pos = source.IndexOf("rel=\"bookmark\" title=\"");
                Console.WriteLine(source.IndexOf("rel=\"bookmark\" title=\""));
                title.Add(source.Substring(source.IndexOf("rel=\"bookmark\" title=\"", pos) + 22, 8));
                //title.Add(MyLibrary.Text.GetMiddle(source, "rel=\"bookmark\" title=\"", "\">", pos));
            } while (false);
            Console.WriteLine(title[0]);
            Console.WriteLine(link[0]);

        }
        /*//取屏幕像素点
        [DllImport("gdi32.dll")]
        private static extern int GetPixel(IntPtr hdc, System.Drawing.Point p);
        //取设备场景句柄
        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);
        */
    }
}
