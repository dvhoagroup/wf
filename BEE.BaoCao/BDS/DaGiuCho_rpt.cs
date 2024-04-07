using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.BDS
{
    public partial class DaGiuCho_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public DaGiuCho_rpt(string MaDA, string MaKhu)
        {
            InitializeComponent();
            BEE_BatDongSan_DaGiuCho_rptTableAdapter.Fill(danhSachLo_src1._BEE_BatDongSan_DaGiuCho_rpt, MaDA, MaKhu);
        }
    }
}
