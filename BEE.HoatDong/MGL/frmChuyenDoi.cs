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

namespace BEE.HoatDong.MGL
{
    public partial class frmChuyenDoi : DevExpress.XtraEditors.XtraForm
    {

        public bool isSave { get; set; }
        public string NoiDung { get; set; }
        public byte MaLoaiTruoc { get; set; }
        public byte MaLoai { get; set; }
        public string TieuDe { get; set; }
        public DateTime NgayXL { get; set; }
        public byte? MaPT { get; set; }
        public frmChuyenDoi()
        {
            InitializeComponent();
        }

        private void frmDuyet_Load(object sender, EventArgs e)
        {
            this.NoiDung = txtLyDo.Text;
            dateNgayXL.EditValue = DateTime.Now;
            using (var db = new MasterDataContext())
            {
                var lstTT = db.mglTrangThaiGiaoDiches.OrderBy(p => p.Ord);
                lookTrangthaiHT.Properties.DataSource = lstTT;
                lookTrangThai.Properties.DataSource = lstTT;
                lookPhuongThuc.Properties.DataSource = db.PhuongThucXuLies;

                lookTrangthaiHT.EditValue = this.MaLoaiTruoc;
                lookTrangThai.EditValue = this.MaLoai;
            }
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lookTrangThai.Text))
            {
                DialogBox.Warning("Vui lòng chọn trạng thái");
                lookTrangThai.Focus();
                return;
            }
            this.isSave = true;
            this.NoiDung = txtLyDo.Text;
            this.MaLoaiTruoc = (byte)lookTrangthaiHT.EditValue;
            this.MaLoai = (byte)lookTrangThai.EditValue;
            this.TieuDe = txtTieuDe.Text;
            this.MaPT = (byte?)lookPhuongThuc.EditValue;
            this.NgayXL = (DateTime)dateNgayXL.EditValue;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}