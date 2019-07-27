using System;
using System.Collections.Generic;
using System.Text;

namespace CodeIT.Airlines.Models.Exceptions
{
    public class PessoasDeLocalDiferenteException : Exception
    {
        public PessoasDeLocalDiferenteException(string message) : base(message)
        {
        }
    }
}
