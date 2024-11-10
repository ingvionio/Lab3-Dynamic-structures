using System;


namespace Lab3_dynamic_structures
{
    class Program
    {
        public static void Main()
        {
            /*string expression = "";
            var list = RpnCalculator.Parse(expression);

            list = RpnCalculator.ToRPN(list);

            foreach (var item in list)
            {
                Console.Write(item.ToString() + " ");
            }

            Console.WriteLine(RpnCalculator.Calculate(list));*/

            // Input case:
            string homepage = "gfg.org";

            // Initialize the object of BrowserHistory
            BrowserHistory obj = new BrowserHistory(homepage);

            string url = "google.com";
            obj.Visit(url);

            url = "facebook.com";
            obj.Visit(url);

            url = "youtube.com";
            obj.Visit(url);

            Console.WriteLine(obj.Back(1));
            Console.WriteLine(obj.Back(1));
            Console.WriteLine(obj.Forward(1));
            obj.Visit("linkedin.com");
            Console.WriteLine(obj.Forward(2));
            Console.WriteLine(obj.Back(2));
            Console.WriteLine(obj.Back(7));
        }
    }
}