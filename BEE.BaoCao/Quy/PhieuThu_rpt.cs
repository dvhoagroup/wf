using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.Quy
{
    public partial class PhieuThu_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieuThu_rpt(int MaPT, int MaLGD)
        {
            InitializeComponent();

            if (MaLGD == 1)
            {
                it.pgcPhieuThuCls o = new it.pgcPhieuThuCls(MaPT);
                lblBangChu.Text = it.ConvertMoney.ToString(o.SoTien * o.TyGia);
                lblBangChu2.Text = "Đã nhận đủ số tiền (viết bằng chữ): " + it.ConvertMoney.ToString(o.SoTien * o.TyGia);
                lblChungTu.Text = o.ChungTuGoc + "  chứng từ gốc";
                lblDiaChi.Text = o.DiaChi;
                lblLyDoNop.Text = o.DienGiai;
                lblNgayThu.Text = string.Format("Ngày: {0:dd/MM/yyyy}", o.NgayThu);
                lblNguoiNop.Text = o.NguoiNop;
                lblSoPT.Text = "Số chứng từ: " + o.SoPhieu;
                lblSoTien.Text = string.Format("{0:n0} đ", o.SoTien * o.TyGia);
                lblTKCo.Text = "TK có: " + o.TKCo.MaTK;
                lblTKNo.Text = "TK nợ: " + o.TKNo.MaTK;
            }
            else
            {
                it.hdgvPhieuThuCls o = new it.hdgvPhieuThuCls(MaPT);
                lblBangChu.Text = it.ConvertMoney.ToString(o.SoTien * o.TyGia);
                lblBangChu2.Text = "Đã nhận đủ số tiền (viết bằng chữ): " + it.ConvertMoney.ToString(o.SoTien * o.TyGia);
                lblChungTu.Text = o.ChungTuGoc + " Chứng từ gốc";
                lblDiaChi.Text = o.DiaChi;
                lblLyDoNop.Text = o.DienGiai;
                lblNgayThu.Text = string.Format("Ngày: {0:dd/MM/yyyy}", o.NgayThu);
                lblNguoiNop.Text = o.NguoiNop;
                lblSoPT.Text = "Số chứng từ: " + o.SoPhieu;
                lblSoTien.Text = string.Format("{0:n0} đ", o.SoTien * o.TyGia);
                lblTKCo.Text = "TK có: " + o.TKCo.MaTK;
                lblTKNo.Text = "TK nợ: " + o.TKNo.MaTK;
            }
        }
    }
}
