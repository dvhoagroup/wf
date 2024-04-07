using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class ViTriCls
    {
        public byte MaViTri;
        public string TenViTri;

        public ViTriCls()
        {
        }

        public ViTriCls(byte _MaViTri)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("ViTri_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaViTri", _MaViTri);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaViTri = byte.Parse(dread["MaViTri"].ToString());
                TenViTri = dread["TenViTri"] as string;
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("ViTri_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenViTri", TenViTri);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("ViTri_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaViTri", MaViTri);
            sqlCmd.Parameters.AddWithValue("@TenViTri", TenViTri);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("ViTri_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("ViTri_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaViTri", MaViTri);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public byte GetMaVT()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("ViTri_getMaVT", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenViTri", TenViTri);
            sqlCmd.Parameters.Add("@MaViTri", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return byte.Parse(sqlCmd.Parameters["@MaViTri"].Value.ToString());
        }
    }
}