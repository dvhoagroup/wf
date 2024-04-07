using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.ThongKe.CTHDQT
{
    public partial class KhacHangChuaDongTien_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public KhacHangChuaDongTien_rpt(DateTime ThoiGian, int BlockID, string BlockName)
        {
            InitializeComponent();
            khachHang_ChuaDongTienTableAdapter.Fill(congNo_src1.KhachHang_ChuaDongTien, ThoiGian, BlockID);
            lblNgayLapBieu.Text = string.Format("TP HCM, ngày {0:dd} tháng {1:MM} năm {2:yyyy}", DateTime.Now, DateTime.Now, DateTime.Now);
            lblBlockName.Text = "BLOCK " + BlockName;
            lblTongThuDen.Text = string.Format("Tổng thu đến {0:dd/MM/yyyy}", ThoiGian);
            lblPhatSinhThang.Text = string.Format("Phát sinh Tháng {0:MM/yyyy}", ThoiGian);
        }
    }
}
