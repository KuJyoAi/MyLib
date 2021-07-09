using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary;
using System.Diagnostics;
using System.Numerics;

namespace Get_PI
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            //Console.WriteLine(GetPi(12));
            Stopwatch S = new Stopwatch();
            S.Start();
            Console.WriteLine(GetPiD(1000000000) * 4);
            S.Stop();
            Console.WriteLine("用时:{0}",S.ElapsedMilliseconds);
            */
            Console.WriteLine(GetPiBig(100) * 4);
        }
        static double GetPiBig(int times)
        {
            //确定分母的值
            BigInteger Deno = 1;
            for (int i = 0; i < times; i++)
            {
                Deno = Deno * (2 * i + 1);
            }
            //Console.WriteLine("Deno:" + Deno);

            //分子加值
            //1 - 1/3 + 1/5 = (15/1)/15 - (15/3)/15 + (15/5)/15
            BigInteger Nume = Deno;
            for (int i = 1; i < times; i++)
            {
                if (i % 2 != 0)
                    Nume = Nume - Deno / (2 * i + 1);
                else
                    Nume = Nume + Deno / (2 * i + 1);

                //Console.WriteLine("Nume:" + Nume);
            }

            //Console.WriteLine("Nume:{0} \t Deno:{1}", Nume, Deno);
            return double.Parse(Nume.ToString()) / double.Parse(Deno.ToString());
        }

        static string GetPi(int times)
        {
            Fraction result = new Fraction(0, 1);
            for (int i = 0; i < times; i++)
            {
                Fraction Addition = new Fraction(1, 2 * i + 1);
                if (i % 2 == 0)
                    result = Fraction.Addition(result, Addition);
                else
                    result = Fraction.Subtraction(result, Addition);
            }
            return Fraction.ToString(result);
        }

        static double GetPiD(int times)
        {
            double result = 0;
            for (int i = 0; i < times; i++)
            {
                if (i % 2 == 0)
                    result = result + 1 / (double)(2 * i + 1);
                else
                    result = result - 1 / (double)(2 * i + 1);
            }
            return result;
        }
    }
}
