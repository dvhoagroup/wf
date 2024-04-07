using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.HDMB
{
    public partial class OuputList_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public OuputList_rpt(DateTime TuNgay, DateTime DenNgay)
        {
            InitializeComponent();
            if (TuNgay.CompareTo(DenNgay) == 0)
                lblThoiGian.Text = string.Format("Ngày {0:dd/MM/yyyy}", TuNgay);
            else
                lblThoiGian.Text = string.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", TuNgay, DenNgay);
            hopDongMuaBan_outputListTableAdapter.Fill(outputList_src1.HopDongMuaBan_outputList, TuNgay, DenNgay);
        }
    }
}
