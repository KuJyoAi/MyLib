using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper_Engine_outset
{
    class Board
    {
        public block[,] game;//游戏版
        public Pixel p;//交互者
        public IntPtr hwnd;//窗口句柄
        public int width;
        public int heigh;
        public Board()
        {
            hwnd = ScannerHwnd.GetTarget(out width, out heigh);
            game = new block[width, heigh];
            p = new Pixel(hwnd, width, heigh);
            for (int y = 0; y < heigh; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    game[x, y] = new block(p.getValue(x, y), x, y);
                }
            }
        }
    }
}
