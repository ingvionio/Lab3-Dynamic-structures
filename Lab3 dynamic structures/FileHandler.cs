using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab3_dynamic_structures
{
    public class QueueAdapter<T> : IDynamicStruct<string>
    {
        private Queue<string> _queue;

        public QueueAdapter()
        {
            _queue = new Queue<string>();
        }

        public void Push(string item)
        {
            _queue.Enqueue(item); // Добавление элемента в очередь
        }

        public string Pop()
        {
            return _queue.Count > 0 ? _queue.Dequeue() : null; // Удаление элемента из начала очереди
        }

        public string Peek()
        {
            return _queue.Count > 0 ? _queue.Peek() : null; // Просмотр элемента в начале очереди
        }

        public bool IsEmpty()
        {
            return _queue.Count == 0; // Проверка, пуста ли очередь
        }

        public void Print()
        {
            if (_queue.Count == 0)
            {
                Console.WriteLine("Queue is empty");
            }
            else
            {
                Console.WriteLine("Queue contents:");
                foreach (var item in _queue)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
    public class StackAdapter<T> : IDynamicStruct<string>
    {
        private Stack<string> _stack;

        public StackAdapter()
        {
            _stack = new Stack<string>();
        }

        public void Push(string item)
        {
            _stack.Push(item);
        }

        public string Pop()
        {
            return _stack.Count > 0 ? _stack.Pop() : null;
        }

        public string Peek()
        {
            return _stack.Count > 0 ? _stack.Peek() : null;
        }

        public bool IsEmpty()
        {
            return _stack.Count == 0;
        }

        public void Print()
        {
            if (_stack.Count == 0)
            {
                Console.WriteLine("Stack is empty");
            }
            else
            {
                Console.WriteLine("Stack contents:");
                foreach (var item in _stack)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
    public class FileHandler
    {
        public static string[] FileParse(string filePath)
        {
            string content = File.ReadAllText(filePath);

            string[] splitedString = content.Split(' ');
            return splitedString;
        }

        public static void HandleFile(string filePath, IDynamicStruct<string> structure)
        {
            string[] data = FileParse(filePath);

            foreach (string str in data)
            {
                string structName = structure.GetType().Name.ToString();
                structName = structName.Substring(0, structName.Length - 2);
                string result = Regex.Replace(str, @"\s", "");
                switch (result.Substring(0, 1))
                {
                    case "1":
                        Console.WriteLine("Элемент \"" + str.Substring(2) + "\" добавлен в " + structName);
                        structure.Push(str.Substring(2));
                        break;
                    case "2":
                        var pop = structure.Pop();
                        Console.WriteLine("Элемент \"" + pop + "\" убран из " + structName);
                        break;
                    case "3":
                        var top = structure.Peek();
                        Console.WriteLine("Первый элемент - " + top);
                        break;
                    case "4":
                        if (structure.IsEmpty())
                        {
                            Console.WriteLine(structName + " Пусто");
                        }
                        else
                        {
                            Console.WriteLine(structName + " Не пусто");
                        }
                        break;
                    case "5":
                        structure.Print();
                        break;
                    default:
                        Console.Write("Некорректная команда " + str);
                        Console.WriteLine();
                        break;
                }
            }
        }

        public static List<TimeSpan> HandleDirectory(string directoryPath, IDynamicStruct<string> structure)
        {
            if (!Directory.Exists(directoryPath))
            {
                //Console.WriteLine("Указанная директория не существует.");
                return new List<TimeSpan>(); // Возвращаем пустой список
            }

            List<TimeSpan> executionTimes = new List<TimeSpan>();
            string[] files = Directory.GetFiles(directoryPath);
            Stopwatch time = new Stopwatch();

            foreach (string filePath in files)
            {

                time.Start(); // Начало замера времени
                HandleFile(filePath, structure);
                time.Stop(); // Окончание замера времени

                TimeSpan elapsedTime = time.Elapsed;
                executionTimes.Add(elapsedTime); // Сохраняем время выполнения
                time.Reset();

            }

            return executionTimes;
        }
    }
}
