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

namespace CodeIT.Airlines.Business.Cenarios
{
    /// <summary>
    /// Testa um cenário onde a tripulação tecnica e a tripulação de cabine se encontra na Aeronave.
    /// </summary>
    public sealed class CenarioExemplo : IGerarLog
    {
        public readonly ILocal Aeronave = new Local("Aeronave");
        public readonly ILocal Terminal = new Local("Terminal");
        public readonly ICarro Carro;

        public event Action<string> OnLog;

        public CenarioExemplo()
        {
            Terminal.LocalChanged += VerificarCondicaoSucesso;
            Aeronave.LocalChanged += VerificarCondicaoSucesso;

            Terminal.RegistrarEntrada(GerarPessoasIniciaisTerminal());
            Aeronave.RegistrarEntrada(GerarPessoasIniciaisAeronave());

            Carro = new Carro(Aeronave, "Smart Fortwo", 2);
        }

        private void VerificarCondicaoSucesso(ILocal local)
        {
            if (Terminal.Pessoas.Count() == 0 && Carro.Pessoas.Count() == 0)
            {
                OnLog?.Invoke($"Sucesso: Todas as pessoas do {Terminal.Nome} foram transferidas.");
            }
        }

        public void IniciarEmbarque()
        {
            TransferirPessoas(Carro, Terminal, Aeronave.PessoaPorTipo(TripulacaoTipo.Piloto));
            TransferirPessoas(Carro, Aeronave, 
                Terminal.PessoaPorTipo(TripulacaoTipo.Policial), Terminal.PessoaPorTipo(TripulacaoTipo.Presidiario));

            while (Terminal.Pessoas.Any())
            {
                TransferirPessoas(Carro, Terminal,
                    Aeronave.PessoaPorTipo(TripulacaoTipo.ChefeServico));

                TransferirPessoas(Carro, Aeronave, 
                    Terminal.PessoaPorTipo(TripulacaoTipo.ChefeServico), Terminal.Pessoas.First());
            }
        }

        private void TransferirPessoas(ICarro carro, ILocal destino, params IPessoa[] pessoas)
        {
            carro.RegistrarEntrada(pessoas);

            var foiforam = pessoas.Count() == 1 ? "foi" : "foram";
            OnLog?.Invoke($"{PessoasToText(pessoas)} {foiforam} para o {destino.Nome} de {carro.Modelo}.");

            carro.DirigirAte(destino);

            string PessoasToText(params IPessoa[] _pessoas)
            {
                var res = new StringBuilder();

                for (int i = 0; i < _pessoas.Count(); i++)
                {
                    if (i != 0)
                        res.Append(" e ");
                    res.Append(_pessoas[i].GetType().Name);
                }

                return res.ToString();
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
    }
}
