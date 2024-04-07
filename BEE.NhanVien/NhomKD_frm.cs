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
    public partial class NhomKD_frm : DevExpress.XtraEditors.XtraForm
    {
        public byte KeyID = 0;
        public bool IsUpdate = false;
        public NhomKD_frm()
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
                it.NhomKinhDoanhCls o = new it.NhomKinhDoanhCls(KeyID);
                txtTenHuong.Text = o.TenNKD;
                ckCongTacVien.Checked = (bool)o.isAgent;
            }
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên nhóm kinh doanh. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }
            it.NhomKinhDoanhCls o = new it.NhomKinhDoanhCls();
            o.TenNKD = txtTenHuong.Text;
            o.isAgent = ckCongTacVien.Checked;
            if (KeyID != 0)
            {
                o.MaNKD = KeyID;
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