using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab3_dynamic_structures
{
    public class FileHandler
    {
        public static string[] FileParse(string filePath)
        {
            string content = File.ReadAllText(filePath);

            string[] splitedString = content.Split(' ');
            return splitedString;
        }

        public static void Do(string filePath, IDynamicStruct<string> structure)
        {
            string[] data = FileParse(filePath);

            foreach (string str in data)
            {
                string structName = structure.GetType().Name.ToString();
                structName = structName.Substring(0, structName.Length - 2);
                string result = Regex.Replace(str, @"\s", "");
                switch (result.Substring(0,1))
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
                        Console.WriteLine("Первый элемент - " + top );
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
    }
}
