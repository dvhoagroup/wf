using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.GiuCho
{
    public partial class NotLoList_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public NotLoList_rpt()
        {
            InitializeComponent();
            pdcPhieuDatCoc_notLoTableAdapter.Fill(outputList_src1.pdcPhieuDatCoc_notLo);
        }
    }
}
