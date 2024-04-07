using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class pgcPhieuChiCls
	{
		public int MaPC;
		public string SoPhieu;
		public DateTime NgayChi;
		public string DienGiai;
        public TaiKhoanCls TKCo = new TaiKhoanCls();
        public TaiKhoanCls TKNo = new TaiKhoanCls();
        public double SoTien, GopVon, ThueVAT, LaiThanhLy, PhiMoiGioi, TraTienNopThua, KhoanKhac;
		public double TienPhat;
		public string NguoiNhan;
		public string DiaChi;
		public string ChungTuGoc;
        public KhachHangCls KhachHang = new KhachHangCls();
        public NhanVienCls NhanVien = new NhanVienCls();
		public pgcPhieuGiuChoCls PGC = new pgcPhieuGiuChoCls();
        public HopDongMuaBanCls HDMB = new HopDongMuaBanCls();

		public pgcPhieuChiCls()
		{
		}

		public pgcPhieuChiCls(int _MaPC)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcPhieuChi_get", sqlCon);
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
				TKCo.MaTK = dread["TKCo"] as string;
				TKNo.MaTK = dread["TKNo"] as string;
				SoTien = double.Parse(dread["SoTien"].ToString());
                TienPhat = dread["TienPhat"].ToString() == "" ? 0 : double.Parse(dread["TienPhat"].ToString());
                KhoanKhac = dread["KhoanKhac"].ToString() == "" ? 0 : double.Parse(dread["KhoanKhac"].ToString());
                GopVon = dread["GopVon"].ToString() == "" ? 0 : double.Parse(dread["GopVon"].ToString());
                ThueVAT = dread["ThueVAT"].ToString() == "" ? 0 : double.Parse(dread["ThueVAT"].ToString());
                LaiThanhLy = dread["LaiThanhLy"].ToString() == "" ? 0 : double.Parse(dread["LaiThanhLy"].ToString());
                TraTienNopThua = dread["TraTienNopThua"].ToString() == "" ? 0 : double.Parse(dread["TraTienNopThua"].ToString());
                PhiMoiGioi = dread["PhiMoiGioi"].ToString() == "" ? 0 : double.Parse(dread["PhiMoiGioi"].ToString());
				NguoiNhan = dread["NguoiNhan"] as string;
				DiaChi = dread["DiaChi"] as string;
				ChungTuGoc = dread["ChungTuGoc"] as string;
                KhachHang.MaKH = dread["MaKH"].ToString() == "" ? 0 : int.Parse(dread["MaKH"].ToString());
				NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());
                PGC.MaPGC = dread["MaPGC"].ToString() == "" ? 0 : int.Parse(dread["MaPGC"].ToString());
                HDMB.MaHDMB = dread["MaHDMB"].ToString() == "" ? 0 : int.Parse(dread["MaHDMB"].ToString());
			}
			sqlCon.Close();
		}

		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcPhieuChi_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
			sqlCmd.Parameters.AddWithValue("@NgayChi", NgayChi);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@TKCo", TKCo.MaTK);
			sqlCmd.Parameters.AddWithValue("@TKNo", TKNo.MaTK);
			sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
			sqlCmd.Parameters.AddWithValue("@NguoiNhan", NguoiNhan);
			sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
			sqlCmd.Parameters.AddWithValue("@ChungTuGoc", ChungTuGoc);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
			sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            if (PGC.MaPGC != 0)
                sqlCmd.Parameters.AddWithValue("@MaPGC", PGC.MaPGC);
            else
                sqlCmd.Parameters.AddWithValue("@MaPGC", DBNull.Value);
            if (HDMB.MaHDMB == 0)
                sqlCmd.Parameters.AddWithValue("@MaHDMB", DBNull.Value);
            else
                sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            sqlCmd.Parameters.AddWithValue("@TienPhat", TienPhat);
            sqlCmd.Parameters.AddWithValue("@GopVon", GopVon);
            sqlCmd.Parameters.AddWithValue("@ThueVAT", ThueVAT);
            sqlCmd.Parameters.AddWithValue("@PhiMoiGioi", PhiMoiGioi);
            sqlCmd.Parameters.AddWithValue("@LaiThanhLy", LaiThanhLy);
            sqlCmd.Parameters.AddWithValue("@TraTienNopThua", TraTienNopThua);
            sqlCmd.Parameters.AddWithValue("@KhoanKhac", KhoanKhac);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcPhieuChi_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPC", MaPC);
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayChi", NgayChi);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@TKCo", TKCo.MaTK);
            sqlCmd.Parameters.AddWithValue("@TKNo", TKNo.MaTK);
            sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCmd.Parameters.AddWithValue("@NguoiNhan", NguoiNhan);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@ChungTuGoc", ChungTuGoc);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            if (PGC.MaPGC != 0)
                sqlCmd.Parameters.AddWithValue("@MaPGC", PGC.MaPGC);
            else
                sqlCmd.Parameters.AddWithValue("@MaPGC", DBNull.Value);
            if (HDMB.MaHDMB == 0)
                sqlCmd.Parameters.AddWithValue("@MaHDMB", DBNull.Value);
            else
                sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            sqlCmd.Parameters.AddWithValue("@TienPhat", TienPhat);
            sqlCmd.Parameters.AddWithValue("@GopVon", GopVon);
            sqlCmd.Parameters.AddWithValue("@ThueVAT", ThueVAT);
            sqlCmd.Parameters.AddWithValue("@PhiMoiGioi", PhiMoiGioi);
            sqlCmd.Parameters.AddWithValue("@LaiThanhLy", LaiThanhLy);
            sqlCmd.Parameters.AddWithValue("@TraTienNopThua", TraTienNopThua);
            sqlCmd.Parameters.AddWithValue("@KhoanKhac", KhoanKhac);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("pgcPhieuChi_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable SelectAll(DateTime _TuNgay, DateTime _DenNgay)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuChi_getAllByDate", sqlCon);
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

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuChi_TaoSoPhieu", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoPhieu", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoPhieu"].Value.ToString();
        }

        public DataTable Select(int _PGC)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcPhieuChi_getByMaPGC " + _PGC, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(int _MaHDMB, string val)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcPhieuChi_getByMaHDMB " + _MaHDMB, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcPhieuChi_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPC", MaPC);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public bool CheckPaymentSurplus(byte DotTT)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_checkPaymentSurplus", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", PGC.MaPGC);
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }
	}
}
