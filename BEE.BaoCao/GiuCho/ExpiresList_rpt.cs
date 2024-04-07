using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.GiuCho
{
    public partial class ExpiresList_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public ExpiresList_rpt()
        {
            InitializeComponent();
            pdcPhieuDatCoc_expires_rptTableAdapter.Fill(outputList_src1.pdcPhieuDatCoc_expires_rpt);

            lblThoiGian.Text = string.Format("Ngày {0:dd/MM/yyyy}", DateTime.Now);
        }
    }
}