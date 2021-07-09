using RecognitionDLL;

namespace Physical_World
{
    class obj
    {
        /*支持函数类型:
        1:x(t)
        2:v(t)
        3:a(t)
        4:a(x)
        5:a(v)
        6:v(x)
        7:v(a)*/
        public double t = 0;//时间
        public double m = 1;//质量
        public vector2 v = new vector2(0, 0);//速度
        public vector2 a = new vector2(0, 0);//加速度
        public vector2 r = new vector2(0, 0);//位矢量
        public Monomial exp_x;
        public Monomial exp_y;
        public int exp_type = 0;
        public obj(string exp_x, string exp_y, int exp_type)
        {
            this.exp_type = exp_type;
            this.exp_x = new Monomial(exp_x);
            this.exp_y = new Monomial(exp_y);
            //初始化
            Initialize(exp_type);
        }

        public void run_xt(double tick)
        {
            //System.Console.WriteLine(t);
            t += tick;
            r.x = exp_x.calc(t);
            r.y = exp_y.calc(t);
        }
        public void run_vt(double tick)
        {
            //这里取保守估计,若误差大可更改此处取平均值
            r.x += v.x * tick;
            r.y += v.y * tick;

            t += tick;
            v.x = exp_x.calc(t);
            v.y = exp_y.calc(t);
        }
        public void run_at(double tick)
        {
            //这里取保守估计,若误差大可更改此处取平均值
            r.x += v.x * tick;
            r.y += v.y * tick;

            //速度仍取保守估计
            v.x += a.x * tick;
            v.y += a.y * tick;

            t += tick;
            a.x = exp_x.calc(t);
            a.y = exp_y.calc(t);

        }
        public void run_ax(double tick)
        {
            
        }
        //初始化参量
        void Initialize(int type)
        {

        }

    }
    //二维向量
    public class vector2
    {
        public double x;
        public double y;

        public vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        //向量加法
        public static vector2 operator +(vector2 l, vector2 r)
        {
            return new vector2(l.x + r.x, l.y + r.y);
        }
        //向量减法
        public static vector2 operator -(vector2 l, vector2 r)
        {
            return new vector2(l.x - r.x, l.y - r.y);
        }
        //向量点乘(dot product)
        public static double dp(vector2 l, vector2 r)
        {
            return l.x * r.x + l.y * r.y;
        }

    }
}
