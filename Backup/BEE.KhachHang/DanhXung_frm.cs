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
    public partial class DanhXung_frm : DevExpress.XtraEditors.XtraForm
    {
        public byte KeyID = 0;
        public bool IsUpdate = false;
        public DanhXung_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Huong_frm_Load(object sender, EventArgs e)
        {
            BEE.NgonNgu.Language.TranslateControl(this);

            if (KeyID != 0)
            {
                it.QuyDanhCls o = new it.QuyDanhCls(KeyID);
                txtTenHuong.Text = o.TenQD;
            }
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên quý danh. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }

            it.QuyDanhCls o = new it.QuyDanhCls();
            o.STT = 1;
            o.TenQD = txtTenHuong.Text;

            if (KeyID != 0)
            {
                o.MaQD = KeyID;
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