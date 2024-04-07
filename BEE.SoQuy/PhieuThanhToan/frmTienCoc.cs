﻿using System;
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

namespace BEE.SoQuy.PhieuThanhToan
{
    public partial class frmTienCoc : DevExpress.XtraEditors.XtraForm
    {
        public int? MaPTT, MaPGC, MaKH, TyLeTT, MaLGD;
        public byte DotTT = 1, MaKTT = 1;
        public decimal SoTien, TienTT, ConLai, LaiSuat;
        public int ActionID = 2;

        MasterDataContext db = new MasterDataContext();
        pgcPhieuThanhToan objPT;

        public frmTienCoc()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            lookLoaiTien.Properties.DataSource = db.LoaiTiens;

            if (this.MaPTT != null)
            {
                objPT = db.pgcPhieuThanhToans.Single(p => p.MaPTT == this.MaPTT);
                txtSoPhieu.EditValue = objPT.SoPhieu;
                dateNgayThu.EditValue = objPT.NgayTT;
                spinDotTT.EditValue = objPT.DotTT;

                spinPhaiNop.EditValue = objPT.TuongUngTT;
                spinThucNop.EditValue = objPT.ThucNop;
                spinConNo.EditValue = objPT.TuongUngTT - objPT.ThucNop;
                lookLoaiTien.EditValue = objPT.MaLoaiTien;
                spinTyGia.EditValue = objPT.TyGia;
                spinQuyDoi.EditValue = objPT.TyGia * objPT.ThucNop;
                txtDienGiai.EditValue = objPT.DienGiai;

                txtNguoiNop.EditValue = txtNguoiNop2.EditValue = objPT.NguoiNop;
                txtDiaChi.EditValue = objPT.DiaChi;

                MaKH = objPT.MaKH;

                var objSP = objPT.pgcPhieuGiuCho.bdsSanPham;
                txtMaO.Text = objSP.MaLo;
                spinDienTich.EditValue = objSP.DienTichXD;
                spinDonGia.EditValue = objSP.DonGiaXD;
                spinThanhTien.EditValue = objSP.ThanhTienXD;
            }
            else
            {
                txtPhongKinhDoanh.Text = BEE.THUVIEN.Common.TenNV;
                objPT = new pgcPhieuThanhToan();
                objPT.pgcPhieuGiuCho = db.pgcPhieuGiuChos.Single(p => p.MaPGC == this.MaPGC);
                var objSP = objPT.pgcPhieuGiuCho.bdsSanPham;
                txtMaO.Text = objSP.MaLo;
                spinDienTich.EditValue = objSP.DienTichXD;
                spinDonGia.EditValue = objSP.DonGiaXD;
                spinThanhTien.EditValue = objSP.ThanhTienXD;
                objPT.TyGia = objSP.LoaiTien.TyGia;
                objPT.DotTT = this.DotTT;
                spinDotTT.EditValue = this.DotTT;

                string soPhieu = "";
                db.pgcPhieuThanhToan_TaoSoPhieu(ref soPhieu);
                txtSoPhieu.EditValue = soPhieu;

                dateNgayThu.EditValue = DateTime.Now;

                pgcPhieuGiuCho objPGC = objPT.pgcPhieuGiuCho;
                txtNguoiNop.EditValue = txtNguoiNop2.EditValue = objPGC.KhachHang.IsPersonal.Value ? objPGC.KhachHang.TenKH : objPGC.KhachHang.TenCongTy;
                MaKH = objPGC.KhachHang.MaKH;
                txtDiaChi.EditValue = objPGC.KhachHang.DiaChi;
                txtDienGiai.EditValue = "Thu tiền cọc";

                lookLoaiTien.EditValue = objSP.LoaiTien.MaLoaiTien;
                spinTyGia.EditValue = objSP.LoaiTien.TyGia;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtSoPhieu.Text.Trim() == "")
            {
                BEE.DialogBox.Error("Vui lòng nhập [Số phiếu], xin cảm ơn.");
                txtSoPhieu.Focus();
                return;
            }
            else
            {
                var count = db.pgcPhieuThus.Where(p => p.SoPhieu == txtSoPhieu.Text && p.MaPT != this.MaPTT).Count();
                if (count > 0)
                {
                    BEE.DialogBox.Error("Trùng số phiếu, vui lòng nhập lại");
                    txtSoPhieu.Focus();
                    return;
                }
            }

            if (dateNgayThu.Text == "")
            {
                BEE.DialogBox.Error("Vui lòng nhập [Ngày lập], xin cảm ơn.");
                dateNgayThu.Focus();
                return;
            }

            if (txtNguoiNop.Text.Trim() == "")
            {
                BEE.DialogBox.Error("Vui lòng nhập [Người nộp], xin cảm ơn.");
                txtNguoiNop.Focus();
                return;
            }

            objPT.SoPhieu = txtSoPhieu.Text;
            objPT.NgayTT = dateNgayThu.DateTime;
            objPT.TuongUngTT = spinPhaiNop.Value;
            objPT.LaiSom = 0;
            objPT.DaNop = 0;
            objPT.DotTT = (byte?)spinDotTT.Value;
            objPT.KhoanThanhToan = txtThanhToan.Text;
            objPT.LaiSuat = 0;
            objPT.PhongKeToan = txtPhongKeToan.Text.Trim();
            objPT.PhongKinhDoanh = txtPhongKinhDoanh.Text.Trim();
            objPT.ThucNop = spinThucNop.Value;
            objPT.ThueVAT = 0;
            objPT.ThuQuy = txtThuQuy.Text.Trim();
            objPT.TyLeTT = 0;
            objPT.TyLeVAT = 0;
            objPT.VATDat = 0;
            objPT.TyGia = spinTyGia.Value;
            objPT.MaLoaiTien = Convert.ToByte(lookLoaiTien.EditValue);
            objPT.LoaiPTT = 0;

            objPT.NguoiNop = txtNguoiNop.Text;
            objPT.DiaChi = txtDiaChi.Text;
            objPT.DienGiai = txtDienGiai.Text;
            objPT.MaNV = BEE.THUVIEN.Common.MaNV;
            objPT.MaKH = MaKH;

            if (this.MaPTT == null)
            {
                objPT.MaNV = BEE.THUVIEN.Common.MaNV;
                db.pgcPhieuThanhToans.InsertOnSubmit(objPT);
            }
            else
                objPT.MaNVCN = BEE.THUVIEN.Common.MaNV;

            db.SubmitChanges();

            if (this.MaPTT == null)
                db.gdNghiepVu_add(MaPGC, MaLGD, ActionID, BEE.THUVIEN.Common.MaNV);

            BEE.DialogBox.Infomation();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetThucThu()
        {
            spinConNo.EditValue = spinPhaiNop.Value - spinThucNop.Value;
            spinQuyDoi.EditValue = spinThucNop.Value * spinTyGia.Value;
        }

        private void spinTienTT_EditValueChanged(object sender, EventArgs e)
        {
            SetThucThu();
        }

        private void spinLaiSuat_EditValueChanged(object sender, EventArgs e)
        {
            SetThucThu();
        }

        private void spinPhuThu_EditValueChanged(object sender, EventArgs e)
        {
            SetThucThu();
        }

        private void spinChietKhau_EditValueChanged(object sender, EventArgs e)
        {
            SetThucThu();
        }

        private void dateNgayThu_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void lookLoaiTien_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                spinTyGia.EditValue = Convert.ToDecimal(lookLoaiTien.GetColumnValue("TyGia"));
            }
            catch { }
        }

        private void spin_EditValueChanged(object sender, EventArgs e)
        {
            SetThucThu();
        }

        private void txtNguoiNop_EditValueChanged(object sender, EventArgs e)
        {
            txtNguoiNop2.Text = txtNguoiNop.Text;
        }
    }
}