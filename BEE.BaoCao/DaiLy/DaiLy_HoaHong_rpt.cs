using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.DaiLy
{
    public partial class DaiLy_HoaHong_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public DaiLy_HoaHong_rpt(int MaDA, DateTime TuNgay, DateTime DenNgay, string TenDA, string MaDL, byte IsQuy)
        {
            InitializeComponent();
            daiLy_HoaHong_rptTableAdapter.Fill(daiLy_HoaHong_src1.DaiLy_HoaHong_rpt, TuNgay, DenNgay, MaDA, MaDL, IsQuy);
            lblDate.Text = string.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", TuNgay, DenNgay);
            lblDuAn.Text = "Dự án: " + TenDA;
        }
    }
}
