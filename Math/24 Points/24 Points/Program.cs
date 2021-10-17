using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24_Points
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] num = new int[4] { 3, 3, 8, 8 };

            List<string> resc = Enum_num(num);
            for (int i = 0; i < resc.Count; i++)
            {
                resc[i] = resc[i].Replace("[0]", "+");
                resc[i] = resc[i].Replace("[1]", "-");
                resc[i] = resc[i].Replace("[2]", "*");
                resc[i] = resc[i].Replace("[3]", "/");
                Console.WriteLine(resc[i]);
                
            }
            Console.WriteLine(resc.Count);
            /* 
            int rcount = 0;
            for (int a = 1; a < 14; a++)
            {
                for (int b = 1; b < 14; b++)
                {
                    for (int c = 1; c < 14; c++)
                    {
                        for (int d = 1; d < 14; d++)
                        {
                            if (Enum_num(new int[4] { a, b, c, d }).Count != 0)
                            {
                                rcount++;
                            }
                        }
                    }
                }
            }
            Console.WriteLine(rcount);
            */
        }
        static List<string> Enum_num(int[] num)
        {
            List<string> resc = new List<string>();
            for (int a = 0; a < 4; a++)
            {
                for (int b = 0; b < 4; b++)
                {
                    for (int c = 0; c < 4; c++)
                    {
                        for (int d = 0; d < 4; d++)
                        {
                            if (a == b || b == c || c == d || a == c || a == d || b == d)
                            {
                                break;
                            }
                            resc.AddRange(Enum_calc(num[a], num[b], num[c], num[d]));
                            //Console.WriteLine("{0}{1}{2}{3}", a, b, c, d);
                        }
                    }
                }
            }
            return resc;
        }
        static List<string> Enum_calc(int a, int b, int c, int d)
        {
            int result1 = 0;
            int result2 = 0;
            int result3 = 0;
            List<string> resc = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                for (int o = 0; o < 4; o++)
                {
                    for (int p = 0; p < 4; p++)
                    {
                        
                    }
                }
            }
            /*
            //上面无法计算双括号,因此作个补充:(a_b)_(c_d)
            for (int i = 0; i < 4; i++)
            {
                for (int o = 0; o < 4; o++)
                {
                    for (int p = 0; p < 4; p++)
                    {
                        result1 = calc(a, b, i);
                        if (i == 3 && result1 == -250)
                        {
                            break;
                        }
                        result2 = calc(c, d, o);
                        if (o == 3 && result2 == -250)
                        {
                            break;
                        }
                        result3 = calc(result1, result2, p);
                        if (p == 3 && result3 == -250)
                        {
                            break;
                        }
                        //Console.WriteLine(("(" + a + "[" + i + "]" + b + ")" + "[" + p + "]" + "(" + c + "[" + o + "]" + d + ")" + "=" + result3));
                        if (result3 == 24)
                        {
                            resc.Add("(" + a + "[" + i + "]" + b + ")" + "[" + p + "]" + "(" + c + "[" + o + "]" + d + ")");
                        }
                    }
                }
            }*/
            return resc;
        }
        static string Type_calc(int a, int b, int c, int d, int i, int o, int p)
        {

            int result1 = 0;
            int result2 = 0;
            int result3 = 0;
            result1 = calc(a, b, i);
            if (i == 3 && result1 == -250)
            {
                return "";
            }
            result2 = calc(result1, c, o);
            if (o == 3 && result2 == -250)
            {
                return "";
            }
            result3 = calc(result2, d, p);
            if (p == 3 && result3 == -250)
            {
                return "";
            }
            //Console.WriteLine((a + "[" + i + "]" + b + "[" + o + "]" + c + "[" + p + "]" + d + "=" + result3));
            if (result3 == 24)
            {
                return "((" + a + "[" + i + "]" + b + ")" + "[" + o + "]" + c + ")" + "[" + p + "]" + d;
            }
        }

        static int calc(int a, int b, int type)
        {
            switch (type)
            {
                case 0:
                    return a + b;
                case 1:
                    return a - b;
                case 2:
                    return a * b;
                case 3:
                    if (b != 0 && a % b == 0)
                    {
                        return a / b;
                    }
                    else
                    {
                        //不整除,错误
                        return -250;
                    }
                default:
                    return -250;
            }
        }
    }
}
