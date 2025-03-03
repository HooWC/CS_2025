using System;

namespace CS1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("C# 编程示例集合");
            Console.WriteLine("==================");

            while (true)
            {
                Console.WriteLine("\n请选择要运行的示例:");
                Console.WriteLine("1. 字符串操作示例");
                Console.WriteLine("2. LINQ查询示例");
                Console.WriteLine("3. 算法示例");
                Console.WriteLine("4. 高级特性示例");
                Console.WriteLine("0. 退出");

                Console.Write("\n请输入选项 (0-4): ");
                string input = Console.ReadLine();

                if (input == "0")
                    break;

                try
                {
                    switch (input)
                    {
                        case "1":
                            RunStringExamples();
                            break;
                        case "2":
                            RunLinqExamples();
                            break;
                        case "3":
                            RunAlgorithmExamples();
                            break;
                        case "4":
                            RunAdvancedExamples();
                            break;
                        default:
                            Console.WriteLine("无效选项，请重新选择");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"运行示例时出错: {ex.Message}");
                }

                Console.WriteLine("\n按任意键继续...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void RunStringExamples()
        {
            Console.WriteLine("\n=== 字符串操作示例 ===\n");

            Console.WriteLine("1. 字符串分割示例:");
            StringOperations.SplitExamples();

            Console.WriteLine("\n2. 变位词检查:");
            string s1 = "listen";
            string s2 = "silent";
            Console.WriteLine($"'{s1}' 和 '{s2}' 是变位词? {StringOperations.AreAnagrams(s1, s2)}");

            Console.WriteLine("\n3. 字符串优化拼接:");
            string[] words = { "Hello", "C#", "World" };
            string result = StringOperations.ConcatStringsOptimized(words, 3);
            Console.WriteLine($"结果: {result}");

            Console.WriteLine("\n4. 邮箱验证:");
            string email1 = "user@example.com";
            string email2 = "invalid-email";
            Console.WriteLine($"'{email1}' 是有效邮箱? {StringOperations.IsValidEmail(email1)}");
            Console.WriteLine($"'{email2}' 是有效邮箱? {StringOperations.IsValidEmail(email2)}");

            Console.WriteLine("\n5. 第一个不重复字符:");
            string text = "leetcode";
            int index = StringOperations.FirstUniqueCharIndex(text);
            Console.WriteLine($"在 '{text}' 中第一个不重复字符的索引: {index}");
        }

        static void RunLinqExamples()
        {
            Console.WriteLine("\n=== LINQ示例 ===\n");

            Console.WriteLine("1. 基本LINQ查询:");
            LinqExamples.BasicQueries();

            Console.WriteLine("\n2. 排序和分组:");
            LinqExamples.SortingAndGrouping();

            Console.WriteLine("\n3. 数据转换:");
            LinqExamples.TransformationExamples();

            Console.WriteLine("\n4. 连接操作:");
            LinqExamples.JoinOperations();

            Console.WriteLine("\n5. 高级LINQ操作:");
            LinqExamples.AdvancedLinqOperations();
        }

        static void RunAlgorithmExamples()
        {
            Console.WriteLine("\n=== 算法示例 ===\n");

            Console.WriteLine("1. 两数之和:");
            int[] nums = { 2, 7, 11, 15 };
            int target = 9;
            int[] result = AlgorithmExamples.TwoSum(nums, target);

            if (result.Length == 2)
                Console.WriteLine($"结果索引: [{result[0]}, {result[1]}], 值: [{nums[result[0]]}, {nums[result[1]]}]");
            else
                Console.WriteLine("未找到解");

            Console.WriteLine("\n2. 回文检查:");
            string palindrome1 = "A man, a plan, a canal: Panama";
            string palindrome2 = "race a car";
            Console.WriteLine($"'{palindrome1}' 是回文? {AlgorithmExamples.IsPalindrome(palindrome1)}");
            Console.WriteLine($"'{palindrome2}' 是回文? {AlgorithmExamples.IsPalindrome(palindrome2)}");

            Console.WriteLine("\n3. 最大子数组和:");
            int[] array = { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
            int maxSum = AlgorithmExamples.MaxSubArray(array);
            Console.WriteLine($"最大子数组和: {maxSum}");

            Console.WriteLine("\n4. 有效括号:");
            string brackets1 = "()[]{}";
            string brackets2 = "([)]";
            Console.WriteLine($"'{brackets1}' 有效? {AlgorithmExamples.IsValidParentheses(brackets1)}");
            Console.WriteLine($"'{brackets2}' 有效? {AlgorithmExamples.IsValidParentheses(brackets2)}");
        }

        static void RunAdvancedExamples()
        {
            Console.WriteLine("\n=== 高级特性示例 ===\n");

            Console.WriteLine("1. 扩展方法示例:");
            string text = "这是一个很长的字符串，我们要对它进行截断处理";
            string truncated = text.Truncate(10);
            Console.WriteLine($"原始字符串: {text}");
            Console.WriteLine($"截断后: {truncated}");

            string numberText = "123";
            int number = numberText.ToInt();
            string invalidNumber = "abc".ToInt(-1).ToString();
            Console.WriteLine($"转换字符串到数字: '{numberText}' -> {number}");
            Console.WriteLine($"无效数字使用默认值: 'abc' -> {invalidNumber}");

            Console.WriteLine("\n2. 事件示例:");
            EventExample.DemonstrateEvents();
        }
    }
}