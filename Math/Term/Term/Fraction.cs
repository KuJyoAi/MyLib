using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Term
{
    //分数类
    public class Fraction
    {
        public BigInteger nr = 0;//numerator 分子
        public BigInteger dm = 1;//denominator 分母
        public bool sign = true;//符号: true:正 false:负

        public Fraction(int numerator, bool sign = true)
        {
            dm = 1;
            nr = numerator;
            this.sign = sign;
        }
        public Fraction(BigInteger numerator, bool sign = true)
        {
            nr = numerator;
            dm = 1;
            this.sign = sign;
        }
        public Fraction(int numerator, int denominator, bool sign = true)
        {
            nr = numerator;
            dm = denominator;
            this.sign = sign;
        }
        public Fraction(BigInteger numerator, BigInteger denominator, bool sign = true)
        {
            nr = numerator;
            dm = denominator;
            this.sign = sign;
        }

        //运算符
        public static Fraction operator +(Fraction one, Fraction two)
        {
            //two的符号为正 则两者相加
            if (two.sign)
            {
                BigInteger Lcd_dm = GetLCD(one.dm, two.dm);//最小公倍数(结果的分母)
                BigInteger nr = one.nr * (Lcd_dm / one.dm) + two.nr * (Lcd_dm / two.dm);//分子
                bool sign_ = true;

                //变号
                if (nr < 0)
                {
                    sign_ = false;
                    nr = -nr;
                }

                Reduction(ref nr, ref Lcd_dm);

                return new Fraction(nr, Lcd_dm, sign_);
            }
            else
            {
                two.sign = true;
                return one - two;
            }
        }
        public static Fraction operator -(Fraction one, Fraction two)
        {
            if (two.sign)
            {
                BigInteger Lcd_dm = GetLCD(one.dm, two.dm);//最小公倍数(结果的分母)
                BigInteger nr = one.nr * (Lcd_dm / one.dm) - two.nr * (Lcd_dm / two.dm);//分子
                bool sign = true;

                if (nr < 0)
                {
                    sign = false;
                    nr = -nr;
                }

                //约分
                Reduction(ref nr, ref Lcd_dm);

                return new Fraction(nr, Lcd_dm, sign);
            }
            else
            {
                two.sign = true;
                return one + two;
            }
        }
        public static Fraction operator *(Fraction one, Fraction two)
        {
            bool sign_ = true;
            BigInteger nr = one.nr * two.nr;
            BigInteger dm = one.dm * two.dm;

            //符号不相同时
            if (one.sign ^ two.sign)
            {
                sign_ = false;
            }

            //约分
            Reduction(ref nr, ref dm);

            return new Fraction(nr, dm, sign_);
        }
        public static Fraction operator /(Fraction one, Fraction two)
        {
            return one * new Fraction(two.dm, two.nr, two.sign);
        }

        //约分
        public static void Reduction(ref BigInteger one, ref BigInteger two)
        {
            //约分
            BigInteger Gcd = GetGCD(one, two);
            one = one / Gcd;
            two = two / Gcd;
        }
        //least common multiple 取最小公倍数
        public static BigInteger GetLCD(BigInteger one, BigInteger two)
        {
            return one * two / GetGCD(one, two);
        }
        //greatest common divisor 取最大公约数
        public static BigInteger GetGCD(BigInteger one, BigInteger two)
        {
            //错误跳出
            if (one <= 0 || two <= 0)
            {
                return -1;
            }

            //滚动相减法
            while (one != two)
            {
                if (one > two)
                {
                    one = one - two;
                }
                else
                {
                    two = two - one;
                }
            }
            return one;
        }
        //是否为整数
        public static bool IsZ(Fraction key)
        {
            if (key.dm == 1)
            {
                return true;
            }
            return false;
        }
    }
}
