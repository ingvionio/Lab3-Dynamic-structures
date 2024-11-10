using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab3_dynamic_structures
{
    class Program
    {
        static LinkedList<int> currentList = new LinkedList<int>();
        static int selectedMenuItem = 0;
        static string[] menuItems = { // Объявление menuItems вне функций
                "Создать новый список",
                "Загрузить список из файла",
                "Перевернуть список",
                "Посчитать уникальные элементы",
                "Вставить список в себя",
                "Удалить все элементы E",
                "Добавить список к текущему",
                "Удвоить список",
                "Вывести список",
                "Переместить первый элемент в конец", 
                "Переместить последний элемент в начало",
                "Удалить неуникальные элементы",
                "Выход"
            };

        public static void Main(string[] args)
        {
            while (true)
            {
                DisplayMenu();
                HandleInput();
            }
        }

        static void DisplayMenu()
        {
            Console.Clear();

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedMenuItem)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine(menuItems[i]);
                Console.ResetColor();
            }
        }

        static void HandleInput()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedMenuItem = (selectedMenuItem - 1 + menuItems.Length) % menuItems.Length;
                    DisplayMenu(); // Перерисовать меню после изменения selectedMenuItem
                    break;
                case ConsoleKey.DownArrow:
                    selectedMenuItem = (selectedMenuItem + 1) % menuItems.Length;
                    DisplayMenu(); // Перерисовать меню после изменения selectedMenuItem
                    break;
                case ConsoleKey.Enter:
                    Console.Clear(); // Очистить консоль после выбора пункта меню
                    ExecuteMenuItem(selectedMenuItem);
                    Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
                    Console.ReadKey(true); // Ожидание нажатия любой клавиши
                    break;
            }
        }


        static void ExecuteMenuItem(int selectedItem)
        {
            string[] menuActions =
             {
                "1","2","3","4","5","6","7","8","9","10","11","12","0" // Добавлены действия для новых пунктов
            };

            string input = menuActions[selectedItem];


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
                case "10":
                    MoveFirstToLast();
                    break;
                case "11":
                    MoveLastToFirst();
                    break;
                case "12":
                    RemoveNonUnique();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Некорректный ввод.");
                    break;

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
        static void MoveFirstToLast()
        {
            if (currentList.IsEmpty)
            {
                Console.WriteLine("Список пуст!");
                return;
            }
            currentList.MoveFirstToLast();
            Console.WriteLine("Первый элемент перемещен в конец.");
        }

        static void MoveLastToFirst()
        {
            if (currentList.IsEmpty)
            {
                Console.WriteLine("Список пуст!");
                return;
            }
            currentList.MoveLastToFirst();
            Console.WriteLine("Последний элемент перемещен в начало.");
        }

        static void RemoveNonUnique()
        {
            if (currentList.IsEmpty)
            {
                Console.WriteLine("Список пуст!");
                return;
            }
            currentList.RemoveNonUniqueElements();
            Console.WriteLine("Неуникальные элементы удалены.");
        }
    }
}