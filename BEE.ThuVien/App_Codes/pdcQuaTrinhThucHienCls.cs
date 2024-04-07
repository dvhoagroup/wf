using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class pdcQuaTrinhThucHienCls
	{
		public int MaPDC;
		public byte Lan;
		public DateTime NgayTH;
		public pdcTinhTrangCls TinhTrang = new pdcTinhTrangCls();
		public string DienGiai;
		public NhanVienCls NhanVien = new NhanVienCls();

		public pdcQuaTrinhThucHienCls()
		{
		}

		public pdcQuaTrinhThucHienCls(int _MaPDC)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pdcQuaTrinhThucHien_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPDC", _MaPDC);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaPDC = int.Parse(dread["MaPDC"].ToString());
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
			SqlCommand sqlCmd = new SqlCommand("pdcQuaTrinhThucHien_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPDC", MaPDC);
			sqlCmd.Parameters.AddWithValue("@NgayTH", DateTime.Now);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void Confirm()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcQuaTrinhThucHien_addConfirm", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPDC", MaPDC);
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
			SqlCommand sqlCmd = new SqlCommand("pdcQuaTrinhThucHien_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPDC", MaPDC);
			sqlCmd.Parameters.AddWithValue("@Lan", Lan);
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
			SqlDataAdapter sqlDA = new SqlDataAdapter("pdcQuaTrinhThucHien_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable Select(int _MaPDC)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdcQuaTrinhThucHien_getByMaPGC " + _MaPDC, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pdcQuaTrinhThucHien_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPDC", MaPDC);
			sqlCmd.Parameters.AddWithValue("@Lan", Lan);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
