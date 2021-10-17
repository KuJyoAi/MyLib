using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Circle_Calc
{
    public partial class Main : Form
    {
        bool mode = true;//消y模式
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)listBox1.SelectedItem == "消x")
            {
                label4.Text = "形式:x=my+n";
                label5.Text = "m";
                label6.Text = "n";
            }
            else
            {
                label4.Text = "形式:y=kx+b";
                label5.Text = "k";
                label6.Text = "b";
            }
        }
    }
}
