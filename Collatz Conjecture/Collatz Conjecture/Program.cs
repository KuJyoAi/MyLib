using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collatz_Conjecture
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i <= 100; i++)
            {

                Console.WriteLine("{0}:{1}", i, getCount(i));
            }
        }
        static int getCount(int key)
        {
            int count = 0;
            while (key != 1)
            {
                if (key % 2 == 1)
                {
                    key = 3 * key + 1;
                    count++;
                }
                else
                {
                    key = key / 2;
                    count++;
                }
            }
            return count;
        }
    }
}
