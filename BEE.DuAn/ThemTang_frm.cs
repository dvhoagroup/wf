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
    public partial class ThemTang_frm : DevExpress.XtraEditors.XtraForm
    {
        public bool IsUpdate = false;
        public int BlockID = 0;
        public int MaTang = 0;
        public ThemTang_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTangNha.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên sàn/tầng. Xin cảm ơn");
                txtTangNha.Focus();
                return;
            }
            try
            {
                it.TangNhaCls o = new it.TangNhaCls();                
                o.TenTangNha = txtTangNha.Text;
                o.DienGiai = txtDienGiai.Text;
                o.Blocks.BlockID = BlockID;
                if (MaTang == 0)
                    o.Insert();
                else
                {
                    o.MaTangNha = MaTang;
                    o.Update();
                }
            }
            catch
            {
                DialogBox.Infomation("Sàn/Tầng này đã có trong hệ thống, Vui lòng kiểm tra lại, xin cảm ơn");
                return;
            }

            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }

        private void ThemTang_frm_Load(object sender, EventArgs e)
        {
            if (MaTang != 0)
            {
                it.TangNhaCls o = new it.TangNhaCls(MaTang);
                txtDienGiai.Text = o.DienGiai;
                txtTangNha.Text = o.TenTangNha;
            }
        }
    }
}