using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.DuAn
{
    public partial class AddTemplate_frm : DevExpress.XtraEditors.XtraForm
    {
        public byte KeyID = 0;
        public string TenDA = "";
        public bool IsUpdate = false;
        public AddTemplate_frm()
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
                it.hdmbBieuMauCls o = new it.hdmbBieuMauCls(KeyID);
                txtTenHuong.Text = o.TenBM;
            }
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên biểu mẫu. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }
            it.hdmbBieuMauCls o = new it.hdmbBieuMauCls();
            o.TenBM = txtTenHuong.Text;
            if (KeyID != 0)
            {
                o.MaBM = KeyID;
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