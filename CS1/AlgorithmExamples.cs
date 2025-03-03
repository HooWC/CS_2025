using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS1
{
    public class AlgorithmExamples
    {
        // 面试题1: 两数之和 - 给定一个数组和一个目标值，找出数组中和为目标值的两个数的索引
        public static int[] TwoSum(int[] nums, int target)
        {
            // 使用字典存储数字和索引
            Dictionary<int, int> numDict = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];

                if (numDict.ContainsKey(complement))
                {
                    return new int[] { numDict[complement], i };
                }

                if (!numDict.ContainsKey(nums[i]))
                {
                    numDict.Add(nums[i], i);
                }
            }

            return new int[0]; // 没找到解决方案
        }

        // 面试题2: 回文字符串检查
        public static bool IsPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            // 预处理：只保留字母和数字，并转换为小写
            string processed = new string(s.Where(c => char.IsLetterOrDigit(c)).ToArray()).ToLower();

            // 使用双指针法
            int left = 0;
            int right = processed.Length - 1;

            while (left < right)
            {
                if (processed[left] != processed[right])
                    return false;

                left++;
                right--;
            }

            return true;
        }

        // 面试题3: 找出数组中的最大子数组和 (Kadane算法)
        public static int MaxSubArray(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            int currentMax = nums[0];
            int globalMax = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                // 当前最大子数组和：取当前元素或者当前元素加上之前的最大子数组和
                currentMax = Math.Max(nums[i], currentMax + nums[i]);

                // 更新全局最大值
                globalMax = Math.Max(globalMax, currentMax);
            }

            return globalMax;
        }

        // 面试题4: 合并两个有序数组
        public static void MergeSortedArrays(int[] nums1, int m, int[] nums2, int n)
        {
            int i = m - 1;  // nums1的末尾位置
            int j = n - 1;  // nums2的末尾位置
            int k = m + n - 1;  // 合并后数组的末尾位置

            // 从后向前填充nums1
            while (i >= 0 && j >= 0)
            {
                if (nums1[i] > nums2[j])
                {
                    nums1[k] = nums1[i];
                    i--;
                }
                else
                {
                    nums1[k] = nums2[j];
                    j--;
                }
                k--;
            }

            // 如果nums2还有剩余元素，全部复制到nums1
            while (j >= 0)
            {
                nums1[k] = nums2[j];
                j--;
                k--;
            }
        }

        // 面试题5: 反转链表
        public static ListNode ReverseLinkedList(ListNode head)
        {
            ListNode prev = null;
            ListNode current = head;

            while (current != null)
            {
                ListNode nextTemp = current.Next;  // 保存下一个节点
                current.Next = prev;  // 反转指针
                prev = current;  // 移动prev指针
                current = nextTemp;  // 移动current指针
            }

            return prev;  // 新的头结点
        }

        // 面试题6: 使用栈实现队列
        public class MyQueue
        {
            private Stack<int> inStack;  // 用于入队操作
            private Stack<int> outStack; // 用于出队操作

            public MyQueue()
            {
                inStack = new Stack<int>();
                outStack = new Stack<int>();
            }

            // 入队操作
            public void Push(int x)
            {
                inStack.Push(x);
            }

            // 出队操作
            public int Pop()
            {
                if (outStack.Count == 0)
                {
                    // 如果outStack为空，将inStack中的所有元素倒入outStack
                    while (inStack.Count > 0)
                    {
                        outStack.Push(inStack.Pop());
                    }
                }

                return outStack.Pop();
            }

            // 查看队首元素
            public int Peek()
            {
                if (outStack.Count == 0)
                {
                    // 如果outStack为空，将inStack中的所有元素倒入outStack
                    while (inStack.Count > 0)
                    {
                        outStack.Push(inStack.Pop());
                    }
                }

                return outStack.Peek();
            }

            // 检查队列是否为空
            public bool Empty()
            {
                return inStack.Count == 0 && outStack.Count == 0;
            }
        }

        // 面试题7: 二叉树的最大深度 (递归解法)
        public static int MaxDepth(TreeNode root)
        {
            if (root == null)
                return 0;

            int leftDepth = MaxDepth(root.Left);
            int rightDepth = MaxDepth(root.Right);

            // 当前节点的最大深度 = max(左子树深度, 右子树深度) + 1
            return Math.Max(leftDepth, rightDepth) + 1;
        }

        // 面试题8: 有效的括号
        public static bool IsValidParentheses(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            Stack<char> stack = new Stack<char>();

            foreach (char c in s)
            {
                if (c == '(' || c == '[' || c == '{')
                {
                    stack.Push(c);
                }
                else
                {
                    if (stack.Count == 0)
                        return false;

                    char top = stack.Pop();

                    if (c == ')' && top != '(')
                        return false;
                    if (c == ']' && top != '[')
                        return false;
                    if (c == '}' && top != '{')
                        return false;
                }
            }

            return stack.Count == 0;
        }
    }

    // 支持类：链表节点
    public class ListNode
    {
        public int Val;
        public ListNode Next;

        public ListNode(int val = 0, ListNode next = null)
        {
            Val = val;
            Next = next;
        }
    }

    // 支持类：二叉树节点
    public class TreeNode
    {
        public int Val;
        public TreeNode Left;
        public TreeNode Right;

        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            Val = val;
            Left = left;
            Right = right;
        }
    }
}