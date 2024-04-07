using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class LoaiDACls
	{
		public byte MaLoaiDA;
        public string TenLoaiDA;
        public string TenLoaiDAEN;

		public LoaiDACls()
		{
		}

		public LoaiDACls(byte _MaLoaiDA)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("LoaiDA_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaLoaiDA", _MaLoaiDA);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaLoaiDA = byte.Parse(dread["MaLoaiDA"].ToString());
				TenLoaiDA = dread["TenLoaiDA"] as string;
                TenLoaiDAEN = dread["TenLoaiDAEN"] as string;
			}
			sqlCon.Close();
		}

		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("LoaiDA_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenLoaiDA", TenLoaiDA);
            sqlCmd.Parameters.AddWithValue("@TenLoaiDAEN", TenLoaiDAEN);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("LoaiDA_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaLoaiDA", MaLoaiDA);
            sqlCmd.Parameters.AddWithValue("@TenLoaiDA", TenLoaiDA);
            sqlCmd.Parameters.AddWithValue("@TenLoaiDAEN", TenLoaiDAEN);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("LoaiDA_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("LoaiDA_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaLoaiDA", MaLoaiDA);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
