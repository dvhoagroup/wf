using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.CongNo
{
    public partial class TienDoHDGV_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public TienDoHDGV_rpt(int BlockID, string BlockName, string DuAn)
        {
            InitializeComponent();
            hdgv_TienDoTableAdapter.Fill(congNo_src1.hdgv_TienDo, BlockID);
            lblBlock.Text = "BLOCK " + BlockName;
            lblDuAn.Text = "DỰ ÁN " + DuAn.ToUpper();
        }
    }
}
