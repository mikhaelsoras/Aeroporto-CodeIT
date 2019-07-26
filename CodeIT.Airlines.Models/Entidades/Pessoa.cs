using CodeIT.Airlines.Models.Entidades.Enums;
using CodeIT.Airlines.Models.Entidades.Interfaces;
using CodeIT.Airlines.Models.Exceptions;
using CodeIT.Airlines.Models.Locais.Interfaces;
using CodeIT.Airlines.Models.Services;
using System;

namespace CodeIT.Airlines.Models.Entidades
{
    public abstract class Pessoa : IPessoa
    {
        protected Pessoa(TripulacaoTipo tipoTripulacao)
        {
            if (tipoTripulacao == TripulacaoTipo.Nenhum)
                throw new TripulacaoTipoInvalidaException("Tripulacao do tipo Nenhum inválida.");

            TipoTripulacao = tipoTripulacao;
        }

        public ILocal LocalAtual { get; protected set; }
        public TripulacaoTipo TipoTripulacao { get; protected set; }

        public bool PermissaoDirigir()
        {
            if (Array.IndexOf(PoliticasEmpresa.TripulacaoComPermissaoDirigir, TipoTripulacao) != -1)
                return true;

            return false;
        }

        public void Entrar(ILocal local, bool registrarEntrada = true)
        {
            if (local != LocalAtual)
            {
                if (LocalAtual != null)
                    Sair();

                LocalAtual = local;
                if (registrarEntrada)
                    LocalAtual.RegistrarEntrada(this);
                LocalAtual.LocalChanged += LocalPessoasChanged;
            }
        }

        public void Sair(bool registrarSaida = true)
        {
            if (LocalAtual != null)
            {
                LocalAtual.LocalChanged -= LocalPessoasChanged;
                if (registrarSaida)
                    LocalAtual.RegistrarSaida(this);
                LocalAtual = null;
            }
        }

        protected abstract void LocalPessoasChanged(ILocal local);
    }
}
