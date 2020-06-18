using System;
using System.Collections.Generic;
using System.IO;

namespace lab14
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("The number of arguments less than 2. Enter the paths to input and output files.");
                Environment.Exit(-1);
            }    

            try
            {
                LogicalEnterpretator.Start(args[0], args[1]);
            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine(fnfe.Message);
            }
            catch (DirectoryNotFoundException dnfe)
            {
                Console.WriteLine(dnfe.Message);
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.Message);
            }
            catch (LogicalEnterpretatorException lee)
            {
                Console.WriteLine(lee);
            }

        }
    }
}
