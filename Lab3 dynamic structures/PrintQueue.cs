using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab3_dynamic_structures {
    public class PrintJob
    {
        public string DocumentName { get; set; }
        public int Pages { get; set; }

        public PrintJob(string documentName, int pages)
        {
            DocumentName = documentName;
            Pages = pages;
        }

        public override string ToString()
        {
            return $"{DocumentName} ({Pages} стр.)";
        }
    }

    public class PrintQueue
    {
        private Queue<PrintJob> printQueue = new Queue<PrintJob>();


        public void AddJob(PrintJob job)
        {
            printQueue.Enqueue(job);
            Console.WriteLine($"Задание '{job}' добавлено в очередь.");
        }


        public void ProcessQueue()
        {
            while (printQueue.Count > 0)
            {
                PrintJob currentJob = printQueue.Dequeue();
                Console.WriteLine($"Печать: {currentJob}");
                // Имитация печати (задержка)
                Thread.Sleep(currentJob.Pages * 500); // 500 мс на страницу
                Console.WriteLine($"Задание '{currentJob}' напечатано.");

            }

            Console.WriteLine("Очередь печати пуста.");
        }
    }
}
