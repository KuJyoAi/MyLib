using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int[,] Board = new int[,]{ {0,0,0,0,0,0,0,0,0 },
                                       {2,0,0,6,0,0,4,0,1 },
                                       {0,0,0,8,0,9,0,0,0 },
                                       {0,7,8,0,0,0,0,3,0 },
                                       {0,0,0,0,1,0,7,0,0 },
                                       {0,0,9,0,0,0,0,0,0 },
                                       {0,0,2,0,0,0,0,0,0 },
                                       {0,0,0,7,0,0,0,8,0 },
                                       {1,0,0,0,4,0,0,0,0 }};
            Suduku S = new Suduku(Board);
            S.solve();
            S.print();
        }
    }
    class Suduku
    {
        //游戏板
        public int[,] Board = new int[9, 9];
        public Suduku(int[,] Game)
        {
            Board = Game;
        }
        //获取下一个需要填的值
        private Point Next()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (Board[y, x] == 0)
                    {
                        return new Point(y, x);
                    }
                }
            }
            return new Point(-1, -1);
        }
        public bool solve()
        {
            print();
            Point Blank = Next();
            //无空位 解决完毕
            if (Blank.X < 0 && Blank.Y < 0)
            {
                return true;
            }

            //开始回溯解决
            for (int i = 1; i < 10; i++)
            {
                //填入值并检查 成功开始填下一个
                Board[Blank.Y, Blank.X] = i;
                if (check(Blank.Y, Blank.X))
                {
                    if (solve())
                    {
                        return true;
                    }
                }
            }
            //所有值都不成立 清除
            Board[Blank.Y, Blank.X] = 0;
            return false;
        }
        public bool check(int y, int x)
        {
            //竖排
            for (int a = 0; a < 9; a++)
            {
                if (Board[a, x] == Board[y, x])
                {
                    if (y != a)
                    {
                        return false;
                    }
                }
            }
            //横排
            for (int a = 0; a < 9; a++)
            {
                if (Board[y, a] == Board[y, x])
                {
                    if (a != x)
                    {
                        return false;
                    }
                }
            }
            //宫格
            return CheckBlock(y, x);
        }
        //检查宫格
        private bool CheckBlock(int y, int x)
        {
            //宫数(从1开始左右数)
            int Order = (y / 3) * 3 + x / 3 + 1;
            //Console.WriteLine("Order:" + Order);
            /*
            宫数对应:Y X
            1:0-2 0-2
            2:0-2 3-5
            3:0-2 6-8
            4:3-5 0-2
            5:3-5 3-5
            6:3-5 6-8
            7:6-8 0-2
            8:6-8 3-5
            9:6-8 6-8
            */
            //范围
            int Y = 0, X = 0;
            switch (Order)
            {
                case 1:
                    break;
                case 2:
                    X = 3;
                    break;
                case 3:
                    X = 6;
                    break;
                case 4:
                    Y = 3;
                    break;
                case 5:
                    Y = 3; X = 3;
                    break;
                case 6:
                    Y = 3; X = 6;
                    break;
                case 7:
                    Y = 6;
                    break;
                case 8:
                    Y = 6; X = 3;
                    break;
                case 9:
                    Y = 6; X = 6;
                    break;
                default:
                    return false;
            }
            //Console.WriteLine("Y:" + Y + " X:" + X);

            //遍历
            for (int b = Y; b < Y + 3; b++)
            {
                for (int a = X; a < X + 3; a++)
                {
                    if (Board[b, a] == Board[y, x])
                    {
                        if ((a != x) && (b != y))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        //打印
        public void print()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    Console.Write(Board[y, x] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
