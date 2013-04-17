using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFP
{
    public class Usuario
    {
        public Int32 IdUsuario { get; set; }
        public String Nome { get; set; }
        public String Login { get; set; }
        public String Senha { get; set; }
        public String Email { get; set; }
        public Boolean Bloqueado { get; set; }
        public DateTime DataCadastro { get; set; }
        public String DadosUltimoAcesso { get; set; }
        public String DadosAcesso { get; set; }
    }
}