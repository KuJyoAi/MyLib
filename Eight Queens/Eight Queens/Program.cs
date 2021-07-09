using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eight_Queens
{
    class Program
    {
        static void Main(string[] args)
        {
            Game[] g = new Game[100];
            for (int i = 0; i < 100; i++)
            {
                g[i] = new Game(8);
                g[i].Caculate();  
            }

        }
    }
}
