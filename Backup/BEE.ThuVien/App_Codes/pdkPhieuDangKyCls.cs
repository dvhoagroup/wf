using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class pdkPhieuDangKyCls
	{
		public int MaPDK;
		public string SoPhieu;
		public DateTime NgayKy;
		public int ThoiHan;
		public string FileAttach;
		public string MaBDS;
		public KhachHangCls KhachHang = new KhachHangCls();
        public byte MaNDD;
		public double DTSD;
		public double DonGia;
		public double ThanhTien;
        public LoaiTienCls LoaiTien = new LoaiTienCls();
        public NhanVienCls NhanVienKD = new NhanVienCls();
        public NhanVienCls NhanVienKT = new NhanVienCls();
		public string Template, YeuCau;
        public DaiLyCls DaiLy = new DaiLyCls();
        public NhanVienDaiLyCls NhanVienDL = new NhanVienDaiLyCls();
        public pgcTinhTrangCls TinhTrang = new pgcTinhTrangCls();
        public HinhThucNhacNoCls HTNN = new HinhThucNhacNoCls();
        public HinhThucThongBaoCls HTTB = new HinhThucThongBaoCls();
        public double TienGiuCho;

		public pdkPhieuDangKyCls()
		{
		}

        public pdkPhieuDangKyCls(int _MaPDK)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pdkPhieuDangKy_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPDK", _MaPDK);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaPDK = int.Parse(dread["MaPDK"].ToString());
				SoPhieu = dread["SoPhieu"] as string;
				NgayKy = (DateTime)dread["NgayKy"];
				ThoiHan = int.Parse(dread["ThoiHan"].ToString());
				FileAttach = dread["FileAttach"] as string;
				MaBDS = dread["MaBDS"] as string;
                YeuCau = dread["YeuCau"] as string;
				KhachHang.MaKH = int.Parse(dread["MaKH"].ToString());
                MaNDD = dread["MaNDD"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaNDD"].ToString());
				DTSD = double.Parse(dread["DTSD"].ToString());
				DonGia = double.Parse(dread["DonGia"].ToString());
				ThanhTien = double.Parse(dread["ThanhTien"].ToString());
                LoaiTien.MaLoaiTien = byte.Parse(dread["MaLoaiTien"].ToString());
                NhanVienKD.MaNV = dread["MaNVKD"].ToString() == "" ? 0 : int.Parse(dread["MaNVKD"].ToString());
                NhanVienKT.MaNV = dread["MaNVKT"].ToString() == "" ? 0 : int.Parse(dread["MaNVKT"].ToString());
				Template = dread["Template"] as string;
                DaiLy.MaDL = dread["MaDL"].ToString() == "" ? 0 : int.Parse(dread["MaDL"].ToString());
                NhanVienDL.MaNV = dread["MaNVDL"].ToString() == "" ? 0 : int.Parse(dread["MaNVDL"].ToString());
				TinhTrang.MaTT = byte.Parse(dread["MaTT"].ToString());
                TienGiuCho = double.Parse(dread["TienGiuCho"].ToString());
                HTNN.MaHT = dread["MaHTNN"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaHTNN"].ToString());
                HTTB.MaHT = dread["MaHTTB"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaHTTB"].ToString());
			}
			sqlCon.Close();
		}

		public int Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pdkPhieuDangKy_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaPDK", SqlDbType.Int).Direction = ParameterDirection.Output;
			sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
			sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
			sqlCmd.Parameters.AddWithValue("@ThoiHan", ThoiHan);
			sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
			sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
			sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            if (MaNDD != 0)
                sqlCmd.Parameters.AddWithValue("@MaNDD", MaNDD);
            else
                sqlCmd.Parameters.AddWithValue("@MaNDD", DBNull.Value);
			sqlCmd.Parameters.AddWithValue("@DTSD", DTSD);
			sqlCmd.Parameters.AddWithValue("@DonGia", DonGia);
			sqlCmd.Parameters.AddWithValue("@ThanhTien", ThanhTien);
			sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
			sqlCmd.Parameters.AddWithValue("@MaNVKD", NhanVienKD.MaNV);
			sqlCmd.Parameters.AddWithValue("@MaNVKT", NhanVienKT.MaNV);
			sqlCmd.Parameters.AddWithValue("@Template", Template);
			sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@TienGiuCho", TienGiuCho);
            sqlCmd.Parameters.AddWithValue("@MaHTTB", HTTB.MaHT);
            sqlCmd.Parameters.AddWithValue("@MaHTNN", HTNN.MaHT);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaDL", DBNull.Value);
            if (NhanVienDL.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVDL", NhanVienDL.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDL", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@YeuCau", YeuCau);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaPDK"].Value.ToString());
		}

        public int InsertByDL()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdkPhieuDangKy_addByDL", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaPDK", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@ThoiHan", ThoiHan);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            if (MaNDD != 0)
                sqlCmd.Parameters.AddWithValue("@MaNDD", MaNDD);
            else
                sqlCmd.Parameters.AddWithValue("@MaNDD", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DTSD", DTSD);
            sqlCmd.Parameters.AddWithValue("@DonGia", DonGia);
            sqlCmd.Parameters.AddWithValue("@ThanhTien", ThanhTien);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@MaNVKD", NhanVienKD.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaNVKT", NhanVienKT.MaNV);
            sqlCmd.Parameters.AddWithValue("@Template", Template);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaDL", DBNull.Value);
            if (NhanVienDL.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVDL", NhanVienDL.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDL", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@TienGiuCho", TienGiuCho);
            sqlCmd.Parameters.AddWithValue("@MaHTTB", HTTB.MaHT);
            sqlCmd.Parameters.AddWithValue("@MaHTNN", HTNN.MaHT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaPDK"].Value.ToString());
        }

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pdkPhieuDangKy_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPDK", MaPDK);
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@ThoiHan", ThoiHan);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            if (MaNDD != 0)
                sqlCmd.Parameters.AddWithValue("@MaNDD", MaNDD);
            else
                sqlCmd.Parameters.AddWithValue("@MaNDD", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DTSD", DTSD);
            sqlCmd.Parameters.AddWithValue("@DonGia", DonGia);
            sqlCmd.Parameters.AddWithValue("@ThanhTien", ThanhTien);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@MaNVKD", NhanVienKD.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaNVKT", NhanVienKT.MaNV);
            //sqlCmd.Parameters.AddWithValue("@Template", Template);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaDL", DBNull.Value);
            if (NhanVienDL.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVDL", NhanVienDL.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDL", DBNull.Value);
            //sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@TienGiuCho", TienGiuCho);
            sqlCmd.Parameters.AddWithValue("@MaHTTB", HTTB.MaHT);
            sqlCmd.Parameters.AddWithValue("@MaHTNN", HTNN.MaHT);
            sqlCmd.Parameters.AddWithValue("@YeuCau", YeuCau);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("pdkPhieuDangKy_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable New()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdkPhieuDangKy_New", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(DateTime _TuNgay, DateTime _DenNgay)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);            
            SqlCommand sqlCmd = new SqlCommand("pdkPhieuDangKy_getByDate", sqlCon);
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

        public DataTable SelectByStaff(DateTime _TuNgay, DateTime _DenNgay, int _StaffID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdkPhieuDangKy_getByDateByStaff", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            sqlCmd.Parameters.AddWithValue("@MaNV", _StaffID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByGroup(DateTime _TuNgay, DateTime _DenNgay, int _MaNV, byte _GroupID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdkPhieuDangKy_getByDateByGroup", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            sqlCmd.Parameters.AddWithValue("@MaNV", _MaNV);
            sqlCmd.Parameters.AddWithValue("@GroupID", _GroupID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByDeparment(DateTime _TuNgay, DateTime _DenNgay, int _MaNV, byte _DepID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdkPhieuDangKy_getByDateByDeparment", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            sqlCmd.Parameters.AddWithValue("@MaNV", _MaNV);
            sqlCmd.Parameters.AddWithValue("@DepID", _DepID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(string _MaBDS)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdkPhieuDangKy_getByMaBDS N'" + _MaBDS + "'", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Detail()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdkPhieuDangKy_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPDK", MaPDK);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                KhachHang.MaKH = int.Parse(dread["MaKH"].ToString());
                LoaiTien.MaLoaiTien = byte.Parse(dread["MaLoaiTien"].ToString());
                NhanVienKD.MaNV = dread["MaNVKD"].ToString() == "" ? 0 : int.Parse(dread["MaNVKD"].ToString());
                NhanVienKT.MaNV = dread["MaNVKT"].ToString() == "" ? 0 : int.Parse(dread["MaNVKT"].ToString());
                Template = dread["Template"] as string;
                DaiLy.MaDL = dread["MaDL"].ToString() == "" ? 0 : int.Parse(dread["MaDL"].ToString());
                NhanVienDL.MaNV = dread["MaNVDL"].ToString() == "" ? 0 : int.Parse(dread["MaNVDL"].ToString());
                TienGiuCho = double.Parse(dread["TienGiuCho"].ToString());
            }
            sqlCon.Close();
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pdkPhieuDangKy_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPDK", MaPDK);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdkPhieuDangKy_TaoSoPhieu", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoPhieu", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoPhieu"].Value.ToString();
        }

        public int SelectTop1()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdkPhieuDangKy_getMaPDKTop1", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaPDK", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaPDK"].Value.ToString());
        } 
	}
}
