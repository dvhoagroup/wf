using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.NhanVien
{
    public partial class NhanVienDL_frm : DevExpress.XtraEditors.XtraForm
    {
        public int KeyID = 0, MaNPP = 0, MaDL = 0;
        public bool IsUpdate = false;
        //bool error = false;
        public NhanVienDL_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadInfo()
        {
            it.NhanVienDaiLyCls o = new it.NhanVienDaiLyCls();
            lookUpChucVu.Properties.DataSource = o.ChucVu.Select();
            lookUpNganHang.Properties.DataSource = o.NganHang.Select();
            lookUpNhomKD.Properties.DataSource = o.NhomKD.Select();
            lookUpPhongBan.Properties.DataSource = o.PhongBan.Select();
            lookUpQuyDanh.Properties.DataSource = o.QuyDanh.Select();
            if (MaNPP != 0)
            {
                lookUpDaiLy.Properties.DataSource = o.DaiLy.SelectNPPShow();
                lblOption.Text = "Nhà PP:";
            }
            else
            {
                lookUpDaiLy.Properties.DataSource = o.DaiLy.SelectShow();
                lblOption.Text = "Đại lý:";
            }
        }

        void TaoMaSo()
        {
            it.NhanVienDaiLyCls o = new it.NhanVienDaiLyCls();
            txtMaSo.Text = o.TaoMaSo();
        }

        private void Huong_frm_Load(object sender, EventArgs e)
        {
            LoadInfo();
            it.NhanVienDaiLyCls o;
            if (KeyID != 0)
            {
                o = new it.NhanVienDaiLyCls(KeyID);
                txtHoTen.Text = o.HoTen;
                txtDienThoai.Text = o.DienThoai;
                txtEmail.Text = o.Email;
                txtHoTen.Text = o.HoTen;
                txtMaSo.Text = o.MaSo;
                txtMaSoThue.Text = o.MaTTNCN;
                txtNoiCap.Text = o.NoiCap;
                txtNguyenQuan.Text = o.HoKhau;
                txtSoCMND.Text = o.SoCMND;
                txtSoTaiKhoan.Text = o.SoTKNH;
                txtThuongTru.Text = o.DiaChi;
                lookUpChucVu.EditValue = o.ChucVu.MaCV;
                lookUpNganHang.EditValue = o.NganHang.MaNH;
                lookUpNhomKD.EditValue = o.NhomKD.MaNKD;
                lookUpPhongBan.EditValue = o.PhongBan.MaPB;
                lookUpQuyDanh.EditValue = o.QuyDanh.MaQD;
                chkClock.Checked = o.Lock;
                if (o.NgayCap.Year != 1)
                    dateNgayCap.DateTime = o.NgayCap;
                else
                    dateNgayCap.Text = "";
                if (o.NgaySinh.Year != 1)
                    dateNgaySinh.DateTime = o.NgaySinh;
                else
                    dateNgaySinh.Text = "";
                lookUpDaiLy.EditValue = o.DaiLy.MaDL;
            }
            else
            {
                lookUpQuyDanh.ItemIndex = 0;
                lookUpPhongBan.ItemIndex = 0;
                lookUpNhomKD.ItemIndex = 0;
                lookUpNganHang.ItemIndex = 0;
                lookUpChucVu.ItemIndex = 0;
                TaoMaSo();
            }

            txtHoTen.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtHoTen.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập họ tên nhân viên. Xin cảm ơn");
                txtHoTen.Focus();
                return;
            }

            if (txtMaSo.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập mã số nhân viên. Xin cảm ơn");
                txtMaSo.Focus();
                return;
            }
            doo:
            it.NhanVienDaiLyCls o = new it.NhanVienDaiLyCls();
            o.HoTen = txtHoTen.Text;
            o.HoKhau = txtNguyenQuan.Text;
            o.Lock = chkClock.Checked;
            o.DiaChi = txtThuongTru.Text;
            o.DienThoai = txtDienThoai.Text;
            o.Email = txtEmail.Text;
            o.ChucVu.MaCV = byte.Parse(lookUpChucVu.EditValue.ToString());
            o.NhomKD.MaNKD = byte.Parse(lookUpNhomKD.EditValue.ToString());
            o.NganHang.MaNH = byte.Parse(lookUpNganHang.EditValue.ToString());
            o.PhongBan.MaPB = byte.Parse(lookUpPhongBan.EditValue.ToString());
            o.QuyDanh.MaQD = byte.Parse(lookUpQuyDanh.EditValue.ToString());
            o.MaSo = txtMaSo.Text;
            o.MatKhau = it.CommonCls.MaHoa(txtMaSo.Text.Trim());
            o.MaTTNCN = txtMaSoThue.Text;
            o.NoiCap = txtNoiCap.Text;
            if (dateNgayCap.Text != "")
                o.NgayCap = dateNgayCap.DateTime;
            if (dateNgaySinh.Text != "")
                o.NgaySinh = dateNgaySinh.DateTime;
            o.SoCMND = txtSoCMND.Text;            
            o.SoTKNH = txtSoTaiKhoan.Text;
            o.Lock = chkClock.Checked;
            o.DaiLy.MaDL = int.Parse(lookUpDaiLy.EditValue.ToString());

            if (KeyID != 0)
            {
                o.MaNV = KeyID;
                o.Update();
            }
            else
            {
                try
                {
                    o.Insert();
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Cannot insert duplicate key row in object 'dbo.NhanVienDaiLy' with unique index 'IX_NhanVienDaiLy'.\r\nThe statement has been terminated.")
                    {
                        DialogBox.Infomation("Mã số <" + txtMaSo.Text + "> đã có trong hệ thống. Vui lòng kiểm tra lại, xin cảm ơn.");
                        TaoMaSo();
                        goto doo;
                    }
                }
            }
            
            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }
    }
}