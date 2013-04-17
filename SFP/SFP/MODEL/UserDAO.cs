using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace SFP
{
    public class UserDAO : ICRUD<User>
    {
        StringBuilder sCommand = new StringBuilder();

        public override void Create(User pUsuario, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            try
            {
                sCommand.AppendFormat("INSERT INTO TBUSUARIO (");
                sCommand.AppendFormat(" LOGIN, ");
                sCommand.AppendFormat(" SENHA, ");
                sCommand.AppendFormat(" NOME, ");
                sCommand.AppendFormat(" EMAIL, ");
                sCommand.AppendFormat(" BLOQUEADO, ");
                sCommand.AppendFormat(" ULTIMOACESSO ");
                sCommand.AppendFormat(" ) VALUES  (");
                sCommand.AppendFormat("'{0}', ", pUsuario.Login);
                sCommand.AppendFormat("'{0}', ", pUsuario.Password);
                sCommand.AppendFormat("'{0}', ", pUsuario.Name);
                sCommand.AppendFormat("'{0}', ", pUsuario.Email);
                sCommand.AppendFormat("'{0}', ", pUsuario.IsBlock);
                sCommand.AppendFormat("'{0}'  ", DateTime.Now.ToString("yyyyMMdd"));
                sCommand.AppendFormat(" ) ");
                iLine = 10;
                Command(sCommand.ToString(), out sError);
            }
            catch (Exception ex)
            {
                sError = "UsuarioDAO - Create - Line:" + iLine + " - " + ex.Message;
            }
        }

        public override void Delete(int pId, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            try
            {
                sCommand.AppendFormat("DELETE FROM TBUSUARIO WHERE ID = {0}", pId);
                iLine = 10;
                Command(sCommand.ToString(), out sError);
            }
            catch (Exception ex)
            {
                sError = "UsuarioDAO - Delete - Line:" + iLine + " - " + ex.Message;
            }
        }

        public override void Update(User pUsuario, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            try
            {
                sCommand.AppendFormat("UPDATE TBUSUARIO SET ");
                sCommand.AppendFormat("LOGIN = '{0}', ", pUsuario.Login);
                sCommand.AppendFormat("SENHA = '{0}', ", pUsuario.Password);
                sCommand.AppendFormat("NOME = '{0}', ", pUsuario.Name);
                sCommand.AppendFormat("EMAIL = '{0}', ", pUsuario.Email);
                sCommand.AppendFormat("BLOQUEADO = '{0}', ", pUsuario.IsBlock);
                sCommand.AppendFormat("ULTIMOACESSO = '{0}' ", DateTime.Now.ToString("yyyyMMdd"));
                sCommand.AppendFormat("WHERE ID = {0}", pUsuario.Id);
                iLine = 10;
                Command(sCommand.ToString(), out sError);
            }
            catch (Exception ex)
            {
                sError = "UsuarioDAO - Update - Line:" + iLine + " - " + ex.Message;
            }
        }

        public override User FindByPK(Int32 pId, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            User objUsuario = new User();

            try
            {
                sCommand.AppendFormat("SELECT ");
                sCommand.AppendFormat(" ID, ");
                sCommand.AppendFormat(" LOGIN, ");
                sCommand.AppendFormat(" SENHA, ");
                sCommand.AppendFormat(" NOME, ");
                sCommand.AppendFormat(" EMAIL, ");
                sCommand.AppendFormat(" BLOQUEADO, ");
                sCommand.AppendFormat(" ULTIMOACESSO ");
                sCommand.AppendFormat("FROM TBUSUARIO WHERE ID = {0}", pId);
                iLine = 10;
                DataTable dtDados = CommandSelect(sCommand.ToString(), out sError);
                iLine = 20;
                if (dtDados.Rows.Count > 0)
                {
                    objUsuario.Id = Int32.Parse(dtDados.Rows[0][0].ToString());
                    objUsuario.Login = dtDados.Rows[0][1].ToString();
                    objUsuario.Password = dtDados.Rows[0][2].ToString();
                    objUsuario.Name = dtDados.Rows[0][3].ToString();
                    objUsuario.Email = dtDados.Rows[0][4].ToString();
                    objUsuario.IsBlock = Boolean.Parse(dtDados.Rows[0][5].ToString());
                    objUsuario.DateLastAcess = dtDados.Rows[0][6].ToString();
                }

            }
            catch (Exception ex)
            {
                sError = "UsuarioDAO - FindByPK - Line:" + iLine + " - " + ex.Message;
            }
            return objUsuario;
        }

        public override List<User> FindByWhere(string pWhere, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            List<User> listUsuario = new List<User>();
            if (String.IsNullOrWhiteSpace(pWhere))
                pWhere = " 0=0 ";
            try
            {
                sCommand.AppendFormat("SELECT ");
                sCommand.AppendFormat(" ID, ");
                sCommand.AppendFormat(" LOGIN, ");
                sCommand.AppendFormat(" SENHA, ");
                sCommand.AppendFormat(" NOME, ");
                sCommand.AppendFormat(" EMAIL, ");
                sCommand.AppendFormat(" BLOQUEADO, ");
                sCommand.AppendFormat(" ULTIMOACESSO ");
                sCommand.AppendFormat("FROM TBUSUARIO WHERE {0} ", pWhere);
                iLine = 10;
                DataTable dtDados = CommandSelect(sCommand.ToString(), out sError);
                iLine = 20;
                for (int i = 0; i < dtDados.Rows.Count; i++)
                {
                    User objUsuario = new User();
                    objUsuario.Id = Int32.Parse(dtDados.Rows[i][0].ToString());
                    objUsuario.Login = dtDados.Rows[i][1].ToString();
                    objUsuario.Password = dtDados.Rows[i][2].ToString();
                    objUsuario.Name = dtDados.Rows[i][3].ToString();
                    objUsuario.Email = dtDados.Rows[i][4].ToString();
                    objUsuario.IsBlock = Boolean.Parse(dtDados.Rows[i][5].ToString());
                    objUsuario.DateLastAcess = dtDados.Rows[i][6].ToString();

                    listUsuario.Add(objUsuario);
                }
            }
            catch (Exception ex)
            {
                sError = "UsuarioDAO - FindByWhere - Line:" + iLine + " - " + ex.Message;
            }
            return listUsuario;
        }

        public override void Save(User pObj, out string sError)
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