using CodeIT.Airlines.Models.Entidades.Enums;
using CodeIT.Airlines.Models.Exceptions;
using CodeIT.Airlines.Models.Locais.Interfaces;
using System.Linq;

namespace CodeIT.Airlines.Models.Entidades
{
    public class Presidiario : Pessoa
    {
        public Presidiario() : base(TripulacaoTipo.Presidiario)
        {
        }

        protected override void LocalPessoasChanged(ILocal local)
        {
            var policial = from pessoa in local.Pessoas
                           where pessoa.TipoTripulacao == TripulacaoTipo.Policial
                           select pessoa;

            if (policial.Count() == 0)
                throw new PresidiarioSemSupervisaoException("Presidiário sem supervisão policial.");
        }
    }
}
