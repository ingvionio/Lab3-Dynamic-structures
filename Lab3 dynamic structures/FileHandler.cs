using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            foreach (string s in data)
            {
                switch (s.Substring(0,1))
                {
                    case "1":
                        Console.WriteLine("1");
                        structure.Push(s.Substring(2));
                        break;
                    case "2":
                        Console.WriteLine("2");
                        structure.Pop();
                        break;
                    case "3":
                        Console.WriteLine("3");
                        structure.Top();
                        break;
                    case "4":
                        Console.WriteLine("4");
                        structure.IsEmpty();
                        break;
                    case "5":
                        Console.WriteLine("5");
                        structure.Print();
                        break;
                    default:
                        Console.WriteLine("Некорректная команда" + s);
                        break;
                }
            }
        }
    }
}
