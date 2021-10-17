using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

//编程回溯法解决八皇后问题:
namespace Eight_Queens
{
    class Program
    {
        static void Main(string[] args)
        {
            Board B = new Board();
            B.blocks[0, 0] = true;
            B.QueensCount = 1;
            Console.WriteLine(B.Start());
            B.Show();
        }
    }
    class Board
    {
        //横纵
        static int rect = 8;
        public bool[,] blocks = new bool[rect, rect];

        //皇后数
        public int QueensCount = 0;
        //True:有皇后 Flase:空
        public bool IsRight(Point P)
        {
            //横排
            for (int i = 0; i < rect; i++)
            {
                if (blocks[i, P.Y] == true && i != P.X)
                {
                    return false;
                }
            }
            //竖排
            for (int i = 0; i < rect; i++)
            {
                if (blocks[i, P.X] == true && i != P.Y)
                {
                    return false;
                }
            }
            //斜排

            /*
            //找到增加始点和减少始点
            Point IncreaseP = new Point();
            Point ReduceP = new Point();
            //找到增加始点
            if (P.X >= P.Y)
            {
                IncreaseP.X = P.X - P.Y;
                IncreaseP.Y = 0;
            }
            else
            {
                IncreaseP.Y = P.Y - P.X;
                IncreaseP.X = 0;
            }
            //找到减少始点
            if (7 - P.Y >= P.X)
            {
                ReduceP.X = 0;
                ReduceP.Y = P.Y + P.X;
            }
            else
            {
                ReduceP.Y = 7;
                ReduceP.X = P.X - (7 - P.Y);
            }

            //开始检测
            //增加检测
            //Console.WriteLine("IP:{0},{1}", IncreaseP.X, IncreaseP.Y);
            //Console.WriteLine("RP:{0},{1}", ReduceP.X, ReduceP.Y);
            while (IncreaseP.X < 8 && IncreaseP.Y < 8)
            {
                //满足条件
                if (blocks[IncreaseP.X,IncreaseP.Y] == true)
                {
                    //且不为同一格
                    if (!(IncreaseP.X != P.X && IncreaseP.Y != P.Y))
                    {
                        return false;
                    }
                }
                IncreaseP.X++;
                IncreaseP.Y++;
            }
            //减少检测
            while (ReduceP.X > 0 && ReduceP.Y > 0)
            {
                if (blocks[ReduceP.X,ReduceP.Y]== true)
                {
                    if (!(IncreaseP.X != P.X && IncreaseP.Y != P.Y))
                    {
                        return false;
                    }
                }
                ReduceP.X--;
                ReduceP.Y--;
            }
            */

            //都不满足
            return true;
        }
        //回溯
        public bool Start()
        {
            if (QueensCount == 8)
            {
                return true;
            }
                        
            for (int i = 0; i < 8; i++)
            {
                blocks[i, QueensCount] = true;
                QueensCount++;

                //回溯进入下一轮
                if (IsRight(new Point(i, QueensCount)) == true)
                {
                    Start();
                }
                //错误
                else
                {
                    blocks[i, QueensCount] = false;
                    QueensCount--;
                }
            }
            //所有都回溯完了 失败
            return false;
        }
        //显示
        public void Show()
        {
            for (int a = rect - 1; a >= 0; a--)
            {
                for (int i = 0; i < rect ; i++)
                {
                    if (blocks[i, a] == true)
                    {
                        Console.Write("Q");
                    }
                    else
                    {
                        Console.Write("N");
                    }

                    Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
