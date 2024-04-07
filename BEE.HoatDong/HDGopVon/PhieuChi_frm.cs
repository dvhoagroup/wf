using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandSoft.NghiepVu.HDGopVon
{
    public partial class PhieuChi_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaHDGV = 0, MaPC = 0, MaKH = 0;
        public string HoTenKH = "";
        public byte DotTT = 0;
        public bool IsUpdate = false, PayDeposit = false;
        string SoPhieu = "";
        public double SoTien = 0;
        public PhieuChi_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadTaiKhoan()
        {
            it.TaiKhoanCls o = new it.TaiKhoanCls();
            lookUpTKCo.Properties.DataSource = o.Select();
            lookUpTKCo.EditValue = "156";
            lookUpTKNo.Properties.DataSource = o.Select();
            lookUpTKNo.EditValue = "131";
        }

        void TaoSoPhieu()
        {
            SoPhieu = "";
            it.hdgvPhieuChiCls o = new it.hdgvPhieuChiCls();
            txtSoPhieu.Text = SoPhieu = o.TaoSoPhieu();
        }

        void LoadData(int _HDGV, byte _DotTT)
        {
            it.hdgvLichThanhToanCls o = new it.hdgvLichThanhToanCls(_HDGV, _DotTT);
            spinSoTien.EditValue = o.SoTien;
            lookUpLoaiTien.EditValue = o.HDGV.LoaiTien.MaLoaiTien;
        }

        void AddNew()
        {
            it.hdgvPhieuChiCls o = new it.hdgvPhieuChiCls();
            o.HDGV.MaHDGV = MaHDGV;
            o.HDGV.Detail();
            dateNgayThu.DateTime = DateTime.Now;
            lookUpLoaiTien.Properties.DataSource = o.HDGV.LoaiTien.Select();
            lookUpLoaiTien.EditValue = o.HDGV.LoaiTien.MaLoaiTien;
            lookUpLoaiTien.Tag = o.HDGV.LoaiTien.MaLoaiTien;
            txtNguoiNop.Text = o.HDGV.KhachHang.GetCustomerPay();
            txtNguoiNop.Tag = o.HDGV.KhachHang.MaKH;
            txtDiaChi.Text = o.HDGV.KhachHang.GetAddress();
            if (PayDeposit)
            {
                txtDienGiai.Text = "Trả lại tiền dư cho khách hàng " + o.HDGV.KhachHang.GetCustomerPay() + ", đợt " + DotTT + " HĐGV số: " + o.HDGV.SoPhieu + " ngày " + o.HDGV.NgayKy.ToString("dd/MM/yyyy");
                spinSoTien.EditValue = SoTien;
                spinSoTien.Properties.MaxValue = (decimal)SoTien;
                spinSoTien.Properties.MinValue = 0;
                spinSoTien.Enabled = false;
            }
            else
                txtDienGiai.Text = "Trả tiền cọc";
        }

        void LoadData()
        {
            it.hdgvPhieuChiCls o = new it.hdgvPhieuChiCls(MaPC);
            txtSoPhieu.Text = o.SoPhieu;
            spinSoTien.EditValue = o.SoTien;
            dateNgayThu.DateTime = o.NgayChi;
            lookUpLoaiTien.Properties.DataSource = o.LoaiTien.Select();
            lookUpLoaiTien.EditValue = o.LoaiTien.MaLoaiTien;
            lookUpTKCo.EditValue = o.TKCo.MaTK;
            lookUpTKNo.EditValue = o.TKNo.MaTK;
            txtNguoiNop.Text = o.NguoiNhan;
            txtDienGiai.Text = o.DienGiai;
            txtDiaChi.Text = o.DiaChi;
            txtChungTu.Text = o.ChungTuGoc;
            MaKH = o.KhachHang.MaKH;
        }

        private void PhieuThu_frm_Load(object sender, EventArgs e)
        {
            LoadTaiKhoan();
            if (MaPC != 0)
                LoadData();
            else
            {
                TaoSoPhieu();
                AddNew();    
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
        doo:
            it.hdgvPhieuChiCls o = new it.hdgvPhieuChiCls();
            o.ChungTuGoc = txtChungTu.Text;
            o.HDGV.MaHDGV = MaHDGV;
            o.DiaChi = txtDiaChi.Text;
            o.DienGiai = txtDienGiai.Text;
            o.NguoiNhan = txtNguoiNop.Text;
            o.SoPhieu = txtSoPhieu.Text;
            o.SoTien = double.Parse(spinSoTien.EditValue.ToString());
            o.TKCo.MaTK = lookUpTKCo.EditValue.ToString();
            o.TKNo.MaTK = lookUpTKNo.EditValue.ToString();
            o.TyGia = double.Parse(lookUpLoaiTien.GetColumnValue("TyGia").ToString());
            o.LoaiTien.MaLoaiTien = byte.Parse(lookUpLoaiTien.EditValue.ToString());
            o.KhachHang.MaKH = MaKH;
            o.NgayChi = dateNgayThu.DateTime;
            o.NhanVien.MaNV = LandSoft.Library.Common.StaffID;
            if (MaPC != 0)
            {
                o.MaPC = MaPC;
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
                    if (ex.Message == "Cannot insert duplicate key row in object 'dbo.hdgvPhieuChi' with unique index 'IX_hdgvPhieuChi'.\r\nThe statement has been terminated.")
                    {
                        TaoSoPhieu();
                        goto doo;
                    }
                }
            }
            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã được lưu.");
            this.Close();
        }
    }
}