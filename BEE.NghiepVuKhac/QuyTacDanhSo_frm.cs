using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.NghiepVuKhac
{
    public partial class QuyTacDanhSo_frm : DevExpress.XtraEditors.XtraForm
    {
        public byte KeyID = 0;
        public bool IsUpdate = false;
        public QuyTacDanhSo_frm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Huong_frm_Load(object sender, EventArgs e)
        {
            if (KeyID != 0)
            {
                it.QuyTacDanhSoCls o = new it.QuyTacDanhSoCls(KeyID);
                txtTenHuong.Text = o.KyHieu;
                txtDienGiai.Text = o.DienGiai;
            }
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập ký hiệu. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }
            it.QuyTacDanhSoCls o = new it.QuyTacDanhSoCls();
            o.KyHieu = txtTenHuong.Text;
            o.DienGiai = txtDienGiai.Text;
            if (KeyID != 0)
            {
                o.MaQT = KeyID;
                o.Update();
            }
            else
                o.Insert();

            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }
    }
}