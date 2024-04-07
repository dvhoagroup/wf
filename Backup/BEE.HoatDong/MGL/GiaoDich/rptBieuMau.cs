using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.HoatDong.MGL.GiaoDich
{
    public class rptBieuMau : DevExpress.XtraRichEdit.RichEditControl
    {
        public rptBieuMau(int ID)
        {
            var wait = DialogBox.WaitingForm();
            MasterDataContext db = new MasterDataContext();
            try
            {
                var objTien = new it.TienTeCls();

                var objBM = db.mglgdBieuMaus.Single(p => p.ID == ID);
                this.RtfText = objBM.NoiDung;

                var objGD = objBM.mglgdGiaoDich;
                Document.ReplaceAll("[SoPhieu]", objGD.SoGD, SearchOptions.None);
                Document.ReplaceAll("[NgayKy]", objGD.NgayGD.Value.ToString("dd/MM/yyyy"), SearchOptions.None);
                Document.ReplaceAll("[Ngay]", objGD.NgayGD.Value.ToString("dd"), SearchOptions.None);
                Document.ReplaceAll("[Thang]", objGD.NgayGD.Value.ToString("MM"), SearchOptions.None);
                Document.ReplaceAll("[Nam]", objGD.NgayGD.Value.ToString("yyyy"), SearchOptions.None);
                Document.ReplaceAll("[ThoiHan]", objGD.ThoiHan.ToString(), SearchOptions.None);
                Document.ReplaceAll("[TienCoc]", string.Format("{0:#,0.##} {1}", objGD.TienCoc,
                        objGD.mglbcBanChoThue.LoaiTien.TenLoaiTien), SearchOptions.None);
                Document.ReplaceAll("[TienCocBC]", objTien.DocTienBangChu(objGD.TienCoc.Value,
                    objGD.mglbcBanChoThue.LoaiTien.DienGiai), SearchOptions.None);
                //Bat dong san
                var objBC = objGD.mglbcBanChoThue;
                Document.ReplaceAll("[KyHieu]", objBC.KyHieu, SearchOptions.None);
                Document.ReplaceAll("[Tang]", objBC.SoTang.ToString(), SearchOptions.None);
                Document.ReplaceAll("[DienTichXD]", string.Format("{0:#,0.##} m2", objBC.DienTich), SearchOptions.None);
                Document.ReplaceAll("[DonGiaXD]", string.Format("{0:#,0.##} {1}", objBC.DonGia, objBC.LoaiTien.TenLoaiTien), SearchOptions.None);
                Document.ReplaceAll("[DonGiaXDBC]", objTien.DocTienBangChu(objBC.DonGia.Value, objBC.LoaiTien.DienGiai), SearchOptions.None);
                Document.ReplaceAll("[ThanhTienXD]", string.Format("{0:#,0.##} {1}", objBC.ThanhTien, objBC.LoaiTien.TenLoaiTien), SearchOptions.None);
                Document.ReplaceAll("[ThanhTienXDBC]", objTien.DocTienBangChu(objBC.ThanhTien.Value, objBC.LoaiTien.DienGiai), SearchOptions.None);
                //Ben ban
                var objKH = objBC.KhachHang;
                Document.ReplaceAll("[HoTenBB]", objKH.HoKH + " " + objKH.TenKH, SearchOptions.None);
                if (objKH.IsYear.GetValueOrDefault())
                    Document.ReplaceAll("[NgaySinhBB]", string.Format("{0:yyyy}", objKH.NgaySinh), SearchOptions.None);
                else
                    Document.ReplaceAll("[NgaySinhBB]", string.Format("{0:dd/MM/yyyy}", objKH.NgaySinh), SearchOptions.None);
                Document.ReplaceAll("[CMNDBB]", objKH.SoCMND, SearchOptions.None);
                Document.ReplaceAll("[NgayCapBB]", string.Format("{0:dd/MM/yyyy}", objKH.NoiCap), SearchOptions.None);
                Document.ReplaceAll("[NoiCapBB]", objKH.NoiCap, SearchOptions.None);
                Document.ReplaceAll("[DCLLBB]", objKH.DiaChi, SearchOptions.None);
                Document.ReplaceAll("[DCTTBB]", objKH.ThuongTru, SearchOptions.None);
                Document.ReplaceAll("[DTCDBB]", objKH.DTCD, SearchOptions.None);
                Document.ReplaceAll("[DTDDBB]", objKH.DiDong, SearchOptions.None);
                Document.ReplaceAll("[EmailBB]", objKH.Email, SearchOptions.None);
                Document.ReplaceAll("[MaSoTTNCNBB]", objKH.MaSoTTNCN, SearchOptions.None);
                Document.ReplaceAll("[ChucVuBB]", objKH.ChucVu, SearchOptions.None);
                Document.ReplaceAll("[TenCtyBB]", objKH.TenCongTy, SearchOptions.None);
                Document.ReplaceAll("[DiaChiCtyBB]", objKH.DiaChiCT, SearchOptions.None);
                Document.ReplaceAll("[DTCTBB]", objKH.DienThoaiCT, SearchOptions.None);
                Document.ReplaceAll("[FaxCtyBB]", objKH.FaxCT, SearchOptions.None);
                Document.ReplaceAll("[MaSoThueCtyBB]", objKH.MaSoThueCT, SearchOptions.None);
                //Ben mua
                var objMT = objGD.mglmtMuaThue.KhachHang;
                Document.ReplaceAll("[HoTenBM]", objMT.HoKH + " " + objMT.TenKH, SearchOptions.None);
                if (objMT.IsYear.GetValueOrDefault())
                    Document.ReplaceAll("[NgaySinhBM]", string.Format("{0:yyyy}", objMT.NgaySinh), SearchOptions.None);
                else
                    Document.ReplaceAll("[NgaySinhBM]", string.Format("{0:dd/MM/yyyy}", objMT.NgaySinh), SearchOptions.None);
                Document.ReplaceAll("[CMNDBM]", objMT.SoCMND, SearchOptions.None);
                Document.ReplaceAll("[NgayCapBM]", string.Format("{0:dd/MM/yyyy}", objMT.NoiCap), SearchOptions.None);
                Document.ReplaceAll("[NoiCapBM]", objMT.NoiCap, SearchOptions.None);
                Document.ReplaceAll("[DCLLBM]", objMT.DiaChi, SearchOptions.None);
                Document.ReplaceAll("[DCTTBM]", objMT.ThuongTru, SearchOptions.None);
                Document.ReplaceAll("[DTCDBM]", objMT.DTCD, SearchOptions.None);
                Document.ReplaceAll("[DTDDBM]", objMT.DiDong, SearchOptions.None);
                Document.ReplaceAll("[EmailBM]", objMT.Email, SearchOptions.None);
                Document.ReplaceAll("[MaSoTTNCNBM]", objMT.MaSoTTNCN, SearchOptions.None);
                Document.ReplaceAll("[ChucVuBM]", objMT.ChucVu, SearchOptions.None);
                Document.ReplaceAll("[TenCtyBM]", objMT.TenCongTy, SearchOptions.None);
                Document.ReplaceAll("[DiaChiCtyBM]", objMT.DiaChiCT, SearchOptions.None);
                Document.ReplaceAll("[DTCTBM]", objMT.DienThoaiCT, SearchOptions.None);
                Document.ReplaceAll("[FaxCtyBM]", objMT.FaxCT, SearchOptions.None);
                Document.ReplaceAll("[MaSoThueCtyBM]", objMT.MaSoThueCT, SearchOptions.None);
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
                wait.Close();
            }
        }
    }
}
