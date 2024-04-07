using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class NguoiMoiGioi_frm : DevExpress.XtraEditors.XtraForm
    {
        public bool IsUpdate = false;
        public int MaNMG = 0;
        public NguoiMoiGioi_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
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

            //if (dateNgayCap.Text == "")
            //{
            //    DialogBox.Infomation("Vui lòng nhập ngày cấp số chứng minh nhân dân. Xin cảm ơn.");
            //    dateNgayCap.Focus();
            //    return;
            //}

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

            //if (txtDiaChiTT.Text == "")
            //{
            //    DialogBox.Infomation("Vui lòng nhập địa chỉ thường trú.. Xin cảm ơn.");
            //    txtDiaChiTT.Focus();
            //    return;
            //}

            //if (btnDCTT.Text == "")
            //{
            //    DialogBox.Infomation("Vui lòng chọn xã, huyện, tỉnh cho địa chỉ thường trú. Xin cảm ơn.");
            //    btnDCTT.Focus();
            //    return;
            //}

            it.NguoiMoiGioiCls o = new it.NguoiMoiGioiCls();
            o.Email = txtEmailNDD.Text;
            o.HoTen = txtHoTenNDD.Text;
            o.SoCMND = txtSoCMND.Text;
            o.NoiCap = txtNoiCap.Text;
            if (dateNgaySinh.Text != "")
                o.NgaySinh = dateNgaySinh.DateTime;
            if (dateNgayCap.Text != "")
                o.NgayCap = dateNgayCap.DateTime;
            o.NoiSinh = txtNoiSinh.Text;
            if (btnDCTT.Text != "")
                o.Xa.MaXa = int.Parse(btnDCTT.Tag.ToString());
            else
                o.Xa.MaXa = 0;
            o.DienThoai = txtDTCDNDD.Text;
            o.GhiChu = txtGhiChu.Text;
            o.DiaChi = txtDiaChiTT.Text;
            o.MaNMG = MaNMG;

            if (MaNMG != 0)
            {
                if (txtSoCMND.Text.Trim() != "")
                {
                    if (o.CheckSoCMND2())
                    {
                        DialogBox.Infomation("Số CMND <" + txtSoCMND.Text + "> đã có trong hệ thống. Vui lòng nhập lại số CMND. Xin cảm ơn");
                        txtSoCMND.Focus();
                        return;
                    }
                }
                o.Update();
            }
            else
            {
                if (txtSoCMND.Text.Trim() != "")
                {
                    if (o.CheckSoCMND())
                    {
                        DialogBox.Infomation("Số CMND <" + txtSoCMND.Text + "> đã có trong hệ thống. Vui lòng nhập lại số CMND. Xin cảm ơn");
                        txtSoCMND.Focus();
                        return;
                    }
                }
                o.Insert();
            }

            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }

        private void btnDCTT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCTT.Tag = frm.MaXa;
                btnDCTT.Text = frm.Result;
            }
        }

        void LoadData()
        {
            it.NguoiMoiGioiCls o = new it.NguoiMoiGioiCls(MaNMG);
            txtDiaChiTT.Text = o.DiaChi;
            txtDTCDNDD.Text = o.DienThoai;
            txtEmailNDD.Text = o.Email;
            txtGhiChu.Text = o.GhiChu;
            txtHoTenNDD.Text = o.HoTen;
            txtNoiCap.Text = o.NoiCap;
            txtNoiSinh.Text = o.NoiSinh;
            txtSoCMND.Text = o.SoCMND;
            if (o.NgayCap.Year != 1)
                dateNgayCap.DateTime = o.NgayCap;
            if (o.NgaySinh.Year != 1)
                dateNgaySinh.DateTime = o.NgaySinh;
            btnDCTT.Text = o.Xa.GetAddress();
            btnDCTT.Tag = o.Xa.MaXa;
            MaNMG = o.MaNMG;
        }

        private void NguoiMoiGioi_frm_Load(object sender, EventArgs e)
        {
            if (MaNMG != 0)
                LoadData();
        }
    }
}