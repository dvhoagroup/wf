using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class KhachHang_GhiChuCls
	{
		public int MaKH;
		public byte STT;
		public DateTime NgayGhiChu;
		public string TieuDe;
		public string NoiDung;
		public int MaNV;

		public KhachHang_GhiChuCls()
		{
		}

		public KhachHang_GhiChuCls(int _MaKH, byte _STT)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("KhachHang_GhiChu_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaKH", _MaKH);
			sqlCmd.Parameters.AddWithValue("@STT", _STT);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaKH = int.Parse(dread["MaKH"].ToString());
				STT = byte.Parse(dread["STT"].ToString());
				NgayGhiChu = (DateTime)dread["NgayGhiChu"];
				TieuDe = dread["TieuDe"] as string;
				NoiDung = dread["NoiDung"] as string;
				MaNV = short.Parse(dread["MaNV"].ToString());

			}
			sqlCon.Close();
		}
		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("KhachHang_GhiChu_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
			sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
			sqlCmd.Parameters.AddWithValue("@NoiDung", NoiDung);
			sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("KhachHang_GhiChu_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
			sqlCmd.Parameters.AddWithValue("@STT", STT);
			sqlCmd.Parameters.AddWithValue("@NgayGhiChu", NgayGhiChu);
			sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
			sqlCmd.Parameters.AddWithValue("@NoiDung", NoiDung);
			sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("KhachHang_GhiChu_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable Select(int _MaKH)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KhachHang_GhiChu_getByMaKH " + _MaKH, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("KhachHang_GhiChu_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
			sqlCmd.Parameters.AddWithValue("@STT", STT);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
