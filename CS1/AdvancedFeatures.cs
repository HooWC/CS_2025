using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CS1
{
    // 示例：使用泛型和扩展方法实现实用工具类
    public static class Utils
    {
        // 扩展方法：判断一个集合是否为空
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        // 扩展方法：获取集合中满足条件的第一个元素，找不到则返回默认值
        public static T FirstOrDefault<T>(this IEnumerable<T> collection, Func<T, bool> predicate, T defaultValue)
        {
            if (collection.IsNullOrEmpty())
                return defaultValue;

            foreach (var item in collection)
            {
                if (predicate(item))
                    return item;
            }

            return defaultValue;
        }

        // 扩展方法：安全地将字符串转换为整数
        public static int ToInt(this string value, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            return int.TryParse(value, out int result) ? result : defaultValue;
        }

        // 扩展方法：安全地将字符串转换为DateTime
        public static DateTime ToDateTime(this string value, DateTime defaultValue)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            return DateTime.TryParse(value, out DateTime result) ? result : defaultValue;
        }

        // 扩展方法：截断字符串
        public static string Truncate(this string value, int maxLength, string suffix = "...")
        {
            if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
                return value;

            return value.Substring(0, maxLength) + suffix;
        }
    }

    // 示例：异步编程 - 异步方法和任务处理
    public class AsyncExamples
    {
        // 模拟异步数据获取
        public static async Task<List<string>> FetchDataAsync()
        {
            // 模拟网络延迟
            await Task.Delay(1000);

            return new List<string> { "数据1", "数据2", "数据3" };
        }

        // 并行处理多个异步任务
        public static async Task<List<string>> ProcessMultipleTasksAsync()
        {
            // 创建多个异步任务
            Task<string> task1 = SimulateApiCallAsync("API1");
            Task<string> task2 = SimulateApiCallAsync("API2");
            Task<string> task3 = SimulateApiCallAsync("API3");

            // 等待所有任务完成
            await Task.WhenAll(task1, task2, task3);

            // 收集所有任务的结果
            return new List<string> {
                task1.Result,
                task2.Result,
                task3.Result
            };
        }

        private static async Task<string> SimulateApiCallAsync(string apiName)
        {
            // 模拟不同API的处理时间
            int delay = apiName == "API2" ? 2000 : 1000;
            await Task.Delay(delay);

            return $"{apiName} 返回的数据";
        }

        // 使用任务超时
        public static async Task<string> FetchWithTimeoutAsync()
        {
            try
            {
                // 使用Task.WhenAny和Task.Delay实现超时功能
                Task<string> dataTask = SlowOperationAsync();
                Task timeoutTask = Task.Delay(2000); // 2秒超时

                Task completedTask = await Task.WhenAny(dataTask, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    return "操作超时";
                }

                return await dataTask;
            }
            catch (Exception ex)
            {
                return $"发生错误: {ex.Message}";
            }
        }

        private static async Task<string> SlowOperationAsync()
        {
            await Task.Delay(3000); // 模拟一个耗时3秒的操作
            return "操作完成";
        }
    }

    // 示例：使用委托和事件
    public class EventExample
    {
        // 定义委托和事件
        public delegate void DataReceivedHandler(object sender, DataEventArgs e);
        public event DataReceivedHandler DataReceived;

        // 事件参数类
        public class DataEventArgs : EventArgs
        {
            public string Data { get; set; }
            public DateTime ReceivedTime { get; set; }

            public DataEventArgs(string data)
            {
                Data = data;
                ReceivedTime = DateTime.Now;
            }
        }

        // 触发事件的方法
        public void ProcessData(string data)
        {
            Console.WriteLine($"处理数据: {data}");

            // 触发事件，通知所有订阅者
            OnDataReceived(new DataEventArgs(data));
        }

        // 触发事件的保护方法
        protected virtual void OnDataReceived(DataEventArgs e)
        {
            // 通过线程安全的方式调用事件
            DataReceived?.Invoke(this, e);
        }

        // 示例：如何使用该事件
        public static void DemonstrateEvents()
        {
            EventExample example = new EventExample();

            // 订阅事件
            example.DataReceived += (sender, e) =>
            {
                Console.WriteLine($"收到数据事件: {e.Data}, 时间: {e.ReceivedTime}");
            };

            // 添加第二个事件处理程序
            example.DataReceived += HandleData;

            // 触发事件
            example.ProcessData("测试数据");
            example.ProcessData("更多数据");

            // 取消订阅
            example.DataReceived -= HandleData;

            // 再次触发事件
            example.ProcessData("最终数据");
        }

        private static void HandleData(object sender, DataEventArgs e)
        {
            Console.WriteLine($"处理程序2收到数据: {e.Data}");
        }
    }

    // 示例：使用反射动态访问类型
    public class ReflectionExample
    {
        public static void DemonstrateReflection()
        {
            string className = "CS1.Student";

            // 获取类型
            Type studentType = Type.GetType(className);
            if (studentType == null)
            {
                Console.WriteLine($"找不到类型: {className}");
                return;
            }

            // 创建实例
            object studentInstance = Activator.CreateInstance(studentType);

            // 设置属性值
            var nameProperty = studentType.GetProperty("Name");
            if (nameProperty != null)
            {
                nameProperty.SetValue(studentInstance, "张三");
            }

            var ageProperty = studentType.GetProperty("Age");
            if (ageProperty != null)
            {
                ageProperty.SetValue(studentInstance, 20);
            }

            // 调用方法
            var toStringMethod = studentType.GetMethod("ToString");
            if (toStringMethod != null)
            {
                string result = (string)toStringMethod.Invoke(studentInstance, null);
                Console.WriteLine($"ToString方法返回: {result}");
            }

            // 使用反射遍历类型的所有属性
            Console.WriteLine("\n类型属性:");
            foreach (var prop in studentType.GetProperties())
            {
                var value = prop.GetValue(studentInstance);
                Console.WriteLine($"{prop.Name}: {value}");
            }
        }
    }

    // 示例：使用C#中的特性(Attributes)
    [Serializable]
    [Obsolete("这个类将在未来版本移除，请使用NewPerson类")]
    public class Person
    {
        [JsonPropertyName("full_name")]
        public string Name { get; set; }

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonIgnore]
        public string PrivateInfo { get; set; }
    }

    // 自定义特性示例
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public class AuthorAttribute : Attribute
    {
        public string Name { get; }
        public string Email { get; }

        public AuthorAttribute(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }

    // 应用自定义特性
    [Author("张三", "zhang@example.com")]
    public class Library
    {
        [Author("李四", "li@example.com")]
        public void SomeMethod()
        {
            // 方法实现
        }
    }
}