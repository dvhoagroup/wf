using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.ThongKe.CTHDQT
{
    public partial class HDGVDaRaHDMB_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public HDGVDaRaHDMB_rpt()
        {
            InitializeComponent();
            hdGopVon_daRaHDMBTableAdapter.Fill(congNo_src1.hdGopVon_daRaHDMB);
        }
    }
}
