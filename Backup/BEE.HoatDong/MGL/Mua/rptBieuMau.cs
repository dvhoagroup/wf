using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.HoatDong.MGL.Mua
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

                var objBM = db.mglmtBieuMaus.Single(p => p.ID == ID);
                this.RtfText = objBM.NoiDung;

                var objMT = objBM.mglmtMuaThue;
                Document.ReplaceAll("[SoPhieu]", objMT.SoDK, SearchOptions.None);
                Document.ReplaceAll("[NgayKy]", objMT.NgayDK.Value.ToString("dd/MM/yyyy"), SearchOptions.None);
                Document.ReplaceAll("[Ngay]", objMT.NgayDK.Value.ToString("dd"), SearchOptions.None);
                Document.ReplaceAll("[Thang]", objMT.NgayDK.Value.ToString("MM"), SearchOptions.None);
                Document.ReplaceAll("[Nam]", objMT.NgayDK.Value.ToString("yyyy"), SearchOptions.None);
                Document.ReplaceAll("[ThoiHan]", objMT.ThoiHan.ToString(), SearchOptions.None);
                //Khach hang
                var objKH = objMT.KhachHang;
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
