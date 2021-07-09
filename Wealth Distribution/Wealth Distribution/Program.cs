using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wealth_Distribution
{
    class Program
    {
        const int Common = 0;
        const int Richer = 1;
        const int Striver = 2;

        static void Main(string[] args)
        {
            rand();
        }
        static void Advanced()
        {
            //初始化
            Person[] Man = new Person[100];

            for (int i = 0; i < Man.Length; i++)
            {
                Man[i] = new Person();
            }

            for (int i = 0; i < 8; i++)
            {
                Man[i].ID = Striver;
            }

            Man[8].ID = Richer;
            Man[8].Money = 300;
            Man[9].ID = Richer;
            Man[9].Money = 300;

            Console.WriteLine("初始化完毕...");
            //开始
            Random R = new Random();
            int pos;
            for (int i = 0; i < 1000; i++)
            {
                for (int x = 0; x < Man.Length; x++)
                {
                    if (Man[x].Money > 0)
                    {
                        //支出
                        Man[x].Money--;
                        //收入
                        pos = R.Next(0, Man.Length);
                        if (Man[pos].ID == Striver)
                        {
                            Man[pos].Money += 1.2;
                            Man[x].Money -= 0.2;
                        }
                        else
                            Man[pos].Money += 1;
                    }
                }
            }

            for (int i = 0; i < Man.Length; i++)
            {
                Console.WriteLine(Man[i].Money);
            }
        }
        static void Game()
        {
            int[] Persons = new int[100];
            for (int i = 0; i < Persons.Length; i++)
            {
                Persons[i] = 100;
            }

            Random R = new Random();

            //x:循环次数
            for (int x = 0; x < 1000000; x++)
            {
                for (int i = 0; i < 100; i++)
                {
                    if (Persons[i] > 0)
                    {
                        Persons[R.Next(0, 100)]++;
                        Persons[i]--;
                    }
                }
            }

            //Array.Sort(Persons);

            for (int x = 0; x < 100; x++)
            {
                Console.Write(Persons[x] + "\t");
            }
        }
        static void rand()
        {
            int[] times = new int[100];
            Random R = new Random();
            for (int x = 0; x < 10000; x++)
            {
                times[R.Next(0, 100)]++;
            }
            for (int x = 0; x < 100; x++)
            {
                Console.Write(x + ":" + times[x] + "\t");
                //Console.WriteLine(times[x]);
            }
        }
    }
    class Person
    {
        //钱
        public double Money = 100;
        //身份
        public int ID = Common;

        public const int Common = 0;
        public const int Richer = 1;
        public const int Striver = 2;
    }
}
