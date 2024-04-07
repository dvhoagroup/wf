using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.BatDongSan
{
    public partial class LoaiBDS_frm : DevExpress.XtraEditors.XtraForm
    {
        public byte KeyID = 0;
        public bool IsUpdate = false;
        public LoaiBDS_frm()
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
                it.LoaiBDSCls o = new it.LoaiBDSCls(KeyID);
                txtTenHuong.Text = o.TenLBDS;
                txtCodeSun.Text = o.CodeSUN;
            }
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên loại bất động sản. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }
            it.LoaiBDSCls o = new it.LoaiBDSCls();
            o.TenLBDS = txtTenHuong.Text;
            o.CodeSUN = txtCodeSun.Text.Trim();
            if (KeyID != 0)
            {
                o.MaLBDS = KeyID;
                o.Update();
            }
            else
                o.Insert();
            
            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }
    }
}