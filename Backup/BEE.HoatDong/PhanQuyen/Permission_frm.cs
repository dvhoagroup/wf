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
    public partial class Permission_frm : DevExpress.XtraEditors.XtraForm
    {
        public int KeyID = 0;
        public bool IsUpdate = false;
        public Permission_frm()
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
                it.PermissionsCls o = new it.PermissionsCls(KeyID);
                txtTenHuong.Text = o.PerName;
                txtDienGiai.Text = o.Description;
                chkIsAgent.Checked = o.IsAgent;
            }
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên nhóm người dùng. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }
            it.PermissionsCls o = new it.PermissionsCls();
            o.Description = txtDienGiai.Text;
            o.PerName = txtTenHuong.Text;
            o.IsAgent = chkIsAgent.Checked;
            if (KeyID != 0)
            {
                o.PerID = KeyID;
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