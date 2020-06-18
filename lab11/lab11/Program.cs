using System;

namespace lab11
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var z = new Complex(-1, 0);
            var mdl = Complex.Modul(z);
            Console.WriteLine($"z = {z}\n|z| = {mdl}\n");

            var vrb = new Complex(1.0 / 2, Math.Sqrt(3) / 2);
            Console.WriteLine($"a = {vrb}\na^20 =  {Complex.Pow(vrb, 20)}\n");


            var roots = Complex.Root(z, 4);
            Console.WriteLine("z^(1/4)");
            for (var i = 0; i < roots.Length; i++)
            {
                Console.WriteLine($"z{i + 1} = {roots[i]}");
            }

            Complex numm = Complex.Zero;
            Console.WriteLine(numm);
            z.ComplexDivisionEventHandler += Func;

            try
            {
                Console.WriteLine(z / numm);
            }
            catch (DivideByZeroException dvze)
            {
                Console.WriteLine(dvze.Message);
            }


            var vect1 = new Vector<Complex>(new Complex[] { new Complex(1, 0), new Complex(2, 1), new Complex(2, 3) });
            var vect2 = new Vector<Complex>(new Complex[] { new Complex(1, 0), new Complex(2, 1), new Complex(2, 3) });
            Console.WriteLine(vect1 + vect2);
            Console.WriteLine(Vector<Complex>.Modul(vect1));

            
            var a = new Complex(2, 3);
            var b = new Complex(1, 4);
            var c = new Complex(5, 3);
            var d = new Complex(3, 7);
            var vectSystem = new Vector<Complex>[] { new Vector<Complex>(new Complex[] { a, b }),
                new Vector<Complex>(new Complex[] { c, d }) };
            var res = Vector<Complex>.Organolize(vectSystem);
            for (int i = 0; i < res.Length; i++)
            {
                Console.WriteLine(res[i]);
            }

            Console.ReadKey();
        }


        public static void Func(object obj, ComplexDivisionEventArgs args)
        {
            Console.WriteLine(args.Divider.ToString());
            throw new DivideByZeroException("Divide by zero");
        }
    }


}
