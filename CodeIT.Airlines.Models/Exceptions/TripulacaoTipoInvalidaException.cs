using System;

namespace CodeIT.Airlines.Models.Exceptions
{
    public class TripulacaoTipoInvalidaException : Exception
    {
        public TripulacaoTipoInvalidaException(string message) : base(message)
        {
        }
    }
}
