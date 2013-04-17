using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SFP
{
    public abstract class ICRUD<T>
    {
        public abstract void Create(T pObj, out string sError);
        public abstract void Delete(Int32 pId, out string sError);
        public abstract void Update(T pObj, out string sError);
        public abstract T FindByPK(Int32 pId, out string sError);
        public abstract List<T> FindByWhere(String pWhere, out string sError);
        public abstract void Save(T pObj, out string sError);
        public static string GetConnection(out string sError, string str = "")
        {
            sError = string.Empty;
            short iLine = 0;
            try
            {
                string conn = string.Empty;
                if (!string.IsNullOrEmpty(str))
                {
                    iLine = 10;
                    conn = ConfigurationManager.ConnectionStrings[str].ConnectionString;
                }
                else
                {
                    iLine = 20;
                    conn = ConfigurationManager.ConnectionStrings["ConexaoPrincipal"].ConnectionString;
                }
                return conn;
            }
            catch (Exception ex)
            {
                sError = "GetConnection - Line:" + iLine + " - " + ex.Message;
                return ex.Message;
            }
        }
        public static void Command(string sCommand, out string sError)
        {
            sError = string.Empty;
            short iLine = 0;

            string sConnection = GetConnection(out sError);
            if (sError != string.Empty)
            {// 1 - Erro Connection string
                return;
            }
            iLine = 10;
            SqlConnection conn = new SqlConnection(sConnection);
            try
            {
                conn.Open();
                iLine = 20;
                SqlCommand cmd = new SqlCommand(sCommand, conn);
                iLine = 30;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                sError = "Command - Line:" + iLine + " - " + ex.Message + " Query:" + sCommand;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        public static DataTable CommandSelect(string sCommand, out string sError)
        {
            short iLine = 0;
            DataTable dt = new DataTable();
            string sConexao = GetConnection(out sError);
            if (sError != string.Empty)
            {
                //Error connection string
                return dt;
            }

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sCommand, sConexao);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                sError = "CommandSelect - Line:" + iLine + " - " + ex.Message;
            }
            return dt;
        }
    }
}
