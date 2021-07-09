using System;

namespace Minesweeper_Engine_outset
{
    class Sweeper
    {
        Board board;

        public void Run()
        {
            board = new Board();
            if (board.hwnd == new IntPtr(0))
            {
                return;
            }

            Start();//开局
            Console.WriteLine(2333);
            
            int Count1 = 0;
            int Count2 = 0;
            bool Count_ = false;
            while (true)
            {
                //一级算法
                while (Force1())
                {
                    Count1++;
                    Console.WriteLine("一级算法:" + Count1);
                }
                //Print();
                //Print_();
                //二级算法
                Force2 F2 = new Force2(board);
                Count_ = F2.Run();
                Count2++;
                Console.WriteLine("二级算法:" + Count2 + "||" + Count_);
                if (!Count_)
                {
                    break;
                }

            }
        }

        public bool Force1()
        {
            bool IsContinue = false;//是否要继续
            for (int y = 0; y < board.heigh; y++)
            {
                for (int x = 0; x < board.width; x++)
                {
                    if (board.game[x, y].value > 0)
                    {
                        //方块未被清除
                        if (!board.game[x, y].IsCleared)
                        {
                            //排雷
                            if (board.game[x, y].value == getRoundFlags(board, x, y))
                            {
                                setRoundBlanks(x, y);
                                board.game[x, y].IsCleared = true;
                                IsContinue = true;
                            }
                            //插旗
                            else if (getRoundBlanks(board,x,y) == board.game[x,y].value - getRoundFlags(board,x,y))
                            {
                                setRoundFlags(x, y);
                                board.game[x, y].IsCleared = true;
                                IsContinue = true;
                            }
                        }
                    }
                    //清除
                    else if (board.game[x, y].value == 0 || board.game[x,y].value == -2)
                    {
                        board.game[x, y].IsCleared = true;
                    }
                }
            }

            return IsContinue;
        }
        public void Start()
        {
            board.p.Restart();

            Random r = new Random();
            int x = r.Next(0, board.width);
            int y = r.Next(0, board.heigh);
            board.p.Click(board, x, y);
            if (board.game[x, y].value == 0)
            {
                return;
            }
            //踩到雷 重来
            else if(board.game[x, y].value == -3)
            {
                Start();
                return;
            }

            while (board.game[x, y].value != 0)
            {
                x = r.Next(0, board.width);
                y = r.Next(0, board.heigh);
                board.p.Click(board, x, y);

                //踩到雷 重来
                if (board.game[x, y].value == -3)
                {
                    Start();
                    return;
                }
            }
        }
        private void setRoundBlanks(int x, int y)
        {
            for (int b = y - 1; b < y + 2; b++)
            {
                for (int a = x - 1; a < x + 2; a++)
                {
                    if (Check(board, a, b))
                    {
                        if (board.game[a, b].value == -1)
                        {
                            board.p.Click(board, a, b);
                        }
                    }
                }
            }
        }
        public static int getRoundBlanks(Board board, int x, int y)
        {
            int count = 0;
            for (int b = y - 1; b < y + 2; b++)
            {
                for (int a = x - 1; a < x + 2; a++)
                {
                    if (Check(board, a, b))
                    {
                        if (board.game[a, b].value == -1)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
        private void setRoundFlags(int x, int y)
        {
            for (int b = y - 1; b < y + 2; b++)
            {
                for (int a = x - 1; a < x + 2; a++)
                {
                    if (Check(board, a, b))
                    {
                        if (board.game[a, b].value == -1)
                        {
                            board.p.Flag(board, a, b);
                            board.game[a, b].IsCleared = true;
                        }
                    }
                }
            }
        }
        public static int getRoundFlags(Board board, int x, int y)
        {
            int count = 0;
            for (int b = y - 1; b < y + 2; b++)
            {
                for (int a = x - 1; a < x + 2; a++)
                {
                    if (Check(board,a, b))
                    {
                        if (board.game[a, b].value == -2)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
        //检查是否越界
        public static bool Check(Board board, int x, int y)
        {
            if (x < board.width && x >= 0)
            {
                if (y < board.heigh && y >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        public void Print()
        {
            for (int y = 0; y < board.heigh; y++)
            {
                string line = "";
                for (int x = 0; x < board.width; x++)
                {
                    if (board.game[x, y].value > 0)
                    {
                        line += board.game[x, y].value + " ";
                    }
                    else if (board.game[x, y].value == -1)
                    {
                        line += "白";
                    }
                    else if (board.game[x, y].value == -2)
                    {
                        line += "旗";
                    }
                    else if (board.game[x, y].value == -3)
                    {
                        line += "雷";
                    }
                    else if (board.game[x, y].value == 0)
                    {
                        line += "无";
                    }
                    else if (board.game[x, y].value == -250)
                    {
                        line += "ER";
                    }
                }
                Console.WriteLine(line);
            }
        }
        public void Print_()
        {
            Console.WriteLine();
            for (int i = 0; i < board.width; i++)
            {
                Console.Write(i);
                if (i < 10)
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
            for (int y = 0; y < board.heigh; y++)
            {
                for (int x = 0; x < board.width; x++)
                {
                    if (board.game[x, y].IsCleared)
                    {
                        Console.Write("C ");
                    }
                    else
                    {
                        Console.Write("F ");
                    }
                }
                Console.WriteLine(y);
            }
        }
    }
}
