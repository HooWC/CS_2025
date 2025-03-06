Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace VBExamples
    Public Class AlgorithmExamples
        ' 面试题1: 两数之和 - 给定一个数组和一个目标值，找出数组中和为目标值的两个数的索引
        Public Shared Function TwoSum(nums() As Integer, target As Integer) As Integer()
            ' 使用字典存储数字和索引
            Dim numDict As New Dictionary(Of Integer, Integer)()
            
            For i As Integer = 0 To nums.Length - 1
                Dim complement As Integer = target - nums(i)
                
                If numDict.ContainsKey(complement) Then
                    Return New Integer() {numDict(complement), i}
                End If
                
                If Not numDict.ContainsKey(nums(i)) Then
                    numDict.Add(nums(i), i)
                End If
            Next
            
            Return New Integer() {} ' 没找到解决方案
        End Function
        
        ' 面试题2: 回文字符串检查
        Public Shared Function IsPalindrome(s As String) As Boolean
            If String.IsNullOrEmpty(s) Then
                Return True
            End If
            
            ' 预处理：只保留字母和数字，并转换为小写
            Dim processed As String = New String(s.Where(Function(c) Char.IsLetterOrDigit(c)).ToArray()).ToLower()
            
            ' 使用双指针法
            Dim left As Integer = 0
            Dim right As Integer = processed.Length - 1
            
            While left < right
                If processed(left) <> processed(right) Then
                    Return False
                End If
                
                left += 1
                right -= 1
            End While
            
            Return True
        End Function
        
        ' 面试题3: 找出数组中的最大子数组和 (Kadane算法)
        Public Shared Function MaxSubArray(nums() As Integer) As Integer
            If nums Is Nothing OrElse nums.Length = 0 Then
                Return 0
            End If
            
            Dim currentMax As Integer = nums(0)
            Dim globalMax As Integer = nums(0)
            
            For i As Integer = 1 To nums.Length - 1
                ' 当前最大子数组和：取当前元素或者当前元素加上之前的最大子数组和
                currentMax = Math.Max(nums(i), currentMax + nums(i))
                
                ' 更新全局最大值
                globalMax = Math.Max(globalMax, currentMax)
            Next
            
            Return globalMax
        End Function
        
        ' 面试题4: 合并两个有序数组
        Public Shared Sub MergeSortedArrays(nums1() As Integer, m As Integer, nums2() As Integer, n As Integer)
            Dim i As Integer = m - 1 ' nums1的末尾位置
            Dim j As Integer = n - 1 ' nums2的末尾位置
            Dim k As Integer = m + n - 1 ' 合并后数组的末尾位置
            
            ' 从后向前填充nums1
            While i >= 0 AndAlso j >= 0
                If nums1(i) > nums2(j) Then
                    nums1(k) = nums1(i)
                    i -= 1
                Else
                    nums1(k) = nums2(j)
                    j -= 1
                End If
                k -= 1
            End While
            
            ' 如果nums2还有剩余元素，全部复制到nums1
            While j >= 0
                nums1(k) = nums2(j)
                j -= 1
                k -= 1
            End While
        End Sub
        
        ' 面试题5: 反转链表
        Public Shared Function ReverseLinkedList(head As ListNode) As ListNode
            Dim prev As ListNode = Nothing
            Dim current As ListNode = head
            
            While current IsNot Nothing
                Dim nextTemp As ListNode = current.Next ' 保存下一个节点
                current.Next = prev ' 反转指针
                prev = current ' 移动prev指针
                current = nextTemp ' 移动current指针
            End While
            
            Return prev ' 新的头结点
        End Function
        
        ' 面试题6: 使用栈实现队列
        Public Class MyQueue
            Private inStack As New Stack(Of Integer)() ' 用于入队操作
            Private outStack As New Stack(Of Integer)() ' 用于出队操作
            
            ' 入队操作
            Public Sub Push(x As Integer)
                inStack.Push(x)
            End Sub
            
            ' 出队操作
            Public Function Pop() As Integer
                If outStack.Count = 0 Then
                    ' 如果outStack为空，将inStack中的所有元素倒入outStack
                    While inStack.Count > 0
                        outStack.Push(inStack.Pop())
                    End While
                End If
                
                Return outStack.Pop()
            End Function
            
            ' 查看队首元素
            Public Function Peek() As Integer
                If outStack.Count = 0 Then
                    ' 如果outStack为空，将inStack中的所有元素倒入outStack
                    While inStack.Count > 0
                        outStack.Push(inStack.Pop())
                    End While
                End If
                
                Return outStack.Peek()
            End Function
            
            ' 检查队列是否为空
            Public Function Empty() As Boolean
                Return inStack.Count = 0 AndAlso outStack.Count = 0
            End Function
        End Class
        
        ' 面试题7: 二叉树的最大深度 (递归解法)
        Public Shared Function MaxDepth(root As TreeNode) As Integer
            If root Is Nothing Then
                Return 0
            End If
            
            Dim leftDepth As Integer = MaxDepth(root.Left)
            Dim rightDepth As Integer = MaxDepth(root.Right)
            
            ' 当前节点的最大深度 = max(左子树深度, 右子树深度) + 1
            Return Math.Max(leftDepth, rightDepth) + 1
        End Function
        
        ' 面试题8: 有效的括号
        Public Shared Function IsValidParentheses(s As String) As Boolean
            If String.IsNullOrEmpty(s) Then
                Return True
            End If
            
            Dim stack As New Stack(Of Char)()
            
            For Each c As Char In s
                If c = "("c OrElse c = "["c OrElse c = "{"c Then
                    stack.Push(c)
                Else
                    If stack.Count = 0 Then
                        Return False
                    End If
                    
                    Dim top As Char = stack.Pop()
                    
                    If c = ")"c AndAlso top <> "("c Then
                        Return False
                    End If
                    If c = "]"c AndAlso top <> "["c Then
                        Return False
                    End If
                    If c = "}"c AndAlso top <> "{"c Then
                        Return False
                    End If
                End If
            Next
            
            Return stack.Count = 0
        End Function
    End Class

    ' 支持类：链表节点
    Public Class ListNode
        Public Property Val As Integer
        Public Property [Next] As ListNode
        
        Public Sub New(Optional val As Integer = 0, Optional [next] As ListNode = Nothing)
            Me.Val = val
            Me.Next = [next]
        End Sub
    End Class
    
    ' 支持类：二叉树节点
    Public Class TreeNode
        Public Property Val As Integer
        Public Property Left As TreeNode
        Public Property Right As TreeNode
        
        Public Sub New(Optional val As Integer = 0, Optional left As TreeNode = Nothing, Optional right As TreeNode = Nothing)
            Me.Val = val
            Me.Left = left
            Me.Right = right
        End Sub
    End Class
End Namespace 