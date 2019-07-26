using CodeIT.Airlines.Models.Carros.Interfaces;
using CodeIT.Airlines.Models.Exceptions;
using CodeIT.Airlines.Models.Locais;
using CodeIT.Airlines.Models.Locais.Interfaces;
using System.Linq;

namespace CodeIT.Airlines.Models.Carros
{
    public sealed class Carro : Local, ICarro
    {
        public Carro(string modelo, int capacidade) : base("Carro", capacidade)
        {
            Modelo = modelo;
        }

        public string Modelo { get; private set; }

        public void DirigirAte(ILocal local)
        {
            var motorista = (from pessoa in Pessoas
                            where pessoa.PermissaoDirigir() == true
                            select pessoa).FirstOrDefault();

            if (motorista == null)
                throw new MotoristaNaoEncontradoException("Motorista não encontrado para dirigir.");

            var pessoasRealocar = Pessoas.ToArray();
            RegistrarSaida(pessoasRealocar);
            local.RegistrarEntrada(pessoasRealocar);
        }
    }
}
