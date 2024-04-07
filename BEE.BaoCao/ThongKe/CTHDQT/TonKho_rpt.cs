using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.ThongKe.CTHDQT
{
    public partial class TonKho_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public TonKho_rpt(int MaDA, DateTime ThoiGian)
        {
            InitializeComponent();
            BEE_BatDongSan_ConTonTableAdapter.Fill(congNo_src1._BEE_BatDongSan_ConTon, MaDA);
            lblChiTietCongNo.Text = string.Format("DANH SÁCH CĂN HỘ CÒN TỒN (Tại thời điểm {0:dd/MM/yyyy})", ThoiGian);
            lblLapBieu.Text = string.Format("TP HCM, ngày {0:dd} tháng {0:MM} năm {0:yyyy}", DateTime.Now);
        }
    }
}
