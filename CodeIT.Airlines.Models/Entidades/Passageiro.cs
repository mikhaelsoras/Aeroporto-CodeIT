using System;
using System.Collections.Generic;
using System.Text;
using CodeIT.Airlines.Models.Entidades.Enums;
using CodeIT.Airlines.Models.Locais.Interfaces;

namespace CodeIT.Airlines.Models.Entidades
{
    public class Passageiro : Pessoa
    {
        public Passageiro() : base(TripulacaoTipo.Passageiro)
        {
        }

        protected override void LocalPessoasChanged(ILocal local)
        {            
        }
    }
}
