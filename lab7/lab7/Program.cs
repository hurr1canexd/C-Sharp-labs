using System;

namespace lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите строку: ");
            string str = Console.ReadLine().Trim();

            if (str.Length == 0)
            {
                Console.WriteLine("Пустая строка");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Отсортированная строка: " + SortString(str));

            Console.ReadKey();
        }


        static string SortString(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Sort(chars);

            return new string(chars);
        }
    }
}
