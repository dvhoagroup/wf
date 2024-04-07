using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.Marketing
{
    public partial class SubMail_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public SubMail_rpt(byte MaThiep, string MaKH)
        {
            InitializeComponent();
            khachHang_MergeMailTableAdapter.Fill(mergeMail_src1.KhachHang_MergeMail, MaThiep, MaKH);
        }
    }
}
