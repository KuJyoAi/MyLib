using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Decimal_Conversion
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string TenToTwo(int Decimal)
        {
            /*用2去除十进制整数，可以得到一个商和余数；再用2去除商，
            又会得到一个商和余数，如此进行，直到商为0时为止，
            然后把先得到的余数作为二进制数的低位有效位，
            后得到的余数作为二进制数的高位有效位，
            依次排列起来。也叫“倒序取余”*/

            //算出位数
            int[] Bin = new int[64];
            int pos = Bin.Length - 1;
            while (Decimal!=0)
            {
                Bin[pos] = Decimal % 2;
                Decimal /= 2;
                pos--;
            }
            string result ="";

            //去0的函数,找到有效数位索引
            for (int x = pos;x < Bin.Length; x++)
            {
                if (Bin[x] != 0)
                {
                    pos = x;
                    break;
                }
            }
            //倒序
            for (; pos < Bin.Length; pos++)
            {
                //写给以后忘记的我:result += Bin[pos]相当于result = result + Bin[pos] 
                    result += Bin[pos];
            }
            return (result);
        }
        private string TenToEX(int Decimal,int WeiShu)
        {
            /*用2去除十进制整数，可以得到一个商和余数；再用2去除商，
            又会得到一个商和余数，如此进行，直到商为0时为止，
            然后把先得到的余数作为二进制数的低位有效位，
            后得到的余数作为二进制数的高位有效位，
            依次排列起来。也叫“倒序取余”*/
            //此方法集合二进制+八进制+十六进制

            //算出位数
            int[] Keys = new int[64];
            int pos = Keys.Length - 1;
            while (Decimal != 0)
            {
                Keys[pos] = Decimal % WeiShu;
                Decimal /= WeiShu;
                pos--;
            }
            

            //去0的函数,找到有效数位索引(pos)
            for (int x = pos; x < Keys.Length; x++)
            {
                if (Keys[x] != 0)
                {
                    pos = x;
                    break;
                }
            }
            
            if (WeiShu < 10)
            {
                //倒序相加直接返回
                return Add(pos,Keys);
            }
            else if (WeiShu == 16)
            {
                //如果是16进制则编码
                char[] Temp = IntToString(Keys);
                //倒序相加
                return Add(pos,Temp);

            }
            return "";          
        }
        //倒序相加的函数
        private string Add(int pos, char[] Keys)
        {
            string result = "";
            for (; pos < Keys.Length; pos++)
            {
                result += Keys[pos];
            }
            return result;
        }

        private string Add(int pos, int[] Keys)
        {
            string result = "";
            for (; pos < Keys.Length; pos++)
            {
                result += Keys[pos];
            }
            return result;
        }
        //给十六进制编码
        private char[] IntToString(int[] Args)
        {
            //利用ASCII编码表,0-9为48-57,A-F为65-70
            char[] Temp = new char[64];
            //编码
            for (int i=0;i<Args.Length;i++)
            {
                if (Args[i] < 10)
                {
                    Temp[i] = (char)(Args[i] + 48);
                }
                else if (Args[i] > 10)
                {
                    Temp[i] = (char)(Args[i] + 55);
                }
            }
            return Temp;           
        }

        private void Start_Click(object sender, EventArgs e)
        {
            //BinBox.Text = TenToTwo(int.Parse (DecimalBox.Text));
            BinBox.Text = TenToEX(int.Parse(DecimalBox.Text),2);
            OctalBox.Text = TenToEX(int.Parse(DecimalBox.Text), 8);
            HexBox.Text = TenToEX(int.Parse(DecimalBox.Text), 16);

        }
    }
}
