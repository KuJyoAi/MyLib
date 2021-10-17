using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace 九宫格填色问题
{
    class Program
    {
        /*
        颜色用正数表示
        0为空
        -1为越界
        */
        static int rect = 3;
        static int ways = 6;//颜色数
        static int[,] plane = new int[3,3];
        static void Main(string[] args)
        {
            run();
            //print();
            //Console.WriteLine(isTrue(1,0));
        }
        static void print()
        {
            for (int y = 0; y  < rect; y ++)
            {
                for (int x = 0; x < rect; x++)
                {
                    Console.Write(plane[x,y] + " ");
                }
                Console.WriteLine();
            }
        }
        static bool run()
        {
            Point nextPoint = getNext();
            if (nextPoint.X == -1)
            {
                return true;
            }

            for (int i = 1; i <= ways; i++)
            {
                plane[nextPoint.X, nextPoint.Y] = i;
                if (isTrue(nextPoint.X, nextPoint.Y))
                {
                    if (run())
                    {
                        return true;
                    }
                }
                else
                {
                    plane[nextPoint.X, nextPoint.Y] = 0;
                }
            }
            return false;
        }
        //判断
        static bool isTrue(int x, int y)
        {
            int tmp = getColor(x, y);
            for (int a = x - 1; a <= x + 1; a++)
            {
                for (int b = y - 1; b <= y + 1; b++)
                {
                    if (getColor(a,b) == tmp)
                    {
                        if (a != x && b != y)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        //防止越界
        static int getColor(int x, int y)
        {
            if (x >= 0)
            {
                if (y >= 0 )
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
        //取下一个
        static Point getNext()
        {
            for (int x = 0; x < rect; x++)
            {
                for (int y = 0; y < rect; y++)
                {
                    if (plane[x, y] == 0)
                    {
                        return new Point(x, y);
                    }
                }
            }
            return new Point(-1, -1);
        }
    }
}
