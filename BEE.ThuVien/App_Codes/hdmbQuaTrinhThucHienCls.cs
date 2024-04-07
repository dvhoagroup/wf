using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class hdmbQuaTrinhThucHienCls
	{
		public int MaHDMB;
		public byte Lan;
		public DateTime NgayTH;
        public hdmbTinhTrangCls TinhTrang = new hdmbTinhTrangCls();
		public string DienGiai;
        public NhanVienCls NhanVien = new NhanVienCls();

		public hdmbQuaTrinhThucHienCls()
		{
		}

		public hdmbQuaTrinhThucHienCls(int _MaHDMB, byte _Lan)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdmbQuaTrinhThucHien_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaHDMB", _MaHDMB);
			sqlCmd.Parameters.AddWithValue("@Lan", _Lan);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaHDMB = int.Parse(dread["MaHDMB"].ToString());
				Lan = byte.Parse(dread["Lan"].ToString());
				NgayTH = (DateTime)dread["NgayTH"];
				TinhTrang.MaTT = byte.Parse(dread["MaTT"].ToString());
				DienGiai = dread["DienGiai"] as string;
				NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());

			}
			sqlCon.Close();
		}
		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdmbQuaTrinhThucHien_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
			sqlCmd.Parameters.AddWithValue("@NgayTH", DateTime.Now);
			sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdmbQuaTrinhThucHien_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
			sqlCmd.Parameters.AddWithValue("@Lan", Lan);
			sqlCmd.Parameters.AddWithValue("@NgayTH", NgayTH);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("hdmbQuaTrinhThucHien_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable Select(int _MaHDMB)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdmbQuaTrinhThucHien_getAllByMaHDMB " + _MaHDMB, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdmbQuaTrinhThucHien_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
			sqlCmd.Parameters.AddWithValue("@Lan", Lan);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
