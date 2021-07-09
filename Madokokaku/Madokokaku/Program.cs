using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Madokokaku
{
    class Program
    {
        static string[] AllFile = File.ReadAllLines(@"E:\Everythings\Games\Galgame\Madokokaku\公式.txt", Encoding.UTF8);
        static string[] result = new string[AllFile.Length];
        static string[] Base = new string[AllFile.Length];
        static string[] Remain = new string[AllFile.Length];
        static void Main(string[] args)
        {
            for (int i = 0; i < AllFile.Length; i++)
            {
                result[i] = MyLibrary.Text.GetLeft(AllFile[i], "=");
                Base[i] = MyLibrary.Text.GetMiddle(AllFile[i], "=", "+");
                Remain[i] = MyLibrary.Text.GetRight(AllFile[i], "+");
                //Console.WriteLine(result[i]);
            }

            string Name = Console.ReadLine();
            Start(Name);
            Console.ReadKey();
        }
        static void Start(string Name)
        {
            /*
            展开法
            例如
            4=2+2

            2=1+1
            2=1+1
            把子项依次展开 数量成倍数增长
            */
            List<int> Coll = new List<int>();
            Coll.Add(Search(Name));//第一次
            bool IsContinue = false;//是否继续
            int tmp = 0;//临时
            Console.WriteLine(AllFile[Coll[0]]);

            int SIndex = 0;//开始索引
            int L = 1;//长度
            do
            {
                IsContinue = false;
                for (int i = SIndex; i < SIndex + L; i++)
                {
                    tmp = -1;
                    //防止越界
                    if (Coll[i] != -1)
                    {
                        tmp = Search(Base[Coll[i]]);
                    }
                    //找到则继续
                    if (tmp != -1)
                    {
                        IsContinue = true;
                        Console.WriteLine(AllFile[tmp]);
                    }
                    Coll.Add(tmp);

                    //防止越界
                    if (Coll[i] != -1)
                    {
                        tmp = Search(Remain[Coll[i]]);
                    }
                    //找到则继续
                    if (tmp != -1)
                    {
                        IsContinue = true;
                        Console.WriteLine(AllFile[tmp]);
                    }
                    Coll.Add(tmp);
                }
                //Console.WriteLine("Index:{0}\tL:{1}", SIndex, L);
                SIndex = SIndex + L;
                L = L * 2;
                Console.WriteLine();
            } while (IsContinue);


        }
        /*static void Start(string Name)
        {
            if (!IsContinue)
            {
                return;
            }
            IsContinue = false;
            int pos = Search(Name);
            Console.WriteLine(AllFile[pos]);

            int Tmp = Search(Base[pos]);
            Console.WriteLine(Tmp);
            if (Tmp != -1)
            {
                IsContinue = true;
                Console.WriteLine(AllFile[Tmp]);
                Start(Base[Tmp]);
            }

            Tmp = Search(Remain[pos]);
            Console.WriteLine(Tmp);
            if (Tmp != -1)
            {
                IsContinue = true;
                Console.WriteLine(AllFile[Tmp]);
                Start(Remain[Tmp]);
            }
        }*/
        static int Search(string Name)
        {
            for (int i = 0; i < result.Length; i++)
            {
                if (Name == result[i])
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
