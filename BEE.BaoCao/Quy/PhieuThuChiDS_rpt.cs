using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BEE.ThuVien;

namespace BEE.BaoCao.Quy
{
    public partial class PhieuThuChiDS_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieuThuChiDS_rpt(DateTime TuNgay, DateTime DenNgay, string MaDA, string BlockID, string MaTang)
        {
            InitializeComponent();
            phieuThuChiDSTableAdapter.Fill(quy_src1.PhieuThuChiDS, TuNgay, DenNgay, MaDA, BlockID, MaTang);
            lblDate.Text = string.Format("Ngày {0:dd/MM/yyyy}", TuNgay);
            lblNguoiLap.Text = Common.StaffName;
        }
    }
}
