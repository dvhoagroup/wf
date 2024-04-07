using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class hdgvPhieuChiCls
	{
		public int MaPC;
		public string SoPhieu;
		public DateTime NgayChi;
		public string DienGiai;
        public TaiKhoanCls TKCo = new TaiKhoanCls();
        public TaiKhoanCls TKNo = new TaiKhoanCls();
		public double SoTien;
		public LoaiTienCls LoaiTien = new LoaiTienCls();
		public double TyGia;
		public string NguoiNhan;
		public string DiaChi;
		public string ChungTuGoc;
        public KhachHangCls KhachHang = new KhachHangCls();
        public NhanVienCls NhanVien = new NhanVienCls();
        public hdGopVonCls HDGV = new hdGopVonCls();

		public hdgvPhieuChiCls()
		{
		}

        public hdgvPhieuChiCls(int _MaPC)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvPhieuChi_get", sqlCon);
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
				LoaiTien.MaLoaiTien = byte.Parse(dread["MaTyGia"].ToString());
				TyGia = double.Parse(dread["TyGia"].ToString());
				NguoiNhan = dread["NguoiNhan"] as string;
				DiaChi = dread["DiaChi"] as string;
				ChungTuGoc = dread["ChungTuGoc"] as string;
				KhachHang.MaKH = int.Parse(dread["MaKH"].ToString());
				NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());
                HDGV.MaHDGV = int.Parse(dread["MaHDGV"].ToString());
			}
			sqlCon.Close();
		}

		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvPhieuChi_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
			sqlCmd.Parameters.AddWithValue("@NgayChi", NgayChi);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@TKCo", TKCo.MaTK);
			sqlCmd.Parameters.AddWithValue("@TKNo", TKNo.MaTK);
			sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
			sqlCmd.Parameters.AddWithValue("@MaTyGia", LoaiTien.MaLoaiTien);
			sqlCmd.Parameters.AddWithValue("@TyGia", TyGia);
			sqlCmd.Parameters.AddWithValue("@NguoiNhan", NguoiNhan);
			sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
			sqlCmd.Parameters.AddWithValue("@ChungTuGoc", ChungTuGoc);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
			sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaHDGV", HDGV.MaHDGV);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvPhieuChi_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPC", MaPC);
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayChi", NgayChi);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@TKCo", TKCo.MaTK);
            sqlCmd.Parameters.AddWithValue("@TKNo", TKNo.MaTK);
            sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCmd.Parameters.AddWithValue("@MaTyGia", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@TyGia", TyGia);
            sqlCmd.Parameters.AddWithValue("@NguoiNhan", NguoiNhan);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@ChungTuGoc", ChungTuGoc);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaHDGV", HDGV.MaHDGV);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("hdgvPhieuChi_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable SelectAll(DateTime _TuNgay, DateTime _DenNgay)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdgvPhieuChi_getAllByDate N'" + _TuNgay + "','" + _DenNgay + "'", sqlCon);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdgvPhieuChi_TaoSoPhieu", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoPhieu", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoPhieu"].Value.ToString();
        }

        public DataTable Select(int _HDGV)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdgvPhieuChi_getByMaHDGV " + _HDGV, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvPhieuChi_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPC", MaPC);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public bool CheckPaymentSurplus(byte DotTT)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdgvPhieuGiuCho_checkPaymentSurplus", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDGV", HDGV.MaHDGV);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }
	}
}
