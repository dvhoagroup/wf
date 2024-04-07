using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class hdcnNhacNoCls
	{
		public int MaNN;
		public string SoNN;
		public DateTime NgayNN;
		public int MaHDCN;
		public byte DotTT;
		public byte LanNN;
		public int GiaHan;
		public string TieuDe;
		public string NoiDung;
		public byte LanGui;
		public int MaNV;

		public hdcnNhacNoCls()
		{
		}

		public hdcnNhacNoCls(int _MaNN)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdcnNhacNo_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaNN", _MaNN);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaNN = int.Parse(dread["MaNN"].ToString());
				SoNN = dread["SoNN"] as string;
				NgayNN = (DateTime)dread["NgayNN"];
				MaHDCN = int.Parse(dread["MaHDCN"].ToString());
				DotTT = byte.Parse(dread["DotTT"].ToString());
				LanNN = byte.Parse(dread["LanNN"].ToString());
				GiaHan = int.Parse(dread["GiaHan"].ToString());
				TieuDe = dread["TieuDe"] as string;
				NoiDung = dread["NoiDung"] as string;
				LanGui = byte.Parse(dread["LanGui"].ToString());
				MaNV = int.Parse(dread["MaNV"].ToString());
			}
			sqlCon.Close();
		}

		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdcnNhacNo_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@SoNN", SoNN);
			sqlCmd.Parameters.AddWithValue("@NgayNN", NgayNN);
			sqlCmd.Parameters.AddWithValue("@MaHDCN", MaHDCN);
			sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
			sqlCmd.Parameters.AddWithValue("@LanNN", LanNN);
			sqlCmd.Parameters.AddWithValue("@GiaHan", GiaHan);
			sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
			sqlCmd.Parameters.AddWithValue("@NoiDung", NoiDung);
			sqlCmd.Parameters.AddWithValue("@LanGui", LanGui);
			sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdcnNhacNo_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaNN", MaNN);
			sqlCmd.Parameters.AddWithValue("@SoNN", SoNN);
			sqlCmd.Parameters.AddWithValue("@NgayNN", NgayNN);
			sqlCmd.Parameters.AddWithValue("@MaHDCN", MaHDCN);
			sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
			sqlCmd.Parameters.AddWithValue("@LanNN", LanNN);
			sqlCmd.Parameters.AddWithValue("@GiaHan", GiaHan);
			sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
			sqlCmd.Parameters.AddWithValue("@NoiDung", NoiDung);
			sqlCmd.Parameters.AddWithValue("@LanGui", LanGui);
			sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("hdcnNhacNo_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable SelectAllBy(int _MaHDCN)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdcnNhacNo_getAllByMaHDCN " + _MaHDCN, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdcnNhacNo_TaoSoNN", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoNN", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoNN"].Value.ToString();
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdcnNhacNo_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaNN", MaNN);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
