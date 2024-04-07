using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class ThoiDiemLienLac_frm : DevExpress.XtraEditors.XtraForm
    {
        public byte KeyID = 0;
        public bool IsUpdate = false;
        public ThoiDiemLienLac_frm()
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
                it.ThoiDiemLienHeCls o = new it.ThoiDiemLienHeCls(KeyID);
                txtTenHuong.Text = o.TenTDLH;
            }
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên thời điểm liên lạc. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }

            it.ThoiDiemLienHeCls o = new it.ThoiDiemLienHeCls();
            o.STT = 1;
            o.TenTDLH = txtTenHuong.Text;

            if (KeyID != 0)
            {
                o.MaTDLH = KeyID;
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