using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class BlocksCls
	{
		public int BlockID;
		public string BlockName;
        public DuAnCls DuAn = new DuAnCls();
		public string DienGiai;
        public double TienSDD;
        public byte Thue;
        public PhanKhuCls PhanKhu = new PhanKhuCls();

		public BlocksCls()
		{
		}

		public BlocksCls(int _BlockID)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("Blocks_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@BlockID", _BlockID);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				BlockID = int.Parse(dread["BlockID"].ToString());
				BlockName = dread["BlockName"] as string;
                DuAn.MaDA = int.Parse(dread["MaDA"].ToString());
				DienGiai = dread["DienGiai"] as string;
                Thue = byte.Parse(dread["Thue"].ToString());
                TienSDD = double.Parse(dread["TienSDD"].ToString());
                PhanKhu.MaPK = dread["MaPK"].ToString() == "" ? 0 : int.Parse(dread["MaPK"].ToString());
			}
			sqlCon.Close();
		}

		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("Blocks_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@BlockName", BlockName);
			sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@TienSDD", TienSDD);
            sqlCmd.Parameters.AddWithValue("@Thue", Thue);
            if (PhanKhu.MaPK != 0)
                sqlCmd.Parameters.AddWithValue("@MaPK", PhanKhu.MaPK);
            else
                sqlCmd.Parameters.AddWithValue("@MaPK", DBNull.Value);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("Blocks_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@BlockID", BlockID);
			sqlCmd.Parameters.AddWithValue("@BlockName", BlockName);
			sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@TienSDD", TienSDD);
            sqlCmd.Parameters.AddWithValue("@Thue", Thue);
            if (PhanKhu.MaPK != 0)
                sqlCmd.Parameters.AddWithValue("@MaPK", PhanKhu.MaPK);
            else
                sqlCmd.Parameters.AddWithValue("@MaPK", DBNull.Value);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("Blocks_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable Select(int _MaDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("Blocks_getByBlockID " + _MaDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByPK()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("Blocks_getByMaPK " + PhanKhu.MaPK, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByPK2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("Blocks_getByMaPK2 " + PhanKhu.MaPK, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectAll(int _MaDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("Blocks_getByMaDA " + _MaDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectAll2(int _MaDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("Blocks_getAllByMaDA " + _MaDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("Blocks_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@BlockID", BlockID);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public int GetIDByBlockName(string _BlockName, string _TenVietTat)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("Blocks_getByBlockName", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@BlockName", _BlockName);
            sqlCmd.Parameters.AddWithValue("@TenVietTat", _TenVietTat);
            sqlCmd.Parameters.Add("@BlockID", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@BlockID"].Value.ToString());
        }

        public int GetBlockID()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("Blocks_getBlockID", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@BlockName", BlockName);
            sqlCmd.Parameters.AddWithValue("@MaPK", PhanKhu.MaPK);
            sqlCmd.Parameters.Add("@BlockID", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@BlockID"].Value.ToString());
        }
	}
}
