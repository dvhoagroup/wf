using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class pgcPhieuGiuChoCls
	{
		public int MaPGC;
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
		public string Template;
        public DaiLyCls DaiLy = new DaiLyCls();
        public NhanVienDaiLyCls NhanVienDL = new NhanVienDaiLyCls();
        public pgcTinhTrangCls TinhTrang = new pgcTinhTrangCls();
        public HinhThucNhacNoCls HTNN = new HinhThucNhacNoCls();
        public HinhThucThongBaoCls HTTB = new HinhThucThongBaoCls();
        public double TienGiuCho;
        public bool IsDeposit;
        public int ParentID, MaPDK;
        public pdkGiaoDichCls GiaoDich1 = new pdkGiaoDichCls();
        public pdkGiaoDichCls GiaoDich2 = new pdkGiaoDichCls();
        public decimal GiaTriCK;
        public byte MaDVCK;
        public decimal PhuThu;

		public pgcPhieuGiuChoCls()
		{
		}

		public pgcPhieuGiuChoCls(int _MaPGC)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPGC", _MaPGC);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaPGC = int.Parse(dread["MaPGC"].ToString());
				SoPhieu = dread["SoPhieu"] as string;
				NgayKy = (DateTime)dread["NgayKy"];
				ThoiHan = int.Parse(dread["ThoiHan"].ToString());
				FileAttach = dread["FileAttach"] as string;
				MaBDS = dread["MaBDS"] as string;
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
                IsDeposit = (bool)dread["IsDeposited"];
                ParentID = dread["ParentID"].ToString() == "" ? (byte)0 : byte.Parse(dread["ParentID"].ToString());
                MaPDK = dread["MaPDK"].ToString() == "" ? 0 : int.Parse(dread["MaPDK"].ToString());
                GiaoDich1.MaGD = dread["MaGD1"].ToString() == "" ? 0 : int.Parse(dread["MaGD1"].ToString());
                GiaoDich2.MaGD = dread["MaGD2"].ToString() == "" ? 0 : int.Parse(dread["MaGD2"].ToString());

                GiaTriCK = (decimal)dread["GiaTriCK"];
                MaDVCK = (byte)dread["MaDVCK"];
                PhuThu = (decimal)dread["PhuThu"];
			}
			sqlCon.Close();
		}

		public int Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaPGC", SqlDbType.Int).Direction = ParameterDirection.Output;
			sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
			sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
			sqlCmd.Parameters.AddWithValue("@ThoiHan", ThoiHan);
			sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            if (MaBDS != "")
                sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            else
                sqlCmd.Parameters.AddWithValue("@MaBDS", DBNull.Value);
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
            sqlCmd.Parameters.AddWithValue("@IsDeposit", IsDeposit);
            if (ParentID != 0)
                sqlCmd.Parameters.AddWithValue("@ParentID", ParentID);
            else
                sqlCmd.Parameters.AddWithValue("@ParentID", DBNull.Value);
            if (MaPDK != 0)
                sqlCmd.Parameters.AddWithValue("@MaPDK", MaPDK);
            else
                sqlCmd.Parameters.AddWithValue("@MaPDK", DBNull.Value);
            try
            {
                sqlCmd.Parameters.AddWithValue("@MaGD1", GiaoDich1.MaGD);
            }
            catch { sqlCmd.Parameters.AddWithValue("@MaGD1", DBNull.Value); }
            try
            {
                sqlCmd.Parameters.AddWithValue("@MaGD2", GiaoDich2.MaGD);
            }
            catch { sqlCmd.Parameters.AddWithValue("@MaGD2", DBNull.Value); }

            sqlCmd.Parameters.AddWithValue("@GiaTriCK", GiaTriCK);
            sqlCmd.Parameters.AddWithValue("@MaDVCK", MaDVCK);
            sqlCmd.Parameters.AddWithValue("@PhuThu", PhuThu);
            
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaPGC"].Value.ToString());
		}

        public int InsertByDL()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_addByDL", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaPGC", SqlDbType.Int).Direction = ParameterDirection.Output;
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
            if (NhanVienKD.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKD", NhanVienKD.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKD", DBNull.Value);
            if (NhanVienKT.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKT", NhanVienKT.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKT", DBNull.Value);
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
            sqlCmd.Parameters.AddWithValue("@IsDeposit", IsDeposit);
            if (ParentID != 0)
                sqlCmd.Parameters.AddWithValue("@ParentID", ParentID);
            else
                sqlCmd.Parameters.AddWithValue("@ParentID", DBNull.Value);
            if (MaPDK != 0)
                sqlCmd.Parameters.AddWithValue("@MaPDK", MaPDK);
            else
                sqlCmd.Parameters.AddWithValue("@MaPDK", DBNull.Value);
            try
            {
                sqlCmd.Parameters.AddWithValue("@MaGD1", GiaoDich1.MaGD);
            }
            catch { sqlCmd.Parameters.AddWithValue("@MaGD1", DBNull.Value); }
            try
            {
                sqlCmd.Parameters.AddWithValue("@MaGD2", GiaoDich2.MaGD);
            }
            catch { sqlCmd.Parameters.AddWithValue("@MaGD2", DBNull.Value); }

            sqlCmd.Parameters.AddWithValue("@GiaTriCK", GiaTriCK);
            sqlCmd.Parameters.AddWithValue("@MaDVCK", MaDVCK);
            sqlCmd.Parameters.AddWithValue("@PhuThu", PhuThu);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaPGC"].Value.ToString());
        }

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@ThoiHan", ThoiHan);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            if (MaBDS != "")
                sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            else
                sqlCmd.Parameters.AddWithValue("@MaBDS", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            if (MaNDD != 0)
                sqlCmd.Parameters.AddWithValue("@MaNDD", MaNDD);
            else
                sqlCmd.Parameters.AddWithValue("@MaNDD", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DTSD", DTSD);
            sqlCmd.Parameters.AddWithValue("@DonGia", DonGia);
            sqlCmd.Parameters.AddWithValue("@ThanhTien", ThanhTien);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            if (NhanVienKD.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKD", NhanVienKD.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKD", DBNull.Value);
            if (NhanVienKT.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKT", NhanVienKT.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKT", DBNull.Value);
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
            sqlCmd.Parameters.AddWithValue("@IsDeposit", IsDeposit);
            if (ParentID != 0)
                sqlCmd.Parameters.AddWithValue("@ParentID", ParentID);
            else
                sqlCmd.Parameters.AddWithValue("@ParentID", DBNull.Value);
            if (MaPDK != 0)
                sqlCmd.Parameters.AddWithValue("@MaPDK", MaPDK);
            else
                sqlCmd.Parameters.AddWithValue("@MaPDK", DBNull.Value);

            sqlCmd.Parameters.AddWithValue("@GiaTriCK", GiaTriCK);
            sqlCmd.Parameters.AddWithValue("@MaDVCK", MaDVCK);
            sqlCmd.Parameters.AddWithValue("@PhuThu", PhuThu);

			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void UpdateState()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_updateState", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("pgcPhieuGiuCho_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable LichTTByProject(int _MaDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcPhieuGiuCho_getLichTTByProject " + _MaDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable NextPlay()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcPhieuGiuCho_nextPlay " + NhanVienKD.MaNV, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Expires()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_Expires", sqlCon);
            
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable New()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcPhieuGiuCho_New", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable ListNotInPDC()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcPhieuGiuCho_listChuaDatCoc", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable ListNotInPDC_KG()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcPhieuGiuCho_listChuaDatCocKG", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable ListInPDC()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcPhieuGiuCho_list", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        } 

        public DataTable Select(DateTime _TuNgay, DateTime _DenNgay, int _MaDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);            
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_getByDate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            sqlCmd.Parameters.AddWithValue("@MaDA", _MaDA);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByStaff(DateTime _TuNgay, DateTime _DenNgay, int _MaDA, int _StaffID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_getByDateByStaff", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            sqlCmd.Parameters.AddWithValue("@MaDA", _MaDA);
            sqlCmd.Parameters.AddWithValue("@MaNV", _StaffID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByGroup(DateTime _TuNgay, DateTime _DenNgay, int _MaDA, int _MaNV, byte _GroupID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_getByDateByGroup", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            sqlCmd.Parameters.AddWithValue("@MaDA", _MaDA);
            sqlCmd.Parameters.AddWithValue("@MaNV", _MaNV);
            sqlCmd.Parameters.AddWithValue("@GroupID", _GroupID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByDeparment(DateTime _TuNgay, DateTime _DenNgay, int _MaDA, int _MaNV, byte _DepID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_getByDateByDeparment", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            sqlCmd.Parameters.AddWithValue("@MaDA", _MaDA);
            sqlCmd.Parameters.AddWithValue("@MaNV", _MaNV);
            sqlCmd.Parameters.AddWithValue("@DepID", _DepID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(string _MaBDS, int _MaNV)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcPhieuGiuCho_getByMaBDS N'" + _MaBDS + "'," + _MaNV, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectKG(string _MaBDS, int _MaNV)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcPhieuGiuCho_getByMaBDS_KG N'" + _MaBDS + "'," + _MaNV, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable LichThanhToan(int _MaPGC)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcLichThanhToan_getByMaPGC " + _MaPGC, sqlCon);
            
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable LichThanhToan(int _MaPGC, bool _IsPay)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcLichThanhToan_getByMaPGCNotPay " + _MaPGC, sqlCon);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Detail()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_get2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                KhachHang.MaKH = int.Parse(dread["MaKH"].ToString());
                LoaiTien.MaLoaiTien = byte.Parse(dread["MaLoaiTien"].ToString());
                NhanVienKD.MaNV = dread["MaNVKD"].ToString() == "" ? 0 : int.Parse(dread["MaNVKD"].ToString());
                NhanVienKT.MaNV = dread["MaNVKT"].ToString() == "" ? 0 : int.Parse(dread["MaNVKT"].ToString());
                //Template = dread["Template"] as string;
                DaiLy.MaDL = dread["MaDL"].ToString() == "" ? 0 : int.Parse(dread["MaDL"].ToString());
                NhanVienDL.MaNV = dread["MaNVDL"].ToString() == "" ? 0 : int.Parse(dread["MaNVDL"].ToString());
                TienGiuCho = double.Parse(dread["TienGiuCho"].ToString());
            }
            sqlCon.Close();
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void DeleteNextPlay()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcNextPlay_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void DeleteNextPlay2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcNextPlay_delete2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_TaoSoPhieu", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoPhieu", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoPhieu"].Value.ToString();
        }

        public bool CheckDeposit()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_checkDeposit", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.Add("@Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Result"].Value.ToString());
        }

        public double Payment()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_SoTienDaThu", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            sqlCmd.Parameters.Add("@SoTien", SqlDbType.Money).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return double.Parse(sqlCmd.Parameters["@SoTien"].Value.ToString());
        }

        public int GetMaPGCByMaBDS(string _MaBDS)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_getMaPGCByMaBDS", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure; 
            sqlCmd.Parameters.AddWithValue("@MaBDS", _MaBDS);
            sqlCmd.Parameters.Add("@MaPGC", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaPGC"].Value.ToString());
        }

        public byte GetAvatar()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcGetAvatar", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            sqlCmd.Parameters.Add("@MaNDD", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return byte.Parse(sqlCmd.Parameters["@MaNDD"].Value.ToString());
        }

        public void UpdateAvatar()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_updataAvatar", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            sqlCmd.Parameters.AddWithValue("@MaNDD", MaNDD);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateTransaction()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdkGiaoDich_updateTransaction", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public string GetInfoHDMB()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_getMaHDMB", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            sqlCmd.Parameters.Add("@Re", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@Re"].Value.ToString();
        }

        public string GetInfoPDC()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuGiuCho_getInfoPDC", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            sqlCmd.Parameters.Add("@Re", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("@SoTien", SqlDbType.Money).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            TienGiuCho = double.Parse(sqlCmd.Parameters["@SoTien"].Value.ToString());
            return sqlCmd.Parameters["@Re"].Value.ToString();            
        }
	}
}
