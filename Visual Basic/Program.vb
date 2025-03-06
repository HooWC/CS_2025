Imports System

Namespace VBExamples
    Module Program
        Sub Main(args As String())
            Console.OutputEncoding = System.Text.Encoding.UTF8
            Console.WriteLine("Visual Basic 编程示例集合")
            Console.WriteLine("==========================")
            
            While True
                Console.WriteLine(vbCrLf & "请选择要运行的示例:")
                Console.WriteLine("1. 字符串操作示例")
                Console.WriteLine("2. LINQ查询示例")
                Console.WriteLine("3. 算法示例")
                Console.WriteLine("4. 高级特性示例")
                Console.WriteLine("5. VB特有功能示例")
                Console.WriteLine("0. 退出")
                
                Console.Write(vbCrLf & "请输入选项 (0-5): ")
                Dim input As String = Console.ReadLine()
                
                If input = "0" Then
                    Exit While
                End If
                
                Try
                    Select Case input
                        Case "1"
                            RunStringExamples()
                        Case "2"
                            RunLinqExamples()
                        Case "3"
                            RunAlgorithmExamples()
                        Case "4"
                            RunAdvancedExamples()
                        Case "5"
                            RunVBSpecificExamples()
                        Case Else
                            Console.WriteLine("无效选项，请重新选择")
                    End Select
                Catch ex As Exception
                    Console.WriteLine($"运行示例时出错: {ex.Message}")
                End Try
                
                Console.WriteLine(vbCrLf & "按任意键继续...")
                Console.ReadKey()
                Console.Clear()
            End While
        End Sub
        
        Private Sub RunStringExamples()
            Console.WriteLine(vbCrLf & "=== 字符串操作示例 ===" & vbCrLf)
            
            Console.WriteLine("1. 字符串分割示例:")
            StringOperations.SplitExamples()
            
            Console.WriteLine(vbCrLf & "2. 变位词检查:")
            Dim s1 As String = "listen"
            Dim s2 As String = "silent"
            Console.WriteLine($"'{s1}' 和 '{s2}' 是变位词? {StringOperations.AreAnagrams(s1, s2)}")
            
            Console.WriteLine(vbCrLf & "3. 字符串优化拼接:")
            Dim words() As String = {"Hello", "VB", "World"}
            Dim result As String = StringOperations.ConcatStringsOptimized(words, 3)
            Console.WriteLine($"结果: {result}")
            
            Console.WriteLine(vbCrLf & "4. 邮箱验证:")
            Dim email1 As String = "user@example.com"
            Dim email2 As String = "invalid-email"
            Console.WriteLine($"'{email1}' 是有效邮箱? {StringOperations.IsValidEmail(email1)}")
            Console.WriteLine($"'{email2}' 是有效邮箱? {StringOperations.IsValidEmail(email2)}")
            
            Console.WriteLine(vbCrLf & "5. 第一个不重复字符:")
            Dim text As String = "leetcode"
            Dim index As Integer = StringOperations.FirstUniqueCharIndex(text)
            Console.WriteLine($"在 '{text}' 中第一个不重复字符的索引: {index}")
            
            Console.WriteLine(vbCrLf & "6. VB特有的字符串功能:")
            StringOperations.VBStringExamples()
        End Sub
        
        Private Sub RunLinqExamples()
            Console.WriteLine(vbCrLf & "=== LINQ示例 ===" & vbCrLf)
            
            Console.WriteLine("1. 基本LINQ查询:")
            LinqExamples.BasicQueries()
            
            Console.WriteLine(vbCrLf & "2. 排序和分组:")
            LinqExamples.SortingAndGrouping()
            
            Console.WriteLine(vbCrLf & "3. 数据转换:")
            LinqExamples.TransformationExamples()
            
            Console.WriteLine(vbCrLf & "4. 连接操作:")
            LinqExamples.JoinOperations()
            
            Console.WriteLine(vbCrLf & "5. 高级LINQ操作:")
            LinqExamples.AdvancedLinqOperations()
        End Sub
        
        Private Sub RunAlgorithmExamples()
            Console.WriteLine(vbCrLf & "=== 算法示例 ===" & vbCrLf)
            
            Console.WriteLine("1. 两数之和:")
            Dim nums() As Integer = {2, 7, 11, 15}
            Dim target As Integer = 9
            Dim result() As Integer = AlgorithmExamples.TwoSum(nums, target)
            
            If result.Length = 2 Then
                Console.WriteLine($"结果索引: [{result(0)}, {result(1)}], 值: [{nums(result(0))}, {nums(result(1))}]")
            Else
                Console.WriteLine("未找到解")
            End If
            
            Console.WriteLine(vbCrLf & "2. 回文检查:")
            Dim palindrome1 As String = "A man, a plan, a canal: Panama"
            Dim palindrome2 As String = "race a car"
            Console.WriteLine($"'{palindrome1}' 是回文? {AlgorithmExamples.IsPalindrome(palindrome1)}")
            Console.WriteLine($"'{palindrome2}' 是回文? {AlgorithmExamples.IsPalindrome(palindrome2)}")
            
            Console.WriteLine(vbCrLf & "3. 最大子数组和:")
            Dim array() As Integer = {-2, 1, -3, 4, -1, 2, 1, -5, 4}
            Dim maxSum As Integer = AlgorithmExamples.MaxSubArray(array)
            Console.WriteLine($"最大子数组和: {maxSum}")
            
            Console.WriteLine(vbCrLf & "4. 有效括号:")
            Dim brackets1 As String = "()[]{}";
            Dim brackets2 As String = "([)]";
            Console.WriteLine($"'{brackets1}' 有效? {AlgorithmExamples.IsValidParentheses(brackets1)}")
            Console.WriteLine($"'{brackets2}' 有效? {AlgorithmExamples.IsValidParentheses(brackets2)}")
        End Sub
        
        Private Sub RunAdvancedExamples()
            Console.WriteLine(vbCrLf & "=== 高级特性示例 ===" & vbCrLf)
            
            Console.WriteLine("1. 扩展方法示例:")
            Dim text As String = "这是一个很长的字符串，我们要对它进行截断处理"
            Dim truncated As String = ExtensionMethods.Truncate(text, 10)
            Console.WriteLine($"原始字符串: {text}")
            Console.WriteLine($"截断后: {truncated}")
            
            Dim numberText As String = "123"
            Dim number As Integer = ExtensionMethods.ToInt(numberText)
            Dim invalidNumber As String = ExtensionMethods.ToInt("abc", -1).ToString()
            Console.WriteLine($"转换字符串到数字: '{numberText}' -> {number}")
            Console.WriteLine($"无效数字使用默认值: 'abc' -> {invalidNumber}")
            
            Console.WriteLine(vbCrLf & "2. 事件示例:")
            EventExample.DemonstrateEvents()
        End Sub
        
        Private Sub RunVBSpecificExamples()
            Console.WriteLine(vbCrLf & "=== VB特有功能示例 ===" & vbCrLf)
            
            Console.WriteLine("1. With语句:")
            VBSpecificFeatures.WithStatementExample()
            
            Console.WriteLine(vbCrLf & "2. 错误处理:")
            VBSpecificFeatures.ErrorHandlingExample()
            
            Console.WriteLine(vbCrLf & "3. Option特性:")
            VBSpecificFeatures.OptionFeatures()
        End Sub
    End Module
End Namespace 