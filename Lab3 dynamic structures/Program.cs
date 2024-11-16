using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.Text;


namespace Lab3_dynamic_structures
{
    class Program
    {
        // ... (Existing code for LinkedList and related functions)
        static LinkedList<int> currentList = new LinkedList<int>();
        static int selectedMenuItem = 0;
        static string[] menuItems = {
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
            "Вставить элемент в отсортированный список",
            "Вставить элемент F перед элементом E",
            "Разделить список по элементу",
            "Поменять местами два элемента",
            "Выход"
        };

        static int mainMenuSelected = 0;
        static string[] mainMenuItems = { "Работа со стеком/очередью", "RPN калькулятор", "Примеры использования структур", "Работа со связным списком", "Выход" };



        static void Main(string[] args)
        {
            while (true)
            {
                DisplayMainMenu();
                HandleMainMenuInput();
            }
        }





        // --- Main Menu ---



        static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Главное меню:");

            for (int i = 0; i < mainMenuItems.Length; i++)
            {
                if (i == mainMenuSelected)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine(mainMenuItems[i]);
                Console.ResetColor();
            }
        }

        static void HandleMainMenuInput()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    mainMenuSelected = (mainMenuSelected - 1 + mainMenuItems.Length) % mainMenuItems.Length;
                    DisplayMainMenu();
                    break;
                case ConsoleKey.DownArrow:
                    mainMenuSelected = (mainMenuSelected + 1) % mainMenuItems.Length;
                    DisplayMainMenu();
                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    ExecuteMainMenuItem(mainMenuSelected);
                    Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
                    Console.ReadKey(true); // Removed to allow direct return to main menu
                    break;
            }
        }

        static void ExecuteMainMenuItem(int selectedItem)
        {
            switch (selectedItem)
            {
                case 0:
                    StackQueueMenu();
                    break;
                case 1:
                    RpnCalculatorMenu();
                    break;
                case 2:
                    DataStructureExamplesMenu();
                    break;
                case 3:
                    LinkedListMenu(); // Existing LinkedList Menu
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        }


        // --- Stack/Queue Menu ---

        static void StackQueueMenu()
        {
            int selectedItem = 0;
            string[] menuItems = { "Стек", "Очередь", "Назад" };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите структуру данных:");
                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (i == selectedItem)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine(menuItems[i]);
                    Console.ResetColor();
                }

                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedItem = (selectedItem - 1 + menuItems.Length) % menuItems.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedItem = (selectedItem + 1) % menuItems.Length;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        if (selectedItem == menuItems.Length - 1) return; // "Back" selected

                        IDynamicStruct<string> dynamicStruct = null;
                        if (selectedItem == 0) dynamicStruct = new MyStack<string>();
                        else if (selectedItem == 1) dynamicStruct = new MyQueue<string>();


                        Console.Write("Введите путь к файлу или директории: ");
                        string path = Console.ReadLine();

                        if (File.Exists(path))
                        {
                            try
                            {
                                string[] commands = File.ReadAllText(path).Split(' ');
                                foreach (string command in commands)
                                {
                                    ProcessCommand(dynamicStruct, command);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        else if (Directory.Exists(path))
                        {
                            try
                            {
                                string[] files = Directory.GetFiles(path);
                                List<TimeSpan> executionTimes = new List<TimeSpan>();
                                Stopwatch stopwatch = new Stopwatch();

                                foreach (string file in files)
                                {
                                    stopwatch.Start();
                                    string[] commands = File.ReadAllText(file).Split(' ');
                                    foreach (string command in commands)
                                    {
                                        ProcessCommand(dynamicStruct, command);
                                    }
                                    stopwatch.Stop();
                                    executionTimes.Add(stopwatch.Elapsed);
                                    stopwatch.Reset();
                                }

                                Console.WriteLine("\nВремя выполнения для каждого файла:");
                                for (int i = 0; i < executionTimes.Count; i++)
                                {
                                    Console.WriteLine($"Файл {i + 1}: {executionTimes[i]}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Указанный файл или директория не существует.");
                        }


                        //Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
                        //Console.ReadKey(true);
                        return;
                }
            }
        }





        static void ProcessCommand(IDynamicStruct<string> struct1, string command)
        {

            string structName = struct1.GetType().Name.ToString();
            structName = structName.Substring(0, structName.Length - 2);
            string result = command;
            result = result.Replace(",", "");
            switch (result.Substring(0, 1))
            {

                case "1":
                    if (command.Length >= 2)
                    {
                        Console.WriteLine("Элемент \"" + command.Substring(2) + "\" добавлен в " + structName);

                        struct1.Push(command.Substring(2));
                    }


                    break;
                case "2":

                    var pop = struct1.Pop();
                    Console.WriteLine("Элемент \"" + pop + "\" убран из " + structName);

                    break;

                case "3":

                    var top = struct1.Peek();
                    Console.WriteLine("Первый элемент - " + top);

                    break;
                case "4":
                    if (struct1.IsEmpty())
                    {
                        Console.WriteLine(structName + " Пусто");
                    }
                    else
                    {
                        Console.WriteLine(structName + " Не пусто");
                    }
                    break;
                case "5":
                    struct1.Print();
                    break;

                default:
                    Console.Write("Некорректная команда " + command);
                    Console.WriteLine();
                    break;
            }

        }


        // --- RPN Calculator Menu ---

        static void RpnCalculatorMenu()
        {


            Console.WriteLine("Введите выражение в инфиксной нотации (например, 2+2):");
            string infixExpression = Console.ReadLine();

            try
            {
                // Преобразование в постфиксную нотацию (RPN)
                List<Token> rpnTokens = RpnCalculator.ToRPN(RpnCalculator.Parse(infixExpression));

                Console.WriteLine("Выражение в постфиксной нотации (RPN):");
                foreach (var token in rpnTokens)
                {
                    Console.Write($"{token} ");
                }
                Console.WriteLine();

                // Вычисление выражения
                double[] variableValues = { }; // Если есть переменные, запросите их значения у пользователя
                double result = RpnCalculator.Calculate(rpnTokens, variableValues);

                Console.WriteLine($"Результат: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

        }

        // --- Data Structure Examples Menu ---
        static void DataStructureExamplesMenu()
        {
                int selectedItem = 0;
                string[] examples = {
            "Список (List): Плейлист музыкальных композиций",
            "Стек (Stack): История браузера",
            "Очередь (Queue): Очередь печати документов",
            "Дерево (Heap): Сортировка кучей (Heap Sort)",
            "Назад"
                };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Примеры использования структур данных:");
                for (int i = 0; i < examples.Length; i++)
                {
                    if (i == selectedItem)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine(examples[i]);
                    Console.ResetColor();
                }


                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedItem = (selectedItem - 1 + examples.Length) % examples.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedItem = (selectedItem + 1) % examples.Length;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        if (selectedItem == examples.Length - 1) return; // "Back" selected

                        switch (selectedItem)
                        {
                            case 0: PlaylistExample(); break;
                            case 1: BrowserHistoryExample(); break;
                            case 2: PrintQueueExample(); break;
                            case 3: HeapSortExample(); break;
                        }

                        //Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
                        //Console.ReadKey(true);
                        return;

                }
            }
        }


        // Data Structure Examples implementation here
        //1. List: Music Playlist
        static void PlaylistExample()
        {
            Console.WriteLine("Пример использования списка (List):");
            Console.WriteLine("Список используется для хранения коллекции песен в плейлисте.\n" +
                              "List обеспечивает быстрый доступ к песням по индексу, \n" +
                              "а также эффективное добавление и удаление песен.");


            Playlist playlist = new Playlist();
            playlist.AddSong(new Song("Bohemian Rhapsody", "Queen"));
            playlist.AddSong(new Song("Stairway to Heaven", "Led Zeppelin"));
            playlist.AddSong(new Song("Imagine", "John Lennon"));

            Console.WriteLine("\nТекущий плейлист:");
            playlist.DisplaySongs();

            Song songToPlay = new Song("Stairway to Heaven", "Led Zeppelin");
            Console.WriteLine($"\nВоспроизведение песни: {songToPlay}");
            playlist.PlaySong(songToPlay);

            // ... (Demonstrate adding/removing songs if needed)
        }



        //2. Stack: Browser History
        static void BrowserHistoryExample()
        {
            Console.WriteLine("Пример использования стека (Stack):");
            Console.WriteLine("Стек используется для хранения истории посещенных страниц в браузере.\n" +
                              "Последняя посещенная страница находится на вершине стека.\n" +
                              "Операция \"Назад\" извлекает страницу из стека.");


            BrowserHistory browserHistory = new BrowserHistory("google.com");
            browserHistory.Visit("yandex.ru");
            browserHistory.Visit("github.com");

            Console.WriteLine($"\nТекущая страница: {browserHistory.Current()}");
            Console.WriteLine($"Назад: {browserHistory.Back(1)}");
            Console.WriteLine($"Назад: {browserHistory.Back(1)}");
            Console.WriteLine($"Вперед: {browserHistory.Forward(1)}");
            Console.WriteLine($"Вперед: {browserHistory.Forward(1)}");

        }



        //3. Queue: Print Queue
        static void PrintQueueExample()
        {
            Console.WriteLine("Пример использования очереди (Queue):");
            Console.WriteLine("Очередь используется для управления заданиями на печать.\n" +
                              "Задания добавляются в конец очереди и обрабатываются в порядке FIFO (первым пришел - первым обслужен).");


            PrintQueue printQueue = new PrintQueue();


            printQueue.AddJob(new PrintJob("Document1", 5));
            printQueue.AddJob(new PrintJob("Image1", 1));
            printQueue.AddJob(new PrintJob("Presentation1", 10));


            Console.WriteLine("\nОбработка очереди печати:");
            printQueue.ProcessQueue();
        }



        //4. Heap (Tree): Heap Sort
        static void HeapSortExample()
        {
            Console.WriteLine("Пример использования дерева (Heap - куча):");
            Console.WriteLine("Куча (двоичная куча) - это специальное дерево, \n" +
                              "которое используется в алгоритме сортировки Heap Sort.\n" +
                              "Heap Sort имеет сложность O(n log n).");



            int[] arr = { 12, 11, 13, 5, 6, 7 };
            BinaryTree ob = new BinaryTree();

            foreach(var item in arr)
            {
                ob.Insert(item);
            }

            Console.WriteLine("\nИсходный массив:");
            Array.ForEach(arr, x => Console.Write(x + " "));




            Console.WriteLine("\nОтсортированный массив:");
            ob.HeapSortTraversal();
            Console.WriteLine("\nОбход кучи в глубину:");
            ob.DFSTraversal();
        }

        // ... (other menu functions)

        static void LinkedListMenu()
        {
            int selectedItem = 0;

            while (true)
            {
                DisplayLinkedListMenu(selectedItem); // Pass selectedItem for highlighting

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedItem = (selectedItem - 1 + menuItems.Length) % menuItems.Length;
                        break; // No need to call DisplayMenu again here

                    case ConsoleKey.DownArrow:
                        selectedItem = (selectedItem + 1) % menuItems.Length;
                        break; // No need to call DisplayMenu again here


                    case ConsoleKey.Enter:
                        Console.Clear();
                        if (selectedItem == menuItems.Length - 1)
                        {
                            return; // Exit LinkedListMenu and return to Main Menu
                        }
                        ExecuteMenuItem(selectedItem);
                        Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                        Console.ReadKey(true);
                        break;
                }
            }
        }
        static void DisplayLinkedListMenu(int selectedItem) // Modified function
        {
            Console.Clear();
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedItem) // Highlight selected item
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine(menuItems[i]);
                Console.ResetColor();
            }
        }


        // ... (Existing code for linked list operations)
     
        static void ExecuteMenuItem(int selectedItem)
        {
            string[] menuActions =
            {
                "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "0" // Добавлены действия
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
                case "13":
                    InsertSorted();
                    break;
                case "14":
                    InsertBefore();
                    break;
                case "15":
                    SplitList();
                    break;
                case "16":
                    SwapElements();
                    break;
                case "0":
                    return; // Возврат в главное меню
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
        static void InsertSorted()
        {
            Console.Write("Введите элемент для вставки: ");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                currentList.InsertSorted(value);
                Console.WriteLine("Элемент вставлен.");
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }


        static void InsertBefore()
        {
            Console.Write("Введите элемент F для вставки: ");
            if (!int.TryParse(Console.ReadLine(), out int f))
            {
                Console.WriteLine("Некорректный ввод F.");
                return;
            }

            Console.Write("Введите элемент E, перед которым нужно вставить F: ");
            if (!int.TryParse(Console.ReadLine(), out int e))
            {
                Console.WriteLine("Некорректный ввод E.");
                return;
            }

            currentList.InsertBeforeFirstOccurrence(f, e);
            Console.WriteLine($"Элемент {f} вставлен перед {e}.");
        }

        static void SplitList()
        {
            Console.Write("Введите элемент, по которому нужно разделить список: ");
            if (int.TryParse(Console.ReadLine(), out int target))
            {
                LinkedList<int> secondList = currentList.Split(target);
                Console.WriteLine("Список разделен.");
                Console.WriteLine("Первый список:");
                currentList.Print();
                Console.WriteLine("Второй список:");
                secondList.Print();

            }

            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }


        static void SwapElements()
        {
            Console.Write("Введите первый элемент для обмена: ");
            if (!int.TryParse(Console.ReadLine(), out int element1))
            {
                Console.WriteLine("Некорректный ввод первого элемента.");
                return;
            }

            Console.Write("Введите второй элемент для обмена: ");
            if (!int.TryParse(Console.ReadLine(), out int element2))
            {
                Console.WriteLine("Некорректный ввод второго элемента.");
                return;
            }

            currentList.SwapElements(element1, element2);
            Console.WriteLine("Элементы поменяны местами.");
        }

    }

}