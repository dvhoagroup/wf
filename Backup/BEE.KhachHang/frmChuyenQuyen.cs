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

namespace BEE.KhachHang
{
    public partial class frmChuyenQuyen : DevExpress.XtraEditors.XtraForm
    {
        public int? MaNV { get; set; }
        public string DienGiai { get; set; }

        public frmChuyenQuyen()
        {
            InitializeComponent();
        }

        private void ChuyenQuyen_ctl_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                lookNhanVien.Properties.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.MaSo, p.HoTen }).ToList();
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.MaNV = (int?)lookNhanVien.EditValue;
            this.DienGiai = txtLyDo.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}