using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace SFP
{
    public class CategoriaDAO : ICRUD<Categoria>
    {
        StringBuilder sCommand = new StringBuilder();
        private Int32 IdUsuarioResponse;

        public CategoriaDAO(Int32 idUsuario)
        {
            this.IdUsuarioResponse = idUsuario;
        }

        public override void Create(Categoria pCategoria, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            try
            {
                sCommand.AppendFormat("INSERT INTO TBCATEGORIA (");
                sCommand.AppendFormat(" DESCRICAO, ");
                sCommand.AppendFormat(" BLOQUEADO, ");
                sCommand.AppendFormat(" IDUSUARIO  ");
                sCommand.AppendFormat(" ) VALUES  (");
                sCommand.AppendFormat("'{0}', ", pCategoria.Descricao);
                sCommand.AppendFormat("'{0}', ", pCategoria.Bloqueado);
                sCommand.AppendFormat("'{0}'  ", pCategoria.Usuario.IdUsuario);
                sCommand.AppendFormat(" ) ");
                iLine = 10;
                Command(sCommand.ToString(), out sError);
            }
            catch (Exception ex)
            {
                sError = "CategoriaDAO - Create - Line:" + iLine + " - " + ex.Message;
            }
        }

        public override void Delete(int pId, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            try
            {
                sCommand.AppendFormat("DELETE FROM TBCATEGORIA WHERE ID = {0}", pId);
                iLine = 10;
                Command(sCommand.ToString(), out sError);
            }
            catch (Exception ex)
            {
                sError = "CategoriaDAO - Delete - Line:" + iLine + " - " + ex.Message;
            }
        }

        public override void Update(Categoria pCategoria, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            try
            {
                sCommand.AppendFormat("UPDATE TBCATEGORIA SET ");
                sCommand.AppendFormat("DESCRICAO = '{0}', ", pCategoria.Descricao);
                sCommand.AppendFormat("BLOQUEADO = '{0}', ", pCategoria.Bloqueado);
                sCommand.AppendFormat("IDUSUARIO = '{0}'  ", pCategoria.Usuario.IdUsuario);
                sCommand.AppendFormat("WHERE ID = {0}", pCategoria.IdCategoria);
                iLine = 10;
                Command(sCommand.ToString(), out sError);
            }
            catch (Exception ex)
            {
                sError = "CategoriaDAO - Update - Line:" + iLine + " - " + ex.Message;
            }
        }

        public override Categoria FindByPK(Int32 pId, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            Categoria objCategoria = new Categoria();
            
            try
            {
                sCommand.AppendFormat("SELECT ");
                sCommand.AppendFormat("ID, ");
                sCommand.AppendFormat("DESCRICAO,   ");
                sCommand.AppendFormat("BLOQUEADO,   ");
                sCommand.AppendFormat("IDUSUARIO    ");
                sCommand.AppendFormat("FROM TBCATEGORIA WHERE IDUSUARIO = {0} AND ID = {1}",IdUsuarioResponse, pId);
                iLine = 10;
                DataTable dtDados = CommandSelect(sCommand.ToString(), out sError);
                iLine = 20;
                if (dtDados.Rows.Count > 0)
                {
                    objCategoria.IdCategoria = Int32.Parse(dtDados.Rows[0][0].ToString());
                    iLine = 21;
                    objCategoria.Descricao = dtDados.Rows[0][1].ToString();
                    iLine = 22;
                    objCategoria.Bloqueado = Boolean.Parse(dtDados.Rows[0][2].ToString());
                    iLine = 23;
                    objCategoria.Usuario = new UsuarioDAO().FindByPK(Int32.Parse(dtDados.Rows[0][3].ToString()), out sError);
                }

            }
            catch (Exception ex)
            {
                sError = "CategoriaDAO - FindByPK - Line:" + iLine + " - " + ex.Message;
            }
            return objCategoria;
        }

        public override List<Categoria> FindByWhere(String pWhere, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            List<Categoria> listCategoria = new List<Categoria>();
            if (String.IsNullOrWhiteSpace(pWhere))
                pWhere = " 0=0 ";
            try
            {
                sCommand.AppendFormat("SELECT ");
                sCommand.AppendFormat("ID, ");
                sCommand.AppendFormat("DESCRICAO,   ");
                sCommand.AppendFormat("BLOQUEADO,   ");
                sCommand.AppendFormat("IDUSUARIO    ");
                sCommand.AppendFormat("FROM TBCATEGORIA WHERE IDUSUARIO = {0} AND {1}", IdUsuarioResponse, pWhere);
                iLine = 10;
                DataTable dtDados = CommandSelect(sCommand.ToString(), out sError);
                iLine = 20;
                for (int i = 0; i < dtDados.Rows.Count; i++)
                {
                    Categoria objCategoria = new Categoria();
                    objCategoria.IdCategoria = Int32.Parse(dtDados.Rows[i][0].ToString());
                    iLine = 21;
                    objCategoria.Descricao = dtDados.Rows[i][1].ToString();
                    iLine = 22;
                    objCategoria.Bloqueado = Boolean.Parse(dtDados.Rows[i][2].ToString());
                    iLine = 23;
                    objCategoria.Usuario = new UsuarioDAO().FindByPK(Int32.Parse(dtDados.Rows[0][3].ToString()), out sError);
                    
                    listCategoria.Add(objCategoria);
                }
            }
            catch (Exception ex)
            {
                sError = "CategoriaDAO - FindByWhere - Line:" + iLine + " - " + ex.Message;
            }
            return listCategoria;
        }

        public override void Save(Categoria pObj, out string sError)
        {
            if (pObj.IdCategoria == 0)
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