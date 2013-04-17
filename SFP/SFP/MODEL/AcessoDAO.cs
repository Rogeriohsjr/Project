using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace SFP
{
    public class AcessoDAO : ICRUD<SFP.ENTIDADES.Acesso>
    {
        StringBuilder sCommand = new StringBuilder();
        private Int32 IdUsuarioResponse;

        public AcessoDAO(Int32 IdUsuarioResponse)
        {
            this.IdUsuarioResponse = IdUsuarioResponse;
        }

        public override void Create(ENTIDADES.Acesso pAcesso, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            try
            {
                sCommand.AppendFormat("INSERT INTO TBACESSO (");
                sCommand.AppendFormat(" DATA, ");
                sCommand.AppendFormat(" IDUSUARIO, ");
                sCommand.AppendFormat(" IP  ");
                sCommand.AppendFormat(" ) VALUES  (");
                sCommand.AppendFormat("'{0}', ", pAcesso.Date.ToString("yyyyMMdd HH:mm:ss"));
                sCommand.AppendFormat("'{0}', ", IdUsuarioResponse);
                sCommand.AppendFormat("'{0}'  ", pAcesso.Ip);
                sCommand.AppendFormat(" ) ");
                iLine = 10;
                Command(sCommand.ToString(), out sError);
            }
            catch (Exception ex)
            {
                sError = "AcessoDAO - Create - Line:" + iLine + " - " + ex.Message;
            }
        }

        public override void Delete(int pId, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            try
            {
                sCommand.AppendFormat("DELETE FROM TBACESSO WHERE ID = {0}", pId);
                iLine = 10;
                Command(sCommand.ToString(), out sError);
            }
            catch (Exception ex)
            {
                sError = "AcessoDAO - Delete - Line:" + iLine + " - " + ex.Message;
            }
        }

        public override void Update(ENTIDADES.Acesso pAcesso, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            try
            {
                sCommand.AppendFormat("UPDATE TBACESSO SET ");
                sCommand.AppendFormat("DATA = '{0}', ", pAcesso.Date.ToString("yyyyMMdd"));
                sCommand.AppendFormat("IDUSUARIO = '{0}', ", IdUsuarioResponse);
                sCommand.AppendFormat("IP = '{0}'  ", pAcesso.Ip);
                sCommand.AppendFormat("WHERE ID = {0}", pAcesso.Id);
                iLine = 10;
                Command(sCommand.ToString(), out sError);
            }
            catch (Exception ex)
            {
                sError = "AcessoDAO - Update - Line:" + iLine + " - " + ex.Message;
            }
        }

        public override ENTIDADES.Acesso FindByPK(int pId, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            ENTIDADES.Acesso objAcesso = new ENTIDADES.Acesso();

            try
            {
                sCommand.AppendFormat("SELECT ");
                sCommand.AppendFormat("ID, ");
                sCommand.AppendFormat("DATA,   ");
                sCommand.AppendFormat("IDUSUARIO,   ");
                sCommand.AppendFormat("IP    ");
                sCommand.AppendFormat("FROM TBACESSO WHERE IDUSUARIO = {0} AND ID = {1}", IdUsuarioResponse, pId);
                iLine = 10;
                DataTable dtDados = CommandSelect(sCommand.ToString(), out sError);
                iLine = 20;
                if (dtDados.Rows.Count > 0)
                {
                    objAcesso.Id = Int32.Parse(dtDados.Rows[0][0].ToString());
                    iLine = 21;
                    objAcesso.Date = DateTime.Parse(dtDados.Rows[0][1].ToString());
                    iLine = 22;
                    objAcesso.Usuario = new UsuarioDAO().FindByPK(Int32.Parse(dtDados.Rows[0][2].ToString()), out sError);
                    iLine = 23;
                    objAcesso.Ip = dtDados.Rows[0][3].ToString();
                }

            }
            catch (Exception ex)
            {
                sError = "AcessoDAO - FindByPK - Line:" + iLine + " - " + ex.Message;
            }
            return objAcesso;
        }

        public override List<ENTIDADES.Acesso> FindByWhere(string pWhere, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            List<ENTIDADES.Acesso> listAcesso = new List<ENTIDADES.Acesso>();
            if (String.IsNullOrWhiteSpace(pWhere))
                pWhere = " 0=0 ";
            try
            {
                sCommand.AppendFormat("SELECT ");
                sCommand.AppendFormat("ID, ");
                sCommand.AppendFormat("DATA,   ");
                sCommand.AppendFormat("IDUSUARIO,   ");
                sCommand.AppendFormat("IP    ");
                sCommand.AppendFormat("FROM TBACESSO WHERE IDUSUARIO = {0} AND {1}", IdUsuarioResponse, pWhere);
                iLine = 10;
                DataTable dtDados = CommandSelect(sCommand.ToString(), out sError);
                iLine = 20;
                for (int i = 0; i < dtDados.Rows.Count; i++)
                {
                    ENTIDADES.Acesso objAcesso = new ENTIDADES.Acesso();
                    objAcesso.Id = Int32.Parse(dtDados.Rows[i][0].ToString());
                    iLine = 21;
                    objAcesso.Date = DateTime.Parse(dtDados.Rows[i][1].ToString());
                    iLine = 22;
                    objAcesso.Usuario = new UsuarioDAO().FindByPK(Int32.Parse(dtDados.Rows[i][2].ToString()), out sError);
                    iLine = 23;
                    objAcesso.Ip = dtDados.Rows[i][3].ToString();

                    listAcesso.Add(objAcesso);
                }
            }
            catch (Exception ex)
            {
                sError = "AcessoDAO - FindByWhere - Line:" + iLine + " - " + ex.Message;
            }
            return listAcesso;
        }

        public override void Save(ENTIDADES.Acesso pObj, out string sError)
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

        public List<ENTIDADES.Acesso> FindByWhere(string pWhere, string sTop, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            List<ENTIDADES.Acesso> listAcesso = new List<ENTIDADES.Acesso>();
            if (String.IsNullOrWhiteSpace(pWhere))
                pWhere = " 0=0 ";
            try
            {
                sCommand.AppendFormat("SELECT ");
                sCommand.AppendFormat("TOP {0} ", sTop);
                sCommand.AppendFormat("ID, ");
                sCommand.AppendFormat("DATA,   ");
                sCommand.AppendFormat("IDUSUARIO,   ");
                sCommand.AppendFormat("IP    ");
                sCommand.AppendFormat("FROM TBACESSO WHERE IDUSUARIO = {0} AND {1} ORDER BY DATA DESC",IdUsuarioResponse, pWhere);
                iLine = 10;
                DataTable dtDados = CommandSelect(sCommand.ToString(), out sError);
                iLine = 20;
                for (int i = 0; i < dtDados.Rows.Count; i++)
                {
                    ENTIDADES.Acesso objAcesso = new ENTIDADES.Acesso();
                    objAcesso.Id = Int32.Parse(dtDados.Rows[i][0].ToString());
                    iLine = 21;
                    objAcesso.Date = DateTime.Parse(dtDados.Rows[i][1].ToString());
                    iLine = 22;
                    objAcesso.Usuario = new UsuarioDAO().FindByPK(Int32.Parse(dtDados.Rows[i][2].ToString()), out sError);
                    iLine = 23;
                    objAcesso.Ip = dtDados.Rows[i][3].ToString();

                    listAcesso.Add(objAcesso);
                }
            }
            catch (Exception ex)
            {
                sError = "AcessoDAO - FindByWhere - Line:" + iLine + " - " + ex.Message;
            }
            return listAcesso;
        }
    }
}