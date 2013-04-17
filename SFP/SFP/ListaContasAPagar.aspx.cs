using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SFP.MODEL;
using System.Data;

namespace SFP
{
    public partial class ListaContasAPagar : System.Web.UI.Page
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

        private List<AccountPayable> _Grid_gvListaConta;
        public List<AccountPayable> Grid_gvListaConta
        {
            get
            {
                if (Session["Grid_gvListaConta"] == null)
                {
                    btPesquisar_Onclick(new object(), new EventArgs());
                }

                if (_Grid_gvListaConta == null)
                    _Grid_gvListaConta = ((List<AccountPayable>)Session["Grid_gvListaConta"]);
                return _Grid_gvListaConta;
            }
            set
            {
                Session["Grid_gvListaConta"] = value;
                _Grid_gvListaConta = value;
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            DateTime dtMes = new DateTime();
            DateTime.TryParse("01/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString(), out dtMes);
            txtDataDe.Text = dtMes.ToShortDateString();
            txtDataAte.Text = dtMes.AddMonths(1).AddDays(-1).ToShortDateString();

            AplicandoJS();
            CarregarCategoria();
            btPesquisar_Onclick(sender, e);

        }
        protected void gvListaConta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Find the checkbox control in header and add an attribute
                ((CheckBox)e.Row.FindControl("cbContaSelectDeselectAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                        ((CheckBox)e.Row.FindControl("cbContaSelectDeselectAll")).ClientID + "','" + gvListaConta.ClientID + "')");
            }
        }

        #region SortColumn
        private string _SortExpresstion;
        protected void gvListaConta_Sorting(object sender, GridViewSortEventArgs e)
        {
            _SortExpresstion = e.SortExpression;
            IEnumerable<AccountPayable> ListContasOrder = null;
            switch (e.SortExpression)
            {
                case "CategoryDescription":
                    if (e.SortDirection == SortDirection.Ascending)
                    {
                        ListContasOrder = Grid_gvListaConta.OrderBy(p => p.CategoryDescription);
                    }
                    else
                    {
                        ListContasOrder = Grid_gvListaConta.OrderByDescending(p => p.CategoryDescription);
                    }
                    break;
                case "Description":
                    if (e.SortDirection == SortDirection.Ascending)
                    {
                        ListContasOrder = Grid_gvListaConta.OrderBy(p => p.Description);
                    }
                    else
                    {
                        ListContasOrder = Grid_gvListaConta.OrderByDescending(p => p.Description);
                    }
                    break;
                case "MaturityDate":
                    if (e.SortDirection == SortDirection.Ascending)
                    {
                        ListContasOrder = Grid_gvListaConta.OrderBy(p => p.MaturityDate);
                    }
                    else
                    {
                        ListContasOrder = Grid_gvListaConta.OrderByDescending(p => p.MaturityDate);
                    }
                    break;
                case "TotalPrice":
                    if (e.SortDirection == SortDirection.Ascending)
                    {
                        ListContasOrder = Grid_gvListaConta.OrderBy(p => p.TotalPrice);
                    }
                    else
                    {
                        ListContasOrder = Grid_gvListaConta.OrderByDescending(p => p.TotalPrice);
                    }
                    break;
                case "DatePayment":
                    if (e.SortDirection == SortDirection.Ascending)
                    {
                        ListContasOrder = Grid_gvListaConta.OrderBy(p => p.DatePayment);
                    }
                    else
                    {
                        ListContasOrder = Grid_gvListaConta.OrderByDescending(p => p.DatePayment);
                    }
                    break;
                case "PricePayment":
                    if (e.SortDirection == SortDirection.Ascending)
                    {
                        ListContasOrder = Grid_gvListaConta.OrderBy(p => p.PricePayment);
                    }
                    else
                    {
                        ListContasOrder = Grid_gvListaConta.OrderByDescending(p => p.PricePayment);
                    }
                    break;

                // case statements for your other fields.
            }

            if (ListContasOrder != null)
            {
                Grid_gvListaConta = (List<AccountPayable>)ListContasOrder.ToList() ;
                gvListaConta.DataSource = Grid_gvListaConta;
                gvListaConta.DataBind();
                upGrid.Update();
            }


        }
        protected void gvListaConta_OnRowCreated(Object sender, GridViewRowEventArgs e)
        {

            // Use the RowType property to determine whether the 
            // row being created is the header row. 
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // Call the GetSortColumnIndex helper method to determine
                // the index of the column being sorted.
                int sortColumnIndex = GetSortColumnIndex();

                if (sortColumnIndex != -1)
                {
                    // Call the AddSortImage helper method to add
                    // a sort direction image to the appropriate
                    // column header. 
                    AddSortImage(sortColumnIndex, e.Row);
                }
            }
        }
        // This is a helper method used to determine the index of the
        // column being sorted. If no column is being sorted, -1 is returned.
        int GetSortColumnIndex()
        {

            // Iterate through the Columns collection to determine the index
            // of the column being sorted.
            foreach (DataControlField field in gvListaConta.Columns)
            {
                if (field.SortExpression == _SortExpresstion)
                {
                    return gvListaConta.Columns.IndexOf(field);
                }
            }

            return -1;
        }
        // This is a helper method used to add a sort direction
        // image to the header of the column being sorted.
        void AddSortImage(int columnIndex, GridViewRow headerRow)
        {

            // Create the sorting image based on the sort direction.
            Image sortImage = new Image();
            if (gvListaConta.SortDirection == SortDirection.Ascending)
            {
                sortImage.ImageUrl = "App_Themes/Green/Image/cpDownDoubleReverse.png";
                sortImage.AlternateText = "Ascending Order";
            }
            else
            {
                sortImage.ImageUrl = "App_Themes/Green/Image/cpUpDoubleReverse.png";
                sortImage.AlternateText = "Descending Order";
            }

            // Add the image to the appropriate header cell.
            headerRow.Cells[columnIndex].Controls.Add(sortImage);

        }
        #endregion

        protected void btPesquisar_Onclick(object sender, EventArgs e)
        {
            string sWhere = string.Empty;

            if (ddlCategoria.SelectedValue != "")
                sWhere += " idCategoria = " + ddlCategoria.SelectedItem.Value;

            if (txtDescricao.Text.Trim() != string.Empty)
            {
                if (sWhere != string.Empty)
                    sWhere += " AND ";
                sWhere += " DESCRICAO LIKE '%" + txtDescricao.Text + "%'";
            }

            if ((String.IsNullOrWhiteSpace(txtDataDe.Text) && String.IsNullOrWhiteSpace(txtDataAte.Text)))
            {
                TrataMsgPrincipal("Campo Data é obrigatório");
                return;
            }
            else
            {
                if (sWhere != string.Empty)
                    sWhere += " AND ";
                sWhere += " DATAVENCIMENTO BETWEEN CONVERT(CHAR(10),'" + DateTime.Parse(txtDataDe.Text).ToString("yyyyMMdd") + "', 101) AND CONVERT(CHAR(10),'" + DateTime.Parse(txtDataAte.Text).ToString("yyyyMMdd") + "', 101)";
            }

            CarregarGrid(sWhere);

            if (Grid_gvListaConta is List<AccountPayable>)
                ResumoDetalhe(Grid_gvListaConta);

        }
        protected void btNovo_OnClick(object sender, EventArgs e)
        {
            LimparPopup();
            lbTitulo.Text = "Nova Conta";
            tdJaPago.Visible = true;
            ptxtDataVencimento.Text = DateTime.Now.ToShortDateString();
            pddlCategoria.Focus();

            popup_GestaoDeContas.Show();

        }
        protected void btAlterar_OnClick(object sender, EventArgs e)
        {
            LimparPopup();
            lbTitulo.Text = "Alterar Conta";
            ptxtQtdParcela.ReadOnly = true;
            List<AccountPayable> listConta;
            BuscarItensSelecionado(out listConta);

            if (listConta.Count == 1)
            {
                foreach (AccountPayable pConta in listConta)
                {
                    pddlCategoria.SelectedValue = pConta.Category.Id.ToString();
                    ptxtDescricao.Text = pConta.Description;
                    ptxtDataVencimento.Text = pConta.MaturityDate.ToShortDateString();
                    ptxtValorTotal.Text = pConta.TotalPrice.ToString();
                    ptxtDataPagamento.Text = pConta.DatePaymentFormated;
                    ptxtValorPago.Text = pConta.PricePayment.ToString();
                    ptxtDocumento.Text = pConta.Document;
                    ptxtHistorico.Text = pConta.Historic;
                    hfIdContaSelecionada.Value = pConta.Id.ToString();
                }

                popup_GestaoDeContas.Show();
            }

        }
        protected void btPagar_OnClick(object sender, EventArgs e)
        {
            LimparPopup();

            List<AccountPayable> listConta;
            BuscarItensSelecionado(out listConta);

            if (listConta.Count == 1)
            {
                lbTitulo.Text = "Pagar Conta";
                pbtGravar.Text = "Pagar Conta";
                ptxtQtdParcela.ReadOnly = true;

                foreach (AccountPayable pConta in listConta)
                {
                    pddlCategoria.SelectedValue = pConta.Category.Id.ToString();
                    ptxtDescricao.Text = pConta.Description;
                    ptxtDataVencimento.Text = pConta.MaturityDate.ToShortDateString();
                    ptxtValorTotal.Text = pConta.TotalPrice.ToString();
                    ptxtDataPagamento.Text = (pConta.DatePayment.ToString() == "" ? DateTime.Now.ToShortDateString() : pConta.DatePayment.Value.ToShortDateString());
                    ptxtValorPago.Text = (pConta.PricePayment != 0 ? pConta.PricePayment.ToString() : pConta.TotalPrice.ToString());
                    ptxtDocumento.Text = pConta.Document;
                    ptxtHistorico.Text = pConta.Historic;
                    hfIdContaSelecionada.Value = pConta.Id.ToString();
                }

                popup_GestaoDeContas.Show();
            }

        }
        protected void btExcluir_OnClick(object sender, EventArgs e)
        {
            AccountPayableDAO objSave = new AccountPayableDAO(IdUserSession);
            string sError = string.Empty;
            List<AccountPayable> listConta;
            BuscarItensSelecionado(out listConta);

            foreach (AccountPayable pConta in listConta)
            {
                objSave.Delete(pConta.Id, out sError);
                if (TrataMsgPrincipal(sError))
                    return;
            }

            btPesquisar_Onclick(sender, e);
        }
        protected void btGravar_OnClick(object sender, EventArgs e)
        {
            string sError;
            AccountPayableDAO objSave = new AccountPayableDAO(IdUserSession);
            AccountPayable objContas = new AccountPayable();

            objContas.Id = int.Parse(hfIdContaSelecionada.Value);
            objContas.Category = new CategoryDAO(IdUserSession)
                .FindByPK(int.Parse(pddlCategoria.SelectedItem.Value.ToString()), out sError);
            objContas.Description = ptxtDescricao.Text;
            objContas.MaturityDate = DateTime.Parse(ptxtDataVencimento.Text);
            objContas.TotalPrice = Decimal.Parse(ptxtValorTotal.Text);
            if (!String.IsNullOrWhiteSpace(ptxtDataPagamento.Text))
                objContas.DatePayment = DateTime.Parse(ptxtDataPagamento.Text);
            objContas.PricePayment = Decimal.Parse(ptxtValorPago.Text);
            objContas.Document = ptxtDocumento.Text;
            objContas.Historic = ptxtHistorico.Text;
            objContas.User = new UserDAO().FindByPK(IdUserSession, out sError);

            if (TrataMsgPopup(sError))
                return;
            int iTotalParcela = int.Parse(ptxtQtdParcela.Text);
            string sDescricao = objContas.Description;

            for (int i = 0; i < iTotalParcela; i++)
            {
                if (iTotalParcela > 1)
                {
                    objContas.Description = sDescricao + " " + (i + 1) + "/" + iTotalParcela;
                    if (i != 0)
                        objContas.MaturityDate = objContas.MaturityDate.AddMonths(1);
                }

                objSave.Save(objContas, out sError);
                if (TrataMsgPopup(sError))
                    return;

            }
            popup_GestaoDeContas.Hide();
            btPesquisar_Onclick(sender, e);
        }
        protected void btFechar_OnClick(object sender, EventArgs e)
        {
            popup_GestaoDeContas.Hide();
        }

        private void LimparPopup()
        {
            ptxtDataPagamento.Text = "";
            ptxtDataVencimento.Text = "";
            ptxtDescricao.Text = "";
            ptxtValorTotal.Text = "0";
            ptxtValorPago.Text = "0";
            ptxtDocumento.Text = "";
            ptxtHistorico.Text = "";
            ptxtQtdParcela.Text = "1";
            hfIdContaSelecionada.Value = "0";
            pbtGravar.Text = "Salvar";
            ptxtQtdParcela.ReadOnly = false;
            tdJaPago.Visible = false;
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
            AccountPayableDAO objContasAPagarDao = new AccountPayableDAO(IdUserSession);
            List<AccountPayable> listContas = new List<AccountPayable>();
            listContas = objContasAPagarDao.FindByWhere(sWhere, out sError);
            Grid_gvListaConta = listContas.OrderBy(p => p.MaturityDate).ToList();
            gvListaConta.DataSource = Grid_gvListaConta;
            if (listContas.Count == 0)
            {
                DataTable dtEmpty = new DataTable();
                dtEmpty.Columns.Add("Id");
                dtEmpty.Columns.Add("CategoryDescription");
                dtEmpty.Columns.Add("Description");
                dtEmpty.Columns.Add("MaturityDate");
                dtEmpty.Columns.Add("TotalPrice");
                dtEmpty.Columns.Add("DatePayment");
                dtEmpty.Columns.Add("PricePayment");

                DataRow dr = dtEmpty.NewRow();
                dr["Id"] = "";
                dr["CategoryDescription"] = "";
                dr["Description"] = "";
                dr["MaturityDate"] = "";
                dr["TotalPrice"] = "";
                dr["DatePayment"] = "";
                dr["PricePayment"] = "";
                dtEmpty.Rows.Add(dr);
                gvListaConta.DataSource = dtEmpty;
            }


            if (TrataMsgPrincipal(sError))
                return;
            gvListaConta.DataBind();
            upGrid.Update();

        }
        private void CarregarCategoria()
        {
            string sResult;
            CategoryDAO objCategoriaDAO = new CategoryDAO(IdUserSession);
            ddlCategoria.DataTextField = "Description";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataSource = objCategoriaDAO.FindByWhere("BLOQUEADO = 0 ORDER BY DESCRICAO", out sResult);
            ddlCategoria.DataBind();
            ddlCategoria.Items.Add(new ListItem("[Todas]", ""));
            ddlCategoria.SelectedValue = "";

            pddlCategoria.DataTextField = "Description";
            pddlCategoria.DataValueField = "Id";
            pddlCategoria.DataSource = ddlCategoria.DataSource;
            pddlCategoria.DataBind();
        }
        private void AplicandoJS()
        {
            txtDataAte.Attributes.Add("onKeyPress", "FormataData(this,event)");
            txtDataDe.Attributes.Add("onKeyPress", "FormataData(this,event)");
            ptxtDataPagamento.Attributes.Add("onKeyPress", "FormataData(this,event)");
            ptxtDataVencimento.Attributes.Add("onKeyPress", "FormataData(this,event)");
            ptxtValorPago.Attributes.Add("onKeyPress", "FormataDecimal(this,event)");
            ptxtValorTotal.Attributes.Add("onKeyPress", "FormataDecimal(this,event)");
            ptxtQtdParcela.Attributes.Add("onKeyPress", "FormataNumero(this,event)");

        }

        private void BuscarItensSelecionado(out List<AccountPayable> listContas)
        {
            listContas = new List<AccountPayable>();
            foreach (GridViewRow gvrLinha in gvListaConta.Rows)
            {
                CheckBox cbAux = (CheckBox)gvrLinha.FindControl("cbContaSelectedDeselect");
                TextBox txtValor = (TextBox)gvrLinha.FindControl("Id");

                if (cbAux.Checked)
                {
                    listContas.Add((Grid_gvListaConta)[gvrLinha.DataItemIndex]);

                }
            }
        }
        private void ResumoDetalhe(List<AccountPayable> listContas)
        {
            decimal dValorTotal = 0,
                    dValorTotalPago = 0,
                    dValorRestante = 0;
            Dictionary<string, decimal> listTopCategorias = new Dictionary<string, decimal>();

            foreach (AccountPayable pConta in listContas)
            {
                dValorTotal += pConta.TotalPrice;
                dValorTotalPago += pConta.PricePayment;
                dValorRestante = dValorTotal - dValorTotalPago;

                if (listTopCategorias.ContainsKey(pConta.CategoryDescription))
                {
                    listTopCategorias[pConta.CategoryDescription] += pConta.TotalPrice;
                }
                else
                {
                    listTopCategorias.Add(pConta.CategoryDescription, pConta.TotalPrice);
                }
            }

            DataTable dtTopCategoria = new DataTable();
            dtTopCategoria.Columns.Add("Category");


            dtTopCategoria.Columns.Add(CreateColumnCustom("Valor Total", "TotalPrice", true, "System.Decimal"));

            foreach (KeyValuePair<string, decimal> pCategoria in listTopCategorias)
            {
                DataRow dr = dtTopCategoria.NewRow();
                dr["Category"] = pCategoria.Key;
                dr["TotalPrice"] = pCategoria.Value;
                dtTopCategoria.Rows.Add(dr);
            }
            DataView dvView = dtTopCategoria.DefaultView;
            dvView.Sort = " TotalPrice DESC";
            dtTopCategoria = dvView.ToTable();
            gvTopCategoria.DataSource = dtTopCategoria;
            gvTopCategoria.DataBind();

            DataTable dtResumo = new DataTable();
            dtResumo.Columns.Add("Resumo");
            dtResumo.Columns.Add(CreateColumnCustom("Valor Total", "TotalPrice", true, "System.Decimal"));
            DataRow drResumo = dtResumo.NewRow();
            drResumo["Resumo"] = "Valor Total:";
            drResumo["TotalPrice"] = dValorTotal;
            dtResumo.Rows.Add(drResumo);
            drResumo = dtResumo.NewRow();
            drResumo["Resumo"] = "Valor Total Pago:";
            drResumo["TotalPrice"] = dValorTotalPago;
            dtResumo.Rows.Add(drResumo);
            drResumo = dtResumo.NewRow();
            drResumo["Resumo"] = "Valor Total a Pagar:";
            drResumo["TotalPrice"] = (dValorRestante > 0 ? dValorRestante : 0);
            dtResumo.Rows.Add(drResumo);
            drResumo = dtResumo.NewRow();
            drResumo["Resumo"] = "Valor Pago a Mais:";
            drResumo["TotalPrice"] = (dValorRestante < 0 ? dValorRestante * -1 : 0);
            dtResumo.Rows.Add(drResumo);
            gvTopTotal.DataSource = dtResumo;
            gvTopTotal.DataBind();

            upCaixaResumo.Update();
        }

        private DataColumn CreateColumnCustom(string sCaption, string sColumnName, bool bAllowNull, string sDataType)
        {
            DataColumn dc = new DataColumn();
            dc.Caption = sCaption;
            dc.ColumnName = sColumnName;
            dc.AllowDBNull = bAllowNull;
            dc.DefaultValue = 0;
            dc.DataType = Type.GetType(sDataType);
            return dc;
        }
    }
}