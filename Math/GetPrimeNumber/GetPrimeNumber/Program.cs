using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPrimeNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            
            /*for (int n = 2; n < 10000; n++)
            {
                if (IsPrimeNumber(n))
                {
                    Console.Write(n + ", ");
                }
            }*/
            Console.WriteLine(IsPrimeNumber(79));
            Console.ReadKey();

        }
        static bool IsPrimeNumber(int number)
        {
            /*
            例如 17是素数，因为它不能被2~16间任意一整数整除。因此判断一个整数m是否为素数，
            只需用2~m-1之间的每一个整数去除，如果都不能被整除，那么m就是一个素数。

            其实可以简化，m不必被2~m-1之间的每一个整数去除，只需被2~根号m之间的每个数去除就可以了。例如判别17是否为素数，
            只需使2~4之间的每一个整数去除。为什么可以做如此简化呢？因为如果m能被2~m-1之间任意整数整除，如果这个数大于根号m，
            那这个数必定对应的还有一个比根号m小的因子（以16为例，2、8是它的因子，8大于4，2小于4）。
            */
            for (int n = 2; n <= Math.Sqrt(number); n++)
            {
                if (number % n == 0)
                {
                    Console.WriteLine(n);
                    return false;
                }
            }
            return true;
        }
    }
}
