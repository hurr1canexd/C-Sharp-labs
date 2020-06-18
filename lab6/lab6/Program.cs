using System;
using System.Linq;
using System.Collections;
using System.Text;

namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the string: ");
            string[] arr = Console.ReadLine().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == 0)
            {
                Console.WriteLine("No words.");
                Console.ReadKey();
                return;
            }

            // Вывод массива слов
            Console.WriteLine("\nBefore:");
            for (var i = 0; i < arr.Length; i++)
            {
                Console.WriteLine($"{i+1}. {arr[i]}");
            }

            // Сортировка слов в массиве
            for (var i = 0; i < arr.Length; i++)
            {
                arr[i] = SortString(arr[i]);
            }

            // Вывод массива отсортированных слов
            Console.WriteLine("\nAfter:");
            for (var i = 0; i < arr.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {arr[i]}");
            }

            // Составляем string builder из последних символов слов в массиве
            StringBuilder sb = new StringBuilder(); 
            for (var i = 0; i < arr.Length; i++)
            {
                sb.Append((arr[i])[arr[i].Length - 1]);
            }
            Console.WriteLine($"\n{sb.ToString()}");

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
