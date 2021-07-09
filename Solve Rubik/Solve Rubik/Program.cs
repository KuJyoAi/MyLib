using System;

namespace Solve_Rubik
{

    class Program
    {
        static void Main(string[] args)
        {
            //打乱顺序的魔方 可解
            /*
            byte[] F = new byte[9] { 5, 6, 4, 6, 1, 1, 4, 6, 2 };
            byte[] B = new byte[9] { 2, 5, 6, 6, 2, 5, 5, 3, 6 };
            byte[] R = new byte[9] { 2, 4, 3, 4, 6, 2, 6, 2, 4 };
            byte[] L = new byte[9] { 1, 3, 3, 2, 5, 3, 1, 1, 5 };
            byte[] O = new byte[9] { 4, 4, 5, 1, 3, 2, 1, 1, 6 };
            byte[] U = new byte[9] { 2, 4, 3, 6, 4, 3, 3, 5, 1 };
            */
            //完成前两步的魔方
            
            byte[] F = new byte[9] { 2, 2, 3, 3, 1, 6, 1, 1, 1 };
            byte[] B = new byte[9] { 2, 5, 3, 3, 2, 1, 2, 2, 2 };
            byte[] L = new byte[9] { 1, 5, 6, 5, 5, 1, 5, 5, 5 };
            byte[] R = new byte[9] { 5, 6, 3, 3, 6, 2, 6, 6, 6 };
            byte[] O = new byte[9] { 6, 2, 5, 3, 3, 1, 3, 6, 1 };
            byte[] U = new byte[9] { 4, 4, 4, 4, 4, 4, 4, 4, 4 };
            
            Rubik RB = new Rubik(F, B, O, U, L, R);

            Rubik Ri = new Rubik();
            RB.U();
            RB.Show();

            //ThirdStep(ref RB);

        }
        //static void ThirdStep(ref Rubik RB)
        //{
        //    while (true)
        //    {
        //        while (true)
        //        {
        //            //判断上方是否有没对完的块 若全为黄块 则完毕
        //            if (RB.Over[1] == RB.Over[4])
        //            {
        //                if (RB.Over[3] == RB.Over[4])
        //                {
        //                    if (RB.Over[5] == RB.Over[4])
        //                    {
        //                        if (RB.Over[7] == RB.Over[4])
        //                        {
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        //判断是否完成
        //        //正面对准
        //        if (RB.Front[3] == RB.Front[4])
        //        {
        //            if (RB.Front[5] == RB.Front[4])
        //            {
        //                //后面对准
        //                if (RB.Behind[3] == RB.Left[4])
        //                {
        //                    if (RB.Behind[5] == RB.Behind[4])
        //                    {
        //                        //左面对准
        //                        if (RB.Left[3] == RB.Left[4])
        //                        {
        //                            if (RB.Left[5] == RB.Left[4])
        //                            {
        //                                //右面对准
        //                                if (RB.Right[3] == RB.Right[4])
        //                                {
        //                                    if (RB.Right[5] == RB.Right[4])
        //                                    {
        //                                        break;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
    class Rubik
    {
        public byte[] Front = new byte[9];//前面
        public byte[] Behind = new byte[9];//后面
        public byte[] Over = new byte[9];//上面
        public byte[] Under = new byte[9];//下面
        public byte[] Left = new byte[9];//左面
        public byte[] Right = new byte[9];//右面

        //颜色代码
        public const byte Green = 1;
        public const byte Blue = 2;
        public const byte Yellow = 3;
        public const byte White = 4;
        public const byte Red = 5;
        public const byte Orange = 6;

        //构造函数
        public Rubik(byte[] F, byte[] B, byte[] O, byte[] U, byte[] L, byte[] R)
        {
            Front = F;
            Behind = B;
            Over = O;
            Under = U;
            Left = L;
            Right = R;
        }
        //默认已经还原
        public Rubik()
        {
            Front = new byte[9] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            Behind = new byte[9] { 2, 2, 2, 2, 2, 2, 2, 2, 2 };
            Over = new byte[9] { 3, 3, 3, 3, 3, 3, 3, 3, 3 };
            Under = new byte[9] { 4, 4, 4, 4, 4, 4, 4, 4, 4 };
            Left = new byte[9] { 5, 5, 5, 5, 5, 5, 5, 5, 5 };
            Right = new byte[9] { 6, 6, 6, 6, 6, 6, 6, 6, 6 };
        }

        //使某一面旋转
        private void Cycle(ref byte[] Block, bool IsCW)
        {
            byte temp = Block[0];
            if (IsCW)
            {
                //1 = 7
                //7 = 9
                //9 = 3
                //3 = 1

                Block[0] = Block[6];
                Block[6] = Block[8];
                Block[8] = Block[2];
                Block[2] = temp;

                //2 = 4
                //4 = 8
                //8 = 6
                //6 = 2

                temp = Block[1];
                Block[1] = Block[3];
                Block[3] = Block[7];
                Block[7] = Block[5];
                Block[5] = temp;
            }
            else
            {

                //1 = 3
                //3 = 9
                //9 = 7
                //7 = 1

                Block[0] = Block[2];
                Block[2] = Block[8];
                Block[8] = Block[6];
                Block[6] = temp;

                //2 = 6
                //6 = 8
                //8 = 4
                //4 = 2

                temp = Block[1];
                Block[1] = Block[5];
                Block[5] = Block[7];
                Block[7] = Block[3];
                Block[3] = temp;
            }
        }

        public void U()
        {
            Cycle(ref Over, true);
            byte[] Tmp = new byte[3] { Front[0], Front[1], Front[2] };

            Front[0] = Right[0];
            Front[1] = Right[1];
            Front[2] = Right[2];

            Right[0] = Behind[0];
            Right[1] = Behind[1];
            Right[2] = Behind[2];

            Behind[0] = Left[0];
            Behind[1] = Left[1];
            Behind[2] = Left[2];

            Left[0] = Tmp[0];
            Left[1] = Tmp[1];
            Left[2] = Tmp[2];
        }
        public void Ui()
        {

            Cycle(ref Over, false);
            byte[] Tmp = new byte[3] { Front[0], Front[1], Front[2] };

            Front[0] = Left[0];
            Front[1] = Left[1];
            Front[2] = Left[2];

            Left[0] = Behind[0];
            Left[1] = Behind[1];
            Left[2] = Behind[2];

            Behind[0] = Right[0];
            Behind[1] = Right[1];
            Behind[2] = Right[2];

            Right[0] = Behind[0];
            Right[1] = Behind[1];
            Right[2] = Behind[2];

          }
        //打印状态函数
        public void Show()
        {
            Console.WriteLine("F/B");
            for (int i = 0; i < 9; i += 3)
            {
                Console.Write(Front[i] + "\t");
                Console.Write(Front[i + 1] + "\t");
                Console.Write(Front[i + 2] + "\t");
                Console.Write(Behind[i] + "\t");
                Console.Write(Behind[i + 1] + "\t");
                Console.Write(Behind[i + 2] + "\t");
                Console.WriteLine();
            }
            Console.WriteLine("L/R");
            for (int i = 0; i < 9; i += 3)
            {
                Console.Write(Left[i] + "\t");
                Console.Write(Left[i + 1] + "\t");
                Console.Write(Left[i + 2] + "\t");
                Console.Write(Right[i] + "\t");
                Console.Write(Right[i + 1] + "\t");
                Console.Write(Right[i + 2] + "\t");
                Console.WriteLine();
            }
            Console.WriteLine("O/U");
            for (int i = 0; i < 9; i += 3)
            {
                Console.Write(Over[i] + "\t");
                Console.Write(Over[i + 1] + "\t");
                Console.Write(Over[i + 2] + "\t");
                Console.Write(Under[i] + "\t");
                Console.Write(Under[i + 1] + "\t");
                Console.Write(Under[i + 2] + "\t");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
    //魔方类:
    //class Rubik
    //{
    //    //块
    //    public byte[] Front = new byte[9];//前面
    //    public byte[] Behind = new byte[9];//后面
    //    public byte[] Over = new byte[9];//上面
    //    public byte[] Under = new byte[9];//下面
    //    public byte[] Left = new byte[9];//左面
    //    public byte[] Right = new byte[9];//右面

    //    //颜色代码
    //    public const byte Green = 1;
    //    public const byte Blue = 2;
    //    public const byte Yellow = 3;
    //    public const byte White = 4;
    //    public const byte Red = 5;
    //    public const byte Orange = 6;

    //    //构造函数
    //    public Rubik(byte[] F, byte[] B, byte[] O,byte[] U,byte[] L,byte[] R)
    //    {
    //        Front = F;
    //        Behind = B;
    //        Over = O;
    //        Under = U;
    //        Left = L;
    //        Right = R;
    //    }
    //    //默认已经还原
    //    public Rubik()
    //    {
    //        Front = new byte[9] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    //        Behind = new byte[9] { 2, 2, 2, 2, 2, 2, 2, 2, 2 };
    //        Over = new byte[9] { 3, 3, 3, 3, 3, 3, 3, 3, 3 };
    //        Under = new byte[9] { 4, 4, 4, 4, 4, 4, 4, 4, 4 };
    //        Left = new byte[9] { 5, 5, 5, 5, 5, 5, 5, 5, 5 };
    //        Right = new byte[9] { 6, 6, 6, 6, 6, 6, 6, 6, 6 };
    //    }

    //    //使某一面旋转
    //    private void Cycle(ref byte[] Block,bool IsCW)
    //    {
    //        byte temp = Block[0];
    //        if (IsCW)
    //        {
    //            //1 = 7
    //            //7 = 9
    //            //9 = 3
    //            //3 = 1

    //            Block[0] = Block[6];
    //            Block[6] = Block[8];
    //            Block[8] = Block[2];
    //            Block[2] = temp;

    //            //2 = 4
    //            //4 = 8
    //            //8 = 6
    //            //6 = 2

    //            temp = Block[1];
    //            Block[1] = Block[3];
    //            Block[3] = Block[7];
    //            Block[7] = Block[5];
    //            Block[5] = temp;
    //        }
    //        else
    //        {

    //            //1 = 3
    //            //3 = 9
    //            //9 = 7
    //            //7 = 1

    //            Block[0] = Block[2];
    //            Block[2] = Block[8];
    //            Block[8] = Block[6];
    //            Block[6] = temp;

    //            //2 = 6
    //            //6 = 8
    //            //8 = 4
    //            //4 = 2

    //            temp = Block[1];
    //            Block[1] = Block[5];
    //            Block[5] = Block[7];
    //            Block[7] = Block[3];
    //            Block[3] = temp;
    //        }
    //    }

    //    //U IsCW表示方向
    //    public void U(bool IsCW)
    //    {
    //        byte[] Tmp = Front;
    //        byte temp = Over[0];
    //        //顺时针
    //        if (IsCW)
    //        {
    //            //顶面顺时针旋转
    //            Cycle(ref Over, true);


    //            //不能Tmp = Front 否则将指向同一对象
    //            Tmp = new byte[3] { Front[0], Front[1], Front[2] };
    //            for (int x = 0; x < 3; x++)
    //            {
    //                Front[x] = Right[x];
    //                Right[x] = Behind[x];
    //                Behind[x] = Left[x];
    //                Left[x] = Tmp[x];
    //            }
    //        }
    //        else
    //        {
    //            //顶面逆时针旋转
    //            Cycle(ref Over, false);

    //            Tmp = new byte[3] { Front[0], Front[1], Front[2] };
    //            for (int x = 0; x < 3; x++)
    //            {
    //                Front[x] = Left[x];
    //                Left[x] = Behind[x];
    //                Behind[x] = Right[x];
    //                Right[x] = Tmp[x];
    //            }
    //        }
    //    }
    //    //R
    //    public void R(bool IsCW)
    //    {
    //        byte[] Tmp = Front;
    //        byte temp = Right[0];
    //        //顺时针
    //        if (IsCW)
    //        {
    //            Cycle(ref Right, true);

    //            //不能Tmp = Front 否则将指向同一对象
    //            Tmp = new byte[3] { Front[2], Front[5], Front[8] };
    //            Front[2] = Under[2];
    //            Front[5] = Under[5];
    //            Front[8] = Under[8];

    //            Under[2] = Behind[6];
    //            Under[5] = Behind[3];
    //            Under[8] = Behind[0];

    //            Behind[6] = Over[2];
    //            Behind[3] = Over[5];
    //            Behind[0] = Over[8];

    //            Over[2] = Tmp[0];
    //            Over[5] = Tmp[1];
    //            Over[8] = Tmp[2];
    //        } 
    //        else
    //        {
    //            Cycle(ref Right, false);

    //            Tmp = new byte[3] { Front[2], Front[5], Front[8] };
    //            Front[2] = Over[2];
    //            Front[5] = Over[5];
    //            Front[8] = Over[8];

    //            Over[2] = Behind[6];
    //            Over[5] = Behind[3];
    //            Over[8] = Behind[0];

    //            Behind[6] = Under[2];
    //            Behind[3] = Under[5];
    //            Behind[0] = Under[8];

    //            Under[2] = Behind[6];
    //            Under[5] = Behind[3];
    //            Under[8] = Behind[0];
    //        }
    //    }
    //    //D
    //    public void D(bool IsCW)
    //    {
    //        byte[] Tmp = Front;
    //        byte temp = Under[0];
    //        //顺时针
    //        //!:上下是镜像的
    //        if (!IsCW)
    //        {
    //            Cycle(ref Under, true);

    //            //不能Tmp = Front 否则将指向同一对象
    //            Tmp = new byte[3] { Front[6], Front[7], Front[8] };
    //            for (int x = 6; x < 9; x++)
    //            {
    //                Front[x] = Right[x];
    //                Right[x] = Behind[x];
    //                Behind[x] = Left[x];
    //                Left[x] = Tmp[x - 6];
    //            }
    //        }
    //        else
    //        {
    //            Cycle(ref Under, false);

    //            Tmp = new byte[3] { Front[0], Front[1], Front[2] };
    //            for (int x = 6; x < 9; x++)
    //            {
    //                Front[x] = Left[x];
    //                Left[x] = Behind[x];
    //                Behind[x] = Right[x];
    //                Right[x] = Tmp[x - 6];
    //            }
    //        }
    //    }
    //    //L
    //    public void L(bool IsCW)
    //    {
    //        byte[] Tmp = Front;
    //        byte temp = Left[0];
    //        //顺时针 L与R互为镜像
    //        if (!IsCW)
    //        {
    //            Cycle(ref Left, true);

    //            //不能Tmp = Front 否则将指向同一对象
    //            Tmp = new byte[3] { Front[0], Front[3], Front[6] };
    //            Front[0] = Over[0];
    //            Front[3] = Over[3];
    //            Front[6] = Over[6];

    //            Over[0] = Behind[8];
    //            Over[3] = Behind[5];
    //            Over[6] = Behind[2];

    //            Behind[8] = Under[0];
    //            Behind[5] = Under[3];
    //            Behind[2] = Under[6];

    //            Under[0] = Tmp[0];
    //            Under[3] = Tmp[1];
    //            Under[6] = Tmp[2];
    //        }
    //        else
    //        {
    //            Cycle(ref Left, false);

    //            Tmp = new byte[3] { Front[0], Front[3], Front[6] };
    //            Front[0] = Under[0];
    //            Front[3] = Under[3];
    //            Front[6] = Under[6];

    //            Under[0] = Behind[8];
    //            Under[3] = Behind[5];
    //            Under[6] = Behind[2];

    //            Behind[8] = Over[0];
    //            Behind[5] = Over[3];
    //            Behind[2] = Over[6];

    //            Over[0] = Tmp[0];
    //            Over[3] = Tmp[1];
    //            Over[6] = Tmp[2];
    //        }
    //    }
    //    //F
    //    public void F(bool IsCW)
    //    {
    //        byte[] Tmp = Front;
    //        byte temp = Front[0];
    //        //顺时针 L与R互为镜像
    //        if (IsCW)
    //        {
    //            Cycle(ref Front, true);

    //            //不能Tmp = Front 否则将指向同一对象
    //            Tmp = new byte[3] { Over[6], Over[7], Over[8] };
    //            Over[6] = Left[8];
    //            Over[7] = Left[5];
    //            Over[8] = Left[2];

    //            Left[8] = Under[2];
    //            Left[5] = Under[1];
    //            Left[2] = Under[0];

    //            Under[2] = Right[0];
    //            Under[1] = Right[3];
    //            Under[0] = Right[6];

    //            Right[0] = Tmp[2];
    //            Right[3] = Tmp[1];
    //            Right[6] = Tmp[0];
    //        }
    //        else
    //        {
    //            Cycle(ref Front, false);

    //            Tmp = new byte[3] { Over[6], Over[7], Over[8] };
    //            Over[6] = Right[0];
    //            Over[7] = Right[3];
    //            Over[8] = Right[6];

    //            Right[0] = Under[2];
    //            Right[3] = Under[1];
    //            Right[6] = Under[0];

    //            Under[2] = Left[8];
    //            Under[1] = Left[5];
    //            Under[0] = Left[2];

    //            Left[8] = Tmp[0];
    //            Left[5] = Tmp[1];
    //            Left[2] = Tmp[2];
    //        }
    //    }

    //    //x旋转
    //    public void y(bool IsCW)
    //    {
    //        byte[] Tmp = new byte[9] { Front[0], Front[1], Front[2], Front[3], Front[4], Front[5], Front[6], Front[7], Front[8] };
    //        if (IsCW)
    //        {
    //            Cycle(ref Over, true);
    //            Cycle(ref Under, false);

    //            Front = Right;
    //            Right = Behind;
    //            Behind = Left;
    //            Left = Tmp;
    //        }
    //        else
    //        {
    //            Cycle(ref Over, false);
    //            Cycle(ref Under, true);

    //            Front = Left;
    //            Left = Behind;
    //            Behind = Right;
    //            Right = Tmp;
    //        }
    //    }
    //    //y旋转
    //    public void x(bool IsCW)
    //    {
    //        byte[] Tmp = new byte[9] { Front[0], Front[1], Front[2], Front[3], Front[4], Front[5], Front[6], Front[7], Front[8] };
    //        if (IsCW)
    //        {
    //            Cycle(ref Left, false);
    //            Cycle(ref Right, true);

    //            Front = Under;
    //            Under = Behind;
    //            Behind = Over;
    //            Over = Tmp;
    //        }
    //        else
    //        {


    //            Cycle(ref Left, true);
    //            Cycle(ref Right, false);

    //            Front = Over;
    //            Over = Behind;
    //            Behind = Under;
    //            Under = Tmp;
    //        }
    //    }

    //    //打印状态函数
    //    public void Show()
    //    {
    //        Console.WriteLine("F/B");
    //        for (int i = 0; i < 9; i += 3)
    //        {
    //            Console.Write(Front[i] + "\t");
    //            Console.Write(Front[i + 1] + "\t");
    //            Console.Write(Front[i + 2] + "\t");
    //            Console.Write(Behind[i] + "\t");
    //            Console.Write(Behind[i + 1] + "\t");
    //            Console.Write(Behind[i + 2] + "\t");
    //            Console.WriteLine();
    //        }
    //        Console.WriteLine("L/R");
    //        for (int i = 0; i < 9; i += 3)
    //        {
    //            Console.Write(Left[i] + "\t");
    //            Console.Write(Left[i + 1] + "\t");
    //            Console.Write(Left[i + 2] + "\t");
    //            Console.Write(Right[i] + "\t");
    //            Console.Write(Right[i + 1] + "\t");
    //            Console.Write(Right[i + 2] + "\t");
    //            Console.WriteLine();
    //        }
    //        Console.WriteLine("O/U");
    //        for (int i = 0; i < 9; i += 3)
    //        {
    //            Console.Write(Over[i] + "\t");
    //            Console.Write(Over[i + 1] + "\t");
    //            Console.Write(Over[i + 2] + "\t");
    //            Console.Write(Under[i] + "\t");
    //            Console.Write(Under[i + 1] + "\t");
    //            Console.Write(Under[i + 2] + "\t");
    //            Console.WriteLine();
    //        }
    //        Console.WriteLine();
    //    }
    //    //已经完成:U D R L F x y
    //    //未完成:B M E S
    //}
}
