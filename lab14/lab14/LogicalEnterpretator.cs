using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lab14
{
    class LogicalEnterpretator
    {
        static public void Start(string inputFile, string outputFile)
        {
            using (var sr = new StreamReader(inputFile))
            {
                using (var sw = new StreamWriter(outputFile))
                {
                    string str, PDNF, PCNF;
                    int strNumber = 0;

                    while ((str = sr.ReadLine()) != null)
                    {
                        if (!LogicalExpression.TryParse(str, out LogicalExpression formula))
                        {
                            throw new LogicalEnterpretatorException("Error in formula!", str, strNumber);
                        }

                        Console.WriteLine("LogicalExpression:");
                        Console.WriteLine(formula.Original);
                        Console.WriteLine();
                        Console.WriteLine("All formula results:");

                        try
                        {
                            foreach (var val in formula.AllFormulaResults())
                            { 
                                Console.WriteLine(val);
                            }

                            PDNF = formula.PDNF();
                            PCNF = formula.PCNF();
                        }
                        catch (FormulaCalculationException exception)
                        {
                            throw new LogicalEnterpretatorException(exception.Error, exception.ErrorFormula, strNumber);
                        }

                        // Вывод формулы, её СКНФ и СДНФ в консоль
                        Console.WriteLine($"{Environment.NewLine}PCNF:");
                        Console.WriteLine(PCNF);
                        Console.WriteLine($"{Environment.NewLine}PDNF:");
                        Console.WriteLine(PDNF);
                        Console.WriteLine();
                        
                        // Вывод формулы, её СКНФ и СДНФ в файл
                        sw.WriteLine("================================");
                        sw.WriteLine("Исходная формула:");
                        sw.WriteLine(formula.Original);
                        sw.WriteLine($"{Environment.NewLine}СКНФ:");
                        sw.WriteLine(PCNF);
                        sw.WriteLine($"{Environment.NewLine}СДНФ:");
                        sw.WriteLine(PDNF);
                        sw.WriteLine();

                        strNumber++;
                    }
                }
            }
        }
        
    }
}
