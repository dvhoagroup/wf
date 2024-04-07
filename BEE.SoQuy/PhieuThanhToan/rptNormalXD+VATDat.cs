using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BEE.THUVIEN;using BEE;using BEE.DULIEU;
using System.Linq;

namespace BEE.SoQuy.PhieuThanhToan
{
    public partial class rptNormalXD_VATDat : DevExpress.XtraReports.UI.XtraReport
    {
        public rptNormalXD_VATDat(int ID)
        {
            InitializeComponent();

            try
            {
                using (var db = new MasterDataContext())
                {
                    var obj = db.pgcPhieuThanhToans.Single(p => p.MaPTT == ID);
                    lblDuAn.Text = obj.pgcPhieuGiuCho.bdsSanPham.DuAn.TenDA;
                    lblNgayTT.Text = string.Format("Ngày {0:dd} tháng {0:MM} năm {0:yyyy}", obj.NgayTT.Value);
                    decimal PhaiNop = obj.TuongUngTT.Value + obj.ThueVAT.Value;
                    cellConPhaiNop.Text = string.Format("{0:#,0.#}", PhaiNop - obj.DaNop.Value);
                    cellDaNop.Text = string.Format("{0:#,0.#}", obj.DaNop);
                    cellDiaChi.Text = obj.DiaChi;
                    cellDienTich.Text = cellDienTich2.Text = string.Format("{0:#,0.#}", obj.pgcPhieuGiuCho.bdsSanPham.DienTichXD);
                    cellDonGia.Text = cellDonGia2.Text = string.Format("{0:#,0.#}", obj.pgcPhieuGiuCho.bdsSanPham.DonGiaXD);
                    cellGhiChu.Text = "  " + obj.DienGiai;
                    cellHoTen.Text = lblNguoiNop.Text = obj.NguoiNop;
                    cellLaiMuon.Text = string.Format("{0:#,0.#}", obj.LaiSuat);
                    cellLoaiTien.Text = obj.MaLoaiTien == 1 ? "VND" : "USD";
                    cellMaO.Text = cellMaO2.Text = obj.pgcPhieuGiuCho.bdsSanPham.MaLo;
                    cellPhaiNop.Text = string.Format("{0:#,0.#}", PhaiNop);
                    cellSoTien.Text = string.Format("{0:#,0.#}", obj.TuongUngTT);
                    cellThanhTien.Text = string.Format("{0:#,0.#}", obj.pgcPhieuGiuCho.bdsSanPham.ThanhTienXD);
                    cellThucNop.Text = string.Format("{0:#,0.#}", obj.ThucNop);
                    cellTongGiaTri.Text = string.Format("{0:#,0.#}", obj.pgcPhieuGiuCho.bdsSanPham.ThanhTienXD);
                    cellTyLe.Text = string.Format("{0:#,0.#} %", obj.TyLeTT);
                    cellVAT.Text = string.Format("{0:#,0.#}", obj.ThueVAT);
                    lblPhaiNopBC.Text = "Bằng chữ: " + BEE.ConvertMoney.toString(obj.ThucNop ?? 0);
                    lblThanhToan.Text = obj.KhoanThanhToan;

                    lblPhongKeToan.Text = obj.PhongKeToan;
                    lblPhongKinhDoanh.Text = obj.PhongKinhDoanh;
                    lblThuQuy.Text = obj.ThuQuy;
                }
            }
            catch { }
        }
    }
}