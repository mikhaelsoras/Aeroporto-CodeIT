using System;
using System.Collections.Generic;
using System.Text;
using CodeIT.Airlines.Models.Entidades.Enums;
using CodeIT.Airlines.Models.Locais.Interfaces;

namespace CodeIT.Airlines.Models.Entidades
{
    public class ChefeServico : Pessoa
    {
        public ChefeServico() : base(TripulacaoTipo.ChefeServico)
        {
        }

        protected override void LocalPessoasChanged(ILocal local)
        {
        }
    }
}
