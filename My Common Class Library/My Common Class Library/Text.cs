using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    /// <summary>
    /// 文本操作
    /// </summary>
    public class Text
    {
        /// <summary>
        /// 取左边文本
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="key">标识符</param>
        /// <returns>返回标识符左边的字符</returns>
        public static string GetLeft(string sourceText, string key)
        {
            /*
            如果异常,则肯定是
            例外 条件ArgumentOutOfRangeException startIndex 
            加上 length 指示的位置不在此实例。
            -或 - startIndex 或 length 小于零。
            所以没有找到,所以返回""
            */
            try
            {
                return sourceText = sourceText.Substring(0, sourceText.IndexOf(key));
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 取左边文本,可设置起始搜寻位置
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="key">标识符</param>
        /// <param name="pos">起始搜寻位置</param>
        /// <returns>返回标识符左边的字符</returns>
        public static string GetLeft(string sourceText, string key, int pos)
        {
            try
            {
                return sourceText = sourceText.Substring(0, sourceText.IndexOf(key, pos));
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 取右边文本
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="key">标识符</param>
        /// <returns>返回标识符右边的文本</returns>
        public static string GetRight(string sourceText, string key)
        {
            //从某个字符起到尾

            int point = sourceText.IndexOf(key);
            if (point > 0)
            {
                return sourceText.Substring(point + key.Length);
            }
            return "";

        }
        /// <summary>
        /// 取右边文本,可设置起始搜寻位置
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="key">标识符</param>
        /// <param name="pos">起始搜寻位置</param>
        /// <returns>返回标识符右边的文本</returns>
        public static string GetRight(string sourceText, string key, int pos)
        {
            int point = sourceText.IndexOf(key, pos);
            if (point > 0)
            {
                return sourceText.Substring(point + key.Length);
            }
            return "";
        }

        /// <summary>
        /// 取中间文本,此方法默认取出左标识符后面的第一个右标识符匹配的文本
        /// 例如:/**/Left/**/Middle/**/Right/**/ 如果Left是第一个 则Right为第二个 结果为Left
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="LeftKey">左标识符</param>
        /// <param name="RightKey">右标识符</param>
        /// <returns>左标识符与右标识符之间的文本</returns>
        public static string GetMiddle(string sourceText, string Left, string Right)
        {
            //例子
            //文本:1 2 3 4 5 6 L:12 R:56
            //数字: 1 2 3 4 5 6
            //pointLeft:2 pointRight = 5

            int pointLeft = sourceText.IndexOf(Left) + Left.Length - 1;

            //pointLeft + 1:如果左边标识符=右边标识符,会产生错误
            int pointRight = sourceText.IndexOf(Right, pointLeft + 1);

            //Console.WriteLine("Left:" + pointLeft + "\tRight:" + pointRight);

            if (pointLeft > 0 && pointRight > 0)
            {
                //右边标识符 - 左边标识符 - 1 = 文本长度
                return sourceText.Substring(pointLeft + 1, pointRight - pointLeft - 1);
            }
            return "";
        }
        /// <summary>
        /// 取中间文本,可设置起始搜寻位置
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="Left">左标识符</param>
        /// <param name="Right">右标识符</param>
        /// <param name="pos">起始搜寻位置</param>
        /// <returns>左标识符与右标识符之间的文本</returns>
        public static string GetMiddle(string sourceText, string Left, string Right, int pos)
        {
            int pointLeft = sourceText.IndexOf(Left, pos) + Left.Length - 1;
            //pointLeft + 1:如果左边标识符=右边标识符,则无法取出
            
            int pointRight = sourceText.IndexOf(Right, pointLeft + 1);
            //Console.WriteLine("Left:" + pointLeft + "\tRight:" + pointRight);

            if (pointLeft > 0 && pointRight > 0)
            {
                //右边标识符 - 左边标识符 - 1 = 文本长度
                return sourceText.Substring(pointLeft + 1, pointRight - pointLeft - 1);
            }
            return "";
        }
        /// <summary>
        /// 批量取中间文本,但速度慢,不建议使用
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="Left">左边文本</param>
        /// <param name="Right">右边文本</param>
        /// <returns></returns>
        public static string[] BatchGetMiddle(string sourceText, string Left, string Right)
        {
            List<string> list = new List<string>();
            int pos = 0;
            //循环取中间文本
            for (int i = 0; pos != -1; i++)
            {
                list.Add(GetMiddle(sourceText, Left, Right, pos));
                pos = sourceText.IndexOf(Right + Right.Length + 1);
            }
            Console.WriteLine(list.Count);
            //删除末尾的空文本
            list.RemoveAt(list.Count - 1);

            /*//转string
            string[] result = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                result[i] = (list[i]);
            }*/
            return list.ToArray();
        }
        /// <summary>
        /// 取一段文本在源文本的第几行 返回值减-1可得前面出现的换行符数量
        /// </summary>
        /// <param name="Source">源文本</param>
        /// <param name="Key">要找的文本</param>
        /// <returns>Key的行数</returns>
        public static int GetLine(string Source, string Key)
        {
            Source = Source.Substring(0, Source.IndexOf(Key) + 1);//取出前面的文本
            //换行符出现数量
            int count = 0;
            //下标
            int pos = 0;
            while (pos != -1)
            {
                //下标
                pos = Source.IndexOf("\r\n", pos + 1);
                if (pos != -1)
                {
                    //找到一次 +1
                    count++;
                }
            }
            //本身行数 = 前面换行符数量 + 1
            return count + 1;
        }
    }
}