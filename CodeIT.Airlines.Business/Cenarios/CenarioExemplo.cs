using CodeIT.Airlines.Models.Carros;
using CodeIT.Airlines.Models.Carros.Interfaces;
using CodeIT.Airlines.Models.Entidades;
using CodeIT.Airlines.Models.Entidades.Enums;
using CodeIT.Airlines.Models.Entidades.Interfaces;
using CodeIT.Airlines.Models.Exceptions;
using CodeIT.Airlines.Models.Locais;
using CodeIT.Airlines.Models.Locais.Interfaces;
using System.Linq;

namespace CodeIT.Airlines.Business.Cenarios
{
    public sealed class CenarioExemplo
    {
        public readonly ILocal Aeronave = new Local("Aeroporto");
        public readonly ILocal Terminal = new Local("Terminal");
        public readonly ICarro Carro;

        public CenarioExemplo()
        {
            Terminal.LocalChanged += VerificarCondicaoSucesso;
            Terminal.RegistrarEntrada(GerarPessoasIniciaisTerminal());
            Aeronave.RegistrarEntrada(GerarPessoasIniciaisAeronave());

            Carro = new Carro(Aeronave, "Smart Fortwo", 2);
        }

        private void VerificarCondicaoSucesso(ILocal local)
        {
            if (local.Pessoas.Count() == 0)
            {
                // Sucesso;
            }
        }

        public void IniciarEmbarque()
        {
            TransferirPessoas(Carro, Terminal, Aeronave.PessoaPorTipo(TripulacaoTipo.Piloto));
            TransferirPessoas(Carro, Aeronave, Terminal.PessoaPorTipo(TripulacaoTipo.Policial),
                Terminal.PessoaPorTipo(TripulacaoTipo.Presidiario));
        }

        private void TransferirPessoas(ICarro carro, ILocal destino, params IPessoa[] pessoas)
        {
            carro.RegistrarEntrada(pessoas);
            carro.DirigirAte(destino);
        }

        private static IPessoa[] GerarPessoasIniciaisTerminal()
        {
            IPessoa[] pessoas = {
                new Presidiario(),
                new Policial(),
                new Passageiro(),
                new Passageiro(),
                new Passageiro(),
                new Passageiro(),
                new Passageiro(),
                new Passageiro()
            };

            return pessoas;
        }

        private static IPessoa[] GerarPessoasIniciaisAeronave()
        {
            IPessoa[] pessoas = {
                new Piloto(),
                new Oficial(),
                new Oficial(),
                new ChefeServico(),
                new Comissaria(),
                new Comissaria()
            };

            return pessoas;
        }
    }
}
