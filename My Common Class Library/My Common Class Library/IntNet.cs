using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace MyLibrary
{
    public class IntNet
    {
        public static string getNetSource(string address)
        {
            WebClient wc = new WebClient();
            Stream s = wc.OpenRead(address);
            StreamReader sr = new StreamReader(s);
            return sr.ReadToEnd();
        }
    }
}
