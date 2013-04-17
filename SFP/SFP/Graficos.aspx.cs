using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Trirand.Web.UI.WebControls;
using SFP.MODEL;
using System.Globalization;
using System.Data;

namespace SFP
{
    public partial class Graficos : System.Web.UI.Page
    {
        GlobalMethod objUtil = new GlobalMethod();
        private int _IdUserSession;
        private int IdUserSession
        {
            get
            {
                return objUtil.ValidSessionUser();
            }
            set { _IdUserSession = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            DateTime dtMes = new DateTime();
            DateTime.TryParse("01/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString(), out dtMes);
            txtDataDe.Text = dtMes.ToShortDateString();
            txtDataAte.Text = dtMes.AddMonths(1).AddDays(-1).ToShortDateString();
            txtDataAte.Attributes.Add("onKeyPress", "FormataData(this,event)");
            txtDataDe.Attributes.Add("onKeyPress", "FormataData(this,event)");

            for (int i = -2; i < 3; i++)
            {
                ddlYear.Items.Add(new ListItem((DateTime.Now.Year + i).ToString()));
            }
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void btPesquisarColuna_OnClick(object sender, EventArgs e)
        {

            string sErro = string.Empty;

            string sWhere = " DATAVENCIMENTO BETWEEN CONVERT(CHAR(10),'" + DateTime.Parse("01/01/" + ddlYear.SelectedValue).ToString("yyyyMMdd") + "', 101) AND CONVERT(CHAR(10),'" +
                DateTime.Parse("31/12/" + ddlYear.SelectedValue).ToString("yyyyMMdd") + "', 101)";

            List<AccountPayable> listContas = new AccountPayableDAO(IdUserSession).FindByWhere(sWhere, out sErro);

            DataTable dtAno = CreateTableYear();
            Dictionary<string, decimal> listTopCategorias = new Dictionary<string, decimal>();

            //Preenchendo o DataTable com as informações
            foreach (AccountPayable pConta in listContas)
            {
                string sCategoriaDescricao = pConta.CategoryDescription;

                if (ddlType.SelectedValue.Equals("Total"))
                {
                    sCategoriaDescricao = "Total";//QualMes(pConta.DataVencimento.Month, true);

                }

                DataRow[] dr = dtAno.Select("CATEGORIA = '" + sCategoriaDescricao + "'");
                string sMesIndex = WichMonth(pConta.MaturityDate.Month);
                //Verificar se na tabela já existe a categoria
                if (dr != null && dr.Length > 0)
                {
                    int iIndex = int.Parse(dr[0]["INDEX"].ToString());
                    //Se existir verifica qual é o Mês desta conta e preenche a tabela referente ao mes
                    dtAno.Rows[iIndex][sMesIndex] =
                        Decimal.Parse(dtAno.Rows[iIndex][sMesIndex].ToString()) + pConta.TotalPrice;
                }
                else
                {
                    DataRow dr1 = dtAno.NewRow();
                    dr1["CATEGORIA"] = sCategoriaDescricao;
                    dr1[sMesIndex] = pConta.TotalPrice;
                    dtAno.Rows.Add(dr1);

                    for (int i = 0; i < dtAno.Rows.Count; i++)
                    {
                        dtAno.Rows[i]["INDEX"] = i;
                    }
                }
            }

            foreach (DataRow pLinha in dtAno.Rows)
            {
                ChartPointCollection pCollection = new ChartPointCollection();
                for (int i = 1; i < 13; i++)
                {
                    ChartPoint p = new ChartPoint();
                    p.Y = Double.Parse(pLinha[WichMonth(i)].ToString());
                    pCollection.Add(p);
                }

                ChartSeriesSettings c = new ChartSeriesSettings();
                c.Data = pCollection;
                c.Name = pLinha["CATEGORIA"].ToString();

                ColumnChart.Series.Add(c);
            }

        }

        private string WichMonth(int iMes, bool bInteiro = false)
        {
            switch (iMes)
            {
                case 1: return (bInteiro ? "Janeiro" : "JAN");
                case 2: return (bInteiro ? "Fevereiro" : "FEV");
                case 3: return (bInteiro ? "Março" : "MAR");
                case 4: return (bInteiro ? "Abril" : "ABR");
                case 5: return (bInteiro ? "Maio" : "MAI");
                case 6: return (bInteiro ? "Junho" : "JUN");
                case 7: return (bInteiro ? "Julho" : "JUL");
                case 8: return (bInteiro ? "Agosto" : "AGO");
                case 9: return (bInteiro ? "Setembro" : "SET");
                case 10: return (bInteiro ? "Outubro" : "OUT");
                case 11: return (bInteiro ? "Novembro" : "NOV");
                case 12: return (bInteiro ? "Dezembro" : "DEZ");
                default: return "";
            }
        }

        private DataTable CreateTableYear()
        {
            DataTable dtYear = new DataTable();
            //Criando as Colunas
            dtYear.Columns.Add("INDEX");
            dtYear.Columns.Add("CATEGORIA");

            dtYear.Columns.Add(ColumnDecimal("JAN"));
            dtYear.Columns.Add(ColumnDecimal("FEV"));
            dtYear.Columns.Add(ColumnDecimal("MAR"));
            dtYear.Columns.Add(ColumnDecimal("ABR"));
            dtYear.Columns.Add(ColumnDecimal("MAI"));
            dtYear.Columns.Add(ColumnDecimal("JUN"));
            dtYear.Columns.Add(ColumnDecimal("JUL"));
            dtYear.Columns.Add(ColumnDecimal("AGO"));
            dtYear.Columns.Add(ColumnDecimal("SET"));
            dtYear.Columns.Add(ColumnDecimal("OUT"));
            dtYear.Columns.Add(ColumnDecimal("NOV"));
            dtYear.Columns.Add(ColumnDecimal("DEZ"));
            return dtYear;
        }


        private DataColumn ColumnDecimal(string sCaption)
        {
            DataColumn dc = new DataColumn();
            dc.DefaultValue = 0;
            dc.Caption = sCaption;
            dc.ColumnName = sCaption;
            return dc;
        }



        protected void btPesquisar_OnClick(object sender, EventArgs e)
        {
            string sErro = string.Empty;

            string sWhere = " DATAVENCIMENTO BETWEEN CONVERT(CHAR(10),'" + DateTime.Parse(txtDataDe.Text).ToString("yyyyMMdd") + "', 101) AND CONVERT(CHAR(10),'" + DateTime.Parse(txtDataAte.Text).ToString("yyyyMMdd") + "', 101)";

            List<AccountPayable> listContas = new AccountPayableDAO(IdUserSession).FindByWhere(sWhere, out sErro);
            ChartPointCollection pCollection = new ChartPointCollection();

            Dictionary<string, decimal> listTopCategorias = new Dictionary<string, decimal>();
            decimal dValorTotal = 0;
            foreach (AccountPayable pConta in listContas)
            {
                if (listTopCategorias.ContainsKey(pConta.CategoryDescription))
                {
                    listTopCategorias[pConta.CategoryDescription] += pConta.TotalPrice;
                }
                else
                {
                    listTopCategorias.Add(pConta.CategoryDescription, pConta.TotalPrice);
                }

                dValorTotal += pConta.TotalPrice;
            }

            var list = listTopCategorias.OrderBy(p => p.Value);
            foreach (KeyValuePair<string, decimal> pConta in list)
            {
                ChartPoint p = new ChartPoint();
                p.Text = pConta.Key;
                decimal dPercentual = (pConta.Value * 100) / dValorTotal;
                p.Y = Double.Parse(ValorComFormatacao(dPercentual, 2).ToString());

                pCollection.Add(p);
            }

            ChartSeriesSettings c = new ChartSeriesSettings();
            c.Data = pCollection;

            PieChart.Type = ChartType.Pie;
            PieChart.Series.Add(c);
        }

        public static string ValorComFormatacao(Decimal valor, short decimais)
        {
            // Converte para decimal
            decimal vlr = valor;
            // Obtemos o formato corrente de n£mero para a cultura Atual (da Thread    corrente)...
            NumberFormatInfo culturaAtual = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
            // Ajustamos a quantidade de casas decimais para o desejado...
            culturaAtual.NumberDecimalDigits = decimais;
            // Retorna o n£mero formatado conforme a cultura atual
            return (vlr.ToString("N", culturaAtual));
        }
    }
}