using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace BEE.BaoCao.DaiLy
{
    public partial class PhieuDangDaiLy_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieuDangDaiLy_rpt(int MaDL, string TenDA)
        {
            InitializeComponent();

            it.DaiLyCls objDL = new it.DaiLyCls(MaDL);
            lblDaiLy.Text = objDL.TenDL;
            lblDienThoai.Text = objDL.DienThoaiNLH;
            lblNguoiDaiDien.Text = objDL.NguoiDaiDien;
            lblNguoiLienHe.Text = objDL.NguoiLienHe;
            lblChucVu.Text = objDL.ChucVu;
            lblDiaChi.Text = objDL.DiaChi;
            lblDienThoaiCT.Text = objDL.DienThoai;
            lblEmail.Text = objDL.Email;
            lblFax.Text = objDL.Fax;
            lblGiayPhep.Text = objDL.GiayPhepKD;
            lblMaSoThue.Text = objDL.MaSoThue;
            lblNgayCap.Text = string.Format("{0:dd/MM/yyyy}", objDL.NgayCap);
            lblWebsite.Text = objDL.Website;
            if (objDL.HinhThuc)
            {
                chkChinhThuc.Checked = true;
                chkKoChinhThuc.Checked = false;
            }
            else
            {
                chkKoChinhThuc.Checked = true;
                chkChinhThuc.Checked = false;
            }
            lblDuAn.Text = TenDA;
        }
    }
}
