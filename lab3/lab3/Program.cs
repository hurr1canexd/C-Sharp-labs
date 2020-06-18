using System;
using System.Linq;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите числа: ");
            string[] arr = Console.ReadLine().Replace('.', ',').Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == 0)
            {
                Console.WriteLine("Пустая строка");
                Console.ReadKey();
                return;
            }

            #region task A
            //double avg = arr.Select(x => double.Parse(x)).Average();
            double avg = 0;
            int n = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (!double.TryParse(arr[i], out var curNumber))
                {
                    continue; // игнорирую не числа
                }
                avg += curNumber;
                n++;
            }

            if (n != 0)
            {
                avg /= n;
                Console.WriteLine($"Среднее арифметическое: {avg}");
            }
            else
            {
                Console.WriteLine("Чисел нет");
                Console.ReadKey();
                return;
            }
                
            #endregion

            #region task B
            double geometricMean = 1;
            n = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                double curNumber;
                bool ok = double.TryParse(arr[i], out curNumber);

                if (!ok)
                {
                    Console.WriteLine($"На {i+1}-й позиции не число.");
                    continue; // игнорирую не числа
                }
                
                geometricMean *= curNumber;
                n++;
            }

            if (n % 2 == 0)
            {
                if (geometricMean > 0)
                {
                    geometricMean = Math.Pow(geometricMean, 1.0 / n);
                }
                else
                {
                    Console.WriteLine("Корень чётной степени из отрицательного числа");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                geometricMean = (geometricMean > 0) ? Math.Pow(geometricMean, 1.0 / n) : -Math.Pow(-geometricMean, 1.0 / n);
            }

            Console.WriteLine($"Среднее геометрическое: {geometricMean}");
            #endregion

            Console.ReadKey();
        }
    }
}
