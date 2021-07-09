using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Rubik_Master
{
    class Program
    {
        public const int gr = 1;//green
        public const int or = 2;//orange
        public const int rd = 3;//red
        public const int wh = 4;//white
        public const int yl = 5;//yellow
        public const int bl = 6;//blue

        static void Main(string[] args)
        {
            int[] fr = new int[9] { rd, or, gr, rd, gr, gr, wh, yl, bl };
            int[] le = new int[9] { yl, bl, wh, gr, or, yl, or, rd, gr };
            int[] rt = new int[9] { wh, gr, wh, or, rd, bl, yl, wh, yl };
            int[] ud = new int[9] { rd, gr, rd, wh, yl, bl, gr, rd, or };
            int[] bd = new int[9] { bl, or, gr, yl, bl, wh, bl, bl, yl };
            int[] tp = new int[9] { rd, wh, or, or, wh, rd, bl, yl, or };
            Rubik sample = new Rubik(fr, le, rt, ud, bd, tp);
            sample.U_();
            sample.print();
        }
    }
    class Rubik
    {
        //魔方类二代
        public const int gr = 1;//green
        public const int or = 2;//orange
        public const int rd = 3;//red
        public const int wh = 4;//white
        public const int yl = 5;//yellow
        public const int bl = 6;//blue

        //fr为正面;le rt以向左向右旋转90度为正面;tp ud以向上向下旋转90度为正面;bd以向左或向右旋转180度为正面
        public int[] fr = new int[9];
        public int[] le = new int[9];
        public int[] rt = new int[9];
        public int[] ud = new int[9];
        public int[] bd = new int[9];
        public int[] tp = new int[9];

        public Rubik(int[] front, int[] left, int[] right, int[] under, int[] behind, int[] top)
        {
            fr = front;
            le = left;
            rt = right;
            ud = under;
            bd = behind;
            tp = top;
        }
        
        //打印
        public void print()
        {
            Console.WriteLine("front:");
            for (int i = 0; i < 9; i += 3)
            {
                toKanji(fr[i]);
                Console.Write("\t");
                toKanji(fr[i + 1]);
                Console.Write("\t");
                toKanji(fr[i + 2]);
                Console.Write("\t");
                Console.WriteLine();
            }

            Console.WriteLine("left:");
            for (int i = 0; i < 9; i += 3)
            {
                toKanji(le[i]);
                Console.Write("\t");
                toKanji(le[i + 1]);
                Console.Write("\t");
                toKanji(le[i + 2]);
                Console.Write("\t");
                Console.WriteLine();
            }

            Console.WriteLine("right:");
            for (int i = 0; i < 9; i += 3)
            {
                toKanji(rt[i]);
                Console.Write("\t");
                toKanji(rt[i + 1]);
                Console.Write("\t");
                toKanji(rt[i + 2]);
                Console.Write("\t");
                Console.WriteLine();
            }

            Console.WriteLine("under:");
            for (int i = 0; i < 9; i += 3)
            {
                toKanji(ud[i]);
                Console.Write("\t");
                toKanji(ud[i + 1]);
                Console.Write("\t");
                toKanji(ud[i + 2]);
                Console.Write("\t");
                Console.WriteLine();
            }

            Console.WriteLine("behind:");
            for (int i = 0; i < 9; i += 3)
            {
                toKanji(bd[i]);
                Console.Write("\t");
                toKanji(bd[i + 1]);
                Console.Write("\t");
                toKanji(bd[i + 2]);
                Console.Write("\t");
                Console.WriteLine();
            }

            Console.WriteLine("top:");
            for (int i = 0; i < 9; i += 3)
            {
                toKanji(tp[i]);
                Console.Write("\t");
                toKanji(tp[i + 1]);
                Console.Write("\t");
                toKanji(tp[i + 2]);
                Console.Write("\t");
                Console.WriteLine();
            }
        }
        private void toKanji(int key)
        {
            switch (key)
            {
                case 1:
                    Console.Write("绿");
                    break;
                case 2:
                    Console.Write("橙");
                    break;
                case 3:
                    Console.Write("红");
                    break;
                case 4:
                    Console.Write("白");
                    break;
                case 5:
                    Console.Write("黄");
                    break;
                case 6:
                    Console.Write("蓝");
                    break;
                default:
                    Console.Write("");
                    break;
            }
        }

        //动作
        public void U()
        {
            int[] fr_ = new int[] { fr[0], fr[1], fr[2] };
            //四边
            for (int i = 0; i < 3; i++)
            {
                fr[i] = rt[i];
                rt[i] = bd[i];
                bd[i] = le[i];
                le[i] = fr_[i];
            }
            //顶层
            Rotation(tp, true);
        }
        public void U_()
        {
            int[] fr_ = new int[] { fr[0], fr[1], fr[2] };
            //四边
            for (int i = 0; i < 3; i++)
            {
                fr[i] = le[i];
                le[i] = bd[i];
                bd[i] = rt[i];
                rt[i] = fr_[i];
            }
            //顶层
            Rotation(tp, false);
        }

        //旋转 true:顺时针 false:逆时针
        private void Rotation(int[] plane, bool clockwise)
        {
            int[] tmp = new int[] { plane[0], plane[1], plane[2], plane[3], plane[4], plane[5], plane[6], plane[7], plane[8] };
            if (clockwise)
            {
                plane[0] = tmp[6];
                plane[1] = tmp[3];
                plane[2] = tmp[0];
                plane[3] = tmp[7];
                //plane[4] = tmp[4];
                plane[5] = tmp[1];
                plane[6] = tmp[8];
                plane[7] = tmp[5];
                plane[8] = tmp[2];
            }
            else
            {
                plane[0] = tmp[2];
                plane[1] = tmp[5];
                plane[2] = tmp[8];
                plane[3] = tmp[1];
                //plane[4] = tmp[4];
                plane[5] = tmp[7];
                plane[6] = tmp[0];
                plane[7] = tmp[3];
                plane[8] = tmp[6];
            }
        }
    }

}
