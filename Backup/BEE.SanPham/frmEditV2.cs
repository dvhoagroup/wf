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
    public partial class frmEditV2 : DevExpress.XtraEditors.XtraForm
    {
        public int MaSP;
        public bool IsSave;

        MasterDataContext db = new MasterDataContext();
        bdsSanPham objSP;

        public frmEditV2()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            //BEE.NgonNgu.Language.TranslateControl(this, barManager1);
            BEE.NgonNgu.Language.TranslateGridControl(gcHangMuc);
            if (BEE.NgonNgu.Language.LangID != 1)
            {
                tabGeneralInfo.Text = tabGeneralInfo.Tag.ToString();
                tabOtherCategories.Text = tabOtherCategories.Tag.ToString();
                tabOtherInfo.Text = tabOtherInfo.Tag.ToString();
            }

            lookDuAn.Properties.DataSource = db.DuAns.Select(p => new { p.MaDA, p.TenDA });
            lookNVKD.Properties.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen });
            lookLoaiTien.Properties.DataSource = db.LoaiTiens;
            lookLoaiBDS.Properties.DataSource = db.LoaiBDs;
            lookHuong.Properties.DataSource = lookHuongBanCong.Properties.DataSource = db.PhuongHuongs;
            lookPhapLy.Properties.DataSource = db.PhapLies;
            cmbTienIch.Properties.DataSource = db.TienIches;
            lookLoaiDuong.Properties.DataSource = db.LoaiDuongs;

            if (this.MaSP != 0)
            {
                objSP = db.bdsSanPhams.Single(p => p.MaSP == this.MaSP);
                txtKyHieu.EditValue = objSP.KyHieu;
                lookDuAn.EditValue = objSP.MaDA;
                lookKhu.EditValue = objSP.MaKhu;
                lookPhanKhu.EditValue = objSP.MaPK;
                txtMaLo.EditValue = objSP.MaLo;
                txtTenDuong.EditValue = objSP.TenDuong;
                lookLoaiBDS.EditValue = objSP.MaLBDS;
                lookHuong.EditValue = objSP.MaHuong;
                lookHuongBanCong.EditValue = objSP.MaHuong2;
                lookPhapLy.EditValue = objSP.MaPL;
                lookLoaiDuong.EditValue = objSP.MaLD;

                spinDienTichXD.EditValue = objSP.DienTichXD;
                spinDonGiaXD.EditValue = objSP.DonGiaXD;
                spinThanhTienXD.EditValue = objSP.ThanhTienXD;
                spinDienTichKV.EditValue = objSP.DienTichKV;
                spinDonGiaKV.EditValue = objSP.DonGiaKV;
                spinThanhTienKV.EditValue = objSP.ThanhTienKV;
                spinVAT.EditValue = objSP.TyLeVAT ?? 0;
                spinTyLeMG.EditValue = objSP.TyLeMG ?? 0;
                spinPhiMG.EditValue = objSP.PhiMG ?? 0;
                lookLoaiTien.EditValue = objSP.MaLT;

                spinPhongKHach.EditValue = objSP.PhongKhach;
                spinPhongNgu.EditValue = objSP.PhongNgu;
                spinPhongTam.EditValue = objSP.PhongTam;
                spinDuongRong.EditValue = objSP.DuongRong;
                spinSoTang.EditValue = objSP.SoTang;
                spinSoDot.EditValue = objSP.DotTT ?? 0;
                spinNgangKV.EditValue = objSP.NgangKV;
                spinDaiKV.EditValue = objSP.DaiKV;
                spinNoHauKV.EditValue = objSP.SauKV;
                spinNgangXD.EditValue = objSP.NgangXD;
                spinDaiXD.EditValue = objSP.DaiXD;
                spinNoHauXD.EditValue = objSP.SauXD;
                txtDienGiai.EditValue = objSP.DienGiai;
                lookNVKD.EditValue = objSP.MaNVKD;
                txtNhomKH.Text = objSP.NhomKH;
                txtTinhTrangXD.Text = objSP.TinhTrangXD;
                txtGCNQSDD.Text = objSP.GCNQSDD;
                txtSoVSGCN.Text = objSP.SoVaoSoGCN;
                txtSoThua.Text = objSP.SoThua;
                txtDiaChiNha.Text = objSP.DiaChiNha;
                txtCodeSUN.Text = objSP.KyHieuSUN;
                txtPaymentTerm.Text = objSP.PaymentTerm;
                spinSize.EditValue = objSP.Size ?? 1;
                spinViTri.EditValue = objSP.ViTri ?? 0;
                chkIsUse.Checked = objSP.IsUse.GetValueOrDefault();

                if (objSP.NgayKyGCN != null)
                    dateNgayKyGCN.DateTime = objSP.NgayKyGCN.Value;

                if (objSP.MaTT > 2)
                    rdbTrangThai.Enabled = false;
                else
                    rdbTrangThai.EditValue = objSP.MaTT;
                //Tien ich
                string tienIch = "";
                foreach (bdsTienIch t in objSP.bdsTienIches)
                    tienIch += t.MaTI + ", ";
                tienIch = tienIch.TrimEnd(' ').TrimEnd(',');
                cmbTienIch.SetEditValue(tienIch);
                //hang muc
                gcHangMuc.DataSource = objSP.bdsHangMucs;
                spinTienSDDat.EditValue = objSP.TienSDDat ?? 0;
                spinGiaTriQuyetToan.EditValue = objSP.GiaTriQuyetToan ?? 0;
                spinGiaTriDuToan.EditValue = objSP.GiaTriDuToan ?? 0;
                spinTang.EditValue = objSP.LauSo;
                spinChieuSau.EditValue = objSP.ChieuSau ?? 0;
                spinDienTichCH.EditValue = objSP.DienTichCH ?? 0;
                spinDTThongThuy.EditValue = objSP.DienTichTT ?? 0;
                spinDienTichSS.EditValue = objSP.DTSanSau ?? 0;
                spinDienTichST.EditValue = objSP.DTSanTruoc ?? 0;
                spinDTBanCong.EditValue = objSP.DTBanCong ?? 0;
                spinDTBep.EditValue = objSP.DTBep ?? 0;
                spinDTLogia.EditValue = objSP.DTLogia ?? 0;
                spinDTPKhach.EditValue = objSP.DTPKhach ?? 0;
                spinDTPNgu1.EditValue = objSP.DTPNgu1 ?? 0;
                spinDTPNgu2.EditValue = objSP.DTPNgu2 ?? 0;
                spinDTPNgu3.EditValue = objSP.DTPNgu3 ?? 0;
                spinDTPNgu4.EditValue = objSP.DTPNgu4 ?? 0;
                spinDTPTho.EditValue = objSP.DTPTho ?? 0;
                spinDTPVeSinh1.EditValue = objSP.DTVS1 ?? 0;
                spinDTPVeSinh2.EditValue = objSP.DTVS2 ?? 0;
                spinDTPVeSinh3.EditValue = objSP.DTVS3 ?? 0;
                spinDTPVeSinh4.EditValue = objSP.DTVS4 ?? 0;
                spinDTPVeSinh5.EditValue = objSP.DTVS5 ?? 0;
                spinGiaBan.EditValue = objSP.GiaBan ?? 0;
                spinMatTien.EditValue = objSP.MatTien ?? 0;
                spingTongGiaBan.EditValue = objSP.TongGiaBan ?? 0;
                spinTyLeChietKhau.EditValue = objSP.Discount ?? 0;
                spinTienChietKhau.EditValue = objSP.DiscountMoney ?? 0;
                spinTienSauCK.EditValue = spingTongGiaBan.Value + spinTienChietKhau.Value;
                spinPhiBaoTri.EditValue = objSP.PhiBaoTri ?? 0;
                spinViaHeRong.EditValue = objSP.ViaHe ?? 0;
                lookCtyQL.EditValue = objSP.MaCT;
            }
            else
            {
                lookLoaiTien.ItemIndex = 0;
                lookNVKD.EditValue = Common.StaffID;
                SetAddNew();
            }
        }

        void SetEnableControl(bool enabel)
        {
            txtKyHieu.Enabled = enabel;
            lookDuAn.Enabled = enabel;
            lookKhu.Enabled = enabel;
            lookPhanKhu.Enabled = enabel;
            txtMaLo.Enabled = enabel;
            txtTenDuong.Enabled = enabel;
            lookLoaiBDS.Enabled = enabel;
            lookHuong.Enabled = enabel;
            lookPhapLy.Enabled = enabel;

            lookLoaiDuong.Enabled = enabel;
            spinDienTichXD.Enabled = enabel;
            spinDonGiaXD.Enabled = enabel;
            spinThanhTienXD.Enabled = enabel;
            spinDienTichKV.Enabled = enabel;
            spinDonGiaKV.Enabled = enabel;
            spinThanhTienKV.Enabled = enabel;
            spinTyLeMG.Enabled = enabel;
            spinPhiMG.Enabled = enabel;
            lookLoaiTien.Enabled = enabel;
            txtCodeSUN.Enabled = enabel;
            
            spinPhongKHach.Enabled = enabel;
            spinPhongNgu.Enabled = enabel;
            spinPhongTam.Enabled = enabel;
            spinDuongRong.Enabled = enabel;
            spinSoTang.Enabled = enabel;
            spinSoDot.Enabled = enabel;
            spinNgangKV.Enabled = enabel;
            spinDaiKV.Enabled = enabel;
            spinNoHauKV.Enabled = enabel;
            spinNgangXD.Enabled = enabel;
            spinDaiXD.Enabled = enabel;
            spinNoHauXD.Enabled = enabel;
            cmbTienIch.Enabled = enabel;
            lookNVKD.Enabled = enabel;
            txtGCNQSDD.Enabled = enabel;
            txtNhomKH.Enabled = enabel;
            txtDiaChiNha.Enabled = enabel;
            txtSoThua.Enabled = enabel;
            txtSoVSGCN.Enabled = enabel;
            dateNgayKyGCN.Enabled = enabel;
            txtTinhTrangXD.Enabled = enabel;
            if (objSP.MaTT < 3)
                rdbTrangThai.Enabled = enabel;
            txtDienGiai.Enabled = enabel;
            //nut chuc nang
            itemThem.Enabled = !enabel;
            itemSua.Enabled = !enabel;
            itemLuu.Enabled = enabel;
            itemHoan.Enabled = enabel;
            //
            itemXoa.Enabled = this.MaSP != 0;
            //
            gcHangMuc.Enabled = enabel;
        }

        void SetAddNew()
        {
            this.MaSP = 0;
            objSP = new bdsSanPham();
            string kyHieu = "";
            db.bdsSanPham_TaoKyHieu(ref kyHieu);
            txtKyHieu.EditValue = kyHieu;
            txtMaLo.EditValue = null;
            rdbTrangThai.SelectedIndex = 0;
            spinVAT.EditValue = 10;
            objSP.MaTT = 1;
            gcHangMuc.DataSource = objSP.bdsHangMucs;
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetAddNew();
            SetEnableControl(true);
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetEnableControl(true);
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                #region Rang buoc
                if (txtKyHieu.Text.Trim() == "")
                {
                    DialogBox.Error("Vui lòng nhập [Mã sản phẩm], xin cảm ơn.");
                    txtKyHieu.Focus();
                    return;
                }
                //else
                //{
                //    var count = db.bdsSanPhams.Where(p => p.KyHieu == txtKyHieu.Text.Trim() & p.MaSP != this.MaSP).Count();
                //    if (count > 0)
                //    {
                //        DialogBox.Error("Ký hiệu <" + txtKyHieu.Text + "> đã có trong hệ thống. Vui lòng nhập lại.");
                //        txtKyHieu.Focus();
                //        return;
                //    }
                //}

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
                #endregion

                objSP.KyHieu = txtKyHieu.Text;
                objSP.MaLBDS = (short?)lookLoaiBDS.EditValue;
                objSP.MaDA = (int)lookDuAn.EditValue;
                objSP.MaKhu = (int?)lookKhu.EditValue;
                objSP.MaPK = (int?)lookPhanKhu.EditValue;
                objSP.MaLo = txtMaLo.Text;
                objSP.TenDuong = txtTenDuong.Text;
                objSP.MaHuong = (short?)lookHuong.EditValue;
                objSP.MaHuong2 = (short?)lookHuongBanCong.EditValue;
                objSP.MaPL = (short?)lookPhapLy.EditValue;
                objSP.MaLD = (short?)lookLoaiDuong.EditValue;
                objSP.PaymentTerm = txtPaymentTerm.Text;
                objSP.ViTri = (int?)spinViTri.Value;
                objSP.Size = (decimal?)spinSize.Value;
                objSP.IsUse = chkIsUse.Checked;

                objSP.DienTichXD = spinDienTichXD.Value;
                objSP.DonGiaXD = spinDonGiaXD.Value;
                objSP.ThanhTienXD = spinThanhTienXD.Value;
                objSP.DienTichKV = spinDienTichKV.Value;
                objSP.DonGiaKV = spinDonGiaKV.Value;
                objSP.ThanhTienKV = spinThanhTienKV.Value;
                objSP.ThanhTienHM = objSP.bdsHangMucs.Sum(p => p.ThanhTien).GetValueOrDefault();
                objSP.ThanhTien = objSP.ThanhTienXD + objSP.ThanhTienKV + objSP.ThanhTienHM;
                objSP.TyLeVAT = spinVAT.Value;
                objSP.GiaTriDuToan = spinGiaTriDuToan.Value;
                objSP.GiaTriQuyetToan = spinGiaTriQuyetToan.Value;
                objSP.TienSDDat = spinTienSDDat.Value;
                objSP.KyHieuSUN = txtCodeSUN.Text.Trim();

                objSP.ChieuSau = spinChieuSau.Value;
                objSP.DienTichCH = spinDienTichCH.Value;
                objSP.DienTichTT = spinDTThongThuy.Value;
                objSP.DTSanSau = spinDienTichSS.Value;
                objSP.DTSanTruoc = spinDienTichST.Value;
                objSP.DTBanCong = spinDTBanCong.Value;
                objSP.DTBep = spinDTBep.Value;
                objSP.DTLogia = spinDTLogia.Value;
                objSP.DTPKhach = spinDTPKhach.Value;
                objSP.DTPNgu1 = spinDTPNgu1.Value;
                objSP.DTPNgu2 = spinDTPNgu2.Value;
                objSP.DTPNgu3 = spinDTPNgu3.Value;
                objSP.DTPNgu4 = spinDTPNgu4.Value;
                objSP.DTPTho = spinDTPTho.Value;
                objSP.DTVS1 = spinDTPVeSinh1.Value;
                objSP.DTVS2 = spinDTPVeSinh2.Value;
                objSP.DTVS3 = spinDTPVeSinh3.Value;
                objSP.DTVS4 = spinDTPVeSinh4.Value;
                objSP.DTVS5 = spinDTPVeSinh5.Value;
                objSP.GiaBan = spinGiaBan.Value;
                objSP.TongGiaBan = spingTongGiaBan.Value;
                objSP.MatTien = spinMatTien.Value;
                objSP.PhiBaoTri = spinPhiBaoTri.Value;
                objSP.ViaHe = spinViaHeRong.Value;
                objSP.MaCT = (int?)lookCtyQL.EditValue;

                objSP.Discount = spinTyLeChietKhau.Value;
                objSP.DiscountMoney = spinTienChietKhau.Value;

                if (spinTyLeMG.Value >= 0)
                {
                    objSP.TyLeMG = spinTyLeMG.Value;
                    objSP.PhiMG = objSP.ThanhTien * objSP.TyLeMG / 100;
                }
                else
                {
                    objSP.PhiMG = spinPhiMG.Value;
                    try
                    {
                        objSP.TyLeMG = objSP.PhiMG / objSP.ThanhTien * 100;
                    }
                    catch { }
                }
                objSP.MaLT = (byte)lookLoaiTien.EditValue;

                objSP.PhongKhach = Convert.ToByte(spinPhongKHach.Value);
                objSP.PhongNgu = Convert.ToByte(spinPhongNgu.Value);
                objSP.PhongTam = Convert.ToByte(spinPhongTam.Value);
                objSP.DuongRong = spinDuongRong.Value;
                objSP.SoTang = Convert.ToByte(spinSoTang.Value);
                objSP.DotTT = Convert.ToByte(spinSoDot.Value);
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
                        objTI.MaTI = byte.Parse(i);
                        objSP.bdsTienIches.Add(objTI);
                    }
                }
                objSP.MaNVKD = (int)lookNVKD.EditValue;
                if (objSP.MaTT < 3)
                    objSP.MaTT = (byte)rdbTrangThai.EditValue;
                objSP.MaNVKT = Common.StaffID;
                objSP.NgayCN = DateTime.Now;
                objSP.MaNVCN = Common.StaffID;
                objSP.DienGiai = txtDienGiai.Text;
                objSP.GCNQSDD = txtGCNQSDD.Text.Trim();
                objSP.TinhTrangXD = txtTinhTrangXD.Text.Trim();
                objSP.NhomKH = txtNhomKH.Text.Trim();
                objSP.SoVaoSoGCN = txtSoVSGCN.Text.Trim();
                objSP.SoThua = txtSoThua.Text.Trim();
                objSP.DiaChiNha = txtDiaChiNha.Text.Trim();
                try
                {
                    objSP.LauSo = Convert.ToByte(spinTang.EditValue);
                }
                catch { objSP.LauSo = 0; }
                if (dateNgayKyGCN.DateTime.Year != 1)
                    objSP.NgayKyGCN = dateNgayKyGCN.DateTime;
                else
                    objSP.NgayKyGCN = null;

                if (this.MaSP == 0)
                {
                    objSP.NgayNhap = DateTime.Now;
                    db.bdsSanPhams.InsertOnSubmit(objSP);
                }

                db.SubmitChanges();

                this.MaSP = objSP.MaSP;
                this.IsSave = true;
                //SetEnableControl(false);
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemHoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetEnableControl(false);
        }

        private void itemDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void spinDienTichXD_EditValueChanged(object sender, EventArgs e)
        {
            spinThanhTienXD.Value = spinDonGiaXD.Value * spinDienTichXD.Value;
        }

        private void spinDonGiaXD_EditValueChanged(object sender, EventArgs e)
        {
            spinThanhTienXD.Value = spinDonGiaXD.Value * spinDienTichXD.Value;
            SetTotal();
        }

        private void spinDienTichKV_EditValueChanged(object sender, EventArgs e)
        {
            spinThanhTienKV.Value = spinDonGiaKV.Value * spinDienTichKV.Value;
        }

        private void spinDonGiaKV_EditValueChanged(object sender, EventArgs e)
        {
            spinThanhTienKV.Value = spinDonGiaKV.Value * spinDienTichKV.Value;
            SetTotal();
        }

        decimal getTongGiaTri()
        {
            decimal tien = spinThanhTienXD.Value + spinThanhTienKV.Value;
            for (int i = 0; i < grvHangMuc.RowCount - 1; i++)
                tien += (decimal)grvHangMuc.GetRowCellValue(i, "ThanhTien");
            return tien;
        }

        private void spinTyLeMG_EditValueChanged(object sender, EventArgs e)
        {
            decimal phiMG = getTongGiaTri() * spinTyLeMG.Value / 100;
            if (phiMG != spinPhiMG.Value)
                spinPhiMG.Value = phiMG;
        }

        private void spinPhiMG_EditValueChanged(object sender, EventArgs e)
        {
            decimal tyLeMG = (spinPhiMG.Value / getTongGiaTri()) * 100;
            if (tyLeMG != spinTyLeMG.Value)
                spinTyLeMG.Value = tyLeMG;
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

        private void lookDuAn_EditValueChanged(object sender, EventArgs e)
        {
            lookKhu.Properties.DataSource = db.Khus.Where(p => p.MaDA == (int?)lookDuAn.EditValue).OrderBy(p=>p.STT);
        }

        private void lookKhu_EditValueChanged(object sender, EventArgs e)
        {
            lookPhanKhu.Properties.DataSource = db.PhanKhus.Where(p => p.MaKhu == (int?)lookKhu.EditValue).OrderBy(p => p.STT);
        }

        private void spinVAT_EditValueChanged(object sender, EventArgs e)
        {
            SetTotal();
        }

        void SetTotal()
        {
            spinGiaBan.Value = spinDonGiaKV.Value + spinDonGiaXD.Value;
            spinTienChietKhau.Value = spingTongGiaBan.Value * spinTyLeChietKhau.Value / 100;
            spinTienSauCK.EditValue = spingTongGiaBan.Value + spinTienChietKhau.Value;
            spinTongGiaVAT.Value = (spinVAT.Value * spinTienSauCK.Value / 100) + spinTienSauCK.Value;
            //spingTongGiaBan.EditValue = spinGiaBan.Value * spinDienTichCH.Value;
            spinPhiBaoTri.EditValue = spinTienSauCK.Value * 0.02M;
            spinTongGiaTriHD.Value = spinPhiBaoTri.Value + spinTongGiaVAT.Value;
        }

        private void spinPhiBaoTri_EditValueChanged(object sender, EventArgs e)
        {
            SetTotal();
        }

        private void spinTongGiaVAT_EditValueChanged(object sender, EventArgs e)
        {
            SetTotal();
        }

        private void spinDienTichCH_EditValueChanged(object sender, EventArgs e)
        {
            SetTotal();
        }

        private void spinGiaBan_EditValueChanged(object sender, EventArgs e)
        {
            SetTotal();
        }

        private void spingTongGiaBan_EditValueChanged(object sender, EventArgs e)
        {
            SetTotal();
        }
    }
}