
namespace Minesweeper_Engine_outset
{
    class block
    {
        const int none = 0;
        const int unkw = -1;
        const int flag = -2;
        const int mine = -3;

        public int value = 0;
        public bool IsCleared = false;//是否已清除(为了提升效率)
        public int x;
        public int y;
        public block(int value, int x, int y)
        {
            this.value = value;
            this.x = x;
            this.y = y;
        }
    }
}
