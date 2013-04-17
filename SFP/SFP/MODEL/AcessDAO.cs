using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace SFP
{
    public class AcessDAO : ICRUD<SFP.ENTIDADES.Acess>
    {
        StringBuilder sCommand = new StringBuilder();
        private Int32 IdUserControl;

        public AcessDAO(Int32 IdUserControl)
        {
            this.IdUserControl = IdUserControl;
        }

        public override void Create(ENTIDADES.Acess pAcess, out string sError)
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
                sCommand.AppendFormat("'{0}', ", pAcess.Date.ToString("yyyyMMdd HH:mm:ss"));
                sCommand.AppendFormat("'{0}', ", IdUserControl);
                sCommand.AppendFormat("'{0}'  ", pAcess.Ip);
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

        public override void Update(ENTIDADES.Acess pAcess, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            try
            {
                sCommand.AppendFormat("UPDATE TBACESSO SET ");
                sCommand.AppendFormat("DATA = '{0}', ", pAcess.Date.ToString("yyyyMMdd"));
                sCommand.AppendFormat("IDUSUARIO = '{0}', ", IdUserControl);
                sCommand.AppendFormat("IP = '{0}'  ", pAcess.Ip);
                sCommand.AppendFormat("WHERE ID = {0}", pAcess.Id);
                iLine = 10;
                Command(sCommand.ToString(), out sError);
            }
            catch (Exception ex)
            {
                sError = "AcessoDAO - Update - Line:" + iLine + " - " + ex.Message;
            }
        }

        public override ENTIDADES.Acess FindByPK(int pId, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            ENTIDADES.Acess objAcess = new ENTIDADES.Acess();

            try
            {
                sCommand.AppendFormat("SELECT ");
                sCommand.AppendFormat("ID, ");
                sCommand.AppendFormat("DATA,   ");
                sCommand.AppendFormat("IDUSUARIO,   ");
                sCommand.AppendFormat("IP    ");
                sCommand.AppendFormat("FROM TBACESSO WHERE IDUSUARIO = {0} AND ID = {1}", IdUserControl, pId);
                iLine = 10;
                DataTable dtDados = CommandSelect(sCommand.ToString(), out sError);
                iLine = 20;
                if (dtDados.Rows.Count > 0)
                {
                    objAcess.Id = Int32.Parse(dtDados.Rows[0][0].ToString());
                    iLine = 21;
                    objAcess.Date = DateTime.Parse(dtDados.Rows[0][1].ToString());
                    iLine = 22;
                    objAcess.User = new UserDAO().FindByPK(Int32.Parse(dtDados.Rows[0][2].ToString()), out sError);
                    iLine = 23;
                    objAcess.Ip = dtDados.Rows[0][3].ToString();
                }

            }
            catch (Exception ex)
            {
                sError = "AcessoDAO - FindByPK - Line:" + iLine + " - " + ex.Message;
            }
            return objAcess;
        }

        public override List<ENTIDADES.Acess> FindByWhere(string pWhere, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            List<ENTIDADES.Acess> ListAcess = new List<ENTIDADES.Acess>();
            if (String.IsNullOrWhiteSpace(pWhere))
                pWhere = " 0=0 ";
            try
            {
                sCommand.AppendFormat("SELECT ");
                sCommand.AppendFormat("ID, ");
                sCommand.AppendFormat("DATA,   ");
                sCommand.AppendFormat("IDUSUARIO,   ");
                sCommand.AppendFormat("IP    ");
                sCommand.AppendFormat("FROM TBACESSO WHERE IDUSUARIO = {0} AND {1}", IdUserControl, pWhere);
                iLine = 10;
                DataTable dtDados = CommandSelect(sCommand.ToString(), out sError);
                iLine = 20;
                for (int i = 0; i < dtDados.Rows.Count; i++)
                {
                    ENTIDADES.Acess objAcesso = new ENTIDADES.Acess();
                    objAcesso.Id = Int32.Parse(dtDados.Rows[i][0].ToString());
                    iLine = 21;
                    objAcesso.Date = DateTime.Parse(dtDados.Rows[i][1].ToString());
                    iLine = 22;
                    objAcesso.User = new UserDAO().FindByPK(Int32.Parse(dtDados.Rows[i][2].ToString()), out sError);
                    iLine = 23;
                    objAcesso.Ip = dtDados.Rows[i][3].ToString();

                    ListAcess.Add(objAcesso);
                }
            }
            catch (Exception ex)
            {
                sError = "AcessoDAO - FindByWhere - Line:" + iLine + " - " + ex.Message;
            }
            return ListAcess;
        }

        public override void Save(ENTIDADES.Acess pObj, out string sError)
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

        public List<ENTIDADES.Acess> FindByWhere(string pWhere, string sTop, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            List<ENTIDADES.Acess> ListAcess = new List<ENTIDADES.Acess>();
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
                sCommand.AppendFormat("FROM TBACESSO WHERE IDUSUARIO = {0} AND {1} ORDER BY DATA DESC",IdUserControl, pWhere);
                iLine = 10;
                DataTable dtData = CommandSelect(sCommand.ToString(), out sError);
                iLine = 20;
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    ENTIDADES.Acess objAcess = new ENTIDADES.Acess();
                    objAcess.Id = Int32.Parse(dtData.Rows[i][0].ToString());
                    iLine = 21;
                    objAcess.Date = DateTime.Parse(dtData.Rows[i][1].ToString());
                    iLine = 22;
                    objAcess.User = new UserDAO().FindByPK(Int32.Parse(dtData.Rows[i][2].ToString()), out sError);
                    iLine = 23;
                    objAcess.Ip = dtData.Rows[i][3].ToString();

                    ListAcess.Add(objAcess);
                }
            }
            catch (Exception ex)
            {
                sError = "AcessoDAO - FindByWhere - Line:" + iLine + " - " + ex.Message;
            }
            return ListAcess;
        }
    }
}