using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    /// <summary>
    /// 分数运算
    /// </summary>
    public class Fraction
    {
        public Fraction(int Numerator, int Denominator)
        {
            //如果分母是0直接跳出
            if (Denominator == 0)
            {
                return;
            }
            //如果同为正或同为负,则为正号,如果不同,则为负号(0在此规定为正数)
            if ((Denominator >= 0) == (Numerator > 0))
            {
                IsPlus = true;
                //分子分母化正
                this.Numerator = Math.Abs(Numerator);
                this.Denominator = Math.Abs(Denominator);
            }
            else
            {
                IsPlus = false;
                this.Numerator = Math.Abs(Numerator);
                this.Denominator = Math.Abs(Denominator);
            }
            //this.Denominator = Denominator;
            //this.Numerator = Numerator;
        }
        //分子
        public int Numerator;
        //分母
        public int Denominator;
        //分数符号,true:+ , false:-
        public bool IsPlus = true;

        //判断符号
        public static Fraction WhatSign(Fraction a)
        {
            //如果同为正或同为负,则为正号,如果不同,则为负号
            if ((a.Denominator > 0) == (a.Numerator > 0))
            {
                a.IsPlus = true;
            }
            else
            {
                a.IsPlus = false;
            }
            return a;
        }
        /// <summary>
        /// 乘法
        /// </summary>
        /// <param name="a">乘数a</param>
        /// <param name="b">乘数b</param>
        /// <returns>结果</returns>
        public static Fraction Multiplication(Fraction a, Fraction b)
        {
            //如果结果的分子为0,则直接返回0
            if (a.Numerator == 0 || b.Numerator == 0)
            {
                return new Fraction(0, a.Denominator * b.Denominator);
            }

            //分子*分子,分母*分母
            
            Fraction result = Fraction.Simplify(new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator));
            //如果乘数a和b都是正或负,则积的符号为正
            if (WhatSign(a) == WhatSign(b))
            {
                result.IsPlus = true;
                return result;
            }
            else
            {
                result.IsPlus = false;
                return result;
            }
        }
        /// <summary>
        /// 除法
        /// </summary>
        /// <param name="a">被除数</param>
        /// <param name="b">除数</param>
        /// <returns>结果</returns>
        public static Fraction Division(Fraction a,Fraction b)
        {
            //分母不能为0
            if (a.Denominator == 0 || b.Denominator == 0)
            {
                return new Fraction(0, 1);
            }
            //分子分母换顺序后相乘再化简出结果
            return Simplify(Multiplication(a,new Fraction(b.Denominator,b.Numerator)));
        }
        /// <summary>
        /// 加法
        /// </summary>
        /// <param name="a">加数</param>
        /// <param name="b">加数</param>
        /// <returns>结果</returns>
        public static Fraction Addition(Fraction a, Fraction b)
        {
            //交叉相乘法
            // a/b+c/d
            //=ad/bd+bc/bd
            //=(ad+bc)bd
            /*
            1.a>0,b>0:a+b
            2.a>0,b<0:a-b
            3.a<0,b<0:-(a+b)
            4.a<0,b>0:b-a
            */
            //a>0,b>0
            if ((a.IsPlus) && (b.IsPlus))
            {
                //创建时已经设置好符号
                Fraction result = new Fraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);
                //计算符号后返回化简
                return Simplify(result);
            }
            //a<0,b<0
            else if ((!a.IsPlus) && (!b.IsPlus))
            {
                Fraction result = new Fraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);
                //计算符号后返回化简
                result.IsPlus = false;
                return Simplify(result);
            }
            //a>0,b<0
            else if ((a.IsPlus) && (!b.IsPlus))
            {
                Fraction result = new Fraction(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator);
                //计算符号后返回化简
                return Simplify(result);
            }
            //a<0,b>0
            else if ((!a.IsPlus) && (b.IsPlus))
            {
                Fraction result = new Fraction(b.Numerator * a.Denominator - a.Numerator * b.Denominator, a.Denominator * b.Denominator);
                //计算符号后返回化简
                return Simplify(result);
            }
            else
            {
                return new Fraction(0, 1);

            }

        }
        /// <summary>
        /// 减法
        /// </summary>
        /// <param name="a"><被减数/param>
        /// <param name="b">减数</param>
        /// <returns>结果</returns>
        public static Fraction Subtraction(Fraction a, Fraction b)
        {
            //a-b=a+(-b)
            b.IsPlus = !b.IsPlus;
            return Addition(a, b);
        }
        /// <summary>
        /// 化简分数
        /// </summary>
        /// <param name="key">需化简的分数</param>
        /// <returns>化简后</returns>
        public static Fraction Simplify(Fraction key)
        {
            //如果分子为0,则直接返回0
            if (key.Numerator == 0)
            {
                return new Fraction(0, key.Denominator);
            }

            //解出最大公约数
            int GeatestCF = MyLibrary.MathEX.GetGreatestCF(key.Numerator,key.Denominator);
            key.Numerator = key.Numerator / GeatestCF;
            key.Denominator = key.Denominator / GeatestCF;
            //Console.WriteLine(GeatestCF);
            return key;
        }
        //转文本显示
        public static string ToString(Fraction key)
        {
            //分母等于1直接返回分子
            if (key.Denominator == 1)
            {
                int result = key.Numerator;
                //带符号
                if (key.IsPlus)
                {
                    return result.ToString();
                }
                else
                {
                    return (-result).ToString();
                }
            }
            //如果分子不等于0再输出,等于0直接返回0
            else if (key.Numerator != 0)
            {
                string result = key.Numerator + "/" + key.Denominator;
                if (key.IsPlus)
                {
                    return result;
                }
                else
                {
                    return "-" + result;
                }
            }
            return "0";
        }
    }
}
