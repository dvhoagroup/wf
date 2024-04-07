using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class pgcNhacNoCls
	{
		public int MaNN;
		public string SoNN;
		public DateTime NgayNN;
		public int MaPGC;
		public byte DotTT;
		public byte LanNN;
		public int GiaHan;
		public string TieuDe;
		public string NoiDung;
		public byte LanGui, LoaiNN;
		public int MaNV;
        public int MaHDMB;

		public pgcNhacNoCls()
		{
		}

		public pgcNhacNoCls(int _MaNN)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcNhacNo_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaNN", _MaNN);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaNN = int.Parse(dread["MaNN"].ToString());
				SoNN = dread["SoNN"] as string;
				NgayNN = (DateTime)dread["NgayNN"];
                MaPGC = dread["MaPGC"].ToString() == "" ? 0 : int.Parse(dread["MaPGC"].ToString());
                MaHDMB = dread["MaHDMB"].ToString() == "" ? 0 : int.Parse(dread["MaHDMB"].ToString());
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

		public int Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcNhacNo_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaNN", SqlDbType.Int).Direction = ParameterDirection.Output;
			sqlCmd.Parameters.AddWithValue("@SoNN", SoNN);
			sqlCmd.Parameters.AddWithValue("@NgayNN", NgayNN);
            if (MaPGC != 0)
                sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            else
                sqlCmd.Parameters.AddWithValue("@MaPGC", DBNull.Value);
            if (MaHDMB != 0)
                sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            else
                sqlCmd.Parameters.AddWithValue("@MaHDMB", DBNull.Value);
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

            return int.Parse(sqlCmd.Parameters["@MaNN"].Value.ToString());
		}

        public void Insert2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcNhacNo_add2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoNN", SoNN);
            sqlCmd.Parameters.AddWithValue("@NgayNN", NgayNN);
            if (MaPGC != 0)
                sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            else
                sqlCmd.Parameters.AddWithValue("@MaPGC", DBNull.Value);
            if (MaHDMB != 0)
                sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            else
                sqlCmd.Parameters.AddWithValue("@MaHDMB", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
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

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcNhacNo_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaNN", MaNN);
			sqlCmd.Parameters.AddWithValue("@SoNN", SoNN);
			sqlCmd.Parameters.AddWithValue("@NgayNN", NgayNN);
            if (MaPGC != 0)
                sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            else
                sqlCmd.Parameters.AddWithValue("@MaPGC", DBNull.Value);
            if (MaHDMB != 0)
                sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            else
                sqlCmd.Parameters.AddWithValue("@MaHDMB", DBNull.Value);
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

        public void UnDeposit()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcNhacNo_MatCoc", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("pgcNhacNo_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable SelectAllBy(int _MaPDC)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcNhacNo_getAllByMaPGC " + _MaPDC, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectAllBy(int _MaHDMB, string val)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcNhacNo_getAllByMaHDMB " + _MaHDMB, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcNhacNo_TaoSoNN", sqlCon);
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
            SqlCommand sqlCmd = new SqlCommand("pgcNhacNo_getLaNN", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.Add("@LanNN", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return byte.Parse(sqlCmd.Parameters["@LanNN"].Value.ToString());
        }

        public byte GetLanNNHDMB()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcNhacNo_getLaNNHDMB", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
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
			SqlCommand sqlCmd = new SqlCommand("pgcNhacNo_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaNN", MaNN);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void Extension()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcNhacNo_extension", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNN", MaNN);
            sqlCmd.Parameters.AddWithValue("@GiaHan", GiaHan);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateAmountSend()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcNhacNo_ppdateAmountSend", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNN", MaNN);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
	}
}
