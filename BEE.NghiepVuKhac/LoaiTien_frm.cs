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
    public partial class LoaiTien_frm : DevExpress.XtraEditors.XtraForm
    {
        public byte KeyID = 0;
        public bool IsUpdate = false;
        public LoaiTien_frm()
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
                it.LoaiTienCls o = new it.LoaiTienCls(KeyID);
                txtTenHuong.Text = o.TenLoaiTien;
                txtDienGiai.Text = o.DienGiai;
                spinTyGia.EditValue = o.TyGia;
            }
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên loại tiền tệ. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }
            it.LoaiTienCls o = new it.LoaiTienCls();
            o.TenLoaiTien = txtTenHuong.Text;
            o.TyGia = double.Parse(spinTyGia.EditValue.ToString());
            o.DienGiai = txtDienGiai.Text;
            if (KeyID != 0)
            {
                o.MaLoaiTien = KeyID;
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