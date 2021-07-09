using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hex
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine();
        }
        static string[] ReadHex(string Path)
        {
            byte[] fileInfo = File.ReadAllBytes(Path);
            string[] Hex = new string[fileInfo.Length];

            for (int x = 0; x < fileInfo.Length; x++)
            {
                //转16进制并大写
                Hex[x] = Convert.ToString(fileInfo[x], 16).ToUpper();
                //补0
                if (Hex[x].Length == 1)
                {
                    Hex[x] = "0" + Hex[x];
                }
            }

            return Hex;
        }
    }
}
