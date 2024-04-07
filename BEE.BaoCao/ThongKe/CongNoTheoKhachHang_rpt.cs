using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.ThongKe
{
    public partial class CongNoTheoKhachHang_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public CongNoTheoKhachHang_rpt(DateTime Date, string MaDA, string BlockID, string MaTang, string MaNKH)
        {
            InitializeComponent();
            lblDate.Text = string.Format("Tính đến thời điểm ngày {0:dd} tháng {0:MM} năm {0:yyyy}.", Date);
            khachHang_getAllShowTableAdapter.Fill(phanTichTuoiNo_src1.KhachHang_getAllShow);
            nguoiDaiDienTableAdapter.Fill(phanTichTuoiNo_src1.NguoiDaiDien);
            nguoiMoiGioiTableAdapter.Fill(phanTichTuoiNo_src1.NguoiMoiGioi);
            khachHang_CongNo_rptTableAdapter.Fill(phanTichTuoiNo_src1.KhachHang_CongNo_rpt, Date, MaDA, BlockID, MaTang, MaNKH);
        }
    }
}
