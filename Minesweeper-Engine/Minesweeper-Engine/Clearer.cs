using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace Minesweeper_Engine
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Game g = new Game(8, 8, 10);
            //Game g = new Minesweeper_Engine.Game("SSug0204070a17232428393e40484c4e56575a5e5f67696e7071727b7d818a8f969da2a3adb2b3b4b6b9bec1c3c5c6cbced9e1e2e3e4e5e6ebedf0f3f4fag1h4hbi1i4i5jbjdjek3k5l4l7m2m3m9mamdn2n3ndp8p9papeq4q8qcr0r7r9rarbrerfs5sasetc");
            Random r = new Random();
            Point p = new Point(8, 8);
            g.Start_2(p.Y, p.X);
            Console.WriteLine("Start Point:({0},{1})", p.X, p.Y);
            Console.WriteLine("GameCode:{0}", g.getGameHash());
            Stopwatch st = new Stopwatch();

            st.Start();
            Console.WriteLine(SweeperForce.Sovle(g));
            st.Stop();

            Console.WriteLine(st.Elapsed);
            
            //Console.WriteLine(Scanner.FindWindow("TMain", "Minesweeper Arbiter "));
            
        }
    }
    static class SweeperForce
    {
        public static bool Sovle(Game s)
        {
            while (!s.gameIsEnd())
            {
                //一级推理
                while (!SweeperForce1.Force(s))
                {
                    
                }

                //二级推理
                SweeperForce2 sf = new SweeperForce2(s);
                if (!sf.Force(s))
                {
                    break;
                }

            }
            //Console.WriteLine("Final Result:");
            //s.print_2();
            return s.gameIsEnd();
        }
    }
    static class SweeperForce1
    {
        const int boom = -1;
        const int blank = 0;
        const int error = -10;

        //一级推理
        public static bool Force(Game s)
        {
            bool isCompleted = true;
            for (int y = 0; y < s.heigh; y++)
            {
                for (int x = 0; x < s.width; x++)
                {
                    //数字判定
                    if (s.board[y, x].value > 0 && s.board[y, x].IsKnown)
                    {
                        //对周围无空白格的数字格(IsCleared == true)进行过滤 增加效率
                        if (!s.board[y, x].IsCleared)
                        {
                            //若未知格数=数字数-旗帜数,则全部为旗帜
                            if (getUnknows(y, x, s) == s.board[y, x].value - getFlags(y, x, s))
                            {
                                setFlags(y, x, s);
                                s.board[y, x].IsCleared = true;
                                isCompleted = false;
                            }
                            //若周围旗帜数=数字 且 未知格数>0,则全部为空白
                            else if (getFlags(y, x, s) == s.board[y, x].value && getUnknows(y, x, s) > 0)
                            {
                                setBlanks(y, x, s);
                                s.board[y, x].IsCleared = true;
                                isCompleted = false;
                            }
                        }
                    }
                }
            }
            return isCompleted;
        }
        //取周围未知格数
        public static int getUnknows(int y, int x, Game s)
        {
            int count = 0;
            for (int b = y - 1; b < y + 2; b++)
            {
                for (int a = x - 1; a < x + 2; a++)
                {
                    if (!s.isException(b, a))
                    {
                        //未被插旗且未知
                        if (!s.board[b, a].IsKnown && !s.board[b, a].IsFlag)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
        //取周围旗帜数
        public static int getFlags(int y, int x, Game s)
        {
            int count = 0;
            for (int b = y - 1; b < y + 2; b++)
            {
                for (int a = x - 1; a < x + 2; a++)
                {
                    if (!s.isException(b, a))
                    {
                        if (s.board[b, a].IsFlag)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
        //周围未知格都为旗帜
        public static void setFlags(int y, int x, Game s)
        {
            for (int b = y - 1; b < y + 2; b++)
            {
                for (int a = x - 1; a < x + 2; a++)
                {
                    if (!s.isException(b, a))
                    {
                        if (!s.board[b, a].IsKnown)
                        {
                            s.board[b, a].IsFlag = true;
                        }
                    }
                }
            }
        }
        //点击周围
        public static void setBlanks(int y, int x, Game s)
        {
            for (int b = y - 1; b < y + 2; b++)
            {
                for (int a = x - 1; a < x + 2; a++)
                {
                    if (!s.isException(b, a))
                    {
                        if (!s.board[b, a].IsFlag)
                        {
                            s.discover(b, a);
                        }
                    }
                }
            }
        }
    }
    class SweeperForce2
    {
        int bia = 3;//如果两个集合的位置超过这个数字 则不比较
        public List<CLCT> Ifmt = new List<CLCT>();

        public SweeperForce2(Game s)
        {
            for (int y = 0; y < s.heigh; y++)
            {
                for (int x = 0; x < s.width; x++)
                {
                    if (s.board[y, x].value > 0 && s.board[y, x].IsKnown)
                    {
                        if (!s.board[y, x].IsCleared)
                        {
                            Ifmt.Add(new CLCT(y, x, s));
                        }
                    }
                }
            }
        }
        public bool Force(Game s)
        {
            bool isCompleted = false;
            //使用单循环算法 复杂度为:n(n-1)/2
            //PS:包含关系比较一次可能不出结果 例如A={a1,a2,a3,a4} B={a2,a3,a4,a5} C={a1,a2} 则AC比较之后的结果可以和B比较
            //但大部分情况下 比较一次即可 因此此处只比较一次 如果没有某个块解决 则再来一次

            int RemainChance = 1;//算法无法解决时 剩余重复的机会
            do
            {
                for (int i = 0; i < Ifmt.Count - 1; i++)
                {
                    for (int p = i + 1; p < Ifmt.Count; p++)
                    {
                        //距离过远不比较
                        if (Math.Abs(Ifmt[i].Position.X - Ifmt[p].Position.X) <= bia && Math.Abs(Ifmt[i].Position.Y - Ifmt[p].Position.Y) <= bia)
                        {
                            if (CLCT.isIncluded(Ifmt[i], Ifmt[p]))
                            {
                                CLCT.Separate(Ifmt[i], Ifmt[p]);
                                //检测状态并点击 这一步只要执行了 就认定算法有用
                                if (Ifmt[i].value == 0)
                                {
                                    CLCT.setBlanks(Ifmt[i], s);
                                    isCompleted = true;
                                }
                                else if (Ifmt[i].value == Ifmt[i].Rds.Count)
                                {
                                    CLCT.setFlags(Ifmt[i], s);
                                    isCompleted = true;
                                }
                                if (Ifmt[p].value == 0)
                                {
                                    CLCT.setBlanks(Ifmt[p], s);
                                    isCompleted = true;
                                }
                                else if (Ifmt[p].value == Ifmt[p].Rds.Count)
                                {
                                    CLCT.setFlags(Ifmt[p], s);
                                    isCompleted = true;
                                }
                            }
                        }
                    }
                }

                //特殊情况处理在算法没有解决的情况下启动
                if (!isCompleted)
                {
                    isCompleted = SpecialSituations(s);
                }

                //处理成功 跳出
                if (isCompleted)
                {
                    break;
                }
                //无法处理 减1
                else
                {
                    RemainChance--;
                }
            } while (RemainChance > 0);

            return isCompleted;
        }
        private bool SpecialSituations(Game s)
        {
            for (int i = 0; i < Ifmt.Count - 1; i++)
            {
                for (int p = i + 1; p < Ifmt.Count; p++)
                {
                    if (Ifmt[i].value + Ifmt[p].value == 3 && Math.Abs(Ifmt[i].value - Ifmt[p].value) == 1)
                    {
                        /*
                        第一种特殊情况:
                        A={a1,a2,...,an-1,an}=1
                        B={an-1,an,an+1}=2
                        根据推理可知{a1,a2,an-2}不是雷 an+1是雷
                        */

                        //也许是type1
                        //如果完成 直接返回true 否则如果把值赋给变量 则即使有true 也会被后面的false覆盖
                        if (SS_type1(Ifmt[i], Ifmt[p], s))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        private bool SS_type1(CLCT one, CLCT two, Game s)
        {
            //值为2的一方元素数必须为3 且二者必须有且仅有2个相同元素
            CLCT same = new CLCT(1, one.Position);

            //one为值大的一方
            if (one.value == 2 && one.Rds.Count == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (two.Rds.Contains(one.Rds[i]))
                    {
                        same.Rds.Add(one.Rds[i]);
                    }
                }

                //非type1 跳出
                if (same.Rds.Count != 2)
                {
                    return false;
                }

                //加入到集合中
                Ifmt.Add(same);
                //移除公共块 并修正值
                one.Rds.Remove(same.Rds[0]);
                one.Rds.Remove(same.Rds[1]);
                one.value--;
                two.Rds.Remove(same.Rds[0]);
                two.Rds.Remove(same.Rds[1]);
                two.value--;
                //输出
                CLCT.setBlanks(two, s);
                CLCT.setFlags(one, s);
            }
            else if (two.value == 2 && two.Rds.Count == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (one.Rds.Contains(two.Rds[i]))
                    {
                        same.Rds.Add(two.Rds[i]);
                    }
                }

                //非type1 跳出
                if (same.Rds.Count != 2)
                {
                    return false;
                }

                //加入到集合中
                Ifmt.Add(same);
                //移除公共块 并修正值
                one.Rds.Remove(same.Rds[0]);
                one.Rds.Remove(same.Rds[1]);
                one.value--;
                two.Rds.Remove(same.Rds[0]);
                two.Rds.Remove(same.Rds[1]);
                two.value--;
                //输出
                CLCT.setBlanks(one, s);
                CLCT.setFlags(two, s);
            }
            else
            {
                return false;
            }

            //是type1
            return true;
        }
        //打印
        public void print()
        {
            //1:{(4,5)(6,7).....} = 1 Count = n
            for (int i = 0; i < Ifmt.Count; i++)
            {
                Console.Write(i + ":{");
                for (int a = 0; a < Ifmt[i].Rds.Count; a++)
                {
                    Console.Write("({0},{1})", Ifmt[i].Rds[a].x, Ifmt[i].Rds[a].y);
                }
                Console.Write("} = " + Ifmt[i].value);
                Console.WriteLine("\t\t\t\tCount = {0}", Ifmt[i].Rds.Count);
            }
        }
    }
    //集合类
    class CLCT
    {

        public List<block> Rds = new List<block>();//周围块
        public int value;//值
        public Point Position;//信息位置

        public CLCT(int y, int x,Game s)
        {
            Position = new Point(y, x);
            for (int b = y - 1; b < y + 2; b++)
            {
                for (int a = x - 1; a < x + 2; a++)
                {
                    if (!s.isException(b, a))
                    {
                        if (!s.board[b,a].IsKnown && !s.board[b,a].IsFlag)
                        {
                            Rds.Add(s.board[b, a]);
                            value = s.board[y, x].value - SweeperForce1.getFlags(y, x, s);
                        }
                    }
                }
            }
        }

        public CLCT(int value, Point P)
        {
            this.value = value;
            Position = P;
        }

        //是否包含
        public static bool isIncluded(CLCT one, CLCT two)
        {
            if (one.Rds.Count > two.Rds.Count)
            {
                //将数量较小的一方与另一方比较 如果较小的一方有某一元素不在另一方 则不包含
                for (int i = 0; i < two.Rds.Count; i++)
                {
                    if (!one.Rds.Contains(two.Rds[i]))
                    {
                        return false;
                    }
                }
            }
            else if (one.Rds.Count < two.Rds.Count)
            {
                for (int i = 0; i < one.Rds.Count; i++)
                {
                    if (!two.Rds.Contains(one.Rds[i]))
                    {
                        return false;
                    }
                }
            }
            //当它们元素数量相同时 由于两个相同数量的集合不可能互相包含(若包含则必须为同一集合)
            else
            {
                return false;
            }
            return true;
        }
        //依据其中一方分离另一方(相减)
        public static void Separate(CLCT one, CLCT two)
        {
            if (one.Rds.Count > two.Rds.Count)
            {
                //以2为依据使1分离
                //PS:若A是B的子集 则A的值一定小于或等于B
                one.value = one.value - two.value;
                for (int i = 0; i < two.Rds.Count; i++)
                {
                    one.Rds.Remove(two.Rds[i]);
                }
            }
            else
            {
                two.value = two.value - one.value;
                for (int i = 0; i < one.Rds.Count; i++)
                {
                    two.Rds.Remove(one.Rds[i]);
                }
            }
        }

        //点击或插旗集合内的元素
        public static void setBlanks(CLCT obj, Game s)
        {
            for (int i = 0; i < obj.Rds.Count; i++)
            {
                s.discover(obj.Rds[i].y, obj.Rds[i].x);
            }
        }
        public static void setFlags(CLCT obj, Game s)
        {
            for (int i = 0; i < obj.Rds.Count; i++)
            {
                obj.Rds[i].IsFlag = true;
            }
        }
    }
}
