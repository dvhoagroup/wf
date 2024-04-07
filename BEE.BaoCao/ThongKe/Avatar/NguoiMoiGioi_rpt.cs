using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.ThongKe.Avatar
{
    public partial class NguoiMoiGioi_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public NguoiMoiGioi_rpt(string MaDA, DateTime TuNgay, DateTime DenNgay)
        {
            InitializeComponent();
            nguoiMoiGioi_rptTableAdapter.Fill(nguoiMoiGioi_src1.NguoiMoiGioi_rpt, TuNgay, DenNgay, MaDA);
            lblDate.Text = string.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", TuNgay, DenNgay);
            //lblDuAn.Text = "Dự án: " + TenDA;
        }
    }
}
