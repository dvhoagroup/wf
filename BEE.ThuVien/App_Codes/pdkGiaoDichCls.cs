using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class pdkGiaoDichCls
    {
        public int MaGD;
        public string SoPhieu;
        public DateTime NgayKy;
        public NhanVienCls NhanVien1 = new NhanVienCls();
        public double TyLe1;
        public NhanVienCls NhanVien2 = new NhanVienCls();
        public double TyLe2;
        public KhachHangCls KhachHang1 = new KhachHangCls();
        public KhachHangCls KhachHang2 = new KhachHangCls();
        public string NguoiLienHe;
        public string MoiQuanHe;
        public string DiDong1;
        public string DiDong2;
        public string DTNha;
        public string DTCoQuan;
        public string MaBDS;
        public double DonGia;
        public double TongTien;
        public LoaiTienCls LoaiTien = new LoaiTienCls();
        public bool IsVAT, Share;
        public string DienGiai;
        public byte ThoiHan;
        public double PhiMoiGioi, DienTichXD;
        public string HoSo1;
        public string HoSo2;
        public string HoSo3;
        public string HoSo4;
        public string HoSo5;
        public string HoSo6;
        public string HoSo7;
        public string HoSo8;
        public string HoSo9;
        public string HoSo10;
        public string FileAttach;
        public NhanVienCls NVKT = new NhanVienCls();
        public pdkgdTinhTrangCls TinhTrang = new pdkgdTinhTrangCls();
        public byte PhongKhach;
        public double DTPhongKhach;
        public byte PhongNgu;
        public byte PhongWC;
        public bool PhongDocSach;
        public bool PhongNGV;
        public bool Bep;
        public bool SanTruoc;
        public double DTSanTruoc;
        public bool SanSau;
        public double DTSanSau;
        public double DuongTruoc;
        public double DuongBen;
        public double DuongSau;
        public PhapLyCls PhapLy = new PhapLyCls();
        public string PhapLyKhac;
        public LoaiGiaoDichCls LGD = new LoaiGiaoDichCls();
        public byte SoTang;
        public byte ViTriTang;
        public LoaiBDSCls LoaiBDS = new LoaiBDSCls();
        public string LoaiBDSKhac;
        public double Rong;
        public double Dai;
        public PhuongHuongCls Huong = new PhuongHuongCls();

        public pdkGiaoDichCls()
        {
        }

        public pdkGiaoDichCls(int _MaGD)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdkGiaoDich_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGD", _MaGD);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaGD = int.Parse(dread["MaGD"].ToString());
                SoPhieu = dread["SoPhieu"] as string;
                NgayKy = (DateTime)dread["NgayKy"];
                NhanVien1.MaNV = int.Parse(dread["MaNV1"].ToString());
                TyLe1 = double.Parse(dread["TyLe1"].ToString());
                if (dread["MaNV2"].ToString() != "")
                    NhanVien2.MaNV = int.Parse(dread["MaNV2"].ToString());
                else
                    NhanVien2.MaNV = 0;
                TyLe2 = double.Parse(dread["TyLe2"].ToString());
                KhachHang1.MaKH = int.Parse(dread["MaKH"].ToString());
                if (dread["MaKH2"].ToString() != "")
                    KhachHang2.MaKH = int.Parse(dread["MaKH2"].ToString());
                else
                    KhachHang2.MaKH = 0;
                NguoiLienHe = dread["NguoiLienHe"] as string;
                MoiQuanHe = dread["MoiQuanHe"] as string;
                DiDong1 = dread["DiDong1"] as string;
                DiDong2 = dread["DiDong2"] as string;
                DTNha = dread["DTNha"] as string;
                DTCoQuan = dread["DTCoQuan"] as string;
                MaBDS = dread["MaBDS"] as string;
                DonGia = double.Parse(dread["DonGia"].ToString());
                DienTichXD = double.Parse(dread["DienTichXD"].ToString());
                TongTien = double.Parse(dread["TongTien"].ToString());
                LoaiTien.MaLoaiTien = byte.Parse(dread["MaLoaiTien"].ToString());
                IsVAT = (bool)dread["IsVAT"];
                Share = dread["Share"].ToString() == "" ? false : (bool)dread["Share"];
                DienGiai = dread["DienGiai"] as string;
                ThoiHan = byte.Parse(dread["ThoiHan"].ToString());
                PhiMoiGioi = double.Parse(dread["PhiMoiGioi"].ToString());
                HoSo1 = dread["HoSo1"] as string;
                HoSo2 = dread["HoSo2"] as string;
                HoSo3 = dread["HoSo3"] as string;
                HoSo4 = dread["HoSo4"] as string;
                HoSo5 = dread["HoSo5"] as string;
                HoSo6 = dread["HoSo6"] as string;
                HoSo7 = dread["HoSo7"] as string;
                HoSo8 = dread["HoSo8"] as string;
                HoSo9 = dread["HoSo9"] as string;
                HoSo10 = dread["HoSo10"] as string;
                FileAttach = dread["FileAttach"] as string;
                if (dread["MaNVKT"].ToString() != "")
                    NVKT.MaNV = int.Parse(dread["MaNVKT"].ToString());

                TinhTrang.MaTT = byte.Parse(dread["MaTT"].ToString());
                PhongKhach = byte.Parse(dread["PhongKhach"].ToString());
                DTPhongKhach = double.Parse(dread["DTPhongKhach"].ToString());
                PhongNgu = byte.Parse(dread["PhongNgu"].ToString());
                PhongWC = byte.Parse(dread["PhongWC"].ToString());
                PhongDocSach = (bool)dread["PhongDocSach"];
                PhongNGV = (bool)dread["PhongNGV"];
                Bep = (bool)dread["Bep"];
                SanTruoc = (bool)dread["SanTruoc"];
                DTSanTruoc = double.Parse(dread["DTSanTruoc"].ToString());
                SanSau = (bool)dread["SanSau"];
                DTSanSau = double.Parse(dread["DTSanSau"].ToString());
                DuongTruoc = double.Parse(dread["DuongTruoc"].ToString());
                DuongBen = double.Parse(dread["DuongBen"].ToString());
                DuongSau = double.Parse(dread["DuongSau"].ToString());
                PhapLy.MaPL = byte.Parse(dread["MaPL"].ToString());
                PhapLyKhac = dread["PhapLyKhac"] as string;
                LGD.MaLDG = byte.Parse(dread["MaLGD"].ToString());
                SoTang = byte.Parse(dread["SoTang"].ToString());
                ViTriTang = byte.Parse(dread["ViTriTang"].ToString());
                LoaiBDS.MaLBDS = byte.Parse(dread["LoaiBDS"].ToString());
                LoaiBDSKhac = dread["LoaiBDSKhac"] as string;
                Rong = double.Parse(dread["Rong"].ToString());
                Dai = double.Parse(dread["Dai"].ToString());
                Huong.MaPhuongHuong = byte.Parse(dread["MaHuong"].ToString());
            }
            sqlCon.Close();
        }

        public int Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdkGiaoDich_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGD", MaGD).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@MaNV1", NhanVien1.MaNV);
            sqlCmd.Parameters.AddWithValue("@TyLe1", TyLe1);
            if (NhanVien2.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNV2", NhanVien2.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNV2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@TyLe2", TyLe2);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang1.MaKH);
            if (KhachHang2.MaKH != 0)
                sqlCmd.Parameters.AddWithValue("@MaKH2", KhachHang2.MaKH);
            else
                sqlCmd.Parameters.AddWithValue("@MaKH2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NguoiLienHe", NguoiLienHe);
            sqlCmd.Parameters.AddWithValue("@MoiQuanHe", MoiQuanHe);
            sqlCmd.Parameters.AddWithValue("@DiDong1", DiDong1);
            sqlCmd.Parameters.AddWithValue("@DiDong2", DiDong2);
            sqlCmd.Parameters.AddWithValue("@DTNha", DTNha);
            sqlCmd.Parameters.AddWithValue("@DTCoQuan", DTCoQuan);
            if (MaBDS != "")
                sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            else
                sqlCmd.Parameters.AddWithValue("@MaBDS", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DonGia", DonGia);
            sqlCmd.Parameters.AddWithValue("@TongTien", TongTien);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@IsVAT", IsVAT);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@ThoiHan", ThoiHan);
            sqlCmd.Parameters.AddWithValue("@PhiMoiGioi", PhiMoiGioi);
            sqlCmd.Parameters.AddWithValue("@HoSo1", HoSo1);
            sqlCmd.Parameters.AddWithValue("@HoSo2", HoSo2);
            sqlCmd.Parameters.AddWithValue("@HoSo3", HoSo3);
            sqlCmd.Parameters.AddWithValue("@HoSo4", HoSo4);
            sqlCmd.Parameters.AddWithValue("@HoSo5", HoSo5);
            sqlCmd.Parameters.AddWithValue("@HoSo6", HoSo6);
            sqlCmd.Parameters.AddWithValue("@HoSo7", HoSo7);
            sqlCmd.Parameters.AddWithValue("@HoSo8", HoSo8);
            sqlCmd.Parameters.AddWithValue("@HoSo9", HoSo9);
            sqlCmd.Parameters.AddWithValue("@HoSo10", HoSo10);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            if (NVKT.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVKT", NVKT.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVKT", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@Share", Share);
            sqlCmd.Parameters.AddWithValue("@PhongKhach", PhongKhach);
            sqlCmd.Parameters.AddWithValue("@DTPhongKhach", DTPhongKhach);
            sqlCmd.Parameters.AddWithValue("@PhongNgu", PhongNgu);
            sqlCmd.Parameters.AddWithValue("@PhongWC", PhongWC);
            sqlCmd.Parameters.AddWithValue("@PhongDocSach", PhongDocSach);
            sqlCmd.Parameters.AddWithValue("@PhongNGV", PhongNGV);
            sqlCmd.Parameters.AddWithValue("@Bep", Bep);
            sqlCmd.Parameters.AddWithValue("@SanTruoc", SanTruoc);
            sqlCmd.Parameters.AddWithValue("@DTSanTruoc", DTSanTruoc);
            sqlCmd.Parameters.AddWithValue("@SanSau", SanSau);
            sqlCmd.Parameters.AddWithValue("@DTSanSau", DTSanSau);
            sqlCmd.Parameters.AddWithValue("@DuongTruoc", DuongTruoc);
            sqlCmd.Parameters.AddWithValue("@DuongBen", DuongBen);
            sqlCmd.Parameters.AddWithValue("@DuongSau", DuongSau);
            sqlCmd.Parameters.AddWithValue("@MaPL", PhapLy.MaPL);
            sqlCmd.Parameters.AddWithValue("@PhapLyKhac", PhapLyKhac);
            sqlCmd.Parameters.AddWithValue("@MaLGD", LGD.MaLDG);
            sqlCmd.Parameters.AddWithValue("@SoTang", SoTang);
            sqlCmd.Parameters.AddWithValue("@ViTriTang", ViTriTang);
            sqlCmd.Parameters.AddWithValue("@LoaiBDS", LoaiBDS.MaLBDS);
            sqlCmd.Parameters.AddWithValue("@LoaiBDSKhac", LoaiBDSKhac);
            sqlCmd.Parameters.AddWithValue("@Rong", Rong);
            sqlCmd.Parameters.AddWithValue("@Dai", Dai);
            sqlCmd.Parameters.AddWithValue("@MaHuong", Huong.MaPhuongHuong);
            sqlCmd.Parameters.AddWithValue("@DienTichXD", DienTichXD);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaGD"].Value.ToString());
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdkGiaoDich_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGD", MaGD);
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@MaNV1", NhanVien1.MaNV);
            sqlCmd.Parameters.AddWithValue("@TyLe1", TyLe1);
            if (NhanVien2.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNV2", NhanVien2.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNV2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@TyLe2", TyLe2);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang1.MaKH);
            if (KhachHang2.MaKH != 0)
                sqlCmd.Parameters.AddWithValue("@MaKH2", KhachHang2.MaKH);
            else
                sqlCmd.Parameters.AddWithValue("@MaKH2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NguoiLienHe", NguoiLienHe);
            sqlCmd.Parameters.AddWithValue("@MoiQuanHe", MoiQuanHe);
            sqlCmd.Parameters.AddWithValue("@DiDong1", DiDong1);
            sqlCmd.Parameters.AddWithValue("@DiDong2", DiDong2);
            sqlCmd.Parameters.AddWithValue("@DTNha", DTNha);
            sqlCmd.Parameters.AddWithValue("@DTCoQuan", DTCoQuan);
            if (MaBDS != "")
                sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            else
                sqlCmd.Parameters.AddWithValue("@MaBDS", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DonGia", DonGia);
            sqlCmd.Parameters.AddWithValue("@TongTien", TongTien);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@IsVAT", IsVAT);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@ThoiHan", ThoiHan);
            sqlCmd.Parameters.AddWithValue("@PhiMoiGioi", PhiMoiGioi);
            sqlCmd.Parameters.AddWithValue("@HoSo1", HoSo1);
            sqlCmd.Parameters.AddWithValue("@HoSo2", HoSo2);
            sqlCmd.Parameters.AddWithValue("@HoSo3", HoSo3);
            sqlCmd.Parameters.AddWithValue("@HoSo4", HoSo4);
            sqlCmd.Parameters.AddWithValue("@HoSo5", HoSo5);
            sqlCmd.Parameters.AddWithValue("@HoSo6", HoSo6);
            sqlCmd.Parameters.AddWithValue("@HoSo7", HoSo7);
            sqlCmd.Parameters.AddWithValue("@HoSo8", HoSo8);
            sqlCmd.Parameters.AddWithValue("@HoSo9", HoSo9);
            sqlCmd.Parameters.AddWithValue("@HoSo10", HoSo10);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@Share", Share);
            sqlCmd.Parameters.AddWithValue("@PhongKhach", PhongKhach);
            sqlCmd.Parameters.AddWithValue("@DTPhongKhach", DTPhongKhach);
            sqlCmd.Parameters.AddWithValue("@PhongNgu", PhongNgu);
            sqlCmd.Parameters.AddWithValue("@PhongWC", PhongWC);
            sqlCmd.Parameters.AddWithValue("@PhongDocSach", PhongDocSach);
            sqlCmd.Parameters.AddWithValue("@PhongNGV", PhongNGV);
            sqlCmd.Parameters.AddWithValue("@Bep", Bep);
            sqlCmd.Parameters.AddWithValue("@SanTruoc", SanTruoc);
            sqlCmd.Parameters.AddWithValue("@DTSanTruoc", DTSanTruoc);
            sqlCmd.Parameters.AddWithValue("@SanSau", SanSau);
            sqlCmd.Parameters.AddWithValue("@DTSanSau", DTSanSau);
            sqlCmd.Parameters.AddWithValue("@DuongTruoc", DuongTruoc);
            sqlCmd.Parameters.AddWithValue("@DuongBen", DuongBen);
            sqlCmd.Parameters.AddWithValue("@DuongSau", DuongSau);
            sqlCmd.Parameters.AddWithValue("@MaPL", PhapLy.MaPL);
            sqlCmd.Parameters.AddWithValue("@PhapLyKhac", PhapLyKhac);
            sqlCmd.Parameters.AddWithValue("@MaLGD", LGD.MaLDG);
            sqlCmd.Parameters.AddWithValue("@SoTang", SoTang);
            sqlCmd.Parameters.AddWithValue("@ViTriTang", ViTriTang);
            sqlCmd.Parameters.AddWithValue("@LoaiBDS", LoaiBDS.MaLBDS);
            sqlCmd.Parameters.AddWithValue("@LoaiBDSKhac", LoaiBDSKhac);
            sqlCmd.Parameters.AddWithValue("@Rong", Rong);
            sqlCmd.Parameters.AddWithValue("@Dai", Dai);
            sqlCmd.Parameters.AddWithValue("@MaHuong", Huong.MaPhuongHuong);
            sqlCmd.Parameters.AddWithValue("@DienTichXD", DienTichXD);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdkGiaoDich_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(DateTime _TuNgay, DateTime _DenNgay)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdkGiaoDich_getDate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien1.MaNV);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByBDS()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdkGiaoDich_getByMaBDS " + MaGD, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByKhachHang()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KhachHang_getByMaGD " + MaGD + "," + NhanVien1.MaNV, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdkGiaoDich_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGD", MaGD);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdkGiaoDich_TaoSoPhieu", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoPhieu", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoPhieu"].Value.ToString();
        }

        public void GetShare()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdkGiaoDich_getShare", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGD", MaGD);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                if (dread["MaNV1"].ToString() != "")
                    NhanVien1.MaNV = int.Parse(dread["MaNV1"].ToString());
                else
                    NhanVien2.MaNV = 0;
                if (dread["MaNV2"].ToString() != "")
                    NhanVien2.MaNV = int.Parse(dread["MaNV2"].ToString());
                else
                    NhanVien2.MaNV = 0;
                Share = dread["Share"].ToString() == "" ? false : (bool)dread["Share"];
                KhachHang1.MaKH = int.Parse(dread["MaKH"].ToString());
                if (dread["MaKH2"].ToString() != "")
                    KhachHang2.MaKH = int.Parse(dread["MaKH2"].ToString());
                else
                    KhachHang2.MaKH = 0;
            }
            sqlCon.Close();
        }
    }
}