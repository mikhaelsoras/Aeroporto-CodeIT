using System.Linq;
using CodeIT.Airlines.Models.Entidades.Enums;
using CodeIT.Airlines.Models.Exceptions;
using CodeIT.Airlines.Models.Locais.Interfaces;

namespace CodeIT.Airlines.Models.Entidades
{
    public class Oficial : Pessoa
    {
        public Oficial() : base(TripulacaoTipo.Oficial)
        {
        }

        protected override void LocalPessoasChanged(ILocal local)
        {
            var sozinhoComOutraPessoa = local.Pessoas.Count() == 2;
            if (!sozinhoComOutraPessoa)
                return;

            var chefeServico = from pessoa in local.Pessoas
                               where pessoa.TipoTripulacao == TripulacaoTipo.ChefeServico
                               select pessoa;

            if (chefeServico.Count() == 1)
                throw new OficialSozinhoComChefeServicoException("Oficial sozinho com Chefe de Serviço.");
        }
    }
}
