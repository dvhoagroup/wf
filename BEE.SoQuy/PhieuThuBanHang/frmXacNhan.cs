using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEE.SoQuy.PhieuThuBanHang
{
    public partial class frmXacNhan : DevExpress.XtraEditors.XtraForm
    {
        public string DienGiai = "";
        public frmXacNhan()
        {
            InitializeComponent();
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
           this.DienGiai = txtDienGiai.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}