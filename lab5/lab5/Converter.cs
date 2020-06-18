using System;
using System.Collections.Generic;
using System.Text;

namespace lab5
{
    public class Converter
    {
        private static string ConvertIntPart(int num, int toBase)
        {
            decimal eps = (decimal)1e-10;

            if (Math.Abs(num) < eps)
            {
                return "0";
            }

            if (num < 0)
            {
                return "-" + ConvertIntPart(-num, toBase);
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


        private static string ConvertFractPart(decimal num, int toBase)
        {
            decimal eps = (decimal)1e-10;

            if (Math.Abs(num) < eps)
            {
                return "0";
            }

            var res = new StringBuilder();
            var list = new List<decimal>();
            var newNum = num * toBase;

            while (Math.Abs(newNum) > eps)
            {
                if (!list.Contains(newNum))
                {
                    list.Add(newNum);
                    if (decimal.Truncate(newNum) < 10)
                    {
                        res.Append((char)(decimal.Truncate(newNum) + '0'));
                    }
                    else
                    {
                        res.Append((char)(decimal.Truncate(newNum) + 'A' - 10));
                    }
                    newNum -= decimal.Truncate(newNum);
                    newNum *= toBase;
                }
                else
                {
                    var index = list.IndexOf(newNum);
                    res.Insert(index, "(");
                    res.Append(")");
                    return res.ToString();
                }
            }

            return res.ToString();
        }


        public static string Convert(decimal num, int toBase)
        {
            if (toBase < 2 || toBase > 36)
            {
                throw new ArgumentException("The new base is not between 2 and 36", nameof(toBase));
            }

            var intPart = (int)num;
            var fractPart = num % 1.0m;

            return $"{ConvertIntPart(intPart, toBase)}," +
                $"{ConvertFractPart(fractPart, toBase)}" +
                $"_{toBase}";
        }
    }
}
