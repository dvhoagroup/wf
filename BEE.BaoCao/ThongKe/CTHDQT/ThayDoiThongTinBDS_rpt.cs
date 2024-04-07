using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.ThongKe.CTHDQT
{
    public partial class ThayDoiThongTinBDS_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public ThayDoiThongTinBDS_rpt(int BlockID, string BlockName)
        {
            InitializeComponent();
            bdsLichSuThayDoiDT_getAll2TableAdapter.Fill(congNo_src1.bdsLichSuThayDoiDT_getAll2, BlockID);
            lblBlockName.Text = "BLOCK " + BlockName;
        }
    }
}
