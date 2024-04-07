using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class KhachHangCls
    {
        public int MaKH, MaNMG;
        public string HoKH;
        public string TenKH;
        public DateTime NgaySinh;
        public string SoCMND;
        public DateTime NgayCap, NgayCapGPKD, NgayCapGPKD2;
        public string NoiCap, NoiCapEN, NoiCapGPKD, NoiCapGPKDEN;
        public string NguyenQuan, NguyenQuanEN;
        public string ThuongTru, ThuongTruEN;
        public string MaSoTTNCN;
        public string DiDong, DTCoQuan;
        public string DTCD;
        public string Email;
        public string GhiChu, CongTy1;
        public NgheNghiepCls NgheNghiep = new NgheNghiepCls();
        public string ChucVu;
        public ThoiDiemLienHeCls ThoiDiemLH = new ThoiDiemLienHeCls();
        public string Yahoo;
        public XaCls Xa = new XaCls();
        public XaCls Xa2 = new XaCls();
        public NhanVienCls NhanVien = new NhanVienCls();
        public NhomKHCls NhomKH = new NhomKHCls();
        public bool IsAvatar, IsYear, IsYearNC, IsYearNC2;
        public QuyDanhCls QuyDanh = new QuyDanhCls();
        public string TenCongTy, TenCongTyEN;
        public string MaSoThueCT;
        public string DienThoaiCT;
        public string FaxCT;
        public string DiaChiCT, DiaChiCTEN;
        public LoaiHinhKDCls LHKD = new LoaiHinhKDCls();
        public bool IsPersonal, CBCNV;
        public string DiaChi, DiaChiEN, SoGPKD;
        public XaCls Xa3 = new XaCls();
        public DuAnCls DuAn = new DuAnCls();
        public BlocksCls Blocks = new BlocksCls();
        public TangNhaCls Tang = new TangNhaCls();
        public double DonGia, DienTich, MucThuNhap;
        public byte SoPhongNgu, SoThanhVien, MaPTTT;
        public DaiLyCls DaiLy = new DaiLyCls();
        public NhanVienDaiLyCls NVDL = new NhanVienDaiLyCls();
        public byte MaTinh, MaTinh2, MaQG;
        public short MaHuyen, MaHuyen2;
        public LoaiBDSCls LoaiBDS = new LoaiBDSCls();
        public string dd, MM, yyyy, dd2, MM2, yyyy2;
        public string SanPham;
        public string SoTaiKhoan;
        public int MaNH, MaCN;
        public string SoDTKhanCap, QuocTich;
        public string CodeDIP;
        public string CodeSUN;
        public int HowToKnowID;
        public int LevelID;
        public int PurposeID;
        public string Logo;
        public string DiDong2, DiDong3;

        public KhachHangCls()
        {
        }

        public KhachHangCls(int _MaKH)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", _MaKH);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                CodeDIP = dread["CodeDIP"] as string;
                DiDong2 = dread["DiDong2"] as string;
                DiDong3 = dread["DiDong3"] as string;
                CodeSUN = dread["CodeSUN"] as string;
                MaKH = int.Parse(dread["MaKH"].ToString());
                HoKH = dread["HoKH"] as string;
                TenKH = dread["TenKH"] as string;
                if (dread["NgaySinh"].ToString() != "")
                    NgaySinh = (DateTime)dread["NgaySinh"];
                SoCMND = dread["SoCMND"] as string;
                if (dread["NgayCap"].ToString() != "")
                    NgayCap = (DateTime)dread["NgayCap"];
                NoiCap = dread["NoiCap"] as string;
                NoiCapEN = dread["NoiCapEN"] as string;
                NguyenQuan = dread["NguyenQuan"] as string;
                NguyenQuanEN = dread["NguyenQuanEN"] as string;
                ThuongTru = dread["ThuongTru"] as string;
                ThuongTruEN = dread["ThuongTruEN"] as string;
                MaSoTTNCN = dread["MaSoTTNCN"] as string;
                DiDong = dread["DiDong"] as string;
                DTCD = dread["DTCD"] as string;
                Email = dread["Email"] as string;
                GhiChu = dread["GhiChu"] as string;
                NgheNghiep.MaNN = dread["MaNN"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaNN"].ToString());
                ChucVu = dread["ChucVu"] as string;
                if (dread["MaTDLH"] != DBNull.Value)
                    ThoiDiemLH.MaTDLH = byte.Parse(dread["MaTDLH"].ToString());
                Yahoo = dread["Yahoo"] as string;
                Xa.MaXa = dread["MaXa"].ToString() == "" ? 0 : int.Parse(dread["MaXa"].ToString());
                Xa2.MaXa = dread["MaXa2"].ToString() == "" ? 0 : int.Parse(dread["MaXa2"].ToString());
                Xa3.MaXa = dread["KhuVuc"].ToString() == "" ? 0 : int.Parse(dread["KhuVuc"].ToString());
                NhanVien.MaNV = dread["MaNV"].ToString() == "" ? 0 : int.Parse(dread["MaNV"].ToString());
                if (dread["MaNKH"] != DBNull.Value)
                    NhomKH.MaNKH = byte.Parse(dread["MaNKH"].ToString());
                if (dread["IsAvatar"] != DBNull.Value)
                    IsAvatar = (bool)dread["IsAvatar"];
                IsYear = dread["IsYear"].ToString() != "" ? (bool)dread["IsYear"] : false;
                IsYearNC = dread["IsYearNC"].ToString() != "" ? (bool)dread["IsYearNC"] : false;
                IsYearNC2 = dread["IsYearNC2"].ToString() != "" ? (bool)dread["IsYearNC2"] : false;
                if (dread["MaQD"] != DBNull.Value)
                    QuyDanh.MaQD = byte.Parse(dread["MaQD"].ToString());
                TenCongTy = dread["TenCongTy"] as string;
                TenCongTyEN = dread["TenCongTyEN"] as string;
                MaSoThueCT = dread["MaSoThueCT"] as string;
                DienThoaiCT = dread["DienThoaiCT"] as string;
                FaxCT = dread["FaxCT"] as string;
                DiaChiCT = dread["DiaChiCT"] as string;
                DiaChiCTEN = dread["DiaChiCTEN"] as string;
                LHKD.MaLHKD = dread["MaLHKD"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaLHKD"].ToString());
                IsPersonal = (bool)dread["IsPersonal"];
                CBCNV = dread["CBCNV"].ToString() == "" ? false : (bool)dread["CBCNV"];
                CongTy1 = dread["CongTy1"] as string;
                DiaChi = dread["DiaChi"] as string;
                DiaChiEN = dread["DiaChiEN"] as string;
                SoGPKD = dread["SoGPKD"] as string;
                DuAn.MaDA = dread["MaDA"].ToString() == "" ? -1 : int.Parse(dread["MaDA"].ToString());
                LoaiBDS.MaLBDS = dread["MaLBDS"].ToString() == "" ? (short)0 : short.Parse(dread["MaLBDS"].ToString());
                Blocks.BlockID = dread["BlockID"].ToString() == "" ? 0 : int.Parse(dread["BlockID"].ToString());
                Tang.MaTangNha = dread["MaTang"].ToString() == "" ? 0 : int.Parse(dread["MaTang"].ToString());
                MaPTTT = dread["MaPTTT"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaPTTT"].ToString());
                SoPhongNgu = dread["SoPhongNgu"].ToString() == "" ? (byte)0 : byte.Parse(dread["SoPhongNgu"].ToString());
                SoThanhVien = dread["SoThanhVien"].ToString() == "" ? (byte)0 : byte.Parse(dread["SoThanhVien"].ToString());
                MucThuNhap = dread["MucThuNhap"].ToString() == "" ? 0 : double.Parse(dread["MucThuNhap"].ToString());
                DonGia = dread["DonGia"].ToString() == "" ? 0 : double.Parse(dread["DonGia"].ToString());
                DienTich = dread["DienTich"].ToString() == "" ? 0 : double.Parse(dread["DienTich"].ToString());
                DaiLy.MaDL = dread["MaDL"].ToString() == "" ? 0 : int.Parse(dread["MaDL"].ToString());
                NVDL.MaNV = dread["MaNVDL"].ToString() == "" ? 0 : int.Parse(dread["MaNVDL"].ToString());
                MaTinh = dread["MaTinh"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaTinh"].ToString());
                MaTinh2 = dread["MaTinh2"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaTinh2"].ToString());
                MaHuyen = dread["MaHuyen"].ToString() == "" ? (short)0 : short.Parse(dread["MaHuyen"].ToString());
                MaHuyen2 = dread["MaHuyen2"].ToString() == "" ? (short)0 : short.Parse(dread["MaHuyen2"].ToString());
                MaQG = dread["MaQG"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaQG"].ToString());
                HowToKnowID = dread["HowToKnowID"].ToString() == "" ? (int)0 : int.Parse(dread["HowToKnowID"].ToString());
                LevelID = dread["LevelID"].ToString() == "" ? (int)0 : int.Parse(dread["LevelID"].ToString());
                PurposeID = dread["PurposeID"].ToString() == "" ? (int)0 : int.Parse(dread["PurposeID"].ToString());

                dd = dread["dd"] as string;
                MM = dread["MM"] as string;
                yyyy = dread["yyyy"] as string;
                dd2 = dread["dd2"] as string;
                MM2 = dread["MM2"] as string;
                yyyy2 = dread["yyyy2"] as string;
                SanPham = dread["SanPham"] as string;
                SoDTKhanCap = dread["SoDTKhanCap"] as string;
                QuocTich = dread["QuocTich"] as string;
                NoiCapGPKD = dread["NoiCapGDKKD"] as string;
                NoiCapGPKDEN = dread["NoiCapGDKKDEN"] as string;
                if (dread["NgayCapGDKKD"].ToString() != "")
                    NgayCapGPKD = (DateTime)dread["NgayCapGDKKD"];
                if (dread["NgayCapGDKKD2"].ToString() != "")
                    NgayCapGPKD2 = (DateTime)dread["NgayCapGDKKD2"];
                SoTaiKhoan = dread["SoTaiKhoan"] as string;
                MaNH = dread["MaNH"].ToString() == "" ? 0 : int.Parse(dread["MaNH"].ToString());
                MaCN = dread["MaCN"].ToString() == "" ? 0 : int.Parse(dread["MaCN"].ToString());
                Logo = dread["Logo"] as string;
            }
            sqlCon.Close();
        }

        public void Detail()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_detail", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                HoKH = dread["HoKH"] as string;
                TenKH = dread["TenKH"] as string;
                if (dread["NgaySinh"].ToString() != "")
                    NgaySinh = (DateTime)dread["NgaySinh"];
                SoCMND = dread["SoCMND"] as string;
                if (dread["NgayCap"].ToString() != "")
                    NgayCap = (DateTime)dread["NgayCap"];
                NoiCap = dread["NoiCap"] as string;
                ThuongTru = dread["ThuongTru"] as string;
                MaSoTTNCN = dread["MaSoTTNCN"] as string;
                DiDong = dread["DiDong"] as string;
                DiDong2 = dread["DiDong2"] as string;
                DiDong3 = dread["DiDong3"] as string;
                DTCoQuan = dread["DTCoQuan"] as string;
                DTCD = dread["DTCD"] as string;
                Email = dread["Email"] as string;
                ChucVu = dread["ChucVu"] as string;
                DiaChi = dread["DiaChi"] as string;
            }
            sqlCon.Close();
        }

        public int Insert()
        {

            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaKH", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.AddWithValue("@HoKH", HoKH);
            sqlCmd.Parameters.AddWithValue("@TenKH", TenKH);
            if (NgaySinh.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            else
                sqlCmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCmd.Parameters.AddWithValue("@NoiCapEN", NoiCapEN);
            sqlCmd.Parameters.AddWithValue("@NguyenQuan", NguyenQuan);
            sqlCmd.Parameters.AddWithValue("@NguyenQuanEN", NguyenQuanEN);
            sqlCmd.Parameters.AddWithValue("@ThuongTru", ThuongTru);
            sqlCmd.Parameters.AddWithValue("@ThuongTruEN", ThuongTruEN);
            sqlCmd.Parameters.AddWithValue("@MaSoTTNCN", MaSoTTNCN);
            sqlCmd.Parameters.AddWithValue("@DiDong", DiDong);
            sqlCmd.Parameters.AddWithValue("@DTCD", DTCD);
            sqlCmd.Parameters.AddWithValue("@Email", Email);
            sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            sqlCmd.Parameters.AddWithValue("@MaNN", NgheNghiep.MaNN);
            sqlCmd.Parameters.AddWithValue("@ChucVu", ChucVu);
            sqlCmd.Parameters.AddWithValue("@MaTDLH", ThoiDiemLH.MaTDLH);
            //sqlCmd.Parameters.AddWithValue("@CodeSUN", CodeSUN);
            //sqlCmd.Parameters.AddWithValue("@CodeDIP", CodeDIP);
            sqlCmd.Parameters.AddWithValue("@Yahoo", Yahoo);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@DiaChiEN", DiaChiEN);
            if (Xa.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa", Xa.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa", DBNull.Value);
            if (Xa2.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa2", Xa2.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaNKH", NhomKH.MaNKH);
            sqlCmd.Parameters.AddWithValue("@IsAvatar", IsAvatar);
            sqlCmd.Parameters.AddWithValue("@MaQD", QuyDanh.MaQD);
            sqlCmd.Parameters.AddWithValue("@TenCongTy", TenCongTy);
            sqlCmd.Parameters.AddWithValue("@TenCongTyEN", TenCongTyEN);
            sqlCmd.Parameters.AddWithValue("@MaSoThueCT", MaSoThueCT);
            sqlCmd.Parameters.AddWithValue("@DienThoaiCT", DienThoaiCT);
            sqlCmd.Parameters.AddWithValue("@FaxCT", FaxCT);
            sqlCmd.Parameters.AddWithValue("@DiaChiCT", DiaChiCT);
            sqlCmd.Parameters.AddWithValue("@DiaChiCTEN", DiaChiCTEN);
            sqlCmd.Parameters.AddWithValue("@MaLHKD", LHKD.MaLHKD);
            sqlCmd.Parameters.AddWithValue("@IsPersonal", IsPersonal);
            sqlCmd.Parameters.AddWithValue("@SoGPKD", SoGPKD);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaDL", DBNull.Value);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVDL", NVDL.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDL", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@IsYear", IsYear);
            if (MaTinh != 0)
                sqlCmd.Parameters.AddWithValue("@MaTinh", MaTinh);
            else
                sqlCmd.Parameters.AddWithValue("@MaTinh", DBNull.Value);
            if (MaTinh2 != 0)
                sqlCmd.Parameters.AddWithValue("@MaTinh2", MaTinh2);
            else
                sqlCmd.Parameters.AddWithValue("@MaTinh2", DBNull.Value);
            if (MaHuyen != 0)
                sqlCmd.Parameters.AddWithValue("@MaHuyen", MaHuyen);
            else
                sqlCmd.Parameters.AddWithValue("@MaHuyen", DBNull.Value);
            if (MaHuyen2 != 0)
                sqlCmd.Parameters.AddWithValue("@MaHuyen2", MaHuyen2);
            else
                sqlCmd.Parameters.AddWithValue("@MaHuyen2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@dd", dd);
            sqlCmd.Parameters.AddWithValue("@MM", MM);
            sqlCmd.Parameters.AddWithValue("@yyyy", yyyy);
            sqlCmd.Parameters.AddWithValue("@dd2", dd2);
            sqlCmd.Parameters.AddWithValue("@MM2", MM2);
            sqlCmd.Parameters.AddWithValue("@yyyy2", yyyy2);
            sqlCmd.Parameters.AddWithValue("@SanPham", SanPham);
            if (NgayCapGPKD.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCapGDKKD", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCapGDKKD", DBNull.Value);
            if (NgayCapGPKD2.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCapGDKKD2", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCapGDKKD2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiCapGDKKD", NoiCapGPKD);
            sqlCmd.Parameters.AddWithValue("@NoiCapGDKKDEN", NoiCapGPKDEN);
            sqlCmd.Parameters.AddWithValue("@SoDTKhanCap", SoDTKhanCap);
            sqlCmd.Parameters.AddWithValue("@QuocTich", QuocTich);
            sqlCmd.Parameters.AddWithValue("@SoTaiKhoan", SoTaiKhoan);
            if (MaNH != 0)
                sqlCmd.Parameters.AddWithValue("@MaNH", MaNH);
            else
                sqlCmd.Parameters.AddWithValue("@MaNH", DBNull.Value);
            if (MaCN != 0)
                sqlCmd.Parameters.AddWithValue("@MaCN", MaCN);
            else
                sqlCmd.Parameters.AddWithValue("@MaCN", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MucThuNhap", MucThuNhap);
            sqlCmd.Parameters.AddWithValue("@SoThanhVien", SoThanhVien);
            if (DuAn.MaDA != -1)
                sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            else
                sqlCmd.Parameters.AddWithValue("@MaDA", DBNull.Value);
            if (LoaiBDS.MaLBDS != 0)
                sqlCmd.Parameters.AddWithValue("@MaLBDS", LoaiBDS.MaLBDS);
            else
                sqlCmd.Parameters.AddWithValue("@MaLBDS", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaQG", MaQG);
            if (HowToKnowID != 0)
                sqlCmd.Parameters.AddWithValue("@HowToKnowID", HowToKnowID);
            else
                sqlCmd.Parameters.AddWithValue("@HowToKnowID", DBNull.Value);
            if (LevelID != 0)
                sqlCmd.Parameters.AddWithValue("@LevelID", LevelID);
            else
                sqlCmd.Parameters.AddWithValue("@LevelID", DBNull.Value);
            if (PurposeID != 0)
                sqlCmd.Parameters.AddWithValue("@PurposeID", PurposeID);
            else
                sqlCmd.Parameters.AddWithValue("@PurposeID", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@Logo", Logo);

            sqlCon.Open();

            try
            {
                sqlCmd.ExecuteNonQuery();

                return int.Parse(sqlCmd.Parameters["@MaKH"].Value.ToString());
            }
            catch { return 0; }
            finally
            {
                sqlCon.Close();
            }
        }

        public int InsertPersonal()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_addPersonal", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaKH", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.AddWithValue("@HoKH", HoKH);
            sqlCmd.Parameters.AddWithValue("@TenKH", TenKH);
            if (NgaySinh.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            else
                sqlCmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCmd.Parameters.AddWithValue("@NoiCapEN", NoiCapEN);
            sqlCmd.Parameters.AddWithValue("@NguyenQuan", NguyenQuan);
            sqlCmd.Parameters.AddWithValue("@NguyenQuanEN", NguyenQuanEN);
            sqlCmd.Parameters.AddWithValue("@ThuongTru", ThuongTru);
            sqlCmd.Parameters.AddWithValue("@ThuongTruEN", ThuongTruEN);
            sqlCmd.Parameters.AddWithValue("@MaSoTTNCN", MaSoTTNCN);
            sqlCmd.Parameters.AddWithValue("@DiDong", DiDong);
            sqlCmd.Parameters.AddWithValue("@DTCD", DTCD);
            sqlCmd.Parameters.AddWithValue("@Email", Email);
            sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            sqlCmd.Parameters.AddWithValue("@MaNN", NgheNghiep.MaNN);
            sqlCmd.Parameters.AddWithValue("@ChucVu", ChucVu);
            sqlCmd.Parameters.AddWithValue("@MaTDLH", ThoiDiemLH.MaTDLH);
            sqlCmd.Parameters.AddWithValue("@Yahoo", Yahoo);
            sqlCmd.Parameters.AddWithValue("@CodeDIP", CodeDIP);
            sqlCmd.Parameters.AddWithValue("@CodeSUN", CodeSUN);
            if (Xa.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa", Xa.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa", DBNull.Value);
            if (Xa2.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa2", Xa2.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaNKH", NhomKH.MaNKH);
            sqlCmd.Parameters.AddWithValue("@IsAvatar", IsAvatar);
            sqlCmd.Parameters.AddWithValue("@IsYear", IsYear);
            sqlCmd.Parameters.AddWithValue("@MaQD", QuyDanh.MaQD);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@DiaChiEN", DiaChiEN);
            sqlCmd.Parameters.AddWithValue("@MucThuNhap", MucThuNhap);
            sqlCmd.Parameters.AddWithValue("@SoPhongNgu", SoPhongNgu);
            sqlCmd.Parameters.AddWithValue("@SoThanhVien", SoThanhVien);
            if (DuAn.MaDA != -1)
                sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            else
                sqlCmd.Parameters.AddWithValue("@MaDA", DBNull.Value);
            if (LoaiBDS.MaLBDS != 0)
                sqlCmd.Parameters.AddWithValue("@MaLBDS", LoaiBDS.MaLBDS);
            else
                sqlCmd.Parameters.AddWithValue("@MaLBDS", DBNull.Value);
            if (Blocks.BlockID != 0)
                sqlCmd.Parameters.AddWithValue("@BlockID", Blocks.BlockID);
            else
                sqlCmd.Parameters.AddWithValue("@BlockID", DBNull.Value);
            if (Tang.MaTangNha != 0)
                sqlCmd.Parameters.AddWithValue("@MaTangNha", Tang.MaTangNha);
            else
                sqlCmd.Parameters.AddWithValue("@MaTangNha", DBNull.Value);
            if (Xa3.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@KhuVuc", Xa3.MaXa);//Khu vuc
            else
                sqlCmd.Parameters.AddWithValue("@KhuVuc", DBNull.Value);
            if (MaPTTT != 0)
                sqlCmd.Parameters.AddWithValue("@MaPTTT", MaPTTT);
            else
                sqlCmd.Parameters.AddWithValue("@MaPTTT", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DonGia", DonGia);
            sqlCmd.Parameters.AddWithValue("@DienTich", DienTich);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaDL", DBNull.Value);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVDL", NVDL.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDL", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@CBCNV", CBCNV);
            sqlCmd.Parameters.AddWithValue("@CongTy1", CongTy1);
            if (MaTinh != 0)
                sqlCmd.Parameters.AddWithValue("@MaTinh", MaTinh);
            else
                sqlCmd.Parameters.AddWithValue("@MaTinh", DBNull.Value);
            if (MaTinh2 != 0)
                sqlCmd.Parameters.AddWithValue("@MaTinh2", MaTinh2);
            else
                sqlCmd.Parameters.AddWithValue("@MaTinh2", DBNull.Value);
            if (MaHuyen != 0)
                sqlCmd.Parameters.AddWithValue("@MaHuyen", MaHuyen);
            else
                sqlCmd.Parameters.AddWithValue("@MaHuyen", DBNull.Value);
            if (MaHuyen2 != 0)
                sqlCmd.Parameters.AddWithValue("@MaHuyen2", MaHuyen2);
            else
                sqlCmd.Parameters.AddWithValue("@MaHuyen2", DBNull.Value);
            if (HowToKnowID != 0)
                sqlCmd.Parameters.AddWithValue("@HowToKnowID", HowToKnowID);
            else
                sqlCmd.Parameters.AddWithValue("@HowToKnowID", DBNull.Value);
            if (LevelID != 0)
                sqlCmd.Parameters.AddWithValue("@LevelID", LevelID);
            else
                sqlCmd.Parameters.AddWithValue("@LevelID", DBNull.Value);
            if (PurposeID != 0)
                sqlCmd.Parameters.AddWithValue("@PurposeID", PurposeID);
            else
                sqlCmd.Parameters.AddWithValue("@PurposeID", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@dd", dd);
            sqlCmd.Parameters.AddWithValue("@MM", MM);
            sqlCmd.Parameters.AddWithValue("@yyyy", yyyy);
            sqlCmd.Parameters.AddWithValue("@dd2", dd2);
            sqlCmd.Parameters.AddWithValue("@MM2", MM2);
            sqlCmd.Parameters.AddWithValue("@yyyy2", yyyy2);
            sqlCmd.Parameters.AddWithValue("@SanPham", SanPham);
            sqlCmd.Parameters.AddWithValue("@SoDTKhanCap", SoDTKhanCap);
            sqlCmd.Parameters.AddWithValue("@QuocTich", QuocTich);
            sqlCmd.Parameters.AddWithValue("@MaQG", MaQG);
            sqlCmd.Parameters.AddWithValue("@Logo", Logo);
            sqlCmd.Parameters.AddWithValue("@DiDong2", DiDong2);
            sqlCmd.Parameters.AddWithValue("@DiDong3", DiDong3);
            sqlCon.Open();
            try
            {
                sqlCmd.ExecuteNonQuery();

                return int.Parse(sqlCmd.Parameters["@MaKH"].Value.ToString());
            }
            catch
            {
                return 0;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public int InsertFast()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_addPersonalFast", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaKH", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.AddWithValue("@HoKH", HoKH);
            sqlCmd.Parameters.AddWithValue("@TenKH", TenKH);
            if (NgaySinh.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            else
                sqlCmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCmd.Parameters.AddWithValue("@NguyenQuan", ThuongTru);
            sqlCmd.Parameters.AddWithValue("@ThuongTru", ThuongTru);
            sqlCmd.Parameters.AddWithValue("@DiDong", DiDong);
            sqlCmd.Parameters.AddWithValue("@DTCD", DTCD);
            sqlCmd.Parameters.AddWithValue("@Email", Email);
            if (Xa.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa", Xa.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa", DBNull.Value);
            if (Xa2.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa2", Xa2.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaKH"].Value.ToString());
        }

        public void UpdatePersonal()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_updatePersonal", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@HoKH", HoKH);
            sqlCmd.Parameters.AddWithValue("@TenKH", TenKH);
            if (NgaySinh.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            else
                sqlCmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCmd.Parameters.AddWithValue("@NoiCapEN", NoiCapEN);
            sqlCmd.Parameters.AddWithValue("@NguyenQuan", NguyenQuan);
            sqlCmd.Parameters.AddWithValue("@NguyenQuanEN", NguyenQuanEN);
            sqlCmd.Parameters.AddWithValue("@ThuongTru", ThuongTru);
            sqlCmd.Parameters.AddWithValue("@ThuongTruEN", ThuongTruEN);
            sqlCmd.Parameters.AddWithValue("@MaSoTTNCN", MaSoTTNCN);
            sqlCmd.Parameters.AddWithValue("@DiDong", DiDong);
            sqlCmd.Parameters.AddWithValue("@DTCD", DTCD);
            sqlCmd.Parameters.AddWithValue("@Email", Email);
            sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            sqlCmd.Parameters.AddWithValue("@MaNN", NgheNghiep.MaNN);
            sqlCmd.Parameters.AddWithValue("@ChucVu", ChucVu);
            sqlCmd.Parameters.AddWithValue("@MaTDLH", ThoiDiemLH.MaTDLH);
            sqlCmd.Parameters.AddWithValue("@Yahoo", Yahoo);
            sqlCmd.Parameters.AddWithValue("@CodeDIP", CodeDIP);
            sqlCmd.Parameters.AddWithValue("@CodeSUN", CodeSUN);
            if (Xa.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa", Xa.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa", DBNull.Value);
            if (Xa2.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa2", Xa2.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaNKH", NhomKH.MaNKH);
            sqlCmd.Parameters.AddWithValue("@IsAvatar", IsAvatar);
            sqlCmd.Parameters.AddWithValue("@IsYear", IsYear);
            sqlCmd.Parameters.AddWithValue("@MaQD", QuyDanh.MaQD);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@DiaChiEN", DiaChiEN);
            sqlCmd.Parameters.AddWithValue("@MucThuNhap", MucThuNhap);
            sqlCmd.Parameters.AddWithValue("@SoPhongNgu", SoPhongNgu);
            sqlCmd.Parameters.AddWithValue("@SoThanhVien", SoThanhVien);
            if (DuAn.MaDA != -1)
                sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            else
                sqlCmd.Parameters.AddWithValue("@MaDA", DBNull.Value);
            if (LoaiBDS.MaLBDS != 0)
                sqlCmd.Parameters.AddWithValue("@MaLBDS", LoaiBDS.MaLBDS);
            else
                sqlCmd.Parameters.AddWithValue("@MaLBDS", DBNull.Value);
            if (Blocks.BlockID != 0)
                sqlCmd.Parameters.AddWithValue("@BlockID", Blocks.BlockID);
            else
                sqlCmd.Parameters.AddWithValue("@BlockID", DBNull.Value);
            if (Tang.MaTangNha != 0)
                sqlCmd.Parameters.AddWithValue("@MaTangNha", Tang.MaTangNha);
            else
                sqlCmd.Parameters.AddWithValue("@MaTangNha", DBNull.Value);
            if (Xa3.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@KhuVuc", Xa3.MaXa);//Khu vuc
            else
                sqlCmd.Parameters.AddWithValue("@KhuVuc", DBNull.Value);
            if (MaPTTT != 0)
                sqlCmd.Parameters.AddWithValue("@MaPTTT", MaPTTT);
            else
                sqlCmd.Parameters.AddWithValue("@MaPTTT", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DonGia", DonGia);
            sqlCmd.Parameters.AddWithValue("@DienTich", DienTich);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaDL", DBNull.Value);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVDL", NVDL.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDL", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@CBCNV", CBCNV);
            sqlCmd.Parameters.AddWithValue("@CongTy1", CongTy1);
            if (MaTinh != 0)
                sqlCmd.Parameters.AddWithValue("@MaTinh", MaTinh);
            else
                sqlCmd.Parameters.AddWithValue("@MaTinh", DBNull.Value);
            if (MaTinh2 != 0)
                sqlCmd.Parameters.AddWithValue("@MaTinh2", MaTinh2);
            else
                sqlCmd.Parameters.AddWithValue("@MaTinh2", DBNull.Value);
            if (MaHuyen != 0)
                sqlCmd.Parameters.AddWithValue("@MaHuyen", MaHuyen);
            else
                sqlCmd.Parameters.AddWithValue("@MaHuyen", DBNull.Value);
            if (MaHuyen2 != 0)
                sqlCmd.Parameters.AddWithValue("@MaHuyen2", MaHuyen2);
            else
                sqlCmd.Parameters.AddWithValue("@MaHuyen2", DBNull.Value);

            sqlCmd.Parameters.AddWithValue("@dd", dd);
            sqlCmd.Parameters.AddWithValue("@MM", MM);
            sqlCmd.Parameters.AddWithValue("@yyyy", yyyy);
            sqlCmd.Parameters.AddWithValue("@dd2", dd2);
            sqlCmd.Parameters.AddWithValue("@MM2", MM2);
            sqlCmd.Parameters.AddWithValue("@yyyy2", yyyy2);
            sqlCmd.Parameters.AddWithValue("@SanPham", SanPham);
            sqlCmd.Parameters.AddWithValue("@SoDTKhanCap", SoDTKhanCap);
            sqlCmd.Parameters.AddWithValue("@QuocTich", QuocTich);
            sqlCmd.Parameters.AddWithValue("@MaQG", MaQG);

            if (HowToKnowID != 0)
                sqlCmd.Parameters.AddWithValue("@HowToKnowID", HowToKnowID);
            else
                sqlCmd.Parameters.AddWithValue("@HowToKnowID", DBNull.Value);
            if (LevelID != 0)
                sqlCmd.Parameters.AddWithValue("@LevelID", LevelID);
            else
                sqlCmd.Parameters.AddWithValue("@LevelID", DBNull.Value);
            if (PurposeID != 0)
                sqlCmd.Parameters.AddWithValue("@PurposeID", PurposeID);
            else
                sqlCmd.Parameters.AddWithValue("@PurposeID", DBNull.Value);
            if (Logo != null)
                sqlCmd.Parameters.AddWithValue("@Logo", Logo);
            else
                sqlCmd.Parameters.AddWithValue("@Logo", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DiDong2", DiDong2);
            sqlCmd.Parameters.AddWithValue("@DiDong3", DiDong3);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@HoKH", HoKH);
            sqlCmd.Parameters.AddWithValue("@TenKH", TenKH);
            if (NgaySinh.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            else
                sqlCmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCmd.Parameters.AddWithValue("@NoiCapEN", NoiCapEN);
            sqlCmd.Parameters.AddWithValue("@NguyenQuan", NguyenQuan);
            sqlCmd.Parameters.AddWithValue("@NguyenQuanEN", NguyenQuanEN);
            sqlCmd.Parameters.AddWithValue("@ThuongTru", ThuongTru);
            sqlCmd.Parameters.AddWithValue("@ThuongTruEN", ThuongTruEN);
            sqlCmd.Parameters.AddWithValue("@MaSoTTNCN", MaSoTTNCN);
            sqlCmd.Parameters.AddWithValue("@DiDong", DiDong);
            sqlCmd.Parameters.AddWithValue("@DTCD", DTCD);
            sqlCmd.Parameters.AddWithValue("@Email", Email);
            sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            sqlCmd.Parameters.AddWithValue("@MaNN", NgheNghiep.MaNN);
            sqlCmd.Parameters.AddWithValue("@ChucVu", ChucVu);
            sqlCmd.Parameters.AddWithValue("@MaTDLH", ThoiDiemLH.MaTDLH);
            sqlCmd.Parameters.AddWithValue("@Yahoo", Yahoo);
            sqlCmd.Parameters.AddWithValue("@CodeDIP", CodeDIP);
            sqlCmd.Parameters.AddWithValue("@CodeSUN", CodeSUN);
            if (Xa.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa", Xa.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa", DBNull.Value);
            if (Xa2.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa2", Xa2.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaNKH", NhomKH.MaNKH);
            sqlCmd.Parameters.AddWithValue("@IsAvatar", IsAvatar);
            sqlCmd.Parameters.AddWithValue("@IsYear", IsYear);
            sqlCmd.Parameters.AddWithValue("@MaQD", QuyDanh.MaQD);
            sqlCmd.Parameters.AddWithValue("@TenCongTy", TenCongTy);
            sqlCmd.Parameters.AddWithValue("@TenCongTyEN", TenCongTyEN);
            sqlCmd.Parameters.AddWithValue("@MaSoThueCT", MaSoThueCT);
            sqlCmd.Parameters.AddWithValue("@DienThoaiCT", DienThoaiCT);
            sqlCmd.Parameters.AddWithValue("@FaxCT", FaxCT);
            sqlCmd.Parameters.AddWithValue("@DiaChiCT", DiaChiCT);
            sqlCmd.Parameters.AddWithValue("@DiaChiCTEN", DiaChiCTEN);
            sqlCmd.Parameters.AddWithValue("@MaLHKD", LHKD.MaLHKD);
            sqlCmd.Parameters.AddWithValue("@IsPersonal", IsPersonal);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@DiaChiEN", DiaChiEN);
            sqlCmd.Parameters.AddWithValue("@SoGPKD", SoGPKD);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaDL", DBNull.Value);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVDL", NVDL.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDL", DBNull.Value);
            if (MaTinh != 0)
                sqlCmd.Parameters.AddWithValue("@MaTinh", MaTinh);
            else
                sqlCmd.Parameters.AddWithValue("@MaTinh", DBNull.Value);
            if (MaTinh2 != 0)
                sqlCmd.Parameters.AddWithValue("@MaTinh2", MaTinh2);
            else
                sqlCmd.Parameters.AddWithValue("@MaTinh2", DBNull.Value);
            if (MaHuyen != 0)
                sqlCmd.Parameters.AddWithValue("@MaHuyen", MaHuyen);
            else
                sqlCmd.Parameters.AddWithValue("@MaHuyen", DBNull.Value);
            if (MaHuyen2 != 0)
                sqlCmd.Parameters.AddWithValue("@MaHuyen2", MaHuyen2);
            else
                sqlCmd.Parameters.AddWithValue("@MaHuyen2", DBNull.Value);
            if (HowToKnowID != 0)
                sqlCmd.Parameters.AddWithValue("@HowToKnowID", HowToKnowID);
            else
                sqlCmd.Parameters.AddWithValue("@HowToKnowID", DBNull.Value);
            if (LevelID != 0)
                sqlCmd.Parameters.AddWithValue("@LevelID", LevelID);
            else
                sqlCmd.Parameters.AddWithValue("@LevelID", DBNull.Value);
            if (PurposeID != 0)
                sqlCmd.Parameters.AddWithValue("@PurposeID", PurposeID);
            else
                sqlCmd.Parameters.AddWithValue("@PurposeID", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@dd", dd);
            sqlCmd.Parameters.AddWithValue("@MM", MM);
            sqlCmd.Parameters.AddWithValue("@yyyy", yyyy);
            sqlCmd.Parameters.AddWithValue("@dd2", dd2);
            sqlCmd.Parameters.AddWithValue("@MM2", MM2);
            sqlCmd.Parameters.AddWithValue("@yyyy2", yyyy2);
            sqlCmd.Parameters.AddWithValue("@SanPham", SanPham);
            if (NgayCapGPKD.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCapGDKKD", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCapGDKKD", DBNull.Value);
            if (NgayCapGPKD2.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCapGDKKD2", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCapGDKKD2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiCapGDKKD", NoiCapGPKD);
            sqlCmd.Parameters.AddWithValue("@NoiCapGDKKDEN", NoiCapGPKDEN);
            sqlCmd.Parameters.AddWithValue("@SoTaiKhoan", SoTaiKhoan);
            sqlCmd.Parameters.AddWithValue("@SoDTKhanCap", SoDTKhanCap);
            sqlCmd.Parameters.AddWithValue("@QuocTich", QuocTich);
            if (MaNH != 0)
                sqlCmd.Parameters.AddWithValue("@MaNH", MaNH);
            else
                sqlCmd.Parameters.AddWithValue("@MaNH", DBNull.Value);
            if (MaCN != 0)
                sqlCmd.Parameters.AddWithValue("@MaCN", MaCN);
            else
                sqlCmd.Parameters.AddWithValue("@MaCN", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MucThuNhap", MucThuNhap);
            sqlCmd.Parameters.AddWithValue("@SoThanhVien", SoThanhVien);
            if (DuAn.MaDA != -1)
                sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            else
                sqlCmd.Parameters.AddWithValue("@MaDA", DBNull.Value);
            if (LoaiBDS.MaLBDS != 0)
                sqlCmd.Parameters.AddWithValue("@MaLBDS", LoaiBDS.MaLBDS);
            else
                sqlCmd.Parameters.AddWithValue("@MaLBDS", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaQG", MaQG);
            sqlCmd.Parameters.AddWithValue("@Logo", Logo);
            sqlCmd.Parameters.AddWithValue("@DiDong2", DiDong2);
            sqlCmd.Parameters.AddWithValue("@DiDong3", DiDong3);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        /// <summary>
        /// Update customer when set PhieuGiuCho, PhieuDatCoc, HopDongMuaBan
        /// </summary>
        public void Update2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_update2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            if (NgaySinh.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            else
                sqlCmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCmd.Parameters.AddWithValue("@NguyenQuan", NguyenQuan);
            sqlCmd.Parameters.AddWithValue("@ThuongTru", ThuongTru);
            sqlCmd.Parameters.AddWithValue("@DiDong", DiDong);
            sqlCmd.Parameters.AddWithValue("@DTCD", DTCD);
            sqlCmd.Parameters.AddWithValue("@Email", Email);
            if (Xa.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa", Xa.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa", DBNull.Value);
            if (Xa2.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa2", Xa2.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update3()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_update3", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            if (NgaySinh.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            else
                sqlCmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update4()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_update4", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCmd.Parameters.AddWithValue("@ThuongTru", ThuongTru);
            sqlCmd.Parameters.AddWithValue("@DiDong", DiDong);
            sqlCmd.Parameters.AddWithValue("@DTCD", DTCD);
            if (Xa.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa", Xa.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa", DBNull.Value);
            if (Xa2.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa2", Xa2.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@DiDong2", DiDong2);
            sqlCmd.Parameters.AddWithValue("@DTCoQuan", DTCoQuan);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateMaNMG()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_updateMaNMG", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@MaNMG", MaNMG);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KhachHang_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable GiaoDich(int _MaKH)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KhachHang_GiaoDich " + _MaKH, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable GiaoDich2(int _MaKH)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KhachHang_GiaoDich2 " + _MaKH, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public string GetAddress()
        {

            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_getAddress", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            if (Xa.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa", Xa.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa", DBNull.Value);
            if (MaHuyen != 0)
                sqlCmd.Parameters.AddWithValue("@MaHuyen", MaHuyen);
            else
                sqlCmd.Parameters.AddWithValue("@MaHuyen", DBNull.Value);
            if (MaTinh != 0)
                sqlCmd.Parameters.AddWithValue("@MaTinh", MaTinh);
            else
                sqlCmd.Parameters.AddWithValue("@MaTinh", DBNull.Value);
            sqlCmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            return sqlCmd.Parameters["@DiaChi"].Value.ToString();
        }

        public string GetAddress2()
        {

            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_getAddress", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            if (Xa2.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa", Xa2.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa", DBNull.Value);
            if (MaHuyen2 != 0)
                sqlCmd.Parameters.AddWithValue("@MaHuyen", MaHuyen2);
            else
                sqlCmd.Parameters.AddWithValue("@MaHuyen", DBNull.Value);
            if (MaTinh2 != 0)
                sqlCmd.Parameters.AddWithValue("@MaTinh", MaTinh2);
            else
                sqlCmd.Parameters.AddWithValue("@MaTinh", DBNull.Value);
            sqlCmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            return sqlCmd.Parameters["@DiaChi"].Value.ToString();
        }

        public string GetCustomerPay()
        {

            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_getCustomerPay", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.Add("@Re", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            return sqlCmd.Parameters["@Re"].Value.ToString();
        }

        public int GetByHoTenPersonal()
        {

            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_getByHoTenPersonal", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@HoTenKH", HoKH);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            sqlCmd.Parameters.Add("@MaKH", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            return int.Parse(sqlCmd.Parameters["@MaKH"].Value.ToString());
        }

        public int GetByTenCongTy()
        {

            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_getByTenCongTy", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenCongTy", TenCongTy);
            sqlCmd.Parameters.Add("@MaKH", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            return int.Parse(sqlCmd.Parameters["@MaKH"].Value.ToString());
        }

        public DataTable SelectPersonal()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KhachHang_getCaNhan", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectPerByStaff(int _StaffID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_getCaNhanByStaff", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", _StaffID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectPerByGroup(int _MaNV, byte _GroupID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_getCaNhanByGroup", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", _MaNV);
            sqlCmd.Parameters.AddWithValue("@GroupID", _GroupID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectPerByDeparment(int _MaNV, byte _DepID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_getCaNhanByDeparment", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", _MaNV);
            sqlCmd.Parameters.AddWithValue("@DepID", _DepID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectTiemNang()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KhachHang_TiemNang " + MaKH, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectCompany()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KhachHang_getByDoanhNghiep", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectComByStaff(int _StaffID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_getByDoanhNghiepByStaff", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", _StaffID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectComByGroup(int _MaNV, byte _GroupID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_getByDoanhNghiepByGroup", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", _MaNV);
            sqlCmd.Parameters.AddWithValue("@GroupID", _GroupID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectComByDeparment(int _MaNV, byte _DepID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_getByDoanhNghiepByDeparment", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", _MaNV);
            sqlCmd.Parameters.AddWithValue("@DepID", _DepID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShow()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KhachHang_getAllShow", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectReferrer()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NguoiMoiGioi_getByMaNMG " + MaKH, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public bool CheckCustomerIsPersonal()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_checkIsPersonal", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                HoKH = dread["HoKH"] as string;
                TenKH = dread["TenKH"] as string;
                IsPersonal = (bool)dread["IsPersonal"];
            }
            sqlCon.Close();
            return IsPersonal;
        }

        public DataTable CongNo(int _MaDA, DateTime _DenNgay)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("CongNo_getAll", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDA", _MaDA);
            if (_DenNgay != DateTime.MinValue)
                sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            else
                sqlCmd.Parameters.AddWithValue("@DenNgay", DBNull.Value);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable CongNoCu(string _MaDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_CongNoCu", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDA", _MaDA);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable CongNoAuto(int _SoNgay)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_CongNoAuto", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoNgay", _SoNgay);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public bool CheckDuplicatePersonal()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_checkDuplicatePersonal", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@HoKH", HoKH);
            sqlCmd.Parameters.AddWithValue("@TenKH", TenKH);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public bool CheckDuplicate()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_checkDuplicate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenCongTy", TenCongTy);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public bool CheckSoCMND()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_checkSoCMND", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public bool CheckSoCMND2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_checkSoCMND2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public bool CheckEmail()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_checkEmail", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@Email", Email);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public bool CheckDiDong()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_checkDiDong", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@DiDong", DiDong);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public bool CheckSoGPKD()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_checkSoGPKD", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoGPKD", SoGPKD);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public bool CheckSoGPKD2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_checkSoGPKD2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoGPKD", SoGPKD);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateReferrer()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_updateRefer", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            if (MaNMG != 0)
                sqlCmd.Parameters.AddWithValue("@MaNMG", MaNMG);
            else
                sqlCmd.Parameters.AddWithValue("@MaNMG", DBNull.Value);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void GetReferrer()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_getReferrer", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaNMG = int.Parse(dread["MaNMG"].ToString());
                HoKH = dread["HoTen"].ToString();
            }
            sqlCon.Close();
        }

        public DataTable SearchSoCMND()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KhachHang_searchBySoCMND N'" + SoCMND + "'", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public string GetCustomer()
        {

            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_getCustomer", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.Add("@Re", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            return sqlCmd.Parameters["@Re"].Value.ToString();
        }

        public DataTable FindPersonal(string queryString, int opTion)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);

            SqlDataAdapter sqlDA = new SqlDataAdapter("KhachHang_findAllPersonal N'" + queryString + "'," + opTion, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable FindPersonalByDepartment(string queryString, int opTion)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KhachHang_findAllPersonalByDepartment N'" + queryString + "'," + opTion + ", " + NhanVien.PhongBan.MaPB, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable FindPersonalByGroup(string queryString, int opTion)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KhachHang_findAllPersonalByGroup N'" + queryString + "', " + opTion + ", " + NhanVien.NKD.MaNKD, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable FindPersonalByStaff(string queryString, int opTion)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KhachHang_findAllPersonalByStaff N'" + queryString + "', " + opTion + ", " + NhanVien.MaNV, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectStaffManager()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("Staff_Customer_getAllByCusID " + MaKH, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectHistory()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KhachHang_NhatKy_getByMaKH " + MaKH, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void UpdateStaff()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KhachHang_updateStaff", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}
