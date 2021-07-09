using System;

namespace Minesweeper_Engine
{
    //游戏类
    class Game
    {
        const int boom = -1;
        const int blank = 0;
        const int error = -10;
        public block[,] board;

        public int heigh;
        public int width;
        public int boomCount;

        //游戏是否完成
        public bool gameIsEnd()
        {
            int count = boomCount;
            for (int y = 0; y < heigh; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (board[y,x].value == boom)
                    {
                        if (board[y, x].IsFlag)
                        {
                            count--;
                        }
                    }
                }
            }
            if (count == 0)
            {
                return true;
            }
            return false;
        }
        //游戏特征码初始化
        public Game(string key)
        {
            /*
            类型+游戏大小[高宽hw]+雷区
            type1:SS
            size:2 + 2 + 2 * booms(格式为(y,x))注意位置从0起始
            type2:LL
            size:2 + 4 + 4 * booms
            */
            byte[] code = charToByte_S(key.ToCharArray());
            if (code[0] == 54)
            {
                //type1:
                heigh = code[2];
                width = code[3];
                boomCount = (code.Length - 4) / 2;
            }

            //初始化
            board = new block[heigh, width];//y,x
            for (int y = 0; y < heigh; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    board[y, x] = new block(y, x, 0);
                }
            }
            //放雷
            for (int i = 4; i < code.Length; i += 2)
            {
                board[code[i], code[i + 1]].value = boom;
            }
            //填数字
            for (int y = 0; y < heigh; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (board[y, x].value != -1)
                    {
                        board[y, x].value = getBooms(y, x);
                    }
                }
            }
        }
        //导出
        public string getGameHash()
        {
            byte[] ID;
            string result;
            if (heigh < 61 && width < 61)
            {
                //type1:
                ID = new byte[2 + boomCount * 2];
                ID[0] = (byte)heigh;
                ID[1] = (byte)width;

                //雷区编码
                int count = boomCount;
                for (int y = 0; y < heigh; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (board[y, x].value == boom)
                        {
                            //(boomCount - count) * 2 + 2:第N个雷的y值地址
                            ID[(boomCount - count) * 2 + 2] = (byte)y;
                            ID[(boomCount - count) * 2 + 3] = (byte)x;
                            count--;
                        }
                    }
                }

                result = new string(byteToChar_S(ID));
                return "SS" + result;
            }

            return "";
        }
        //编码表转换(typeS)
        private byte[] charToByte_S(char[] key)
        {
            /*
            0-9:0-9|48-57
            a-z:10-35|97-122
            A-Z:36-61|65-90
            S:54
            L:47
            */
            byte[] result = new byte[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] <= 57 && key[i] >= 48)
                {
                    result[i] = (byte)(key[i] - 48);
                }
                else if (key[i] <= 122 && key[i] >= 97)
                {
                    result[i] = (byte)(key[i] - 87);
                }
                else if (key[i] <= 90 && key[i] >= 65)
                {
                    result[i] = (byte)(key[i] - 29);
                }
            }
            return result;
        }
        private char[] byteToChar_S(byte[] key)
        {
            /*
            0-9:0-9|48-57
            a-z:10-35|97-122
            A-Z:36-61|65-90
            S:54
            L:47
            */
            char[] result = new char[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] <= 9)
                {
                    result[i] = (char)(key[i] + 48);
                }
                else if (key[i] >= 10 && key[i] <= 35)
                {
                    result[i] = (char)(key[i] + 87);
                }
                else
                {
                    result[i] = (char)(key[i] + 29);
                }
            }
            return result;
        }
        //随机初始游戏
        public Game(int heigh,int width,int boom)
        {
            //初始化
            this.heigh = heigh;
            this.width = width;
            boomCount = boom;
            randomBoard(heigh, width, boom);
        }
        private void randomBoard(int heigh, int width, int boom)
        {
            board = new block[heigh, width];//y,x
            for (int y = 0; y < heigh; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    board[y, x] = new block(y, x, 0);
                }
            }
            //随机雷
            Random r = new Random();
            for (int i = 0; i < boom; i++)
            {
                int y = r.Next(0, heigh);
                int x = r.Next(0, width);
                if (board[y, x].value == -1)
                {
                    i--;
                }
                board[y, x].value = -1;
            }
            //填数字
            for (int y = 0; y < heigh; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (board[y, x].value != -1)
                    {
                        board[y, x].value = getBooms(y, x);
                    }
                }
            }
        }
        //打印
        public void print()
        {
            string s;
            for (int y = 0; y < heigh; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    s = board[y, x].value.ToString();
                    if (board[y, x].value == -1)
                    {
                        s = "△";
                    }
                    if (board[y, x].IsKnown)
                    {
                        s += "k";
                    }
                    else
                    {
                        s += "u";
                    }
                    if (board[y, x].IsFlag)
                    {
                        s += "F";
                    }
                    if (board[y,x].IsCleared)
                    {
                        s += "c";
                    }
                    Console.Write(s + "\t");
                }
                Console.WriteLine();
            }
        }
        public void print_2()
        {
            //列索引打印
            Console.Write("  ");
            for (int i = 0; i < width; i++)
            {
                Console.Write(i);
                if (i < 10)
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();

            string s;
            for (int y = 0; y < heigh; y++)
            {
                Console.Write(y);
                if (y < 10)
                {
                    Console.Write(" ");
                }
                for (int x = 0; x < width; x++)
                {
                    if (board[y, x].IsKnown == true && board[y, x].value > 0)
                    {
                        s = board[y, x].value.ToString() + " ";//数字
                    }
                    else if (board[y, x].IsKnown == true && board[y, x].value == 0)
                    {
                        s = "无";//空白 已知
                    }
                    else if (board[y, x].IsFlag)
                    {
                        s = "旗";//已插旗
                    }
                    else
                    {
                        s = "空";//未知
                    }
                    Console.Write(s);
                }             
                Console.WriteLine();
            }
        }
        //是否越界
        public bool isException(int y, int x)
        {
            if (y >= 0 && y < heigh)
            {
                if (x >= 0 && x < width)
                {

                    return false;
                }
            }
            return true;
        }
        public int getValue(int y, int x)
        {
            if (!isException(y,x))
            {
                return board[y, x].value;
            }
            return -10;
        }
        public void setFlag(int y, int x)
        {
            if (!isException(y, x))
            {
                board[y, x].IsFlag = true;
            }
        }
        public bool getFlag(int y, int x)
        {
            if (!isException(y, x))
            {
                return board[y, x].IsFlag;
            }
            return false;
        }
        //取周围格雷数
        public int getBooms(int y, int x)
        {
            int count = 0;
            for (int a = y - 1; a < y + 2; a++)
            {
                for (int b = x - 1; b < x + 2; b++)
                {
                    if (getValue(a, b) == -1)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        //点击(区域)
        public void discover(int y, int x)
        {
            board[y, x].IsKnown = true;
            //这样写可以提高效率 但会导致IsClicked的属性只在0上有效 即其他格被点击不会被记录
            if (board[y, x].value == 0 && !board[y, x].IsClicked)
            {
                //此语句的位置很重要 后面需要用到它 需要先在这里把属性赋值
                board[y, x].IsClicked = true;
                //遍历周围并把周围每一个格子点一次
                for (int b = y - 1; b < y + 2; b++)
                {
                    for (int a = x - 1; a < x + 2; a++)
                    {
                        if (!isException(b, a))
                        {
                            discover(b, a);
                        }
                    }
                }
                board[y, x].IsCleared = true;
            }
            
        }
        public bool Start(int y, int x)
        {
            discover(y, x);
            //若点中的格子直接为0格 则直接返回开局成功
            if (board[y, x].value == 0)
            {
                return true;
            }
            else if (board[y,x].value == boom)
            {
                randomBoard(heigh, width, boomCount);
                Start(y, x);
                return false;
            }
            //若非 则寻找周围
            for (int b = y - 1; b < y + 2; b++)
            {
                for (int a = x - 1; a < x + 2; a++)
                {
                    if (!isException(b, a))
                    {
                        if (board[b, a].value == 0)
                        {
                            discover(b, a);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        //强行开局
        public void Start_2(int y, int x)
        {
            discover(y, x);
            //若点中的格子为0格 则开局成功
            if (board[y, x].value == 0)
            {
                return;
            }
            //不是0格 再来
            else
            {
                randomBoard(heigh, width, boomCount);
                Start_2(y, x);
            }
            return;
        }
    }
    //格子类
    class block
    {
        public int x;
        public int y;
        public int value;
        public bool IsKnown = false;
        public bool IsFlag = false;

        public bool IsClicked = false;//是否已点击
        public bool IsCleared = false;//是否已清除周围的空白格
        public block(int y, int x, int value)
        {
            this.x = x;
            this.y = y;
            this.value = value;
        }
    }
}
