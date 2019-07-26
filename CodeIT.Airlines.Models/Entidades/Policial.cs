using System;
using System.Collections.Generic;
using System.Text;
using CodeIT.Airlines.Models.Entidades.Enums;
using CodeIT.Airlines.Models.Locais.Interfaces;

namespace CodeIT.Airlines.Models.Entidades
{
    public class Policial : Pessoa
    {
        public Policial() : base(TripulacaoTipo.Policial)
        {
        }

        protected override void LocalPessoasChanged(ILocal local)
        {
            
        }
    }
}
