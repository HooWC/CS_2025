using System;
using System.Collections.Generic;
using System.Linq;

namespace CS1
{
    public class LinqExamples
    {
        // 定义一些示例数据
        private static List<Student> GetStudents()
        {
            return new List<Student>
            {
                new Student { Id = 1, Name = "张三", Age = 20, Score = 85, Major = "计算机科学" },
                new Student { Id = 2, Name = "李四", Age = 22, Score = 92, Major = "软件工程" },
                new Student { Id = 3, Name = "王五", Age = 21, Score = 78, Major = "数据科学" },
                new Student { Id = 4, Name = "赵六", Age = 23, Score = 96, Major = "人工智能" },
                new Student { Id = 5, Name = "钱七", Age = 20, Score = 85, Major = "计算机科学" },
                new Student { Id = 6, Name = "孙八", Age = 22, Score = 65, Major = "软件工程" },
            };
        }

        // LINQ基本查询和过滤
        public static void BasicQueries()
        {
            var students = GetStudents();

            // 查询语法 - 查找计算机科学专业的学生
            var csStudents = from s in students
                             where s.Major == "计算机科学"
                             select s;

            Console.WriteLine("计算机科学专业的学生:");
            foreach (var s in csStudents)
                Console.WriteLine($"{s.Name}, 年龄: {s.Age}, 分数: {s.Score}");

            // 方法语法 - 查找成绩大于等于90的学生
            var highScoreStudents = students.Where(s => s.Score >= 90);

            Console.WriteLine("\n成绩优秀的学生:");
            foreach (var s in highScoreStudents)
                Console.WriteLine($"{s.Name}, 分数: {s.Score}, 专业: {s.Major}");
        }

        // 面试题：使用LINQ对集合进行排序和分组
        public static void SortingAndGrouping()
        {
            var students = GetStudents();

            // 按分数降序排序
            var sortedByScore = students.OrderByDescending(s => s.Score);
            Console.WriteLine("按分数降序排序:");
            foreach (var s in sortedByScore)
                Console.WriteLine($"{s.Name}: {s.Score}");

            // 按专业分组并计算每组的平均分
            var groupedByMajor = students
                .GroupBy(s => s.Major)
                .Select(g => new
                {
                    Major = g.Key,
                    AverageScore = g.Average(s => s.Score),
                    Count = g.Count()
                });

            Console.WriteLine("\n专业分组与平均分:");
            foreach (var group in groupedByMajor)
                Console.WriteLine($"{group.Major}: 平均分 {group.AverageScore:F2}, 学生数 {group.Count}");
        }

        // 使用LINQ执行复杂的数据转换
        public static void TransformationExamples()
        {
            var students = GetStudents();

            // 使用Select进行投影转换
            var studentSummaries = students.Select(s => new
            {
                FullInfo = $"{s.Name} ({s.Age}岁)",
                GradeLevel = s.Score >= 90 ? "优秀" : (s.Score >= 75 ? "良好" : "一般")
            });

            Console.WriteLine("学生成绩评级:");
            foreach (var summary in studentSummaries)
                Console.WriteLine($"{summary.FullInfo} - {summary.GradeLevel}");

            // 使用SelectMany拍平集合
            var studentCourses = new List<StudentCourses>
            {
                new StudentCourses {
                    StudentId = 1,
                    Courses = new List<string> { "数据结构", "算法", "数据库" }
                },
                new StudentCourses {
                    StudentId = 2,
                    Courses = new List<string> { "编译原理", "操作系统", "网络编程" }
                }
            };

            // 获取所有课程的平铺列表
            var allCourses = studentCourses.SelectMany(sc => sc.Courses);

            Console.WriteLine("\n所有课程列表:");
            foreach (var course in allCourses)
                Console.WriteLine(course);
        }

        // 面试题：使用LINQ实现SQL-like查询（连接操作）
        public static void JoinOperations()
        {
            var students = GetStudents();

            var courses = new List<Course>
            {
                new Course { Id = 101, Name = "数据结构", MajorId = "计算机科学" },
                new Course { Id = 102, Name = "数据库基础", MajorId = "计算机科学" },
                new Course { Id = 201, Name = "软件工程方法学", MajorId = "软件工程" },
                new Course { Id = 301, Name = "数据挖掘", MajorId = "数据科学" },
                new Course { Id = 401, Name = "机器学习", MajorId = "人工智能" }
            };

            // 内连接：获取每个学生可以学习的课程
            var studentCourses = students.Join(
                courses,
                student => student.Major,
                course => course.MajorId,
                (student, course) => new
                {
                    StudentName = student.Name,
                    CourseName = course.Name,
                    Major = student.Major
                }
            );

            Console.WriteLine("学生与其专业课程:");
            foreach (var item in studentCourses)
                Console.WriteLine($"{item.StudentName} ({item.Major}) - {item.CourseName}");

            // 左外连接(使用GroupJoin和SelectMany结合实现)
            var leftJoin = students.GroupJoin(
                courses,
                student => student.Major,
                course => course.MajorId,
                (student, courseGroup) => new
                {
                    student,
                    courseGroup
                })
                .SelectMany(
                    x => x.courseGroup.DefaultIfEmpty(),
                    (studentGroup, course) => new
                    {
                        StudentName = studentGroup.student.Name,
                        CourseName = course == null ? "无课程" : course.Name,
                        Major = studentGroup.student.Major
                    }
                );
        }

        // 面试题：使用高阶LINQ操作
        public static void AdvancedLinqOperations()
        {
            var numbers = Enumerable.Range(1, 20).ToList();

            // Skip和Take的组合使用 - 分页操作
            int pageSize = 5;
            int pageNumber = 2; // 第二页

            var pageItems = numbers.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            Console.WriteLine($"第{pageNumber}页 (每页{pageSize}项):");
            Console.WriteLine(string.Join(", ", pageItems));

            // 聚合函数
            var students = GetStudents();
            double averageScore = students.Average(s => s.Score);
            double maxScore = students.Max(s => s.Score);
            double totalScore = students.Sum(s => s.Score);

            Console.WriteLine($"\n统计: 平均分={averageScore:F2}, 最高分={maxScore}, 总分={totalScore}");

            // 自定义聚合
            string namesConcatenated = students.Aggregate("学生名单: ",
                (current, student) => current + student.Name + ", ",
                result => result.TrimEnd(',', ' ') + "。");

            Console.WriteLine(namesConcatenated);

            // 使用Zip合并两个序列
            var firstNames = new List<string> { "张", "李", "王" };
            var lastNames = new List<string> { "小明", "小红", "小刚" };

            var fullNames = firstNames.Zip(lastNames, (first, last) => $"{first}{last}");
            Console.WriteLine("\n合并姓名: " + string.Join(", ", fullNames));
        }
    }

    // 定义支持类
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Score { get; set; }
        public string Major { get; set; }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MajorId { get; set; }
    }

    public class StudentCourses
    {
        public int StudentId { get; set; }
        public List<string> Courses { get; set; }
    }
}