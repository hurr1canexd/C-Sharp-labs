using System;
using System.Text;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Incorrect input.");
                return;
            }

            bool isNum = int.TryParse(args[0], out var oldNum);
            bool ok = int.TryParse(args[1], out var newBase);

            if (!isNum || !ok)
            {
                Console.WriteLine("Incorrect input.");
                Console.ReadKey();
                return;
            }

            try
            {
                string newNum = InvertedGorner(oldNum, newBase);
                Console.WriteLine($"{oldNum}(10) = {newNum}({newBase})");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }


        static string InvertedGorner(int num, int toBase)
        {
            if (num == 0)
            {
                return "0";
            }
            
            if (num < 0)
            {
                return "-" + InvertedGorner(Math.Abs(num), toBase);
            }
                
            if (toBase < 1 && toBase > 36)
            {
                throw new ArgumentException("The new base is not between 2 and 36", nameof(toBase));
            }
            StringBuilder res = new StringBuilder();
            int remainder;

            while (num != 0)
            {
                remainder = num % toBase;
                if (remainder < 10)
                {
                    remainder += '0';
                }
                else
                {
                    remainder += 'A' - 10;
                }
                res.Insert(0, (char)remainder); // вставляем в начало
                num = num / toBase;
            }
            return res.ToString();
        }
        
    }
}
