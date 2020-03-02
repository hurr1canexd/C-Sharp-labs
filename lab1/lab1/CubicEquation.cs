using System;
using System.Numerics;

namespace lab1
{

    // a*x^3 + b*x^2 + c*x + d = 0
    public class CubicEquation
    {
        private double _a, _b, _c, _d;
        

        public CubicEquation(double a, double b, double c, double d)
        {
            _a = a;
            _b = b;
            _c = c;
            _d = d;
        }

        public CubicEquation(double[] coeffs)
        {
            _a = coeffs[0];
            _b = coeffs[1];
            _c = coeffs[2];
            _d = coeffs[3];
        }

        public override string ToString()
        {
            string equationStr = $"{_a}*x^3 + {_b}*x^2 + {_c}*x + {_d} = 0";

            equationStr = equationStr.Replace("+ -", "- ");

            return equationStr;
        }


        public Complex[] SolveByCardano()
        {
            var solution = new Complex[3];
            if (_a == 0)
            { 
                return null;
            }
            var p = -_b * _b / (3 * _a * _a) + _c / _a;
            var q = 2 * _b * _b * _b / (27 * _a * _a * _a) - _b * _c / (3 * _a * _a) + _d / _a;
            var Q = q * q / 4 + p * p * p / 27;

            var alpha = new Complex();
            var beta = new Complex();

            if (Q > 0)
            {
                var sqrt_Q = Math.Sqrt(Q);

                var tmpPositive = -q / 2 + sqrt_Q;
                var tmpNegative = -q / 2 - sqrt_Q;

                if (tmpPositive < 0)
                {
                    alpha = -Math.Pow(-tmpPositive, 1.0 / 3);
                }
                else
                {
                    alpha = Math.Pow(tmpPositive, 1.0 / 3);
                }

                if (tmpNegative < 0)
                {
                    beta = -Math.Pow(-tmpNegative, 1.0 / 3);
                }
                else
                {
                    beta = Math.Pow(tmpNegative, 1.0 / 3);
                }

                solution[0] = alpha + beta;
                solution[1] = -(alpha + beta) / 2 + (Math.Sqrt(3) * (alpha - beta) / 2) * Complex.ImaginaryOne;
                solution[2] = -(alpha + beta) / 2 - (Math.Sqrt(3) * (alpha - beta) / 2) * Complex.ImaginaryOne;
            }
            else if (Q == 0)
            {
                if (-q / 2 > 0)
                {
                    alpha = Math.Pow(-q / 2, 1.0 / 3);
                }
                else
                {
                    alpha = -Math.Pow(q / 2, 1.0 / 3);
                }

                solution[0] = 2 * alpha;
                solution[1] = solution[2] = -alpha;
            }
            else  // Q < 0
            {
                var sqrt_Q = new Complex(Q, 0);
                sqrt_Q = Complex.Sqrt(Q);

                var tmpPositive = -q / 2 + sqrt_Q;
                var tmpNegative = -q / 2 - sqrt_Q;

                alpha = Complex.Pow(tmpPositive, 1.0 / 3);
                beta = Complex.Pow(tmpNegative, 1.0 / 3);

                solution[0] = alpha + beta;
                solution[1] = -(alpha + beta) / 2 + (Math.Sqrt(3) * (alpha - beta) / 2) * Complex.ImaginaryOne;
                solution[2] = -(alpha + beta) / 2 - (Math.Sqrt(3) * (alpha - beta) / 2) * Complex.ImaginaryOne;
            }

            for (int i = 0; i < solution.Length; i++)
            {
                solution[i] -= _b / (3 * _a);
            }

            return solution;
        }


        public Complex[] SolveByVietCardano()
        {
            var solution = new Complex[3];

            double Q = (_a * _a - 3 * _b) / 9;
            double R = (2 * _a * _a * _a - 9 * _a * _b + 27 * _c) / 54;

            if (R * R < Q * Q * Q)
            {
                var t = Math.Acos(R / Math.Sqrt(Math.Pow(Q, 3))) / 3;
                var x1 = -2 * Math.Sqrt(Q) * Math.Cos(t) - _a / 3;
                var x2 = -2 * Math.Sqrt(Q) * Math.Cos(t + (2 * Math.PI / 3)) - _a / 3;
                var x3 = -2 * Math.Sqrt(Q) * Math.Cos(t - (2 * Math.PI / 3)) - _a / 3;

                solution[0] = new Complex(x1, 0);
                solution[1] = new Complex(x2, 0);
                solution[2] = new Complex(x3, 0);
            }
            else
            {
                var A = -Math.Sign(R) * Math.Pow(Complex.Abs(R) + Math.Sqrt(Math.Pow(R, 2) - Math.Pow(Q, 3)), (1.0 / 3.0));
                var B = (A == 0) ? 0.0 : Q / A;

                var x1 = (A + B) - _a / 3;

                if (A == B)
                {
                    var x2 = -A - _a / 3;

                    solution[0] = new Complex(x1, 0);
                    solution[1] = new Complex(x2, 0);
                    solution[2] = new Complex(double.NaN, double.NaN);
                }
                else
                {
                    var x2 = new Complex(-(A + B) / 2 - _a / 3, -Math.Sqrt(3) * (A - B) / 2);
                    var x3 = new Complex(-(A + B) / 2 - _a / 3, Math.Sqrt(3) * (A - B) / 2);

                    solution[0] = new Complex(x1, 0);
                    solution[1] = x2;
                    solution[2] = x3;
                }
            }

            return solution;
        }
    }
}
