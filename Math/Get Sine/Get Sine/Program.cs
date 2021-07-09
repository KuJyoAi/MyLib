using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using MyLibrary;

namespace Get_Sine
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = "1";
            BigInteger JC = new BigInteger(6);
            result = LargeNumber.Subtract(result, LargeNumber.Divide("1", JC.ToString(), 40));
            JC = 120;
            result = LargeNumber.Add(result, LargeNumber.Divide("1", JC.ToString(), 40));
            JC = 5040;
            result = LargeNumber.Subtract(result, LargeNumber.Divide("1", JC.ToString(), 40));
            JC = 362880;
            result = LargeNumber.Add(result, LargeNumber.Divide("1", JC.ToString(), 40));
            JC = 3991680;
            result = LargeNumber.Subtract(result, LargeNumber.Divide("1", JC.ToString(), 40));
            JC = 6227020800;
            result = LargeNumber.Add(result, LargeNumber.Divide("1", JC.ToString(), 40));
            JC = JC * 14 * 15;
            result = LargeNumber.Subtract(result, LargeNumber.Divide("1", JC.ToString(), 40));
            JC = JC * 16 * 17;
            result = LargeNumber.Add(result, LargeNumber.Divide("1", JC.ToString(), 40));
            Console.WriteLine(result);
        }
        /*static string GetNearlyZero(int angel)
        {
            //弧度制
            BigInteger JC = 1;
            int terms = 10;
            string value = angel.ToString();
            string valueSqare = (angel * angel).ToString();
            string result = "0";
            for (int i = 1; i < terms; i++)
            {
                JC = JC * i;
                if (i % 2 != 0)
                {
                    if ((i + 1) % 4 == 0)
                    {
                        Console.WriteLine("Value:{0}\tresult:{1}\tJC:{2}", value, result, JC);
                        Console.WriteLine("term:" + LargeNumber.Add(result, LargeNumber.Divide(value, JC.ToString(), 20)));
                        result = LargeNumber.Add(result, LargeNumber.Divide(value, JC.ToString(), 20));
                        value = LargeNumber.Multiply(value, valueSqare);
                    }
                    else if((i + 1) % 4 == 2)
                    {
                        Console.WriteLine("Value:{0}\tresult:{1}\tJC:{2}", value, result, JC);
                        Console.WriteLine("term:" + LargeNumber.Subtract(result, LargeNumber.Divide(value, JC.ToString(), 20)));
                        result = LargeNumber.Subtract(result, LargeNumber.Divide(value, JC.ToString(), 20));
                        value = LargeNumber.Multiply(value, valueSqare);
                    }
                }
            }
            return result;
        }*/
    }
}
