using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MyLibrary
{
    /// <summary>
    /// 数学算法
    /// </summary>
    public static class MathEX
    {
        /// <summary>
        /// 取两个数的Greatest Common Factor(最大公因数)
        /// </summary>
        /// <param name="key1">数1</param>
        /// <param name="key2">数2</param>
        /// <returns>返回key1和key2的最大公因数</returns>
        public static int GetGreatestCF(int key1, int key2)
        {
            //如果某一数为0 则返回0
            if (key1 < 0 && key2 < 0)
            {
                return 0;
            }
            //相减法取最大公因数
            while (key1 != key2)
            {
                if (key1 > key2)
                {
                    key1 -= key2;
                }
                else
                {
                    key2 -= key1;
                }
            }

            return key1;
        }
        /// <summary>
        /// 取一组数据的中位数
        /// </summary>
        /// <param name="key">数据</param>
        /// <returns>返回中位数</returns>
        public static double Median(int[] key)
        {
            Array.Sort(key);//排序
            if (key.Length % 2 == 1)
            {

                return key[(key.Length + 1) / 2 - 1];//若为奇数就返回中间那个

            }
            else
            {
                return (double)(key[key.Length / 2 - 1] + key[key.Length / 2]) / 2;//若为偶数则返回中间两数的平均值
            }
        }
        public static double Median(double[] key)
        {
            Array.Sort(key);//排序
            if (key.Length % 2 == 1)
            {

                return key[(key.Length + 1) / 2 - 1];//若为奇数就返回中间那个

            }
            else
            {
                return (double)(key[key.Length / 2 - 1] + key[key.Length / 2]) / 2;//若为偶数则返回中间两数的平均值
            }
        }
        /// <summary>
        /// 取一组数据的平均数
        /// </summary>
        /// <param name="key">数据</param>
        /// <returns>返回平均数</returns>
        public static double Average(double[] key)
        {
            //全部值加起来
            double all = 0;
            for (int i = 0; i < key.Length; i++)
            {
                all += key[i];
            }
            return all / key.Length;
        }
        public static double Average(int[] key)
        {
            //全部值加起来
            double all = 0;
            for (int i = 0; i < key.Length; i++)
            {
                all += key[i];
            }
            return all / key.Length;
        }
        /// <summary>
        /// 取一组数据的方差,标准差对返回值取算数平方根即可
        /// </summary>
        /// <param name="key">数据</param>
        /// <returns>返回方差</returns>
        public static double Variance(double[] key)
        {
            double all = 0;
            double Ave = Average(key);
            for (int i = 0; i < key.Length; i++)
            {
                all += (key[i] - Ave) * (key[i] - Ave);
            }
            return all / key.Length;
            /*
            快速算法:(X1² + X2² + ... + Xn²)/n - Ave²
            */
        }
        public static double Variance(int[] key)
        {
            double all = 0;
            double Ave = Average(key);
            for (int i = 0; i < key.Length; i++)
            {
                all += (key[i] - Ave) * (key[i] - Ave);
            }
            return all / key.Length;
        }

        /// <summary>
        /// 检测一个数是否为质数
        /// </summary>
        /// <param name="Number">输入值</param>
        /// <returns>返回结果</returns>
        public static bool IsPrime(int Number)
        {
            for (int x = 2; x <= Number / 2; x++)
            {
                //为某数的乘积
                if (Number % x == 0)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsPrime(BigInteger Number)
        {
            for (int x = 2; x <= Number / 2; x++)
            {
                if (Number % x == 0)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 阶乘
        /// </summary>
        /// <param name="times">阶乘次数</param>
        /// <returns>返回结果</returns>
        public static int Factorial(int times)
        {
            int result = 1;
            for (int x = 1; x <= times; x++)
            {
                result = result * x;
            }
            return result;
        }
        /// <summary>
        /// 分解质因数
        /// </summary>
        /// <param name="Value">需分解的数字,为整数</param>
        /// <returns>返回整数集</returns>
        public static int[] DecayNumber(string Value)
        {
            BigInteger Key = BigInteger.Parse(Value);
            if (Key == 1)
            {
                return new int[1] { 1 };
            }
            else if (Key < 1)
            {
                return new int[1] { 0 };
            }
            
            //数集
            List<int> NumberList = new List<int>();
            while (Key != 1)
            {
                //一个一个取出质因数
                for (int i = 2; i <= Key; i++)
                {
                    //Console.WriteLine("i:{0}\tKey:{1}", i, Key);
                    if (Key % i == 0)
                    {
                        NumberList.Add(i);
                        Key = Key / i;
                        break;
                    }
                }
            }
            return NumberList.ToArray();
        }
    }
}
