using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace lab10
{
    public sealed class SquareMatrix : ICloneable, IEnumerable<double>, IComparable<SquareMatrix>, IEquatable<SquareMatrix>, IComparable
    {
        private double[,] _matr = new double[0, 0];
        //private int _size = 0;

        public double[,] Matr { get; set; }
        public int Size => _matr.GetLength(0);

        #region Constructors
        public SquareMatrix() { }


        // Instance constructor
        public SquareMatrix(double[,] matr)
        {
            if (matr.GetLength(0) < 1)
            {
                throw new ArgumentException("Size can't be less then 1.", nameof(matr));
            }
            if (matr.GetLength(0) != matr.GetLength(1))
            {
                throw new ArgumentException("Matrix is not square", nameof(matr));
            }
            _matr = (double[,])matr.Clone();
        }


        // Copy constructor
        public SquareMatrix(SquareMatrix matrix) : this(matrix._matr)
        {
            //_matr = (double[,])matrix._matr.Clone(); // TODO: use constructor
        }
        #endregion


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    sb.Append($"{_matr[i, j]} ");
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }


        #region Operations
        public static SquareMatrix Add(SquareMatrix A, SquareMatrix B)
        {
            if (A.Size == 0) return B;
            if (B.Size == 0) return A;
            if (A.Size != B.Size)
            {
                throw new ArgumentException("Size A != Size B", nameof(A.Size) + " != " + nameof(B.Size));
            }
            var C = new SquareMatrix(new double[A.Size, A.Size]);

            for (int i = 0; i < A.Size; i++)
            {
                for (int j = 0; j < A.Size; j++)
                {
                    C._matr[i, j] = A._matr[i, j] + B._matr[i, j];
                }
            }

            return C;
        }


        public static SquareMatrix Sub(SquareMatrix A, SquareMatrix B)
        {
            if (A.Size == 0) return MulByNum(-1, B); // TODO унарный минус
            if (B.Size == 0) return A;
            if (A.Size != B.Size)
            {
                throw new ArgumentException("Size A != Size B", nameof(A.Size) + " != " + nameof(B.Size));
            }

            var C = new SquareMatrix(new double[A.Size, A.Size]);

            for (int i = 0; i < A.Size; i++)
            {
                for (int j = 0; j < A.Size; j++)
                {
                    C._matr[i, j] = A._matr[i, j] - B._matr[i, j];
                }
            }

            return C;
        }


        public static SquareMatrix Mul(SquareMatrix A, SquareMatrix B)
        {
            if (A.Size != B.Size)
            {
                throw new ArgumentException("Size A != Size B", nameof(A.Size) + " != " +nameof(B.Size));
            }
            var C = new SquareMatrix(new double[A.Size, A.Size]);

            for (int i = 0; i < A.Size; i++)
            {
                for (int j = 0; j < A.Size; j++)
                {
                    for (int inner = 0; inner < A.Size; inner++)
                    {
                        C._matr[i, j] += A._matr[i, inner] * B._matr[inner, j];
                    }
                }
            }

            return C;
        }


        // A / B
        public static SquareMatrix Div(SquareMatrix A, SquareMatrix B)
        {
            if (A.Size != B.Size)
            {
                throw new ArgumentException("Size A != Size B", nameof(A.Size) + " != " + nameof(B.Size));
            }

            return Mul(A, B.Inverse());
        }


        // k * A
        public static SquareMatrix MulByNum(double k, SquareMatrix A)
        {
            var res = new SquareMatrix(new double[A.Size, A.Size]);

            for (int i = 0; i < A.Size; i++)
            {
                for (int j = 0; j < A.Size; j++)
                {
                    res._matr[i, j] = k * A._matr[i, j];
                }
            }

            return res;
        }


        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is SquareMatrix)
            {
                var b = obj as SquareMatrix;
                if (Size != b.Size) return false;
                return Equals(b);
            }

            if (obj is int num)
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        if (_matr[i, j] != num) return false;
                    }
                }
                return true;
            }

            return false;
        }
        #endregion


        #region Operators override
        // A + B
        public static SquareMatrix operator +(SquareMatrix A, SquareMatrix B)
        {
            return Add(A, B);
        }


        // A - B
        public static SquareMatrix operator -(SquareMatrix A, SquareMatrix B)
        {
            return Sub(A, B);
        }


        // A * B
        public static SquareMatrix operator *(SquareMatrix A, SquareMatrix B)
        {
            return Mul(A, B);
        }


        // A / B
        public static SquareMatrix operator /(SquareMatrix A, SquareMatrix B)
        {
            return Div(A, B);
        }


        // k * A
        public static SquareMatrix operator *(double k, SquareMatrix A)
        {
            return MulByNum(k, A);
        }


        // A * k
        public static SquareMatrix operator *(SquareMatrix A, double k)
        {
            return MulByNum(k, A);
        }


        // A == B
        public static bool operator ==(SquareMatrix A, object B)
        {
            return A.Equals(B);
        }


        // A != B
        public static bool operator !=(SquareMatrix A, object B)
        {
            return !A.Equals(B);
        }


        // A > B
        public static bool operator >(SquareMatrix A, SquareMatrix B)
        {
            return A.CompareTo(B) == 1;
        }


        // A < B
        public static bool operator <(SquareMatrix A, SquareMatrix B)
        {
            return A.CompareTo(B) == -1;
        }


        // A >= B
        public static bool operator >=(SquareMatrix A, SquareMatrix B)
        {
            return A.CompareTo(B) > -1;
        }


        // A <= B
        public static bool operator <=(SquareMatrix A, SquareMatrix B)
        {
            return A.CompareTo(B) < 1;
        }
        #endregion


        // Determinant
        public double GetDeterminant()
        {
            const double Eps = 1E-9;
            double det = 1;
            var obj = this.Clone() as SquareMatrix;

            for (var i = 0; i < Size; ++i)
            {
                var k = i;
                for (var j = i + 1; j < Size; ++j)
                {
                    if (Math.Abs(obj._matr[j, i]) > Math.Abs(obj._matr[k, i]))
                        k = j;
                }
                if (Math.Abs(obj._matr[k, i]) < Eps)
                {
                    det = 0;
                    return 0;
                }
                // swap rows (i<->k)
                for (var inner = 0; inner < Size; inner++)
                {
                    var tmp = obj._matr[i, inner];
                    obj._matr[i, inner] = obj._matr[k, inner];
                    obj._matr[k, inner] = tmp;
                }

                if (i != k)
                {
                    det = -det;
                }
                det *= obj._matr[i, i];
                for (var j = i + 1; j < Size; ++j)
                {
                    obj._matr[i, j] /= obj._matr[i, i];
                }
                for (var j = 0; j < Size; ++j)
                {
                    if (j != i && Math.Abs(obj._matr[j, i]) > Eps)
                    {
                        for (var t = i + 1; t < Size; ++t)
                        {
                            obj._matr[j, t] -= obj._matr[i, t] * obj._matr[j, i];
                        }
                    }
                }
            }

            return det;
        }


        public static SquareMatrix GetTranspose(SquareMatrix matrix)
        {
            var AT = matrix.Clone() as SquareMatrix;

            for (var i = 0; i < AT.Size; i++)
            {
                for (var j = 0; j < AT.Size; j++)
                {
                    AT._matr[i, j] = matrix._matr[j, i];
                }
            }

            return AT;
        }

        public SquareMatrix Transpose()
        {
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    _matr[j, i] = _matr[j, i];
                }
            }

            return this;
        }

        #region Inverse matrix
        private double[,] CreateMatrixWithoutRowAndColumn(int row, int column)
        {
            double[,] resMatrix = new double[Size - 1, Size - 1];

            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                {
                    if (i == row || j == column)
                        continue;
                    if (i < row)
                    {
                        if (j < column)
                        {
                            resMatrix[i, j] = _matr[i, j];
                        }
                        else
                        {
                            resMatrix[i, j - 1] = _matr[i, j];
                        }
                    }
                    else
                    {
                        if (j < column)
                        {
                            resMatrix[i - 1, j] = _matr[i, j];
                        }
                        else
                        {
                            resMatrix[i - 1, j - 1] = _matr[i, j];
                        }
                    }
                }

            return resMatrix;
        }


        public SquareMatrix Inverse()
        {
            double eps = 1e-10;
            double[,] resArr = new double[Size, Size];
            double det = GetDeterminant();

            if (Math.Abs(det) < eps)
            {
                throw new MatrixException("Determinant = 0", nameof(GetDeterminant));
            }

            for (int i = 0; i < Size; i++)
            { 
                for (int j = 0; j < Size; j++)
                {
                    int sign = ((i + j) & 1) == 0 ? 1 : -1;

                    resArr[i, j] = sign * new SquareMatrix(CreateMatrixWithoutRowAndColumn(i, j)).GetDeterminant() / det;
                }
            }

            return new SquareMatrix(resArr).Transpose();
        }
        #endregion


        public object Clone()
        {
            return new SquareMatrix(_matr);
        }


        public IEnumerator<double> GetEnumerator()
        {
            foreach (double val in _matr)
            {
                yield return val; // Оператор yield return используется для возврата каждого элемента по одному.
                                  // Применяя yield return мы декларируем, что данный метод возвращает последовательность
                                  // IEnumerable, элементами которой являются результаты выражений каждого из yield return
            }
        }


        // метод, возвращающий ссылку на другой интерфейс-перечислитель
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public int CompareTo(SquareMatrix other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            return GetDeterminant().CompareTo(other.GetDeterminant());
        }

        public bool Equals(SquareMatrix other)
        {
            if (Size != other.Size) return false;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (_matr[i, j] != other._matr[i, j]) return false;
                }
            }
            return true;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            if (obj is SquareMatrix)
            {
                return CompareTo(obj as SquareMatrix);
            }
            else
            {
                throw new ArgumentException("Invalid type", nameof(obj));
            }

        }
    }
}
