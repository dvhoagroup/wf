using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandSoft.NghiepVu.HDGopVon
{
    public partial class Duyet_frm : DevExpress.XtraEditors.XtraForm
    {
        public bool IsUpdate = false;
        public int MaHDGV = 0;
        public byte MaTT = 1;
        public Duyet_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            it.hdgvQuaTrinhThucHienCls o = new it.hdgvQuaTrinhThucHienCls();
            o.MaHDGV = MaHDGV;
            o.DienGiai = txtDienGiai.Text;
            o.NhanVien.MaNV = LandSoft.Library.Common.StaffID;
            o.TinhTrang.MaTT = MaTT;
            o.Insert();
            IsUpdate = true;
            this.Close();
        }
    }
}