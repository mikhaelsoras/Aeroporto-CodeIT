using System;

namespace CodeIT.Airlines.Business.Interfaces
{
    public interface IGerarLog
    {
        event Action<string> OnLog;
    }
}
