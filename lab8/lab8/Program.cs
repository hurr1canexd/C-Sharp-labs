using System;
using System.Text.RegularExpressions;
using System.Text;

namespace lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "аккка dlkfdfkg dlfg dg fdg idfg- dg   dg gdg f-0 dg-0d     -dfgd0df-g-f0-0gf";
            Console.WriteLine(str);
            string[] words = str.Split(' ', StringSplitOptions.RemoveEmptyEntries); // записываю слова, разделённые пробелами, в массив
            if (words.Length == 0)
            {
                Console.WriteLine("Слов нет.");
                Console.ReadKey();
                return;
            }

            #region task A
            string need = "Я".Trim(); // искомое слово
            int counter = 0;
            
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Equals(need))
                    counter++;
            }

            //counter = Regex.Matches(str, need).Count;

            Console.WriteLine($"Слов \"{need}\" в строке найдено: {counter}\n");
            #endregion


            #region task B
            if (words.Length > 2)
            {
                StringBuilder sb = new StringBuilder();
                words[words.Length - 2] = need;

                for (int i = 0; i < words.Length; i++)
                {
                    sb.Append($"{words[i]} ");
                }

                Console.WriteLine($"Строка после замены предпоследнего слова:\n{sb.ToString()}\n");
            }
            else
            {
                Console.WriteLine("Меньше 2 слов.\n");
            }
            #endregion


            #region task C
            int k;
            Console.Write("Введите число: ");
            bool isNum = int.TryParse(Console.ReadLine(), out k);

            if (!isNum || k > words.Length || k < 1)
            {
                Console.WriteLine("Некорректное число");
                Console.ReadKey();
                return;
            }

            int j = 0; // счётчик слов с большой буквы
            bool flag = false;

            for (int i = 0; i < words.Length; i++)
            {
                if (Char.IsUpper(words[i][0]))
                {
                    j++;
                    if (j == k)
                    {
                        Console.WriteLine($"Cлово с заглавной буквы на {k} позиции: {words[i]}");
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag)
                Console.WriteLine($"Нет {k} слов с заглавной буквы");
            #endregion

            Console.ReadKey();
        }
    }
}
