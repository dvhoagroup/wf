using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BEE.ThuVien;

namespace BEE.BaoCao.ThongKe.CTHDQT
{
    public partial class PhaiTraKhachHang_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public PhaiTraKhachHang_rpt(int BlockID, string BlockName)
        {
            InitializeComponent();
            lblNguoiLap.Text = Common.StaffName;
            lblBlockName.Text = "BLOCK " + BlockName;
            khachHang_PhaiTraLaiTableAdapter.Fill(congNo_src1.KhachHang_PhaiTraLai, BlockID);
        }
    }
}
