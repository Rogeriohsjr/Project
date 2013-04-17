using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace SFP
{
    public class CategoryDAO : ICRUD<Category>
    {
        StringBuilder sCommand = new StringBuilder();
        private Int32 IdUserControl;

        public CategoryDAO(Int32 IdUserControl)
        {
            this.IdUserControl = IdUserControl;
        }

        public override void Create(Category pCategory, out string sError)
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
                sCommand.AppendFormat("'{0}', ", pCategory.Description);
                sCommand.AppendFormat("'{0}', ", pCategory.IsBlock);
                sCommand.AppendFormat("'{0}'  ", pCategory.User.Id);
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

        public override void Update(Category pCategory, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            try
            {
                sCommand.AppendFormat("UPDATE TBCATEGORIA SET ");
                sCommand.AppendFormat("DESCRICAO = '{0}', ", pCategory.Description);
                sCommand.AppendFormat("BLOQUEADO = '{0}', ", pCategory.IsBlock);
                sCommand.AppendFormat("IDUSUARIO = '{0}'  ", pCategory.User.Id);
                sCommand.AppendFormat("WHERE ID = {0}", pCategory.Id);
                iLine = 10;
                Command(sCommand.ToString(), out sError);
            }
            catch (Exception ex)
            {
                sError = "CategoriaDAO - Update - Line:" + iLine + " - " + ex.Message;
            }
        }

        public override Category FindByPK(Int32 pId, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            Category objCategory = new Category();
            
            try
            {
                sCommand.AppendFormat("SELECT ");
                sCommand.AppendFormat("ID, ");
                sCommand.AppendFormat("DESCRICAO,   ");
                sCommand.AppendFormat("BLOQUEADO,   ");
                sCommand.AppendFormat("IDUSUARIO    ");
                sCommand.AppendFormat("FROM TBCATEGORIA WHERE IDUSUARIO = {0} AND ID = {1}",IdUserControl, pId);
                iLine = 10;
                DataTable dtData = CommandSelect(sCommand.ToString(), out sError);
                iLine = 20;
                if (dtData.Rows.Count > 0)
                {
                    objCategory.Id = Int32.Parse(dtData.Rows[0][0].ToString());
                    iLine = 21;
                    objCategory.Description = dtData.Rows[0][1].ToString();
                    iLine = 22;
                    objCategory.IsBlock = Boolean.Parse(dtData.Rows[0][2].ToString());
                    iLine = 23;
                    objCategory.User = new UserDAO().FindByPK(Int32.Parse(dtData.Rows[0][3].ToString()), out sError);
                }

            }
            catch (Exception ex)
            {
                sError = "CategoriaDAO - FindByPK - Line:" + iLine + " - " + ex.Message;
            }
            return objCategory;
        }

        public override List<Category> FindByWhere(String pWhere, out string sError)
        {
            short iLine = 0;
            sCommand.Clear();
            sError = string.Empty;
            List<Category> ListCategory = new List<Category>();
            if (String.IsNullOrWhiteSpace(pWhere))
                pWhere = " 0=0 ";
            try
            {
                sCommand.AppendFormat("SELECT ");
                sCommand.AppendFormat("ID, ");
                sCommand.AppendFormat("DESCRICAO,   ");
                sCommand.AppendFormat("BLOQUEADO,   ");
                sCommand.AppendFormat("IDUSUARIO    ");
                sCommand.AppendFormat("FROM TBCATEGORIA WHERE IDUSUARIO = {0} AND {1}", IdUserControl, pWhere);
                iLine = 10;
                DataTable dtData = CommandSelect(sCommand.ToString(), out sError);
                iLine = 20;
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    Category objCategory = new Category();
                    objCategory.Id = Int32.Parse(dtData.Rows[i][0].ToString());
                    iLine = 21;
                    objCategory.Description = dtData.Rows[i][1].ToString();
                    iLine = 22;
                    objCategory.IsBlock = Boolean.Parse(dtData.Rows[i][2].ToString());
                    iLine = 23;
                    objCategory.User = new UserDAO().FindByPK(Int32.Parse(dtData.Rows[0][3].ToString()), out sError);
                    
                    ListCategory.Add(objCategory);
                }
            }
            catch (Exception ex)
            {
                sError = "CategoriaDAO - FindByWhere - Line:" + iLine + " - " + ex.Message;
            }
            return ListCategory;
        }

        public override void Save(Category pObj, out string sError)
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