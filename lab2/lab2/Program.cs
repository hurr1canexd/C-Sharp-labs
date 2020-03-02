using System;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            double e = calculateEulerNumber();
            double pi = calculatePi();
            double ln_2 = calculateLnTwo();

            Console.WriteLine($"e = {e}\npi = {pi}\nln_2 = {ln_2}");
            Console.ReadKey();
        }


        static double calculateEulerNumber()
        {
            double e = 1;
            long fact = 1; // будем хранить значение, чтобы не высчитывать на каждой итерации факториал, а лишь выполнять одну операци умножения

            for (int i = 1; i < 8; i++)
            {
                fact *= i;
                e += 1.0 / fact;
            }

            return e;
        }


        static double calculatePi()
        {
            double pi = 0;
            int denom = 1; // будем хранить знаменатель, чтобы делать 1 операцию умножения вместо 2 (умножения и вычитания) на каждой итерации 

            for (int i = 0; i < 3000; i++)
            {
                if (i % 2 == 0)
                {
                    pi += 4.0 / denom;
                }
                else
                {
                    pi -= 4.0 / denom;
                }
                denom += 2;
            }

            return pi;
        }


        static double calculateLnTwo()
        {
            double ln_2 = 0;            
            int degreeOfThree = 3; // будем хранить значение степени тройки, чтобы на каждой итерации не высчитывать снова, а просто домножать на тройку
            
            for (int i = 1; i < 100; i++)
            {
                ln_2 += 2.0 / ((2 * i - 1) * degreeOfThree);
                degreeOfThree *= 9;
            }

            return ln_2;
        }
    }
}
