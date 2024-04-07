using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandSoft.NghiepVu.GiaoDich
{
    public partial class Duyet_frm : DevExpress.XtraEditors.XtraForm
    {
        public bool IsUpdate = false;
        public int MaGD = 0;
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
            it.pdkgdQuaTrinhThucHienCls o = new it.pdkgdQuaTrinhThucHienCls();
            o.MaGD = MaGD;
            o.DienGiai = txtDienGiai.Text;
            o.NhanVien.MaNV = LandSoft.Library.Common.StaffID;
            o.TinhTrang.MaTT = MaTT;
            o.Insert();
            IsUpdate = true;
            this.Close();
        }
    }
}