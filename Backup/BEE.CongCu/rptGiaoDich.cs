using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.Linq;
using BEE.ThuVien;

namespace BEE.CongCu
{
    public class rptGiaoDich : DevExpress.XtraRichEdit.RichEditControl
    {
        public rptGiaoDich(int maGD, byte maLGD)
        {
            MasterDataContext db = new MasterDataContext();
            pgcPhieuGiuCho objGC;
            var objTien = new it.TienTeCls();

            switch (maLGD)
            {
                case 1: //Phieu giu cho
                    objGC = db.pgcPhieuGiuChos.Single(p => p.MaPGC == maGD);
                    this.RtfText = objGC.Template;
                    Document.ReplaceAll("[SoPhieu]", objGC.SoPhieu, SearchOptions.None);
                    Document.ReplaceAll("[NgayKy]", objGC.NgayKy.Value.ToString("dd/MM/yyyy"), SearchOptions.None);
                    Document.ReplaceAll("[Ngay]", objGC.NgayKy.Value.ToString("dd"), SearchOptions.None);
                    Document.ReplaceAll("[Thang]", objGC.NgayKy.Value.ToString("MM"), SearchOptions.None);
                    Document.ReplaceAll("[Nam]", objGC.NgayKy.Value.ToString("yyyy"), SearchOptions.None);
                    Document.ReplaceAll("[ThoiHan]", objGC.ThoiHan.ToString(), SearchOptions.None);
                    Document.ReplaceAll("[TienCoc]", string.Format("{0:#,0.##} {1}", objGC.TienGiuCho, 
                        objGC.bdsSanPham.LoaiTien.DienGiai), SearchOptions.None);
                    Document.ReplaceAll("[TienCocBC]", objTien.DocTienBangChu(objGC.TienGiuCho.Value, 
                        objGC.bdsSanPham.LoaiTien.DienGiai), SearchOptions.None);
                    break;
                case 2: //Phieu dat coc
                    var objDC = db.pdcPhieuDatCocs.Single(p => p.MaPDC == maGD);
                    objGC = objDC.pgcPhieuGiuCho;
                    this.RtfText = objDC.Template;
                    Document.ReplaceAll("[SoPhieu]", objDC.SoPhieu, SearchOptions.None);
                    Document.ReplaceAll("[NgayKy]", objDC.NgayKy.Value.ToString("dd/MM/yyyy"), SearchOptions.None);
                    Document.ReplaceAll("[Ngay]", objDC.NgayKy.Value.ToString("dd"), SearchOptions.None);
                    Document.ReplaceAll("[Thang]", objDC.NgayKy.Value.ToString("MM"), SearchOptions.None);
                    Document.ReplaceAll("[Nam]", objDC.NgayKy.Value.ToString("yyyy"), SearchOptions.None);
                    Document.ReplaceAll("[ThoiHan]", objDC.ThoiHan.ToString(), SearchOptions.None);
                    Document.ReplaceAll("[TienCoc]", string.Format("{0:#,0.##} {1}", objDC.TienCoc,
                        objGC.bdsSanPham.LoaiTien.DienGiai), SearchOptions.None);
                    Document.ReplaceAll("[TienCocBC]", objTien.DocTienBangChu(objDC.TienCoc.Value,
                        objGC.bdsSanPham.LoaiTien.DienGiai), SearchOptions.None);
                    break;
                case 3: //vay von
                    var objVV = db.vvbhHopDongs.Single(p => p.MaHDVV == maGD);
                    objGC = objVV.pgcPhieuGiuCho;
                    this.RtfText = objVV.Template;
                    Document.ReplaceAll("[SoPhieu]", objVV.SoHDVV, SearchOptions.None);
                    Document.ReplaceAll("[NgayKy]", objVV.NgayKy.Value.ToString("dd/MM/yyyy"), SearchOptions.None);
                    Document.ReplaceAll("[Ngay]", objVV.NgayKy.Value.ToString("dd"), SearchOptions.None);
                    Document.ReplaceAll("[Thang]", objVV.NgayKy.Value.ToString("MM"), SearchOptions.None);
                    Document.ReplaceAll("[Nam]", objVV.NgayKy.Value.ToString("yyyy"), SearchOptions.None);
                    Document.ReplaceAll("[ThoiHan]", objVV.ThoiHan.ToString(), SearchOptions.None);
                    break;
                case 4: //Hop dong mua ban
                    var objHD = db.HopDongMuaBans.Single(p => p.MaHDMB == maGD);
                    objGC = objHD.pgcPhieuGiuCho;
                    this.RtfText = objHD.Template;
                    Document.ReplaceAll("[SoPhieu]", objHD.SoHDMB, SearchOptions.None);
                    Document.ReplaceAll("[NgayKy]", objHD.NgayKy.Value.ToString("dd/MM/yyyy"), SearchOptions.None);
                    Document.ReplaceAll("[Ngay]", objHD.NgayKy.Value.ToString("dd"), SearchOptions.None);
                    Document.ReplaceAll("[Thang]", objHD.NgayKy.Value.ToString("MM"), SearchOptions.None);
                    Document.ReplaceAll("[Nam]", objHD.NgayKy.Value.ToString("yyyy"), SearchOptions.None);
                    Document.ReplaceAll("[ThoiHan]", objHD.ThoiHan.ToString(), SearchOptions.None);
                    break;
                default:
                    return;
            }
            //Bat dong san
            var objSP = objGC.bdsSanPham;
            Document.ReplaceAll("[KyHieu]", objSP.KyHieu, SearchOptions.None);
            Document.ReplaceAll("[TenKhu]", objSP.MaKhu != null ? objSP.Khu.TenKhu : "", SearchOptions.None);
            Document.ReplaceAll("[TenPK]", objSP.MaPK != null ? objSP.PhanKhu.TenPK : "", SearchOptions.None);
            Document.ReplaceAll("[MaLo]", objSP.MaLo, SearchOptions.None);
            Document.ReplaceAll("[Tang]", objSP.SoTang.ToString(), SearchOptions.None);
            Document.ReplaceAll("[DienTichXD]", string.Format("{0:#,0.##} m2", objSP.DienTichXD), SearchOptions.None);
            Document.ReplaceAll("[DonGiaXD]", string.Format("{0:#,0.##} {1}", objSP.DonGiaXD, objSP.LoaiTien.DienGiai), SearchOptions.None);
            Document.ReplaceAll("[DonGiaXDBC]", objTien.DocTienBangChu(objSP.DonGiaXD.Value, objSP.LoaiTien.DienGiai), SearchOptions.None);
            Document.ReplaceAll("[ThanhTienXD]", string.Format("{0:#,0.##} {1}", objSP.ThanhTienXD, objSP.LoaiTien.DienGiai), SearchOptions.None);
            Document.ReplaceAll("[ThanhTienXDBC]", objTien.DocTienBangChu(objSP.ThanhTienXD.Value, objSP.LoaiTien.DienGiai), SearchOptions.None);
            Document.ReplaceAll("[DienTichKV]", string.Format("{0:#,0.##} m2", objSP.DienTichKV), SearchOptions.None);
            Document.ReplaceAll("[DonGiaKV]", string.Format("{0:#,0.##} {1}", objSP.DonGiaKV, objSP.LoaiTien.DienGiai), SearchOptions.None);
            Document.ReplaceAll("[DonGiaKVBC]", objTien.DocTienBangChu(objSP.DonGiaKV.Value, objSP.LoaiTien.DienGiai), SearchOptions.None);
            Document.ReplaceAll("[ThanhTienKV]", string.Format("{0:#,0.##} {1}", objSP.ThanhTienKV, objSP.LoaiTien.DienGiai), SearchOptions.None);
            Document.ReplaceAll("[ThanhTienKVBC]", objTien.DocTienBangChu(objSP.ThanhTienKV.Value, objSP.LoaiTien.DienGiai), SearchOptions.None);
            Document.ReplaceAll("[ThanhTienHM]", string.Format("{0:#,0.##} {1}", objSP.ThanhTienHM, objSP.LoaiTien.DienGiai), SearchOptions.None);
            Document.ReplaceAll("[ThanhTienHMBC]", objTien.DocTienBangChu(objSP.ThanhTienHM.Value, objSP.LoaiTien.DienGiai), SearchOptions.None);
            Document.ReplaceAll("[ThanhTien]", string.Format("{0:#,0.##} {1}", objSP.ThanhTien, objSP.LoaiTien.DienGiai), SearchOptions.None);
            Document.ReplaceAll("[ThanhTienBC]", objTien.DocTienBangChu(objSP.ThanhTien.Value, objSP.LoaiTien.DienGiai), SearchOptions.None);
            //Hang muc
            for (int i = 1; i < 30; i++)
            {
                if (i <= objSP.bdsHangMucs.Count)
                {
                    bdsHangMuc objHM = objSP.bdsHangMucs[i - 1];
                    Document.ReplaceAll("[TenHM" + i + "]", objHM.TenDVT, SearchOptions.None);
                    Document.ReplaceAll("[SLHM" + i + "]", string.Format("{0:#,0.##}", objHM.SoLuong), SearchOptions.None);
                    Document.ReplaceAll("[DVTHM" + i + "]", objHM.TenDVT, SearchOptions.None);
                    Document.ReplaceAll("[DonGiaHM" + i + "]", string.Format("{0:#,0.##} {1}", objHM.DonGia, objSP.LoaiTien.DienGiai), SearchOptions.None);
                    Document.ReplaceAll("[DonGiaHM" + i + "BC]", objTien.DocTienBangChu(objHM.DonGia.Value, objSP.LoaiTien.DienGiai), SearchOptions.None);
                    Document.ReplaceAll("[ThanhTienHM" + i + "]", string.Format("{0:#,0.##} {1}", objHM.ThanhTien, objSP.LoaiTien.DienGiai), SearchOptions.None);
                    Document.ReplaceAll("[ThanhTienHM" + i + "BC]", objTien.DocTienBangChu(objHM.ThanhTien.Value, objSP.LoaiTien.DienGiai), SearchOptions.None);
                }
                else
                {
                    Document.ReplaceAll("[TenHM" + i + "]", "", SearchOptions.None);
                    Document.ReplaceAll("[SLHM" + i + "]", "", SearchOptions.None);
                    Document.ReplaceAll("[DVTHM" + i + "]", "", SearchOptions.None);
                    Document.ReplaceAll("[DonGiaHM" + i + "]", "", SearchOptions.None);
                    Document.ReplaceAll("[DonGiaHM" + i + "BC]", "", SearchOptions.None);
                    Document.ReplaceAll("[ThanhTienHM" + i + "]", "", SearchOptions.None);
                    Document.ReplaceAll("[ThanhTienHM" + i + "BC]", "", SearchOptions.None);
                }
            }
            //Lich thanh toan
            for (int i = 1; i < 30; i++)
            {
                if (i <= objGC.pgcLichThanhToans.Count)
                {
                    pgcLichThanhToan objLTT = objGC.pgcLichThanhToans[i - 1];
                    Document.ReplaceAll("[DotTT" + i + "]", objLTT.DotTT.ToString(), SearchOptions.None);
                    Document.ReplaceAll("[NgayTT" + i + "]", objLTT.NgayTT.Value.ToString("dd/MM/yyyy"), SearchOptions.None);
                    Document.ReplaceAll("[TyLeTT" + i + "]", string.Format("{0:#,0.##} %", objLTT.TyLeTT), SearchOptions.None);
                    Document.ReplaceAll("[TuongUngTT" + i + "]", string.Format("{0:#,0.##} {1}", objLTT.TuongUng, objSP.LoaiTien.DienGiai), SearchOptions.None);
                    Document.ReplaceAll("[TyLeVATTT" + i + "]", string.Format("{0:#,0.##} %", objLTT.TyLeVAT), SearchOptions.None);
                    Document.ReplaceAll("[ThueVATTT" + i + "]", string.Format("{0:#,0.##} {1}", objLTT.ThueVAT, objSP.LoaiTien.DienGiai), SearchOptions.None);
                    Document.ReplaceAll("[SoTienTT" + i + "]", string.Format("{0:#,0.##} {1}", objLTT.SoTien, objSP.LoaiTien.DienGiai), SearchOptions.None);
                    Document.ReplaceAll("[DienGiaiTT" + i + "]", objLTT.DienGiai.ToString(), SearchOptions.None);
                    Document.ReplaceAll("[TuongUngTT" + i + "BC]", objTien.DocTienBangChu(objLTT.TuongUng.Value, objSP.LoaiTien.DienGiai), SearchOptions.None);
                    Document.ReplaceAll("[ThueVATTT" + i + "BC]", objTien.DocTienBangChu(objLTT.ThueVAT.Value, objSP.LoaiTien.DienGiai), SearchOptions.None);
                    Document.ReplaceAll("[SoTienTT" + i + "BC]", objTien.DocTienBangChu(objLTT.SoTien.Value, objSP.LoaiTien.DienGiai), SearchOptions.None);
                }
                else
                {
                    Document.ReplaceAll("[DotTT" + i + "]", "", SearchOptions.None);
                    Document.ReplaceAll("[NgayTT" + i + "]", "", SearchOptions.None);
                    Document.ReplaceAll("[TyLeTT" + i + "]", "", SearchOptions.None);
                    Document.ReplaceAll("[TuongUngTT" + i + "]", "", SearchOptions.None);
                    Document.ReplaceAll("[TyLeVATTT" + i + "]", "", SearchOptions.None);
                    Document.ReplaceAll("[ThueVATTT" + i + "]", "", SearchOptions.None);
                    Document.ReplaceAll("[SoTienTT" + i + "]", "", SearchOptions.None);
                    Document.ReplaceAll("[DienGiaiTT" + i + "]", "", SearchOptions.None);
                    Document.ReplaceAll("[TuongUngTT" + i + "BC]", "", SearchOptions.None);
                    Document.ReplaceAll("[ThueVATTT" + i + "BC]", "", SearchOptions.None);
                    Document.ReplaceAll("[SoTienTT" + i + "BC]", "", SearchOptions.None);
                }
            }
            //Khach hang
            var objKH = objGC.KhachHang;
            Document.ReplaceAll("[HoTenBM]", objKH.HoKH + " " + objKH.TenKH, SearchOptions.None);
            if (objKH.IsYear.GetValueOrDefault())
                Document.ReplaceAll("[NgaySinhBM]", string.Format("{0:yyyy}", objKH.NgaySinh), SearchOptions.None);
            else
                Document.ReplaceAll("[NgaySinhBM]", string.Format("{0:dd/MM/yyyy}", objKH.NgaySinh), SearchOptions.None);
            Document.ReplaceAll("[CMNDBM]", objKH.SoCMND, SearchOptions.None);
            Document.ReplaceAll("[NgayCapBM]", string.Format("{0:dd/MM/yyyy}", objKH.NoiCap), SearchOptions.None);
            Document.ReplaceAll("[NoiCapBM]", objKH.NoiCap, SearchOptions.None);
            Document.ReplaceAll("[DCLLBM]", objKH.DiaChi, SearchOptions.None);
            Document.ReplaceAll("[DCTTBM]", objKH.ThuongTru, SearchOptions.None);
            Document.ReplaceAll("[DTCDBM]", objKH.DTCD, SearchOptions.None);
            Document.ReplaceAll("[DTDDBM]", objKH.DiDong, SearchOptions.None);
            Document.ReplaceAll("[EmailBM]", objKH.Email, SearchOptions.None);
            Document.ReplaceAll("[MaSoTTNCNBM]", objKH.MaSoTTNCN, SearchOptions.None);
            Document.ReplaceAll("[ChucVuBM]", objKH.ChucVu, SearchOptions.None);
            Document.ReplaceAll("[TenCtyBM]", objKH.TenCongTy, SearchOptions.None);
            Document.ReplaceAll("[DiaChiCtyBM]", objKH.DiaChiCT, SearchOptions.None);
            Document.ReplaceAll("[DTCTBM]", objKH.DienThoaiCT, SearchOptions.None);
            Document.ReplaceAll("[FaxCtyBM]", objKH.FaxCT, SearchOptions.None);
            Document.ReplaceAll("[MaSoThueCtyBM]", objKH.MaSoThueCT, SearchOptions.None);
        }
    }
}
