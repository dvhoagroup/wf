using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class NgheNghiep_frm : DevExpress.XtraEditors.XtraForm
    {
        public byte KeyID = 0;
        public bool IsUpdate = false;
        public NgheNghiep_frm()
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
                it.NgheNghiepCls o = new it.NgheNghiepCls(KeyID);
                txtTenHuong.Text = o.TenNN;
            }
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên nghề nghiệp. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }
            it.NgheNghiepCls o = new it.NgheNghiepCls();
            o.STT = 1;
            o.TenNN = txtTenHuong.Text;

            if (KeyID != 0)
            {
                o.MaNN = KeyID;
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