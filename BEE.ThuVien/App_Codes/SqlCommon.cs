using System;
using System.Data;
using System.Data.SqlClient;

namespace it
{
    public class SqlCommon
    {
        //Chuoi ket noi csdl
        public static string Conn = CommonCls.Conn;
        //Test connect sql
        public static bool sqlTestConnect(string Conn)
        {
            SqlConnection sqlConn = new SqlConnection(Conn);
            try
            {
                sqlConn.Open();
                sqlConn.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                sqlConn.Dispose();
            }
        }
        //Truy van SQL
        public static DataTable getData(string sqlSring)
        {
            SqlConnection sqlConn = new SqlConnection(Conn);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlSring, sqlConn);
            try
            {
                using (DataTable tbl = new DataTable())
                {
                    sqlConn.Open();
                    sqlAdapter.Fill(tbl);
                    sqlConn.Close();
                    return tbl;
                }
            }
            catch
            {
                sqlConn.Close();
                return null;
            }
            finally
            {
                sqlConn.Dispose();
                sqlAdapter.Dispose();
            }
        }
        //Xu ly sql cmd ExecuteNonQuery
        public static void exeCuteNonQuery(SqlCommand sqlCmd)
        {
            sqlCmd.Connection = new SqlConnection(Conn);
            try
            {
                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Connection.Close();
            }
            catch
            {
                sqlCmd.Connection.Close();
            }
            finally
            {
                sqlCmd.Connection.Dispose();
            }
        }
        //Xu ly sql cmd string
        public static void exeCuteNonQueryPro(SqlCommand sqlCmd)
        {
            sqlCmd.CommandType = CommandType.StoredProcedure;
            exeCuteNonQuery(sqlCmd);
        }
        //Xu ly sql cmd string
        public static void ExecuteNonQueryText(string sqlString)
        {
            using (SqlCommand sqlCmd = new SqlCommand(sqlString))
            {
                sqlCmd.CommandType = CommandType.Text;
                exeCuteNonQuery(sqlCmd);
            }
        }
    }
}