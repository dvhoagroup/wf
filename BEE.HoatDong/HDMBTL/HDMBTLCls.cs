using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.Threading;

namespace LandSoft.HDMBTL
{
    class HDMBCls : DevExpress.XtraRichEdit.RichEditControl
    {
        public void Print(int MaHDMB)
        {
            var wait = DialogBox.WaitingForm();
            
            //DataRow rowTem = it.CommonCls.Row("BieuMau_get 3");
            //this.RtfText = rowTem["NoiDung"].ToString();

            DataRow r = it.CommonCls.Row("HopDongMuaBan_rpt " + MaHDMB);
            this.RtfText = r["Template"].ToString();
            Document.ReplaceAll("[TenDA]", r["TenDA"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[TenThuongMai]", r["TenThuongMai"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[DiaChiDA]", r["DiaChi"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[DienTich]", r["DTSD"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[HoTenKH]", r["HoTenKH"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[NgaySinh]", string.Format("{0:dd/MM/yyyy}", DateTime.Parse(r["NgaySinh"].ToString())), SearchOptions.None);
            Document.ReplaceAll("[LoaiCH]", r["LoaiCH"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[ViTri]", r["ViTri"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[Email]", r["Email"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[DonGia]", string.Format("{0:n0}", double.Parse(r["DonGia"].ToString())), SearchOptions.None);
            Document.ReplaceAll("[ThanhTien]", string.Format("{0:n0}", double.Parse(r["ThanhTien"].ToString())), SearchOptions.None);
            Document.ReplaceAll("[GiaTriText]", it.ConvertMoney.toString(decimal.Parse(r["ThanhTien"].ToString())), SearchOptions.None);
            Document.ReplaceAll("[MaSoThue]", r["MaSoTTNCN"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[CMND]", r["SoCMND"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[DCTT]", r["ThuongTru"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[DCLL]", r["DiaChiKH"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[Tang]", r["TenTangNha"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[Block]", r["BlockName"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[MaBDS]", r["MaSo"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[DTSD]", r["DTSD"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[DTKH]", r["DTCD"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[DTKH]", r["DTCD"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[DDKH]", r["DiDong"].ToString(), SearchOptions.None);
            Document.ReplaceAll("[Ngay]", string.Format("{0:dd}", DateTime.Parse(r["NgayKy"].ToString())), SearchOptions.None);
            Document.ReplaceAll("[Thang]", string.Format("{0:MM}", DateTime.Parse(r["NgayKy"].ToString())), SearchOptions.None);
            Document.ReplaceAll("[Nam]", string.Format("{0:yyyy}", DateTime.Parse(r["NgayKy"].ToString())), SearchOptions.None);

            wait.Close(); wait.Dispose();

            this.ShowPrintPreview();           
        }
    }
}
