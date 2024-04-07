using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class pgcPhieuThuCls
	{
		public int MaPT;
		public string SoPhieu;
		public DateTime NgayThu;
		public string DienGiai;
        public TaiKhoanCls TKCo = new TaiKhoanCls();
        public TaiKhoanCls TKNo = new TaiKhoanCls();
		public double SoTien, GopVon, ThueVAT, LaiSuat, TienSDDat, PhiChuyenNhuong, PhiMoiGioi, KhoanKhac;
        public LoaiTienCls LoaiTien = new LoaiTienCls();
		public double TyGia;
		public string NguoiNop;
		public string DiaChi;
		public string ChungTuGoc;
        public KhachHangCls KhachHang = new KhachHangCls();
        public NhanVienCls NhanVien = new NhanVienCls();
        public DaiLyCls DaiLy = new DaiLyCls();
        public NhanVienDaiLyCls NVDL = new NhanVienDaiLyCls();
        public pgcLichThanhToanCls Lich = new pgcLichThanhToanCls();
        public HopDongMuaBanCls HDMB = new HopDongMuaBanCls();
        public bool HinhThuc;
        public byte MaNH;
        public string Error;
        public decimal TienCK;

		public pgcPhieuThuCls()
		{
		}

		public pgcPhieuThuCls(int _MaPT)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcPhieuThu_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPT", _MaPT);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaPT = int.Parse(dread["MaPT"].ToString());
				SoPhieu = dread["SoPhieu"] as string;
				NgayThu = (DateTime)dread["NgayThu"];
				DienGiai = dread["DienGiai"] as string;
				TKCo.MaTK = dread["TKCo"] as string;
				TKNo.MaTK = dread["TKNo"] as string;
                SoTien = dread["SoTien"].ToString() == "" ? 0 : double.Parse(dread["SoTien"].ToString());
                GopVon = dread["GopVon"].ToString() == "" ? 0 : double.Parse(dread["GopVon"].ToString());
                ThueVAT = dread["ThueVAT"].ToString() == "" ? 0 : double.Parse(dread["ThueVAT"].ToString());
                LaiSuat = dread["LaiSuat"].ToString() == "" ? 0 : double.Parse(dread["LaiSuat"].ToString());
                TienSDDat = dread["TienSDDat"].ToString() == "" ? 0 : double.Parse(dread["TienSDDat"].ToString());
                TienCK = (decimal)dread["TienCK"];
                PhiChuyenNhuong = dread["PhiChuyenNhuong"].ToString() == "" ? 0 : double.Parse(dread["PhiChuyenNhuong"].ToString());
                PhiMoiGioi = dread["PhiMoiGioi"].ToString() == "" ? 0 : double.Parse(dread["PhiMoiGioi"].ToString());
                KhoanKhac = dread["KhoanKhac"].ToString() == "" ? 0 : double.Parse(dread["KhoanKhac"].ToString());
				LoaiTien.MaLoaiTien = byte.Parse(dread["MaLoaiTien"].ToString());
				TyGia = double.Parse(dread["TyGia"].ToString());
				NguoiNop = dread["NguoiNop"] as string;
				DiaChi = dread["DiaChi"] as string;
				ChungTuGoc = dread["ChungTuGoc"] as string;
				KhachHang.MaKH = int.Parse(dread["MaKH"].ToString());
				NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());
                DaiLy.MaDL = dread["MaDL"].ToString() == "" ? 0 : int.Parse(dread["MaDL"].ToString());
                NVDL.MaNV = dread["MaNVDL"].ToString() == "" ? 0 : int.Parse(dread["MaNVDL"].ToString());
                Lich.PGC.MaPGC = dread["MaPGC"].ToString() == "" ? 0 : int.Parse(dread["MaPGC"].ToString());
                Lich.DotTT = dread["DotTT"].ToString() == "" ? (byte)0 : byte.Parse(dread["DotTT"].ToString());
                HDMB.MaHDMB = dread["MaHDMB"].ToString() == "" ? 0 : int.Parse(dread["MaHDMB"].ToString());
                HinhThuc = dread["HinhThuc"].ToString() == "" ? false : (bool)dread["HinhThuc"];
                MaNH = dread["MaNH"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaNH"].ToString());
			}
			sqlCon.Close();
		}

		public int Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcPhieuThu_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaPT", SqlDbType.Int).Direction = ParameterDirection.Output;
			sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
			sqlCmd.Parameters.AddWithValue("@NgayThu", NgayThu);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@TKCo", TKCo.MaTK);
			sqlCmd.Parameters.AddWithValue("@TKNo", TKNo.MaTK);
			sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
			sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
			sqlCmd.Parameters.AddWithValue("@TyGia", TyGia);
			sqlCmd.Parameters.AddWithValue("@NguoiNop", NguoiNop);
			sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
			sqlCmd.Parameters.AddWithValue("@ChungTuGoc", ChungTuGoc);
			sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
			sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            if (Lich.PGC.MaPGC != 0)
            {
                sqlCmd.Parameters.AddWithValue("@MaPGC", Lich.PGC.MaPGC);
                sqlCmd.Parameters.AddWithValue("@MaHDMB", DBNull.Value);
            }
            else
            {
                sqlCmd.Parameters.AddWithValue("@MaPGC", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            }
            if (Lich.DotTT != 0)
                sqlCmd.Parameters.AddWithValue("@DotTT", Lich.DotTT);
            else
                sqlCmd.Parameters.AddWithValue("@DotTT", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@GopVon", GopVon);
            sqlCmd.Parameters.AddWithValue("@ThueVAT", ThueVAT);
            sqlCmd.Parameters.AddWithValue("@LaiSuat", LaiSuat);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
            sqlCmd.Parameters.AddWithValue("@TienCK", TienCK);
            sqlCmd.Parameters.AddWithValue("@PhiChuyenNhuong", PhiChuyenNhuong);
            sqlCmd.Parameters.AddWithValue("@PhiMoiGioi", PhiMoiGioi);
            sqlCmd.Parameters.AddWithValue("@KhoanKhac", KhoanKhac);
            sqlCmd.Parameters.AddWithValue("@HinhThuc", HinhThuc);
            if (MaNH != 0)
                sqlCmd.Parameters.AddWithValue("@MaNH", MaNH);
            else
                sqlCmd.Parameters.AddWithValue("@MaNH", DBNull.Value);
            sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
            
            return int.Parse(sqlCmd.Parameters["@MaPT"].Value.ToString());
		}

        public int InsertMulti()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuThu_addSubNextPay", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaPT", SqlDbType.Int).Direction = ParameterDirection.Output;
			sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
			sqlCmd.Parameters.AddWithValue("@NgayThu", NgayThu);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@TKCo", TKCo.MaTK);
			sqlCmd.Parameters.AddWithValue("@TKNo", TKNo.MaTK);
			sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
			sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
			sqlCmd.Parameters.AddWithValue("@TyGia", TyGia);
			sqlCmd.Parameters.AddWithValue("@NguoiNop", NguoiNop);
			sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
			sqlCmd.Parameters.AddWithValue("@ChungTuGoc", ChungTuGoc);
			sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
			sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            if (Lich.PGC.MaPGC != 0)
            {
                sqlCmd.Parameters.AddWithValue("@MaPGC", Lich.PGC.MaPGC);
                sqlCmd.Parameters.AddWithValue("@MaHDMB", DBNull.Value);
            }
            else
            {
                sqlCmd.Parameters.AddWithValue("@MaPGC", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            }
            if (Lich.DotTT != 0)
                sqlCmd.Parameters.AddWithValue("@DotTT", Lich.DotTT);
            else
                sqlCmd.Parameters.AddWithValue("@DotTT", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@GopVon", GopVon);
            sqlCmd.Parameters.AddWithValue("@ThueVAT", ThueVAT);
            sqlCmd.Parameters.AddWithValue("@LaiSuat", LaiSuat);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
            sqlCmd.Parameters.AddWithValue("@PhiChuyenNhuong", PhiChuyenNhuong);
            sqlCmd.Parameters.AddWithValue("@PhiMoiGioi", PhiMoiGioi);
            sqlCmd.Parameters.AddWithValue("@KhoanKhac", KhoanKhac);
            sqlCmd.Parameters.AddWithValue("@HinhThuc", HinhThuc);
            if (MaNH != 0)
                sqlCmd.Parameters.AddWithValue("@MaNH", MaNH);
            else
                sqlCmd.Parameters.AddWithValue("@MaNH", DBNull.Value);

            sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaPT"].Value.ToString());
		}

        public void InsertByDL()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuThu_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayThu", NgayThu);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@TKCo", TKCo.MaTK);
            sqlCmd.Parameters.AddWithValue("@TKNo", TKNo.MaTK);
            sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@TyGia", TyGia);
            sqlCmd.Parameters.AddWithValue("@NguoiNop", NguoiNop);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@ChungTuGoc", ChungTuGoc);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            sqlCmd.Parameters.AddWithValue("@MaNVDL", NVDL.MaNV);
            if (Lich.PGC.MaPGC != 0)
            {
                sqlCmd.Parameters.AddWithValue("@MaPGC", Lich.PGC.MaPGC);
                sqlCmd.Parameters.AddWithValue("@MaHDMB", DBNull.Value);
            }
            else
            {
                sqlCmd.Parameters.AddWithValue("@MaPGC", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            }
            if (Lich.DotTT != 0)
                sqlCmd.Parameters.AddWithValue("@DotTT", Lich.DotTT);
            else
                sqlCmd.Parameters.AddWithValue("@DotTT", DBNull.Value);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcPhieuThu_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPT", MaPT);
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayThu", NgayThu);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@TKCo", TKCo.MaTK);
            sqlCmd.Parameters.AddWithValue("@TKNo", TKNo.MaTK);
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
            if (Lich.PGC.MaPGC != 0)
                sqlCmd.Parameters.AddWithValue("@MaPGC", Lich.PGC.MaPGC);
            else
                sqlCmd.Parameters.AddWithValue("@MaPGC", DBNull.Value);
            if (Lich.DotTT != 0)
                sqlCmd.Parameters.AddWithValue("@DotTT", Lich.DotTT);
            else
                sqlCmd.Parameters.AddWithValue("@DotTT", DBNull.Value);
            if (HDMB.MaHDMB != 0)
                sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            else
                sqlCmd.Parameters.AddWithValue("@MaHDMB", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@GopVon", GopVon);
            sqlCmd.Parameters.AddWithValue("@ThueVAT", ThueVAT);
            sqlCmd.Parameters.AddWithValue("@LaiSuat", LaiSuat);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
            sqlCmd.Parameters.AddWithValue("@TienCK", TienCK);
            sqlCmd.Parameters.AddWithValue("@PhiChuyenNhuong", PhiChuyenNhuong);
            sqlCmd.Parameters.AddWithValue("@PhiMoiGioi", PhiMoiGioi);
            sqlCmd.Parameters.AddWithValue("@KhoanKhac", KhoanKhac);
            sqlCmd.Parameters.AddWithValue("@HinhThuc", HinhThuc);
            if (MaNH != 0)
                sqlCmd.Parameters.AddWithValue("@MaNH", MaNH);
            else
                sqlCmd.Parameters.AddWithValue("@MaNH", DBNull.Value);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuThu_TaoSoPhieu", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoPhieu", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoPhieu"].Value.ToString();
        }

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("pgcPhieuThu_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable Select(int _PGC)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcPhieuThu_getByMaPGC " + _PGC, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(int _HDMB, string val)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcPhieuThu_getByMaHDMB " + _HDMB, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectAll(DateTime _TuNgay, DateTime _DenNgay)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuThu_getAllByDate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcPhieuThu_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPT", MaPT);

			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void DeletePaymenSurplus()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuThu_deletePaymentSurplus", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", Lich.PGC.MaPGC);
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            sqlCmd.Parameters.AddWithValue("@DotTT", Lich.DotTT);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public static DataTable getByDotTTandPGC(byte dotTT, int maPGC)
        {
            return SqlCommon.getData(string.Format("pgcPhieuThu_getByDotTTandPGC {0}, {1}", dotTT, maPGC));
        }

        public static DataTable getByDotTTandHDMB(byte dotTT, int maHDMB)
        {
            return SqlCommon.getData(string.Format("pgcPhieuThu_getByDotTTandHDMB {0}, {1}", dotTT, maHDMB));
        }
	}
}
