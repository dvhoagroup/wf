using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.SanPham
{
    public partial class frmChuyenQuyen : DevExpress.XtraEditors.XtraForm
    {
        public int MaNV;
        public string LyDo;
        MasterDataContext db = new MasterDataContext();
        public frmChuyenQuyen()
        {
            InitializeComponent();
            lookNhanVien.Properties.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen, p.MaSo });
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            if (lookNhanVien.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn nhân viên nhận");
                return;
            }

            this.MaNV = (int)lookNhanVien.EditValue;
            this.LyDo = txtLyDo.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}