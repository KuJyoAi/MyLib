using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Big_Prize___International_Olympiad_in_Informatics_2017
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    class BigPrize
    {
        int[] list;
        public BigPrize(int n)
        {
            Random r = new Random();
            list = new int[n];
            list[r.Next(0, n)] = 1;

        }
    }
}
