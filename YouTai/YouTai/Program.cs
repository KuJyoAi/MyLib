using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTai
{
    class Program
    {
        static void QuestionDate()
        {
            int startYear = 1;
            int EndYear = 9999;
            //求时间间隔
            //Console.WriteLine((new DateTime(2050, 1, 1) - new DateTime(1, 1, 1)).Days);
            int tick = (new DateTime(EndYear, 1, 1) - new DateTime(startYear, 1, 1)).Days;
            //11个数字
            int[] Count = new int[11];
            int Temp = 0;
            DateTime Dt = new DateTime(startYear, 1, 1);
            //计算
            for (int i = 0; i < tick; i++)
            {
                Temp = Caculate(Dt);
                if (Temp <= 9)
                {
                    Count[Temp - 1]++;
                }
                else if (Temp == 11)
                {
                    Count[9]++;
                }
                else if (Temp == 22)
                {
                    Count[10]++;
                }
                Dt = Dt.AddDays(1);
            }
            for (int i = 0; i < Count.Length; i++)
            {
                //Console.WriteLine("{0}:{1}", i + 1, Count[i]);
                Console.WriteLine(Count[i]);
            }
            Console.WriteLine("From " + startYear + ",1,1 to {0},{1},{2}", Dt.Year, Dt.Month, Dt.Day);
        }
        static int Caculate(DateTime Info)
        {
            char[] Year = Info.Year.ToString().ToCharArray();//年
            char[] Month = Info.Month.ToString().ToCharArray();//月
            char[] Day = Info.Day.ToString().ToCharArray();//日

            int result = 0;
            //遍历相加
            for (int i = 0; i < Year.Length; i++)
            {
                result += int.Parse(Year[i].ToString());
            }
            for (int i = 0; i < Month.Length; i++)
            {
                result += int.Parse(Month[i].ToString());
            }
            for (int i = 0; i < Day.Length; i++)
            {
                result += int.Parse(Day[i].ToString());
            }
            while (true)
            {
                if (result <= 9 || result == 11 || result == 22)
                {
                    return result;
                }
                //如果不符合条件 继续相加
                char[] Temp = result.ToString().ToCharArray();
                result = 0;
                for (int i = 0; i < Temp.Length; i++)
                {
                    result += int.Parse(Temp[i].ToString());
                }
            }

        }
        static void Main()
        {
            QuestionDate();
        }
    }
}
