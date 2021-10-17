using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Get_Perfect_Number
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int x = 1; x <= 10000; x++)
            {
                int result = 0;
                for (int y = 1; y < x; y++)
                {
                    if (x % y == 0)
                    {
                        result += y;
                    }
                }
                if (result == x)
                {
                    Console.WriteLine(x);
                }
            }

            Console.WriteLine("完毕");
            Console.ReadKey();
        }
    }
}
