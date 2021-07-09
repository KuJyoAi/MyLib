using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Numerics;
using System.Collections;
using System.Threading;
using Term;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;

namespace Test_Console
{
    
    class Program
    {
        static void Main()
        {
            //\d\d\d\d-\d\d-\d\d 日期
            //\d.\d\d\d\d 单位净值+累计净值
            //-?\d.\d\d%

            string url = "http://fund.eastmoney.com/f10/F10DataApi.aspx?type=lsjz&code=003095&page=1&sdate=2020-01-06&edate=2021-07-05&per=20";
            /*string content = File.ReadAllText(@"D:\1.txt");
            MatchCollection mc = Regex.Matches(content, @"pages:\d*");

            foreach (Match m in mc)
            {
                Console.WriteLine(m.Value);
            }
            */
            //Match m = Regex.Match(content, @"\d\d\d\d-\d\d-\d\d");
            //Console.WriteLine(m.Value);

            FundData fd = new FundData("003096", "2021-01-06", "2021-07-05");
            
            fd.print();
        }
        static string read(string url)
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
    class FundData
    {
        string handle = "000001";//股票代码

        public List<DateTime> dt = new List<DateTime>();
        public List<double> value = new List<double>();
        public List<double> valueA = new List<double>();
        public List<double> rate = new List<double>();

        public FundData(string handle,string StartDate,string EndDate)
        {
            int curpage = 1;
            string url = "http://fund.eastmoney.com/f10/F10DataApi.aspx?type=lsjz&code=" + handle +
                "&page=" + curpage +
                "&sdate=" + StartDate +
                "&edate=" + EndDate +
                "&per=20";

            while (read(url,curpage.ToString()))
            {
                //返回true,即未到最后一页,下一页
                url = url.Replace("&page=" + curpage, "&page=" + ++curpage);//不能写成curpage++,这里是个经典顺序问题
                //Console.WriteLine(curpage);
            }
        }
        bool read(string url,string curpage)
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
            
        public void print()
        {
            //dt.Reverse();
            //value.Reverse();
            Console.WriteLine("日期\t星期\t单值\t累值\t涨跌");
            for (int i = 0; i < dt.Count; i++)
            {

                //Console.WriteLine("{0}月{1}日" ,dt[i].Month, dt[i].Day);
                //Console.WriteLine(value[i]);
                Console.WriteLine("{0}-{1}-{2} {3}\t{4}\t{5}\t{6}",
                                    dt[i].Year, dt[i].Month, dt[i].Day, dt[i].DayOfWeek.ToString().Substring(0, 3), value[i].ToString(), valueA[i].ToString(), rate[i].ToString());
            }
        }
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
