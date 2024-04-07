using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class KeHoachBanHangCls
	{
		public int MaKHBH;
		public string MaSo;
		public string DienGiai;
		public DateTime TuNgay;
		public DateTime DenNgay;
		public string KhuyenMai;
		public NhanVienCls NhanVien = new NhanVienCls();

		public KeHoachBanHangCls()
		{
		}

		public KeHoachBanHangCls(int _MaKHBH)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("KeHoachBanHang_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaKHBH", _MaKHBH);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaKHBH = int.Parse(dread["MaKHBH"].ToString());
				MaSo = dread["MaSo"] as string;
				DienGiai = dread["DienGiai"] as string;
				TuNgay = (DateTime)dread["TuNgay"];
				DenNgay = (DateTime)dread["DenNgay"];
				KhuyenMai = dread["KhuyenMai"] as string;
                NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());

			}
			sqlCon.Close();
		}
		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("KeHoachBanHang_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@TuNgay", TuNgay);
			sqlCmd.Parameters.AddWithValue("@DenNgay", DenNgay);
			sqlCmd.Parameters.AddWithValue("@KhuyenMai", KhuyenMai);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("KeHoachBanHang_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaKHBH", MaKHBH);
			sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@TuNgay", TuNgay);
			sqlCmd.Parameters.AddWithValue("@DenNgay", DenNgay);
			sqlCmd.Parameters.AddWithValue("@KhuyenMai", KhuyenMai);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("KeHoachBanHang_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable Expires()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KeHoachBanHang_expires", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("KeHoachBanHang_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaKHBH", MaKHBH);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
