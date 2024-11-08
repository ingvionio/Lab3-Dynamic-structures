using System;


namespace Lab3_dynamic_structures
{
    class Program
    {
        public static void Main()
        {
            string expression = "";
            var list = RpnCalculator.Parse(expression);

            list = RpnCalculator.ToRPN(list);

            foreach (var item in list)
            {
                Console.Write(item.ToString() + " ");
            }

            Console.WriteLine(RpnCalculator.Calculate(list));


        }
    }
}