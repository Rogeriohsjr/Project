using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFP
{
    public class User
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
        public Boolean IsBlock { get; set; }
        public DateTime DateRegister { get; set; }
        public String DateLastAcess { get; set; }
        public String DataAcess { get; set; }
    }
}