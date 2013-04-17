using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFP
{
    public class Category
    {
        public Int32 Id {get;set;}
        public String Description { get; set; }
        public Boolean IsBlock { get; set; }
        public User User { get; set; }
        

        public string Status
        {
            get
            {
                if (IsBlock)
                    return "Bloqueado";
                else
                    return "Ativo";
            }
        }
    }
}