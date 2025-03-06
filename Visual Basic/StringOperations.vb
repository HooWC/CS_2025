Imports System
Imports System.Text
Imports System.Text.RegularExpressions

Namespace VBExamples
    Public Class StringOperations
        ' 字符串分割的高级用法
        Public Shared Sub SplitExamples()
            ' 基本Split用法
            Dim csvLine As String = "苹果,香蕉,橙子,葡萄"
            Dim fruits() As String = csvLine.Split(","c)
            Console.WriteLine($"分割后的水果数量: {fruits.Length}")
            
            ' 使用多个分隔符
            Dim data As String = "苹果,香蕉;橙子|葡萄"
            Dim separators() As Char = {","c, ";"c, "|"c}
            Dim mixedFruits() As String = data.Split(separators)
            Console.WriteLine($"使用多分隔符: {String.Join(" & ", mixedFruits)}")
            
            ' 限制分割次数
            Dim text As String = "a,b,c,d,e,f"
            Dim limitedParts() As String = text.Split(","c, 3)
            Console.WriteLine($"限制分割: {String.Join(" - ", limitedParts)}")
            
            ' 移除空条目
            Dim spaceText As String = "一,,二, ,三"
            Dim options As StringSplitOptions = StringSplitOptions.RemoveEmptyEntries
            Dim noEmptyParts() As String = spaceText.Split(New Char() {","c}, options)
            Console.WriteLine($"移除空条目: {String.Join("/", noEmptyParts)}")
        End Sub
        
        ' 字符串处理面试题: 判断两个字符串是否为变位词(字母相同但顺序不同)
        Public Shared Function AreAnagrams(s1 As String, s2 As String) As Boolean
            If String.IsNullOrEmpty(s1) OrElse String.IsNullOrEmpty(s2) OrElse s1.Length <> s2.Length Then
                Return False
            End If
            
            ' 方法1: 排序比较
            Dim chars1() As Char = s1.ToLower().ToCharArray()
            Dim chars2() As Char = s2.ToLower().ToCharArray()
            
            Array.Sort(chars1)
            Array.Sort(chars2)
            
            Return New String(chars1) = New String(chars2)
        End Function
        
        ' 使用StringBuilder优化字符串拼接
        Public Shared Function ConcatStringsOptimized(words() As String, repeatTimes As Integer) As String
            Dim sb As New StringBuilder()
            
            For i As Integer = 0 To repeatTimes - 1
                For Each word As String In words
                    sb.Append(word)
                Next
                
                If i < repeatTimes - 1 Then
                    sb.Append(" | ")
                End If
            Next
            
            Return sb.ToString()
        End Function
        
        ' 使用正则表达式进行模式匹配
        Public Shared Function IsValidEmail(email As String) As Boolean
            ' 简单的邮箱验证正则表达式
            Dim pattern As String = "^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
            Return Regex.IsMatch(email, pattern)
        End Function
        
        ' 面试题: 找出字符串中第一个不重复的字符的索引
        Public Shared Function FirstUniqueCharIndex(s As String) As Integer
            ' 使用字典记录每个字符出现的次数
            Dim charCount As New Dictionary(Of Char, Integer)()
            
            For Each c As Char In s
                If charCount.ContainsKey(c) Then
                    charCount(c) += 1
                Else
                    charCount.Add(c, 1)
                End If
            Next
            
            ' 找到第一个只出现一次的字符
            For i As Integer = 0 To s.Length - 1
                If charCount(s(i)) = 1 Then
                    Return i
                End If
            Next
            
            Return -1 ' 没找到不重复的字符
        End Function
        
        ' VB特有的字符串功能：字符串匹配
        Public Shared Sub VBStringExamples()
            ' Like运算符
            Dim text As String = "HelloWorld"
            Dim pattern As String = "Hello*"
            Console.WriteLine($"'HelloWorld' Like 'Hello*': {text Like pattern}")
            
            ' Mid用于替换字符串中的字符
            Dim sentence As String = "这是一个示例字符串"
            Mid(sentence, 3, 2) = "不是"
            Console.WriteLine($"修改后的字符串: {sentence}")
            
            ' InStr和InStrRev函数
            Dim searchText As String = "Visual Basic是一种非常易用的编程语言"
            Dim position As Integer = InStr(searchText, "编程")
            Console.WriteLine($"'编程'在字符串中的位置: {position}")
            
            Dim lastPosition As Integer = InStrRev(searchText, "是")
            Console.WriteLine($"'是'最后出现的位置: {lastPosition}")
        End Sub
    End Class
End Namespace 