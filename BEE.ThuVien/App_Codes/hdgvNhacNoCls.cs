using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class hdgvNhacNoCls
	{
		public int MaNN;
		public string SoNN;
		public DateTime NgayNN;
		public int MaHDGV;
		public byte DotTT;
		public byte LanNN;
		public int GiaHan;
		public string TieuDe;
		public string NoiDung;
		public byte LanGui, LoaiNN;
		public int MaNV;

		public hdgvNhacNoCls()
		{
		}

        public hdgvNhacNoCls(int _MaNN)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvNhacNo_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaNN", _MaNN);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaNN = int.Parse(dread["MaNN"].ToString());
				SoNN = dread["SoNN"] as string;
				NgayNN = (DateTime)dread["NgayNN"];
                MaHDGV = int.Parse(dread["MaHDGV"].ToString());
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
			SqlCommand sqlCmd = new SqlCommand("hdgvNhacNo_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@SoNN", SoNN);
			sqlCmd.Parameters.AddWithValue("@NgayNN", NgayNN);
			sqlCmd.Parameters.AddWithValue("@MaHDGV", MaHDGV);
			sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
			sqlCmd.Parameters.AddWithValue("@LanNN", LanNN);
			sqlCmd.Parameters.AddWithValue("@GiaHan", GiaHan);
			sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
			sqlCmd.Parameters.AddWithValue("@NoiDung", NoiDung);
			sqlCmd.Parameters.AddWithValue("@LanGui", LanGui);
			sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@LoaiNN", LoaiNN);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void Insert2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdgvNhacNo_add2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoNN", SoNN);
            sqlCmd.Parameters.AddWithValue("@NgayNN", NgayNN);
            sqlCmd.Parameters.AddWithValue("@MaHDGV", MaHDGV);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
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
			SqlCommand sqlCmd = new SqlCommand("hdgvNhacNo_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaNN", MaNN);
			sqlCmd.Parameters.AddWithValue("@SoNN", SoNN);
			sqlCmd.Parameters.AddWithValue("@NgayNN", NgayNN);
			sqlCmd.Parameters.AddWithValue("@MaHDGV", MaHDGV);
			sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
			sqlCmd.Parameters.AddWithValue("@LanNN", LanNN);
			sqlCmd.Parameters.AddWithValue("@GiaHan", GiaHan);
			sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
			sqlCmd.Parameters.AddWithValue("@NoiDung", NoiDung);
			sqlCmd.Parameters.AddWithValue("@LanGui", LanGui);
			sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@LoaiNN", LoaiNN);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void UnDeposit()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdgvNhacNo_MatCoc", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDGV", MaHDGV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("hdgvNhacNo_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable SelectAllBy(int _MaHDGV)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdgvNhacNo_getAllByMaHDGV " + _MaHDGV, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdgvNhacNo_TaoSoNN", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoNN", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoNN"].Value.ToString();
        }

        public byte GetLanNN()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdgvNhacNo_getLaNN", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDGV", MaHDGV);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.Add("@LanNN", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return byte.Parse(sqlCmd.Parameters["@LanNN"].Value.ToString());
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvNhacNo_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaNN", MaNN);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
