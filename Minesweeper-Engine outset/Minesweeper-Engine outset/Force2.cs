using System.Drawing;
using System.Collections.Generic;
using System;


namespace Minesweeper_Engine_outset
{
    class Force2
    {
        //缺陷:在Updata的时候 二级推理仍然会发挥作用
        Board board;
        List<CLCT> CLCTs = new List<CLCT>();
        int bias = 3;//偏重

        public Force2(Board board)
        {
            this.board = board;
            for (int y = 0; y < board.heigh; y++)
            {
                for (int x = 0; x < board.width; x++)
                {
                    if (!board.game[x, y].IsCleared && board.game[x, y].value > 0)
                    {
                        CLCTs.Add(new CLCT(board, new Point(x, y)));
                    }
                }
            }
        }
        public bool Run()
        {
            bool Continue = false;//二级推理是否有进行过操作 true为进行过了
            //比对一次不一定能比对出全部 因此进行n次比对
            int times = 2;
            do
            {
                //检测并分离
                for (int p = 0; p < CLCTs.Count - 1; p++)
                {
                    for (int q = p + 1; q < CLCTs.Count; q++)
                    {
                        if (System.Math.Abs((CLCTs[p].p.Y - CLCTs[q].p.Y)) <= bias && System.Math.Abs(CLCTs[p].p.X - CLCTs[q].p.X) <= bias)
                        {
                            //通常情况处理
                            if (CLCT.IsContain(CLCTs[p], CLCTs[q]))
                            {
                                CLCT.Separate(CLCTs[p], CLCTs[q]);
                            }
                            //特殊情况处理
                            else
                            {
                                SpecialSituation(CLCTs[p], CLCTs[q], ref Continue);
                                
                            }
                        }
                    }
                }

                //点击
                for (int i = 0; i < CLCTs.Count; i++)
                {
                    Click(CLCTs[i], ref Continue);
                }

                //进行过操作 退出
                if (Continue)
                {
                    break;
                }
                times--;
            } while (times > 0);
            return Continue;
        }
        //特殊情况判定
        private void SpecialSituation(CLCT one, CLCT two, ref bool Continue)
        {
            //根据特殊情况1 只有两个块相邻时才有2个公共元素
            //则可以将[恰好有两个公共元素]转变成:相邻 即|x1-x2|+|y1-y2| = 1
            if (Math.Abs(one.p.X - two.p.X) + Math.Abs(one.p.Y - two.p.Y) == 1)
            {
                //A集合值为1,元素为n;B集合值为2,元素为3 满足情况1
                if (one.value == 1 && two.value == 2 && two.elt.Count == 3)
                {
                    SS_1(one, two);
                    Continue = true;//进行过处理
                }
                else if (one.value == 2 && one.elt.Count == 3 && two.value == 1)
                {
                    SS_1(two, one);
                    Continue = true;
                }
            }

            //为了不影响之前是否处理过 这里不赋值
        }
        //特殊情况1
        private void SS_1(CLCT A, CLCT B)
        {
            /*
            A = {a1,a2,...an-1, an} = 1
            B = {an-1, an, an+1} = 2
            处理后
            A' = {a1, a2,...an-2} = 0;
            B' = {an-1, an} = 1;
            C' = {an+1} = 1;
            */
            //注意传值时严格按照A B的顺序
            for (int i = 0; i < B.elt.Count; i++)
            {
                if (!A.elt.Contains(B.elt[i]))
                {
                    //B中非公共元素即为雷
                    CLCTs.Add(new CLCT(1, B.elt[i], B.p));
                    B.elt.Remove(B.elt[i]);
                    B.value = 1;
                    break;
                }
            }
            //移除在A中的B的元素 值变为0
            A.elt.Remove(B.elt[0]);
            A.elt.Remove(B.elt[1]);
            A.value = 0;
            Console.WriteLine("Special Situation Sovle!");
        }
        public void Click(CLCT key, ref bool Continue)
        {
            if (key.value == 0)
            {
                //点击
                for (int i = 0; i < key.elt.Count; i++)
                {
                    board.p.Click(board, key.elt[i].x, key.elt[i].y);
                }
                Continue = true;
            }
            else if (key.value == key.elt.Count)
            {
                //插旗
                for (int i = 0; i < key.elt.Count; i++)
                {
                    board.p.Flag(board, key.elt[i].x, key.elt[i].y);
                }
                Continue = true;
            }
        }
        public void Print()
        {
            for (int i = 0; i < CLCTs.Count; i++)
            {
                CLCTs[i].Print();
                Console.WriteLine();
            }
        }
    }
    class CLCT
    {
        public int value;
        public List<block> elt = new List<block>();//element 元素
        public Point p;
        public CLCT(Board board, Point Position)
        {
            value = board.game[Position.X, Position.Y].value - Sweeper.getRoundFlags(board, Position.X, Position.Y);
            p = Position;
            for (int y = Position.Y - 1; y < Position.Y + 2; y++)
            {
                for (int x = Position.X - 1; x < Position.X + 2; x++)
                {
                    //防止越界
                    if (Sweeper.Check(board,x,y))
                    {
                        if (board.game[x, y].value == -1)
                        {
                            elt.Add(board.game[x, y]);
                        }
                    }       
                }
            }
        }
        public CLCT(int value, block Single, Point p)
        {
            this.value = value;
            elt.Add(Single);
            this.p = p;
        }
        public void Print()
        {
            if (elt.Count <=0)
            {
                Console.WriteLine("() = " + value);
                return;
            }

            Console.Write("({0},{1}):", p.X, p.Y);
            for (int i = 0; i < elt.Count - 1; i++)
            {
                Console.Write("({0},{1}),", elt[i].x, elt[i].y);
            }
            Console.WriteLine("({0},{1}) = {2}", elt[elt.Count - 1].x, elt[elt.Count - 1].y, value);
        }
        //是否包含
        public static bool IsContain(CLCT one, CLCT two)
        {
            if (one.elt.Count > two.elt.Count)
            {
                for (int i = 0; i < two.elt.Count; i++)
                {
                    if (!one.elt.Contains(two.elt[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (one.elt.Count < two.elt.Count)
            {
                for (int i = 0; i < one.elt.Count; i++)
                {
                    if (!two.elt.Contains(one.elt[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            //如果两个集合元素数相同 又因为两个集合位置不同 那么它们绝对不互相包含
            else
            {
                return false;
            }
        }
        //求补集
        public static void Separate(CLCT one, CLCT two)
        {
            if (one.elt.Count > two.elt.Count)
            {
                for (int i = 0; i < two.elt.Count; i++)
                {
                    one.elt.Remove(two.elt[i]);
                }
                one.value = one.value - two.value;
            }
            else
            {
                for (int i = 0; i < one.elt.Count; i++)
                {
                    two.elt.Remove(one.elt[i]);
                }
                two.value = two.value - one.value;
            }
        }
    }
}
