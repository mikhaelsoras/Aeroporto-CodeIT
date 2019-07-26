using System;

namespace CodeIT.Airlines.Models.Exceptions
{
    public class MotoristaNaoEncontradoException : Exception
    {
        public MotoristaNaoEncontradoException(string message) : base(message)
        {
        }
    }
}
