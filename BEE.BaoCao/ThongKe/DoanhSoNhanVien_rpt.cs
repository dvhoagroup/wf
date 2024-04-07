using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.BaoCao.ThongKe
{
    public partial class DoanhSoNhanVien_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public DoanhSoNhanVien_rpt(DateTime TuNgay, DateTime DenNgay, string MaNV)
        {
            InitializeComponent();
            nHanVien_DoanhSo_rptTableAdapter.Fill(danhSoDaiLy_src1.NHanVien_DoanhSo_rpt, TuNgay, DenNgay, MaNV);
        }
    }
}
