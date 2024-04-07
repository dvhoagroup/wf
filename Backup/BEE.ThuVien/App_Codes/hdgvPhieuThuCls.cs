using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class hdgvPhieuThuCls
	{
		public int MaPT;
        public string SoPhieu;
		public DateTime NgayThu;
		public string DienGiai;
        public TaiKhoanCls TKCo = new TaiKhoanCls();
        public TaiKhoanCls TKNo = new TaiKhoanCls();
		public double SoTien, GopVon, ThueVAT, LaiSuat, TienSDDat;
        public LoaiTienCls LoaiTien = new LoaiTienCls();
		public double TyGia;
		public string NguoiNop;
		public string DiaChi;
		public string ChungTuGoc;
        public KhachHangCls KhachHang = new KhachHangCls();
        public NhanVienCls NhanVien = new NhanVienCls();
        public hdgvLichThanhToanCls Lich = new hdgvLichThanhToanCls();
        public bool HinhThuc;
        public byte MaNH;

		public hdgvPhieuThuCls()
		{
		}

        public hdgvPhieuThuCls(int _MaPT)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvPhieuThu_get", sqlCon);
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
				SoTien = double.Parse(dread["SoTien"].ToString());
                GopVon = dread["GopVon"].ToString() == "" ? 0 : double.Parse(dread["GopVon"].ToString());
                ThueVAT = dread["ThueVAT"].ToString() == "" ? 0 : double.Parse(dread["ThueVAT"].ToString());
                LaiSuat = dread["LaiSuat"].ToString() == "" ? 0 : double.Parse(dread["LaiSuat"].ToString());
                TienSDDat = dread["TienSDDat"].ToString() == "" ? 0 : double.Parse(dread["TienSDDat"].ToString());
				LoaiTien.MaLoaiTien = byte.Parse(dread["MaLoaiTien"].ToString());
				TyGia = double.Parse(dread["TyGia"].ToString());
				NguoiNop = dread["NguoiNop"] as string;
				DiaChi = dread["DiaChi"] as string;
				ChungTuGoc = dread["ChungTuGoc"] as string;
				KhachHang.MaKH = int.Parse(dread["MaKH"].ToString());
				NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());
                Lich.HDGV.MaHDGV = int.Parse(dread["MaHDGV"].ToString());
                Lich.DotTT = byte.Parse(dread["DotTT"].ToString());
                HinhThuc = dread["HinhThuc"].ToString() == "" ? false : (bool)dread["HinhThuc"];
                MaNH = dread["MaNH"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaNH"].ToString());
			}
			sqlCon.Close();
		}

		public int Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvPhieuThu_add", sqlCon);
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
            sqlCmd.Parameters.AddWithValue("@MaHDGV", Lich.HDGV.MaHDGV);
            sqlCmd.Parameters.AddWithValue("@DotTT", Lich.DotTT);
            sqlCmd.Parameters.AddWithValue("@GopVon", GopVon);
            sqlCmd.Parameters.AddWithValue("@ThueVAT", ThueVAT);
            sqlCmd.Parameters.AddWithValue("@LaiSuat", LaiSuat);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
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
            SqlCommand sqlCmd = new SqlCommand("hdgvPhieuThu_add", sqlCon);
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
            sqlCmd.Parameters.AddWithValue("@MaHDGV", Lich.HDGV.MaHDGV);
            sqlCmd.Parameters.AddWithValue("@DotTT", Lich.DotTT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public int InsertMulti()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdgvPhieuThu_addSubNextPay", sqlCon);
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
            sqlCmd.Parameters.AddWithValue("@MaHDGV", Lich.HDGV.MaHDGV);
            if (Lich.DotTT != 0)
                sqlCmd.Parameters.AddWithValue("@DotTT", Lich.DotTT);
            else
                sqlCmd.Parameters.AddWithValue("@DotTT", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@GopVon", GopVon);
            sqlCmd.Parameters.AddWithValue("@ThueVAT", ThueVAT);
            sqlCmd.Parameters.AddWithValue("@LaiSuat", LaiSuat);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
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

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvPhieuThu_update", sqlCon);
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
            sqlCmd.Parameters.AddWithValue("@MaHDGV", Lich.HDGV.MaHDGV);
            sqlCmd.Parameters.AddWithValue("@DotTT", Lich.DotTT);
            sqlCmd.Parameters.AddWithValue("@GopVon", GopVon);
            sqlCmd.Parameters.AddWithValue("@ThueVAT", ThueVAT);
            sqlCmd.Parameters.AddWithValue("@LaiSuat", LaiSuat);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
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
            SqlCommand sqlCmd = new SqlCommand("hdgvPhieuThu_TaoSoPhieu", sqlCon);
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
			SqlDataAdapter sqlDA = new SqlDataAdapter("hdgvPhieuThu_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable SelectBy()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdgvPhieuThu_getByMaHDGV " + Lich.HDGV.MaHDGV, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectAll(DateTime _TuNgay, DateTime _DenNgay)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdgvPhieuThu_getAllByDate N'" + _TuNgay + "','" + _DenNgay + "'", sqlCon);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvPhieuThu_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPT", MaPT);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void DeletePaymenSurplus()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdgvPhieuThu_deletePaymentSurplus", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDGV", Lich.HDGV.MaHDGV);
            sqlCmd.Parameters.AddWithValue("@DotTT", Lich.DotTT);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
	}
}
