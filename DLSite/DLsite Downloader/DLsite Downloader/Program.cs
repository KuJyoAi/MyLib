using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace DLsite_Downloader
{
    class Program
    {
        static string path = @"E:\Everythings\Temporary Service\Voice\";
        static void Main(string[] args)
        {
            Console.WriteLine("输入路径:");
            Console.WriteLine("1:默认路径Voice 2.新音声模式 3.任意模式");
            string set = Console.ReadLine();
            if (set == "2")
            {
                path = @"E:\Everythings\Temporary Service\Voice_test";
            }
            else if (set == "3")
            {
                Console.WriteLine("输入路径");
                path = Console.ReadLine();
            }
            else if (set == "1")
            {
                path = @"E:\Everythings\Temporary Service\Voice";
            }
            else
            {
                Console.WriteLine("error");
                return;
            }

            string[] fs = Directory.GetFileSystemEntries(path);
            int[] rj = new int[fs.Length];
            //rj[i] = int.Parse(fs[i].Substring(fs[i].IndexOf("[RJ") + 3, 6));
            int count = 0;
            foreach (var i in fs)
            {
                if (!File.Exists(i + @"\info\cover.jpg"))
                {
                    Download(i, int.Parse(i.Substring(i.IndexOf("[RJ") + 3, 6)));
                    count++;
                }
            }
            Console.WriteLine("下载{0}个封面",count);
            Console.ReadKey();
        }
        static void Download(string dirName,int rj)
        {
            //图片路径格式:https://img.dlsite.jp/modpub/images2/work/doujin/RJ274000/RJ273917_img_main.jpg
            string url = "https://img.dlsite.jp/modpub/images2/work/doujin/RJ" + (((int)(rj / 1000) + 1) * 1000) + "/RJ" + rj + "_img_main.jpg";
            Console.WriteLine(url);

            if (!Directory.Exists(dirName + @"\info"))
            {
                Directory.CreateDirectory(dirName + @"\info");
            }
            WebClient wc = new WebClient();

            wc.DownloadFile(url, dirName + @"\info\cover.jpg");
        }
    }
}
