using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace lab12
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("No argument");
                Environment.Exit(-1);
            }

            //Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\" + args[0];
            //Console.OutputEncoding = Encoding.Unicode;
            var fileName = args[0];

            var counter = TextHandler.CountWords(fileName);
            foreach (var elem in counter)
            {
                Console.WriteLine($"{elem.Key}: {elem.Value}");
            }

            Console.WriteLine("\nВведите слово, которое будем искать:");
            var word = Console.ReadLine();
            Console.WriteLine("Встречается раз: " + TextHandler.SearchWord(counter, word));


            var res = TextHandler.GetMostCommonWord(counter);
            Console.WriteLine("\nБольше всего раз встречаются:");
            foreach (var elem in res)
            {
                Console.WriteLine(elem);
            }
            Console.ReadKey();
        }
    }
}
