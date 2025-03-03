using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CS1
{
    public class StringOperations
    {
        // 字符串分割的高级用法
        public static void SplitExamples()
        {
            // 基本Split用法
            string csvLine = "苹果,香蕉,橙子,葡萄";
            string[] fruits = csvLine.Split(',');
            Console.WriteLine($"分割后的水果数量: {fruits.Length}");

            // 使用多个分隔符
            string data = "苹果,香蕉;橙子|葡萄";
            string[] mixedFruits = data.Split(new char[] { ',', ';', '|' });
            Console.WriteLine($"使用多分隔符: {string.Join(" & ", mixedFruits)}");

            // 限制分割次数
            string text = "a,b,c,d,e,f";
            string[] limitedParts = text.Split(',', 3); // 分割3次，得到3+1个元素
            Console.WriteLine($"限制分割: {string.Join(" - ", limitedParts)}"); // 输出: a - b - c,d,e,f

            // 移除空条目
            string spaceText = "一,,二, ,三";
            string[] noEmptyParts = spaceText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine($"移除空条目: {string.Join("/", noEmptyParts)}");
        }

        // 字符串处理面试题: 判断两个字符串是否为变位词(字母相同但顺序不同)
        public static bool AreAnagrams(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2) || s1.Length != s2.Length)
                return false;

            // 方法1: 排序比较
            char[] chars1 = s1.ToLower().ToCharArray();
            char[] chars2 = s2.ToLower().ToCharArray();

            Array.Sort(chars1);
            Array.Sort(chars2);

            return new string(chars1) == new string(chars2);

            // 方法2 (另一种实现):
            // return s1.ToLower().OrderBy(c => c).SequenceEqual(s2.ToLower().OrderBy(c => c));
        }

        // 使用StringBuilder优化字符串拼接
        public static string ConcatStringsOptimized(string[] words, int repeatTimes)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < repeatTimes; i++)
            {
                foreach (string word in words)
                {
                    sb.Append(word);
                }

                if (i < repeatTimes - 1)
                    sb.Append(" | ");
            }

            return sb.ToString();
        }

        // 使用正则表达式进行模式匹配
        public static bool IsValidEmail(string email)
        {
            // 简单的邮箱验证正则表达式
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            return Regex.IsMatch(email, pattern);
        }

        // 面试题: 找出字符串中第一个不重复的字符的索引
        public static int FirstUniqueCharIndex(string s)
        {
            // 使用字典记录每个字符出现的次数
            Dictionary<char, int> charCount = new Dictionary<char, int>();

            foreach (char c in s)
            {
                if (charCount.ContainsKey(c))
                    charCount[c]++;
                else
                    charCount[c] = 1;
            }

            // 找到第一个只出现一次的字符
            for (int i = 0; i < s.Length; i++)
            {
                if (charCount[s[i]] == 1)
                    return i;
            }

            return -1; // 没找到不重复的字符
        }
    }
}