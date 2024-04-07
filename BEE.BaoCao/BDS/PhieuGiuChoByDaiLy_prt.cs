using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.BDS
{
    public partial class PhieuGiuChoByDaiLy_prt : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieuGiuChoByDaiLy_prt(string MaBDS, string MaSo)
        {
            InitializeComponent();
            daiLy_getAllShowTableAdapter.Fill(phieuGiuChoByBDS_src1.DaiLy_getAllShow);
            pgcPhieuGiuCho_rptByMaBDSTableAdapter.Fill(phieuGiuChoByBDS_src1.pgcPhieuGiuCho_rptByMaBDS, MaBDS);
            lblBDS.Text = "Mã số: " + MaSo;
        }
    }
}
