using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFP
{
    public class Categoria
    {
        public Int32 IdCategoria {get;set;}
        public String Descricao { get; set; }
        public Boolean Bloqueado { get; set; }
        public Usuario Usuario { get; set; }

        public string Status
        {
            get
            {
                if (Bloqueado)
                    return "Bloqueado";
                else
                    return "Ativo";
            }
        }
    }
}