using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MyLibrary
{
    class Math_L
    {
        /// <summary>
        /// 大数取绝对值
        /// </summary>
        /// <param name="">数</param>
        /// <returns></returns>
        public static BigInteger Abs(BigInteger a)
        {
            if (a >= 0)
                return a;
            else
                return -a;
        }
        /// <summary>
        /// 取两个数的Greatest Common Factor(最大公因数)
        /// </summary>
        /// <param name="key1">数1</param>
        /// <param name="key2">数2</param>
        /// <returns>返回key1和key2的最大公因数</returns>
        public static BigInteger GetGreatestCF(BigInteger key1, BigInteger key2)
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
    }
}
