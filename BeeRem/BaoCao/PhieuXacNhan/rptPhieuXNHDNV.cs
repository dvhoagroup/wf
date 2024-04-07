using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BEE.ThuVien;
using System.Linq;

namespace BEEREMA.BaoCao.PhieuXacNhan
{
    public partial class rptPhieuXNHDNV : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPhieuXNHDNV(int maPGC)
        {
            InitializeComponent();

            try
            {
                using (var db = new MasterDataContext())
                {
                    //var obj = db.pgcPhieuGiuChos.Single(p => p.MaPGC == maPGC);
                    //lblDuAn.Text = obj.bdsSanPham.DuAn.TenDA;
                    //lblNgayTT.Text = string.Format("Ngày {0:dd} tháng {0:MM} năm {0:yyyy}", DateTime.Now);
                    //decimal PhaiNop = obj.TuongUngTT.Value + obj.ThueVAT.Value + obj.VATDat.Value;
                    //cellConPhaiNop.Text = string.Format("{0:#,0.#}", PhaiNop - obj.DaNop.Value);
                    //cellDaNop.Text = string.Format("{0:#,0.#}", obj.DaNop);
                    //cellDiaChi.Text = obj.KhachHang.DCLL;
                    //cellDienTich.Text = cellDienTich2.Text = string.Format("{0:#,0.#}", obj.bdsSanPham.DienTichKV);
                    //cellDonGia.Text = cellDonGia2.Text = string.Format("{0:#,0.#}", obj.bdsSanPham.DonGiaKV);
                    //cellGhiChu.Text = "  " + obj.DienGiai;
                    //cellHoTen.Text = lblNguoiNop.Text = obj.NguoiNop;
                    //cellLaiMuon.Text = string.Format("{0:#,0.#}", obj.LaiSuat);
                    //cellLoaiTien.Text = obj.MaLoaiTien == 1 ? "VND" : "USD";
                    //cellMaO.Text = cellMaO2.Text = obj.bdsSanPham.MaLo;
                    //cellPhaiNop.Text = string.Format("{0:#,0.#}", PhaiNop);
                    //cellSoTien.Text = string.Format("{0:#,0.#}", obj.TuongUngTT);
                    //cellThanhTien.Text = string.Format("{0:#,0.#}", obj.bdsSanPham.ThanhTienKV);
                    //cellThucNop.Text = string.Format("{0:#,0.#}", obj.ThucNop);
                    //cellTyLe.Text = string.Format("{0:#,0.#} %", obj.TyLeTT); 

                    //lblPhongKinhDoanh.Text = obj.PhongKeToan;
                    //lblPhongKeToan.Text = obj.PhongKinhDoanh;
                }
            }
            catch { }
        }
    }
}