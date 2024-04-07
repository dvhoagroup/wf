using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class khbh_BDSCls
	{
		public int MaKHBH;
		public BatDongSanCls BDS = new BatDongSanCls();

		public khbh_BDSCls()
		{
		}

		public khbh_BDSCls(int _MaKHBH, string _MaBDS)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("khbh_BDS_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaKHBH", _MaKHBH);
			sqlCmd.Parameters.AddWithValue("@MaBDS", _MaBDS);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaKHBH = int.Parse(dread["MaKHBH"].ToString());
                BDS.MaBDS = dread["MaBDS"] as string;
			}
			sqlCon.Close();
		}
		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("khbh_BDS_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKHBH", MaKHBH);
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaBDS);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("khbh_BDS_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaKHBH", MaKHBH);
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaBDS);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("khbh_BDS_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable khbh_BDS_Select(string _MaDA, string _BlockID, byte _MaLoaiDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("khbh_BDS_Select '" + _MaDA + "','" + _BlockID + "'," + _MaLoaiDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }
        
        public DataTable Select(int _MaKHBH, int _MaDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("khbh_BDS_getByMaKHBH " + _MaKHBH + "," + _MaDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectBy(int _MaKHBH)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("khbh_BDS_getByMaKHBHShow " + _MaKHBH, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public bool Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("khbh_BDS_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaKHBH", MaKHBH);
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaBDS);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
		}
	}
}
