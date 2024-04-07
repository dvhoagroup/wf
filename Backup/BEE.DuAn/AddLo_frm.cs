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
    public partial class AddLo_frm : DevExpress.XtraEditors.XtraForm
    {
        public int BlockID = 0, MaLo = 0;
        public bool IsUpdate = false;

        public AddLo_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            it.LoCls o = new it.LoCls();
            o.Blocks.BlockID = BlockID;
            o.TenLo = txtTenLo.Text;

            if (o.Check())
            {
                DialogBox.Infomation("Block này đã có lô <" + txtTenLo.Text + ">. Vui lòng kiểm tra lại, xin cảm ơn");
                txtTenLo.Focus();
                return;
            }

            if (MaLo != 0)
            {
                o.MaLo = MaLo;
                o.Update();
            }
            else
                o.Insert();

            IsUpdate = true;
            //DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }

        private void AddLo_frm_Load(object sender, EventArgs e)
        {
            if (MaLo != 0)
            {
                it.LoCls o = new it.LoCls(MaLo);
                txtTenLo.Text = o.TenLo;
            }
        }
    }
}