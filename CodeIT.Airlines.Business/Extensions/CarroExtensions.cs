using CodeIT.Airlines.Models.Carros.Interfaces;
using CodeIT.Airlines.Models.Entidades.Interfaces;
using CodeIT.Airlines.Models.Locais.Interfaces;
using System.Linq;
using System.Text;
using CodeIT.Airlines.Business.Interfaces;

namespace CodeIT.Airlines.Business.Extensions
{
    public static class CarroExtension
    {
        public static void LevarPessoasParaDestino(this ICarro carro, ILocal destino, IGerarLog logger, params IPessoa[] pessoas)
        {
            carro.RegistrarEntrada(pessoas);

            var foiforam = pessoas.Count() == 1 ? "foi" : "foram";
            logger?.Log($"{PessoasToText(pessoas)} {foiforam} para o {destino.Nome} de {carro.Modelo}.");

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
    }
}
