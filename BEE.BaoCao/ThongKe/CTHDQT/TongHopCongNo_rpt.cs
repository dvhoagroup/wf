using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BEE.ThuVien;

namespace BEE.BaoCao.ThongKe.CTHDQT
{
    public partial class TongHopCongNo_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public TongHopCongNo_rpt(DateTime ThoiGian, int BlockID, string BlockName)
        {
            InitializeComponent();
            lblTongThuDen.Text = string.Format("Tổng thu đến {0:dd/MM/yyyy}", ThoiGian);
            lblPhatSinh.Text = string.Format("Phát sinh Tháng {0:MM/yyyy}", ThoiGian);
            lblTitle.Text = string.Format("BẢNG TỔNG HỢP CÔNG NỢ (Đến thời điểm {0:dd/MM/yyyy})", ThoiGian);
            lblBlockName.Text = "BLOCK " + BlockName;
            lblNguoiLap.Text = Common.StaffName;
            khachHang_TongHopCongNoTableAdapter.Fill(congNo_src1.KhachHang_TongHopCongNo, ThoiGian, BlockID);
        }
    }
}
