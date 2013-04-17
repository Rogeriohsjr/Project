using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Globalization;

namespace SFP
{
    public class AccountPayableDAO : ICRUD<AccountPayable>
    {
        StringBuilder sCommand = new StringBuilder();
        private Int32 IdUserControl;

        public AccountPayableDAO(Int32 idUser)
        {
            this.IdUserControl = idUser;
        }


        public override void Create(AccountPayable pAccount, out string sError)
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
                sCommand.AppendFormat("'{0}', ", pAccount.Category.Id);
                sCommand.AppendFormat("'{0}', ", pAccount.MaturityDate.ToString("yyyyMMdd"));
                sCommand.AppendFormat("'{0}', ", pAccount.TotalPrice.ToString().Replace(",", "."));
                sCommand.AppendFormat("'{0}', ", pAccount.Description);
                sCommand.AppendFormat("'{0}', ", (pAccount.DatePayment.HasValue ? pAccount.DatePayment.Value.ToString("yyyyMMdd") : null));
                sCommand.AppendFormat("'{0}', ", pAccount.Document);
                sCommand.AppendFormat("'{0}', ", pAccount.Historic);
                sCommand.AppendFormat("'{0}', ", pAccount.PricePayment.ToString().Replace(",", "."));
                sCommand.AppendFormat("'{0}'  ", pAccount.User.Id);
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

        public override void Update(AccountPayable pAccount, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            try
            {
                sCommand.AppendFormat("UPDATE TBCONTASAPAGAR SET ");
                sCommand.AppendFormat("IDCATEGORIA = '{0}', ", pAccount.Category.Id);
                sCommand.AppendFormat("DATAVENCIMENTO = '{0}', ", pAccount.MaturityDate.ToString("yyyyMMdd"));
                sCommand.AppendFormat("VALORTOTAL = '{0}', ", pAccount.TotalPrice.ToString().Replace(",", "."));
                sCommand.AppendFormat("DESCRICAO = '{0}', ", pAccount.Description);
                sCommand.AppendFormat("PAGO_DATA = '{0}', ", (pAccount.DatePayment.HasValue ? pAccount.DatePayment.Value.ToString("yyyyMMdd") : null));
                sCommand.AppendFormat("DOCUMENTO = '{0}', ", pAccount.Document);
                sCommand.AppendFormat("HISTORICO = '{0}', ", pAccount.Historic);
                sCommand.AppendFormat("PAGO_VALOR = '{0}', ", pAccount.PricePayment.ToString().Replace(",", "."));
                sCommand.AppendFormat("IDUSUARIO = '{0}' ", pAccount.User.Id);
                sCommand.AppendFormat("WHERE ID = {0}", pAccount.Id);
                iLine = 10;
                Command(sCommand.ToString(), out sError);
            }
            catch (Exception ex)
            {
                sError = "ContasAPagarDAO - Update - Line:" + iLine + " - " + ex.Message;
            }
        }

        public override AccountPayable FindByPK(int pId, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            AccountPayable objAccount = new AccountPayable();

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
                sCommand.AppendFormat("FROM TBCONTASAPAGAR WHERE IDUSUARIO = {0} AND ID = {0}", IdUserControl, pId);
                iLine = 10;
                DataTable dtData = CommandSelect(sCommand.ToString(), out sError);
                iLine = 20;
                if (dtData.Rows.Count > 0)
                {
                    objAccount.Id = Int32.Parse(dtData.Rows[0][0].ToString());
                    objAccount.Category = new CategoryDAO(IdUserControl).FindByPK(Int32.Parse(dtData.Rows[0][1].ToString()), out sError);
                    objAccount.MaturityDate = DateTime.Parse(dtData.Rows[0][2].ToString());
                    objAccount.TotalPrice = Decimal.Parse(dtData.Rows[0][3].ToString());
                    objAccount.Description = dtData.Rows[0][4].ToString();
                    if (dtData.Rows[0][5].ToString() != "")
                        objAccount.DatePayment = DateTime.Parse(dtData.Rows[0][5].ToString());
                    objAccount.Document = dtData.Rows[0][6].ToString();
                    objAccount.Historic = dtData.Rows[0][7].ToString();
                    objAccount.PricePayment = Decimal.Parse(dtData.Rows[0][8].ToString());
                    objAccount.User = new UserDAO().FindByPK(Int32.Parse(dtData.Rows[0][9].ToString()), out sError);
                }
            }
            catch (Exception ex)
            {
                sError = "ContasAPagarDAO - FindByPK - Line:" + iLine + " - " + ex.Message;
            }
            return objAccount;
        }

        public override List<AccountPayable> FindByWhere(string pWhere, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            List<AccountPayable> ListAccount = new List<AccountPayable>();
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
                sCommand.AppendFormat("FROM TBCONTASAPAGAR WHERE IDUSUARIO = {0} AND {1} ", IdUserControl, pWhere);
                iLine = 10;
                DataTable dtData = CommandSelect(sCommand.ToString(), out sError);
                iLine = 20;
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    AccountPayable objAccount = new AccountPayable();
                    objAccount.Id = Int32.Parse(dtData.Rows[i][0].ToString());
                    objAccount.Category = new CategoryDAO(IdUserControl).FindByPK(Int32.Parse(dtData.Rows[i][1].ToString()), out sError);
                    objAccount.MaturityDate = DateTime.Parse(dtData.Rows[i][2].ToString());
                    objAccount.TotalPrice = Decimal.Parse(dtData.Rows[i][3].ToString());
                    objAccount.Description = dtData.Rows[i][4].ToString();
                    if (dtData.Rows[i][5].ToString() != "")
                        objAccount.DatePayment = DateTime.Parse(dtData.Rows[i][5].ToString());
                    objAccount.Document = dtData.Rows[i][6].ToString();
                    objAccount.Historic = dtData.Rows[i][7].ToString();
                    if (dtData.Rows[i][8].ToString() != "")
                        objAccount.PricePayment = Decimal.Parse(dtData.Rows[i][8].ToString());
                    objAccount.User = new UserDAO().FindByPK(Int32.Parse(dtData.Rows[i][9].ToString()), out sError);
                    ListAccount.Add(objAccount);
                }
            }
            catch (Exception ex)
            {
                sError = "ContasAPagarDAO - FindByWhere - Line:" + iLine + " - " + ex.Message;
            }
            return ListAccount;
        }

        public override void Save(AccountPayable pObj, out string sError)
        {
            if (pObj.Id == 0)
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