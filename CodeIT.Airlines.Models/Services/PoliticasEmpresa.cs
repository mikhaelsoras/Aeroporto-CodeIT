using CodeIT.Airlines.Models.Entidades;
using CodeIT.Airlines.Models.Entidades.Enums;

namespace CodeIT.Airlines.Models.Services
{
    public static class PoliticasEmpresa
    {
        public static TripulacaoTipo[] TripulacaoComPermissaoDirigir
        {
            get
            {
                TripulacaoTipo[] permitidos = { TripulacaoTipo.Piloto, TripulacaoTipo.ChefeServico, TripulacaoTipo.Policial };
                return permitidos;
            }
        }
    }
}
