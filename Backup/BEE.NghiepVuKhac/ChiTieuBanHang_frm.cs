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
    public partial class ChiTieuBanHang_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaCT = 0;
        public bool IsUpdate = false;
        public ChiTieuBanHang_frm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenChiTieu.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên chỉ tiêu bán hàng. Xin cảm ơn");
                txtTenChiTieu.Focus();
                return;
            }

            if (lookUpDuAn.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn dự án. Xin cảm ơn");
                lookUpDuAn.Focus();
                return;
            }

            it.ChiTieuBanHangCls o = new it.ChiTieuBanHangCls();
            o.TenCT = txtTenChiTieu.Text;
            o.DuAn.MaDA = int.Parse(lookUpDuAn.EditValue.ToString());
            if (MaCT != 0)
            {
                o.MaCT = MaCT;
                o.Update();
            }
            else
                o.Insert();
            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }

        private void ChiTieuBanHang_frm_Load(object sender, EventArgs e)
        {
            it.DuAnCls o = new it.DuAnCls();
            lookUpDuAn.Properties.DataSource = o.SelectShow();

            if (MaCT != 0)
            {
                it.ChiTieuBanHangCls objCT = new it.ChiTieuBanHangCls(MaCT);
                txtTenChiTieu.Text = objCT.TenCT;
                lookUpDuAn.EditValue = objCT.DuAn.MaDA;
            }
        }
    }
}