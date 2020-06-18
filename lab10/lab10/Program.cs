using System;

namespace lab10
{
    public sealed class Program
    {
        static void Main(string[] args)
        {
            SquareMatrix A = new SquareMatrix(new double[,]
            {
                { 5, 2 },
                { -7, 3 }
            });

            SquareMatrix B = new SquareMatrix(new double[,]
            {
                { 1, 2 },
                { -2, 7 }
            });

            Console.WriteLine(B);
            B.Transpose();
            Console.WriteLine(B);
            B.Transpose();
            Console.WriteLine(B);

            Console.WriteLine("detA = " + A.GetDeterminant() + "; detB = " + B.GetDeterminant());
            Console.WriteLine(" > " + (A > B));
            Console.WriteLine(" < " + (A < B));
            Console.WriteLine(" >= " + (A >= B));
            Console.WriteLine(" <= " + (A <= B));


            #region matrix
            /*
            Console.WriteLine("|B| = " + B.GetDeterminant() + "\n");

            Console.WriteLine(A + B);
            Console.WriteLine(A - B);
            Console.WriteLine(A * B);

            Console.WriteLine(B.Inverse());
            Console.WriteLine(A / B);

            foreach (var el in B)
            {
                Console.WriteLine(el);
            }
            */
            #endregion

            /*
            var pol1 = new Polynomial<double>(new double[] { 1, 0, -1 });
            var pol2 = new Polynomial<double>(new double[] { 1, 0, 1 });
            var sub = pol1 - pol2;
            var mul = pol1 * pol2;
            Console.WriteLine(sub);
            Console.WriteLine(mul);
            Console.WriteLine(pol1);
            Console.WriteLine(pol2);
            */

            //var polyRgrr = new Polynomial<double>(new int[] { 0, 2 }, new double[] { 4, 6 });
            //var poly1 = new Polynomial<double>(new double[] { 2, -1, 5, -8, 1 });
            //var poly2 = new Polynomial<double>(new double[] { 1, -1, 1 });
            var poly1 = new Polynomial<SquareMatrix>(new SquareMatrix[] { 2.0 * A, -1 * A, 3 * B });
            var poly2 = new Polynomial<SquareMatrix>(new SquareMatrix[] { 3 * B, 1 * A});

            var sum = poly1 + poly2;
            Console.WriteLine("poly1 + poly2\n" + sum);

            var res = poly1.Calculate(1);
            Console.WriteLine("poly1(x) = \n" + res);

            var mul = poly1 * poly2;
            Console.WriteLine("poly1 * poly2\n" + mul);

            try
            {
                var del = poly1 / poly2;
                Console.WriteLine("poly1 / poly2\n" + del);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }


            var poly3 = Polynomial<SquareMatrix>.Superposition(poly1, poly2);
            Console.WriteLine("poly1(poly2) = \n" + poly3);
            

            Console.ReadKey();
        }
    }
}
