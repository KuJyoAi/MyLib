using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Voice_Recorder
{
    class Program
    {
        static List<string> paths = new List<string>();
        static List<string[]> dires = new List<string[]>();
        static void Main(string[] args)
        {

            paths.Add(@"E:\Everythings\Temporary Service\Voice");
            paths.Add(@"E:\Everythings\Temporary Service\Voice_test");
            //paths.Add(@"E:\Everythings\Temporary Service\Voice_tmp");
            paths.Add(@"E:\Everythings\Temporary Service\Voice_tmp\EAT");
            paths.Add(@"D:\Everythings\E_tmp_Library\日语听力材料");
            paths.Add(@"D:\Everythings\E_tmp_Library\百合");
            paths.Add(@"E:\Everythings\Temporary Service\YOSEKAI");
            getdires();

            compare();
        }
        static void compare()
        {
            List<string> all = new List<string>();
            List<int> pos = new List<int>();
            List<string> res = new List<string>();
            foreach (var a in dires)
            {
                all.AddRange(a);
                pos.Add(a.Length);
            }

            for (int i = 0; i < all.Count; i++)
            {
                int ct = 0;
                for (int p = i + 1; p < all.Count; p++)
                {
                    if (all[i] == all[p])
                    {
                        ct++;
                        pos.Add(p);
                    }
                }
                if (ct != 0)
                {
                    pos.Add(i);
                    Console.WriteLine("{0}:{1}", all[i], ct);
                }
            }

            
        }
        static void getdires()
        {
            for (int i = 0; i < paths.Count; i++)
            {
                string[] tmp = Directory.GetDirectories(paths[i]);
                int p = tmp[0].LastIndexOf("\\");
                //Console.WriteLine(p);
                for (int a = 0; a < tmp.Length; a++)
                {
                    //Console.WriteLine(tmp[a]);
                    tmp[a] = tmp[a].Substring(p + 4, 6);
                }
                dires.Add(tmp);
            }
        }
    }
}
