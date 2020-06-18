using System;
using System.Collections.Generic;
using System.Text;

namespace lab11
{
    public sealed class Vector<T> : IComparable, IComparable<Vector<T>>
        where T: IComparable, new()
    {
        T[] _vect;

        public T[] Vect { get => _vect; }


        public Vector(T[] vect)
        {
            _vect = vect;
        }


        public Vector(Vector<T> oldVect)
        {
            _vect = (T[])oldVect._vect.Clone();
        }


        private static void ThrowIfVectorsLengthsNotEqual(Vector<T> left, Vector<T> right)
        {
            if (left._vect.Length != right._vect.Length)
            {
                throw new ArgumentException("The vector sizes is not equals");
            }
        }


        #region Operations
        public static Vector<T> Add(Vector<T> a, Vector<T> b)
        {
            ThrowIfVectorsLengthsNotEqual(a, b);

            var resVect = new T[a._vect.Length];
            for (int i = 0; i < a._vect.Length; i++)
            {
                resVect[i] = a._vect[i] + (dynamic)b._vect[i];
            }

            return new Vector<T>(resVect);
        }


        public static Vector<T> Sub(Vector<T> a, Vector<T> b)
        {
            ThrowIfVectorsLengthsNotEqual(a, b);

            var resVect = new T[a._vect.Length];
            for (int i = 0; i < a._vect.Length; i++)
            {
                resVect[i] = a._vect[i] - (dynamic)b._vect[i];
            }

            return new Vector<T>(resVect);
        }


        public void Sub(Vector<T> subtrahend)
        {
            ThrowIfVectorsLengthsNotEqual(subtrahend, this);

            for (int i = 0; i < subtrahend._vect.Length; i++)
            {
                _vect[i] -= (dynamic)subtrahend._vect[i];
            }
        }


        public static Vector<T> MulByNum(T alpha, Vector<T> a)
        {
            var resVect = new T[a._vect.Length];
            for (int i = 0; i < a._vect.Length; i++)
            {
                resVect[i] = alpha * (dynamic)a._vect[i];
            }

            return new Vector<T>(resVect);
        }
        #endregion


        #region Overrides of the operators
        public static Vector<T> operator +(Vector<T> a, Vector<T> b)
        {
            return Add(a, b);
        }


        public static Vector<T> operator -(Vector<T> a, Vector<T> b)
        {
            return Sub(a, b);
        }


        public static Vector<T> operator *(T alpha, Vector<T> a)
        {
            return MulByNum(alpha, a);
        }


        public static Vector<T> operator *(Vector<T> a, T alpha)
        {
            return MulByNum(alpha, a);
        }


        public static bool operator >(Vector<T> a, Vector<T> b)
        {
            return a.CompareTo(b) == 1;
        }


        public static bool operator <(Vector<T> a, Vector<T> b)
        {
            return a.CompareTo(b) == -1;
        }

        public static bool operator >=(Vector<T> a, Vector<T> b)
        {
            return a.CompareTo(b) != -1;
        }


        public static bool operator <=(Vector<T> a, Vector<T> b)
        {
            return a.CompareTo(b) != 1;
        }
        #endregion


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("(");
            for (int i = 0; i < _vect.Length - 1; i++)
            {
                sb.Append($"{_vect[i]}, ");
            }
            sb.Append($"{_vect[_vect.Length - 1]})");

            return sb.ToString();
        }


        public static double Modul(Vector<T> a)
        {
            double sum = 0;

            var obj = new T();
            if (obj is Complex)
            {
                foreach (var x in a._vect)
                {
                    var y = (dynamic)x;
                    sum += Complex.Modul(y) * Complex.Modul(y);
                }
            }
            else
            {
                for (int i = 0; i < a._vect.Length; i++)
                {
                    sum += (dynamic)a._vect[i] * a._vect[i];
                }
            }

            return Math.Sqrt(sum);
        }


        public static T ScalarProduct(Vector<T> a, Vector<T> b)
        {
            ThrowIfVectorsLengthsNotEqual(a, b);

            T res = new T();
            for (int i = 0; i < a._vect.Length; i++)
            {
                res += a._vect[i] * (dynamic)b._vect[i];
            }

            return res;
        }


        // Оператор проекции вектора a на вектор b
        public static Vector<T> Proj(Vector<T> a, Vector<T> b)
        {
            return (dynamic)ScalarProduct(a, b) / ScalarProduct(b, b) * b;
        }


        public static Vector<T>[] Organolize(Vector<T>[] vectors)
        {
            var res = new Vector<T>[vectors.Length];

            res[0] = vectors[0];
            for (int i = 1; i < vectors.Length; i++)
            {
                var curVect = new Vector<T>(vectors[i]);
                for (int j = 0; j < vectors.Length - 1; j++)
                {
                    curVect.Sub(Proj(vectors[i], res[j]));
                }
                res[i] = curVect;
            }

            return res;
        }


        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            else if (obj is Vector<T>)
            {
                return CompareTo(obj as Vector<T>);
            }
            else
            {
                throw new ArgumentException("Invalid type", nameof(obj));
            }
        }


        public int CompareTo(Vector<T> other)
        {
            if (_vect.Length != other._vect.Length)
            {
                throw new ArithmeticException("Invalid sizes");
            }

            return Modul(this).CompareTo(Modul(other));
        }
    }
}
