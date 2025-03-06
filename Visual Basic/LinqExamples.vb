Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace VBExamples
    Public Class LinqExamples
        ' 定义一些示例数据
        Private Shared Function GetStudents() As List(Of Student)
            Return New List(Of Student) From {
                New Student With {.Id = 1, .Name = "张三", .Age = 20, .Score = 85, .Major = "计算机科学"},
                New Student With {.Id = 2, .Name = "李四", .Age = 22, .Score = 92, .Major = "软件工程"},
                New Student With {.Id = 3, .Name = "王五", .Age = 21, .Score = 78, .Major = "数据科学"},
                New Student With {.Id = 4, .Name = "赵六", .Age = 23, .Score = 96, .Major = "人工智能"},
                New Student With {.Id = 5, .Name = "钱七", .Age = 20, .Score = 85, .Major = "计算机科学"},
                New Student With {.Id = 6, .Name = "孙八", .Age = 22, .Score = 65, .Major = "软件工程"}
            }
        End Function
        
        ' LINQ基本查询和过滤
        Public Shared Sub BasicQueries()
            Dim students = GetStudents()
            
            ' 查询语法 - 查找计算机科学专业的学生
            Dim csStudents = From s In students
                             Where s.Major = "计算机科学"
                             Select s
            
            Console.WriteLine("计算机科学专业的学生:")
            For Each s In csStudents
                Console.WriteLine($"{s.Name}, 年龄: {s.Age}, 分数: {s.Score}")
            Next
            
            ' 方法语法 - 查找成绩大于等于90的学生
            Dim highScoreStudents = students.Where(Function(s) s.Score >= 90)
            
            Console.WriteLine(vbCrLf & "成绩优秀的学生:")
            For Each s In highScoreStudents
                Console.WriteLine($"{s.Name}, 分数: {s.Score}, 专业: {s.Major}")
            Next
        End Sub
        
        ' 面试题：使用LINQ对集合进行排序和分组
        Public Shared Sub SortingAndGrouping()
            Dim students = GetStudents()
            
            ' 按分数降序排序
            Dim sortedByScore = students.OrderByDescending(Function(s) s.Score)
            Console.WriteLine("按分数降序排序:")
            For Each s In sortedByScore
                Console.WriteLine($"{s.Name}: {s.Score}")
            Next
            
            ' 按专业分组并计算每组的平均分
            Dim groupedByMajor = students _
                .GroupBy(Function(s) s.Major) _
                .Select(Function(g) New With {
                    .Major = g.Key,
                    .AverageScore = g.Average(Function(s) s.Score),
                    .Count = g.Count()
                })
            
            Console.WriteLine(vbCrLf & "专业分组与平均分:")
            For Each group In groupedByMajor
                Console.WriteLine($"{group.Major}: 平均分 {group.AverageScore:F2}, 学生数 {group.Count}")
            Next
        End Sub
        
        ' 使用LINQ执行复杂的数据转换
        Public Shared Sub TransformationExamples()
            Dim students = GetStudents()
            
            ' 使用Select进行投影转换
            Dim studentSummaries = students.Select(Function(s) New With {
                .FullInfo = $"{s.Name} ({s.Age}岁)",
                .GradeLevel = If(s.Score >= 90, "优秀", If(s.Score >= 75, "良好", "一般"))
            })
            
            Console.WriteLine("学生成绩评级:")
            For Each summary In studentSummaries
                Console.WriteLine($"{summary.FullInfo} - {summary.GradeLevel}")
            Next
            
            ' 使用SelectMany拍平集合
            Dim studentCourses = New List(Of StudentCourses) From {
                New StudentCourses With {
                    .StudentId = 1,
                    .Courses = New List(Of String) From {"数据结构", "算法", "数据库"}
                },
                New StudentCourses With {
                    .StudentId = 2,
                    .Courses = New List(Of String) From {"编译原理", "操作系统", "网络编程"}
                }
            }
            
            ' 获取所有课程的平铺列表
            Dim allCourses = studentCourses.SelectMany(Function(sc) sc.Courses)
            
            Console.WriteLine(vbCrLf & "所有课程列表:")
            For Each course In allCourses
                Console.WriteLine(course)
            Next
        End Sub
        
        ' 面试题：使用LINQ实现SQL-like查询（连接操作）
        Public Shared Sub JoinOperations()
            Dim students = GetStudents()
            
            Dim courses = New List(Of Course) From {
                New Course With {.Id = 101, .Name = "数据结构", .MajorId = "计算机科学"},
                New Course With {.Id = 102, .Name = "数据库基础", .MajorId = "计算机科学"},
                New Course With {.Id = 201, .Name = "软件工程方法学", .MajorId = "软件工程"},
                New Course With {.Id = 301, .Name = "数据挖掘", .MajorId = "数据科学"},
                New Course With {.Id = 401, .Name = "机器学习", .MajorId = "人工智能"}
            }
            
            ' 内连接：获取每个学生可以学习的课程
            ' 使用查询语法
            Dim studentCourses = From student In students
                                Join course In courses
                                On student.Major Equals course.MajorId
                                Select New With {
                                    .StudentName = student.Name,
                                    .CourseName = course.Name,
                                    .Major = student.Major
                                }
            
            Console.WriteLine("学生与其专业课程:")
            For Each item In studentCourses
                Console.WriteLine($"{item.StudentName} ({item.Major}) - {item.CourseName}")
            Next
            
            ' 使用方法语法实现左连接
            ' VB中使用Group Join和SelectMany组合实现左连接
            Dim leftJoin = students.GroupJoin(
                courses,
                Function(student) student.Major,
                Function(course) course.MajorId,
                Function(student, courseGroup) New With {
                    .Student = student,
                    .Courses = courseGroup
                }).SelectMany(
                    Function(x) x.Courses.DefaultIfEmpty(),
                    Function(studentGroup, course) New With {
                        .StudentName = studentGroup.Student.Name,
                        .CourseName = If(course Is Nothing, "无课程", course.Name),
                        .Major = studentGroup.Student.Major
                    }
                )
                
            Console.WriteLine(vbCrLf & "左连接结果:")
            For Each item In leftJoin
                Console.WriteLine($"{item.StudentName} ({item.Major}) - {item.CourseName}")
            Next
        End Sub
        
        ' 面试题：使用高阶LINQ操作
        Public Shared Sub AdvancedLinqOperations()
            Dim numbers = Enumerable.Range(1, 20).ToList()
            
            ' Skip和Take的组合使用 - 分页操作
            Dim pageSize As Integer = 5
            Dim pageNumber As Integer = 2 ' 第二页
            
            Dim pageItems = numbers.Skip((pageNumber - 1) * pageSize).Take(pageSize)
            Console.WriteLine($"第{pageNumber}页 (每页{pageSize}项):")
            Console.WriteLine(String.Join(", ", pageItems))
            
            ' 聚合函数
            Dim students = GetStudents()
            Dim averageScore As Double = students.Average(Function(s) s.Score)
            Dim maxScore As Double = students.Max(Function(s) s.Score)
            Dim totalScore As Double = students.Sum(Function(s) s.Score)
            
            Console.WriteLine($"{vbCrLf}统计: 平均分={averageScore:F2}, 最高分={maxScore}, 总分={totalScore}")
            
            ' 自定义聚合
            Dim namesConcatenated As String = students.Aggregate("学生名单: ",
                Function(current, student) current & student.Name & ", ",
                Function(result) result.TrimEnd(","c, " "c) & "。")
            
            Console.WriteLine(namesConcatenated)
            
            ' 使用Zip合并两个序列
            Dim firstNames = New List(Of String) From {"张", "李", "王"}
            Dim lastNames = New List(Of String) From {"小明", "小红", "小刚"}
            
            Dim fullNames = firstNames.Zip(lastNames, Function(first, last) $"{first}{last}")
            Console.WriteLine(vbCrLf & "合并姓名: " & String.Join(", ", fullNames))
            
            ' VB特有的LINQ功能：使用Distinct去重
            Dim duplicateScores = New List(Of Integer) From {85, 92, 78, 85, 96, 78, 65}
            Dim uniqueScores = duplicateScores.Distinct().OrderBy(Function(x) x)
            Console.WriteLine(vbCrLf & "去重后的分数: " & String.Join(", ", uniqueScores))
        End Sub
    End Class

    ' 定义支持类
    Public Class Student
        Public Property Id As Integer
        Public Property Name As String
        Public Property Age As Integer
        Public Property Score As Double
        Public Property Major As String
    End Class
    
    Public Class Course
        Public Property Id As Integer
        Public Property Name As String
        Public Property MajorId As String
    End Class
    
    Public Class StudentCourses
        Public Property StudentId As Integer
        Public Property Courses As List(Of String)
    End Class
End Namespace 