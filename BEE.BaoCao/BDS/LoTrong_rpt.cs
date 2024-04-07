using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.BDS
{
    public partial class LoTrong_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public LoTrong_rpt(string MaDA, string MaKhu)
        {
            InitializeComponent();
            BEE_BatDongSan_ConTrong_rptTableAdapter.Fill(danhSachLo_src1._BEE_BatDongSan_ConTrong_rpt, MaDA, MaKhu);
        }
    }
}
