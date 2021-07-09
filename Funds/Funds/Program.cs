using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Funds
{
    class Program
    {
        static void Main(string[] args)
        {
            FundData fd = new FundData("003096", "2021-01-06", "2021-07-08");
            fd.print();
        }
    }
    class StrategyH
    {
        FundData data;
        int period = 0;
        int type = 0;
        /*策略:
         * 1:定额目标止盈
         * 2:定投移动止盈
         * 3:非定投目标止盈
         * 4:非定投移动止盈
         * 5:函数补仓函数回撤
         * 
         */
        public double rate = 0;//盈亏率
        public int count = 0;//投入次数
        public double input = 0;//已投入量
        public double output = 0;//盈亏
        public double BaseInput = 0;//基础投入量
        public double TargetRate = 0;//目标收益
        public double AvgValue = 0;//平均净值
        
        //历史性定投计算器:定投周期,定投金额,目标止盈率
        StrategyH(FundData fd,int period)
        {
            this.period = period;
            for (int i = 0; i < fd.dt.Count; i++)
            {

            }
        }
        void run(int type)
        {
            switch (type)
            {
                case 1:
                    type1();
                    break;
                default:
                    break;
            }
        }
        void type1()
        {
            while (true)
            {
                
            }
        }
        void putin(int i, double input)
        {
            count++;
            this.input += input;
            //AvgValue与period挂钩,不在此计算
        }

    }
    class FundData
    {
        string handle = "000001";//股票代码

        public List<DateTime> dt = new List<DateTime>();
        public List<double> value = new List<double>();//单位净值
        public List<double> valueA = new List<double>();//累积净值
        public List<double> rate = new List<double>();
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="handle">股票代码</param>
        /// <param name="StartDate">起始日期,用-隔开</param>
        /// <param name="EndDate">终末日期,用-隔开</param>
        public FundData(string handle, string StartDate, string EndDate)
        {
            int curpage = 1;
            string url = "http://fund.eastmoney.com/f10/F10DataApi.aspx?type=lsjz&code=" + handle +
                "&page=" + curpage +
                "&sdate=" + StartDate +
                "&edate=" + EndDate +
                "&per=20";

            while (read(url, curpage.ToString()))
            {
                //返回true,即未到最后一页,下一页
                url = url.Replace("&page=" + curpage, "&page=" + ++curpage);//不能写成curpage++,这里是个经典顺序问题
                //Console.WriteLine(curpage);
            }
        }
        //读取一页的内容并正则
        bool read(string url, string curpage)
        {
            string content = readUrl(url);
            //Console.WriteLine(Regex.Match(content, @"每份派现金\d.\d\d\d\d元").Value);
            //除去分红的正则干扰
            if (content.Contains("每份派现金"))
            {
                content = content.Replace(Regex.Match(content, @"每份派现金\d.\d\d\d\d元").Value, "");
            }

            //读取日期
            MatchCollection ms = Regex.Matches(content, @"\d\d\d\d-\d\d-\d\d");
            foreach (Match m in ms)
            {
                //Console.WriteLine(m);
                //Console.WriteLine(int.Parse(m.Value.Substring(0, 4)));
                //Console.WriteLine(m.Value.Substring(5, 2));
                //Console.WriteLine(int.Parse(m.Value.Substring(8)));
                dt.Add(new DateTime(int.Parse(m.Value.Substring(0, 4)), int.Parse(m.Value.Substring(5, 2)), int.Parse(m.Value.Substring(8))));
            }

            //读取净值
            ms = Regex.Matches(content, @"\d.\d\d\d\d");
            int p = 0;
            foreach (Match m in ms)
            {

                if (p % 2 == 0)
                {

                    value.Add(double.Parse(m.Value));
                }
                else
                {
                    //Console.WriteLine(p);
                    //Console.WriteLine(double.Parse(m.Value));
                    valueA.Add(double.Parse(m.Value));
                }
                p++;
            }

            //读取涨跌
            ms = Regex.Matches(content, @"-?\d.\d\d%");
            foreach (Match m in ms)
            {
                rate.Add(double.Parse(m.Value.Substring(0, m.Value.Length - 1)));
            }

            //检查是否为最后一页
            string cp = Regex.Match(content, @"pages:\d*").Value;
            if (cp.Substring(cp.Length - 1) == curpage)
            {
                return false;
            }
            return true;
        }
        //打印结果
        public void print()
        {
            Console.WriteLine("日期\t星期\t单值\t累值\t涨跌");
            for (int i = 0; i < dt.Count; i++)
            {
                Console.WriteLine("{0}-{1}-{2} {3}\t{4}\t{5}\t{6}",
                                    dt[i].Year, dt[i].Month, dt[i].Day, dt[i].DayOfWeek.ToString().Substring(0, 3), value[i].ToString(), valueA[i].ToString(), rate[i].ToString());
            }
        }
        //给定网址读取资源
        static string readUrl(string url)
        {
            HttpWebRequest myrq = (HttpWebRequest)WebRequest.Create(url);

            myrq.KeepAlive = false;
            myrq.Timeout = 30 * 1000; //超时时间
            myrq.Method = "Get";  //请求方式 
            myrq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            myrq.Host = "baike.baidu.com"; //来源
                                           //定义请求请求Referer 
            myrq.Referer = "https://www.baidu.com/link?url=krnoB2YHt94yzV5ewGRncTo8ayAJETxd_Yv2VXwmkO6wN9K401boggwFVgiPulgwix76akOoMOt72D6UBXb1WtxZoXFok4wW_BADpdDbcQk8U114CohHj0j-JPr0epo1&wd=&eqid=c0dedaf300022d3f000000025d4a87cd";
            //定义浏览器代理
            myrq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 UBrowser/6.2.4098.3 Safari/537.36";

            //请求网页
            HttpWebResponse myrp = (HttpWebResponse)myrq.GetResponse();

            //判断请求状态
            if (myrp.StatusCode != HttpStatusCode.OK)
            {
                return "";
            }
            using (StreamReader sr = new StreamReader(myrp.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
