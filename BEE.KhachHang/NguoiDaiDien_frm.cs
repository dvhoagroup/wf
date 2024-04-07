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
using BEE.NghiepVuKhac;

namespace BEE.KhachHang
{
    public partial class NguoiDaiDien_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaKH { get; set; }
        public int MaNDD { get; set; }

        private MasterDataContext db = new MasterDataContext();
        private NguoiDaiDien objNDD;

        public NguoiDaiDien_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            #region ràng buộc
            if (txtHoTenNDD.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập họ tên người đại diện. Xin cảm ơn");
                txtHoTenNDD.Focus();
                return;
            }

            //if (txtSoCMND.Text == "")
            //{
            //    DialogBox.Infomation("Vui lòng nhập số chứng minh nhân dân. Xin cảm ơn.");
            //    txtSoCMND.Focus();
            //    return;
            //}

            if (txtDiDongNDD.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập điện thoại di động. Xin cảm ơn.");
                txtDiDongNDD.Focus();
                return;
            }

            //if (txtNoiCap.Text == "")
            //{
            //    DialogBox.Infomation("Vui lòng nhập nơi cấp số chứng minh nhân dân. Xin cảm ơn.");
            //    txtNoiCap.Focus();
            //    return;
            //}

            //if (dateNgaySinh.Text == "")
            //{
            //    DialogBox.Infomation("Vui lòng nhập ngày sinh của khách hàng. Xin cảm ơn.");
            //    dateNgaySinh.Focus();
            //    return;
            //}

            //if (dateNgaySinh.DateTime.CompareTo(dateNgayCap.DateTime) >= 0)
            //{
            //    DialogBox.Infomation("Ngày cấp CMND phải lớn hơn ngày sinh. Vui lòng kiểm tra lại, xin cảm ơn.");
            //    dateNgayCap.Focus();
            //    return;
            //}

            //if (btnDCTT.Text == "")
            //{
            //    DialogBox.Infomation("Vui lòng chọn xã, huyện, tỉnh cho địa chỉ thường trú. Xin cảm ơn.");
            //    btnDCTT.Focus();
            //    return;
            //}

            //if (txtDiaChiTT.Text == "")
            //{
            //    DialogBox.Infomation("Vui lòng nhập địa chỉ thường trú của khách hàng. Xin cảm ơn.");
            //    txtDiaChiTT.Focus();
            //    return;
            //}            

            //if (btnDCLH.Text == "")
            //{
            //    DialogBox.Infomation("Vui lòng chọn xã, huyện, tỉnh cho địa chi liên hệ. Xin cảm ơn.");
            //    btnDCLH.Focus();
            //    return;
            //}

            //if (txtDiaChiLH.Text == "")
            //{
            //    DialogBox.Infomation("Vui lòng nhập địa chỉ liên hệ của khách hàng. Xin cảm ơn.");
            //    txtDiaChiLH.Focus();
            //    return;
            //}
            #endregion

            objNDD.MaQD = (byte?)lookQuyDanh.EditValue;
            objNDD.MaQH = (short?)lookMoiQuanHe.EditValue;
            objNDD.DiaChiLL = txtDiaChiLH.Text;
            objNDD.DiaChiTT = txtDiaChiTT.Text;
            objNDD.DTCD = txtDTCDNDD.Text;
            objNDD.DTDD = txtDiDongNDD.Text;
            objNDD.Email = txtEmailNDD.Text;
            objNDD.HoTen = txtHoTenNDD.Text;
            objNDD.SoCMND = txtSoCMND.Text;
            objNDD.NoiCap = txtNoiCap.Text;
            objNDD.MaSoThue = txtMaSoThue.Text;

            if (dateNgaySinh.Text != "")
                objNDD.NgaySinh = dateNgaySinh.DateTime;
            if (dateNgayCap.Text != "")
                objNDD.NgayCap = dateNgayCap.DateTime;
            objNDD.NoiSinh = txtNoiSinh.Text;
            objNDD.NgayNhap = DateTime.Now;
            objNDD.MaNVN = Common.StaffID;

            if (MaNDD == 0)
            {
                objNDD.MaKH = MaKH;
                db.NguoiDaiDiens.InsertOnSubmit(objNDD);
            }

            db.SubmitChanges();

            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void NguoiDaiDien_frm_Load(object sender, EventArgs e)
        {
            lookQuyDanh.Properties.DataSource = db.QuyDanhs;
            lookMoiQuanHe.Properties.DataSource = db.MoiQuanHes;
            if (MaNDD != 0)
            {
                objNDD = db.NguoiDaiDiens.Single(p => p.MaNDD == MaNDD);
                lookQuyDanh.EditValue = objNDD.MaQD;
                lookMoiQuanHe.EditValue = objNDD.MaQH;
                txtDiaChiLH.Text = objNDD.DiaChiLL;
                txtDiaChiTT.Text = objNDD.DiaChiTT;
                txtDiDongNDD.Text = objNDD.DTDD;
                txtDTCDNDD.Text = objNDD.DTCD;
                txtEmailNDD.Text = objNDD.Email;
                txtHoTenNDD.Text = objNDD.HoTen;
                dateNgayCap.EditValue = objNDD.NgayCap;
                dateNgaySinh.EditValue = objNDD.NgaySinh;
                txtNoiCap.Text = objNDD.NoiCap;
                txtSoCMND.Text = objNDD.SoCMND;
                txtMaSoThue.Text = objNDD.MaSoThue;
                txtNoiSinh.Text = objNDD.NoiSinh;
                try
                {
                    btnDCTT.Text = string.Format("{0}, {1}, {2}", objNDD.Xa.TenXa, objNDD.Xa.Huyen.TenHuyen, objNDD.Xa.Huyen.Tinh.TenTinh);
                    btnDCLH.Text = string.Format("{0}, {1}, {2}", objNDD.Xa1.TenXa, objNDD.Xa1.Huyen.TenHuyen, objNDD.Xa1.Huyen.Tinh.TenTinh);

                }
                catch { }
            }
            else
            {
                objNDD = new NguoiDaiDien();
            }
        }

        private void btnMaXa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            NghiepVuKhac.SelectPosition_frm frm = new NghiepVuKhac.SelectPosition_frm();
            frm.MaXa = objNDD.MaXa2.GetValueOrDefault();
            frm.ShowDialog();
            if (frm.MaXa != 0)
            {
                objNDD.Xa1 = db.Xas.Single(p => p.MaXa == frm.MaXa);
                btnDCLH.Text = frm.Result;
            }
        }

        private void btnDCTT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SelectPosition_frm frm = new SelectPosition_frm();
            frm.MaXa = objNDD.MaXa.GetValueOrDefault();
            frm.ShowDialog();
            if (frm.MaXa != 0)
            {
                objNDD.Xa = db.Xas.Single(p => p.MaXa == frm.MaXa);
                btnDCTT.Text = frm.Result;
            }
        }

        private void picAddress_DoubleClick(object sender, EventArgs e)
        {
            txtDiaChiLH.Text = txtDiaChiTT.Text;
            btnDCLH.Text = btnDCTT.Text;
            objNDD.Xa1 = objNDD.Xa;
        }
        public void checkDienThoai1(string text)
        {
            if (text.Contains(" "))
            {
                DialogBox.Error("Không dùng khoảng trắng");
                txtDiDongNDD.Focus();
                txtDiDongNDD.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            var obj = db.KyTuDacBiets.ToList();
            foreach (var i in obj)
            {
                if (text.Contains(i.TenKT))
                {
                    DialogBox.Error("Số điện thoại không bao gồm ký tự: " + "[" + i.TenKT + "]");
                    txtDiDongNDD.Focus();
                    txtDiDongNDD.BackColor = Color.Red;
                    btnDongY.Enabled = false;
                    return;

                }

            }
        }

        private void txtDiDongNDD_TextChanged(object sender, EventArgs e)
        {
            var text = txtDiDongNDD.Text;
            if (MaNDD != 0 && string.IsNullOrEmpty(text))
            {
                return;
            }
            if (text == null | text == "")
            {
                txtDiDongNDD.BackColor = Color.White;
                btnDongY.Enabled = false;
            }
            else
            {
                btnDongY.Enabled = true;
            }


            if (text == " ")
            {
                DialogBox.Error("Không dùng khoảng trắng");
                txtDiDongNDD.Focus();
                txtDiDongNDD.BackColor = Color.Red;
                btnDongY.Enabled = false;

            }
            else
            {
                btnDongY.Enabled = true;
                txtDiDongNDD.BackColor = Color.White;
            }

            var obj = db.KyTuDacBiets.Where(p => p.TenKT == text).ToList();
            if (obj.Count > 0)
            {
                DialogBox.Error("Số điện thoại không bao gồm ký tự: " + "[" + text + "]");
                txtDiDongNDD.Focus();
                txtDiDongNDD.BackColor = Color.Red;
                btnDongY.Enabled = false;

            }
            else
            {
                btnDongY.Enabled = true;
                txtDiDongNDD.BackColor = Color.White;
            }


            checkDienThoai1(text);
        }
        public void checkDienThoai2(string text)
        {
            if (text.Contains(" "))
            {
                DialogBox.Error("Không dùng khoảng trắng");
                txtDTCDNDD.Focus();
                txtDTCDNDD.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            var obj = db.KyTuDacBiets.ToList();
            foreach (var i in obj)
            {
                if (text.Contains(i.TenKT))
                {
                    DialogBox.Error("Số điện thoại không bao gồm ký tự: " + "[" + i.TenKT + "]");
                    txtDTCDNDD.Focus();
                    txtDTCDNDD.BackColor = Color.Red;
                    btnDongY.Enabled = false;
                    return;

                }

            }
        }
        private void txtDTCDNDD_TextChanged(object sender, EventArgs e)
        {
            var text = txtDTCDNDD.Text;
            if (MaNDD != 0 && string.IsNullOrEmpty(text))
            {
                return;
            }
            if (text == null | text == "")
            {
                txtDTCDNDD.BackColor = Color.White;
                btnDongY.Enabled = false;
            }



            if (text == " ")
            {
                DialogBox.Error("Không dùng khoảng trắng");
                txtDTCDNDD.Focus();
                txtDTCDNDD.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            else
            {
                btnDongY.Enabled = true;
                txtDTCDNDD.BackColor = Color.White;
            }

            var obj = db.KyTuDacBiets.Where(p => p.TenKT == text).ToList();
            if (obj.Count > 0)
            {
                DialogBox.Error("Số điện thoại không bao gồm ký tự: " + "[" + text + "]");
                txtDTCDNDD.Focus();
                txtDTCDNDD.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            else
            {
                btnDongY.Enabled = true;
                txtDTCDNDD.BackColor = Color.White;
            }

            checkDienThoai2(text);
        }
    }
}