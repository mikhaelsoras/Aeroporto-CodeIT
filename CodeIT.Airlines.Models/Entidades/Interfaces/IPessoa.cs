using CodeIT.Airlines.Models.Entidades.Enums;
using CodeIT.Airlines.Models.Locais.Interfaces;

namespace CodeIT.Airlines.Models.Entidades.Interfaces
{
    public interface IPessoa : IPermissaoDirigir
    {
        TripulacaoTipo TipoTripulacao { get; }
        ILocal LocalAtual { get; }
        void Entrar(ILocal local, bool registrarEntrada = true);
        void Sair(bool registrarSaida = true);
    }
}
