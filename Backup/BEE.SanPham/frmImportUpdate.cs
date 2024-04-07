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
    public partial class frmImportUpdate : DevExpress.XtraEditors.XtraForm
    {
        public bdsSanPham objSP = new bdsSanPham();

        public frmImportUpdate()
        {
            InitializeComponent();
        }

        private void frmImportUpdate_Load(object sender, EventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                lookDuAn.Properties.DataSource = db.DuAns.Select(p => new { p.MaDA, p.TenDA });
                lookNVKD.Properties.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen });
                lookLoaiTien.Properties.DataSource = db.LoaiTiens;
                lookLoaiBDS.Properties.DataSource = db.LoaiBDs;
                lookHuong.Properties.DataSource = db.PhuongHuongs;
                lookPhapLy.Properties.DataSource = db.PhapLies;
                cmbTienIch.Properties.DataSource = db.TienIches;
                lookLoaiDuong.Properties.DataSource = db.LoaiDuongs;
            }
            gcHangMuc.DataSource = objSP.bdsHangMucs;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (lookDuAn.Text == "")
            {
                DialogBox.Error("Vui lòng chọn [Dự án], xin cảm ơn.");
                lookDuAn.Focus();
                return;
            }

            if (lookLoaiBDS.Text == "")
            {
                DialogBox.Error("Vui lòng chọn [Loại bất động sản], xin cảm ơn.");
                lookLoaiBDS.Focus();
                return;
            }

            objSP.MaLBDS = (short?)lookLoaiBDS.EditValue;
            objSP.MaDA = (int?)lookDuAn.EditValue;
            objSP.MaHuong = (short?)lookHuong.EditValue;
            objSP.MaPL = (short?)lookPhapLy.EditValue;
            objSP.MaLD = (short?)lookLoaiDuong.EditValue;
            objSP.ThanhTienHM = objSP.bdsHangMucs.Sum(p => p.ThanhTien).GetValueOrDefault();
            objSP.MaLT = (byte?)lookLoaiTien.EditValue;
            objSP.MaNVKD = (int?)lookNVKD.EditValue;
            objSP.MaTT = (byte?)rdbTrangThai.EditValue;
            objSP.DuongRong = spinDuongRong.Value;
            objSP.NgangKV = spinNgangKV.Value;
            objSP.DaiKV = spinDaiKV.Value;
            objSP.SauKV = spinNoHauKV.Value;
            objSP.NgangXD = spinNgangXD.Value;
            objSP.DaiXD = spinDaiXD.Value;
            objSP.SauXD = spinNoHauXD.Value;
            //Tien ich
            objSP.bdsTienIches.Clear();
            string[] ts = cmbTienIch.Properties.GetCheckedItems().ToString().Split(',');
            if (ts[0] != "")
            {
                foreach (var i in ts)
                {
                    bdsTienIch objTI = new bdsTienIch();
                    objTI.MaTI = short.Parse(i);
                    objSP.bdsTienIches.Add(objTI);
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void spinSoLuognHM_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit s = (SpinEdit)sender;
            decimal? donGia = (decimal?)grvHangMuc.GetFocusedRowCellValue("DonGia");
            grvHangMuc.SetFocusedRowCellValue("ThanhTien", s.Value * donGia.GetValueOrDefault());
        }

        private void spinDonGiaHM_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit s = (SpinEdit)sender;
            decimal? sonLuong = (decimal?)grvHangMuc.GetFocusedRowCellValue("SoLuong");
            grvHangMuc.SetFocusedRowCellValue("ThanhTien", s.Value * sonLuong.GetValueOrDefault());
        }

        private void grvHangMuc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                grvHangMuc.DeleteSelectedRows();
            }
        }
    }
}