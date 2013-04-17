using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SFP.MODEL;

namespace SFP
{
    public partial class Acesso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
                return;

            txtLogin.Focus();

        }

        protected void btTour_OnClick(object sender, EventArgs e)
        {
            string sError = string.Empty;
            UserDAO objDao = new UserDAO();
            List<User> sListUser =
                            objDao.FindByWhere(" LOGIN = 'DEMO' AND SENHA = 'DEMO'", out sError);
            Session["Usuario"] = sListUser[0];
            Session["IdUsuario"] = sListUser[0].Id;
            GravarAcesso(sListUser[0], out sError);
            Response.Redirect("ListaContasAPagar.aspx");
        }

        protected void btAcessar_OnClick(object sender, EventArgs e)
        {
            UserDAO objDao = new UserDAO();
            string sError = string.Empty;
            string sLogin = RetirarCaracterInvalido(txtLogin.Text);
            string sSenha = RetirarCaracterInvalido(txtSenha.Text);
            string sMsg = string.Empty;

            List<User> sListUser =
                objDao.FindByWhere(" LOGIN = '" + sLogin + "' AND SENHA = '" + sSenha + "'", out sError);

            if (sListUser.Count == 0 || sListUser.Count > 1)
            {
                sMsg = "Campo Login ou Senha errado!";
            }
            else
            {
                Session["Usuario"] = sListUser[0];
                Session["IdUsuario"] = sListUser[0].Id;
                GravarAcesso(sListUser[0], out sError);
                Response.Redirect("ListaContasAPagar.aspx");
            }

            TrataMsgPrincipal(sMsg);
        }

        private bool TrataMsgPrincipal(string sError)
        {
            if (String.IsNullOrWhiteSpace(sError))
            {
                pnMsgPrincipal.Visible = false;
                lbMsgPrincipal.Text = "";
                return false;
            }
            pnMsgPrincipal.Visible = true;
            lbMsgPrincipal.Text = sError;
            return true;
        }

        private string RetirarCaracterInvalido(string sText)
        {
            sText = sText.Replace("'", "").Replace("\"", "").Replace(".", "").Replace(",", "");
            sText = sText.Replace("|", "").Replace("/", "").Replace("?", "").Replace("º", "");
            sText = sText.Replace("#", "").Replace("$", "").Replace("%", "").Replace("*", "");
            sText = sText.Replace("]", "").Replace("[", "").Replace(")", "").Replace("(", "");

            return sText;
        }

        private void GravarAcesso(User pUser, out string sErrorMessage)
        {
            ENTIDADES.Acess objAcesso = new ENTIDADES.Acess();
            AcessDAO objAcessoDAO = new AcessDAO(pUser.Id);

            objAcesso.Date = DateTime.Now;
            objAcesso.User = pUser;

            string sIp = "IP NÃO IDENTIFICADO";

            try
            {
                sIp = "[" + Context.Request.UserHostAddress + "] [" + System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName + "]";
            }
            catch
            {}

            objAcesso.Ip = sIp;
            objAcessoDAO.Save(objAcesso, out sErrorMessage);
        }

    }
}