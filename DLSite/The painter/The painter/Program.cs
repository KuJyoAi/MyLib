using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_painter
{
    class Program
    {
        static int[,] board;
        static int XL = 5;
        static int YL = 5;

        static int xt = -1;
        static int yt = 0;

        static int types = 3;

        static int count = 0;

        static void Main(string[] args)
        {
            board = new int[XL, YL];
            for (int x = 0; x < XL; x++)
            {
                for (int y = 0; y < YL; y++)
                {
                    board[x, y] = new int();
                    board[x, y] = 0;
                }
            }

            //for (int i = 2; i < 11; i++)
            //{
            //    Stopwatch sw = new Stopwatch();
            //    types = i;
            //    sw.Start();
            //    getback();
            //    sw.Stop();
                
            //    Console.WriteLine("types={0} count={1} timecost={2}ms",i,count,sw.ElapsedMilliseconds);
            //    count = 0;
            //}
            getback();
            Console.WriteLine("count={0} types={1} rect={2}*{3}", count, types, XL, YL);
        }
        static void print()
        {
            for (int x = 0; x < XL; x++)
            {
                for (int y = 0; y < YL; y++)
                {
                    Console.Write(board[y, x]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static void next()
        {
            
            xt += 1;
            if (xt == XL)
            {
                xt = 0;
                yt += 1;
            }
        }
        static void last()
        {
            xt--;
            if (xt == -1)
            {
                yt--;
                xt = XL - 1;
            }
            //Console.WriteLine("last:{0},{1}",xt,yt);
        }
        static bool getback()
        {
            //print();
            next();
            //Console.WriteLine("st:{0},{1}",xt,yt);
            //Console.WriteLine();

            if (yt == YL)
            {
                //print();
                //全部填完
                count++;
                last();
                //Console.WriteLine("c+");
                return false;
            }

            int typet = 1;
            board[xt, yt] = typet;
            while (true)
            {
                //Console.WriteLine("for");
                //print();
                if (!(check(xt, yt) && getback()))
                {
                    //下一颜色
                    typet++;
                    if (typet > types)
                    {
                        //颜色涂完了,仍然不行,回溯
                        //清除颜色
                        board[xt, yt] = 0;
                        last();
                        return false;
                    }

                    board[xt, yt] = typet;
                }

            }
        }
        static bool check(int x, int y)
        {
            //int pos = get(x, y);
            //for (int a = x - 1; a < x + 2; a++)
            //{
            //    for (int b = y - 1; b < y + 2; b++)
            //    {
            //        if (pos == get(a,b))
            //        {
            //            if (!(a == x && b == y))
            //            {
            //                return false;
            //            }
            //        }
            //    }
            //}
            if (get(x, y) == get(x - 1, y) || get(x, y) == get(x + 1, y) || get(x, y) == get(x, y - 1) || get(x, y) == get(x, y + 1))
            {
                return false;
            }
            return true;
        }
        static int get(int x, int y)
        {
            if (x < 0 || x >= XL || y < 0 || y >= YL)
            {
                return 0;
            }
            return board[x, y];
        }
    }
}
