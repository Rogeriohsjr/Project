using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFP.ENTIDADES
{
    public class Acess
    {
        public Int64 Id { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public String Ip { get; set; }
    }
}