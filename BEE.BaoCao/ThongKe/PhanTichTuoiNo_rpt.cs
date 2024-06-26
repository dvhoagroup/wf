using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.ThongKe
{
    public partial class PhanTichTuoiNo_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public PhanTichTuoiNo_rpt(DateTime Date, string MaDA, string BlockID, string MaTang, string MaNKH)
        {
            InitializeComponent();
            lblDate.Text = string.Format("Tính đến thời điểm ngày {0} tháng {1} năm {2}.", Date.Day, Date.Month, Date.Year);
            khachHang_getAllShowTableAdapter.Fill(phanTichTuoiNo_src1.KhachHang_getAllShow);
            phanTichTuoiNo_rptTableAdapter.Fill(phanTichTuoiNo_src1.PhanTichTuoiNo_rpt, Date, MaDA, BlockID, MaTang, MaNKH);
        }
    }
}
