using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Globalization;

namespace SFP
{
    public class ContasAPagarDAO : ICRUD<ContasAPagar>
    {
        StringBuilder sCommand = new StringBuilder();
        public Int32 IdUsuarioResponse;

        public ContasAPagarDAO(Int32 idUsuario)
        {
            this.IdUsuarioResponse = idUsuario;
        }


        public override void Create(ContasAPagar pContas, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            try
            {
                sCommand.AppendFormat("INSERT INTO TBCONTASAPAGAR (");
                sCommand.AppendFormat(" IDCATEGORIA, ");
                sCommand.AppendFormat(" DATAVENCIMENTO, ");
                sCommand.AppendFormat(" VALORTOTAL, ");
                sCommand.AppendFormat(" DESCRICAO, ");
                sCommand.AppendFormat(" PAGO_DATA, ");
                sCommand.AppendFormat(" DOCUMENTO, ");
                sCommand.AppendFormat(" HISTORICO, ");
                sCommand.AppendFormat(" PAGO_VALOR, ");
                sCommand.AppendFormat(" IDUSUARIO ");
                sCommand.AppendFormat(" ) VALUES  (");
                sCommand.AppendFormat("'{0}', ", pContas.Categoria.IdCategoria);
                sCommand.AppendFormat("'{0}', ", pContas.DataVencimento.ToString("yyyyMMdd"));
                sCommand.AppendFormat("'{0}', ", pContas.ValorTotal.ToString().Replace(",", "."));
                sCommand.AppendFormat("'{0}', ", pContas.Descricao);
                sCommand.AppendFormat("'{0}', ", (pContas.DataPagamento.HasValue ? pContas.DataPagamento.Value.ToString("yyyyMMdd") : null));
                sCommand.AppendFormat("'{0}', ", pContas.Documento);
                sCommand.AppendFormat("'{0}', ", pContas.Historico);
                sCommand.AppendFormat("'{0}', ", pContas.ValorPago.ToString().Replace(",", "."));
                sCommand.AppendFormat("'{0}'  ", pContas.Usuario.IdUsuario);
                sCommand.AppendFormat(" ) ");
                iLine = 10;
                Command(sCommand.ToString(), out sError);
            }
            catch (Exception ex)
            {
                sError = "ContasAPagarDAO - Create - Line:" + iLine + " - " + ex.Message;
            }
        }

        public override void Delete(int pId, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            try
            {
                sCommand.AppendFormat("DELETE FROM TBCONTASAPAGAR WHERE ID = {0}", pId);
                iLine = 10;
                Command(sCommand.ToString(), out sError);
            }
            catch (Exception ex)
            {
                sError = "ContasAPagarDAO - Delete - Line:" + iLine + " - " + ex.Message;
            }
        }

        public override void Update(ContasAPagar pContas, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            try
            {
                sCommand.AppendFormat("UPDATE TBCONTASAPAGAR SET ");
                sCommand.AppendFormat("IDCATEGORIA = '{0}', ", pContas.Categoria.IdCategoria);
                sCommand.AppendFormat("DATAVENCIMENTO = '{0}', ", pContas.DataVencimento.ToString("yyyyMMdd"));
                sCommand.AppendFormat("VALORTOTAL = '{0}', ", pContas.ValorTotal.ToString().Replace(",", "."));
                sCommand.AppendFormat("DESCRICAO = '{0}', ", pContas.Descricao);
                sCommand.AppendFormat("PAGO_DATA = '{0}', ", (pContas.DataPagamento.HasValue ? pContas.DataPagamento.Value.ToString("yyyyMMdd") : null));
                sCommand.AppendFormat("DOCUMENTO = '{0}', ", pContas.Documento);
                sCommand.AppendFormat("HISTORICO = '{0}', ", pContas.Historico);
                sCommand.AppendFormat("PAGO_VALOR = '{0}', ", pContas.ValorPago.ToString().Replace(",", "."));
                sCommand.AppendFormat("IDUSUARIO = '{0}' ", pContas.Usuario.IdUsuario);
                sCommand.AppendFormat("WHERE ID = {0}", pContas.IdContasAPagar);
                iLine = 10;
                Command(sCommand.ToString(), out sError);
            }
            catch (Exception ex)
            {
                sError = "ContasAPagarDAO - Update - Line:" + iLine + " - " + ex.Message;
            }
        }

        public override ContasAPagar FindByPK(int pId, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            ContasAPagar objContas = new ContasAPagar();

            try
            {
                sCommand.AppendFormat("SELECT ");
                sCommand.AppendFormat(" ID, ");
                sCommand.AppendFormat(" IDCATEGORIA, ");
                sCommand.AppendFormat(" DATAVENCIMENTO, ");
                sCommand.AppendFormat(" VALORTOTAL, ");
                sCommand.AppendFormat(" DESCRICAO, ");
                sCommand.AppendFormat(" PAGO_DATA, ");
                sCommand.AppendFormat(" DOCUMENTO, ");
                sCommand.AppendFormat(" HISTORICO, ");
                sCommand.AppendFormat(" PAGO_VALOR, ");
                sCommand.AppendFormat(" IDUSUARIO ");
                sCommand.AppendFormat("FROM TBCONTASAPAGAR WHERE IDUSUARIO = {0} AND ID = {0}", IdUsuarioResponse, pId);
                iLine = 10;
                DataTable dtDados = CommandSelect(sCommand.ToString(), out sError);
                iLine = 20;
                if (dtDados.Rows.Count > 0)
                {
                    objContas.IdContasAPagar = Int32.Parse(dtDados.Rows[0][0].ToString());
                    objContas.Categoria = new CategoriaDAO(IdUsuarioResponse).FindByPK(Int32.Parse(dtDados.Rows[0][1].ToString()), out sError);
                    objContas.DataVencimento = DateTime.Parse(dtDados.Rows[0][2].ToString());
                    objContas.ValorTotal = Decimal.Parse(dtDados.Rows[0][3].ToString());
                    objContas.Descricao = dtDados.Rows[0][4].ToString();
                    if (dtDados.Rows[0][5].ToString() != "")
                        objContas.DataPagamento = DateTime.Parse(dtDados.Rows[0][5].ToString());
                    objContas.Documento = dtDados.Rows[0][6].ToString();
                    objContas.Historico = dtDados.Rows[0][7].ToString();
                    objContas.ValorPago = Decimal.Parse(dtDados.Rows[0][8].ToString());
                    objContas.Usuario = new UsuarioDAO().FindByPK(Int32.Parse(dtDados.Rows[0][9].ToString()), out sError);
                }
            }
            catch (Exception ex)
            {
                sError = "ContasAPagarDAO - FindByPK - Line:" + iLine + " - " + ex.Message;
            }
            return objContas;
        }

        public override List<ContasAPagar> FindByWhere(string pWhere, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            List<ContasAPagar> listaContas = new List<ContasAPagar>();
            if (String.IsNullOrWhiteSpace(pWhere))
                pWhere = " 0=0 ";

            try
            {
                sCommand.AppendFormat("SELECT ");
                sCommand.AppendFormat(" ID, ");
                sCommand.AppendFormat(" IDCATEGORIA, ");
                sCommand.AppendFormat(" DATAVENCIMENTO, ");
                sCommand.AppendFormat(" VALORTOTAL, ");
                sCommand.AppendFormat(" DESCRICAO, ");
                sCommand.AppendFormat(" PAGO_DATA, ");
                sCommand.AppendFormat(" DOCUMENTO, ");
                sCommand.AppendFormat(" HISTORICO, ");
                sCommand.AppendFormat(" PAGO_VALOR, ");
                sCommand.AppendFormat(" IDUSUARIO ");
                sCommand.AppendFormat("FROM TBCONTASAPAGAR WHERE IDUSUARIO = {0} AND {1} ", IdUsuarioResponse, pWhere);
                iLine = 10;
                DataTable dtDados = CommandSelect(sCommand.ToString(), out sError);
                iLine = 20;
                for (int i = 0; i < dtDados.Rows.Count; i++)
                {
                    ContasAPagar objContas = new ContasAPagar();
                    objContas.IdContasAPagar = Int32.Parse(dtDados.Rows[i][0].ToString());
                    objContas.Categoria = new CategoriaDAO(IdUsuarioResponse).FindByPK(Int32.Parse(dtDados.Rows[i][1].ToString()), out sError);
                    objContas.DataVencimento = DateTime.Parse(dtDados.Rows[i][2].ToString());
                    objContas.ValorTotal = Decimal.Parse(dtDados.Rows[i][3].ToString());
                    objContas.Descricao = dtDados.Rows[i][4].ToString();
                    if (dtDados.Rows[i][5].ToString() != "")
                        objContas.DataPagamento = DateTime.Parse(dtDados.Rows[i][5].ToString());
                    objContas.Documento = dtDados.Rows[i][6].ToString();
                    objContas.Historico = dtDados.Rows[i][7].ToString();
                    if (dtDados.Rows[i][8].ToString() != "")
                        objContas.ValorPago = Decimal.Parse(dtDados.Rows[i][8].ToString());
                    objContas.Usuario = new UsuarioDAO().FindByPK(Int32.Parse(dtDados.Rows[i][9].ToString()), out sError);
                    listaContas.Add(objContas);
                }
            }
            catch (Exception ex)
            {
                sError = "ContasAPagarDAO - FindByWhere - Line:" + iLine + " - " + ex.Message;
            }
            return listaContas;
        }

        public override void Save(ContasAPagar pObj, out string sError)
        {
            if (pObj.IdContasAPagar == 0)
            {
                Create(pObj, out sError);
            }
            else
            {
                Update(pObj, out sError);
            }
        }
    }
}