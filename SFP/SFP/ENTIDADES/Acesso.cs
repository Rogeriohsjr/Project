using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFP.ENTIDADES
{
    public class Acesso
    {
        public Int64 Id { get; set; }
        public DateTime Date { get; set; }
        public Usuario Usuario { get; set; }
        public String Ip { get; set; }
    }
}