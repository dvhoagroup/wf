using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.ThongKe
{
    public partial class DoanhSoDaiLy_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public DoanhSoDaiLy_rpt(int MaDL)
        {
            InitializeComponent();
            daiLy_DoanhSoByMaDL_rptTableAdapter.Fill(danhSoDaiLy_src1.DaiLy_DoanhSoByMaDL_rpt, MaDL);
        }
    }
}
