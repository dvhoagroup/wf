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
    public partial class Blocks_frm : DevExpress.XtraEditors.XtraForm
    {
        public int KeyID = 0, MaDA = 0, MaPK = 0;
        public bool IsUpdate = false;
        public Blocks_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadDA()
        {
            it.DuAnCls o = new it.DuAnCls();
            lookUpDuAn.Properties.DataSource = o.SelectShow();
            if (MaDA != 0)
            {
                lookUpDuAn.EditValue = MaDA;
                lookUpDuAn.Enabled = false;
            }
        }

        private void Huong_frm_Load(object sender, EventArgs e)
        {
            LoadDA();

            if (KeyID != 0)
            {
                it.BlocksCls o = new it.BlocksCls(KeyID);
                txtTenHuong.Text = o.BlockName;
                lookUpDuAn.EditValue = o.DuAn.MaDA;
                txtDienGiai.Text = o.DienGiai;
                spinTienSDD.EditValue = o.TienSDD;
                spinThue.EditValue = o.Thue;
            }
            else
                if (MaDA != 0)
                    lookUpDuAn.EditValue = MaDA;
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên block. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }

            if (lookUpDuAn.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn dự án. Xin cảm ơn");
                lookUpDuAn.Focus();
                return;
            }

            try
            {
                it.BlocksCls o = new it.BlocksCls();                
                o.BlockName = txtTenHuong.Text;
                o.DienGiai = txtDienGiai.Text;
                o.DuAn.MaDA = int.Parse(lookUpDuAn.EditValue.ToString());
                o.Thue = byte.Parse(spinThue.EditValue.ToString());
                o.TienSDD = double.Parse(spinTienSDD.EditValue.ToString());
                o.PhanKhu.MaPK = MaPK;
                if (KeyID != 0)
                {
                    o.BlockID = KeyID;
                    o.Update();
                }
                else
                    o.Insert();
            }
            catch
            {
                DialogBox.Infomation("Block này đã có trong hệ thống, Vui lòng kiểm tra lại, xin cảm ơn");
                return;
            }

            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }
    }
}