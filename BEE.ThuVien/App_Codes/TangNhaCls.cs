using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class TangNhaCls
	{
        public BlocksCls Blocks = new BlocksCls();
		public int MaTangNha, SoLuong;
		public string TenTangNha;
		public string DienGiai;

		public TangNhaCls()
		{
		}

		public TangNhaCls(int _MaTangNha)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("TangNha_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaTangNha", _MaTangNha);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
                Blocks.BlockID = int.Parse(dread["BlockID"].ToString());
				MaTangNha = int.Parse(dread["MaTangNha"].ToString());
				TenTangNha = dread["TenTangNha"] as string;
				DienGiai = dread["DienGiai"] as string;
			}
			sqlCon.Close();
		}

		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("TangNha_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@BlockID", Blocks.BlockID);
			sqlCmd.Parameters.AddWithValue("@TenTangNha", TenTangNha);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void InsertMultiRow()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("TangNha_MultiAdd", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@BlockID", Blocks.BlockID);
            sqlCmd.Parameters.AddWithValue("@SoLuong", SoLuong);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("TangNha_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@BlockID", Blocks.BlockID);
			sqlCmd.Parameters.AddWithValue("@MaTangNha", MaTangNha);
			sqlCmd.Parameters.AddWithValue("@TenTangNha", TenTangNha);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("TangNha_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public int GetMaTang(string _TenTangNha, string _BlockName, string _TenVietTat)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("TangNha_getByTenTang", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenTangNha", _TenTangNha);
            sqlCmd.Parameters.AddWithValue("@BlockName", _BlockName);
            sqlCmd.Parameters.AddWithValue("@TenVietTat", _TenVietTat);
            sqlCmd.Parameters.Add("@MaTangNha", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaTangNha"].Value.ToString());
        }

        public DataTable Select(int _BlockID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("TangNha_getByBlockID " + _BlockID, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectAll(int _BlockID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("TangNha_getAllByBlockID " + _BlockID, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public int GetBlockID()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("TangNha_getBy", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaTangNha", MaTangNha);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                Blocks.BlockID = int.Parse(dread["BlockID"].ToString());
                Blocks.BlockName = dread["BlockName"].ToString();
                TenTangNha = dread["TenTangNha"].ToString();
            }
            sqlCon.Close();

            return Blocks.BlockID;
        }

        public int GetMaTangBy(int _TenVietTat)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("DuAn_getByTenVietTat", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenVietTat", _TenVietTat);
            sqlCmd.Parameters.Add("@MaDA", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@BlockID"].Value.ToString());
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("TangNha_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaTangNha", MaTangNha);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
