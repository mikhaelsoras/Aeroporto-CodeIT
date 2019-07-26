using CodeIT.Airlines.Models.Entidades.Interfaces;
using CodeIT.Airlines.Models.Locais.Interfaces;
using System;
using System.Collections.Generic;

namespace CodeIT.Airlines.Models.Locais
{
    public class Local : ILocal
    {
        protected readonly List<IPessoa> pessoas;

        public event Action<ILocal> LocalChanged;

        public IEnumerable<IPessoa> Pessoas => pessoas;
        public string Nome { get; protected set; }
        public int? Capacidade { get; protected set; }

        public Local(string nome, int? capacidade = null)
        {
            Nome = nome;
            Capacidade = capacidade;
            pessoas = new List<IPessoa>();
        }

        public void RegistrarEntrada(params IPessoa[] pessoas)
        {
            if (Capacidade != null && Capacidade > this.pessoas.Count + pessoas.Length)
                throw new StackOverflowException($"Capacidade máxima de {Capacidade} excedida.");

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

        public void RegistrarSaida(params IPessoa[] pessoas)
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
    }
}
