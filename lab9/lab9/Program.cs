using System;

namespace lab9
{
    class Program
    {
        static void Main(string[] args)
        {
            const double epsilon = 1e-10;

            double sol = dichotomy_method(1, 5, epsilon);

            Console.WriteLine($"x = {sol}");
            Console.ReadKey();
        }


        static double f(double x)
        {
            return x * x - 4 * x;
        }


        static double dichotomy_method(double a, double b, double eps)
        {
            double c, ans;

            while (b - a > eps)
            { 
                c = (a + b) / 2;

                if (f(a) * f(c) < 0)
                {
                    b = c;
                }
                else if (f(a)*f(c) > 0)
                { 
                    a = c;
                }
                else
                { 
                    return c;
                }
            }

            return a;
        }
    }
}
