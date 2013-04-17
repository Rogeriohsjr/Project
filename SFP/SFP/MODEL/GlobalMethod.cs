using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SFP
{
    public class GlobalMethod : System.Web.UI.Page
    {

        public Int32 ValidSessionUser()
        {
            if (Session["IdUsuario"] == null)
            {
                //TODO: Verficicar Cache
                Context.Response.Redirect("/Acesso.aspx");
                return 0;
            }
            else
            {
                return int.Parse(Session["IdUsuario"].ToString());
            }
        
        }

    }
}