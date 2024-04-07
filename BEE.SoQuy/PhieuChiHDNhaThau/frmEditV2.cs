using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.THUVIEN;
using BEE;
using BEE.DULIEU;
using System.Linq;
using BEEREM;
namespace BEE.SoQuy.PhieuChiHDNhaThau
{
    public partial class frmEditV2 : BForm
    {
        public int? MaPC { get; set; }
        public int? ContractID { get; set; }
        public int? DotTT { get; set; }
        public string TenDotTT { get; set; }
        public decimal? SoTien { get; set; }
        public decimal? TienChi { get; set; }
        public decimal? ConLai { get; set; }
        public decimal? PhanTramTT { get; set; }
        public string DotThanhToan { get; set; }
        public bool IsSave { get; set; }


        cNTPhieuChi objPC;

        public frmEditV2()
        {
            InitializeComponent();
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

            objPC.SoPC = txtSoPhieu.Text;
            objPC.NgayChi = dateNgayChi.DateTime;
          //  objPC.TKCo = lookUpTKCo.Text;
           // objPC.TKNo = lookUpTKNo.Text;
            objPC.SoTien = spinSoTienThu.Value;
            objPC.DotTT = DotTT;

            objPC.NguoiNhan = txtNguoiNhan.Text;
            objPC.SoCMND = txtSoCMND.Text;
            objPC.NoiCap = txtNoiCap.Text;
            if (dateNgayCap.DateTime.Year != 1)
                //objPC.NgayCap = dateNgayCap.DateTime;
            objPC.DiaChiNN = txtDiaChi.Text;
            objPC.LyDo = txtDienGiai.Text;
            objPC.ChungTuGoc = txtChungTu.Text;
            objPC.MaNVN = BEE.THUVIEN.Common.MaNV;
            objPC.NgayNhap = DateTime.Now;
            objPC.MaNV = BEE.THUVIEN.Common.MaNV;
            objPC.SoTien = (decimal?)spinThucThu.EditValue;
            if (this.MaPC == null)
            {
                db.cNTPhieuChis.InsertOnSubmit(objPC);
            }

            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmEditV2_Load(object sender, EventArgs e)
        {
            TranslateUserControl(this);

            var listTaiKhoan = db.TaiKhoans;
            lookUpTKCo.Properties.DataSource = listTaiKhoan;
            lookUpTKNo.Properties.DataSource = listTaiKhoan;
            lookUpLoaiChi.Properties.DataSource = db.pgcLoaiPhieuThuChis.Where(p => !p.IsPaid.GetValueOrDefault());
            lookUpCompany.Properties.DataSource = db.Companies;

            if (this.MaPC != null)
            {
                objPC = db.cNTPhieuChis.Single(p => p.ID == this.MaPC);
                txtSoPhieu.EditValue = objPC.SoPC;
                dateNgayChi.EditValue = objPC.NgayChi;
                lookUpTKNo.EditValue = objPC.TKNo;
                lookUpTKCo.EditValue = objPC.TKCo;
                spinThucThu.EditValue = objPC.SoTien;
                txtNguoiNhan.EditValue = objPC.NguoiNhan;
                txtSoCMND.Text = objPC.SoCMND;
                dateNgayCap.EditValue = objPC.NgayCap;
                txtNoiCap.Text = objPC.NoiCap;
                txtDiaChi.EditValue = objPC.DiaChiNN;
                txtDienGiai.EditValue = objPC.LyDo;
                txtChungTu.EditValue = objPC.ChungTuGoc;
                var objDTT = db.cLichThanhToanNhaThaus.SingleOrDefault(p => p.ID == DotTT);
                spinDotTT.EditValue = objDTT.DotThanhToan;
                lookUpCompany.EditValue = objPC.MaCT;
                lookUpLoaiChi.EditValue = objPC.MaLoai;


            }
            else
            {
                objPC = new cNTPhieuChi();
                objPC.cHopDongNhaThau = db.cHopDongNhaThaus.Single(p => p.ContractID == this.ContractID);
                string soPhieu = "";
                db.pgcPhieuChi_TaoSoPhieu(ref soPhieu);
                txtSoPhieu.EditValue = soPhieu;
                dateNgayChi.EditValue = DateTime.Now;
                cHopDongNhaThau objHDNT = objPC.cHopDongNhaThau;
                var objKH = db.KhachHangs.SingleOrDefault(p => p.MaKH == objHDNT.CusID);
                txtNguoiNhan.EditValue = objKH.IsPersonal.Value ? objKH.TenKH : objKH.TenCongTy;
                txtSoCMND.Text = objKH.SoCMND;
                dateNgayCap.EditValue = objKH.NgayCap;
                txtNoiCap.Text = objKH.NoiCap;
                txtDiaChi.EditValue = objKH.DiaChi;
                txtCodeSUN.Text = objKH.CodeSUN;
                spinThucThu.EditValue = this.ConLai;
                var objDTT = db.cLichThanhToanNhaThaus.SingleOrDefault(p => p.ID == DotTT);
                spinDotTT.EditValue = objDTT.DotThanhToan;
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void spinSoTienThu_EditValueChanged(object sender, EventArgs e)
        {
            if (spinSoTienThu.Value - spinThucThu.Value > 0)
            {
                DialogBox.Infomation("Số tiền thu vượt quá số tiền còn lại, vui lòng kiểm tra lại.");
                btnLuu.Enabled = false;
                return;
            }
            btnLuu.Enabled = true;
            spinConLai.EditValue = spinThucThu.Value - spinSoTienThu.Value;
           
        }
    }
}