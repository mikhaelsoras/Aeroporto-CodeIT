using CodeIT.Airlines.Business.Interfaces;

namespace CodeIT.Airlines.Business.Cenarios
{
    public interface ICenario : IGerarLog
    {
        void IniciarEmbarque();
    }
}