using System.Threading;

namespace Minesweeper_Engine_outset
{
    class Run
    {
        static void Main(string[] args)
        {
            ParameterizedThreadStart PTS = new ParameterizedThreadStart(Start);
            Thread t = new Thread(PTS);
            t.Start(0);
        }
        static void Start(object obj)
        {
            Sweeper sp = new Sweeper();
            
            sp.Run();
        }
    }
}
