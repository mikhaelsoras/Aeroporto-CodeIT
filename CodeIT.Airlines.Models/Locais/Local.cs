using CodeIT.Airlines.Models.Entidades.Enums;
using CodeIT.Airlines.Models.Entidades.Interfaces;
using CodeIT.Airlines.Models.Exceptions;
using CodeIT.Airlines.Models.Locais.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeIT.Airlines.Models.Locais
{
    public class Local : ILocal
    {
        protected readonly List<IPessoa> pessoas;

        public event Action<ILocal> LocalChanged;

        public IEnumerable<IPessoa> Pessoas => pessoas;
        public string Nome { get; protected set; }
        public int? Capacidade { get; protected set; }

        /// <summary>
        /// Construtor do Local.
        /// </summary>
        /// <param name="nome">Nome do Local</param>
        /// <param name="capacidade">Capacidade indica o limite de pessoas nesse local, caso seja NULL será ilimitado.</param>
        public Local(string nome, int? capacidade = null)
        {
            Nome = nome;
            Capacidade = capacidade;
            pessoas = new List<IPessoa>();
        }

        public virtual void RegistrarEntrada(params IPessoa[] pessoas)
        {
            if (Capacidade != null && Capacidade < this.pessoas.Count + pessoas.Length)
                throw new StackOverflowException($"Capacidade máxima de {Capacidade} excedida.");

            ValidarLocal(pessoas)?.RegistrarSaida(pessoas);

            foreach (var pessoa in pessoas)
            {
                if (!this.pessoas.Contains(pessoa))
                {
                    this.pessoas.Add(pessoa);
                    pessoa.Entrar(this, false);
                }
            }

            LocalChanged?.Invoke(this);
        }

        public virtual void RegistrarSaida(params IPessoa[] pessoas)
        {
            foreach (var pessoa in pessoas)
            {
                if (this.pessoas.Contains(pessoa))
                {
                    this.pessoas.Remove(pessoa);
                    pessoa.Sair(false);
                }
            }

            LocalChanged?.Invoke(this);
        }

        public static ILocal ValidarLocal(params IPessoa[] pessoas)
        {
            var grupos = pessoas.GroupBy(p => p.LocalAtual);
            if (grupos.Count() != 1)
                throw new PessoasDeLocalDiferenteException("Pessoas de locais diferentes.");
            return pessoas[0].LocalAtual;
        }

        public IPessoa PessoaPorTipo(TripulacaoTipo tripulacaoTipo)
        {
            var pessoas = from pessoa in Pessoas
                    where pessoa.TipoTripulacao == tripulacaoTipo
                    select pessoa;

            return pessoas.FirstOrDefault();
        }
    }
}
