using System;

namespace CodeIT.Airlines.Models.Exceptions
{
    public class OficialSozinhoComChefeServicoException : Exception
    {
        public OficialSozinhoComChefeServicoException(string message) : base(message)
        {
        }
    }
}
