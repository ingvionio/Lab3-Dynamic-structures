using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab3_dynamic_structures
{
    class Program
    {
        static LinkedList<int> currentList = new LinkedList<int>();

        public static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Создать новый список");
                Console.WriteLine("2. Загрузить список из файла");
                Console.WriteLine("3. Перевернуть список");
                Console.WriteLine("4. Посчитать уникальные элементы");
                Console.WriteLine("5. Вставить список в себя");
                Console.WriteLine("6. Удалить все элементы E");
                Console.WriteLine("7. Добавить список к текущему");
                Console.WriteLine("8. Удвоить список");
                Console.WriteLine("9. Вывести список");
                Console.WriteLine("0. Выход");


                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateNewList();
                        break;
                    case "2":
                        LoadListFromFile();
                        break;
                    case "3":
                        currentList.Reverse();
                        Console.WriteLine("Список перевернут.");
                        break;
                    case "4":
                        Console.WriteLine($"Уникальных элементов: {currentList.CountDistinct()}");
                        break;
                    case "5":
                        InsertListIntoItself();
                        break;
                    case "6":
                        RemoveAllElements();
                        break;
                    case "7":
                        AppendList();
                        break;
                    case "8":
                        currentList.Double();
                        Console.WriteLine("Список удвоен.");
                        break;
                    case "9":
                        Console.WriteLine("Список:");
                        currentList.Print();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод.");
                        break;
                }
            }
        }


        static void CreateNewList()
        {
            currentList = new LinkedList<int>();
            Console.WriteLine("Новый список создан.");
            AddElementsToList();
        }

        static void LoadListFromFile()
        {
            Console.Write("Введите путь к файлу: ");
            string filePath = Console.ReadLine();

            try
            {
                currentList = new LinkedList<int>();
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (int.TryParse(line, out int num))
                    {
                        currentList.Add(num);
                    }
                }
                Console.WriteLine("Список загружен из файла.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке файла: {ex.Message}");
            }
        }




        static void AddElementsToList()
        {
            Console.WriteLine("Вводите числа (для завершения введите пустую строку):");

            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) break;

                if (int.TryParse(input, out int num))
                {
                    currentList.Add(num);

                }


                else
                {
                    Console.WriteLine("Некорректный ввод. Введите число.");
                }
            }
        }

        static void InsertListIntoItself()
        {
            Console.Write("Введите число x после которого список должен быть вставлен: ");
            if (int.TryParse(Console.ReadLine(), out int x))
            {
                currentList.InsertIntoItself(x);
                Console.WriteLine("Список вставлен.");
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }


        static void RemoveAllElements()
        {
            Console.Write("Введите элемент E для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int e))
            {
                currentList.RemoveAll(e);
                Console.WriteLine("Элементы удалены.");
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }

        static void AppendList()
        {
            Console.WriteLine("Создайте новый список для добавления:");
            LinkedList<int> newList = new LinkedList<int>();
            AddElementsToListNew(newList);


            currentList.AppendList(newList);
            Console.WriteLine("Список добавлен.");
        }

        static void AddElementsToListNew(LinkedList<int> list)
        {
            Console.WriteLine("Вводите числа (для завершения введите пустую строку):");
            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) break;

                if (int.TryParse(input, out int num))
                {
                    list.Add(num);
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Введите число.");
                }
            }
        }

    }
}