using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.BDS
{
    public partial class PhieuGiuChoByBDS_prt : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieuGiuChoByBDS_prt(string MaBDS, string MaSo)
        {
            InitializeComponent();

            pgcPhieuGiuCho_rptByMaBDSTableAdapter.Fill(phieuGiuChoByBDS_src1.pgcPhieuGiuCho_rptByMaBDS, MaBDS);
            lblBDS.Text = "Mã số: " + MaSo;
        }
    }
}
