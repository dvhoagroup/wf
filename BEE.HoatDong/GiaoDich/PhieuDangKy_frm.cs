using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandSoft.NghiepVu.GiaoDich
{
    public partial class PhieuDangKy_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaKH = 0, MaKH2 = 0, MaNV2 = 0, MaGD = 0;
        public string MaBDS = "";
        string SoPhieu = "", OldFileName = "";
        bool error = false;
        public bool IsUpdate = false;
        public PhieuDangKy_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picAddress2_DoubleClick(object sender, EventArgs e)
        {
            txtDiaChiLienHe.Text = txtThuongTru.Text;
            btnDCLH.Text = btnDCTT.Text;
            btnDCLH.Tag = btnDCTT.Tag;
        }

        private void btnDCTT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            LandSoft.Khac.SelectPosition_frm frm = new LandSoft.Khac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCTT.Tag = frm.MaXa;
                btnDCTT.Text = frm.Result;
            }
        }

        private void btnDCLH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            LandSoft.Khac.SelectPosition_frm frm = new LandSoft.Khac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCLH.Tag = frm.MaXa;
                btnDCLH.Text = frm.Result;
            }
        }

        private void btnDCTT2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            LandSoft.Khac.SelectPosition_frm frm = new LandSoft.Khac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCTT2.Tag = frm.MaXa;
                btnDCTT2.Text = frm.Result;
            }
        }

        private void btnDCLH2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            LandSoft.Khac.SelectPosition_frm frm = new LandSoft.Khac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCLH2.Tag = frm.MaXa;
                btnDCLH2.Text = frm.Result;
            }
        }

        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            txtDiaChiLH2.Text = txtDiaChiTT2.Text;
            btnDCLH2.Text = btnDCTT2.Text;
            btnDCLH2.Tag = btnDCTT2.Tag;
        }

        void AddNew()
        {
            it.TienIchCls o = new it.TienIchCls();
            chkListTienIch.DataSource = o.Select();

            it.LoaiTienCls objLoaiTien = new it.LoaiTienCls();
            lookUpLoaiTien.Properties.DataSource = objLoaiTien.Select();
            lookUpLoaiTien.ItemIndex = 0;
        }

        void LoadData()
        {
            it.pdkGiaoDichCls o = new it.pdkGiaoDichCls(MaGD);
            txtSoPhieu.Text = o.SoPhieu;
            dateNgayKy.DateTime = o.NgayKy;
            txtNhanVien1.Text = o.NhanVien1.GetName();

            MaNV2 = o.NhanVien2.MaNV;
            btnNhanVien2.Text = o.NhanVien2.GetName();            
            spinDonGia.EditValue = o.DonGia;            
            spinTongTien.EditValue = o.TongTien;
            spinTyLe1.EditValue = o.TyLe1;
            spinTyLe2.EditValue = o.TyLe2;            
            spinPhiMoiGioi.EditValue = o.PhiMoiGioi;            
            radioGroupThoiHan.SelectedIndex = o.ThoiHan - 1;            
            chkVAT.Checked = o.IsVAT;
            MaKH = o.KhachHang1.MaKH;
            LoadKhachHang();
            MaKH2 = o.KhachHang2.MaKH;
            LoadKhachHang2();

            txtHS1.Text = o.HoSo1;
            txtHS10.Text = o.HoSo10;
            txtHS2.Text = o.HoSo2;
            txtHS3.Text = o.HoSo3;
            txtHS4.Text = o.HoSo4;
            txtHS5.Text = o.HoSo5;
            txtHS6.Text = o.HoSo6;
            txtHS7.Text = o.HoSo7;
            txtHS8.Text = o.HoSo8;
            txtHS9.Text = o.HoSo9;

            btnFileAttach.Text = o.FileAttach;            
            txtHoTen3.Text = o.NguoiLienHe;
            txtDiDong1NLH.Text = o.DiDong1;
            txtDiDong2NLH.Text = o.DiDong2;
            txtDienGiai.Text = o.DienGiai;
            txtDienThoaiCDNLH.Text = o.DTNha;
            txtDTCoQuanNLH.Text = o.DTCoQuan;
            txtMoiQuanHe.Text = o.MoiQuanHe;

            lookUpLoaiTien.EditValue = o.LoaiTien.MaLoaiTien;
            if (o.Share)
            {
                chkPrivate.Checked = false;
                chkPublic.Checked = true;
            }
            else
            {
                chkPrivate.Checked = true;
                chkPublic.Checked = false;
            }

            txtLoaiBDSKhac.Text = o.LoaiBDSKhac;
            txtPhapLyKhac.Text = o.PhapLyKhac;
            chkBep.Checked = o.Bep;
            chkNguoiGV.Checked = o.PhongNGV;
            chkPhongDocSach.Checked = o.PhongDocSach;
            chkSanSau.Checked = o.SanSau;
            chkSanTruoc.Checked = o.SanTruoc;
            radioGroupHuong.SelectedIndex = o.Huong.MaPhuongHuong - 1;
            radioGroupLoaiBDS.SelectedIndex = o.LoaiBDS.MaLBDS - 1;
            radioGroupLoaiGiaoDich.SelectedIndex = o.LGD.MaLDG - 1;
            radioGroupPhapLy.SelectedIndex = o.PhapLy.MaPL - 1;
            spinDTPhongKhach.EditValue = o.DTPhongKhach;
            spinDTSanSau.EditValue = o.DTSanSau;
            spinDTSanTruoc.EditValue = o.DTSanTruoc;
            spinDuongBen.EditValue = o.DuongBen;
            spinDuongSau.EditValue = o.DuongSau;
            spinDuongTruoc.EditValue = o.DuongTruoc;
            spinDai.EditValue = o.Dai;
            spinRong.EditValue = o.Rong;
            spinPhongKhach.EditValue = o.PhongKhach;
            spinSoPhongNgu.EditValue = o.PhongNgu;
            spinSoPhongWC.EditValue = o.PhongWC;
            spinSoTang.EditValue = o.SoTang;
            spinDienTichXD.EditValue = o.DienTichXD;
            spinViTri.EditValue = o.ViTriTang;

            LoadTienIch();
        }

        void LoadTienIch()
        {
            it.pdkgd_TienIchCls o = new it.pdkgd_TienIchCls();
            o.MaPGD = MaGD;
            DataTable tbl = o.SelectBy();
            foreach (DataRow r in tbl.Rows)
            {
                for (int j = 0; j < chkListTienIch.ItemCount; j++)
                {
                    if (byte.Parse(r["MaTienIch"].ToString()) == byte.Parse(chkListTienIch.GetItemValue(j).ToString()))
                        chkListTienIch.SetItemChecked(j, true);
                }
            }
        }

        private void PhieuDangKy_frm_Load(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 2;
            xtraTabControl1.SelectedTabPageIndex = 1;
            xtraTabControl1.SelectedTabPageIndex = 0;

            AddNew();
            if (MaGD == 0)
            {
                txtNhanVien1.Text = LandSoft.Library.Common.StaffName;
                dateNgayKy.DateTime = DateTime.Now;
                TaoSoPhieu();
            }
            else
                LoadData();
        }

        private void txtHoTen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                KhachHang.Find_frm frm = new LandSoft.KhachHang.Find_frm();
                frm.ShowDialog();
                if (frm.MaKH != 0)
                {
                    MaKH = frm.MaKH;
                    txtHoTen.Text = frm.HoTen;
                    LoadKhachHang();
                }
            }
            else
            {
                //Them nhan vien moi
                AddNewCustomer();
            }
        }

        void AddNewCustomer()
        {
            if (txtHoTen.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập họ và tên khách hàng. Xin cảm ơn.");
                txtHoTen.Focus();
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
                o.HoKH = txtHoTen.Text.Trim();
                string[] s = o.HoKH.Split(' ');
                if (s.Length < 2)
                    o.HoKH = "";
                else
                    o.HoKH = o.HoKH.Substring(0, o.HoKH.LastIndexOf(" ")).Trim();
            }
            catch { o.HoKH = ""; }
            try
            {
                o.TenKH = txtHoTen.Text.Trim();
                try
                {
                    o.TenKH = o.TenKH.Substring(o.TenKH.LastIndexOf(" ")).Trim();
                }
                catch { }
            }
            catch { o.TenKH = ""; }

            o.NoiCap = txtNoiCap.Text;
            o.NgayCap = dateNgayCap.DateTime;
            o.DiDong = txtDiDong.Text;
            o.DTCD = txtDienThoaiCD.Text;
            o.Email = "";
            o.ThuongTru = txtThuongTru.Text;
            o.Xa.MaXa = int.Parse(btnDCTT.Tag.ToString());
            o.DiaChi = txtDiaChiLienHe.Text;
            o.Xa2.MaXa = int.Parse(btnDCLH.Tag.ToString());
            MaKH = o.InsertFast();
            txtHoTen.Properties.ReadOnly = true;
        }

        void UpdateCustomer()
        {            
            it.KhachHangCls o = new it.KhachHangCls();
            o.SoCMND = txtSoCMND.Text.Trim();
            try
            {
                o.HoKH = txtHoTen.Text.Trim();
                string[] s = o.HoKH.Split(' ');
                if (s.Length < 2)
                    o.HoKH = "";
                else
                    o.HoKH = o.HoKH.Substring(0, o.HoKH.LastIndexOf(" ")).Trim();
            }
            catch { o.HoKH = ""; }
            try
            {
                o.TenKH = txtHoTen.Text.Trim();
                try
                {
                    o.TenKH = o.TenKH.Substring(o.TenKH.LastIndexOf(" ")).Trim();
                }
                catch { }
            }
            catch { o.TenKH = ""; }

            o.NoiCap = txtNoiCap.Text;
            o.NgayCap = dateNgayCap.DateTime;
            o.DiDong = txtDiDong.Text;
            o.DTCD = txtDienThoaiCD.Text;
            o.ThuongTru = txtThuongTru.Text;
            o.Xa.MaXa = int.Parse(btnDCTT.Tag.ToString());
            o.DiaChi = txtDiaChiLienHe.Text;
            o.Xa2.MaXa = int.Parse(btnDCLH.Tag.ToString());
            o.DTCoQuan = txtDTCoQuan.Text;
            o.DiDong2 = txtDiDong2.Text;
            o.MaKH = MaKH;
            o.Update4();
            txtHoTen.Properties.ReadOnly = true;
        }

        void AddNewCustomer2()
        {
            if (txtHoTen2.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập họ và tên khách hàng. Xin cảm ơn.");
                txtHoTen2.Focus();
                return;
            }

            if (txtSoCMND2.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập số chứng minh nhân dân. Xin cảm ơn.");
                txtSoCMND2.Focus();
                return;
            }

            if (dateNgayCap2.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập ngày cấp số chứng minh nhân dân. Xin cảm ơn.");
                dateNgayCap2.Focus();
                return;
            }

            if (txtNoiCap2.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập nơi cấp số chứng minh nhân dân. Xin cảm ơn.");
                txtNoiCap2.Focus();
                return;
            }

            if (txtDiaChiTT2.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập địa chỉ thường trú của khách hàng. Xin cảm ơn.");
                txtDiaChiTT2.Focus();
                return;
            }

            if (btnDCTT2.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn xã, huyện, tỉnh cho địa chỉ thường trú. Xin cảm ơn.");
                btnDCTT2.Focus();
                return;
            }

            if (txtDiaChiLH2.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập địa chỉ liên hệ của khách hàng. Xin cảm ơn.");
                txtDiaChiLH2.Focus();
                return;
            }

            if (btnDCLH2.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn xã, huyện, tỉnh cho địa chỉ liên hệ. Xin cảm ơn.");
                btnDCLH2.Focus();
                return;
            }
            it.KhachHangCls o = new it.KhachHangCls();
            o.SoCMND = txtSoCMND2.Text;
            if (o.CheckSoCMND())
            {
                DialogBox.Infomation("Số CMND <" + txtSoCMND.Text + "> đã có trong hệ thống. Vui lòng nhập lại số CMND. Xin cảm ơn");
                txtSoCMND2.Focus();
                return;
            }
            try
            {
                o.HoKH = txtHoTen2.Text.Trim();
                string[] s = o.HoKH.Split(' ');
                if (s.Length < 2)
                    o.HoKH = "";
                else
                    o.HoKH = o.HoKH.Substring(0, o.HoKH.LastIndexOf(" ")).Trim();
            }
            catch { o.HoKH = ""; }
            try
            {
                o.TenKH = txtHoTen2.Text.Trim();
                try
                {
                    o.TenKH = o.TenKH.Substring(o.TenKH.LastIndexOf(" ")).Trim();
                }
                catch { }
            }
            catch { o.TenKH = ""; }

            o.NoiCap = txtNoiCap2.Text;
            o.NgayCap = dateNgayCap2.DateTime;
            o.DiDong = txtDiDong2.Text;
            o.DTCD = txtDienThoaiCD2.Text;
            o.ThuongTru = txtDiaChiTT2.Text;
            o.Xa.MaXa = int.Parse(btnDCTT2.Tag.ToString());
            o.DiaChi = txtDiaChiLH2.Text;
            o.Xa2.MaXa = int.Parse(btnDCLH2.Tag.ToString());
            o.Email = "";
            MaKH2 = o.InsertFast();
            txtHoTen2.Properties.ReadOnly = true;
        }

        void UpdateCustomer2()
        {
            it.KhachHangCls o = new it.KhachHangCls();
            o.SoCMND = txtSoCMND2.Text.Trim();
            try
            {
                o.HoKH = txtHoTen2.Text.Trim();
                string[] s = o.HoKH.Split(' ');
                if (s.Length < 2)
                    o.HoKH = "";
                else
                    o.HoKH = o.HoKH.Substring(0, o.HoKH.LastIndexOf(" ")).Trim();
            }
            catch { o.HoKH = ""; }
            try
            {
                o.TenKH = txtHoTen2.Text.Trim();
                try
                {
                    o.TenKH = o.TenKH.Substring(o.TenKH.LastIndexOf(" ")).Trim();
                }
                catch { }
            }
            catch { o.TenKH = ""; }

            o.NoiCap = txtNoiCap2.Text;
            o.NgayCap = dateNgayCap2.DateTime;
            o.DiDong = txtDiDong3.Text;
            o.DTCD = txtDienThoaiCD2.Text;
            o.ThuongTru = txtDiaChiTT2.Text;
            o.Xa.MaXa = int.Parse(btnDCTT2.Tag.ToString());
            o.DiaChi = txtDiaChiLH2.Text;
            o.Xa2.MaXa = int.Parse(btnDCLH2.Tag.ToString());
            o.DTCoQuan = txtDTCoQuan2.Text;
            o.DiDong2 = txtDiDong4.Text;
            o.MaKH = MaKH2;
            o.Update4();
        }

        void LoadKhachHang()
        {
            it.KhachHangCls o = new it.KhachHangCls(MaKH);
            txtHoTen.Text = o.HoKH +" "+ o.TenKH;
            txtSoCMND.Text = o.SoCMND;
            if (o.NgayCap.Year != 1)
                dateNgayCap.DateTime = o.NgayCap;
            else
                dateNgayCap.Text = "";

            txtNoiCap.Text = o.NoiCap;
            txtThuongTru.Text = o.ThuongTru;
            txtDienThoaiCD.Text = o.DTCD;
            txtDiDong.Text = o.DiDong;
            txtDiaChiLienHe.Text = o.DiaChi;
            btnDCLH.Tag = o.Xa2.MaXa;
            btnDCLH.Text = o.Xa2.GetAddress();
            btnDCTT.Tag = o.Xa.MaXa;
            btnDCTT.Text = o.Xa.GetAddress();
        }

        void LoadKhachHang2()
        {
            it.KhachHangCls o = new it.KhachHangCls(MaKH2);
            txtHoTen2.Text = o.HoKH + " " + o.TenKH;
            txtSoCMND2.Text = o.SoCMND;
            if (o.NgayCap.Year != 1)
                dateNgayCap2.DateTime = o.NgayCap;
            else
                dateNgayCap2.Text = "";
            txtNoiCap2.Text = o.NoiCap;
            txtDiaChiTT2.Text = o.ThuongTru;
            txtDienThoaiCD2.Text = o.DTCD;
            txtDiDong3.Text = o.DiDong;
            txtDiDong4.Text = o.DiDong;
            txtDiaChiLH2.Text = o.DiaChi;
            btnDCLH2.Tag = o.Xa2.MaXa;
            btnDCLH2.Text = o.Xa2.GetAddress();
            btnDCTT2.Tag = o.Xa.MaXa;
            btnDCTT2.Text = o.Xa.GetAddress();
        }

        void TaoSoPhieu()
        {
            SoPhieu = "";
            it.pdkGiaoDichCls o = new it.pdkGiaoDichCls();
            SoPhieu = txtSoPhieu.Text = o.TaoSoPhieu();
        }

        private void txtHoTen2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                KhachHang.Find_frm frm = new LandSoft.KhachHang.Find_frm();
                frm.ShowDialog();
                if (frm.MaKH != 0)
                {
                    MaKH2 = frm.MaKH;
                    txtHoTen2.Text = frm.HoTen;
                    LoadKhachHang2();
                }
            }
            else
            {
                if (e.Button.Index == 1)
                    //Them nhan vien moi
                    AddNewCustomer2();
                else
                {
                    MaKH2 = 0;
                    LoadKhachHang2();
                }
            }
        }

        private void radioGroupLoaiBDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroupLoaiBDS.SelectedIndex == 8)
                txtLoaiBDSKhac.Enabled = true;
            else
                txtLoaiBDSKhac.Enabled = false;
        }

        private void radioGroupPhapLy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroupPhapLy.SelectedIndex == 11)
                txtPhapLyKhac.Enabled = true;
            else
                txtPhapLyKhac.Enabled = false;
        }

        private void btnNhanVien2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                NhanVien.Select_frm frm = new LandSoft.NhanVien.Select_frm();
                frm.ShowDialog();
                if (frm.MaNV != 0)
                {
                    btnNhanVien2.Text = frm.HoTen;
                    MaNV2 = frm.MaNV;
                }
            }
            else
            {
                MaNV2 = 0;
                btnNhanVien2.Text = "";
            }
        }

        private void spinDonGia_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit _New = (SpinEdit)sender;
            try
            {
                spinTongTien.EditValue = double.Parse(_New.EditValue.ToString()) * double.Parse(lookUpLoaiTien.GetColumnValue("TyGia").ToString()) * double.Parse(spinDai.EditValue.ToString()) * double.Parse(spinRong.EditValue.ToString());
            }
            catch
            {
                spinTongTien.EditValue = 0;
            }
        }

        void SaveBDS()
        {
            try
            {
                it.BatDongSanCls o = new it.BatDongSanCls();
                o.Bep = chkBep.Checked;
                o.Dai = double.Parse(spinDai.EditValue.ToString());
                o.DTPhongKhach = double.Parse(spinDTPhongKhach.EditValue.ToString());
                o.DTSanSau = double.Parse(spinDTSanSau.EditValue.ToString());
                o.DTSanTruoc = double.Parse(spinDTSanTruoc.EditValue.ToString());
                o.DienTichXD = double.Parse(spinDienTichXD.EditValue.ToString());
                o.DuongBen = double.Parse(spinDuongBen.EditValue.ToString());
                o.DuongSau = double.Parse(spinDuongSau.EditValue.ToString());
                o.DuongTruoc = double.Parse(spinDuongTruoc.EditValue.ToString());
                o.PhuongHuong.MaPhuongHuong = (byte)(radioGroupHuong.SelectedIndex + 1);
                o.KhachHang.MaKH = MaKH;
                o.KhachHang2.MaKH = MaKH2;
                o.LoaiBDS.MaLBDS = (byte)(radioGroupLoaiBDS.SelectedIndex + 1);
                o.LoaiBDSKhac = o.LoaiBDS.MaLBDS == 8 ? txtLoaiBDSKhac.Text : "";
                o.LoaiGD.MaLDG = (byte)(radioGroupLoaiGiaoDich.SelectedIndex + 1);
                o.LoaiTien.MaLoaiTien = byte.Parse(lookUpLoaiTien.EditValue.ToString());
                o.NhanVien.MaNV = LandSoft.Library.Common.StaffID;
                o.PhapLy.MaPL = (byte)(radioGroupPhapLy.SelectedIndex + 1);
                o.PhapLyKhac = o.PhapLy.MaPL == 1 ? txtPhapLyKhac.Text : "";
                o.PhongDocSach = chkPhongDocSach.Checked;
                o.PhongKhach = byte.Parse(spinPhongKhach.EditValue.ToString());
                o.PhongNgu = byte.Parse(spinSoPhongNgu.EditValue.ToString());
                o.PhongNGV = chkNguoiGV.Checked;
                o.Toilet = byte.Parse(spinSoPhongWC.EditValue.ToString());
                o.Rong = double.Parse(spinRong.EditValue.ToString());
                o.GiaBan = double.Parse(spinDonGia.EditValue.ToString());
                o.SanSau = chkSanSau.Checked;
                o.SanTruoc = chkSanTruoc.Checked;
                o.TangCao = byte.Parse(spinSoTang.EditValue.ToString());
                o.TinhTrang.MaTT = 1;
                o.ViTriTang = byte.Parse(spinViTri.EditValue.ToString());
                if (MaBDS != "")
                {
                    o.MaBDS = MaBDS;
                    o.UpdateTransaction();
                }
                else
                    MaBDS = o.InsertTransaction();
            }
            catch { MaBDS = ""; error = true; }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            it.KhachHangCls objKH = new it.KhachHangCls();
            if (txtHoTen.Text.Trim() == "")
            {
                DialogBox.Infomation("Vui lòng chọn khách hàng. Xin cảm ơn.");
                txtHoTen.Focus();
                return;
            }

            if (txtSoCMND.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập số chứng minh nhân dân. Xin cảm ơn.");
                txtSoCMND.Focus();
                return;
            }

            objKH.SoCMND = txtSoCMND.Text.Trim();
            if (objKH.SoCMND != "")
            {
                if (objKH.CheckSoCMND2())
                {
                    DialogBox.Infomation("Số CMND <" + txtSoCMND.Text + "> đã có trong hệ thống. Vui lòng nhập lại số CMND. Xin cảm ơn");
                    txtSoCMND.Focus();
                    return;
                }
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

            if (dateNgayKy.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập ngày lập phiếu giao dịch. Xin cảm ơn.");
                dateNgayKy.Focus();
                return;
            }

            if (txtHoTen2.Text.Trim() != "")
            {
                if (txtSoCMND2.Text == "")
                {
                    DialogBox.Infomation("Vui lòng nhập số chứng minh nhân dân. Xin cảm ơn.");
                    txtSoCMND2.Focus();
                    return;
                }

                objKH.SoCMND = txtSoCMND2.Text.Trim();
                if (objKH.SoCMND != "")
                {
                    if (objKH.CheckSoCMND2())
                    {
                        DialogBox.Infomation("Số CMND <" + txtSoCMND2.Text + "> đã có trong hệ thống. Vui lòng nhập lại số CMND. Xin cảm ơn");
                        txtSoCMND2.Focus();
                        return;
                    }
                }

                if (dateNgayCap2.Text == "")
                {
                    DialogBox.Infomation("Vui lòng nhập ngày cấp số chứng minh nhân dân. Xin cảm ơn.");
                    dateNgayCap2.Focus();
                    return;
                }

                if (txtNoiCap2.Text == "")
                {
                    DialogBox.Infomation("Vui lòng nhập nơi cấp số chứng minh nhân dân. Xin cảm ơn.");
                    txtNoiCap2.Focus();
                    return;
                }

                if (txtDiaChiTT2.Text == "")
                {
                    DialogBox.Infomation("Vui lòng nhập địa chỉ thường trú của khách hàng. Xin cảm ơn.");
                    txtDiaChiTT2.Focus();
                    return;
                }

                if (btnDCTT2.Text == "")
                {
                    DialogBox.Infomation("Vui lòng chọn xã, huyện, tỉnh cho địa chỉ thường trú. Xin cảm ơn.");
                    btnDCTT2.Focus();
                    return;
                }

                if (txtDiaChiLH2.Text == "")
                {
                    DialogBox.Infomation("Vui lòng nhập địa chỉ liên hệ của khách hàng. Xin cảm ơn.");
                    txtDiaChiLH2.Focus();
                    return;
                }

                if (btnDCLH2.Text == "")
                {
                    DialogBox.Infomation("Vui lòng chọn xã, huyện, tỉnh cho địa chỉ liên hệ. Xin cảm ơn.");
                    btnDCLH2.Focus();
                    return;
                } 
            }

        doo:
            it.pdkGiaoDichCls o = new it.pdkGiaoDichCls();
            o.SoPhieu = txtSoPhieu.Text;
            o.DiDong1 = txtDiDong1NLH.Text;
            o.DiDong2 = txtDiDong2NLH.Text;
            o.DienGiai = txtDienGiai.Text;
            o.DonGia = double.Parse(spinDonGia.EditValue.ToString());
            o.DTCoQuan = txtDTCoQuanNLH.Text;
            o.DTNha = txtDienThoaiCDNLH.Text;
            o.FileAttach = btnFileAttach.Text;
            o.HoSo1 = txtHS1.Text;
            o.HoSo10 = txtHS10.Text;
            o.HoSo2 = txtHS2.Text;
            o.HoSo3 = txtHS3.Text;
            o.HoSo4 = txtHS4.Text;
            o.HoSo5 = txtHS5.Text;
            o.HoSo6 = txtHS6.Text;
            o.HoSo7 = txtHS7.Text;
            o.HoSo8 = txtHS8.Text;
            o.HoSo9 = txtHS9.Text;
            o.IsVAT = chkVAT.Checked;
            o.KhachHang1.MaKH = MaKH;
            o.KhachHang2.MaKH = MaKH2;
            o.LoaiTien.MaLoaiTien = byte.Parse(lookUpLoaiTien.EditValue.ToString());
            
            o.MoiQuanHe = txtMoiQuanHe.Text;
            o.NgayKy = dateNgayKy.DateTime;
            o.NguoiLienHe = txtHoTen3.Text;
            o.NhanVien1.MaNV = LandSoft.Library.Common.StaffID;
            o.NhanVien2.MaNV = MaNV2;
            o.PhiMoiGioi = double.Parse(spinPhiMoiGioi.EditValue.ToString());
            o.TinhTrang.MaTT = 1;
            o.TongTien = double.Parse(spinTongTien.EditValue.ToString());
            o.TyLe1 = double.Parse(spinTyLe1.EditValue.ToString());
            o.TyLe2 = double.Parse(spinTyLe2.EditValue.ToString());
            o.ThoiHan = (byte)(radioGroupThoiHan.SelectedIndex + 1);
            o.Share = chkPrivate.Checked ? false : true;

            o.Bep = chkBep.Checked;
            o.Dai = double.Parse(spinDai.EditValue.ToString());
            o.DTPhongKhach = double.Parse(spinDTPhongKhach.EditValue.ToString());
            o.DTSanSau = double.Parse(spinDTSanSau.EditValue.ToString());
            o.DTSanTruoc = double.Parse(spinDTSanTruoc.EditValue.ToString());
            o.DienTichXD = double.Parse(spinDienTichXD.EditValue.ToString());
            o.DuongBen = double.Parse(spinDuongBen.EditValue.ToString());
            o.DuongSau = double.Parse(spinDuongSau.EditValue.ToString());
            o.DuongTruoc = double.Parse(spinDuongTruoc.EditValue.ToString());
            o.Huong.MaPhuongHuong = (byte)(radioGroupHuong.SelectedIndex + 1);
            o.KhachHang1.MaKH = MaKH;
            o.KhachHang2.MaKH = MaKH2;
            o.LoaiBDS.MaLBDS = (byte)(radioGroupLoaiBDS.SelectedIndex + 1);
            o.LoaiBDSKhac = o.LoaiBDS.MaLBDS == 8 ? txtLoaiBDSKhac.Text : "";
            o.LGD.MaLDG = (byte)(radioGroupLoaiGiaoDich.SelectedIndex + 1);
            o.LoaiTien.MaLoaiTien = byte.Parse(lookUpLoaiTien.EditValue.ToString());
            o.NhanVien1.MaNV = LandSoft.Library.Common.StaffID;
            o.NhanVien2.MaNV = MaNV2;
            o.PhapLy.MaPL = (byte)(radioGroupPhapLy.SelectedIndex + 1);
            o.PhapLyKhac = o.PhapLy.MaPL == 1 ? txtPhapLyKhac.Text : "";
            o.PhongDocSach = chkPhongDocSach.Checked;
            o.PhongKhach = byte.Parse(spinPhongKhach.EditValue.ToString());
            o.PhongNgu = byte.Parse(spinSoPhongNgu.EditValue.ToString());
            o.PhongNGV = chkNguoiGV.Checked;
            o.PhongWC = byte.Parse(spinSoPhongWC.EditValue.ToString());
            o.Rong = double.Parse(spinRong.EditValue.ToString());
            o.DonGia = double.Parse(spinDonGia.EditValue.ToString());
            o.SanSau = chkSanSau.Checked;
            o.SanTruoc = chkSanTruoc.Checked;
            o.SoTang = byte.Parse(spinSoTang.EditValue.ToString());
            o.TinhTrang.MaTT = 1;
            o.ViTriTang = byte.Parse(spinViTri.EditValue.ToString());

            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            SaveBDS();
            o.MaBDS = MaBDS;

            if (MaGD != 0)
            {
                o.MaGD = MaGD;
                o.Update();
            }
            else
            {
                try
                {                    
                    MaGD = o.Insert();
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Cannot insert duplicate key row in object 'dbo.pdkGiaoDich' with unique index 'IX_pdkGiaoDich'.\r\nThe statement has been terminated.")
                    {
                        TaoSoPhieu();
                        goto doo;
                    }
                    else
                    {
                        error = true;
                        DialogBox.Infomation(ex.Message);
                    }
                }
            }
            if (!error)
            {
                //Them tien ich
                //if (radioGroupLoaiGiaoDich.SelectedIndex == 0 || radioGroupLoaiGiaoDich.SelectedIndex == 2)
                    AddConfortable();
                //else
                //    AddConfortableLand();

                if (OldFileName != btnFileAttach.Text.Trim())
                {
                    //Xoa file cu
                    it.FTPCls objFTP = new it.FTPCls();
                    objFTP.Delete(OldFileName);
                }             
                //Cap nhat thong tin khach hagn
                UpdateCustomer();
                if (MaKH2 != 0)
                    UpdateCustomer2();

                IsUpdate = true;
                DialogBox.Infomation("Dữ liệu đã được lưu.");
                this.Close();
            }
            Cursor.Current = currentCursor;
        }

        void AddConfortable()
        {
            it.pdkgd_TienIchCls objTienIch;

            int i = 0;
            while (chkListTienIch.GetItem(i) != null)
            {
                if (chkListTienIch.GetItemCheckState(i) == CheckState.Checked)
                {
                    objTienIch = new it.pdkgd_TienIchCls();
                    objTienIch.MaPGD = MaGD;
                    objTienIch.MaTienIch = byte.Parse(chkListTienIch.GetItemValue(i).ToString());
                    objTienIch.Insert();
                }
                i++;
            }
        }

        void AddConfortableLand()
        {
            it.BDS_TienIchCls objTienIch;

            int i = 0;
            while (chkListTienIch.GetItem(i) != null)
            {
                if (chkListTienIch.GetItemCheckState(i) == CheckState.Checked)
                {
                    objTienIch = new it.BDS_TienIchCls();
                    objTienIch.MaBDS = MaBDS;
                    objTienIch.MaTienIch = byte.Parse(chkListTienIch.GetItemValue(i).ToString());
                    objTienIch.Insert();
                }
                i++;
            }
        }

        private void lookUpLoaiTien_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit _New = (LookUpEdit)sender;
            try
            {
                spinTongTien.EditValue = double.Parse(spinDonGia.EditValue.ToString()) * double.Parse(_New.GetColumnValue("TyGia").ToString()) * double.Parse(spinDai.EditValue.ToString()) * double.Parse(spinRong.EditValue.ToString());
            }
            catch
            {
                spinTongTien.EditValue = 0;
            }
        }

        private void spinKichThuoc_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit _New = (SpinEdit)sender;
            try
            {
                spinTongTien.EditValue = double.Parse(_New.EditValue.ToString()) * double.Parse(lookUpLoaiTien.GetColumnValue("TyGia").ToString()) * double.Parse(spinDonGia.EditValue.ToString())  * double.Parse(spinRong.EditValue.ToString());
            }
            catch
            {
                spinTongTien.EditValue = 0;
            }
        }

        private void btnFileAttach_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OldFileName = btnFileAttach.Text;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LandSoft.NghiepVu.Khac.InsertImage_frm frm = new LandSoft.NghiepVu.Khac.InsertImage_frm();
                frm.IsLoading = true;
                frm.FileName = ofd.FileName;
                frm.Directory = "httpdocs/upload/pdkgd";
                frm.ShowDialog();
                btnFileAttach.Text = frm.FileName;
            }
        }

        private void chkPrivate_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit _New = (CheckEdit)sender;
            if (_New.Checked)
                chkPublic.Checked = false;
            else
                chkPublic.Checked = true;
        }

        private void chkPublic_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit _New = (CheckEdit)sender;
            if (_New.Checked)
                chkPrivate.Checked = false;
            else
                chkPrivate.Checked = true;
        }

        private void spinRong_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit _New = (SpinEdit)sender;
            try
            {
                spinTongTien.EditValue = double.Parse(_New.EditValue.ToString()) * double.Parse(lookUpLoaiTien.GetColumnValue("TyGia").ToString()) * double.Parse(spinDonGia.EditValue.ToString())  * double.Parse(spinDai.EditValue.ToString());
            }
            catch
            {
                spinTongTien.EditValue = 0;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}