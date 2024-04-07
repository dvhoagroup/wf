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
    public partial class ThemNhieuTang_frm : DevExpress.XtraEditors.XtraForm
    {
        public bool IsUpdate = false;
        public int BlockID = 0;
        public ThemNhieuTang_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (int.Parse(spinSoLuong.EditValue.ToString()) == 0)
            {
                DialogBox.Infomation("Vui lòng nhập số lượng tầng muốn thêm. Xin cảm ơn.");
                spinSoLuong.Focus();
                return;
            }

            it.TangNhaCls o = new it.TangNhaCls();
            o.Blocks.BlockID = BlockID;
            o.SoLuong = int.Parse(spinSoLuong.EditValue.ToString());
            o.InsertMultiRow();

            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }
    }
}