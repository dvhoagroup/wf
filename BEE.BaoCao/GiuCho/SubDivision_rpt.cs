using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.GiuCho
{
    public partial class SubDivision_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public SubDivision_rpt()
        {
            InitializeComponent();
            pdcPhieuDatCoc_subDivisionTableAdapter.Fill(outputList_src1.pdcPhieuDatCoc_subDivision);
        }
    }
}
