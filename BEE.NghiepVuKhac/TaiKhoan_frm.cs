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
    public partial class TaiKhoan_frm : DevExpress.XtraEditors.XtraForm
    {
        public string KeyID = "";
        public bool IsUpdate = false, Edit = false;
        public TaiKhoan_frm()
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
            if (KeyID != "")
            {
                it.TaiKhoanCls o = new it.TaiKhoanCls(KeyID);
                txtMaSo.Text = o.MaTK;
                txtTenHuong.Text = o.TenTK;
            }
            txtMaSo.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtMaSo.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập mã số tài khoản. Xin cảm ơn");
                txtMaSo.Focus();
                return;
            }

            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên tài khoản. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }
            it.TaiKhoanCls o = new it.TaiKhoanCls();
            o.TenTK = txtTenHuong.Text;
            o.MaTK = txtMaSo.Text;
            try
            {
                if (Edit)
                    o.Update();
                else
                    o.Insert();

                IsUpdate = true;
                DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
                this.Close();
            }
            catch
            {
                DialogBox.Infomation("Mã số này đã tồn tại trong hệ thống, vui lòng kiểm tra lại, xin cảm ơn.");
            }            
        }
    }
}