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
    public partial class PhieuThu_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaHDGV = 0, MaPT = 0, MaKH = 0;
        public string HoTenKH = "";
        public byte DotTT = 0;
        public bool IsUpdate = false;
        string SoPhieu = "";
        public PhieuThu_frm()
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
            it.hdgvPhieuThuCls o = new it.hdgvPhieuThuCls();
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
            it.hdgvLichThanhToanCls o = new it.hdgvLichThanhToanCls();
            o.HDGV.MaHDGV = MaHDGV;
            o.SelectNextPay();
            o.HDGV.Detail();
            spinSoTien.EditValue = o.SoTien;
            //spinSoTien.Properties.MaxValue = ̣decimal.Parse(o.SoTien.ToString());
            dateNgayThu.DateTime = DateTime.Now;
            lookUpLoaiTien.Properties.DataSource = o.HDGV.LoaiTien.Select();
            lookUpLoaiTien.EditValue = o.HDGV.LoaiTien.MaLoaiTien;
            lookUpLoaiTien.Tag = o.HDGV.LoaiTien.MaLoaiTien;
            txtNguoiNop.Text = o.HDGV.KhachHang.GetCustomerPay();
            MaKH = o.HDGV.KhachHang.MaKH;
            txtDiaChi.Text = o.HDGV.KhachHang.GetAddress();
            DotTT = o.DotTT;
            txtDienGiai.Text = o.DotTT > 1 ? "Thu tiền đợt " + o.DotTT : "";
        }

        void LoadData()
        {
            it.hdgvPhieuThuCls o = new it.hdgvPhieuThuCls(MaPT);
            txtSoPhieu.Text = o.SoPhieu;
            spinSoTien.EditValue = o.SoTien;
            dateNgayThu.DateTime = o.NgayThu;
            lookUpLoaiTien.Properties.DataSource = o.LoaiTien.Select();
            lookUpLoaiTien.EditValue = o.LoaiTien.MaLoaiTien;
            lookUpTKCo.EditValue = o.TKCo.MaTK;
            lookUpTKNo.EditValue = o.TKNo.MaTK;
            txtNguoiNop.Text = o.NguoiNop;
            txtDienGiai.Text = o.DienGiai;
            txtDiaChi.Text = o.DiaChi;
            txtChungTu.Text = o.ChungTuGoc;
            MaHDGV = o.Lich.HDGV.MaHDGV;
        }

        private void PhieuThu_frm_Load(object sender, EventArgs e)
        {
            LoadTaiKhoan();
            if (MaPT != 0)
            {
                LoadData();
            }
            else
            {
                TaoSoPhieu();
                AddNew();    
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
        doo:
            it.hdgvPhieuThuCls o = new it.hdgvPhieuThuCls();
            o.ChungTuGoc = txtChungTu.Text;
            o.DiaChi = txtDiaChi.Text;
            o.DienGiai = txtDienGiai.Text;
            o.NguoiNop = txtNguoiNop.Text;
            o.SoPhieu = txtSoPhieu.Text;
            o.SoTien = double.Parse(spinSoTien.EditValue.ToString());
            o.TKCo.MaTK = lookUpTKCo.EditValue.ToString();
            o.TKNo.MaTK = lookUpTKNo.EditValue.ToString();
            o.TyGia = double.Parse(lookUpLoaiTien.GetColumnValue("TyGia").ToString());
            o.LoaiTien.MaLoaiTien = byte.Parse(lookUpLoaiTien.EditValue.ToString());
            o.KhachHang.MaKH = MaKH;
            o.NgayThu = dateNgayThu.DateTime;
            o.NhanVien.MaNV = LandSoft.Library.Common.StaffID;
            o.Lich.HDGV.MaHDGV = MaHDGV;
            o.Lich.DotTT = DotTT;
            if (MaPT != 0)
            {
                o.MaPT = MaPT;
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
                    if (ex.Message == "Cannot insert duplicate key row in object 'dbo.hdgvPhieuThu' with unique index 'IX_hdgvPhieuThu'.\r\nThe statement has been terminated.")
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