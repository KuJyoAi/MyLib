using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Get_Word
{
    class Program
    {
        static void Main(string[] args)
        {
            string right = "<div id=\"webTrans\" class=\"trans-wrapper trans-tab\">";
            string left = "<h2 class=\"wordbook-js\">";
            //http://dict.youdao.com/w/eng/sign/#keyfrom=dict2.top.suggest
            string word = "sign";
            WebClient wc = new WebClient();
            Stream s = wc.OpenRead("http://dict.youdao.com/w/eng/" + word + "/#keyfrom=dict2.top.suggest");
            StreamReader sr = new StreamReader(s,Encoding.UTF8);
            string con = sr.ReadToEnd();
            //Console.WriteLine(con);
            Console.WriteLine(con.IndexOf(left));
            Console.WriteLine(con.IndexOf(right));
            con = con.Substring(con.IndexOf(left), con.IndexOf(right) - con.IndexOf(left) + right.Length);
            Console.WriteLine(con);
            
        }

    }
}
