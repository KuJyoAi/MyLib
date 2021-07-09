using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24_point
{
    class Program
    {
        static List<char> hash = new List<char>();
        static void Main(string[] args)
        {
            hash.Add('+');
            hash.Add('-');
            hash.Add('*');
            hash.Add('/');
            List<string> result = new List<string>();


            oneCycle(1, 2, 3, 4, ref result);
            foreach (string a in result)
            {
                Console.WriteLine(a);
            }

        }
        static void oneCycle(int a, int b, int c, int d, ref List<string> result)
        {
            double tmp = 0;
            for (int i = 0; i < 2; i++)
            {
                tmp = caculate(0, a, i);
                for (int j = 0; j < 4; j++)
                {
                    tmp = caculate(a, b, j);
                    for (int k = 0; k < 4; k++)
                    {
                        tmp = caculate(tmp, c, k);
                        for (int l = 0; l < 4; l++)
                        {
                            tmp = caculate(tmp, d, l);
                            if (tmp == 24)
                            {
                                result.Add(toString(a, b, c, d, i, j, k, l));
                            }
                        }
                    }
                }
            }
        }
        static double caculate(double a, double b, int pattern)
        {
            switch (pattern)
            {
                case 0:
                    return a + b;
                case 1:
                    return a - b;
                case 2:
                    return a * b;
                case 3:
                    return a / b;
                default:
                    return -250;
            }
        }
        static string toString(int a, int b, int c, int d, int i, int j, int k, int l)
        {
            return hash[i] + a.ToString() + hash[j] + b + hash[k] + c + hash[l] + d;
        }
    }
}
