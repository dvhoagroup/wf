using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.HoatDong.MGL.Ban
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

                mglbcBieuMau objBM = db.mglbcBieuMaus.Single(p => p.ID == ID);
                this.RtfText = objBM.NoiDung;

                mglbcBanChoThue objBC = objBM.mglbcBanChoThue;
                Document.ReplaceAll("[SoPhieu]", objBC.SoDK, SearchOptions.None);
                Document.ReplaceAll("[NgayKy]", objBC.NgayDK.Value.ToString("dd/MM/yyyy"), SearchOptions.None);
                Document.ReplaceAll("[Ngay]", objBC.NgayDK.Value.ToString("dd"), SearchOptions.None);
                Document.ReplaceAll("[Thang]", objBC.NgayDK.Value.ToString("MM"), SearchOptions.None);
                Document.ReplaceAll("[Nam]", objBC.NgayDK.Value.ToString("yyyy"), SearchOptions.None);
                Document.ReplaceAll("[ThoiHan]", objBC.ThoiHan.ToString(), SearchOptions.None);
                //Bat dong san
                Document.ReplaceAll("[KyHieu]", objBC.KyHieu, SearchOptions.None);
                Document.ReplaceAll("[Tang]", objBC.SoTang.ToString(), SearchOptions.None);
                Document.ReplaceAll("[DienTichXD]", string.Format("{0:#,0.##} m2", objBC.DienTich), SearchOptions.None);
                Document.ReplaceAll("[DonGiaXD]", string.Format("{0:#,0.##} {1}", objBC.DonGia, objBC.LoaiTien.TenLoaiTien), SearchOptions.None);
                Document.ReplaceAll("[DonGiaXDBC]", objTien.DocTienBangChu(objBC.DonGia.Value, objBC.LoaiTien.DienGiai), SearchOptions.None);
                Document.ReplaceAll("[ThanhTienXD]", string.Format("{0:#,0.##} {1}", objBC.ThanhTien, objBC.LoaiTien.TenLoaiTien), SearchOptions.None);
                Document.ReplaceAll("[ThanhTienXDBC]", objTien.DocTienBangChu(objBC.ThanhTien.Value, objBC.LoaiTien.DienGiai), SearchOptions.None);
                //Khach hang
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
