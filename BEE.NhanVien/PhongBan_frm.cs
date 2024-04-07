using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.NhanVien
{
    public partial class PhongBan_frm : DevExpress.XtraEditors.XtraForm
    {
        public byte KeyID = 0;
        public bool IsUpdate = false;
        public PhongBan_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Huong_frm_Load(object sender, EventArgs e)
        {
            if (KeyID != 0)
            {
                it.PhongBanCls o = new it.PhongBanCls(KeyID);
                txtTenHuong.Text = o.TenPB;
                ckCongTacVien.Checked = o.isAgent;
            }
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên phòng ban. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }
            it.PhongBanCls o = new it.PhongBanCls();
            o.STT = 1;
            o.isAgent = ckCongTacVien.Checked;
            o.TenPB = txtTenHuong.Text;
            if (KeyID != 0)
            {
                o.MaPB = KeyID;
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