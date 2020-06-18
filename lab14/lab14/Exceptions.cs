using System;
using System.Collections.Generic;
using System.Text;

namespace lab14
{
    // Исключения, возникающие в классе вычисления СКНФ и СДНФ
    class FormulaCalculationException : Exception
    {
        public char Operator { get; private set; }
        public string Error { get; private set; }
        public string ErrorFormula { get; private set; }
        public FormulaCalculationException(string error, string formula, char oper) : base()
        {
            Error = error;
            Operator = oper;
            ErrorFormula = formula;
        }

        public override string ToString()
        {
            return $"{Error}; Operator: {Operator}";
        }
    }


    // Исключения, возникающие в классе логического интерпретатора
    class LogicalEnterpretatorException : Exception
    {
        public string Message { get; private set; }
        public string ErrorFormula { get; private set; }
        public int StrErrorNumber { get; private set; }
        public LogicalEnterpretatorException(string err, string formula, int num) : base()
        {
            Message = err;
            ErrorFormula = formula;
            StrErrorNumber = num;
        }

        public override string ToString()
        {
            return $"Error: {Message}; In formula: {ErrorFormula}; In file string: {StrErrorNumber}";
        }
    }


    // Исключение, возникающее при переводе в постфиксную форму
    class PostfixFormulasException : Exception
    {
        public string Formula { get; private set; }
        public string Error { get; private set; }

        public PostfixFormulasException(string strFormulas, string strError) : base()
        {
            Formula = strFormulas;
            Error = strError;
        }

        public override string ToString()
        {
            return $"In formula: {Formula} program find error: {Error}";
        }
    }
}
