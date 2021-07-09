using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MyLibrary
{
    /// <summary>
    /// 大数运算库(文本版)
    /// </summary>
    public class LargeNumber_T
    {
        /// <summary> 
        /// 减法运算
        /// </summary>
        /// <param name="a">被减数</param>
        /// <param name="b">减数</param>
        /// <returns>差</returns>
        static public string Subtract(string Left, string Right)
        {
            return Add(Left, "-" + Right);
        }
        /*static public string Subtract(string Left, string Right)
        {
            StringBuilder a = new StringBuilder(Left);
            StringBuilder b = new StringBuilder(Right);
            //需要加的位数
            int PointPos = 0;
            //小数位数
            int PointPosIG_a = a.Length - a.ToString().IndexOf(".") - 1;
            int PointPosIG_b = b.Length - b.ToString().IndexOf(".") - 1;

            //去掉小数点
            a = a.Replace(".", "");
            b = b.Replace(".", "");

            //末尾添0
            if (PointPosIG_a > PointPosIG_b)
            {
                PointPos = PointPosIG_a;
                for (int x = 0; x < PointPosIG_a - PointPosIG_b; x++)
                {
                    b.Append(0);
                }
            }
            else
            {
                PointPos = PointPosIG_b;
                for (int x = 0; x < PointPosIG_b - PointPosIG_b; x++)
                {
                    a.Append(0);
                }
            }
            //Console.WriteLine("PointPos:" + PointPos);
            //Console.WriteLine("PointPosIG_a:{0}    PointPosIG_b:{1}", PointPosIG_a, PointPosIG_b);
            //Console.WriteLine("A:{0}    B:{1}", a, b);

            //被减数 减数 差
            BigInteger Minuend = BigInteger.Parse(a.ToString());
            BigInteger Subtrahend = BigInteger.Parse(b.ToString());
            BigInteger Difference = Minuend - Subtrahend;
            //Console.WriteLine(Difference);

            
            //加小数点
            string Result = Difference.ToString();
            //Console.WriteLine(Result);
            Result = Result.Insert(Result.Length - PointPos, ".");
            //Console.WriteLine("Final Result:" + Result);
            return Result;
        }*/
        /// <summary>
        /// 加法运算
        /// </summary>
        /// <param name="AddendA">加数</param>
        /// <param name="AddendB">加数</param>
        /// <returns>和</returns>
        static public string Add(string Left, string Right)
        {
            /*
                3.14
               36.784

                3140
               36784
               扩大10^n倍变成整数 然后缩小同样倍数 效率可能较低
            */

            StringBuilder L = new StringBuilder(Left);
            StringBuilder R = new StringBuilder(Right);

            //小数位数 = 整个长度 - 小数点位置 - 1(小数点本身占1)
            int BitsA = Left.Length - Left.IndexOf('.') - 1;
            int BitsB = Right.Length - Right.IndexOf('.') - 1;
            int IncreaseTimes = 0;//扩大倍数 以10为底
            //Console.WriteLine("BitsA:{0}\tBitsB:{1}", BitsA, BitsB);

            //根据上面的公式 小数位数等于整个长度即没有小数(整个长度 - (-1) - 1)
            if (BitsA == Left.Length)
            {
                BitsA = 0;
            }
            if (BitsB == Right.Length)
            {
                BitsB = 0;
            }

            //有小数的情况去小数点 有一方有小数即可
            if (BitsA != 0 || BitsB != 0)
            {
                L.Replace(".", "");
                R.Replace(".", "");

                //分情况讨论:1 双方小数位数相等 都去掉小数点即可 扩大倍数为小数位数
                if (BitsA == BitsB)
                {
                    //L.Replace(".", "");
                    //R.Replace(".", "");
                    IncreaseTimes = BitsA;
                }
                //A的小数位数>B的小数位数 则A去小数点 B末尾添0
                else if (BitsA > BitsB)
                {
                    //L.Replace(".", "");
                    //R.Replace(".", "");
                    IncreaseTimes = BitsA;
                    for (int i = 0; i < BitsA - BitsB; i++)
                    {
                        R.Append('0');
                    }
                }
                else if (BitsB > BitsA)
                {
                    //L.Replace(".", "");
                    //R.Replace(".", "");
                    IncreaseTimes = BitsB;
                    for (int i = 0; i < BitsB - BitsA; i++)
                    {
                        L.Append('0');
                    }
                }
            }
            //Console.WriteLine("L:{0}\tR:{1}\tIncreaseTimes:{2}", L, R, IncreaseTimes);

            //两数相加
            StringBuilder result = new StringBuilder((BigInteger.Parse(L.ToString()) + BigInteger.Parse(R.ToString())).ToString());
            //result = result.Insert(result.Length - IncreaseTimes, ".");
            //Console.WriteLine(result);

            //头部添0(需要添0的情况)
            if (result.Length <= IncreaseTimes)
            {
                for (int i = 0; i < IncreaseTimes - result.Length; i++)
                {
                    result.Insert(0, '0');
                }
                return "0." + result.ToString();
            }
            return result.Insert(result.Length - IncreaseTimes, '.').ToString();
            
            //插入小数点(缩小倍数)
        }
        /*static public string Add(string AddendA, string AddendB)
        {
            //小数点位置
            int posA = AddendA.IndexOf(".");
            int posB = AddendB.IndexOf(".");
            //Console.WriteLine("posA:" + posA + "\tposB:" + posB);

            //整数部分
            string integerA = "0";
            string integerB = "0";

            //小数部分(+ 1:小数点不包括在内)
            StringBuilder decimalA = new StringBuilder(AddendA.Substring(posA + 1));
            StringBuilder decimalB = new StringBuilder(AddendB.Substring(posB + 1));

            //如果没有找到小数点则清零
            if (posA < 0)
            {
                integerA = AddendA;
                decimalA = new StringBuilder("0");
            }
            else
            {
                //否则就是找到了
                integerA = AddendA.Substring(0, posA);

            }
            if (posB < 0)
            {
                integerB = AddendB;
                decimalB = new StringBuilder("0");
            }
            else
            {
                integerB = AddendB.Substring(0, posB);
            }
            //Console.WriteLine("integerA:" + integerA + "\tB:" + integerB);
            //Console.WriteLine("decimalA:" + decimalA + "\tB:" + decimalB);

            //小数位数
            int bitA = decimalA.Length;
            int bitB = decimalB.Length;
            //Console.WriteLine("bitA:" + bitA + "\tbitB:" + bitB);

            //如果两个加数都有小数部分,则加0
            if (decimalA.ToString() != "0" & decimalB.ToString() != "0")
            {
                //给小数位数少的加0
                if (bitA > bitB)
                {
                    for (int x = 0; x < bitA - bitB; x++)
                    {
                        decimalB.Append("0");
                    }
                }
                else if (bitB > bitA)
                {
                    for (int x = 0; x < bitB - bitA; x++)
                    {
                        decimalA.Append(0);
                    }
                }
            }
            else if (decimalA.ToString() == "0" || decimalB.ToString() == "0")
            {
                //如果有一方的小数是0的话,直接返回
                if (decimalA.ToString() == "0")
                {
                    return (BigInteger.Parse(integerA) + BigInteger.Parse(integerB)).ToString() + "." + decimalB;
                }
                else if (decimalB.ToString() == "0")
                {
                    return (BigInteger.Parse(integerA) + BigInteger.Parse(integerB)).ToString() + "." + decimalA;
                }

            }
            //Console.WriteLine("decimalModifiedA:" + decimalA + "\tB:" + decimalB);

            //整数部分相加
            BigInteger integerResult = BigInteger.Parse(integerA) + BigInteger.Parse(integerB);

            //小数部分相加
            BigInteger decimalResult = BigInteger.Parse(decimalA.ToString()) + BigInteger.Parse(decimalB.ToString());
            //Console.WriteLine("integerResult:" + integerResult + "\tdecimalResult:" + decimalResult);

            //没有小数
            if (decimalResult == 0)
            {
                //如果小数部分相加为0,则没有小数
                return integerResult.ToString();
            }

            //需要进位
            //相加后小数位数,如果比原来的大需要进位
            int bitResult = decimalResult.ToString().Length;
            if (bitResult != decimalA.ToString().Length & bitResult != decimalB.ToString().Length)
            {
                //比相加前大,进位(只加1是因为加数不可能进2位(9 + 9 = 18))
                integerResult += 1;

                //小数后面的位数
                string Result = decimalResult.ToString().Substring(1);

                //返回整数+小数点+小数
                return integerResult.ToString() + "." + Result;
            }

            //无需进位也有小数
            return integerResult.ToString() + "." + decimalResult.ToString();

        }*/
        /// <summary>
        /// 乘法运算
        /// </summary>
        /// <param name="FactorA">因数</param>
        /// <param name="FactorB">因数</param>
        /// <returns>积</returns>
        static public string Multiply(string FactorA, string FactorB)
        {
            //找出小数位数
            int posA = FactorA.Length - FactorA.IndexOf(".") - 1;
            int posB = FactorB.Length - FactorB.IndexOf(".") - 1;
            //若没有找到小数点则清空找到的小数点位数
            if (FactorA.IndexOf(".") == -1)
            {
                posA = 0;
            }
            if (FactorB.IndexOf(".") == -1)
            {
                posB = 0;
            }


            //Console.WriteLine("posA:" + posA + "        posB:" + posB);

            //去掉小数点
            FactorA = FactorA.Replace(".", "");
            FactorB = FactorB.Replace(".", "");
            //Console.WriteLine("FactorA:" + FactorA + "      FactorB:" + FactorB);

            //相乘
            BigInteger TempA = BigInteger.Parse(FactorA);
            BigInteger TempB = BigInteger.Parse(FactorB);
            BigInteger Result = TempA * TempB;
            //Console.WriteLine("Result:" + Result);

            //转换String
            string ResultString = Result.ToString();
            //Console.WriteLine("ResultString:" + ResultString);

            //加上小数点
            ResultString = ResultString.Insert(ResultString.Length - (posA + posB), ".");
            //Console.WriteLine("The Point Pos:"+ (ResultString.Length - (posA + posB)));
            //Console.WriteLine("ResultString:" + ResultString);
            //若没有小数位数
            if (ResultString.IndexOf(".") == ResultString.Length - 1)
            {
                ResultString.Replace(".", "");
            }
            return ResultString;
        }
        /// <summary>
        /// 除法运算
        /// </summary>
        /// <param name="Divident">被除数</param>
        /// <param name="Divisor">除数</param>
        /// <param name="bits">保留位数</param>
        /// <returns></returns>
        static public string Divide(string Dividend, string Divisor, int bit)
        {
            //除数不为0
            if (Divisor == "0")
            {
                return "";
            }

            //被除数 除数 化为整数
            int DecimalBitsA = Dividend.Length - Dividend.IndexOf(".") - 1;
            int DecimalBitsB = Divisor.Length - Divisor.IndexOf(".") - 1;
            //一样大为没有小数
            if (DecimalBitsA == Dividend.Length)
            {
                DecimalBitsA = 0;
            }
            if (DecimalBitsB == Divisor.Length)
            {
                DecimalBitsB = 0;
            }
            //Console.WriteLine("Bits: A:{0}\tB:{1}", DecimalBitsA, DecimalBitsB);

            //被除数和除数都为小数
            if (DecimalBitsA != 0 && DecimalBitsB != 0)
            {
                StringBuilder Dvd = new StringBuilder(Dividend);
                StringBuilder Dvs = new StringBuilder(Divisor);
                //若被除数小数位数更多 则被除数移去小数点 除数补0
                Dvd.Replace(".", "");
                Dvs.Replace(".", "");
                if (DecimalBitsA > DecimalBitsB)
                {
                    //Dvd.Replace(".", "");
                    //Dvs.Replace(".", "");
                    for (int i = 0; i < DecimalBitsA - DecimalBitsB; i++)
                    {
                        Dvs.Append("0");
                    }
                }
                else if (DecimalBitsB > DecimalBitsA)
                {
                    //Dvd.Replace(".", "");
                    //Dvs.Replace(".", "");
                    for (int i = 0; i < DecimalBitsB - DecimalBitsA; i++)
                    {
                        Dvd.Append("0");
                    }
                }
                //BigInteger类自动能去掉首0 如 00001 = 1
                Dividend = Dvd.ToString();
                Divisor = Dvs.ToString();
            }
            //被除数和除数有一方为小数 另一方为整数:
            //被除数为整数 PS:未找到小数点为-1 所以小数长度会大于或等于整个长度 减去1为小数点
            if (DecimalBitsA == 0 && DecimalBitsB != 0)
            {
                StringBuilder Dvd = new StringBuilder(Dividend);
                StringBuilder Dvs = new StringBuilder(Divisor);
                Dvs.Replace(".", "");
                for (int i = 0; i < DecimalBitsB; i++)
                {
                    Dvd.Append("0");
                }
                Dividend = Dvd.ToString();
                Divisor = Dvs.ToString();
            }//除数为整数
            else if (DecimalBitsB == 0 && DecimalBitsA != 0)
            {
                StringBuilder Dvd = new StringBuilder(Dividend);
                StringBuilder Dvs = new StringBuilder(Divisor);
                Dvd.Replace(".", "");
                for (int i = 0; i < DecimalBitsA; i++)
                {
                    Dvs.Append("0");
                }
                Dividend = Dvd.ToString();
                Divisor = Dvs.ToString();
            }
            //Console.WriteLine("Dividend:{0}\tDivisor:{1}", Dividend, Divisor);

            //被除数 除数 余数
            BigInteger Dd = BigInteger.Parse(Dividend);
            BigInteger Ds = BigInteger.Parse(Divisor);
            BigInteger Remain;

            //整数部分
            StringBuilder result = new StringBuilder(BigInteger.DivRem(Dd, Ds, out Remain).ToString());
            //除尽直接返回
            if (Remain == 0)
            {
                return result.ToString();
            }

            //开始计算
            result.Append(".");

            /*
            //若要保留位数小于50位 则不寻找循环 节省效率
            int KeepBit;
            if (bit < 50)
                KeepBit = bit;
            else
                KeepBit = 50;
            */

            //开始除 除到倒数第二位
            for (int i = 1; i < bit; i++)
            {
                //用余数的10倍除去除数
                result.Append(BigInteger.DivRem(Remain * 10, Ds, out Remain));
            }
            //四舍五入
            int Last = int.Parse(BigInteger.DivRem(Remain * 10, Ds, out Remain).ToString());
            int LastLast = int.Parse(BigInteger.DivRem(Remain * 10, Ds, out Remain).ToString());
            if (LastLast > 5)
            {
                Last++;
            }
            result.Append(Last);

            /*
            //寻找循环节 增加效率
            if (KeepBit < bit)
            {
                
            }
            */
            return result.ToString();
        }
    }
}
