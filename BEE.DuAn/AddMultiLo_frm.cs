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
    public partial class AddMultiLo_frm : DevExpress.XtraEditors.XtraForm
    {
        public int BlockID = 0, MaLo = 0;
        public bool IsUpdate = false;

        public AddMultiLo_frm()
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
                DialogBox.Infomation("Vui lòng nhập số lượng lô muốn thêm. Xin cảm ơn.");
                spinSoLuong.Focus();
                return;
            }
            it.LoCls o = new it.LoCls();
            o.Blocks.BlockID = BlockID;

            o.InsertMulti(int.Parse(spinSoLuong.EditValue.ToString()));

            IsUpdate = true;
            //DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }
    }
}