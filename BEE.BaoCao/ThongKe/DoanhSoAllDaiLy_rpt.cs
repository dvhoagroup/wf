using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.ThongKe
{
    public partial class DoanhSoAllDaiLy_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public DoanhSoAllDaiLy_rpt()
        {
            InitializeComponent();
            daiLy_DoanhSo_rptTableAdapter.Fill(danhSoDaiLy_src1.DaiLy_DoanhSo_rpt);
        }
    }
}
