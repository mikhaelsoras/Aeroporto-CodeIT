using System;
using System.Collections.Generic;
using System.Text;

namespace CodeIT.Airlines.Models.Exceptions
{
    class PresidiarioSemSupervisaoException : Exception
    {
        public PresidiarioSemSupervisaoException(string message) : base(message)
        {
        }
    }
}
