using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EncodingMyself
{
    /*
    加密规则:假设加密数据123,
    123 / 16 = 7
    123 % 16 = 11
    得到数组:7, 11
    这种方法使取得的值范围在:0-15, 0-15
    在ASCII对应码中,A(65) a(97)
    所以65+0-15, 97+0-15
    上面的结果就变成72, 108
    保存为文本得:Hi

    解密规则:需解密数组72, 108
     (72 - 65) * 16 + (108 - 97) = 123
    
    缺陷:只有A-P a-p范围内的字母出现,并且极度占用硬盘空间(加密后2倍大小)
    */
    class Program
    {
        static void Main(string[] args)
        {
            string Path = @"D:\Texts\Document\Uncommon\Read-only\GBK.txt";
            //string Path = @"D:\1.txt";
            byte[] Backup = File.ReadAllBytes(Path);
            string[] BackupPathes = Directory.GetFiles(@"D:\Cache\Backup");
            File.WriteAllBytes(@"D:\Cache\Backup\" + (BackupPathes.Length + 1) + ".txt", Backup);
            Console.WriteLine("请输入操作:1/2");

            int a = int.Parse(Console.ReadLine());

            switch (a)
            {
                case 1: C(Path);
                    break;
                case 2: R(Path);
                    break;
                default:
                    Console.WriteLine("错误");
                    break;
            }

            Console.ReadKey();
        }
        static void C(string Path)
        {
            byte[] NeedCoding = File.ReadAllBytes(Path);
            byte[] AfterCoding = TextCoding(NeedCoding);
            byte[] AfterRecoding = Recoding(AfterCoding);

            if (System.Text.Encoding.UTF8.GetString(NeedCoding) == System.Text.Encoding.UTF8.GetString(AfterRecoding))
            {
                Console.WriteLine("成功");
                //File.WriteAllText(Path, System.Text.Encoding.UTF8.GetString(AfterCoding));
                File.WriteAllBytes(Path, AfterCoding);
            }
            else if (System.Text.Encoding.UTF8.GetString(NeedCoding) != System.Text.Encoding.UTF8.GetString(AfterRecoding))
            {
                Console.WriteLine("失败");
            }
        }
        static void R(string Path)
        {
            byte[] NeedRecoding = File.ReadAllBytes(Path);
            byte[] AfterRecoding = Recoding(NeedRecoding);
            byte[] AfterCoding = TextCoding(AfterRecoding);

            if (System.Text.Encoding.UTF8.GetString(NeedRecoding) == System.Text.Encoding.UTF8.GetString(AfterCoding))
            {
                Console.WriteLine("成功");
                //File.WriteAllText(Path, System.Text.Encoding.UTF8.GetString(AfterRecoding));
                File.WriteAllBytes(Path, AfterRecoding);
            }
            else if (System.Text.Encoding.UTF8.GetString(NeedRecoding) != System.Text.Encoding.UTF8.GetString(AfterRecoding))
            {
                Console.WriteLine("失败");
            }
        }
        /// <summary>
        /// 加密文本
        /// </summary>
        /// <param name="B">加密前文本</param>
        /// <returns>加密后文本</returns>
        static byte[] TextCoding(byte[] B)
        {
            byte[] Result = new byte[B.Length * 2];
            //Console.WriteLine(Result.Length);
            /*
            1:R[0] = B[0] / 16;
              R[1] = B[0] % 16;
            2:R[2] = B[1] / 16;
              R[3] = B[1] % 16;
            */
            for (int i = 0; i < Result.Length; i += 2)
            {
                Result[i] = (byte)(B[(i + 1) / 2] / 16 + 65);
                Result[i + 1] = (byte)(B[(i + 1) / 2] % 16 + 97);
            }
            return Result;
        }
        /// <summary>
        /// 解密文本
        /// </summary>
        /// <param name="B">解密前文本</param>
        /// <returns>解密后文本</returns>
        static byte[] Recoding(byte[] B)
        {
            byte[] Result = new byte[B.Length / 2];
            //Console.WriteLine(Result.Length);
            /*
            1:R[0] = B[0] * 16 + B[1]
            2:R[1] = B[2] * 16 + B[3]

            */
            for (int i = 0; i < Result.Length; i++)
            {
                Result[i] = (byte)((B[i * 2] - 65) * 16 + (B[i * 2 + 1] - 97));
            }
            return Result;
        }

    }
}
