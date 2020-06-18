using System;

namespace lab9
{
    public delegate double Eqtn(double x);

    public class Program
    {
        static readonly double epsilon = 1e-10;

        static double f(double x)
        {
            return x * Math.Cos(x) - Math.Sin(x);
        }

        static void Main(string[] args)
        {
            var segment = Tuple.Create(-10.0, -12.0);
            var es = new EquationSolver();
            var sol = default(double);

            try
            {
                sol = es.DichotomyMethod(segment, f, epsilon);
                Console.WriteLine($"x = {sol}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.ReadKey();
        }
    }
}
