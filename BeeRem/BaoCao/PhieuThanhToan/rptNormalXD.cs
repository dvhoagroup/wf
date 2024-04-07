using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BEE.ThuVien;
using System.Linq;

namespace BEEREMA.BaoCao.PhieuThanhToan
{
    public partial class rptNormalXD : DevExpress.XtraReports.UI.XtraReport
    {
        public rptNormalXD(int ID)
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
                    cellDonGia.Text = cellDonGia2.Text = string.Format("", obj.pgcPhieuGiuCho.bdsSanPham.DonGiaXD);
                    cellGhiChu.Text = obj.DienGiai;
                    cellHoTen.Text = obj.NguoiNop;
                    cellLaiMuon.Text = string.Format("{0:#,0.#}", obj.LaiSuat);
                    cellLoaiTien.Text = "VND";
                    cellMaO.Text = cellMaO2.Text = obj.pgcPhieuGiuCho.bdsSanPham.KyHieu;
                    cellPhaiNop.Text = string.Format("{0:#,0.#}", PhaiNop);
                    cellSoTien.Text = string.Format("{0:#,0.#}", obj.TuongUngTT);
                    cellThanhTien.Text = string.Format("{0:#,0.#}", obj.pgcPhieuGiuCho.bdsSanPham.ThanhTienXD);
                    cellThucNop.Text = string.Format("{0:#,0.#}", obj.ThucNop);
                    cellTongGiaTri.Text = string.Format("{0:#,0.#}", obj.pgcPhieuGiuCho.bdsSanPham.ThanhTienXD);
                    cellTyLe.Text = string.Format("{0:#,0.#} %", obj.TyLeTT);
                    cellVAT.Text = string.Format("{0:#,0.#}", obj.ThueVAT);
                }
            }
            catch { }
        }
    }
}