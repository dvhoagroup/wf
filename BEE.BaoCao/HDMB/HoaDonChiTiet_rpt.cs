using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.HDMB
{
    public partial class HoaDonChiTiet_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public HoaDonChiTiet_rpt(int MaHD)
        {
            InitializeComponent();
            hdGTGTChiTiet_rptTableAdapter.Fill(hdmB_src1.hdGTGTChiTiet_rpt, MaHD);
        }

    }
}
