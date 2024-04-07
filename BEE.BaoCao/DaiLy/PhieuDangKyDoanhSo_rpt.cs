using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace BEE.BaoCao.DaiLy
{
    public partial class PhieuDangKyDoanhSo_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieuDangKyDoanhSo_rpt(int MaDL, int MaKKD, int MaDA)
        {
            InitializeComponent();

            it.DaiLyCls objDL = new it.DaiLyCls(MaDL);
            lblDaiLy.Text = objDL.TenDL;
            lblDienThoai.Text = objDL.DienThoaiNLH;
            lblMaSo.Text = objDL.MaSo;
            lblNguoiLienHe.Text = objDL.NguoiLienHe;

            daiLy_DangKy_rptTableAdapter.Fill(daiLy_DangKy_src1.DaiLy_DangKy_rpt, MaDL, MaKKD);
        }
    }
}
