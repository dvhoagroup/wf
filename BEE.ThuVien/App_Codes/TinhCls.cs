using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class TinhCls
	{
		public byte MaTinh;
		public string TenTinh;

        public TinhCls()
        {
        }

		public TinhCls(byte _MaTinh)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("Tinh_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaTinh", _MaTinh);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaTinh = byte.Parse(dread["MaTinh"].ToString());
				TenTinh = dread["TenTinh"] as string;

			}
			sqlCon.Close();
		}
		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("Tinh_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@TenTinh", TenTinh);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("Tinh_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaTinh", MaTinh);
			sqlCmd.Parameters.AddWithValue("@TenTinh", TenTinh);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("Tinh_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable SelectAll()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("Tinh_getAll2", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("Tinh_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaTinh", MaTinh);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
