using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.HoatDong.PhanQuyen
{
    public partial class Modules_frm : DevExpress.XtraEditors.XtraForm
    {
        public byte KeyID = 0;
        public bool IsUpdate = false;
        public Modules_frm()
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
                it.ModulesCls o = new it.ModulesCls(KeyID);
                txtTenHuong.Text = o.ModulName;
                txtDienGiai.Text = o.Description;
            }
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên module. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }
            it.ModulesCls o = new it.ModulesCls();
            o.Description = txtDienGiai.Text;
            o.ModulName = txtTenHuong.Text;
            if (KeyID != 0)
            {
                o.ModulID = KeyID;
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