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
            FundData fd = new FundData("320007", "2010-04-01", "2020-05-26");
            //fd.print();
            //Console.WriteLine();
            //StrategyH sh = new StrategyH(fd, 7, 0);
            //fd.Save(@"D:\1.txt");
            //FundData d = new FundData(@"D:\1.txt");
            //d.printS();

            fd.printS();
            Strategy s = new Strategy(fd);
            s.RunFix(4, 10);

        }
    }
    class Strategy
    {
        public FundData data;
        public double cost = 0;//当前成本单价
        public double input = 0;//当前投入
        //定期非定额投法计算
        public Strategy(FundData fd)
        {
            data = fd;
        }
        //投入策略,暂时固定
        double NextInput()
        {
            return 100000;
        }
        //变额投入
        public void Run(int week, double bas)
        {
            
        }
        //定额投入
        public void RunFix(int week, double money)
        {
            List<int> pos = new List<int>();
            GetWeek(week, pos);

            //foreach (var i in pos)
            //{
            //    Console.WriteLine(i);
            //}

            //单次净值 单次投入
            double val = 0; 
            double ip;
            for (int i = 0; i < pos.Count; i++)
            {
                ip = money;
                val = data.value[pos[i]];

                cost = (cost * input + val * ip) / (input + ip);
                input += ip;
            }
            val = data.value[data.value.Count - 1];
            Console.WriteLine("本金:{0} 本+利:{1} 盈利率:{2}% 成本|卖出:{3}|{4}", 
                input, input * val / cost, 
                (val / cost - 1) * 100,
                cost, val);
        }
        //取一周某一天的位置集合
        void GetWeek(int target, List<int> res)
        {

            for (int i = 0; i < data.dt.Count; i++)
            {
                if ((int)data.dt[i].DayOfWeek == target)
                {
                    res.Add(i);
                }
            }
        }
    }
    class StrategyH
    {
        FundData data;
        List<int> pos = new List<int>();
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
        public StrategyH(FundData fd,int period,int StartIndex)
        {
            this.period = period;
            data = fd;

            //截取定投所需的位置
            //遇到缺口就失效,不可用
            /*DateTime date = fd.dt[StartIndex];
            for (int i = StartIndex; i < fd.dt.Count;i++)
            {
                //Console.WriteLine(date);
                if (fd.dt[i] == date)
                {
                    pos.Add(i);
                    date = date.AddDays(period);
                    Console.WriteLine(i);
                }
                
            }*/
            List<DateTime> dates = new List<DateTime>();
            DateTime date = fd.dt[StartIndex];
            while (fd.dt.Last() >= date)
            {
                dates.Add(date);
                date.AddDays(7);
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
        string handle = "";//股票代码

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
            this.handle = handle;
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

            dt.Reverse();
            value.Reverse();
            valueA.Reverse();
            rate.Reverse();
        }
        public FundData(string path)
        {
            string content = File.ReadAllText(path);
            //读取日期
            string[] tmp = content.Split('/');
            //最后一个为空
            for (int i = 0; i < tmp.Length - 1; i+= 4)
            {
                dt.Add(new DateTime(int.Parse(tmp[i]), int.Parse(tmp[i + 1]), int.Parse(tmp[i + 2])));
                value.Add(double.Parse(tmp[i + 3]));
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
        public void printS()
        {
            Console.WriteLine("日期\t星期\t单值");
            for (int i = 0; i < dt.Count; i++)
            {
                Console.WriteLine("{0}-{1}-{2} {3}\t{4}\t :{5}",
                                    dt[i].Year, dt[i].Month, dt[i].Day, 
                                    dt[i].DayOfWeek.ToString().Substring(0, 3), 
                                    value[i].ToString(),
                                    i);
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
        public void Save(string path)
        {
            //仅存储日期 单位净值 格式:xxxx/xx/xx 净值
            StringBuilder content = new StringBuilder();
            for (int i = 0; i < dt.Count; i++)
            {
                content.Append(dt[i].Year + "/" + dt[i].Month + "/" + dt[i].Day + "/" + value[i] + "/");
            }
            //Console.WriteLine(content.ToString());

            File.WriteAllText(path, content.ToString());
            
        }
    }
}
