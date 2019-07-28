using CodeIT.Airlines.Business.Interfaces;
using CodeIT.Airlines.Models.Carros;
using CodeIT.Airlines.Models.Carros.Interfaces;
using CodeIT.Airlines.Models.Entidades;
using CodeIT.Airlines.Models.Entidades.Enums;
using CodeIT.Airlines.Models.Entidades.Interfaces;
using CodeIT.Airlines.Models.Locais;
using CodeIT.Airlines.Models.Locais.Interfaces;
using System;
using System.Linq;
using System.Text;
using CodeIT.Airlines.Business.Extensions;

namespace CodeIT.Airlines.Business.Cenarios
{
    /// <summary>
    /// Testa um cenário onde a tripulação tecnica e a tripulação de cabine se encontra na Aeronave.
    /// </summary>
    public sealed class CenarioTradicional : ICenario
    {
        public readonly ILocal Aeronave = new Local("Aeronave");
        public readonly ILocal Terminal = new Local("Terminal");
        public readonly ICarro ForTwo;

        public event Action<string> OnLog;

        public CenarioTradicional()
        {
            Terminal.LocalChanged += VerificarCondicaoSucesso;
            Aeronave.LocalChanged += VerificarCondicaoSucesso;

            Terminal.RegistrarEntrada(GerarPessoasIniciaisTerminal());
            Aeronave.RegistrarEntrada(GerarPessoasIniciaisAeronave());

            ForTwo = new Carro(Aeronave, "Smart Fortwo", 2);
        }

        private void VerificarCondicaoSucesso(ILocal local)
        {
            if (Terminal.Pessoas.Count() == 0 && ForTwo.Pessoas.Count() == 0)
            {
                Log($"Sucesso: Todas as pessoas do {Terminal.Nome} foram transferidas.");
            }
        }

        public void IniciarEmbarque()
        {
            ForTwo.LevarPessoasParaDestino(Terminal, this, Aeronave.PessoaPorTipo(TripulacaoTipo.Piloto));
            ForTwo.LevarPessoasParaDestino(Aeronave, this,
                Terminal.PessoaPorTipo(TripulacaoTipo.Policial), Terminal.PessoaPorTipo(TripulacaoTipo.Presidiario));

            while (Terminal.Pessoas.Any())
            {
                ForTwo.LevarPessoasParaDestino(Terminal, this,
                    Aeronave.PessoaPorTipo(TripulacaoTipo.ChefeServico));

                ForTwo.LevarPessoasParaDestino(Aeronave, this,
                    Terminal.PessoaPorTipo(TripulacaoTipo.ChefeServico), Terminal.Pessoas.First());
            }
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

        public void Log(string message)
        {
            OnLog?.Invoke(message);
        }
    }
}
