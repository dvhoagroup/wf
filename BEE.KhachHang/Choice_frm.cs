using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;
using BEE.NghiepVuKhac;

namespace BEE.KhachHang
{
    public partial class Choice_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaKH = 0;
        public string HoTenKH1 = "", HoTenKH2 = "";
        public byte MaNDD2 = 0;
        public Choice_frm()
        {
            InitializeComponent();
        }

        private void Choice_frm_Load(object sender, EventArgs e)
        {            
            xtraTabControl1.SelectedTabPageIndex = 1;
            xtraTabControl1.SelectedTabPageIndex = 0;
            if (MaKH != 0)
            {
                this.Text = "Cập nhật Khách hàng cùng đứng tên với Ông/Bà " + HoTenKH1;
                LoadKhachHang();
                if (MaNDD2 != 0)
                    LoadAvatar();
            }else
                this.Text = "Chọn Khách hàng cùng đứng tên với Ông/Bà " + HoTenKH1;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (btnKhachHang.Text != "")
                HoTenKH2 = btnKhachHang.Text;

            this.Close();
        }

        void AddNewCustomer()
        {
            if (btnKhachHang.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập họ và tên khách hàng. Xin cảm ơn.");
                btnKhachHang.Focus();
                return;
            }

            if (txtSoCMND.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập số chứng minh nhân dân. Xin cảm ơn.");
                txtSoCMND.Focus();
                return;
            }

            if (dateNgayCap.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập ngày cấp số chứng minh nhân dân. Xin cảm ơn.");
                dateNgayCap.Focus();
                return;
            }

            if (txtNoiCap.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập nơi cấp số chứng minh nhân dân. Xin cảm ơn.");
                txtNoiCap.Focus();
                return;
            }

            if (dateNgaySinh.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập ngày sinh của khách hàng. Xin cảm ơn.");
                dateNgaySinh.Focus();
                return;
            }

            if (dateNgaySinh.DateTime.CompareTo(dateNgayCap.DateTime) >= 0)
            {
                DialogBox.Infomation("Ngày cấp CMND phải lớn hơn ngày sinh. Vui lòng kiểm tra lại, xin cảm ơn.");
                dateNgayCap.Focus();
                return;
            }

            if (txtThuongTru.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập địa chỉ thường trú của khách hàng. Xin cảm ơn.");
                txtThuongTru.Focus();
                return;
            }

            if (btnDCTT.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn xã, huyện, tỉnh cho địa chỉ thường trú. Xin cảm ơn.");
                btnDCTT.Focus();
                return;
            }

            if (txtDiaChiLienHe.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập địa chỉ liên hệ của khách hàng. Xin cảm ơn.");
                txtDiaChiLienHe.Focus();
                return;
            }

            if (btnDCLH.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn xã, huyện, tỉnh cho địa chỉ liên hệ. Xin cảm ơn.");
                btnDCLH.Focus();
                return;
            }
            it.KhachHangCls o = new it.KhachHangCls();
            o.SoCMND = txtSoCMND.Text;
            if (o.CheckSoCMND())
            {
                DialogBox.Infomation("Số CMND <" + txtSoCMND.Text + "> đã có trong hệ thống. Vui lòng nhập lại số CMND. Xin cảm ơn");
                txtSoCMND.Focus();
                return;
            }
            try
            {
                o.HoKH = btnKhachHang.Text.Trim();
                string[] s = o.HoKH.Split(' ');
                if (s.Length < 2)
                    o.HoKH = "";
                else
                    o.HoKH = o.HoKH.Substring(0, o.HoKH.LastIndexOf(" ")).Trim();
            }
            catch { o.HoKH = ""; }
            try
            {
                o.TenKH = btnKhachHang.Text.Trim();
                try
                {
                    o.TenKH = o.TenKH.Substring(o.TenKH.LastIndexOf(" ")).Trim();
                }
                catch { }
            }
            catch { o.TenKH = ""; }
            o.NgaySinh = dateNgaySinh.DateTime;
            o.NoiCap = txtNoiCap.Text;
            o.NgayCap = dateNgayCap.DateTime;
            o.DiDong = txtDiDong.Text;
            o.DTCD = txtDienThoaiCD.Text;
            o.Email = txtEmail.Text;
            o.ThuongTru = txtThuongTru.Text;
            o.Xa.MaXa = int.Parse(btnDCTT.Tag.ToString());
            o.DiaChi = txtDiaChiLienHe.Text;
            o.Xa2.MaXa = int.Parse(btnDCLH.Tag.ToString());
            MaKH = o.InsertFast();
            btnKhachHang.Properties.ReadOnly = true;
        }

        void LoadKhachHang()
        {
            it.KhachHangCls o = new it.KhachHangCls(MaKH);
            btnKhachHang.Text = o.HoKH + " " + o.TenKH;
            txtSoCMND.Text = o.SoCMND;
            if (o.NgayCap.Year != 1)
                dateNgayCap.DateTime = o.NgayCap;
            else
                dateNgayCap.Text = "";
            if (o.NgaySinh.Year != 1)
                dateNgaySinh.DateTime = o.NgaySinh;
            else
                dateNgaySinh.Text = "";
            txtNoiCap.Text = o.NoiCap;
            txtNoiSinh.Text = o.NguyenQuan;
            txtThuongTru.Text = o.ThuongTru;
            txtEmail.Text = o.Email;
            txtDienThoaiCD.Text = o.DTCD;
            txtDiDong.Text = o.DiDong;
            txtDiaChiLienHe.Text = o.DiaChi;
            btnDCLH.Tag = o.Xa2.MaXa;
            btnDCLH.Text = o.Xa2.GetAddress();
            btnDCTT.Tag = o.Xa.MaXa;
            btnDCTT.Text = o.Xa.GetAddress();
        }

        void LoadALLAvatar()
        {
            it.NguoiDaiDienCls o = new it.NguoiDaiDienCls();
            lookUpHoTenNDD.Properties.DataSource = o.SelectAll(MaKH);
            if (MaNDD2 == 0)
                lookUpHoTenNDD.ItemIndex = 0;
        }

        private void btnKhachHang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                Find_frm frm = new Find_frm();
                frm.ShowDialog();
                if (frm.MaKH != 0)
                {
                    MaKH = frm.MaKH;
                    LoadKhachHang();
                    LoadALLAvatar();
                }
                if (btnKhachHang.Text.Trim() != "")
                    lookUpHoTenNDD.Properties.Buttons[1].Visible = true;
                else
                    lookUpHoTenNDD.Properties.Buttons[1].Visible = false;
            }
            else
            {
                //Them khach hang
                AddNewCustomer();
            }
        }

        private void btnDCTT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SelectPosition_frm frm = new SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCTT.Tag = frm.MaXa;
                btnDCTT.Text = frm.Result;
            }
        }

        private void btnDCLH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCLH.Tag = frm.MaXa;
                btnDCLH.Text = frm.Result;
            }
        }

        private void picAddress2_DoubleClick(object sender, EventArgs e)
        {
            txtDiaChiLienHe.Text = txtThuongTru.Text;
            btnDCLH.Text = btnDCTT.Text;
            btnDCLH.Tag = btnDCTT.Tag;
        }

        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            txtDiaChiLH2.Text = txtDiaChiTT2.Text;
            btnDCLH2.Text = btnDCTT2.Text;
            btnDCLH2.Tag = btnDCTT2.Tag;
        }

        private void btnDCTT2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCTT2.Tag = frm.MaXa;
                btnDCTT2.Text = frm.Result;
            }
        }

        private void btnDCLH2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCLH2.Tag = frm.MaXa;
                btnDCLH2.Text = frm.Result;
            }
        }

        private void lookUpHoTenNDD_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                NguoiDaiDien_frm frm = new NguoiDaiDien_frm();
                frm.MaKH = MaKH;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadALLAvatar();
            }
        }

        private void lookUpHoTenNDD_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit _NDD = (LookUpEdit)sender;
            MaNDD2 = byte.Parse(_NDD.EditValue.ToString());
            LoadAvatar();
        }

        void LoadAvatar()
        {
            try
            {
                it.NguoiDaiDienCls o = new it.NguoiDaiDienCls(MaKH);
                txtDiaChiLH2.Text = o.DiaChiLL;
                txtDiaChiTT2.Text = o.DiaChiTT;
                txtDiDong2.Text = o.DTDD;
                txtDTCD2.Text = o.DTCD;
                txtEmail2.Text = o.Email;
                txtNoiCap2.Text = o.NoiCap;
                txtSoCMND2.Text = o.SoCMND;
                txtEmail2.Text = o.Email;
                if (o.NgayCap.Year != 1)
                    dateNgayCap2.DateTime = o.NgayCap;
                if (o.NgaySinh.Year != 1)
                    dateNgaySinh2.DateTime = o.NgaySinh;
                //lookUpHoTenNDD.EditValue = o.STT;
                txtNoiSinh2.Text = o.NoiSinh;
                btnDCLH2.Tag = o.Xa.MaXa;
                btnDCLH2.Text = o.Xa.GetAddress();
                btnDCTT2.Tag = o.Xa2.MaXa;
                btnDCTT2.Text = o.Xa2.GetAddress();
            }
            catch { }
        } 
    }
}