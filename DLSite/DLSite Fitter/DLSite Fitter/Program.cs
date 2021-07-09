using System.IO;
using System.Net;
using System;

namespace DLSite_Fitter
{
    class Program
    {
        static string path = @"E:\Everythings\Temporary Service\Voice";
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
            Console.WriteLine(path);
            //string path = @"E:\Everythings\Temporary Service\Voice\";
            string[] fs = Directory.GetFiles(path);
            //List<int> rj = new List<int>();
            //移动压缩包文件
            int count = 0;
            foreach (var i in fs)
            {
                //string tmp = i.Substring(path.Length);
                //Console.WriteLine(tmp.Substring(3, 6));
                //rj.Add(int.Parse(tmp.Substring(3, 6)));   
                //Console.WriteLine(i.Remove(i.Length - 4));
                string dirName = i.Substring(0, i.LastIndexOf("."));//去扩展名
                //Console.WriteLine(dirName);

                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
                File.Move(i, dirName + "\\" + i.Substring(path.Length));
                //Console.WriteLine(dirName + "\\" + i.Substring(path.Length));
                count++;
            }
            Console.WriteLine("移动{0}个压缩包",count);
            Console.WriteLine("任意键3次联网重命名文件");
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.WriteLine("开始...");
            count = 0;
            string[] dirs = Directory.GetFileSystemEntries(path);
            foreach (var i in dirs)
            {
                //无名字
                if (!isOwnName(i))
                {
                    string url = "https://www.dlsite.com/maniax/work/=/product_id/" + i.Substring(i.IndexOf("[") + 1, 8) + ".html";
                   
                    string tmpName = getName(url);

                    Console.WriteLine(url);
                    //Console.WriteLine(i.IndexOf("]") + 1);
                    Console.WriteLine(getName(url));
                    Console.WriteLine(i.Insert(i.IndexOf("]") + 1, getName(url)));

                    //命名格式为[RJ]NAME[CODE]
                    string newName = i.Insert(i.IndexOf("]") + 1, tmpName);
                    Directory.Move(i, newName);

                    count++;
                }
                //System.Console.WriteLine(fileName);
            }
            Console.WriteLine("重命名{0}个文件",count);
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            //getName("https://www.dlsite.com/maniax/work/=/product_id/RJ277054.html");
        }
        static string getName(string url)
        {
            WebClient wc = new WebClient();
            Stream st = wc.OpenRead(url);
            StreamReader sr = new StreamReader(st);
            string source = sr.ReadToEnd();
            //Console.WriteLine(source.IndexOf("<title>"));
            string title = source.Substring(source.IndexOf("<title>") + "<title>".Length, source.IndexOf("|") - source.IndexOf("<title>") - "<title>".Length);
            /*
            bug修复记录1:"・""."字符无法为路径名,而且前者被中文解析为"?"
            一劳永逸,替换掉同类的.?/\:"*<>|
            */
            //处理下载的名字
            title = title.Replace('・', ' ');
            title = title.Replace('/', ' ');
            title = title.Replace('\\', ' ');
            title = title.Replace('?', ' ');
            title = title.Replace(':', ' ');
            title = title.Replace('/', ' ');
            title = title.Replace('>', ' ');
            title = title.Replace('<', ' ');
            title = title.Replace('|', ' ');
            title = title.Replace('*', ' ');
            return title;
        }
        static bool isOwnName(string name)
        {
            name = name.Substring(name.LastIndexOf(@"\") + 1);
            Console.WriteLine(name);
            if (name == "")
            {
                return false;
            }
            /*int left = name.LastIndexOf("[");
            int right = name.IndexOf("]");
            int leg = left - right - 1;
            
            if (left <= right)
            {
                //没有两个中括号
                leg = right - left + 1;
            }
            Console.WriteLine(left);
            Console.WriteLine(right);
            Console.WriteLine(leg);
            name = name.Remove(left, leg);*/
            int left = name.IndexOf("[");
            int right = name.IndexOf("]");
            int leg = right - left + 1;
            while (left != -1)
            {
                //Console.WriteLine(left);
                //Console.WriteLine(right);
                //Console.WriteLine(leg);
                name = name.Remove(left, leg);
                left = name.IndexOf("[");
                right = name.IndexOf("]");
                leg = right - left + 1;
                //Console.WriteLine(name);
                //Console.WriteLine(left);
                //Console.WriteLine(right);
                //Console.WriteLine(leg);
            }
            //Console.WriteLine(name);
            //name = name.Substring(name.LastIndexOf(@"\") + 1).Substring(name.IndexOf("]") + 1, name.LastIndexOf("[") - name.IndexOf("]") - 1);
            if (name == "")
            {
                return false;
            }
            return true;
        }
    }
}
