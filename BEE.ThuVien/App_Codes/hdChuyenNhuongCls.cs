using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class hdChuyenNhuongCls
    {
        public int MaHDCN;
        public string SoHDCN;
        public DateTime NgayKy;
        public KhachHangCls KHCN = new KhachHangCls();
        public KhachHangCls KHNCN = new KhachHangCls();
        public KhachHangCls KHNCN2 = new KhachHangCls();
        public BatDongSanCls BDS = new BatDongSanCls();
        public double SoTienDaNop;
        public double SoTienKhac;
        public double SoTienCN;
        public string GiayToKhac;
        public int ThoiHanTT;
        public string FileAttach, Template;
        public hdTinhTrangCls TinhTrang = new hdTinhTrangCls();
        public string GhiChu;
        public HopDongMuaBanCls HDMB = new HopDongMuaBanCls();
        public NhanVienCls NVKT = new NhanVienCls();
        public NhanVienDaiLyCls NVDL = new NhanVienDaiLyCls();
        public DaiLyCls DaiLy = new DaiLyCls();
        public double DonGiaCN;
        public pdcPhieuDatCocCls PDC = new pdcPhieuDatCocCls();
        public string SoGXN, SanCapGXN, SoHoaDon, NoiTBTTNCN, SoBienLaiNopThue, VanPhongCongChung;
        public DateTime NgayKyGXN, NgayKyHoaDon, NgayTBTTNCN, NgayNopThue;

        public hdChuyenNhuongCls()
        {
            KHNCN2.MaKH = 0;
        }

        public hdChuyenNhuongCls(int _MaHDCN)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdChuyenNhuong_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDCN", _MaHDCN);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaHDCN = int.Parse(dread["MaHDCN"].ToString());
                SoHDCN = dread["SoHDCN"] as string;
                SoGXN = dread["SoGXN"] as string;
                SanCapGXN = dread["SanCapGXN"] as string;
                SoHoaDon = dread["SoHoaDon"] as string;
                NoiTBTTNCN = dread["NoiTBTTNCN"] as string;
                SoBienLaiNopThue = dread["SoBienLaiNopThue"] as string;
                VanPhongCongChung = dread["VanPhongCongChung"] as string;
                if (dread["NgayKy"].ToString() != "")
                    NgayKy = (DateTime)dread["NgayKy"];

                if (dread["NgayKyGXN"].ToString() != "")
                    NgayKyGXN = (DateTime)dread["NgayKyGXN"];

                if (dread["NgayKyHoaDon"].ToString() != "")
                    NgayKyHoaDon = (DateTime)dread["NgayKyHoaDon"];

                if (dread["NgayTBTTNCN"].ToString() != "")
                    NgayTBTTNCN = (DateTime)dread["NgayTBTTNCN"];

                if (dread["NgayNopThue"].ToString() != "")
                    NgayNopThue = (DateTime)dread["NgayNopThue"];
                KHCN.MaKH = int.Parse(dread["MaKHCN"].ToString());
                KHNCN.MaKH = int.Parse(dread["MaKHNCN"].ToString());
                if (dread["MaKHNCN2"].ToString() != "")
                    KHNCN2.MaKH = int.Parse(dread["MaKHNCN2"].ToString());
                BDS.MaBDS = dread["MaBDS"] as string;
                SoTienDaNop = double.Parse(dread["SoTienDaNop"].ToString());
                SoTienKhac = double.Parse(dread["SoTienKhac"].ToString());
                SoTienCN = double.Parse(dread["SoTienCN"].ToString());
                DonGiaCN = double.Parse(dread["DonGia"].ToString());
                GiayToKhac = dread["GiayToKhac"] as string;
                ThoiHanTT = int.Parse(dread["ThoiHanTT"].ToString());
                FileAttach = dread["FileAttach"] as string;
                TinhTrang.MaTT = byte.Parse(dread["MaTT"].ToString());
                GhiChu = dread["GhiChu"] as string;
                //Template = dread["Template"] as string;
                HDMB.MaHDMB = int.Parse(dread["MaHDMB"].ToString());
                NVKT.MaNV = dread["MaNVKT"].ToString() == "" ? 0 : int.Parse(dread["MaNVKT"].ToString());
                NVDL.MaNV = dread["MaNVDL"].ToString() == "" ? 0 : int.Parse(dread["MaNVDL"].ToString());
                DaiLy.MaDL = dread["MaDL"].ToString() == "" ? 0 : int.Parse(dread["MaDL"].ToString());
                PDC.MaPDC = dread["MaPDC"].ToString() == "" ? 0 : int.Parse(dread["MaPDC"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdChuyenNhuong_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoHDCN", SoHDCN);
            if (NgayKy.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            else
                sqlCmd.Parameters.AddWithValue("@NgayKy", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaKHCN", KHCN.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaKHNCN", KHNCN.MaKH);
            if(KHNCN2.MaKH!=0)
                sqlCmd.Parameters.AddWithValue("@MaKHNCN2", KHNCN2.MaKH);
            else
                sqlCmd.Parameters.AddWithValue("@MaKHNCN2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaBDS);
            sqlCmd.Parameters.AddWithValue("@SoTienDaNop", SoTienDaNop);
            sqlCmd.Parameters.AddWithValue("@SoTienKhac", SoTienKhac);
            sqlCmd.Parameters.AddWithValue("@DonGiaCN", SoTienCN);
            sqlCmd.Parameters.AddWithValue("@GiayToKhac", GiayToKhac);
            sqlCmd.Parameters.AddWithValue("@ThoiHanTT", ThoiHanTT);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            if (NVKT.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKT", NVKT.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKT", DBNull.Value);
            //sqlCmd.Parameters.AddWithValue("@Template", Template);
            if (PDC.MaPDC != 0)
                sqlCmd.Parameters.AddWithValue("@MaPDC", PDC.MaPDC);
            else
                sqlCmd.Parameters.AddWithValue("@MaPDC", DBNull.Value);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaDL", DBNull.Value);
            if (NVDL.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVDL", NVDL.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDL", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@SoGXN", SoGXN);
            sqlCmd.Parameters.AddWithValue("@SoHoaDon", SoHoaDon);
            sqlCmd.Parameters.AddWithValue("@SoBienLaiNopThue", SoBienLaiNopThue);
            sqlCmd.Parameters.AddWithValue("@NoiTBTTNCN", NoiTBTTNCN);
            sqlCmd.Parameters.AddWithValue("@SanCapGXN", SanCapGXN);
            sqlCmd.Parameters.AddWithValue("@VanPhongCongChung", VanPhongCongChung);
            if (NgayKyGXN.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayKyGXN", NgayKyGXN);
            else
                sqlCmd.Parameters.AddWithValue("@NgayKyGXN", DBNull.Value);

            if (NgayKyHoaDon.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayKyHoaDon", NgayKyHoaDon);
            else
                sqlCmd.Parameters.AddWithValue("@NgayKyHoaDon", DBNull.Value);

            if (NgayNopThue.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayNopThue", NgayNopThue);
            else
                sqlCmd.Parameters.AddWithValue("@NgayNopThue", DBNull.Value);

            if (NgayTBTTNCN.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayTBTTNCN", NgayTBTTNCN);
            else
                sqlCmd.Parameters.AddWithValue("@NgayTBTTNCN", DBNull.Value);
            
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Import()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdChuyenNhuong_import", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoHDCN", SoHDCN);
            if (NgayKy.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            else
                sqlCmd.Parameters.AddWithValue("@NgayKy", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaKHCN", KHCN.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaKHNCN", KHNCN.MaKH);
            if (KHNCN2.MaKH != 0)
                sqlCmd.Parameters.AddWithValue("@MaKHNCN2", KHNCN.MaKH);
            else
                sqlCmd.Parameters.AddWithValue("@MaKHNCN2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaBDS);
            sqlCmd.Parameters.AddWithValue("@SoTienDaNop", SoTienDaNop);
            sqlCmd.Parameters.AddWithValue("@SoTienKhac", SoTienKhac);
            sqlCmd.Parameters.AddWithValue("@DonGiaCN", SoTienCN);
            sqlCmd.Parameters.AddWithValue("@GiayToKhac", GiayToKhac);
            sqlCmd.Parameters.AddWithValue("@ThoiHanTT", ThoiHanTT);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            if (NVKT.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKT", NVKT.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKT", DBNull.Value);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdChuyenNhuong_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDCN", MaHDCN);
            sqlCmd.Parameters.AddWithValue("@SoHDCN", SoHDCN);
            if (NgayKy.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            else
                sqlCmd.Parameters.AddWithValue("@NgayKy", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaKHCN", KHCN.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaKHNCN", KHNCN.MaKH);
            if (KHNCN2.MaKH != 0)
                sqlCmd.Parameters.AddWithValue("@MaKHNCN2", KHNCN2.MaKH);
            else
                sqlCmd.Parameters.AddWithValue("@MaKHNCN2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaBDS);
            sqlCmd.Parameters.AddWithValue("@SoTienDaNop", SoTienDaNop);
            sqlCmd.Parameters.AddWithValue("@SoTienKhac", SoTienKhac);
            sqlCmd.Parameters.AddWithValue("@DonGiaCN", SoTienCN);
            sqlCmd.Parameters.AddWithValue("@GiayToKhac", GiayToKhac);
            sqlCmd.Parameters.AddWithValue("@ThoiHanTT", ThoiHanTT);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            if (NVKT.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKT", NVKT.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKT", DBNull.Value);
            if (PDC.MaPDC != 0)
                sqlCmd.Parameters.AddWithValue("@MaPDC", PDC.MaPDC);
            else
                sqlCmd.Parameters.AddWithValue("@MaPDC", DBNull.Value);
            //sqlCmd.Parameters.AddWithValue("@Template", Template);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaDL", DBNull.Value);
            if (NVDL.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVDL", NVDL.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDL", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@SoGXN", SoGXN);
            sqlCmd.Parameters.AddWithValue("@SoHoaDon", SoHoaDon);
            sqlCmd.Parameters.AddWithValue("@SoBienLaiNopThue", SoBienLaiNopThue);
            sqlCmd.Parameters.AddWithValue("@NoiTBTTNCN", NoiTBTTNCN);
            sqlCmd.Parameters.AddWithValue("@SanCapGXN", SanCapGXN);
            sqlCmd.Parameters.AddWithValue("@VanPhongCongChung", VanPhongCongChung);
            if (NgayKyGXN.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayKyGXN", NgayKyGXN);
            else
                sqlCmd.Parameters.AddWithValue("@NgayKyGXN", DBNull.Value);

            if (NgayKyHoaDon.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayKyHoaDon", NgayKyHoaDon);
            else
                sqlCmd.Parameters.AddWithValue("@NgayKyHoaDon", DBNull.Value);

            if (NgayNopThue.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayNopThue", NgayNopThue);
            else
                sqlCmd.Parameters.AddWithValue("@NgayNopThue", DBNull.Value);

            if (NgayTBTTNCN.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayTBTTNCN", NgayTBTTNCN);
            else
                sqlCmd.Parameters.AddWithValue("@NgayTBTTNCN", DBNull.Value);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Detail()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdChuyenNhuong_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDCN", MaHDCN);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaHDCN = int.Parse(dread["MaHDCN"].ToString());
                SoHDCN = dread["SoHDCN"] as string;
                NgayKy = (DateTime)dread["NgayKy"];
                KHCN.MaKH = int.Parse(dread["MaKHCN"].ToString());
                KHNCN.MaKH = int.Parse(dread["MaKHNCN"].ToString());
                BDS.MaBDS = dread["MaBDS"] as string;
                SoTienDaNop = double.Parse(dread["SoTienDaNop"].ToString());
                SoTienKhac = double.Parse(dread["SoTienKhac"].ToString());
                SoTienCN = double.Parse(dread["SoTienCN"].ToString());
                DonGiaCN = double.Parse(dread["DonGia"].ToString());
                GiayToKhac = dread["GiayToKhac"] as string;
                ThoiHanTT = int.Parse(dread["ThoiHanTT"].ToString());
                FileAttach = dread["FileAttach"] as string;
                TinhTrang.MaTT = byte.Parse(dread["MaTT"].ToString());
                GhiChu = dread["GhiChu"] as string;
                Template = dread["Template"] as string;
                HDMB.MaHDMB = int.Parse(dread["MaHDMB"].ToString());
                NVKT.MaNV = dread["MaNVKT"].ToString() == "" ? 0 : int.Parse(dread["MaNVKT"].ToString());
                NVDL.MaNV = dread["MaNVDL"].ToString() == "" ? 0 : int.Parse(dread["MaNVDL"].ToString());
                DaiLy.MaDL = dread["MaDL"].ToString() == "" ? 0 : int.Parse(dread["MaDL"].ToString());
                PDC.MaPDC = dread["MaPDC"].ToString() == "" ? 0 : int.Parse(dread["MaPDC"].ToString());
            }
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdChuyenNhuong_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectBy()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdChuyenNhuong_getByMaHDMB " + HDMB.MaHDMB, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByBDSvsHDMB()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdChuyenNhuong_getByMaBDSvsMaHDMB '" + BDS.MaBDS + "'," + HDMB.MaHDMB, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable List()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdChuyenNhuong_listBy " + MaHDCN, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable ListChuaXacNhan()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdChuyenNhuong_listChuaXacNhan " + MaHDCN, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(DateTime _TuNgay, DateTime _DenNgay, string _DuAn, string _BlockID, string _TangNha)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdChuyenNhuong_getByDate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            sqlCmd.Parameters.AddWithValue("@MaDA", _DuAn);
            sqlCmd.Parameters.AddWithValue("@BlockID", _BlockID);
            sqlCmd.Parameters.AddWithValue("@MaTang", _TangNha);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByStaff(DateTime _TuNgay, DateTime _DenNgay, int _StaffID, string _DuAn, string _BlockID, string _TangNha)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdChuyenNhuong_getByDateByStaff", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            sqlCmd.Parameters.AddWithValue("@MaNV", _StaffID);
            sqlCmd.Parameters.AddWithValue("@MaDA", _DuAn);
            sqlCmd.Parameters.AddWithValue("@BlockID", _BlockID);
            sqlCmd.Parameters.AddWithValue("@MaTang", _TangNha);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByGroup(DateTime _TuNgay, DateTime _DenNgay, int _MaNV, byte _GroupID, string _DuAn, string _BlockID, string _TangNha)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdChuyenNhuong_getByDateByGroup", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            sqlCmd.Parameters.AddWithValue("@MaNV", _MaNV);
            sqlCmd.Parameters.AddWithValue("@GroupID", _GroupID);
            sqlCmd.Parameters.AddWithValue("@MaDA", _DuAn);
            sqlCmd.Parameters.AddWithValue("@BlockID", _BlockID);
            sqlCmd.Parameters.AddWithValue("@MaTang", _TangNha);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByDeparment(DateTime _TuNgay, DateTime _DenNgay, int _MaNV, byte _DepID, string _DuAn, string _BlockID, string _TangNha)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdChuyenNhuong_getByDateByDeparment", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            sqlCmd.Parameters.AddWithValue("@MaNV", _MaNV);
            sqlCmd.Parameters.AddWithValue("@DepID", _DepID);
            sqlCmd.Parameters.AddWithValue("@MaDA", _DuAn);
            sqlCmd.Parameters.AddWithValue("@BlockID", _BlockID);
            sqlCmd.Parameters.AddWithValue("@MaTang", _TangNha);
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
            SqlCommand sqlCmd = new SqlCommand("hdChuyenNhuong_TaoSoHDCN", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoHDCN", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoHDCN"].Value.ToString();
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdChuyenNhuong_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDCN", MaHDCN);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public bool Top1NotConfirm()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdChuyenNhuong_Top1NotConfirm", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaBDS);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public bool CheckNotConfirm()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdChuyenNhuong_check", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public void Insert_HDMB_()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("_HDMB_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoHDMB", HDMB.SoHDMB);
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaSo);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Insert_HDMB_Thu_Chi(string soPhieu, byte dotTT, bool loaiPhieu)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("_HDMB_Thu_Chi_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoHD", HDMB.SoHDMB);
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaSo);
            sqlCmd.Parameters.AddWithValue("@SoPhieu", soPhieu);
            if (dotTT != 0)
                sqlCmd.Parameters.AddWithValue("@DotTT", dotTT);
            else
                sqlCmd.Parameters.AddWithValue("@DotTT", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@KhachHang", KHCN.HoKH);
            sqlCmd.Parameters.AddWithValue("@SoCMND", "'" + KHCN.SoCMND);
            sqlCmd.Parameters.AddWithValue("@MaSoThue", "'" + KHCN.MaSoThueCT);
            sqlCmd.Parameters.AddWithValue("@LoaiPhieu", loaiPhieu);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Insert_KH_()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("_KhachHang_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@KHCN", KHCN.HoKH);
            sqlCmd.Parameters.AddWithValue("@SoCMND", KHCN.SoCMND);
            sqlCmd.Parameters.AddWithValue("@KHNCNB1", KHNCN.HoKH);
            sqlCmd.Parameters.AddWithValue("@SoCMNDB1", KHNCN.SoCMND);
            sqlCmd.Parameters.AddWithValue("@KHNCNB2", KHNCN2.HoKH);
            sqlCmd.Parameters.AddWithValue("@SoCMNDB2", KHNCN2.SoCMND);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}
