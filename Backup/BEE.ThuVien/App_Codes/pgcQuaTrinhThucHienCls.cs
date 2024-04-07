using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class pgcQuaTrinhThucHienCls
	{
        public NhanVienCls NhanVien = new NhanVienCls();
		public int MaPGC;
		public byte Lan;
		public DateTime NgayTH;
        public pgcTinhTrangCls TinhTrang = new pgcTinhTrangCls();
		public string DienGiai;

		public pgcQuaTrinhThucHienCls()
		{
		}

		public pgcQuaTrinhThucHienCls(int _MaPGC, byte _Lan)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcQuaTrinhThucHien_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPGC", _MaPGC);
			sqlCmd.Parameters.AddWithValue("@Lan", _Lan);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
                NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());
				MaPGC = int.Parse(dread["MaPGC"].ToString());
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
			SqlCommand sqlCmd = new SqlCommand("pgcQuaTrinhThucHien_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void Confirm()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcQuaTrinhThucHien_addConfirm", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
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
			SqlCommand sqlCmd = new SqlCommand("pgcQuaTrinhThucHien_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
			sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
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
			SqlDataAdapter sqlDA = new SqlDataAdapter("pgcQuaTrinhThucHien_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable Select(int _MaPGC)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcQuaTrinhThucHien_getByMaPGC " + _MaPGC, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }        

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcQuaTrinhThucHien_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
			sqlCmd.Parameters.AddWithValue("@Lan", Lan);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
