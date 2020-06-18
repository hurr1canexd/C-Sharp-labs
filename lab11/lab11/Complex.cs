using System;
using System.Collections.Generic;
using System.Text;

namespace lab11
{
    public sealed class Complex: ICloneable, IComparable, IComparable<Complex>, IEquatable<Complex>
    {
        private double _real = default(double);
        private double _imag = default(double);


        public static readonly Complex Zero = new Complex(default(double), default(double));
        public static readonly Complex One = new Complex(1.0, default(double));
        public static readonly Complex ImaginaryOne = new Complex(default(double), 1.0);


        #region Constructors
        public Complex() { }
        
        public Complex(double real, double imag)
        {
            _real = real;
            _imag = imag;
        }

        public Complex(Complex num) : this(num._real, num._imag) { }
        #endregion


        #region Operations
        public static Complex Add(Complex a, Complex b)
        {
            return new Complex(a._real + b._real, a._imag + b._imag);
        }


        public static Complex Sub(Complex a, Complex b)
        {
            return new Complex(a._real - b._real, a._imag - b._imag);
        }


        public static Complex Mul(Complex a, Complex b)
        {
            return new Complex(a._real * b._real - a._imag * b._imag,
                a._real * b._imag + a._imag * b._real);
        }


        public static Complex Div(Complex a, Complex b)
        {
            double eps = 1e-10;
            if (Modul(b) < eps)
            {
                var newZeroEvent = new ComplexDivisionEventArgs(a, b);
                a.ComplexDivisionEventHandler?.Invoke(a, newZeroEvent);
            }
            return new Complex((a._real * b._real + a._imag * b._imag) / (b._real * b._real + b._imag * b._imag),
                (a._imag * b._real - a._real * b._imag) / (b._real * b._real + b._imag * b._imag));
        }


        public static Complex MulByNum(double alpha, Complex a)
        {
            return new Complex(alpha * a._real, alpha * a._imag);
        }
        #endregion


        #region Overrides of the operators
        public static Complex operator +(Complex a, Complex b)
        {
            return Add(a, b);
        }


        public static Complex operator -(Complex a, Complex b)
        {
            return Sub(a, b);
        }


        public static Complex operator *(Complex a, Complex b)
        {
            return Mul(a, b);
        }


        public static Complex operator /(Complex a, Complex b)
        {
            return Div(a, b);
        }


        public static Complex operator *(double alpha, Complex a)
        {
            return MulByNum(alpha, a);
        }


        public static Complex operator *(Complex a, double alpha)
        {
            return MulByNum(alpha, a);
        }


        public static bool operator ==(Complex a, Complex b)
        {
            return a.Equals(b);
        }


        public static bool operator !=(Complex a, Complex b)
        {
            return !a.Equals(b);
        }


        public static bool operator >(Complex a, Complex b)
        {
            return a.CompareTo(b) == 1;
        }


        public static bool operator <(Complex a, Complex b)
        {
            return a.CompareTo(b) == -1;
        }


        public static bool operator >=(Complex a, Complex b)
        {
            return a.CompareTo(b) != -1;
        }


        public static bool operator <=(Complex a, Complex b)
        {
            return a.CompareTo(b) != 1;
        }
        #endregion

        public event EventHandler<ComplexDivisionEventArgs> ComplexDivisionEventHandler;

        public override string ToString()
        {
            return $"({_real}, {_imag})";
        }


        public static double Modul(Complex num)
        {
            return Math.Sqrt(num._real * num._real + num._imag * num._imag);
        }


        public static double Arg(Complex num)
        {
            if (num._real > 0) // x > 0
            {
                if (num._imag >= 0) // x > 0, y >= 0
                {
                    return Math.Atan(num._imag / num._real);
                }
                else // x > 0, y < 0
                {
                    return 2 * Math.PI - Math.Atan(Math.Abs(num._imag / num._real));
                }
            }
            else if (num._real < 0) // x < 0
            {
                if (num._imag >= 0) // x < 0, y >= 0
                {
                    return Math.PI - Math.Atan(Math.Abs(num._imag / num._real));
                }
                else // x < 0, y < 0
                {
                    return Math.PI + Math.Atan(Math.Abs(num._imag / num._real));
                }
            }
            else // x = 0
            {
                if (num._imag > 0) // x = 0, y > 0
                {
                    return Math.PI / 2;
                }
                else if (num._imag < 0) // x = 0, y < 0
                {
                    return 3 * Math.PI / 2;
                }
            }
            return default(double); // x = 0, y = 0
        }


        public static Complex Pow(Complex num, int n)
        {
            if (n <= 0)
            {
                throw new ArgumentException("Negative degree", nameof(n));
            }

            return new Complex(
                Math.Pow(Modul(num), (double)n) * Math.Cos(n * Arg(num)),
                Math.Pow(Modul(num), (double)n) * Math.Sin(n * Arg(num))
                );

        }


        public static Complex[] Root(Complex num, int n)
        {
            if (n < 1)
            {
                throw new ArgumentException("Invalid degree", nameof(n));
            }
            var roots = new Complex[n];

            for (int i = 0; i < roots.Length; i++)
            {
                roots[i] = new Complex(
                    Math.Pow(Modul(num), 1.0 / n) * Math.Cos((Arg(num) + 2 * Math.PI * i) / n),
                    Math.Pow(Modul(num), 1.0 / n) * Math.Sin((Arg(num) + 2 * Math.PI * i) / n)
                    );
            }

            return roots;
        }


        public object Clone()
        {
            return new Complex(_real, _imag);
        }


        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            if (obj is Complex)
            {
                return CompareTo(obj as Complex);
            }
            else
            {
                throw new ArgumentException("Invalid type", nameof(obj));
            }
        }

        public int CompareTo(Complex other)
        {
            return Modul(this).CompareTo(Modul(other));
        }

        public bool Equals(Complex other)
        {
            if (other is null)
            {
                return false;
            }
            double eps = 1e-10;
            return Math.Abs(_real - other._real) < eps && Math.Abs(_imag - other._imag) < eps;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (!(obj is Complex c))
            {
                return false;
            }
            return Equals(c);
        }
    }

}
