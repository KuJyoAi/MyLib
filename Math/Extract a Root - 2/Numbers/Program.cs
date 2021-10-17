using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using System.Threading;

namespace Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入需要计算的位数:");
            int a = int.Parse(Console.ReadLine());
            int Time = System.Environment.TickCount;

            Console.Write("任意键开始");
            Console.ReadKey();
            Console.WriteLine();

            GetTwo(a);
            //现在的时间减去刚才的时间就是计算数经过的时间
            Time = System.Environment.TickCount - Time;
            Console.WriteLine("计算"+ a +"位数,共花费"+ Time +"毫秒,已写入D:\\result.txt");
            Console.ReadKey();
        }
        static void WHILE1()
        {
            int i = 1;
            while (true)
            {
                i++;
            }
        }
        static void GetTwo(int key)
        {
            string result = "1.41";
            string Temp = result;
            string factor = "0";
            Console.Write("√2 = " + result);
            for (int l = 2; l <= key;l++)
            {
                //l:小数点位数
                for(int x = 0;x<=10; x++)
                {
                    if (x < 10)
                    {
                        factor = x.ToString();
                    }
                    else if (x == 10)
                    {
                        //如果x等于10 则直接输出
                        result += x - 1;
                        //Console.WriteLine("result:" + result);
                        break;
                    }
                    Temp = multiply(result + factor, result + factor);
                    //Console.WriteLine("Temp:" + Temp);
                    //夹逼法如果大于2则以求出值
                    if (Temp.IndexOf('2') == 0)
                    {
                        result += x - 1;

                        //输出算得的位数
                        Console.Write(x - 1);

                        //Console.WriteLine("result:" +result);
                        break;
                    }
                }
                //打印输出位数长度
                //Console.WriteLine("L: " + l);
            }
            //最后打印输出结果
            //Console.WriteLine(result);
            File.WriteAllText("D:\\Result.txt",result);

            //最后一行与后面的语句空开
            Console.WriteLine();
        }
        static string multiply(string a, string b)
        {
            //找出小数位数
            int posA = a.Length - a.IndexOf(".") - 1;
            int posB = b.Length - b.IndexOf(".") - 1;
            //Console.WriteLine("posA:" + posA + "        posB:" + posB);
            //去掉小数点
            a = a.Replace(".","");
            b = b.Replace(".","");
            //Console.WriteLine("A:" + a + "      B:" + b);
            //相乘
            BigInteger TempA = BigInteger.Parse(a);
            BigInteger TempB = BigInteger.Parse(b);
            BigInteger Result = TempA * TempB;
            //Console.WriteLine("Result:" + Result);
            //转换String
            string ResultString = Result.ToString();
            //Console.WriteLine("ResultString:" + ResultString);
            //加上小数点
            ResultString = ResultString.Insert(ResultString.Length - (posA + posB),".");
            //Console.WriteLine("ResultString:" + ResultString);
            return ResultString;
        }
        /*
        static string add(string a, string b)
        {
            //转换成byte数组
            byte[] TempA = Segmentation(a);
            byte[] TempB = Segmentation(b);
            //结果
            byte[] Result = new byte[100];
            //寻找小数点位置
            int posA = a.IndexOf(".");
            int posB = b.IndexOf(".");
            Console.WriteLine("PosA:"+posA+"  PosB:"+posB);
            if (posA != 0 & posB != 0)
            {
                //得出小数位数
                int DecimalDigitsA = TempA.Length - posA - 1;
                int DecimalDigitsB = TempB.Length - posB - 1;
                Console.WriteLine("DecimalDigitsA:"+DecimalDigitsA+"  B:"+DecimalDigitsB);
                //把其中一个加数多出来的小数位直接赋给结果
                if (DecimalDigitsA > DecimalDigitsB)
                {
                    for (int i = TempA.Length - 1; i > TempA.Length - DecimalDigitsA + 1; i--)
                    {
                        Result[i] = TempA[i];

                    }
                }
                else if (DecimalDigitsB > DecimalDigitsA)
                {
                    for (int i=TempB.Length-1;i > TempB.Length - DecimalDigitsB + 1;i--)
                    {
                       Result[i] = TempB[i];
                    } 
                }
                
            }
            for (int x = 0; x < Result.Length; x++)
            {
                Console.Write(Result[x]);
            }
            
            
            
            
            //for (int x=0;x<TempB.Length;x++)
            //{
            //    Console.WriteLine("B:"+TempB[x]);
            //}
            //小数位相加

            //整数位相加
            return "";
        }
        //string To Byte
        static byte[] Segmentation(string key)
        {
            //转成byte值
            byte[] Temp = System.Text.Encoding.Default.GetBytes(key);
            //转成数字值
            for (int x = 0; x < Temp.Length; x++)
            {
                Temp[x] = (byte)(Temp[x] - 48);
                //Console.WriteLine(Temp[x]);
            }
            return Temp;

        }
        */

    }
}
