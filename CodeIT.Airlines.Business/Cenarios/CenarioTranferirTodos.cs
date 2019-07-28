using CodeIT.Airlines.Business.Extensions;
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

namespace CodeIT.Airlines.Business.Cenarios
{
    public class CenarioTranferirTodos : ICenario
    {
        public readonly ILocal Aeronave = new Local("Aeronave");
        public readonly ILocal Terminal = new Local("Terminal");
        public readonly ICarro ForTwo;

        public event Action<string> OnLog;

        public CenarioTranferirTodos()
        {
            Terminal.LocalChanged += VerificarCondicaoSucesso;
            Aeronave.LocalChanged += VerificarCondicaoSucesso;

            Terminal.RegistrarEntrada(GerarPessoasIniciaisTerminal());

            ForTwo = new Carro(Terminal, "Smart Fortwo", 2);
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
            //Levar ambas comissarias
            ForTwo.LevarPessoasParaDestino(Aeronave, this,
                Terminal.PessoaPorTipo(TripulacaoTipo.ChefeServico), Terminal.PessoaPorTipo(TripulacaoTipo.Comissaria));

            ForTwo.LevarPessoasParaDestino(Terminal, this,
                Aeronave.PessoaPorTipo(TripulacaoTipo.ChefeServico));

            ForTwo.LevarPessoasParaDestino(Aeronave, this,
                Terminal.PessoaPorTipo(TripulacaoTipo.ChefeServico), Terminal.PessoaPorTipo(TripulacaoTipo.Comissaria));

            ForTwo.LevarPessoasParaDestino(Terminal, this,
                Aeronave.PessoaPorTipo(TripulacaoTipo.ChefeServico));

            // Levar Oficiais

            ForTwo.LevarPessoasParaDestino(Aeronave, this,
                Terminal.PessoaPorTipo(TripulacaoTipo.Piloto), Terminal.PessoaPorTipo(TripulacaoTipo.Oficial));

            ForTwo.LevarPessoasParaDestino(Terminal, this,
                Aeronave.PessoaPorTipo(TripulacaoTipo.Piloto));

            ForTwo.LevarPessoasParaDestino(Aeronave, this,
                Terminal.PessoaPorTipo(TripulacaoTipo.Piloto), Terminal.PessoaPorTipo(TripulacaoTipo.Oficial));

            ForTwo.LevarPessoasParaDestino(Terminal, this,
                Aeronave.PessoaPorTipo(TripulacaoTipo.Piloto));

            // Levar ChefeServico
            ForTwo.LevarPessoasParaDestino(Aeronave, this,
                Terminal.PessoaPorTipo(TripulacaoTipo.Piloto), Terminal.PessoaPorTipo(TripulacaoTipo.ChefeServico));

            ForTwo.LevarPessoasParaDestino(Terminal, this,
                Aeronave.PessoaPorTipo(TripulacaoTipo.Piloto));

            // Levar o restante

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
                new Passageiro(),

                new Piloto(),
                new Oficial(),
                new Oficial(),
                new ChefeServico(),
                new Comissaria(),
                new Comissaria()
            };

            return pessoas;
        }

        private static IPessoa[] GerarPessoasIniciaisAeronave()
        {
            IPessoa[] pessoas = {
                
            };

            return pessoas;
        }

        public void Log(string message)
        {
            OnLog?.Invoke(message);
        }
    }
}
