using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MyLibrary
{
    /// <summary>
    /// 单项式
    /// </summary>
    public class Monomial
    {
        public Fraction val;
        public double pow;//分子的幂
        public List<char> NaN = new List<char>();//未知数
        public List<double> powNaN = new List<double>();//未知数的幂,按顺序
        public Monomial(int v)
        {
            val = new Fraction(v, 1);
        }
        public Monomial(double v)
        {
            string text = v.ToString();
            int pow = text.Length - text.IndexOf('.') - 1;
            val = new Fraction((int)(v * Math.Pow(10, pow)), (int)Math.Pow(10, pow));
            //Console.WriteLine(val.Denominator);
            //Console.WriteLine(val.Numerator);
        }
        public Monomial(int num, int den)
        {
            val = new Fraction(num, den);
        }
        public Monomial(int num, int den, double pow)
        {
            if ((int)pow == pow)
            {
                num = (int)Math.Pow(num, pow);
                den = (int)Math.Pow(den, pow);
                pow = 1;
                return;
            }
            val = new Fraction(num, den);
            this.pow = pow;
        }
        public void AppendNaN(char name, double pow)
        {
            NaN.Add(name);
            powNaN.Add(pow);
        }
        public void print()
        {
            Console.Write(val.Numerator + "/" + val.Denominator + "^" + pow);
            for (int i = 0; i < NaN.Count; i++)
            {
                Console.Write(NaN[i] + "^" + powNaN[i]);
            }
        }
        public static Monomial Addtion(Monomial a, Monomial b)
        {
            a.val = Fraction.Addition(a.val, b.val);
            return a;
        }
    }
}
