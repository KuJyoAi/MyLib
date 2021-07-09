using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace RecognitionDLL
{
    public class Monomial
    {
        Hashtable names = new Hashtable();
        List<CalcUnit> strc = new List<CalcUnit>();
        string expression;
        double x = 0;
        bool isNumber(string resource)
        {
            foreach (char c in resource)
            {
                //非数字返回false
                if (!char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }
        void LoadFunctionNames()
        {
            //names.Add("address", 0);
            names.Add("add", 1);
            names.Add("minus", 2);
            names.Add("multiply", 3);
            names.Add("divide", 4);
            names.Add("sqare", 5);
            names.Add("sqrt", 6);
            names.Add("pow", 7);
            names.Add("root", 8);
            names.Add("sin", 9);
            names.Add("cos", 10);
            names.Add("tan", 11);
            names.Add("csc", 12);
            names.Add("sec", 13);
            names.Add("cot", 14);
            names.Add("arcsin", 15);
            names.Add("arccos", 16);
            names.Add("arctan", 17);
            names.Add("arccsc", 18);
            names.Add("arcsec", 19);
            names.Add("arccot", 20);
            names.Add("toarc", 21);
            names.Add("todegree", 22);
            names.Add("ln", 23);
            names.Add("lg", 24);
            names.Add("log", 25);
            names.Add("abs", 26);
        }
        int[] getBlockLR(string res, string center)
        {
            int L = res.IndexOf(center);
            int R = L + center.Length;
            int LL = 0;//左边部分起始
            int RR = res.Length;//右边部分起始，到尾都没找到则返回最后
            //Console.WriteLine("L:{0} R:{1}", L, R);

            char[] sum = res.ToArray();

            //Console.WriteLine("Left");
            //左边查找
            int count = 0;//括号数量
            for (int i = L - 1; i >= 0; i--)
            {
                //Console.WriteLine("i:{0} count:{1} sum:{2}", i, count, sum[i]);
                if (sum[i] == ')')
                {
                    count++;
                }
                else if (sum[i] == '(')
                {
                    count--;

                }
                //当没有括号且遇到符号时分区
                if (count == 0)
                {
                    if (sum[i] == '+' || sum[i] == '-' || sum[i] == '*' || sum[i] == '/' || sum[i] == '^')
                    {
                        LL = i + 1;
                        break;
                    }
                }
                else if (count == -1)
                {
                    //遇到左括号直接结束
                    LL = i + 1;
                    break;
                }
            }

            //Console.WriteLine("Right");
            //右边查找
            count = 0;
            for (int i = R; i < sum.Length; i++)
            {
                //Console.WriteLine("i:{0} count:{1} sum:{2}", i, count, sum[i]);
                if (sum[i] == '(')
                {
                    count++;
                }
                else if (sum[i] == ')')
                {
                    count--;

                }
                //当没有括号且遇到符号时分区
                if (count == 0)
                {
                    if (sum[i] == '+' || sum[i] == '-' || sum[i] == '*' || sum[i] == '/' || sum[i] == '^')
                    {
                        RR = i;
                        break;
                    }
                }
                else if (count == -1)
                {
                    //遇到左括号直接结束
                    RR = i;
                    break;
                }
            }

            //Console.WriteLine("LL:{0} L:{1} R:{2} RR:{3}", LL, L, R, RR);

            int[] result = new int[4];
            result[0] = LL;
            result[1] = L;
            result[2] = R;
            result[3] = RR;
            return result;
        }
        void Transform3()
        {
            //加减转换
            char[] two = new char[2];
            two[0] = '+';
            two[1] = '-';

            int pos = 0;
            while (true)
            {
                pos = expression.IndexOfAny(two);//找到符号
                if (pos == -1)
                {
                    break;
                }

                //取得符号
                string sign = expression.Substring(pos, 1);

                //替换文本
                string newValue;
                if (sign == "+")
                {
                    newValue = "add";
                }
                else
                {
                    newValue = "minus";
                }
                int[] position = getBlockLR(expression, sign);
                newValue += "(" + expression.Substring(position[0], position[1] - position[0]) + "," + expression.Substring(position[2], position[3] - position[2]) + ")";

                expression = expression.Replace(expression.Substring(position[0], position[3] - position[0]), newValue);

                //Console.WriteLine();
                //Console.WriteLine("{0},{1},{2},{3}", position[0], position[1], position[2], position[3]);
                //Console.WriteLine(newValue);
                //Console.WriteLine(expression);
                //Console.WriteLine();

            }


        }
        void Transform2()
        {
            //乘除转换
            char[] two = new char[2];
            two[0] = '*';
            two[1] = '/';

            int pos = 0;
            while (true)
            {
                pos = expression.IndexOfAny(two);//找到符号
                if (pos == -1)
                {
                    break;
                }

                //取得符号
                string sign = expression.Substring(pos, 1);

                //替换文本
                string newValue;
                if (sign == "*")
                {
                    newValue = "multiply";
                }
                else
                {
                    newValue = "divide";
                }
                int[] position = getBlockLR(expression, sign);
                //Console.WriteLine("{0},{1},{2},{3}", position[0], position[1], position[2], position[3]);
                newValue += "(" + expression.Substring(position[0], position[1] - position[0]) + "," + expression.Substring(position[2], position[3] - position[2]) + ")";

                expression = expression.Replace(expression.Substring(position[0], position[3] - position[0]), newValue);
                //Console.WriteLine(newValue);
                //Console.WriteLine(expression);
                //Console.WriteLine();
            }
        }
        void Transform1()
        {

            //次方转换
            int pos = 0;
            while (true)
            {
                pos = expression.IndexOf('^');//找到符号
                if (pos == -1)
                {
                    break;
                }

                //取得符号
                string sign = expression.Substring(pos, 1);

                //替换文本
                string newValue = "pow";
                int[] position = getBlockLR(expression, sign);
                newValue += "(" + expression.Substring(position[0], position[1] - position[0]) + "," + expression.Substring(position[2], position[3] - position[2]) + ")";

                expression = expression.Replace(expression.Substring(position[0], position[3] - position[0]), newValue);
                //Console.WriteLine(newValue);
                //Console.WriteLine(expression);

            }
        }
        public Monomial(string resource)
        {

            expression = resource;

            //初始化载入函数
            LoadFunctionNames();

            //标准化(复合函数化)
            Transform1();
            Transform2();
            Transform3();
            //Console.WriteLine(expression);

            unsafe
            {
                fixed (double* pX = &x)
                {
                    IntPtr p = (IntPtr)pX;
                    expression = expression.Replace("x", "address(" + p.ToString() + ")");
                }
            }
            analyze(expression);

        }
        IntPtr analyze(string resource)
        {
            //Console.WriteLine(resource);
            string name = resource.Substring(0, resource.IndexOf('('));//取得函数名
            //未取出，去括号重新抛入
            if (name == "")
            {
                return analyze(resource.Substring(1, resource.Length - 2));
            }
            //最终迭代至x
            else if (name == "address")
            {
                //address(42217588)
                return new IntPtr(int.Parse(resource.Substring(8, 8)));
            }

            char[] tmp = resource.ToArray();//散列
            string left;
            string right;
            int L = resource.IndexOf('(') + 1;//起始点(左括号位置+1)

            int M = L;//逗号所在位置
            int count = 0;

            //寻找分割逗号
            for (int i = L; i < resource.Length; i++)
            {
                if (tmp[i] == '(')
                {
                    count++;
                }
                else if (tmp[i] == ')')
                {
                    count--;
                }

                if (tmp[i] == ',')
                {
                    if (count == 0)
                    {
                        M = i;
                    }
                }
            }

            if (M != L)
            {
                //双输入函数处理


                left = resource.Substring(L, M - L);
                right = resource.Substring(M + 1, resource.Length - M - 2);

                //Console.WriteLine("Left:" + left);
                //Console.WriteLine("Right" + right);

            }
            else
            {
                //单输入函数处理
                left = resource.Substring(L, resource.Length - L - 1);
                right = "";
            }

            //Console.WriteLine("-----");
            //Console.WriteLine(left);
            //Console.WriteLine(right);
            //Console.WriteLine("-----");

            double obj1 = 0;
            double obj2 = 0;
            IntPtr ads1 = new IntPtr();
            IntPtr ads2 = new IntPtr();
            bool isAddress1 = false;
            bool isAddress2 = false;

            //左边转化
            if (isNumber(left))
            {
                obj1 = double.Parse(left);
            }
            else
            {
                //非数字
                IntPtr pL = analyze(left);
                obj1 = (double)Marshal.PtrToStructure(pL, typeof(double));
                ads1 = pL;
                isAddress1 = true;
                //Console.WriteLine(obj1);
            }
            /*try
            {
                //尝试转化
                obj1 = double.Parse(left);
            }
            finally
            {
                //非数字
                IntPtr pL = analyze(left);
                obj1 = (double)Marshal.PtrToStructure(pL, typeof(double));
                //Console.WriteLine(obj1);
            }
            */
            //右边转化
            if (right != "")
            {
                /*try
                {
                    obj2 = double.Parse(right);
                }
                catch
                {
                    IntPtr pR = analyze(right);
                    obj2 = (double)Marshal.PtrToStructure(pR, typeof(double));
                    //Console.WriteLine(obj2);
                    throw;
                }*/
                if (isNumber(right))
                {
                    obj2 = double.Parse(right);
                }
                else
                {
                    //非数字
                    IntPtr pR = analyze(right);
                    obj2 = (double)Marshal.PtrToStructure(pR, typeof(double));
                    ads2 = pR;
                    isAddress2 = true;
                    //Console.WriteLine(obj1);
                }
            }

            CalcUnit cu = new CalcUnit(obj1, obj2, ads1, ads2, (int)names[name], isAddress1, isAddress2);


            strc.Add(cu);

            IntPtr ptr;//cu的结果指针
            unsafe
            {
                fixed (double* tmp_ = &cu.ans)
                {
                    ptr = (IntPtr)tmp_;
                }
            }

            return ptr;
        }
        public double calc(double input)
        {
            x = input;
            foreach (CalcUnit c in strc)
            {
                c.calc();
                //print();
            }
            return strc[strc.Count - 1].ans;
        }
        public void print()
        {
            Console.WriteLine("-----------------------");
            foreach (CalcUnit c in strc)
            {
                Console.WriteLine("obj1:{0} obj2:{1} type:{2} ans:{3}", c.obj1, c.obj2, c.type, c.ans);
            }
            Console.WriteLine("-----------------------");
        }
        public void printPtr()
        {
            Console.WriteLine("-----------------------");
            foreach (CalcUnit c in strc)
            {
                unsafe
                {
                    fixed (double* tmp_3 = &c.ans)
                    {
                        Console.WriteLine("obj1:{0} obj2:{1} ans:{2}", c.ads1, c.ads2, (IntPtr)tmp_3);
                    }
                }

            }
            Console.WriteLine("-----------------------");
        }

        /*string getLeft(string key)
        {
            return expression.Substring(0, expression.IndexOf(key));
        }
        string getRight(string key)
        {
            return expression.Substring(expression.IndexOf(key) + 1);
        }
        string getMid(string left, string right)
        {
            int lpos = expression.IndexOf(left) + 1;
            int rpos = expression.IndexOf(right);
            return expression.Substring(lpos + left.Length - 1, rpos - lpos - left.Length + 1);
        }*/
    }
    public class CalcUnit
    {
        public double obj1 = 0;
        public IntPtr ads1;
        public bool isAds1 = false;

        public double obj2 = 0;
        public IntPtr ads2;
        public bool isAds2 = false;

        public int type = 0;
        public double ans = 0;
        public string expression;
        //输入的表达式必须是标准形式
        /*public CalcUnit(string expression)
        {
            this.expression = expression;
            if (expression.IndexOf('+') != -1)
            {
                type = 1;
                obj1 = double.Parse(getLeft("+"));
                obj2 = double.Parse(getRight("+"));
            }
            else if (expression.IndexOf('-') != -1)
            {
                type = 2;
                obj1 = double.Parse(getLeft("-"));
                obj2 = double.Parse(getRight("-"));
            }
            else if (expression.IndexOf('*') != -1)
            {
                type = 3;
                
                obj1 = double.Parse(getLeft("*"));
                obj2 = double.Parse(getRight("*"));
            }
            else if (expression.IndexOf('/') != -1)
            {
                type = 4;
                obj1 = double.Parse(getLeft("/"));
                obj2 = double.Parse(getRight( "/"));
            }
            else if (expression.IndexOf("sqare") != -1)
            {
                type = 5;
                obj1 = double.Parse(getMid("sqare(", ")"));
            }
            else if (expression.IndexOf("sqrt") != -1)
            {
                type = 6;
                obj1 = double.Parse(getMid("sqrt(", ")"));
            }
            else if (expression.IndexOf('^') != -1)
            {
                type = 7;
                obj1 = double.Parse(getLeft("^"));
                obj2 = double.Parse(getRight("^"));
            }
            else if (expression.IndexOf("root") != -1)
            {
                //root(obj1, obj2)开obj2的
                type = 8;
                obj1 = double.Parse(getMid("root(", ","));
                obj2 = double.Parse(getMid(",", ")"));
            }
            else if (expression.IndexOf("sin") != -1)
            {
                type = 9;
                obj1 = double.Parse(getMid("sin(", ")"));
            }
            else if (expression.IndexOf("cos") != -1)
            {
                type = 10;
                obj1 = double.Parse(getMid("cos(", ")"));
            }
            else if (expression.IndexOf("tan") != -1)
            {
                type = 11;
                obj1 = double.Parse(getMid("tan(", ")"));
            }
            else if (expression.IndexOf("csc") != -1)
            {
                type = 12;
                obj1 = double.Parse(getMid("csc(", ")"));
            }
            else if (expression.IndexOf("sec") != -1)
            {
                type = 13;
                obj1 = double.Parse(getMid("sec(", ")"));
            }
            else if (expression.IndexOf("cot") != -1)
            {
                type = 14;
                obj1 = double.Parse(getMid("cot(", ")"));
            }
            else if (expression.IndexOf("arcsin") != -1)
            {
                type = 15;
                obj1 = double.Parse(getMid("arcsin(", ")"));
            }
            else if (expression.IndexOf("arccos") != -1)
            {
                type = 16;
                obj1 = double.Parse(getMid("arccos(", ")"));
            }
            else if (expression.IndexOf("arctan") != -1)
            {
                type = 17;
                obj1 = double.Parse(getMid("arctan(", ")"));
            }
            else if (expression.IndexOf("arccsc") != -1)
            {
                type = 18;
                obj1 = double.Parse(getMid("arccsc(", ")"));
            }
            else if (expression.IndexOf("arcsec") != -1)
            {
                type = 19;
                obj1 = double.Parse(getMid("arcsec(", ")"));
            }
            else if (expression.IndexOf("arccot") != -1)
            {
                type = 20;
                obj1 = double.Parse(getMid("arccot(", ")"));
            }
            else if (expression.IndexOf("toarc") != -1)
            {
                type = 21;
                obj1 = double.Parse(getMid("toarc(", ")"));
            }
            else if (expression.IndexOf("todegree") != -1)
            {
                type = 22;
                obj1 = double.Parse(getMid("todegree(", ")"));
            }
            else if (expression.IndexOf("ln") != -1)
            {
                type = 23;
                obj1 = double.Parse(getMid("ln(", ")"));
            }
            else if (expression.IndexOf("lg") != -1)
            {
                type = 24;
                obj1 = double.Parse(getMid("lg(", ")"));
            }
            else if (expression.IndexOf("log") != -1)
            {
                type = 25;
                obj1 = double.Parse(getMid("log(", ","));
                obj2 = double.Parse(getMid(",", ")"));
            }

            calc();
        }
        public CalcUnit(double obj1, double obj2, int type)
        {
            this.obj1 = obj1;
            this.obj2 = obj2;
            this.type = type;
        }
        public CalcUnit(IntPtr ads1, double obj2, int type)
        {
            this.ads1 = ads1;
            this.obj2 = obj2;
            this.type = type;
        }
        public CalcUnit(IntPtr ads1, IntPtr obj2, int type)
        {
            this.ads1 = ads1;
            this.ads2 = obj2;
            this.type = type;
        }
        public CalcUnit(double ads1, IntPtr obj2, int type)
        {
            this.obj1 = ads1;
            this.ads2 = obj2;
            this.type = type;
        }*/
        public CalcUnit(double obj1, double obj2, IntPtr ads1, IntPtr ads2, int type, bool isads1, bool isads2)
        {
            this.obj1 = obj1;
            this.obj2 = obj2;
            this.ads1 = ads1;
            this.ads2 = ads2;
            this.type = type;
            isAds1 = isads1;
            isAds2 = isads2;
        }

        public void calc()
        {
            /*
1:+
2:-
3:*
4:/
5:sqare
6:sqrt
7:^
8:√
9:sin
10:cos
11:tan
12:csc
13:sec
14:cot
15:arcsin
16:arccos
17:arctan
18:arccsc
19:arcsec
20:arccot
21:ln
22:lg
23:log
            */

            if (isAds1)
            {
                obj1 = (double)Marshal.PtrToStructure(ads1, typeof(double));
            }
            if (isAds2)
            {
                obj2 = (double)Marshal.PtrToStructure(ads2, typeof(double));
            }

            switch (type)
            {
                case 1:
                    ans = obj1 + obj2;
                    break;
                case 2:
                    ans = obj1 - obj2;
                    break;
                case 3:
                    ans = obj1 * obj2;
                    break;
                case 4:
                    ans = obj1 / obj2;
                    break;
                case 5:
                    ans = obj1 * obj1;
                    break;
                case 6:
                    ans = Math.Sqrt(obj1);
                    break;
                case 7:
                    ans = Math.Pow(obj1, obj2);
                    break;
                case 8:
                    ans = Math.Pow(obj1, 1 / obj2);
                    break;
                case 9:
                    ans = Math.Sin(obj1);
                    break;
                case 10:
                    ans = Math.Cos(obj1);
                    break;
                case 11:
                    ans = Math.Tan(obj1);
                    break;
                case 12:
                    ans = 1 / Math.Sin(obj1);
                    break;
                case 13:
                    ans = 1 / Math.Cos(obj1);
                    break;
                case 14:
                    ans = 1 / Math.Tan(obj1);
                    break;
                case 15:
                    ans = Math.Asin(obj1);
                    break;
                case 16:
                    ans = Math.Acos(obj1);
                    break;
                case 17:
                    ans = Math.Atan(obj1);
                    break;
                case 18:
                    ans = Math.Asin(1 / obj1);
                    break;
                case 19:
                    ans = Math.Acos(1 / obj1);
                    break;
                case 20:
                    ans = Math.Atan(1 / obj1);
                    break;
                case 21:
                    ans = obj1 * Math.PI / 180;
                    break;
                case 22:
                    ans = obj1 * 180 / Math.PI;
                    break;
                case 23:
                    ans = Math.Log(obj1);
                    break;
                case 24:
                    ans = Math.Log10(obj1);
                    break;
                case 25:
                    //注意 第一个参数是真数 第二个参数是底数
                    ans = Math.Log(obj2, obj1);
                    break;
            }
        }
        /*string getLeft(string key)
        {
            return expression.Substring(0, expression.IndexOf(key));
        }
        string getRight(string key)
        {
            return expression.Substring(expression.IndexOf(key) + 1);
        }
        string getMid(string left, string right)
        {
            int lpos = expression.IndexOf(left) + 1;
            int rpos = expression.IndexOf(right);
            return expression.Substring(lpos + left.Length - 1, rpos - lpos - left.Length + 1);
        }*/
    }
}
