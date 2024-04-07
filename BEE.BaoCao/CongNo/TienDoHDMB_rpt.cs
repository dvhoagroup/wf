using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.CongNo
{
    public partial class TienDoHDMB_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public TienDoHDMB_rpt(int BlockID, string BlockName, string DuAn)
        {
            InitializeComponent();
            hdmb_TienDoTableAdapter.Fill(congNo_src1.hdmb_TienDo, BlockID);
            lblBlock.Text = "BLOCK " + BlockName;
            lblDuAn.Text = "DỰ ÁN " + DuAn.ToUpper();
        }
    }
}
