using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.THUVIEN;using BEE;using BEE.DULIEU;
using BEEREM;

namespace BEE.SoQuy.PhieuChiBanHang
{
    public partial class frmEdit : BForm
    {
        public int? MaPC { get; set; }
        public int? MaPGC { get; set; }
        public byte? DotTT { get; set; }
        public decimal? SoTien { get; set; }

        MasterDataContext db = new MasterDataContext();
        pgcPhieuChi objPC;

        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateUserControl(this);

            var listTaiKhoan = db.TaiKhoans;
            lookUpTKCo.Properties.DataSource = listTaiKhoan;
            lookUpTKNo.Properties.DataSource = listTaiKhoan;

            lookUpLoaiChi.Properties.DataSource = db.pgcLoaiPhieuThuChis.Where(p => !p.IsPaid.GetValueOrDefault());
            lookUpCompany.Properties.DataSource = db.Companies;

            if (this.MaPC != null)
            {
                objPC = db.pgcPhieuChis.Single(p => p.MaPC == this.MaPC);
                txtSoPhieu.EditValue = objPC.SoPhieu;
                dateNgayChi.EditValue = objPC.NgayChi;
                lookUpTKNo.EditValue = objPC.TKNo;
                lookUpTKCo.EditValue = objPC.TKCo;
                spinThucThu.EditValue = objPC.SoTien;
                txtNguoiNhan.EditValue = objPC.NguoiNhan;
                txtSoCMND.Text = objPC.SoCMND;
                dateNgayCap.EditValue = objPC.NgayCap;
                txtNoiCap.Text = objPC.NoiCap;
                txtDiaChi.EditValue = objPC.DiaChi;
                txtDienGiai.EditValue = objPC.DienGiai;
                txtChungTu.EditValue = objPC.ChungTuGoc;
                spinDotTT.EditValue = objPC.DotTT;

                lookUpCompany.EditValue = objPC.MaCT;
                lookUpLoaiChi.EditValue = objPC.MaLoai;

                txtCodeSUN.Text = objPC.pgcPhieuGiuCho.KhachHang.CodeSUN;
            }
            else
            {
                objPC = new pgcPhieuChi();
                objPC.pgcPhieuGiuCho = db.pgcPhieuGiuChos.Single(p => p.MaPGC == this.MaPGC);

                string soPhieu = "";
                db.pgcPhieuChi_TaoSoPhieu(ref soPhieu);
                txtSoPhieu.EditValue = soPhieu;
                dateNgayChi.EditValue = DateTime.Now;

                pgcPhieuGiuCho objPGC = objPC.pgcPhieuGiuCho;
                txtNguoiNhan.EditValue = objPGC.KhachHang.IsPersonal.Value ? objPGC.KhachHang.TenKH : objPGC.KhachHang.TenCongTy;
                txtSoCMND.Text = objPGC.KhachHang.SoCMND;
                dateNgayCap.EditValue = objPGC.KhachHang.NgayCap;
                txtNoiCap.Text = objPGC.KhachHang.NoiCap;
                txtDiaChi.EditValue = objPGC.KhachHang.DiaChi;
                txtCodeSUN.Text = objPGC.KhachHang.CodeSUN;
                spinThucThu.EditValue = this.SoTien;
                spinDotTT.EditValue = DotTT;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtSoPhieu.Text.Trim() == "")
            {
                BEE.DialogBox.Error("Vui lòng nhập số phiếu");
                txtSoPhieu.Focus();
                return;
            }
            else
            {
                var count = db.pgcPhieuChis.Where(p => p.SoPhieu == txtSoPhieu.Text && p.MaPC != this.MaPC).Count();
                if (count > 0)
                {
                    BEE.DialogBox.Error("Trùng số phiếu, vui lòng nhập lại");
                    txtSoPhieu.Focus();
                    return;
                }
            }

            if (dateNgayChi.Text == "")
            {
                BEE.DialogBox.Error("Vui lòng nhập ngày chi");
                dateNgayChi.Focus();
                return;
            }

            if (spinThucThu.Value <= 0)
            {
                BEE.DialogBox.Error("Vui lòng nhập số tiền phải chi");
                return;
            }

            if (txtNguoiNhan.Text.Trim() == "")
            {
                BEE.DialogBox.Error("Vui lòng nhập người nộp");
                txtNguoiNhan.Focus();
                return;
            }

            objPC.SoPhieu = txtSoPhieu.Text;
            objPC.NgayChi = dateNgayChi.DateTime;
            objPC.TKCo = lookUpTKCo.Text;
            objPC.TKNo = lookUpTKNo.Text;
            objPC.SoTien = spinThucThu.Value;
            objPC.DotTT = Convert.ToByte(spinDotTT.EditValue);

            objPC.NguoiNhan = txtNguoiNhan.Text;
            objPC.SoCMND = txtSoCMND.Text;
            objPC.NoiCap = txtNoiCap.Text;
            if (dateNgayCap.DateTime.Year != 1)
                objPC.NgayCap = dateNgayCap.DateTime;
            objPC.DiaChi = txtDiaChi.Text;
            objPC.DienGiai = txtDienGiai.Text;
            objPC.ChungTuGoc = txtChungTu.Text;

            objPC.MaNV = BEE.THUVIEN.Common.MaNV;

            if (this.MaPC == null)
            {
                db.pgcPhieuChis.InsertOnSubmit(objPC);
            }

            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}