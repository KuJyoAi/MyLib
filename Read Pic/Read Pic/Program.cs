using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baidu.Aip;

namespace Read_Pic
{
    class Program
    {
        static void Main(string[] args)
        {
            var image = System.IO.File.ReadAllBytes(@"D:\1.png");
            var client = new Baidu.Aip.Ocr.Ocr("l6lHHKgsP27vGNXUGY28kmp5", "tGXZDDNE0eMiM99qkvWDiohGkuqiimh9 ");
            client.Timeout = 60000;
            Console.WriteLine(client.GeneralBasic(image));
        }
    }
}
