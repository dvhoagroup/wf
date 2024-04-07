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
    public partial class AddKhu_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaKhu = 0;
        public int MaDA = 0;
        public bool IsUpdate = false;
        public AddKhu_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadData()
        {
            it.KhuCls o = new it.KhuCls(MaKhu);
            txtTenKhu.Text = o.TenKhu;
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenKhu.Text.Trim() == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên khu. Xin cảm ơn.");
                txtTenKhu.Focus();
                return;
            }

            it.KhuCls o = new it.KhuCls();
            o.TenKhu = txtTenKhu.Text;
            o.MaDA = MaDA;

            if (MaKhu != 0)
            {
                o.MaKhu = MaKhu;
                o.Update();
            }
            else
                o.Insert();

            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã được cập nhật.");
            this.Close();
        }

        private void AddKhu_frm_Load(object sender, EventArgs e)
        {
            if (MaKhu != 0)
                LoadData();
        }
    }
}