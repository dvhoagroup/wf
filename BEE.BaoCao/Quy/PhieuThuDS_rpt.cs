using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BEE.ThuVien;

namespace BEE.BaoCao.Quy
{
    public partial class PhieuThuDS_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieuThuDS_rpt(DateTime TuNgay, DateTime DenNgay, string MaDA, string BlockID, string MaTang)
        {
            InitializeComponent();
            phieuThuDSTableAdapter.Fill(quy_src1.PhieuThuDS, TuNgay, DenNgay, MaDA, BlockID, MaTang);
            lblDate.Text = string.Format("Ngày {0:dd/MM/yyyy}", TuNgay);
            lblNguoiLap.Text = Common.StaffName;
        }
    }
}
