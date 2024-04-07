using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.Quy
{
    public partial class PhieuChi_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieuChi_rpt(int MaPC)
        {
            InitializeComponent();

            it.pgcPhieuChiCls o = new it.pgcPhieuChiCls(MaPC);
            lblBangChu.Text = it.ConvertMoney.ToString(o.SoTien);
            lblBangChu2.Text = "Đã nhận đủ số tiền (viết bằng chữ): " + it.ConvertMoney.ToString(o.SoTien);
            lblChungTu.Text = o.ChungTuGoc + "   Chứng từ gốc";
            lblDiaChi.Text = o.DiaChi;
            lblLyDoNop.Text = o.DienGiai;
            lblNgayThu.Text = string.Format("Ngày {0:dd} tháng {0:MM} năm {0:yyyy}", o.NgayChi);
            lblNguoiNop.Text = o.NguoiNhan;
            lblSoPT.Text = "Số phiếu: " + o.SoPhieu;
            lblSoTien.Text = string.Format("{0:n0} đ", o.SoTien);
            lblTKCo.Text = "Ghi Có: " + o.TKCo.MaTK;
            lblTKNo.Text = "Ghi Nợ: " + o.TKNo.MaTK;
        }
    }
}
