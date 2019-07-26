using CodeIT.Airlines.Models.Entidades.Enums;
using CodeIT.Airlines.Models.Exceptions;
using CodeIT.Airlines.Models.Locais.Interfaces;
using System.Linq;

namespace CodeIT.Airlines.Models.Entidades
{
    public class Comissaria : Pessoa
    {
        public Comissaria() : base(TripulacaoTipo.Comissaria)
        {
        }

        protected override void LocalPessoasChanged(ILocal local)
        {
            var sozinhaComOutraPessoa = local.Pessoas.Count() == 2;
            if (!sozinhaComOutraPessoa)
                return;

            var policial = from pessoa in local.Pessoas
                           where pessoa.TipoTripulacao == TripulacaoTipo.Piloto
                           select pessoa;

            if (policial.Count() == 0)
                throw new ComissariaSozinhaComPilotoException("Comissária sozinha com o Piloto.");
        }
    }
}
