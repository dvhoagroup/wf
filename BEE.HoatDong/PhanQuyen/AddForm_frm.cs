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
    public partial class AddForm_frm : DevExpress.XtraEditors.XtraForm
    {
        public int KeyID = 0;
        public byte ModulID = 0;
        public bool IsUpdate = false;
        public AddForm_frm()
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
                it.FormsCls o = new it.FormsCls(KeyID);
                txtTenHuong.Text = o.FormName;
                txtDienGiai.Text = o.Description;
            }
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên form. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }
            it.FormsCls o = new it.FormsCls();
            o.Description = txtDienGiai.Text;
            o.FormName = txtTenHuong.Text;
            o.Modul.ModulID = ModulID;
            
            if (KeyID != 0)
            {
                o.FormID = KeyID;
                o.Update();
            }
            else
                o.Insert();

            IsUpdate = true;
            //DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }
    }
}