using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MyLibrary;

namespace ARKNIGHTS
{
    class Program
    {
        
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(1000);
            Point p = new Point(567,804);
            Color c = WindowsAPI.GetPixelEx(new IntPtr(197340), p);
            Console.WriteLine("{0} {1} {2}", c.R,c.B,c.G);
        }
    }
}
