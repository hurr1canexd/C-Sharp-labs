using System;
using System.Collections.Generic;
using System.Text;

namespace lab2
{
    public class Calculation
    {
        private readonly double eps = 1e-15;

        public double CalculateEulerNumber()
        {
            double e = default(double);
            long fact = 1; // будем хранить значение, чтобы не высчитывать на каждой итерации факториал, а лишь выполнять одну операцию умножения

            /*
            for (int i = 1; i < 8; i++)
            {
                fact *= i;
                e += 1.0 / fact;
            }
            */

            int i = 1;
            double next = 1.0;
            while (next > eps)
            {
                fact *= i;
                e += next;
                i++;
                next = 1.0 / fact;
            }

            return e;
        }


        public double CalculatePi()
        {
            double sum = 0;
            //int denom = 1; // будем хранить знаменатель, чтобы делать 1 операцию умножения вместо 2 (умножения и вычитания) на каждой итерации 

            /*
            for (int i = 0; i < 3000; i++)
            {
                if (i % 2 == 0)
                {
                    sum += 4.0 / denom;
                }
                else
                {
                    sum -= 4.0 / denom;
                }
                denom += 2;
            }
            */

            double next = 1.0;
            int n = 1, minus = 1;
            while (next > eps)
            {
                sum += next * minus;
                minus *= -1;
                n++;
                next = 1.0 / (2 * n - 1);
            }

            return sum * 4;
        }


        public double CalculateLnTwo()
        {
            double ln_2 = 0;
            int degreeOfThree = 3, i = 1; // будем хранить значение степени тройки, чтобы на каждой итерации не высчитывать снова, а просто домножать на тройку
            double plus = 2.0 / 3;

            /*
            for (int i = 1; i < 100; i++)
            {
                ln_2 += 2.0 / ((2 * i - 1) * degreeOfThree);
                degreeOfThree *= 9;
            }
            */

            while (plus > eps)
            {
                ln_2 += plus;   
                degreeOfThree *= 9;
                i++;
                plus = 2.0 / ((2 * i - 1) * degreeOfThree);
            }

            return ln_2;
        }
    }
}
