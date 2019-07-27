using CodeIT.Airlines.Models.Carros.Interfaces;
using CodeIT.Airlines.Models.Entidades.Interfaces;
using CodeIT.Airlines.Models.Exceptions;
using CodeIT.Airlines.Models.Locais;
using CodeIT.Airlines.Models.Locais.Interfaces;
using System.Linq;

namespace CodeIT.Airlines.Models.Carros
{
    public sealed class Carro : Local, ICarro
    {
        /// <summary>
        /// Construtor do Carro.
        /// </summary>
        /// <param name="modelo">Modelo do carro.</param>
        /// <param name="capacidade">Capacidade indica o limite de pessoas nesse local, caso seja NULL será ilimitado.</param>
        public Carro(ILocal localAtual, string modelo, int capacidade) : base("Carro", capacidade)
        {
            LocalAtual = localAtual;
            Modelo = modelo;
        }

        public string Modelo { get; private set; }
        public ILocal LocalAtual { get; private set; }

        public void DirigirAte(ILocal local)
        {
            var motorista = (from pessoa in Pessoas
                            where pessoa.PermissaoDirigir() == true
                            select pessoa).FirstOrDefault();

            if (motorista == null)
                throw new MotoristaNaoEncontradoException("Motorista não encontrado para dirigir.");

            LocalAtual = local;

            var pessoasRealocar = Pessoas.ToArray();
            RegistrarSaida(pessoasRealocar);
            local.RegistrarEntrada(pessoasRealocar);
        }

        public override void RegistrarEntrada(params IPessoa[] pessoas)
        {
            foreach (var pessoa in pessoas)
            {
                if (pessoa.LocalAtual != null && pessoa.LocalAtual != LocalAtual)
                    throw new LocalDiferenteDoAtualException("Pessoa se encontra em um local diferente do atual.");
            }

            base.RegistrarEntrada(pessoas);
        }
    }
}
