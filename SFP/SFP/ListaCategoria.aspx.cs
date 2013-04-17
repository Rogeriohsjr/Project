using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SFP
{
    public partial class ListaCategoria : System.Web.UI.Page
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

            btPesquisar_Onclick(sender, e);
        }
        protected void gvCategoria_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Find the checkbox control in header and add an attribute
                ((CheckBox)e.Row.FindControl("cbCategoriaSelectDeselectAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                        ((CheckBox)e.Row.FindControl("cbCategoriaSelectDeselectAll")).ClientID + "','" + gvCategoria.ClientID + "')");
            }
        }

        protected void btPesquisar_Onclick(object sender, EventArgs e)
        {
            string sWhere = string.Empty;

            if (txtDescricao.Text.Trim() != string.Empty)
            {
                if (sWhere != string.Empty)
                    sWhere += " AND ";
                sWhere += " DESCRICAO LIKE '%" + txtDescricao.Text + "%'";
            }

            CarregarGrid(sWhere);
        }
        protected void btNovo_OnClick(object sender, EventArgs e)
        {
            lbTitulo.Text = "Nova Categoria";
            txtDescricao.Focus();

            popup_GestaoDeCategoria.Show();

        }
        protected void btAlterar_OnClick(object sender, EventArgs e)
        {
            LimparPopup();
            lbTitulo.Text = "Alterar Categoria";
            List<Category> listCategoria;
            BuscarItensSelecionado(out listCategoria);

            if (listCategoria.Count == 1)
            {
                foreach (Category pCategoria in listCategoria)
                {
                    ptxtDescricao.Text = pCategoria.Description;
                    pddlStatus.SelectedValue = (pCategoria.IsBlock ? "1" : "0");
                    hfIdCategoriaSelecionada.Value = pCategoria.Id.ToString();
                }
                popup_GestaoDeCategoria.Show();
            }

        }
        protected void btExcluir_OnClick(object sender, EventArgs e)
        {
            CategoryDAO objSave = new CategoryDAO(IdUserSession);
            string sError = string.Empty;
            List<Category> listCategoria;
            BuscarItensSelecionado(out listCategoria);


            foreach (Category pCategoria in listCategoria)
            {
                objSave.Delete(pCategoria.Id, out sError);
                if (TrataMsgPrincipal(sError))
                    return;
            }

            btPesquisar_Onclick(sender, e);
        }
        protected void btGravar_OnClick(object sender, EventArgs e)
        {
            string sError = string.Empty;
            CategoryDAO objSave = new CategoryDAO(IdUserSession);
            Category objCategoria = new Category();

            objCategoria.Id = int.Parse(hfIdCategoriaSelecionada.Value);
            objCategoria.Description = ptxtDescricao.Text;
            objCategoria.IsBlock = (pddlStatus.SelectedItem.Value.Equals("0") ? false : true);
            objCategoria.User = new UserDAO().FindByPK(IdUserSession, out sError);

            if (TrataMsgPopup(sError))
                return;

            objSave.Save(objCategoria, out sError);
            if (TrataMsgPopup(sError))
                return;

            popup_GestaoDeCategoria.Hide();
            btPesquisar_Onclick(sender, e);
            LimparPopup();
        }
        protected void btFechar_OnClick(object sender, EventArgs e)
        {
            LimparPopup();
            popup_GestaoDeCategoria.Hide();
        }

        private void LimparPopup()
        {
            ptxtDescricao.Text = "";
            lbMsgPoup.Text = "";
            pddlStatus.SelectedIndex = 0;
            hfIdCategoriaSelecionada.Value = "0";

        }
        private bool TrataMsgPopup(string sError)
        {
            if (String.IsNullOrWhiteSpace(sError))
            {
                pnMsgPopup.Visible = false;
                lbMsgPoup.Text = "";
                return false;
            }
            pnMsgPopup.Visible = true;
            lbMsgPoup.Text = sError;
            return true;
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
        private void CarregarGrid(string sWhere = "")
        {
            string sError;
            CategoryDAO objCategoriaDAO = new CategoryDAO(IdUserSession);
            List<Category> listCategoria = new List<Category>();
            listCategoria = objCategoriaDAO.FindByWhere(sWhere, out sError);
            gvCategoria.DataSource = listCategoria;
            if (listCategoria.Count == 0)
            {
                DataTable dtEmpty = new DataTable();
                dtEmpty.Columns.Add("Id");
                dtEmpty.Columns.Add("Description");
                dtEmpty.Columns.Add("IsBlock");
                dtEmpty.Columns.Add("Status");
                
                DataRow dr = dtEmpty.NewRow();
                dr["Id"] = "";
                dr["Description"] = "";
                dr["IsBlock"] = "";
                dr["Status"] = "";
                dtEmpty.Rows.Add(dr);
                gvCategoria.DataSource = dtEmpty;
            }

            if (TrataMsgPrincipal(sError))
                return;

            gvCategoria.DataBind();
            upGrid.Update();

            Session["Grid_gvCategoria"] = gvCategoria.DataSource;

        }

        private void BuscarItensSelecionado(out List<Category> listCategoria)
        {
            listCategoria = new List<Category>();
            foreach (GridViewRow gvrLinha in gvCategoria.Rows)
            {
                CheckBox cbAux = (CheckBox)gvrLinha.FindControl("cbCategoriaSelectedDeselect");
                TextBox txtValor = (TextBox)gvrLinha.FindControl("Id");

                if (cbAux.Checked)
                {
                    listCategoria.Add(((List<Category>)Session["Grid_gvCategoria"])[gvrLinha.DataItemIndex]);

                }
            }
        }
    }
}