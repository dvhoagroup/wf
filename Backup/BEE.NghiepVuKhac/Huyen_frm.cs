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
    public partial class Huyen_frm : DevExpress.XtraEditors.XtraForm
    {
        public short KeyID = 0;
        public bool IsUpdate = false;
        public byte MaTinh = 0;
        public Huyen_frm()
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
            it.TinhCls objTinh = new it.TinhCls();
            lookUpTinh.Properties.DataSource = objTinh.Select();
            lookUpTinh.ItemIndex = 0;
            if (KeyID != 0)
            {
                it.HuyenCls o = new it.HuyenCls(KeyID);
                txtTenHuong.Text = o.TenHuyen;
                lookUpTinh.EditValue = o.Tinh.MaTinh;
            }
            else
            {
                lookUpTinh.EditValue = MaTinh;
            }
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên huyện. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }
            it.HuyenCls o = new it.HuyenCls();
            o.TenHuyen = txtTenHuong.Text;
            o.Tinh.MaTinh = byte.Parse(lookUpTinh.EditValue.ToString());
            if (KeyID != 0)
            {
                o.MaHuyen = KeyID;
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