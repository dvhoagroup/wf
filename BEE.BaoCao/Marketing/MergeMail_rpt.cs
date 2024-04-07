using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.Marketing
{
    public partial class MergeMail_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public MergeMail_rpt(string KhachHang, byte MaThiep, string TenThiep)
        {
            InitializeComponent();
            khachHang_MergeMailTableAdapter.Fill(mergeMail_src1.KhachHang_MergeMail, MaThiep, KhachHang);
            lblTenThiep.Text = TenThiep.ToUpper();
        }
    }
}
