using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.HDMB
{
    public partial class ExpirestList_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public ExpirestList_rpt()
        {
            InitializeComponent();
            hopDongMuaBan_expiresTableAdapter.Fill(outputList_src1.HopDongMuaBan_expires);
        }
    }
}
