using System;
using System.Text;

namespace Eight_Queens
{
    class Game
    {
        int width = 8;
        bool[,] board;
        int line = 0;
        string path = @"D:\IO.txt";

        public Game(int width)
        {
            board = new bool[width, width];
        }
        void Print()
        {
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (board[x, y])
                    {
                        Console.Write("皇");
                    }
                    else
                    {
                        Console.Write("空");
                    }
                }
                Console.WriteLine();
            }
        }
        void Print_()
        {
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (Check(x, y))
                    {
                        Console.Write("可");
                    }
                    else
                    {
                        Console.Write("否");
                    }
                }
                Console.WriteLine();
            }
        }
        public bool Caculate()
        {
            if (line == width)
            {
                //如果存在 则继续
                if (Compare(board))
                {
                    return false;
                }
                //不存在 保存退出
                Save();
                return true;
            }

            for (int i = 0; i < width; i++)
            {
                board[i, line] = true;
                if (Check(i, line))
                {
                    line++;
                    if (Caculate())
                        return true;
                    else
                        line--;
                }
                //恢复原状
                board[i, line] = false;
            }

            return false;
        }
        bool Check(int x, int y)
        {
            //横行
            for (int i = 0; i < width; i++)
            {
                if (board[i, y] && i != x)
                {
                    return false;
                }
            }
            //竖行
            for (int i = 0; i < width; i++)
            {
                if (board[x, i] && i != y)
                {
                    return false;
                }
            }

            //左上右下 x+ y+
            for (int i = 1; x + i < width && y + i < width; i++)
            {
                //因为i != 0 所以不会出现自己的情况
                if (board[x + i, y + i])
                {
                    return false;
                }
            }
            for (int i = 1; x - i >= 0 && y - i >= 0; i++)
            {
                if (board[x - i, y - i])
                {
                    return false;
                }
            }
            //右上左下 x+ y-
            for (int i = 1; x + i < width && y - i >= 0; i++)
            {
                //因为i != 0 所以不会出现自己的情况
                if (board[x + i, y - i])
                {
                    return false;
                }
            }
            for (int i = 1; x - i >= 0 && y + i < width; i++)
            {
                if (board[x - i, y + i])
                {
                    return false;
                }
            }
            return true;

        }
        void Save()
        {
            StringBuilder tmp = new StringBuilder();
            tmp = tmp.AppendLine("");
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (board[x, y])
                    {
                        tmp = tmp.Append('皇');
                    }
                    else
                    {
                        tmp = tmp.Append('空');
                    }
                }
                tmp = tmp.AppendLine("");
            }
            System.IO.File.AppendAllText(path, tmp.ToString());
        }
        //存在返回true 不存在返回false
        bool Compare(bool[,] obj)
        {
            string file = System.IO.File.ReadAllText(path);
            file = file.Replace("\r\n", "");//去换行符
            char[] block = file.ToCharArray();

            bool IsSame = true;//是否相同
            for (int p = 0; p < block.Length; p += 64)
            {
                int pos = 0;

                IsSame = true;
                for (int y = 0; y < width && IsSame; y++)
                {
                    for (int x = 0; x < width && IsSame; x++)
                    {
                        //如果有不相同的 则赋值
                        if (obj[x, y] && block[p + pos] != '皇')
                        {
                            IsSame = false;
                        }
                        pos++;
                    }
                }

                //循环一轮有相同的 即返回true存在
                if (IsSame)
                {
                    return true;
                }
            }

            //循环完全部都没有相同的 返回false不存在
            return false;
        }
    }
}
