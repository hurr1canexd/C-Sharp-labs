using System;
using System.Collections.Generic;
using System.Text;

namespace lab14
{
    class LogicalExpression
    {
        public string Original { get; } // исходная формула
        private readonly string _formulaInPostfix; // формула в постфиксной записи
        private bool[] _uniqueValuesCollections; // храню это поле, чтобы не выделять потом память каждый раз под новую строку таблицы
        public List<char> _variables; // список со всеми переменными
        static private Dictionary<char, int> _operationPriority = new Dictionary<char, int> 
        {
            { '(', 0 }, 
            { ')', 0 },
            { '|', 1 }, // штрих Шеффера
            { '/', 1 }, // стрелка Пирса
            { '~', 2 }, // эквиваленция
            { '@', 3 }, // импликация, но в файле можно задавать с помощью двух символов: ->
            { '+', 4 }, // дизъюнкция
            { '^', 4 }, // исключющее "или"
            { '*', 5 }, // конъюнкция
            { '!', 6 }  // отрицание
        };


        private LogicalExpression(string strFormula, string formulaInPostfix, List<char>? variables)
        {
            Original = strFormula;
            _formulaInPostfix = formulaInPostfix;
            _variables = variables;
            _variables.Sort();
            _uniqueValuesCollections = new bool[_variables.Count];
            
        }


        private LogicalExpression() 
            : this(string.Empty, string.Empty, new List<char>()) 
        { }


        public static bool TryParse(string? s, out LogicalExpression result)
        {
            try
            {
                string formulaInPostfix = ConvertToPostfix(s, out List<char> variables);
                result = new LogicalExpression(s, formulaInPostfix, variables);
                return true;
            }
            catch (PostfixFormulasException)
            {
                result = new LogicalExpression();
                return false;
            }
        }


        public override string ToString()
        {
            return Original;
        }

        
        // Вычисление результата для строки таблицы истинности
        private bool Calculation(bool[] arr)
        {
            if (arr.Length != _variables.Count)
            {
                throw new ArgumentException("Array contains incorrect number of values. Expected " 
                    + _variables.Count + "values.");
            }

            Stack<bool> valStack = new Stack<bool>();

            foreach (char ch in _formulaInPostfix)
            {
                // Если это переменная, то в стек помещается ее значение
                if (_variables.Contains(ch))
                {
                    valStack.Push(arr[_variables.IndexOf(ch)]);
                    continue;
                }

                if (ch == '1')
                {
                    valStack.Push(true);
                    continue;
                }
               
                if (ch == '0')
                {
                    valStack.Push(false);
                    continue;
                }

                // Ошибка, если стек не содержит достаточное число операндов
                if (ch == '!') {
                    if (valStack.Count < 1)
                    {
                        throw new FormulaCalculationException($"Error in operator", Original, ch);
                    }
                }
                else
                {
                    if (valStack.Count < 2)
                    {
                        throw new FormulaCalculationException($"Error in operator", Original, ch);
                    }
                }

                switch(ch)
                {
                    case '!': valStack.Push(Not(valStack.Pop()));
                        break;
                    case '+': valStack.Push(Or(valStack.Pop(), valStack.Pop())); 
                        break;
                    case '^': valStack.Push(Xor(valStack.Pop(), valStack.Pop()));
                        break;
                    case '*': valStack.Push(And(valStack.Pop(), valStack.Pop())); 
                        break;
                    case '@': valStack.Push(Implication(valStack.Pop(), valStack.Pop())); 
                        break;
                    case '~': valStack.Push(Equals(valStack.Pop(), valStack.Pop())); 
                        break;
                    case '|': valStack.Push(ShafferStroke(valStack.Pop(), valStack.Pop())); 
                        break;
                    case '/': valStack.Push(PierArrow(valStack.Pop(), valStack.Pop())); 
                        break;                    
                }
            }

            if (valStack.Count != 1)
            {
                throw new FormulaCalculationException("Logical Expression has excess logic number", Original, '-');
            }

            return valStack.Pop();
        }


        // num - количество переменных, val - номер строки в таблице истинности
        // Например, вызовем эту IntToBoolValues(3, 5). Пятой строке в таблице истинности для трёх переменных будет соответствовать число 5
        // 5 = 101. Значит, вернётся массив bool = {true, false, true}
        private bool[] IntToBoolValues(int num, int val) 
        {
            int n = 1;
            for (int i = num - 1; i >= 0; --i)
            {
                if((val & n) == 0)
                {
                    _uniqueValuesCollections[i] = false;
                }
                else
                {
                    _uniqueValuesCollections[i] = true;
                }
                n <<= 1;
            }
            return _uniqueValuesCollections;
        }


        // Возвращает массив - результат для всех подстановок
        // Например, для "A*B" вернётся (false, false, false, true)
        public bool[] AllFormulaResults()
        {
            int size = 1 << _variables.Count;
            bool[] resArr = new bool[size];

            for (int i = 0; i < size; ++i)
            {
                resArr[i] = Calculation(IntToBoolValues(_variables.Count, i));
            }

            return resArr;
        }


        // СКНФ
        public string PCNF()
        {
            if (_variables.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder pcnfSB = new StringBuilder();
            bool[] formValues = AllFormulaResults();

            for (int i = 0; i < formValues.Length; ++i)
            {
                // Если 0 в таблице истинности
                if (!formValues[i])
                {
                    bool[] currentValues = IntToBoolValues(_variables.Count, i);
                    pcnfSB.Append('(');
                    for (int j = 0; j < currentValues.Length; ++j)
                    {
                        if (!currentValues[j])
                        {
                            pcnfSB.Append(_variables[j]);
                        }
                        else
                        {
                            pcnfSB.Append("!" + _variables[j]);
                        }
                        pcnfSB.Append('+');
                    }
                    pcnfSB.Remove(pcnfSB.Length - 1, 1);
                    pcnfSB.Append(")*");
                }
            }

            if (pcnfSB.Length == 0) return "СКНФ нет.";
            return pcnfSB.Length > 1 ? pcnfSB.Remove(pcnfSB.Length - 1, 1).ToString() : pcnfSB.ToString();
        }


        // СДНФ
        public string PDNF()
        {
            if (_variables.Count == 0)
            { 
                return string.Empty;
            }

            StringBuilder pdnfSB = new StringBuilder();
            bool[] formValues = AllFormulaResults();
            
            for(int i = 0; i < formValues.Length; ++i)
            {
                // Если 1 в таблице истинности
                if (formValues[i])
                {
                    bool[] currentValues = IntToBoolValues(_variables.Count, i);
                    pdnfSB.Append('(');
                    for (int j = 0; j < currentValues.Length; ++j)
                    {
                        if(currentValues[j])
                        {
                            pdnfSB.Append(_variables[j]);
                        }
                        else
                        {
                            pdnfSB.Append("!" + _variables[j]);
                        }
                        pdnfSB.Append('*');
                    }
                    pdnfSB.Remove(pdnfSB.Length - 1, 1);
                    pdnfSB.Append(")+");
                }
            }

            if (pdnfSB.Length == 0) return "СДНФ нет.";
            return pdnfSB.Length > 1 ? pdnfSB.Remove(pdnfSB.Length - 1, 1).ToString() : pdnfSB.ToString();
        }


        #region Logical operations
        private bool Not(bool val)
        {
            return !val;
        }


        private bool And(bool lVal, bool rVal)
        {
            return lVal && rVal;
        }


        private bool Or(bool lVal, bool rVal)
        {
            return lVal || rVal;
        }


        private bool Xor(bool lVal, bool rVal)
        {
            return (!lVal && rVal) || (lVal && !rVal);
        }


        private bool Implication(bool lVal, bool rVal)
        {
            return !lVal || rVal;
        }


        private bool Equals(bool lVal, bool rVal)
        {
            return (lVal && rVal) || (!lVal && !rVal);
        }


        private bool PierArrow(bool lVal, bool rVal)
        {
            return !(lVal || rVal);
        }


        private bool ShafferStroke(bool lVal, bool rVal)
        {
            return !(lVal && rVal);
        }
        #endregion


        // Возвращает формулу в польской нотации (AB+)
        private static string ConvertToPostfix(string formula, out List<char> variables)
        {
            string strProc = formula.ToUpper().Replace(" ", "").Replace("->", "@");
            StringBuilder resBuilder = new StringBuilder();
            Stack<char> intermediate = new Stack<char>();
            List<char> tmpVariables = new List<char>();

            foreach (char ch in strProc)
            {
                if (char.IsLetter(ch) || ch == '0' || ch == '1')
                {
                    // Ищу все уникальные переменные
                    if(!tmpVariables.Contains(ch) && ch != '0' && ch != '1')
                    {
                        tmpVariables.Add(ch);
                    }
                    resBuilder.Append(ch);
                    continue;
                }

                if (ch == '(')
                {
                    intermediate.Push(ch);
                    continue;
                }

                if (ch == ')')
                {
                    if (intermediate.Count == 0)
                    {
                        throw new PostfixFormulasException(formula, "Logical Expression hasn't open bracket.");
                    }

                    char symbol = intermediate.Pop();
                    while (symbol != '(')
                    {
                        resBuilder.Append(symbol);

                        if (intermediate.Count == 0)
                        {
                            throw new PostfixFormulasException(formula, "Logical Expression hasn't open bracket.");
                        }
                        symbol = intermediate.Pop();
                    }
                    continue;
                }

                try
                {   
                    // Пока промежуточный стек содержит операторы большего приоритета, чем текущий
                    while (intermediate.Count > 0 && _operationPriority[intermediate.Peek()] >= _operationPriority[ch])
                    {
                        resBuilder.Append(intermediate.Pop());
                    }
                    
                    intermediate.Push(ch);
                   
                }
                catch (KeyNotFoundException)
                {
                    throw new PostfixFormulasException(formula, "Logical Expression contains invalid symbol: " + ch);
                }
            }

            while (intermediate.Count > 0)
            {
                char symbol = intermediate.Pop();
                if (symbol == '(')
                {
                    throw new PostfixFormulasException(formula, "Logical Expression hasn't close bracket.");
                }
                resBuilder.Append(symbol);
            }

            variables = tmpVariables;
            return resBuilder.ToString();
        }
    }
}
