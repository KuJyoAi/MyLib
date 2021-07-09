using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 九宫格填色暴力
{
    class Program
    {
        static int rect = 3;
        static int ways = 7 + 1;
        static int[,] plane = new int[3, 3];
        static void Main(string[] args)
        {
            run();

        }
        static void run()
        {
            int count = 0;
            for (int a = 1; a < ways; a++)
            {
                for (int b = 1; b < ways; b++)
                {
                    for (int c = 1; c < ways; c++)
                    {
                        for (int d = 1; d < ways; d++)
                        {
                            for (int e = 1; e < ways; e++)
                            {
                                for (int f = 1; f < ways; f++)
                                {
                                    for (int g = 1; g < ways; g++)
                                    {
                                        for (int h = 1; h < ways; h++)
                                        {
                                            for (int i = 1; i < ways; i++)
                                            {
                                                plane[0, 0] = a;
                                                plane[1, 0] = b;
                                                plane[2, 0] = c;
                                                plane[0, 1] = d;
                                                plane[1, 1] = e;
                                                plane[2, 1] = f;
                                                plane[0, 2] = g;
                                                plane[1, 2] = h;
                                                plane[2, 2] = i;
                                                //print();
                                                if (Real())
                                                {
                                                    //print();
                                                    count++;
                                                    //Console.WriteLine(count);
                                                   
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine(count);
        }
        static void print()
        {
            for (int y = 0; y < rect; y++)
            {
                for (int x = 0; x < rect; x++)
                {
                    Console.Write(plane[x, y] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static bool Real()
        {
            for (int x = 0; x < rect; x++)
            {
                for (int y = 0; y < rect; y++)
                {
                    if (!isTrue(x, y))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //判断
        static bool isTrue(int x, int y)
        {
            int tmp = getColor(x, y);
            if (getColor(x - 1, y) == tmp)
            {
                return false;
            }
            else if (getColor(x + 1, y) == tmp)
            {
                return false;
            }
            else if (getColor(x, y - 1) == tmp)
            {
                return false;
            }
            else if (getColor(x, y + 1) == tmp)
            {
                return false;
            }
            return true;
        }
        //防止越界
        static int getColor(int x, int y)
        {
            if (x >= 0)
            {
                if (y >= 0)
                {
                    if (x < rect)
                    {
                        if (y < rect)
                        {
                            return plane[x, y];
                        }
                    }
                }
            }
            return -1;
        }
    }
}
