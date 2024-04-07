using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.Threading;
using BEEREMA;

namespace BEE.HoatDong.KyGui
{
    class PhieuKyGuiCls : DevExpress.XtraRichEdit.RichEditControl
    {
        public void Print(int MaPKG)
        {
            var wait = DialogBox.WaitingForm();
            
            try
            {
                DataRow r = it.CommonCls.Row("pkgPhieuKyGui_rpt " + MaPKG);
                this.RtfText = r["Template"].ToString();
                Document.ReplaceAll("[TenDA]", r["TenDA"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[TenThuongMai]", r["TenThuongMai"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DiaChiDA]", r["DiaChi"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DienTich]", r["DTSD"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[HoTenKH]", r["HoKH"].ToString() + " " + r["TenKH"].ToString(), SearchOptions.None);
                if (bool.Parse(r["IsYear"].ToString()))
                    Document.ReplaceAll("[NgaySinh]", string.Format("{0:yyyy}", DateTime.Parse(r["NgaySinh"].ToString())), SearchOptions.None);
                else
                    Document.ReplaceAll("[NgaySinh]", string.Format("{0:dd/MM/yyyy}", DateTime.Parse(r["NgaySinh"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[GhiChu]", r["YeuCau"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[CMND]", r["SoCMND"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DCTT]", r["ThuongTru"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DCLL]", r["DiaChiKH"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[Tang]", r["TenTangNha"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[Block]", r["BlockName"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[MaBDS]", r["MaSo"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DTSD]", r["DTSD"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DTKH]", r["DTCD"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DTKH]", r["DTCD"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[Ngay]", string.Format("{0:dd}", DateTime.Parse(r["NgayKy"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[Thang]", string.Format("{0:MM}", DateTime.Parse(r["NgayKy"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[Nam]", string.Format("{0:yyyy}", DateTime.Parse(r["NgayKy"].ToString())), SearchOptions.None);
            }
            catch { }
            this.Print();

            wait.Close(); wait.Dispose();
        }

        public string ExportRtf(int MaPKG)
        {
            var wait = DialogBox.WaitingForm();
            
            try
            {
                DataRow r = it.CommonCls.Row("pkgPhieuKyGui_rpt " + MaPKG);
                this.RtfText = r["Template"].ToString();
                Document.ReplaceAll("[TenDA]", r["TenDA"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[TenThuongMai]", r["TenThuongMai"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DiaChiDA]", r["DiaChi"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DienTich]", r["DTSD"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[HoTenKH]", r["HoKH"].ToString() + " " + r["TenKH"].ToString(), SearchOptions.None);
                if (bool.Parse(r["IsYear"].ToString()))
                    Document.ReplaceAll("[NgaySinh]", string.Format("{0:yyyy}", DateTime.Parse(r["NgaySinh"].ToString())), SearchOptions.None);
                else
                    Document.ReplaceAll("[NgaySinh]", string.Format("{0:dd/MM/yyyy}", DateTime.Parse(r["NgaySinh"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[GhiChu]", r["YeuCau"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[CMND]", r["SoCMND"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DCTT]", r["ThuongTru"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DCLL]", r["DiaChiKH"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[Tang]", r["TenTangNha"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[Block]", r["BlockName"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[MaBDS]", r["MaSo"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DTSD]", r["DTSD"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DTKH]", r["DTCD"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DTKH]", r["DTCD"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[Ngay]", string.Format("{0:dd}", DateTime.Parse(r["NgayKy"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[Thang]", string.Format("{0:MM}", DateTime.Parse(r["NgayKy"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[Nam]", string.Format("{0:yyyy}", DateTime.Parse(r["NgayKy"].ToString())), SearchOptions.None);
            }
            catch { }

            wait.Close(); wait.Dispose();

            return this.RtfText;
        }

        public void ExportTo(int MaPKG, string fileName)
        {
            var wait = DialogBox.WaitingForm();
            
            try
            {
                DataRow r = it.CommonCls.Row("pkgPhieuKyGui_rpt " + MaPKG);
                this.RtfText = r["Template"].ToString();
                Document.ReplaceAll("[TenDA]", r["TenDA"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[TenThuongMai]", r["TenThuongMai"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DiaChiDA]", r["DiaChi"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DienTich]", r["DTSD"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[HoTenKH]", r["HoKH"].ToString() + " " + r["TenKH"].ToString(), SearchOptions.None);
                if (bool.Parse(r["IsYear"].ToString()))
                    Document.ReplaceAll("[NgaySinh]", string.Format("{0:yyyy}", DateTime.Parse(r["NgaySinh"].ToString())), SearchOptions.None);
                else
                    Document.ReplaceAll("[NgaySinh]", string.Format("{0:dd/MM/yyyy}", DateTime.Parse(r["NgaySinh"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[GhiChu]", r["YeuCau"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[CMND]", r["SoCMND"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DCTT]", r["ThuongTru"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DCLL]", r["DiaChiKH"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[Tang]", r["TenTangNha"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[Block]", r["BlockName"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[MaBDS]", r["MaSo"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DTSD]", r["DTSD"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DTKH]", r["DTCD"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DTKH]", r["DTCD"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[Ngay]", string.Format("{0:dd}", DateTime.Parse(r["NgayKy"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[Thang]", string.Format("{0:MM}", DateTime.Parse(r["NgayKy"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[Nam]", string.Format("{0:yyyy}", DateTime.Parse(r["NgayKy"].ToString())), SearchOptions.None);
            }
            catch { }
            this.SaveDocument(fileName, DocumentFormat.Rtf);

            wait.Close(); wait.Dispose();
        }
    }
}
