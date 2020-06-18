using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace lab10
{
    public sealed class MatrixException : Exception
    {
        private string m_paramName;

        public MatrixException(string message) : base(message) { }


        public MatrixException(string message, string paramName) : base(message)
        {
            m_paramName = paramName;
        }


        public override String Message
        {
            get
            {
                String s = base.Message;
                if (!String.IsNullOrEmpty(m_paramName))
                {
                    String resourceString = nameof(m_paramName);
                    return s + Environment.NewLine + resourceString;
                }
                else
                    return s;
            }
        }
    }
}
