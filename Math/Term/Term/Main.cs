using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Term
{
    public class Term
    {
        Fraction value;
        List<Unknown> uns;
        public Term(Fraction value, List<Unknown> Unknows = null)
        {
            this.value = value;
            uns = Unknows;
        }
        public static Term operator +(Term one, Term two)
        {
            //检测同类项
            if (one.uns.Count == two.uns.Count)
            {
                for (int i = 0; i < one.uns.Count; i++)
                {
                    if (!one.uns.Contains(two.uns[i]))
                    {

                        return one;
                    }
                }
            }
        }
        public static bool isSimilarItems(Term one, Term two)
        {
            if (one.uns.Count == two.uns.Count)
            {
                for (int i = 0; i < one.uns.Count; i++)
                {

                }
            }
        }
    }

    //未知数
    public class Unknown
    {
        public int power = 1;//次方
        public int cft = 1;//coefficient 系数
        public char name;//名字
        public List<int> pos;//下标

        public Unknown(char name, int coefficient = 1, int power = 1)
        {
            this.name = name;
            cft = coefficient;
            this.power = power;
        }
        public Unknown(char name, List<int> pos,int coefficient = 1, int power = 1)
        {
            this.name = name;
            cft = coefficient;
            this.power = power;
            this.pos = pos;
        }
        public Unknown(char name, int pos, int coefficient = 1, int power = 1)
        {
            this.name = name;
            cft = coefficient;
            this.power = power;

            this.pos = new List<int>();
            this.pos.Add(pos);
        }
    }
    
}
