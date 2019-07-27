using System;
using System.Collections.Generic;
using System.Text;

namespace CodeIT.Airlines.Models.Exceptions
{
    public class LocalDiferenteDoAtualException : Exception
    {
        public LocalDiferenteDoAtualException(string message) : base(message)
        {
        }
    }
}
