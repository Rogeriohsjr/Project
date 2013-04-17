using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SFP
{
    public partial class MeuPerfil : System.Web.UI.Page
    {
        GlobalMethod objUtil = new GlobalMethod();
        private int _iIdUser;
        private int IdUserSession
        {
            get
            {
                return objUtil.ValidSessionUser();
            }
            set { _iIdUser = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            string sErrorMessage = string.Empty;
            UserDAO objDao = new UserDAO();
            User objUsuario = objDao.FindByPK(IdUserSession, out sErrorMessage);

            txtEmail.Text = objUsuario.Email;
            txtLogin.Text = objUsuario.Login;
            txtNome.Text = objUsuario.Name;
            txtSenha.Attributes["Value"] = objUsuario.Password;
            txtNome.Focus();

            gvAcesso.DataSource = new AcessDAO(IdUserSession).FindByWhere(string.Empty, "5", out sErrorMessage);
            gvAcesso.DataBind();

        }

        protected void btGravar_OnClick(object sender, EventArgs e)
        {
            string sErrorMessage = string.Empty;
            UserDAO objDao = new UserDAO();
            User objUsuario = objDao.FindByPK(IdUserSession, out sErrorMessage);

            if (txtLogin.Text.Trim().Equals("DEMO"))
            {
                TrataMsgPrincipal("Usuário DEMO não tem acesso para alteração, crie seu próprio usuário! :)");
                return;
            }

            objUsuario.Name = txtNome.Text;
            objUsuario.Email = txtEmail.Text;
            objUsuario.Password = txtSenha.Text;

            objDao.Save(objUsuario, out sErrorMessage);
            TrataMsgPrincipal(sErrorMessage);

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
    }
}