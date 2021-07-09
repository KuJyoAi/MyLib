using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MyLibrary;

namespace Beatmaps_downloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //E:\Everythings\Games\OSU!\Songs
            string path = Path.Text;
            string[] filesName = Directory.GetFileSystemEntries(path);

            string[] codes = new string[filesName.Length - 1];
            Console.WriteLine();
            for (int i = 0; i < codes.Length; i++)
            {
                codes[i] = "https://bloodcat.com/osu/s/" + filesName[i].Substring(0, filesName[i].IndexOf(" ", 0)).Replace(path + "\\","");
                //Console.WriteLine(codes[i]);
            }
            string result = "";
            for (int i = 0; i < codes.Length; i++)
            {
                result += codes[i] + "\n";
            }
            File.WriteAllLines("D:\\1.txt", codes);
        }
        
    }
}
