using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class pdcPhieuDatCocCls
	{
		public int MaPDC;
		public string SoPhieu;
		public DateTime NgayKy;
		public int ThoiHan;
		public string FileAttach;
        public KhachHangCls KhachHang = new KhachHangCls();
		public string MaBDS;
		public double TienCoc, DienTich, DonGia;
		public pgcPhieuGiuChoCls PGC = new pgcPhieuGiuChoCls();
        public NhanVienCls NhanVienKD = new NhanVienCls();
        public NhanVienCls NhanVienKT = new NhanVienCls();
		public string Template;
		public pdcTinhTrangCls TinhTrang = new pdcTinhTrangCls();
        public DaiLyCls DaiLy = new DaiLyCls();
        public NhanVienDaiLyCls NhanVienDL = new NhanVienDaiLyCls();
        public byte SoLuongLo;
        public LongDuongCls Duong = new LongDuongCls();
        public PhuongHuongCls Huong = new PhuongHuongCls();
        public DuAnCls DuAn = new DuAnCls();
        public pdkGiaoDichCls GiaoDich1 = new pdkGiaoDichCls();
        public pdkGiaoDichCls GiaoDich2 = new pdkGiaoDichCls();
        public decimal GiaTriCK;
        public byte MaDVCK;
        public decimal PhuThu;

		public pdcPhieuDatCocCls()
		{
		}

		public pdcPhieuDatCocCls(int _MaPDC)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pdcPhieuDatCoc_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPDC", _MaPDC);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaPDC = int.Parse(dread["MaPDC"].ToString());
				SoPhieu = dread["SoPhieu"] as string;
				NgayKy = (DateTime)dread["NgayKy"];
				ThoiHan = int.Parse(dread["ThoiHan"].ToString());
				FileAttach = dread["FileAttach"] as string;
				KhachHang.MaKH = int.Parse(dread["MaKH"].ToString());
				MaBDS = dread["MaBDS"] as string;
				TienCoc = double.Parse(dread["TienCoc"].ToString());
                DienTich = dread["DienTich"].ToString() == "" ? 0 : double.Parse(dread["DienTich"].ToString());
                DonGia = double.Parse(dread["DonGia"].ToString());
				PGC.MaPGC = int.Parse(dread["MaPGC"].ToString());
                //SoLuongLo = byte.Parse(dread["SoLuongLo"].ToString());
                NhanVienKD.MaNV = dread["MaNVKD"].ToString() == "" ? 0 : int.Parse(dread["MaNVKD"].ToString());
				NhanVienKT.MaNV = int.Parse(dread["MaNVKT"].ToString());
				//Template = dread["Template"] as string;
				TinhTrang.MaTT = byte.Parse(dread["MaTT"].ToString());
                DaiLy.MaDL = dread["MaDL"].ToString() == "" ? 0 : int.Parse(dread["MaDL"].ToString());
				NhanVienDL.MaNV = dread["MaNVDL"].ToString() == "" ? 0 : int.Parse(dread["MaNVDL"].ToString());
                Duong.MaLD = dread["MaLD"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaLD"].ToString());
                Huong.MaPhuongHuong = dread["MaHuong"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaHuong"].ToString());
                DuAn.MaDA = dread["MaDA"].ToString() == "" ? 0 : int.Parse(dread["MaDA"].ToString());
                GiaoDich1.MaGD = dread["MaGD1"].ToString() == "" ? 0 : int.Parse(dread["MaGD1"].ToString());
                GiaoDich2.MaGD = dread["MaGD2"].ToString() == "" ? 0 : int.Parse(dread["MaGD2"].ToString());

                //GiaTriCK = (decimal)dread["GiaTriCK"];
                //MaDVCK = (byte)dread["MaDVCK"];
                //PhuThu = (decimal)dread["PhuThu"];
			}
			sqlCon.Close();
		}

		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pdcPhieuDatCoc_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
			sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
			sqlCmd.Parameters.AddWithValue("@ThoiHan", ThoiHan);
			sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
			sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            if (MaBDS != "")
                sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            else
                sqlCmd.Parameters.AddWithValue("@MaBDS", DBNull.Value);
			sqlCmd.Parameters.AddWithValue("@TienCoc", TienCoc);            
            if (PGC.MaPGC != 0)
                sqlCmd.Parameters.AddWithValue("@MaPGC", PGC.MaPGC);
            else
                sqlCmd.Parameters.AddWithValue("@MaPGC", DBNull.Value);
            if (NhanVienKD.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKD", NhanVienKD.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKD", DBNull.Value);
            if (NhanVienKT.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKT", NhanVienKT.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKT", DBNull.Value);
			//sqlCmd.Parameters.AddWithValue("@Template", Template);
			//sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaDL", DBNull.Value);
            if (NhanVienDL.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVDL", NhanVienDL.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDL", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DienTich", DienTich);
            sqlCmd.Parameters.AddWithValue("@DonGia", DonGia);
            //sqlCmd.Parameters.AddWithValue("@SoLuongLo", SoLuongLo);
            if (Duong.MaLD != 0)
                sqlCmd.Parameters.AddWithValue("@MaLD", Duong.MaLD);
            else
                sqlCmd.Parameters.AddWithValue("@MaLD", DBNull.Value);
            if (Huong.MaPhuongHuong != 0)
                sqlCmd.Parameters.AddWithValue("@MaHuong", Huong.MaPhuongHuong);
            else
                sqlCmd.Parameters.AddWithValue("@MaHuong", DBNull.Value);
            if (DuAn.MaDA != 0)
                sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            else
                sqlCmd.Parameters.AddWithValue("@MaDA", DBNull.Value);
            if (GiaoDich1.MaGD != 0)
                sqlCmd.Parameters.AddWithValue("@MaGD1", GiaoDich1.MaGD);
            else
                sqlCmd.Parameters.AddWithValue("@MaGD1", DBNull.Value);
            if (GiaoDich2.MaGD != 0)
                sqlCmd.Parameters.AddWithValue("@MaGD2", GiaoDich2.MaGD);
            else
                sqlCmd.Parameters.AddWithValue("@MaGD2", DBNull.Value);

            sqlCmd.Parameters.AddWithValue("@GiaTriCK", GiaTriCK);
            sqlCmd.Parameters.AddWithValue("@MaDVCK", MaDVCK);
            sqlCmd.Parameters.AddWithValue("@PhuThu", PhuThu);

			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void InsertJumb()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcPhieuDatCoc_addJumb", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@ThoiHan", ThoiHan);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@TienCoc", TienCoc);
            if (PGC.MaPGC != 0)
                sqlCmd.Parameters.AddWithValue("@MaPGC", PGC.MaPGC);
            else
                sqlCmd.Parameters.AddWithValue("@MaPGC", DBNull.Value);
            if (NhanVienKD.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKD", NhanVienKD.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKD", DBNull.Value);
            if (NhanVienKT.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKT", NhanVienKT.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKT", DBNull.Value);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaDL", DBNull.Value);
            if (NhanVienDL.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVDL", NhanVienDL.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDL", DBNull.Value);
            if (GiaoDich1.MaGD != 0)
                sqlCmd.Parameters.AddWithValue("@MaGD1", GiaoDich1.MaGD);
            else
                sqlCmd.Parameters.AddWithValue("@MaGD1", DBNull.Value);
            if (GiaoDich2.MaGD != 0)
                sqlCmd.Parameters.AddWithValue("@MaGD2", GiaoDich2.MaGD);
            else
                sqlCmd.Parameters.AddWithValue("@MaGD2", DBNull.Value);

            sqlCmd.Parameters.AddWithValue("@GiaTriCK", GiaTriCK);
            sqlCmd.Parameters.AddWithValue("@MaDVCK", MaDVCK);
            sqlCmd.Parameters.AddWithValue("@PhuThu", PhuThu);
            
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Search(string queryString)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdcPhieuDatCoc_search '" + queryString + "'", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pdcPhieuDatCoc_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPDC", MaPDC);
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@ThoiHan", ThoiHan);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            if (MaBDS != "")
                sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            else
                sqlCmd.Parameters.AddWithValue("@MaBDS", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@TienCoc", TienCoc);
            sqlCmd.Parameters.AddWithValue("@MaPGC", PGC.MaPGC);
            if (NhanVienKD.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKD", NhanVienKD.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKD", DBNull.Value);
            if (NhanVienKT.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKT", NhanVienKT.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKT", DBNull.Value);
            //sqlCmd.Parameters.AddWithValue("@Template", Template);
            //sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaDL", DBNull.Value);
            if (NhanVienDL.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVDL", NhanVienDL.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDL", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DienTich", DienTich);
            sqlCmd.Parameters.AddWithValue("@DonGia", DonGia);
            //sqlCmd.Parameters.AddWithValue("@SoLuongLo", SoLuongLo);
            if (Duong.MaLD != 0)
                sqlCmd.Parameters.AddWithValue("@MaLD", Duong.MaLD);
            else
                sqlCmd.Parameters.AddWithValue("@MaLD", DBNull.Value);
            if (Huong.MaPhuongHuong != 0)
                sqlCmd.Parameters.AddWithValue("@MaHuong", Huong.MaPhuongHuong);
            else
                sqlCmd.Parameters.AddWithValue("@MaHuong", DBNull.Value);
            if (DuAn.MaDA != 0)
                sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            else
                sqlCmd.Parameters.AddWithValue("@MaDA", DBNull.Value);

            sqlCmd.Parameters.AddWithValue("@GiaTriCK", GiaTriCK);
            sqlCmd.Parameters.AddWithValue("@MaDVCK", MaDVCK);
            sqlCmd.Parameters.AddWithValue("@PhuThu", PhuThu);

			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void UpdateTemplate()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcPhieuDatCoc_updateTemplate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPDC", MaPDC);
            sqlCmd.Parameters.AddWithValue("@Template", Template);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateMoneyDeposit()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcPhieuDatCoc_updateMoneyDeposit", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPDC", MaPDC);
            sqlCmd.Parameters.AddWithValue("@TienCoc", TienCoc);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("pdcPhieuDatCoc_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable Select(DateTime _TuNgay, DateTime _DenNgay, int _MaDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcPhieuDatCoc_getByDate", sqlCon);
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
            SqlCommand sqlCmd = new SqlCommand("pdcPhieuDatCoc_getByDateByStaff", sqlCon);
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
            SqlCommand sqlCmd = new SqlCommand("pdcPhieuDatCoc_getByDateByGroup", sqlCon);
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
            SqlCommand sqlCmd = new SqlCommand("pdcPhieuDatCoc_getByDateByDeparment", sqlCon);
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

        public DataTable ListNotInPDC()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdcPhieuDatCoc_listChuaBan", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable ListInPDC()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdcPhieuDatCoc_list", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable ListInPDCKG()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdcPhieuDatCoc_listKG", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable ListNotChuyenNhuong()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdcPhieuDatCoc_listNotChuyenNhuong", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcPhieuDatCoc_TaoSoPhieu", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoPhieu", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoPhieu"].Value.ToString();
        }

        /// <summary>
        /// pdcPhieuDatCoc chua duyet
        /// </summary>
        /// <param name="MaLoai">True: Dat coc ky gui, False: Dat coc binh thuong</param>
        /// <returns></returns>
        public bool Top1NotConfirm(bool MaLoai)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcPhieuDatCoc_Top1NotConfirm", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@MaLoai", MaLoai);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public byte GetAvatar()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcGetAvatar", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPDC", MaPDC);
            sqlCmd.Parameters.Add("@MaNDD", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return byte.Parse(sqlCmd.Parameters["@MaNDD"].Value.ToString());
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pdcPhieuDatCoc_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPDC", MaPDC);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void PayDeposit()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcPhieuDatCoc_payDeposit", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPDC", MaPDC);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
        
        public DataTable Expired()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdcPhieuDatCoc_Expires", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable New()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdcPhieuDatCoc_New", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }
	}
}
