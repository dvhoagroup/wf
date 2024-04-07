using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;

namespace LandSoft.NghiepVu.HDGopVon
{
    class ThanhLyCls : DevExpress.XtraRichEdit.RichEditControl
    {
        public void Print(int MaHDGV)
        {
            try
            {
                this.RtfText = it.CommonCls.Row("BieuMau_get 14")["NoiDung"].ToString();

                DataRow r = it.CommonCls.Row("hdgvThanhLy_rpt " + MaHDGV);
                //this.RtfText = r["Template"].ToString();
                Document.ReplaceAll("[SoHDGV]", r["SoPhieu"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[NgayGV]", string.Format("{0:dd}", DateTime.Parse(r["NgayKy"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[ThangGV]", string.Format("{0:MM}", DateTime.Parse(r["NgayKy"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[NamGV]", string.Format("{0:yyyy}", DateTime.Parse(r["NgayKy"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[Ngay]", string.Format("{0:dd}", DateTime.Parse(r["NgayTL"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[Thang]", string.Format("{0:MM}", DateTime.Parse(r["NgayTL"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[Nam]", string.Format("{0:yyyy}", DateTime.Parse(r["NgayTL"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[NgayDeNghi]", string.Format("{0:dd}", DateTime.Parse(r["NgayTL"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[ThangDeNghi]", string.Format("{0:MM}", DateTime.Parse(r["NgayTL"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[NamDeNghi]", string.Format("{0:yyyy}", DateTime.Parse(r["NgayTL"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[NguoiDeNghi]", r["NguoiDeNghi"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[NgayKy]", string.Format("{0:dd/MM/yyyy}", DateTime.Parse(r["NgayKy"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[GiaTriHopDong]", string.Format("{0:n0}", double.Parse(r["GiaTriHD"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[LaiSuat]", string.Format("{0:n2}", double.Parse(r["LaiSuat2"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[LoiNhuan]", string.Format("{0:n2}", double.Parse(r["LoiNhuan"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[SoTienDaGop]", string.Format("{0:n0}", double.Parse(r["SoTienGop"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[LaiSuatNganHang]", string.Format("{0:n0}", double.Parse(r["LaiSuat"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[ThueThuNhapCaNhan]", string.Format("{0:n0}", double.Parse(r["ThueTNCN"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[GiaTriHoanTra]", string.Format("{0:n0}", double.Parse(r["GiaTriHoanTra"].ToString())), SearchOptions.None);
                Document.ReplaceAll("[GiaTriHoanTraText]", it.ConvertMoney.ToString(double.Parse(r["GiaTriHoanTra"].ToString())), SearchOptions.None);

                Document.ReplaceAll("[HoTenKH]", r["HoTenKH"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[NgaySinh]", string.Format("{0:dd/MM/yyyy}", DateTime.Parse(r["NgaySinh"].ToString())), SearchOptions.None);               
                Document.ReplaceAll("[SoCMND]", r["SoCMND"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[NgayCap]", string.Format("{0:dd/MM/yyyy}", DateTime.Parse(r["NgayCap"].ToString())), SearchOptions.None);           
                Document.ReplaceAll("[NoiCap]", r["NoiCap"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DiaChiThuongTru]", r["DCTT"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DiaChiLienLac]", r["DCLH"].ToString(), SearchOptions.None);
                Document.ReplaceAll("[DienThoai]", r["DiDong"].ToString(), SearchOptions.None);                                
            }
            catch
            {
                RtfText = "";
            }
            this.ShowPrintPreview();
        }
    }
}
