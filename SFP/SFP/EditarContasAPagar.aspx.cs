using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SFP.MODEL
{
    public partial class EditarContasAPagar : System.Web.UI.Page
    {
        private Int32 iIdUser = 8;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            
            string sError;
            ddlCategoria.DataTextField = "Descricao";
            ddlCategoria.DataValueField = "IdCategoria";
            ddlCategoria.DataSource = new CategoryDAO(iIdUser).FindByWhere("BLOQUEADO = 0 ORDER BY DESCRICAO", out sError);
            ddlCategoria.DataBind();



        }

        protected void btVoltar_Onclick(object sender, EventArgs e)
        {
            Response.Redirect("ListaContasAPagar.aspx");
        }
        protected void btSalvar_OnClick(object sender, EventArgs e)
        {


            Response.Redirect("ListaContasAPagar.aspx");
        }
    }
}