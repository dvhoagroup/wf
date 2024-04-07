using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.ThongKe.CTHDQT
{
    public partial class KeHoachThuNo_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public KeHoachThuNo_rpt(DateTime Date, string MaDA, string BlockID, string MaTang, string MaNKH)
        {
            InitializeComponent();
            lblDate.Text = string.Format("Tính đến thời điểm ngày {0:dd} tháng {0:MM} năm {0:yyyy}.", Date);
            khachHang_getAllShowTableAdapter.Fill(congNo_src1.KhachHang_getAllShow);
            nguoiDaiDienTableAdapter.Fill(congNo_src1.NguoiDaiDien);
            nguoiMoiGioiTableAdapter.Fill(congNo_src1.NguoiMoiGioi);
            khachHang_KeHoachThuNoTableAdapter.Fill(congNo_src1.KhachHang_KeHoachThuNo, Date, MaDA, BlockID, MaTang, MaNKH);
        }
    }
}
