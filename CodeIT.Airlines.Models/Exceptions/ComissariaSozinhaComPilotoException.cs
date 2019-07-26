using System;
using System.Collections.Generic;
using System.Text;

namespace CodeIT.Airlines.Models.Exceptions
{
    public class ComissariaSozinhaComPilotoException : Exception
    {
        public ComissariaSozinhaComPilotoException(string message) : base(message)
        {
        }
    }
}
