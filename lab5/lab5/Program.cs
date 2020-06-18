using System;
using System.IO;

namespace lab5
{
    public sealed class Program
    {
        static void Main(string[] args)
        {
            decimal num = default(decimal);
            int sys = default(int);

            if (args.Length < 1)
            {
                Console.WriteLine("Введите число: ");
                bool isDecimal = decimal.TryParse(Console.ReadLine().Replace('.', ','), out num);
                Console.WriteLine("Введите систему счисления: ");
                bool isInt = int.TryParse(Console.ReadLine(), out sys);
            }
            else // C:\\Users\\tatyana\\temp.txt
            {
                try
                {
                    using (var sr = new StreamReader(args[0]))
                    {
                        bool isDecimal = decimal.TryParse(sr.ReadLine(), out num);
                        bool isInt = int.TryParse(sr.ReadLine(), out sys);
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    Environment.Exit(-1);
                }
            }

            try
            {
                var res = Converter.Convert(num, sys);
                Console.WriteLine($"{num}_10 = {res}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Environment.Exit(-2);
            }

            Console.ReadKey();
        }
    }
}
