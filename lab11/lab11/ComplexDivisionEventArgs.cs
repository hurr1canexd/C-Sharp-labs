using System;
using System.Collections.Generic;
using System.Text;

namespace lab11
{
    public sealed class ComplexDivisionEventArgs: EventArgs
    {
        public ComplexDivisionEventArgs(Complex dividend, Complex divider)
        {
            Dividend = dividend;
            Divider = divider;
        }

        public Complex Dividend { get; }
        public Complex Divider { get; }
    }
}
