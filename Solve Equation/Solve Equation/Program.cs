using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary;
//请按数学规则写法写方程
namespace Solve_Equation
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //new Equation(EquationEg);
            Equation E = new Equation("5x+9+4=3-8+4");
            Console.ReadKey();
        }
    }
    class Equation
    {
        public Equation(string equation)
        {

        }
    }
    class Term
    {
        public Term(bool IsPlus,int Value)
        {
            this.IsPlus = IsPlus;
            this.Value = Value;
        }
        public Term(bool IsPlus,string Unknown, int Value)
        {
            this.IsPlus = IsPlus;
            this.Unknown = Unknown;
            this.Value = Value;
        }
        public bool IsPlus = true;//符号 +:true
        public bool IsUnknow = false;//是否有未知数
        public string Unknown = "";
        public double Value = 0;
        //public string Power = "1";
    }

}
/*//过程
        static void ProcessAll(string Equation)
        {
            Term[] Terms = new Term[2];
            GetTerms(Equation);
            //Console.WriteLine(Solve(Equation));

        }
        //把含未知数的项移到左边,把常数项移到右边
        static Term[] GetTerms(string Equation)
        {
            //"5x+9+4=3-8+4"
            //int Home = 0;
            //int End = 0;
            char[] Sign = new char[] { '+', '-' };

            string[] LR = Equation.Split('=');
            string[] LTerms_S = LR[0].Split(Sign);
            string[] RTerms_S = LR[1].Split(Sign);
            Term[] Terms = new Term[LTerms_S.Length + RTerms_S.Length];
            for (int x = 0; x < LTerms_S.Length; x++)
            {
                if (LTerms_S[x].IndexOf('x') == -1)
                {
                    Terms[x].Value = int.Parse(LTerms_S[x]);
                    if (Equation.Substring(Equation.IndexOf(LTerms_S[x]) - 1, 1) == "-")
                    {

                    }
                }
                
            }
            return Terms;
        }
        //解二项方程
        static double Solve(string Equation)
        {
            string[] LR = Equation.Split('=');
            //如果x在右边,把未知项x移到左边
            if (LR[1].IndexOf("x") != -1)
            {
                string Temp = LR[1];
                LR[1] = LR[0];
                LR[0] = Temp;
            }

            //Console.WriteLine(LR[0] + "  " + LR[1]);
            //Console.WriteLine(GetCoefficient(LR[0]));
            //解方程,用右边的数除去x的系数
            return double.Parse(LR[1]) / GetCoefficient(LR[0]);
        }
        //取二项式未知数项的系数
        static int GetCoefficient(string key)
        {
            return int.Parse(key.Substring(0, key.IndexOf('x')));
        }*/
