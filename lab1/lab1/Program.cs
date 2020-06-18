using System;
using System.Numerics;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var coeffs = new double[4];
            coeffs = parseCoeffsFromCommandLine(args);
            if (coeffs == null)
            {
                Console.WriteLine("Коэффициенты введены некорректно.");
                Console.ReadKey();
                return;
            }

            var eq = new CubicEquation(coeffs);
            Console.WriteLine(eq);

            Console.WriteLine("\nМетод Кардано");
            var sol1 = eq.SolveByCardano();

            if (sol1 == null)
            {
                Console.WriteLine("Уравнение не кубическое");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < sol1.Length; i++)
            {
                Console.WriteLine($"x{i+1} = {sol1[i]}");
            }

            Console.WriteLine("\nМетод Виета-Кардано");
            var sol2 = eq.SolveByVietCardano();

            for (int i = 0; i < sol2.Length; i++)
            {
                Console.WriteLine($"x{i+1} = {sol2[i]}");
            }

            Console.ReadKey();
        }


        static double[] parseCoeffsFromCommandLine(string[] args)
        {
            if (args.Length < 4)
            {
                return null;
            }

            var coeffs = new double[4];

            for (var i = 0; i < 4; i++)
            {
                bool ok = Double.TryParse(args[i], out coeffs[i]);

                if (!ok)
                {
                    return null;
                }
            }
            
            return coeffs;
        }
    }
}
