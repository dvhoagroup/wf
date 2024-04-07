using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class HopDongMuaBanCls
	{
		public int MaHDMB;
        public string SoHDMB, SoHDCK, MaVach;
		public DateTime NgayKy, NgayBanGiao;
        public KhachHangCls KhachHang = new KhachHangCls();
        public KhachHangCls KhachHang2 = new KhachHangCls();
		public double SoTien;
        public double DTSD;
        public double DonGia;
        public double ThanhTien;
        public LoaiTienCls LoaiTien = new LoaiTienCls();
		public string MaBDS;
		public bool IsBank;
		public pdcPhieuDatCocCls PDC = new pdcPhieuDatCocCls();
        public NhanVienCls NhanVienKD = new NhanVienCls();
        public NhanVienCls NhanVienKT = new NhanVienCls();
		public hdmbTinhTrangCls TinhTrang = new hdmbTinhTrangCls();
        public string Template, Template2;
        public DaiLyCls DaiLy = new DaiLyCls();
        public NhanVienDaiLyCls NhanVienDL = new NhanVienDaiLyCls();
        public string FileAttach;
        public int ThoiHan;
        public hdGopVonCls HDGV = new hdGopVonCls();
        public HinhThucNhacNoCls HTNN = new HinhThucNhacNoCls();
        public HinhThucThongBaoCls HTTB = new HinhThucThongBaoCls();
        public byte MaNDD, MaNDD2;
        public decimal GiaTriCK;
        public byte MaDVCK;
        public decimal PhiMoiGioi;
        public byte MaCTP;

		public HopDongMuaBanCls()
		{
		}

		public HopDongMuaBanCls(int _MaHDMB)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaHDMB", _MaHDMB);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaHDMB = int.Parse(dread["MaHDMB"].ToString());
				SoHDMB = dread["SoHDMB"] as string;
                SoHDCK = dread["SoHDCK"] as string;
                MaVach = dread["MaVach"] as string;
                if (dread["NgayBanGiao"].ToString() != "")
                    NgayBanGiao = (DateTime)dread["NgayBanGiao"];
				NgayKy = (DateTime)dread["NgayKy"];
				KhachHang.MaKH = int.Parse(dread["MaKH"].ToString());
                KhachHang2.MaKH = dread["MaKH2"].ToString() == "" ? 0 : int.Parse(dread["MaKH2"].ToString());
				SoTien = double.Parse(dread["SoTien"].ToString());
				LoaiTien.MaLoaiTien = byte.Parse(dread["MaLoaiTien"].ToString());
				MaBDS = dread["MaBDS"] as string;
                DTSD = double.Parse(dread["DTSD"].ToString());
                DonGia = double.Parse(dread["DonGia"].ToString());
                ThanhTien = double.Parse(dread["ThanhTien"].ToString());
                if (dread["IsBank"] != DBNull.Value)
                    IsBank = (bool)dread["IsBank"];
                PDC.MaPDC = dread["MaPDC"].ToString() == "" ? 0 : int.Parse(dread["MaPDC"].ToString());
                NhanVienKD.MaNV = dread["MaNVKD"].ToString() == "" ? 0 : int.Parse(dread["MaNVKD"].ToString());
                NhanVienKT.MaNV = dread["MaNVKT"].ToString() == "" ? 0 : int.Parse(dread["MaNVKT"].ToString());
				TinhTrang.MaTT = byte.Parse(dread["MaTT"].ToString());
				//Template = dread["Template"] as string;
                //Template2 = dread["Template2"] as string;
                DaiLy.MaDL = dread["MaDL"].ToString() == "" ? 0 : int.Parse(dread["MaDL"].ToString());
                NhanVienDL.MaNV = dread["MaNVDL"].ToString() == "" ? 0 : int.Parse(dread["MaNVDL"].ToString());
                ThoiHan = int.Parse(dread["ThoiHan"].ToString());
                FileAttach = dread["FileAttach"] as string;
                HDGV.MaHDGV = dread["MaHDGV"].ToString() == "" ? 0 : int.Parse(dread["MaHDGV"].ToString());
                HTNN.MaHT = dread["MaHTNN"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaHTNN"].ToString());
                HTTB.MaHT = dread["MaHTTB"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaHTTB"].ToString());
                MaNDD = dread["MaNDD"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaNDD"].ToString());
                MaNDD2 = dread["MaNDD2"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaNDD2"].ToString());
                if (dread["GiaTriCK"] != DBNull.Value)
                    GiaTriCK = (decimal)dread["GiaTriCK"];
                if (dread["MaDVCK"] != DBNull.Value)
                    MaDVCK = (byte)dread["MaDVCK"];
                if (dread["PhiMoiGioi"] != DBNull.Value)
                    PhiMoiGioi = (decimal)dread["PhiMoiGioi"];
                if (dread["MaCTP"] != DBNull.Value)
                    MaCTP = (byte)dread["MaCTP"];
			}
			sqlCon.Close();
		}

        public void Detail()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_get2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                KhachHang.MaKH = int.Parse(dread["MaKH"].ToString());
                KhachHang2.MaKH = dread["MaKH2"].ToString() == "" ? 0 : int.Parse(dread["MaKH2"].ToString());
                LoaiTien.MaLoaiTien = byte.Parse(dread["MaLoaiTien"].ToString());
                NhanVienKD.MaNV = dread["MaNVKD"].ToString() == "" ? 0 : int.Parse(dread["MaNVKD"].ToString());
                NhanVienKT.MaNV = dread["MaNVKT"].ToString() == "" ? 0 : int.Parse(dread["MaNVKT"].ToString());
                //Template = dread["Template"] as string;
                DaiLy.MaDL = dread["MaDL"].ToString() == "" ? 0 : int.Parse(dread["MaDL"].ToString());
                NhanVienDL.MaNV = dread["MaNVDL"].ToString() == "" ? 0 : int.Parse(dread["MaNVDL"].ToString());
                SoHDMB = dread["SoHDMB"] as string;
                NgayKy = (DateTime)dread["NgayKy"];
            }
            sqlCon.Close();
        }

		public int Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaHDMB", SqlDbType.Int).Direction = ParameterDirection.Output;
			sqlCmd.Parameters.AddWithValue("@SoHDMB", SoHDMB);
            sqlCmd.Parameters.AddWithValue("@SoHDCK", SoHDCK);
			sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@NgayBanGiao", NgayBanGiao);
			sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            if (KhachHang2.MaKH != 0)
                sqlCmd.Parameters.AddWithValue("@MaKH2", KhachHang2.MaKH);
            else
                sqlCmd.Parameters.AddWithValue("@MaKH2", DBNull.Value);
			sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCmd.Parameters.AddWithValue("@DTSD", DTSD);
            sqlCmd.Parameters.AddWithValue("@DonGia", DonGia);
            sqlCmd.Parameters.AddWithValue("@ThanhTien", ThanhTien);
			sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@GiaTriCK", GiaTriCK);
            sqlCmd.Parameters.AddWithValue("@MaDVCK", MaDVCK);
            sqlCmd.Parameters.AddWithValue("@PhiMoiGioi", PhiMoiGioi);
            sqlCmd.Parameters.AddWithValue("@MaCTP", MaCTP);
			sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
			sqlCmd.Parameters.AddWithValue("@IsBank", IsBank);
            if (PDC.MaPDC != 0)
                sqlCmd.Parameters.AddWithValue("@MaPDC", PDC.MaPDC);
            else
                sqlCmd.Parameters.AddWithValue("@MaPDC", DBNull.Value);
            if (NhanVienKD.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKD", NhanVienKD.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKD", DBNull.Value);
            if (NhanVienKT.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKT", NhanVienKT.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKT", DBNull.Value);
			sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
			//sqlCmd.Parameters.AddWithValue("@Template", Template);
            //sqlCmd.Parameters.AddWithValue("@Template2", Template2);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaDL", DBNull.Value);
            if (NhanVienDL.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVDL", NhanVienDL.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDL", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@ThoiHan", ThoiHan);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            if(HDGV.MaHDGV !=0)
                sqlCmd.Parameters.AddWithValue("@MaHDGV", HDGV.MaHDGV);
            else
                sqlCmd.Parameters.AddWithValue("@MaHDGV", DBNull.Value);
            if (MaNDD != 0)
                sqlCmd.Parameters.AddWithValue("@MaNDD", MaNDD);
            else
                sqlCmd.Parameters.AddWithValue("@MaNDD", DBNull.Value);
            if (MaNDD2 != 0)
                sqlCmd.Parameters.AddWithValue("@MaNDD2", MaNDD2);
            else
                sqlCmd.Parameters.AddWithValue("@MaNDD2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaHTTB", HTTB.MaHT);
            sqlCmd.Parameters.AddWithValue("@MaHTNN", HTNN.MaHT);

			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaHDMB"].Value.ToString());
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCmd.Parameters.AddWithValue("@SoHDMB", SoHDMB);
            sqlCmd.Parameters.AddWithValue("@SoHDCK", SoHDCK);
            sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@NgayBanGiao", NgayBanGiao);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            if (KhachHang2.MaKH != 0)
                sqlCmd.Parameters.AddWithValue("@MaKH2", KhachHang2.MaKH);
            else
                sqlCmd.Parameters.AddWithValue("@MaKH2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCmd.Parameters.AddWithValue("@DTSD", DTSD);
            sqlCmd.Parameters.AddWithValue("@DonGia", DonGia);
            sqlCmd.Parameters.AddWithValue("@ThanhTien", ThanhTien);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@GiaTriCK", GiaTriCK);
            sqlCmd.Parameters.AddWithValue("@MaDVCK", MaDVCK);
            sqlCmd.Parameters.AddWithValue("@PhiMoiGioi", PhiMoiGioi);
            sqlCmd.Parameters.AddWithValue("@MaCTP", MaCTP);

            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@IsBank", IsBank);
            if (PDC.MaPDC != 0)
                sqlCmd.Parameters.AddWithValue("@MaPDC", PDC.MaPDC);
            else
                sqlCmd.Parameters.AddWithValue("@MaPDC", DBNull.Value);
            if (NhanVienKD.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKD", NhanVienKD.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKD", DBNull.Value);
            if (NhanVienKT.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKT", NhanVienKT.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKT", DBNull.Value);
            //sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            //sqlCmd.Parameters.AddWithValue("@Template", Template);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaDL", DBNull.Value);
            if (NhanVienDL.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVDL", NhanVienDL.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDL", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@ThoiHan", ThoiHan);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            if (HDGV.MaHDGV != 0)
                sqlCmd.Parameters.AddWithValue("@MaHDGV", HDGV.MaHDGV);
            else
                sqlCmd.Parameters.AddWithValue("@MaHDGV", DBNull.Value);
            if (MaNDD != 0)
                sqlCmd.Parameters.AddWithValue("@MaNDD", MaNDD);
            else
                sqlCmd.Parameters.AddWithValue("@MaNDD", DBNull.Value);
            if (MaNDD2 != 0)
                sqlCmd.Parameters.AddWithValue("@MaNDD2", MaNDD2);
            else
                sqlCmd.Parameters.AddWithValue("@MaNDD2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaHTTB", HTTB.MaHT);
            sqlCmd.Parameters.AddWithValue("@MaHTNN", HTNN.MaHT);

			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void UpdateTemplate()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_updateTemplate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCmd.Parameters.AddWithValue("@Template", Template);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateTemplate2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_updateTemplate2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCmd.Parameters.AddWithValue("@Template2", Template2);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("HopDongMuaBan_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        /// <summary>
        /// Xuat hoa don GTGT
        /// </summary>
        /// <param name="_MaHDMB"></param>
        /// <param name="_DotTT"></param>
        /// <returns></returns>
        public DataTable SelectDotTT(int _MaHDMB, byte _DotTT)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("HopDongMuaBan_getByMaHDMB_DotTT " + _MaHDMB + "," + _DotTT, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(DateTime _TuNgay, DateTime _DenNgay, int _MaDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_getByDate", sqlCon);
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
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_getByDateByStaff", sqlCon);
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
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_getByDateByGroup", sqlCon);
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
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_getByDateByDeparment", sqlCon);
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

        public DataTable SelectTL(DateTime _TuNgay, DateTime _DenNgay)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_getByDateDaTL", sqlCon);
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

        public DataTable ListNotInHDMB()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("HopDongMuaBan_list", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Search(string queryString)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("HopDongMuaBan_search '" + queryString + "'", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable ListNotInPKG()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("HopDongMuaBan_listNotKyGui", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable ListNotInHDMB_HDGV()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("HopDongMuaBan_listHDGV", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable ListNotInHDCN()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("HopDongMuaBan_listNotChuyenNhuong", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable ListNotInHDCN2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("HopDongMuaBan_listNotChuyenNhuong2 " + MaHDMB, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable LichSuPhongToa()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("HopDongMuaBan_getAllPhongToa " + MaHDMB, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void ChoThanhLy()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_ChoThanhLy", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_TaoSoPhieu", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoPhieu", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoPhieu"].Value.ToString();
        }

        public string TaoSoHDCK()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_TaoSoHDCK", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoHDCK", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoHDCK"].Value.ToString();
        }

        public DataTable LichTTTemp()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdmbLichTemp_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable LichThanhToan()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdmbLichThanhToan_getByMaHDMB " + MaHDMB, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public double Payment()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_SoTienDaThu", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCmd.Parameters.Add("@SoTien", SqlDbType.Money).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return double.Parse(sqlCmd.Parameters["@SoTien"].Value.ToString());
        }

        /// <summary>
        /// pdcPhieuDatCoc chua duyet
        /// </summary>
        /// <returns></returns>
        public bool Top1NotConfirm()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_Top1NotConfirm", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public bool CheckKyGui()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_checkKyGui", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public bool CheckThanhLy()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_checkThanhLy", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public bool Check()
        {

            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_check", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoHDMB", SoHDMB);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public int CheckHDMB()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HopDongMuaBan_checkHDMB_BDS", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaSo", MaBDS);
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoHDMB);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public DataRow GetTemplate(byte _MaBM)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DuAn_BieuMau_getByMaBDS '" + MaBDS + "'," + _MaBM, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0].Rows[0];
        }
	}
}
