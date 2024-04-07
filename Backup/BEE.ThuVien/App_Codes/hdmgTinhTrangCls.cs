using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class hdmgTinhTrangCls
    {
        public byte MaTT;
        public string TenTT;

        public hdmgTinhTrangCls()
        {
        }

        public hdmgTinhTrangCls(byte _MaTT)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmgTinhTrang_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaTT", _MaTT);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaTT = byte.Parse(dread["MaTT"].ToString());
                TenTT = dread["TenTT"] as string;
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmgTinhTrang_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenTT", TenTT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmgTinhTrang_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaTT", MaTT);
            sqlCmd.Parameters.AddWithValue("@TenTT", TenTT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdmgTinhTrang_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmgTinhTrang_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaTT", MaTT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}