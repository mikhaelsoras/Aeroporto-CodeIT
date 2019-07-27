using System;
using System.Collections.Generic;
using System.Text;
using CodeIT.Airlines.Models.Entidades.Enums;
using CodeIT.Airlines.Models.Locais.Interfaces;

namespace CodeIT.Airlines.Models.Entidades
{
    public class Piloto : Pessoa
    {
        public Piloto() : base(TripulacaoTipo.Piloto)
        {
        }

        protected override void LocalPessoasChanged(ILocal local)
        {
        }
    }
}
