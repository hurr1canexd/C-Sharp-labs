using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace lab10
{
    public class Polynomial<T> : IEnumerable<T>, IEquatable<Polynomial<T>>, IComparable<Polynomial<T>>
        where T : new()
    {
        private Dictionary<int, T> _poly = new Dictionary<int, T>();


        #region Construstors
        public Polynomial()
        {
        }


        public Polynomial(int[] degrees, T[] coeffs)
        {
            if (degrees.Length != coeffs.Length)
            {
                throw new ArgumentException("Invalid sizes.");
            }

            for (var i = 0; i < degrees.Length; i++)
            {
                AddMonom(degrees[i], coeffs[i]);
            }
            Modify();
        }


        public Polynomial(T[] coeffs)
        {
            var count = coeffs.Count() - 1;

            foreach (var coeff in coeffs)
            {
                _poly.Add(count, coeff);
                count--;
            }
            Modify();
        }


        public Polynomial(Dictionary<int, T> poly)
        {
            _poly = poly;
            Modify();
        }


        public Polynomial(Polynomial<T> polynomial) // может быть плохо, если передать тип, который реализует ICloneable
        {
            _poly = new Dictionary<int, T>(polynomial._poly);
        }
        #endregion


        #region Operations
        public static Polynomial<T> Add(Polynomial<T> first, Polynomial<T> second)
        {
            var res = new Polynomial<T>();

            foreach (var item in first._poly)
            {
                res._poly.Add(item.Key, item.Value);
            }
            foreach (var item in second._poly)
            {
                res.AddMonom(item);
            }
            res.Modify();

            return res;
        }


        public static Polynomial<T> Sub(Polynomial<T> first, Polynomial<T> second)
        {
            var res = new Polynomial<T>(first);

            foreach (var monom in second._poly)
            {
                res.SubMonom(monom);
            }
            res.Modify();

            return res;
        }


        public static Polynomial<T> Mul(Polynomial<T> first, Polynomial<T> second)
        {
            var res = new Polynomial<T>();
            
            foreach (var firstMonom in first._poly)
            {
                foreach (var secondMonom in second._poly)
                {
                    var newMonom = new KeyValuePair<int, T>(firstMonom.Key + secondMonom.Key, (dynamic)firstMonom.Value * secondMonom.Value);
                    res.AddMonom(newMonom);
                }
            }
            res.Modify();

            return res;
        }


        //https://www.resolventa.ru/spr/algebra/corner.htm
        public static Polynomial<T> Div(Polynomial<T> dividend, Polynomial<T> divisor)
        {
            // Может тогда надо возвращать пустой полином?
            if (divisor.GetDegree() > dividend.GetDegree())
            {
                throw new ArgumentException("The degree of the divisor more than degree of the divident");
            }

            var res = new Polynomial<T>();

            var divisorDegree = divisor.GetDegree();
            var firstCoeffOfDivisor = divisor._poly[divisorDegree];
            var dividendDegree = 0;
            var firstCoeffOfDividend = dividend._poly[dividendDegree];

            while (true)
            {
                dividendDegree = dividend.GetDegree();
                firstCoeffOfDividend = dividend._poly[dividendDegree];
                var firstMemberOfQuotient = new Polynomial<T>(new int[] { dividendDegree - divisorDegree },
                    new T[] { (dynamic)firstCoeffOfDividend / firstCoeffOfDivisor });
                res.Add(firstMemberOfQuotient); // Добавляю в частное моном
                var subtrahend = Mul(firstMemberOfQuotient, divisor);
                dividend = Sub(dividend, subtrahend);

                if (dividend.IsEmpty() || (dividend.GetDegree() < divisor.GetDegree()))
                {
                    break;
                }
            }
            res.Modify();

            return res;
        }


        public static Polynomial<T> MulByNum(T k, Polynomial<T> a)
        {
            var res = new Polynomial<T>(a);

            foreach (var monom in a._poly)
            {
                res.AddMonom(monom.Key, (dynamic)k * monom.Value);
            }
            res.Modify();

            return res;
        }


        public static Polynomial<T> Pow(Polynomial<T> polynomial, int pow)
        {
            var copy = new Polynomial<T>(polynomial);
            var res = new Polynomial<T>(polynomial);
            for (var i = 0; i < pow; i++)
            {
                res = Mul(res, copy);
            }
            res.Modify();

            return res;
        }


        public void Add(Polynomial<T> a)
        {
            foreach (var item in a._poly)
            {
                AddMonom(item);
            }
        }


        public void Sub(Polynomial<T> a)
        {
            foreach (var item in a._poly)
            {
                SubMonom(item);
            }
        }
        #endregion


        #region Operators overrides
        public static Polynomial<T> operator +(Polynomial<T> first, Polynomial<T> second)
        {
            return Add(first, second);
        }

        public static Polynomial<T> operator -(Polynomial<T> first, Polynomial<T> second)
        {
            return Sub(first, second);
        }

        public static Polynomial<T> operator *(Polynomial<T> first, Polynomial<T> second)
        {
            return Mul(first, second);
        }

        public static Polynomial<T> operator /(Polynomial<T> first, Polynomial<T> second)
        {
            return Div(first, second);
        }

        public static Polynomial<T> operator *(T k, Polynomial<T> a)
        {
            return MulByNum(k, a);
        }


        public static bool operator ==(Polynomial<T> first, Polynomial<T> second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Polynomial<T> first, Polynomial<T> second)
        {
            return !first.Equals(second);
        }

        public static bool operator >(Polynomial<T> A, Polynomial<T> B)
        {
            return A.CompareTo(B) == 1;
        }

        public static bool operator <(Polynomial<T> A, Polynomial<T> B)
        {
            return A.CompareTo(B) == -1;
        }

        public static bool operator >=(Polynomial<T> A, Polynomial<T> B)
        {
            return A.CompareTo(B) > -1;
        }

        public static bool operator <=(Polynomial<T> A, Polynomial<T> B)
        {
            return A.CompareTo(B) < 1;
        }
        #endregion


        public override string ToString()
        {
            var ordered = _poly.OrderByDescending(x => x.Key);
            var sb = new StringBuilder();
            
            foreach (var monom in ordered)
            {
                sb.Append($"{monom.Value}*x^{monom.Key} + \n");
            }

            sb.Replace("*x^0", "");

            return (sb.Length > 3) ? sb.ToString(0, sb.Length - 3) : "";
        }

        private void AddMonom(KeyValuePair<int, T> monom)
        {
            if (_poly.ContainsKey(monom.Key))
            {
                _poly[monom.Key] += (dynamic)monom.Value;
            }
            else
            {
                _poly.Add(monom.Key, monom.Value);
            }
        }

        private void AddMonom(int degree, T value)
        {
            if (_poly.ContainsKey(degree))
            {
                _poly[degree] += (dynamic)value;
            }
            else
            {
                _poly.Add(degree, value);
            }
        }

        private void SubMonom(KeyValuePair<int, T> monom)
        {
            if (_poly.ContainsKey(monom.Key))
            {
                _poly[monom.Key] -= (dynamic)monom.Value;
            }
            else
            {
                _poly.Add(monom.Key, -(dynamic)monom.Value);
            }
        }

        private static KeyValuePair<int, T> DivideMonom(KeyValuePair<int, T> divident, KeyValuePair<int, T> divisor)
        {
            return new KeyValuePair<int, T>(divident.Key - divisor.Key, (dynamic)divident.Value / divisor.Value);
        }

        public bool IsEmpty()
        {
            return _poly.Count == 0;
        }

        private void Modify()
        {
            foreach (var monom in _poly.Where(kvp => kvp.Value == (dynamic)0).ToList())
            {
                _poly.Remove(monom.Key);
            }
        }

        public int GetDegree()
        {
            return _poly.Keys.Max();
        }


        public T Calculate(double x)
        {
            T res = new T();

            foreach (var monom in _poly)
            {   
                res += (dynamic)(monom.Value * Math.Pow(x, (dynamic)monom.Key));
            }

            return res;
        }


        public static Polynomial<T> Superposition(Polynomial<T> f, Polynomial<T> g)
        {
            var res = new Polynomial<T>();

            foreach (var monom in f._poly)
            {
                res += (dynamic)MulByNum(monom.Value, Pow(g, monom.Key));
            }
            res.Modify();

            return res;
        }


        public IEnumerator<T> GetEnumerator()
        {
            foreach (var coef in _poly.Values)
            {
                yield return coef;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        bool IEquatable<Polynomial<T>>.Equals(Polynomial<T> other)
        {
            var polynom = other as Polynomial<T>;

            if (polynom == null) return false;

            foreach (var key in _poly.Keys)
            {
                if (polynom._poly.ContainsKey(key)) return false;
                if (_poly[key] != (dynamic)polynom._poly[key]) return false;
            }
            return true;
        }

        public int CompareTo(Polynomial<T> other)
        {
            var max = Math.Max(GetDegree(), other.GetDegree());
            var min = Math.Min(GetDegree(), other.GetDegree());

            for (var i = max; i > min; i--)
            {
                T val1;
                T val2;
                if (_poly.TryGetValue(i, out val1))
                    if (other._poly.TryGetValue(i, out val2))
                    {
                        if ((dynamic)val1 > val2)
                            return 1;
                        else if ((dynamic)val2 > val1)
                            return -1;
                    }
                    else
                    {
                        if ((dynamic)val1 > 0)
                            return 1;
                        else if ((dynamic)val1 < 0)
                            return -1;
                    }
                else if (other._poly.TryGetValue(i, out val2))
                {
                    if ((dynamic)val2 > 0)
                        return -1;
                    else if ((dynamic)val2 < 0)
                        return 1;
                }
            }
            return 0;
        }
    }
}
