using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class hdcnPhieuThuCls
	{
		public int MaPT;
		public byte DotTT;
        public hdChuyenNhuongCls HDCN = new hdChuyenNhuongCls();
		public string SoPhieu;
		public DateTime NgayThu;
		public string DienGiai;
		public string TKCo;
		public string TKNo;
		public double SoTien;
        public LoaiTienCls LoaiTien = new LoaiTienCls();
		public double TyGia;
		public string NguoiNop;
		public string DiaChi;
		public string ChungTuGoc;
        public KhachHangCls KhachHang = new KhachHangCls();
		public NhanVienCls NhanVien=new NhanVienCls();
        public DaiLyCls DaiLy = new DaiLyCls();
        public NhanVienDaiLyCls NVDL = new NhanVienDaiLyCls();

		public hdcnPhieuThuCls()
		{
		}

		public hdcnPhieuThuCls(int _MaPT, byte _DotTT, int _MaHDCN)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdcnPhieuThu_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPT", _MaPT);
			sqlCmd.Parameters.AddWithValue("@DotTT", _DotTT);
			sqlCmd.Parameters.AddWithValue("@MaHDCN", _MaHDCN);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaPT = int.Parse(dread["MaPT"].ToString());
				DotTT = byte.Parse(dread["DotTT"].ToString());
				HDCN.MaHDCN = int.Parse(dread["MaHDCN"].ToString());
				SoPhieu = dread["SoPhieu"] as string;
				NgayThu = (DateTime)dread["NgayThu"];
				DienGiai = dread["DienGiai"] as string;
				TKCo = dread["TKCo"] as string;
				TKNo = dread["TKNo"] as string;
				SoTien = double.Parse(dread["SoTien"].ToString());
				LoaiTien.MaLoaiTien = byte.Parse(dread["MaLoaiTien"].ToString());
				TyGia = double.Parse(dread["TyGia"].ToString());
				NguoiNop = dread["NguoiNop"] as string;
				DiaChi = dread["DiaChi"] as string;
				ChungTuGoc = dread["ChungTuGoc"] as string;
				KhachHang.MaKH = int.Parse(dread["MaKH"].ToString());
				NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());
                DaiLy.MaDL = dread["MaDL"].ToString() == "" ? 0 : int.Parse(dread["MaDL"].ToString());
                NVDL.MaNV = dread["MaNVDL"].ToString() == "" ? 0 : int.Parse(dread["MaNVDL"].ToString());
			}
			sqlCon.Close();
		}

		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdcnPhieuThu_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@MaHDCN", HDCN.MaHDCN);
			sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
			sqlCmd.Parameters.AddWithValue("@NgayThu", NgayThu);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@TKCo", TKCo);
			sqlCmd.Parameters.AddWithValue("@TKNo", TKNo);
			sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
			sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
			sqlCmd.Parameters.AddWithValue("@TyGia", TyGia);
			sqlCmd.Parameters.AddWithValue("@NguoiNop", NguoiNop);
			sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
			sqlCmd.Parameters.AddWithValue("@ChungTuGoc", ChungTuGoc);
			sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
			sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
			//sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
			//sqlCmd.Parameters.AddWithValue("@MaNVDL", NVDL.MaNV);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdcnPhieuThu_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPT", MaPT);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@MaHDCN", HDCN.MaHDCN);
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayThu", NgayThu);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@TKCo", TKCo);
            sqlCmd.Parameters.AddWithValue("@TKNo", TKNo);
            sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@TyGia", TyGia);
            sqlCmd.Parameters.AddWithValue("@NguoiNop", NguoiNop);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@ChungTuGoc", ChungTuGoc);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            //sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            //sqlCmd.Parameters.AddWithValue("@MaNVDL", NVDL.MaNV);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("hdcnPhieuThu_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable Select(int _MaHDCN)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdcnPhieuThu_getAllByMaHDCN " + _MaHDCN, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdcnPhieuThu_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPT", MaPT);
			sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
			sqlCmd.Parameters.AddWithValue("@MaHDCN", HDCN.MaHDCN);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
