using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class hdcnPhieuChiCls
	{
		public int MaPC;
		public string SoPhieu;
		public DateTime NgayChi;
		public string DienGiai;
		public string TKCo;
		public string TKNo;
		public double SoTien;
        public LoaiTienCls LoaiTien = new LoaiTienCls();
		public double TyGia;
		public string NguoiNhan;
		public string DiaChi;
		public string ChungTuGoc;
        public KhachHangCls KhachHang = new KhachHangCls();
        public NhanVienCls NhanVien = new NhanVienCls();
        public hdChuyenNhuongCls HDCN = new hdChuyenNhuongCls();

		public hdcnPhieuChiCls()
		{
		}

		public hdcnPhieuChiCls(int _MaPC)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdcnPhieuChi_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPC", _MaPC);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaPC = int.Parse(dread["MaPC"].ToString());
				SoPhieu = dread["SoPhieu"] as string;
				NgayChi = (DateTime)dread["NgayChi"];
				DienGiai = dread["DienGiai"] as string;
				TKCo = dread["TKCo"] as string;
				TKNo = dread["TKNo"] as string;
				SoTien = double.Parse(dread["SoTien"].ToString());
                LoaiTien.MaLoaiTien = byte.Parse(dread["MaTyGia"].ToString());
				TyGia = double.Parse(dread["TyGia"].ToString());
				NguoiNhan = dread["NguoiNhan"] as string;
				DiaChi = dread["DiaChi"] as string;
				ChungTuGoc = dread["ChungTuGoc"] as string;
                KhachHang.MaKH = int.Parse(dread["MaKH"].ToString());
				NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());
				HDCN.MaHDCN = int.Parse(dread["MaHDCN"].ToString());
			}
			sqlCon.Close();
		}

		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdcnPhieuChi_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
			sqlCmd.Parameters.AddWithValue("@NgayChi", NgayChi);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@TKCo", TKCo);
			sqlCmd.Parameters.AddWithValue("@TKNo", TKNo);
			sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCmd.Parameters.AddWithValue("@MaTyGia", LoaiTien.MaLoaiTien);
			sqlCmd.Parameters.AddWithValue("@TyGia", TyGia);
			sqlCmd.Parameters.AddWithValue("@NguoiNhan", NguoiNhan);
			sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
			sqlCmd.Parameters.AddWithValue("@ChungTuGoc", ChungTuGoc);
			sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
			sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
			sqlCmd.Parameters.AddWithValue("@MaHDCN", HDCN.MaHDCN);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdcnPhieuChi_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPC", MaPC);
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayChi", NgayChi);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@TKCo", TKCo);
            sqlCmd.Parameters.AddWithValue("@TKNo", TKNo);
            sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCmd.Parameters.AddWithValue("@MaTyGia", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@TyGia", TyGia);
            sqlCmd.Parameters.AddWithValue("@NguoiNhan", NguoiNhan);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@ChungTuGoc", ChungTuGoc);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaHDCN", HDCN.MaHDCN);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("hdcnPhieuChi_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable Select(int _MaHDCN)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdcnPhieuChi_getByMaHDCN " + _MaHDCN, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdcnPhieuChi_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPC", MaPC);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
