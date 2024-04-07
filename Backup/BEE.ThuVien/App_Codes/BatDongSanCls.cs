using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class BatDongSanCls
    {
        public string MaBDS;
        public string TenBDS;
        public string MaSo, LoaiCH, GhiChu, LoaiBDSKhac, PhapLyKhac;
        public DonViTinhCls DonViTinh = new DonViTinhCls();
        public TangNhaCls TangNha = new TangNhaCls();
        public LoaiTienCls LoaiTien = new LoaiTienCls();
        public PhuongHuongCls PhuongHuong = new PhuongHuongCls();
        public TinhTrangBDSCls TinhTrang = new TinhTrangBDSCls();
        public DuAnCls DuAn = new DuAnCls();
        public LoaiBDSCls LoaiBDS = new LoaiBDSCls();
        public double DienTichChung, DienTichKV, DienTichXD, DienTichHB;
        public KhachHangCls KhachHang = new KhachHangCls();
        public KhachHangCls KhachHang2 = new KhachHangCls();
        public DaiLyCls DaiLy = new DaiLyCls();
        public double GiaGoc;
        public double GiaBan;
        public double TienSDDat, VAT, Dai, Rong, DTPhongKhach, DTSanTruoc, DTSanSau, DuongTruoc, DuongBen, DuongSau;
        public int NamADDonGia;
        public short NamXD;
        public DateTime NgayDang;
        public NhanVienCls NhanVien = new NhanVienCls();
        public byte PhongNgu, PhongKhach, Toilet, PhongTam, TangCao, ViTriTang;
        public LoCls Lo = new LoCls();
        public LongDuongCls Duong = new LongDuongCls();
        public ViTriCls ViTri = new ViTriCls();
        public LoaiGiaoDichCls LoaiGD = new LoaiGiaoDichCls();
        public bool PhongDocSach, PhongNGV, Bep, SanTruoc, SanSau;
        public PhapLyCls PhapLy = new PhapLyCls();
        public KhuCls Khu = new KhuCls();

        public BatDongSanCls()
        {
        }

        public BatDongSanCls(string _MaBDS)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", _MaBDS);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaBDS = dread["MaBDS"] as string;
                TenBDS = dread["TenBDS"] as string;
                MaSo = dread["MaSo"] as string;
                ViTri.MaViTri = dread["ViTri"].ToString() == "" ? (byte)0: byte.Parse(dread["ViTri"].ToString());
                DonViTinh.MaDVT = dread["MaDVT"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaDVT"].ToString());
                TangNha.MaTangNha = dread["MaTangNha"].ToString() == "" ? 0 : int.Parse(dread["MaTangNha"].ToString());
                LoaiTien.MaLoaiTien = byte.Parse(dread["MaLoaiTien"].ToString());
                PhuongHuong.MaPhuongHuong = dread["MaPhuongHuong"].ToString() == "" ?(byte)0 : byte.Parse(dread["MaPhuongHuong"].ToString());
                TinhTrang.MaTT = byte.Parse(dread["MaTT"].ToString());
                DuAn.MaDA = dread["MaDA"].ToString() == "" ? 0 : int.Parse(dread["MaDA"].ToString());
                if (dread["LoaiBDS"] != DBNull.Value)
                    LoaiBDS.MaLBDS = byte.Parse(dread["LoaiBDS"].ToString());
                DienTichChung = dread["DienTichChung"].ToString() == "" ? 0 : double.Parse(dread["DienTichChung"].ToString());
                DienTichHB = dread["DienTichHB"].ToString() == "" ? 0 : double.Parse(dread["DienTichHB"].ToString());
                DienTichKV = dread["DienTichKV"].ToString() == "" ? 0 : double.Parse(dread["DienTichKV"].ToString());
                DienTichXD = dread["DienTichXD"].ToString() == "" ? 0 : double.Parse(dread["DienTichXD"].ToString());
                KhachHang.MaKH = dread["MaKH"].ToString() == "" ? 0 : int.Parse(dread["MaKH"].ToString());
                DaiLy.MaDL = dread["MaDL"].ToString() == "" ? 0 : int.Parse(dread["MaDL"].ToString());
                GiaGoc = double.Parse(dread["GiaGoc"].ToString());
                GiaBan = double.Parse(dread["GiaBan"].ToString());
                TienSDDat = dread["TienSDDat"].ToString() == "" ? 0 : double.Parse(dread["TienSDDat"].ToString());
                VAT = dread["VAT"].ToString() == "" ? 0 : double.Parse(dread["VAT"].ToString());
                if (dread["NamADDonGia"] != DBNull.Value)
                    NamADDonGia = int.Parse(dread["NamADDonGia"].ToString());
                if (dread["NamXD"] != DBNull.Value)
                    NamXD = short.Parse(dread["NamXD"].ToString());
                NgayDang = (DateTime)dread["NgayDang"];
                NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());
                if (dread["LoaiCH"] != DBNull.Value)
                    LoaiCH = dread["LoaiCH"] as string;
                GhiChu = dread["GhiChu"] as string;
                PhongKhach = dread["PhongKhach"].ToString() == "" ? (byte)0 : byte.Parse(dread["PhongKhach"].ToString());
                PhongNgu = dread["PhongNgu"].ToString() == "" ? (byte)0 : byte.Parse(dread["PhongNgu"].ToString());
                Toilet = dread["Toilet"].ToString() == "" ? (byte)0 : byte.Parse(dread["Toilet"].ToString());
                PhongTam = dread["PhongTam"].ToString() == "" ? (byte)0 : byte.Parse(dread["PhongTam"].ToString());
                TangCao = dread["TangCao"].ToString() == "" ? (byte)0 : byte.Parse(dread["TangCao"].ToString());
                Dai = dread["Dai"].ToString() == "" ? 0 : double.Parse(dread["Dai"].ToString());
                Rong = dread["Rong"].ToString() == "" ? 0 : double.Parse(dread["Rong"].ToString());
                Lo.MaLo = dread["MaLo"].ToString() == "" ? 0 : int.Parse(dread["MaLo"].ToString());
                Lo.Blocks.PhanKhu.Khu.MaKhu = dread["MaKhu"].ToString() == "" ? 0 : int.Parse(dread["MaKhu"].ToString());
                Lo.Blocks.PhanKhu.MaPK = dread["MaPK"].ToString() == "" ? 0 : int.Parse(dread["MaPK"].ToString());
                Lo.Blocks.BlockID = dread["BlockID"].ToString() == "" ? 0 : int.Parse(dread["BlockID"].ToString());
                Duong.MaLD = dread["MaLD"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaLD"].ToString());
                PhongDocSach = dread["PhongDocSach"].ToString() == "" ? false : (bool)dread["PhongDocSach"];
                PhongNGV = dread["PhongNGV"].ToString() == "" ? false : (bool)dread["PhongNGV"];
                Bep = dread["Bep"].ToString() == "" ? false : (bool)dread["Bep"];
                SanTruoc = dread["SanTruoc"].ToString() == "" ? false : (bool)dread["SanTruoc"];
                DTSanTruoc = dread["DTSanTruoc"].ToString() == "" ? 0 : double.Parse(dread["DTSanTruoc"].ToString());
                SanSau = dread["SanSau"].ToString() == "" ? false : (bool)dread["SanSau"];
                DTSanSau = dread["DTSanSau"].ToString() == "" ? 0 : double.Parse(dread["DTSanSau"].ToString());
                DuongTruoc = dread["DuongTruoc"].ToString() == "" ? 0 : double.Parse(dread["DuongTruoc"].ToString());
                DuongBen = dread["DuongBen"].ToString() == "" ? 0 : double.Parse(dread["DuongBen"].ToString());
                DuongSau = dread["DuongSau"].ToString() == "" ? 0 : double.Parse(dread["DuongSau"].ToString());
                PhapLy.MaPL = dread["MaPL"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaPL"].ToString());
                ViTriTang = dread["ViTriTang"].ToString() == "" ? (byte)0 : byte.Parse(dread["ViTriTang"].ToString());
                PhapLyKhac = dread["PhapLyKhac"] as string;
                DTPhongKhach = dread["DTPhongKhach"].ToString() == "" ? 0 : double.Parse(dread["DTPhongKhach"].ToString());
                LoaiGD.MaLDG = dread["MaLGD"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaLGD"].ToString());
                LoaiBDSKhac = dread["LoaiBDSKhac"] as string;
                Khu.MaKhu = dread["MaKhu"].ToString() == "" ? 0 : int.Parse(dread["MaKhu"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenBDS", TenBDS);
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.AddWithValue("@MaDVT", DonViTinh.MaDVT);
            sqlCmd.Parameters.AddWithValue("@MaTangNha", TangNha.MaTangNha);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@MaPhuongHuong", PhuongHuong.MaPhuongHuong);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            sqlCmd.Parameters.AddWithValue("@LoaiBDS", LoaiBDS.MaLBDS);
            sqlCmd.Parameters.AddWithValue("@DienTichChung", DienTichChung);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            sqlCmd.Parameters.AddWithValue("@GiaGoc", GiaGoc);
            sqlCmd.Parameters.AddWithValue("@GiaBan", GiaBan);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
            sqlCmd.Parameters.AddWithValue("@NamADDonGia", NamADDonGia);
            sqlCmd.Parameters.AddWithValue("@NamXD", NamXD);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@ViTri", ViTri.MaViTri);
            sqlCmd.Parameters.AddWithValue("@LoaiCH", LoaiCH);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public string InsertTransaction()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_addTransaction", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaBDS", SqlDbType.NChar, 12).Direction = ParameterDirection.Output;            
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            if (KhachHang2.MaKH != 0)
                sqlCmd.Parameters.AddWithValue("@MaKH2", KhachHang2.MaKH);
            else
                sqlCmd.Parameters.AddWithValue("@MaKH2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DonGia", GiaBan);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@MaLGD", LoaiGD.MaLDG);
            sqlCmd.Parameters.AddWithValue("@LoaiBDS", LoaiBDS.MaLBDS);
            sqlCmd.Parameters.AddWithValue("@LoaiBDSKhac", LoaiBDSKhac);
            sqlCmd.Parameters.AddWithValue("@Dai", Dai);
            sqlCmd.Parameters.AddWithValue("@Rong", Rong);
            sqlCmd.Parameters.AddWithValue("@DienTichXD", DienTichXD);
            sqlCmd.Parameters.AddWithValue("@TangCao", TangCao);
            sqlCmd.Parameters.AddWithValue("@ViTriTang", ViTriTang);
            sqlCmd.Parameters.AddWithValue("@PhongNgu", PhongNgu);
            sqlCmd.Parameters.AddWithValue("@PhongKhach", PhongKhach);
            sqlCmd.Parameters.AddWithValue("@DTPhongKhach", DTPhongKhach);
            sqlCmd.Parameters.AddWithValue("@Toilet", Toilet);
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
            sqlCmd.Parameters.AddWithValue("@MaHuong", PhuongHuong.MaPhuongHuong);
            sqlCmd.Parameters.AddWithValue("@MaPL", PhapLy.MaPL);
            sqlCmd.Parameters.AddWithValue("@PhapLyKhac", PhapLyKhac);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@MaBDS"].Value.ToString();
        }

        public void Insert2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_add2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenBDS", TenBDS);
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.AddWithValue("@MaDVT", DonViTinh.MaDVT);
            if (TangNha.MaTangNha != 0)
                sqlCmd.Parameters.AddWithValue("@MaTangNha", TangNha.MaTangNha);
            else
                sqlCmd.Parameters.AddWithValue("@MaTangNha", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@MaPhuongHuong", PhuongHuong.MaPhuongHuong);
            //sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            sqlCmd.Parameters.AddWithValue("@LoaiBDS", LoaiBDS.MaLBDS);
            sqlCmd.Parameters.AddWithValue("@DienTichChung", DienTichChung);
            //sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            //sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            sqlCmd.Parameters.AddWithValue("@GiaGoc", GiaGoc);
            sqlCmd.Parameters.AddWithValue("@GiaBan", GiaBan);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
            sqlCmd.Parameters.AddWithValue("@NamADDonGia", NamADDonGia);
            sqlCmd.Parameters.AddWithValue("@NamXD", NamXD);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@ViTri", ViTri.MaViTri);
            sqlCmd.Parameters.AddWithValue("@LoaiCH", LoaiCH);
            sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            sqlCmd.Parameters.AddWithValue("@PhongKhach", PhongKhach);
            sqlCmd.Parameters.AddWithValue("@PhongNgu", PhongNgu);
            sqlCmd.Parameters.AddWithValue("@Toilet", Toilet);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void InsertGround()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_addGround", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenBDS", TenBDS);
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.AddWithValue("@MaDVT", DonViTinh.MaDVT);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            //if (PhuongHuong.MaPhuongHuong != 0)
            sqlCmd.Parameters.AddWithValue("@MaPhuongHuong", PhuongHuong.MaPhuongHuong);
            //else
            //    sqlCmd.Parameters.AddWithValue("@MaPhuongHuong", DBNull.Value);
            //sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            sqlCmd.Parameters.AddWithValue("@LoaiBDS", LoaiBDS.MaLBDS);
            sqlCmd.Parameters.AddWithValue("@DienTichChung", DienTichChung);
            //sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            //sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            sqlCmd.Parameters.AddWithValue("@GiaGoc", GiaGoc);
            sqlCmd.Parameters.AddWithValue("@GiaBan", GiaBan);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
            sqlCmd.Parameters.AddWithValue("@NamADDonGia", NamADDonGia);
            sqlCmd.Parameters.AddWithValue("@NamXD", NamXD);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@ViTri", ViTri.MaViTri);
            sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            sqlCmd.Parameters.AddWithValue("@MaLo", Lo.MaLo);
            sqlCmd.Parameters.AddWithValue("@Rong", Rong);
            sqlCmd.Parameters.AddWithValue("@Dai", Dai);
            sqlCmd.Parameters.AddWithValue("@MaLD", Duong.MaLD);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void InsertVila()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_addVila", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenBDS", TenBDS);
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.AddWithValue("@MaDVT", DonViTinh.MaDVT);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            if (PhuongHuong.MaPhuongHuong != 0)
                sqlCmd.Parameters.AddWithValue("@MaPhuongHuong", PhuongHuong.MaPhuongHuong);
            else
                sqlCmd.Parameters.AddWithValue("@MaPhuongHuong", DBNull.Value);
            //sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            sqlCmd.Parameters.AddWithValue("@LoaiBDS", LoaiBDS.MaLBDS);
            sqlCmd.Parameters.AddWithValue("@DienTichChung", DienTichChung);
            //sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            //sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            sqlCmd.Parameters.AddWithValue("@GiaGoc", GiaGoc);
            sqlCmd.Parameters.AddWithValue("@GiaBan", GiaBan);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
            sqlCmd.Parameters.AddWithValue("@NamADDonGia", NamADDonGia);
            sqlCmd.Parameters.AddWithValue("@NamXD", NamXD);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@ViTri", ViTri.MaViTri);
            sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            sqlCmd.Parameters.AddWithValue("@MaLo", Lo.MaLo);
            sqlCmd.Parameters.AddWithValue("@Rong", Rong);
            sqlCmd.Parameters.AddWithValue("@Dai", Dai);
            sqlCmd.Parameters.AddWithValue("@TangCao", TangCao);
            sqlCmd.Parameters.AddWithValue("@DienTichHB", DienTichHB);
            sqlCmd.Parameters.AddWithValue("@DienTichKV", DienTichKV);
            sqlCmd.Parameters.AddWithValue("@DienTichXD", DienTichXD);
            sqlCmd.Parameters.AddWithValue("@PhongKhach", PhongKhach);
            sqlCmd.Parameters.AddWithValue("@PhongNgu", PhongNgu);
            sqlCmd.Parameters.AddWithValue("@PhongTam", PhongTam);
            sqlCmd.Parameters.AddWithValue("@Toilet", Toilet);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void InsertNear()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_addNear", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenBDS", TenBDS);
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.AddWithValue("@MaDVT", DonViTinh.MaDVT);
            if (TangNha.MaTangNha != 0)
                sqlCmd.Parameters.AddWithValue("@MaTangNha", TangNha.MaTangNha);
            else
                sqlCmd.Parameters.AddWithValue("@MaTangNha", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@MaPhuongHuong", PhuongHuong.MaPhuongHuong);
            sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            sqlCmd.Parameters.AddWithValue("@LoaiBDS", LoaiBDS.MaLBDS);
            sqlCmd.Parameters.AddWithValue("@DienTichChung", DienTichChung);
            sqlCmd.Parameters.AddWithValue("@DienTichXD", DienTichXD);
            sqlCmd.Parameters.AddWithValue("@GiaGoc", GiaGoc);
            sqlCmd.Parameters.AddWithValue("@GiaBan", GiaBan);
            sqlCmd.Parameters.AddWithValue("@NamADDonGia", NamADDonGia);
            sqlCmd.Parameters.AddWithValue("@NamXD", NamXD);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@ViTri", ViTri.MaViTri);
            sqlCmd.Parameters.AddWithValue("@LoaiCH", LoaiCH);
            sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            sqlCmd.Parameters.AddWithValue("@PhongKhach", PhongKhach);
            sqlCmd.Parameters.AddWithValue("@PhongNgu", PhongNgu);
            sqlCmd.Parameters.AddWithValue("@Toilet", Toilet);
            sqlCmd.Parameters.AddWithValue("@ViTriTang", ViTriTang);
            sqlCmd.Parameters.AddWithValue("@MaKhu", Khu.MaKhu);
            sqlCmd.Parameters.AddWithValue("@Rong", Rong);
            sqlCmd.Parameters.AddWithValue("@Dai", Dai);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@TenBDS", TenBDS);
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.AddWithValue("@MaDVT", DonViTinh.MaDVT);
            sqlCmd.Parameters.AddWithValue("@MaTangNha", TangNha.MaTangNha);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@MaPhuongHuong", PhuongHuong.MaPhuongHuong);
            //sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            sqlCmd.Parameters.AddWithValue("@LoaiBDS", LoaiBDS.MaLBDS);
            sqlCmd.Parameters.AddWithValue("@DienTichChung", DienTichChung);
            //sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            //sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            sqlCmd.Parameters.AddWithValue("@GiaGoc", GiaGoc);
            sqlCmd.Parameters.AddWithValue("@GiaBan", GiaBan);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
            sqlCmd.Parameters.AddWithValue("@NamADDonGia", NamADDonGia);
            sqlCmd.Parameters.AddWithValue("@NamXD", NamXD);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@ViTri", ViTri.MaViTri);
            sqlCmd.Parameters.AddWithValue("@LoaiCH", LoaiCH);
            sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            sqlCmd.Parameters.AddWithValue("@PhongKhach", PhongKhach);
            sqlCmd.Parameters.AddWithValue("@PhongNgu", PhongNgu);
            sqlCmd.Parameters.AddWithValue("@Toilet", Toilet);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateGround()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_updateGround", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@TenBDS", TenBDS);
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.AddWithValue("@MaDVT", DonViTinh.MaDVT);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@MaPhuongHuong", PhuongHuong.MaPhuongHuong);
            //sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            sqlCmd.Parameters.AddWithValue("@LoaiBDS", LoaiBDS.MaLBDS);
            sqlCmd.Parameters.AddWithValue("@DienTichChung", DienTichChung);
            //sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            //sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            sqlCmd.Parameters.AddWithValue("@GiaGoc", GiaGoc);
            sqlCmd.Parameters.AddWithValue("@GiaBan", GiaBan);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
            sqlCmd.Parameters.AddWithValue("@NamADDonGia", NamADDonGia);
            sqlCmd.Parameters.AddWithValue("@NamXD", NamXD);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@ViTri", ViTri.MaViTri);
            sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            sqlCmd.Parameters.AddWithValue("@MaLo", Lo.MaLo);
            sqlCmd.Parameters.AddWithValue("@Rong", Rong);
            sqlCmd.Parameters.AddWithValue("@Dai", Dai);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateVila()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_updateVila", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@TenBDS", TenBDS);
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.AddWithValue("@MaDVT", DonViTinh.MaDVT);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            if (PhuongHuong.MaPhuongHuong != 0)
                sqlCmd.Parameters.AddWithValue("@MaPhuongHuong", PhuongHuong.MaPhuongHuong);
            else
                sqlCmd.Parameters.AddWithValue("@MaPhuongHuong", DBNull.Value);
            //sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            sqlCmd.Parameters.AddWithValue("@LoaiBDS", LoaiBDS.MaLBDS);
            sqlCmd.Parameters.AddWithValue("@DienTichChung", DienTichChung);
            //sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            //sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            sqlCmd.Parameters.AddWithValue("@GiaGoc", GiaGoc);
            sqlCmd.Parameters.AddWithValue("@GiaBan", GiaBan);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
            sqlCmd.Parameters.AddWithValue("@NamADDonGia", NamADDonGia);
            sqlCmd.Parameters.AddWithValue("@NamXD", NamXD);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@ViTri", ViTri.MaViTri);
            sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            sqlCmd.Parameters.AddWithValue("@MaLo", Lo.MaLo);
            sqlCmd.Parameters.AddWithValue("@Rong", Rong);
            sqlCmd.Parameters.AddWithValue("@Dai", Dai);
            sqlCmd.Parameters.AddWithValue("@TangCao", TangCao);
            sqlCmd.Parameters.AddWithValue("@DienTichHB", DienTichHB);
            sqlCmd.Parameters.AddWithValue("@DienTichKV", DienTichKV);
            sqlCmd.Parameters.AddWithValue("@DienTichXD", DienTichXD);
            sqlCmd.Parameters.AddWithValue("@PhongKhach", PhongKhach);
            sqlCmd.Parameters.AddWithValue("@PhongNgu", PhongNgu);
            sqlCmd.Parameters.AddWithValue("@PhongTam", PhongTam);
            sqlCmd.Parameters.AddWithValue("@Toilet", Toilet);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateTransaction()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_updateTransaction", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            if (KhachHang2.MaKH != 0)
                sqlCmd.Parameters.AddWithValue("@MaKH2", KhachHang2.MaKH);
            else
                sqlCmd.Parameters.AddWithValue("@MaKH2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DonGia", GiaBan);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@MaLGD", LoaiGD.MaLDG);
            sqlCmd.Parameters.AddWithValue("@LoaiBDS", LoaiBDS.MaLBDS);
            sqlCmd.Parameters.AddWithValue("@LoaiBDSKhac", LoaiBDSKhac);
            sqlCmd.Parameters.AddWithValue("@Dai", Dai);
            sqlCmd.Parameters.AddWithValue("@Rong", Rong);
            sqlCmd.Parameters.AddWithValue("@DienTichXD", DienTichXD);
            sqlCmd.Parameters.AddWithValue("@TangCao", TangCao);
            sqlCmd.Parameters.AddWithValue("@ViTriTang", ViTriTang);
            sqlCmd.Parameters.AddWithValue("@PhongNgu", PhongNgu);
            sqlCmd.Parameters.AddWithValue("@PhongKhach", PhongKhach);
            sqlCmd.Parameters.AddWithValue("@DTPhongKhach", DTPhongKhach);
            sqlCmd.Parameters.AddWithValue("@Toilet", Toilet);
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
            sqlCmd.Parameters.AddWithValue("@MaHuong", PhuongHuong.MaPhuongHuong);
            sqlCmd.Parameters.AddWithValue("@MaPL", PhapLy.MaPL);
            sqlCmd.Parameters.AddWithValue("@PhapLyKhac", PhapLyKhac);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateNear()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_updateNear", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@TenBDS", TenBDS);
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.AddWithValue("@MaDVT", DonViTinh.MaDVT);
            sqlCmd.Parameters.AddWithValue("@MaTangNha", TangNha.MaTangNha);
            sqlCmd.Parameters.AddWithValue("@MaLoaiTien", LoaiTien.MaLoaiTien);
            sqlCmd.Parameters.AddWithValue("@MaPhuongHuong", PhuongHuong.MaPhuongHuong);
            sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            sqlCmd.Parameters.AddWithValue("@LoaiBDS", LoaiBDS.MaLBDS);
            sqlCmd.Parameters.AddWithValue("@DienTichChung", DienTichChung);
            sqlCmd.Parameters.AddWithValue("@DienTichXD", DienTichXD);
            sqlCmd.Parameters.AddWithValue("@GiaGoc", GiaGoc);
            sqlCmd.Parameters.AddWithValue("@GiaBan", GiaBan);
            sqlCmd.Parameters.AddWithValue("@NamADDonGia", NamADDonGia);
            sqlCmd.Parameters.AddWithValue("@NamXD", NamXD);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@ViTri", ViTri.MaViTri);
            sqlCmd.Parameters.AddWithValue("@LoaiCH", LoaiCH);
            sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            sqlCmd.Parameters.AddWithValue("@PhongKhach", PhongKhach);
            sqlCmd.Parameters.AddWithValue("@PhongNgu", PhongNgu);
            sqlCmd.Parameters.AddWithValue("@Toilet", Toilet);
            sqlCmd.Parameters.AddWithValue("@ViTriTang", ViTriTang);
            sqlCmd.Parameters.AddWithValue("@MaKhu", Khu.MaKhu);
            sqlCmd.Parameters.AddWithValue("@Rong", Rong);
            sqlCmd.Parameters.AddWithValue("@Dai", Dai);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("BatDongSan_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectPhongToa()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("bdsPhongToa_getByMaBDS '" + MaBDS + "'", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable GiaoDich(string _MaBDS, int _MaKH)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("BatDongSan_LichSuGiaoDich '" + _MaBDS + "'," + _MaKH, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable LichThanhToan()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("BatDongSan_LichThanhToan '" + MaBDS + "'", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShow(int _MaDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("BatDongSan_getAllShow " + _MaDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShow(string _MaDA, string _BlockID, byte _MaLoaiDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("BatDongSan_getAllShow '" + _MaDA + "','" + _BlockID + "'," + _MaLoaiDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShow(string _MaDA, string _BlockID, int _MaNV, byte _MaLoaiDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("BatDongSan_getAllShowByStaff '" + _MaDA + "','" + _BlockID + "'," + _MaNV + "," + _MaLoaiDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShow(string _MaDA, string _BlockID, byte _MaPB, byte _MaLoaiDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("BatDongSan_getAllShowByDepartment '" + _MaDA + "','" + _BlockID + "'," + _MaPB + "," + _MaLoaiDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShow(string _MaDA, string _BlockID, byte _MaNKD, string _Group, byte _MaLoaiDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("BatDongSan_getAllShowByGroup '" + _MaDA + "','" + _BlockID + "'," + _MaNKD + "," + _MaLoaiDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShowNotOpen(string _MaDA, string _BlockID, byte _MaLoaiDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("BatDongSan_NotOpenGetByMaDA_Block '" + _MaDA + "','" + _BlockID + "'," + _MaLoaiDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(DateTime _TuNgay, DateTime _DenNgay)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BDS_Broker_getDate", sqlCon);
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

        public DataTable SelectByMaDA(string _MaDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("BatDongSan_getByMaDA '" + _MaDA + "'", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByMaDABlockID(string _MaDA, int _BlockID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("BatDongSan_getByMaDA_Block '" + _MaDA + "'," + _BlockID, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByMaDL(int _MaDL)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("BatDongSan_getByMaDL '" + _MaDL + "'", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Distribution()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_Distribution", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void DestroyAgent()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_deleteAgent", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        /// <summary>
        /// Cap nhat KHBH da het thoi han
        /// </summary>
        /// <param name="_MaKHBH">: ...</param>
        public void UpdateKHBH(int _MaKHBH)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_updateMaKHBH", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKHBH", _MaKHBH);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public bool Check()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_check", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public string GetByMaSo()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_getByMaSo", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.Add("@MaBDS", SqlDbType.NChar, 12).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@MaBDS"].Value.ToString();
        }

        public string TaoMaSo()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_TaoMaSo", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaSo", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@MaSo"].Value.ToString();
        }

        public DataTable SelectHDMB(string _MaDA, string _BlockID, DateTime _TuNgay, DateTime _DenNgay)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_getDateHDMB", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDA", _MaDA);
            sqlCmd.Parameters.AddWithValue("@BlockID", _BlockID);
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void UpdateStaff()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("BatDongSan_updateStaff", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}