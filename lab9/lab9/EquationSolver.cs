using System;
using System.Collections.Generic;
using System.Text;

namespace lab9
{
    public class EquationSolver
    {
        public double DichotomyMethod(Tuple<double, double> segment, Eqtn eq, double eps)
        {
            if (segment.Item1 > segment.Item2)
                throw new ArgumentException("Invalid segment", nameof(segment));

            double a = segment.Item1, b = segment.Item2;
            double c;

            while (b - a > eps)
            {
                c = (a + b) / 2;

                if (eq(a) * eq(c) < 0)
                {
                    b = c;
                }
                else if (eq(a) * eq(c) > 0)
                {
                    a = c;
                }
                else
                {
                    return c;
                }
            }

            return a;
        }

    }
}
