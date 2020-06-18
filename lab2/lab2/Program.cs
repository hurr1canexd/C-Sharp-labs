using System;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            var clcltn = new Calculation();
            var e = clcltn.CalculateEulerNumber();
            var pi = clcltn.CalculatePi();
            var ln_2 = clcltn.CalculateLnTwo();

            Console.WriteLine($"e = {e}\npi = {pi}\nln_2 = {ln_2}");
            Console.ReadKey();
        }
       
    }
}
