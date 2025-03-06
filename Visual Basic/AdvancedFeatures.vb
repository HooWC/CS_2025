Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Text.Json
Imports System.Text.RegularExpressions

Namespace VBExamples
    ' 示例：使用扩展方法实现实用工具类
    Public Module ExtensionMethods
        ' 扩展方法：判断一个集合是否为空
        <System.Runtime.CompilerServices.Extension()>
        Public Function IsNullOrEmpty(Of T)(collection As IEnumerable(Of T)) As Boolean
            Return collection Is Nothing OrElse Not collection.Any()
        End Function
        
        ' 扩展方法：获取集合中满足条件的第一个元素，找不到则返回默认值
        <System.Runtime.CompilerServices.Extension()>
        Public Function FirstOrDefault(Of T)(collection As IEnumerable(Of T), predicate As Func(Of T, Boolean), defaultValue As T) As T
            If collection.IsNullOrEmpty() Then
                Return defaultValue
            End If
            
            For Each item As T In collection
                If predicate(item) Then
                    Return item
                End If
            Next
            
            Return defaultValue
        End Function
        
        ' 扩展方法：安全地将字符串转换为整数
        <System.Runtime.CompilerServices.Extension()>
        Public Function ToInt(value As String, Optional defaultValue As Integer = 0) As Integer
            If String.IsNullOrEmpty(value) Then
                Return defaultValue
            End If
            
            Dim result As Integer
            Return If(Integer.TryParse(value, result), result, defaultValue)
        End Function
        
        ' 扩展方法：安全地将字符串转换为DateTime
        <System.Runtime.CompilerServices.Extension()>
        Public Function ToDateTime(value As String, defaultValue As DateTime) As DateTime
            If String.IsNullOrEmpty(value) Then
                Return defaultValue
            End If
            
            Dim result As DateTime
            Return If(DateTime.TryParse(value, result), result, defaultValue)
        End Function
        
        ' 扩展方法：截断字符串
        <System.Runtime.CompilerServices.Extension()>
        Public Function Truncate(value As String, maxLength As Integer, Optional suffix As String = "...") As String
            If String.IsNullOrEmpty(value) OrElse value.Length <= maxLength Then
                Return value
            End If
            
            Return value.Substring(0, maxLength) & suffix
        End Function
    End Module
    
    ' 示例：异步编程 - 异步方法和任务处理
    Public Class AsyncExamples
        ' 模拟异步数据获取
        Public Shared Async Function FetchDataAsync() As Task(Of List(Of String))
            ' 模拟网络延迟
            Await Task.Delay(1000)
            
            Return New List(Of String) From {"数据1", "数据2", "数据3"}
        End Function
        
        ' 并行处理多个异步任务
        Public Shared Async Function ProcessMultipleTasksAsync() As Task(Of List(Of String))
            ' 创建多个异步任务
            Dim task1 As Task(Of String) = SimulateApiCallAsync("API1")
            Dim task2 As Task(Of String) = SimulateApiCallAsync("API2")
            Dim task3 As Task(Of String) = SimulateApiCallAsync("API3")
            
            ' 等待所有任务完成
            Await Task.WhenAll(task1, task2, task3)
            
            ' 收集所有任务的结果
            Return New List(Of String) From {
                task1.Result,
                task2.Result,
                task3.Result
            }
        End Function
        
        Private Shared Async Function SimulateApiCallAsync(apiName As String) As Task(Of String)
            ' 模拟不同API的处理时间
            Dim delay As Integer = If(apiName = "API2", 2000, 1000)
            Await Task.Delay(delay)
            
            Return $"{apiName} 返回的数据"
        End Function
        
        ' 使用任务超时
        Public Shared Async Function FetchWithTimeoutAsync() As Task(Of String)
            Try
                ' 使用Task.WhenAny和Task.Delay实现超时功能
                Dim dataTask As Task(Of String) = SlowOperationAsync()
                Dim timeoutTask As Task = Task.Delay(2000) ' 2秒超时
                
                Dim completedTask As Task = Await Task.WhenAny(dataTask, timeoutTask)
                
                If completedTask Is timeoutTask Then
                    Return "操作超时"
                End If
                
                Return Await dataTask
            Catch ex As Exception
                Return $"发生错误: {ex.Message}"
            End Try
        End Function
        
        Private Shared Async Function SlowOperationAsync() As Task(Of String)
            Await Task.Delay(3000) ' 模拟一个耗时3秒的操作
            Return "操作完成"
        End Function
    End Class
    
    ' 示例：使用委托和事件
    Public Class EventExample
        ' 定义委托和事件
        Public Delegate Sub DataReceivedHandler(sender As Object, e As DataEventArgs)
        Public Event DataReceived As DataReceivedHandler
        
        ' 事件参数类
        Public Class DataEventArgs
            Inherits EventArgs
            
            Public Property Data As String
            Public Property ReceivedTime As DateTime
            
            Public Sub New(data As String)
                Me.Data = data
                Me.ReceivedTime = DateTime.Now
            End Sub
        End Class
        
        ' 触发事件的方法
        Public Sub ProcessData(data As String)
            Console.WriteLine($"处理数据: {data}")
            
            ' 触发事件，通知所有订阅者
            RaiseEvent DataReceived(Me, New DataEventArgs(data))
        End Sub
        
        ' 示例：如何使用该事件
        Public Shared Sub DemonstrateEvents()
            Dim example As New EventExample()
            
            ' 订阅事件 - 使用Lambda表达式
            AddHandler example.DataReceived, Sub(sender, e)
                Console.WriteLine($"收到数据事件: {e.Data}, 时间: {e.ReceivedTime}")
            End Sub
            
            ' 添加第二个事件处理程序
            AddHandler example.DataReceived, AddressOf HandleData
            
            ' 触发事件
            example.ProcessData("测试数据")
            example.ProcessData("更多数据")
            
            ' 取消订阅
            RemoveHandler example.DataReceived, AddressOf HandleData
            
            ' 再次触发事件
            example.ProcessData("最终数据")
        End Sub
        
        Private Shared Sub HandleData(sender As Object, e As DataEventArgs)
            Console.WriteLine($"处理程序2收到数据: {e.Data}")
        End Sub
    End Class
    
    ' 示例：使用反射动态访问类型
    Public Class ReflectionExample
        Public Shared Sub DemonstrateReflection()
            Dim typeName As String = "VBExamples.Student"
            
            ' 获取类型
            Dim studentType As Type = Type.GetType(typeName)
            If studentType Is Nothing Then
                ' 尝试从当前程序集获取类型
                studentType = GetType(Student)
                If studentType Is Nothing Then
                    Console.WriteLine($"找不到类型: {typeName}")
                    Return
                End If
            End If
            
            ' 创建实例
            Dim studentInstance As Object = Activator.CreateInstance(studentType)
            
            ' 设置属性值
            Dim nameProperty As Reflection.PropertyInfo = studentType.GetProperty("Name")
            If nameProperty IsNot Nothing Then
                nameProperty.SetValue(studentInstance, "张三")
            End If
            
            Dim ageProperty As Reflection.PropertyInfo = studentType.GetProperty("Age")
            If ageProperty IsNot Nothing Then
                ageProperty.SetValue(studentInstance, 20)
            End If
            
            ' 调用方法
            Dim toStringMethod As Reflection.MethodInfo = studentType.GetMethod("ToString")
            If toStringMethod IsNot Nothing Then
                Dim result As String = CStr(toStringMethod.Invoke(studentInstance, Nothing))
                Console.WriteLine($"ToString方法返回: {result}")
            End If
            
            ' 使用反射遍历类型的所有属性
            Console.WriteLine(vbCrLf & "类型属性:")
            For Each prop As Reflection.PropertyInfo In studentType.GetProperties()
                Dim value As Object = prop.GetValue(studentInstance)
                Console.WriteLine($"{prop.Name}: {value}")
            Next
        End Sub
    End Class
    
    ' VB特有的语言特性
    Public Class VBSpecificFeatures
        ' 使用With语句
        Public Shared Sub WithStatementExample()
            Dim student As New Student()
            
            ' 使用With语句简化多个属性赋值
            With student
                .Id = 1
                .Name = "张三"
                .Age = 20
                .Score = 85
                .Major = "计算机科学"
            End With
            
            Console.WriteLine($"学生信息: {student.Name}, {student.Age}岁, 专业: {student.Major}")
        End Sub
        
        ' 使用结构化错误处理
        Public Shared Sub ErrorHandlingExample()
            On Error Resume Next ' 忽略错误并继续执行
            
            Dim result As Integer = 10 \ 0 ' 除零错误
            
            If Err.Number <> 0 Then
                Console.WriteLine($"发生错误: {Err.Description}")
                Err.Clear()
            End If
            
            ' 使用Try/Catch结构化错误处理
            On Error GoTo 0 ' 禁用On Error Resume Next
            
            Try
                result = 10 \ 0
            Catch ex As DivideByZeroException
                Console.WriteLine($"捕获到除零错误: {ex.Message}")
            Catch ex As Exception
                Console.WriteLine($"捕获到其他错误: {ex.Message}")
            Finally
                Console.WriteLine("Finally块总是执行")
            End Try
        End Sub
        
        ' 使用Option特性
        Public Shared Sub OptionFeatures()
            ' Option Explicit强制变量声明
            ' Option Strict强制类型检查
            ' Option Infer允许类型推断
            
            ' 类型推断示例
            Dim numbers = New List(Of Integer) From {1, 2, 3, 4, 5} ' 类型推断为List(Of Integer)
            Dim name = "张三" ' 类型推断为String
            
            Console.WriteLine($"名称类型: {name.GetType().Name}")
            Console.WriteLine($"集合类型: {numbers.GetType().Name}")
        End Sub
    End Class
End Namespace 