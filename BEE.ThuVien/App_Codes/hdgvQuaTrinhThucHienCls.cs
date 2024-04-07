using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class hdgvQuaTrinhThucHienCls
	{
        public NhanVienCls NhanVien = new NhanVienCls();
		public int MaHDGV;
		public byte Lan;
		public DateTime NgayTH;
        public hdgvTinhTrangCls TinhTrang = new hdgvTinhTrangCls();
		public string DienGiai;

		public hdgvQuaTrinhThucHienCls()
		{
		}

        public hdgvQuaTrinhThucHienCls(int _MaHDGV, byte _Lan)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvQuaTrinhThucHien_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaHDGV", _MaHDGV);
			sqlCmd.Parameters.AddWithValue("@Lan", _Lan);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
                NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());
				MaHDGV = int.Parse(dread["MaHDGV"].ToString());
				Lan = byte.Parse(dread["Lan"].ToString());
				NgayTH = (DateTime)dread["NgayTH"];
                TinhTrang.MaTT = byte.Parse(dread["MaTT"].ToString());
				DienGiai = dread["DienGiai"] as string;
			}
			sqlCon.Close();
		}

		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvQuaTrinhThucHien_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDGV", MaHDGV);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvQuaTrinhThucHien_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
			sqlCmd.Parameters.AddWithValue("@MaHDGV", MaHDGV);
			sqlCmd.Parameters.AddWithValue("@Lan", Lan);
			sqlCmd.Parameters.AddWithValue("@NgayTH", NgayTH);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("hdgvQuaTrinhThucHien_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable Select(int _MaHDGV)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdgvQuaTrinhThucHien_getByMaHDGV " + _MaHDGV, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }        

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvQuaTrinhThucHien_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaHDGV", MaHDGV);
			sqlCmd.Parameters.AddWithValue("@Lan", Lan);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
