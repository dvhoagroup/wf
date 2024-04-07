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
    public partial class PhieuThu2_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaPT = 0, MaKH = 0, MaHDGV = 0, MaLDG = 0;
        public string HoTenKH = "", MaBDS = "";
        public byte DotTT = 0;
        public bool IsUpdate = false;
        string SoPhieu = "", SoPhieuChi = "";
        public PhieuThu2_frm()
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

        void TaoSoPhieuChi()
        {
            SoPhieuChi = "";
            it.hdgvPhieuChiCls o = new it.hdgvPhieuChiCls();
            SoPhieuChi = o.TaoSoPhieu();
        }

        void LoadData(int _PGC, byte _DotTT)
        {
            it.hdgvLichThanhToanCls o = new it.hdgvLichThanhToanCls(_PGC, _DotTT);
            spinSoTien.EditValue = o.SoTien;
            lookUpLoaiTien.EditValue = o.HDGV.LoaiTien.MaLoaiTien;
        }

        string GetMaBDS()
        {
            it.BatDongSanCls o = new it.BatDongSanCls(MaBDS);
            return o.MaSo;
        }

        void AddNew()
        {
            it.hdgvLichThanhToanCls o = new it.hdgvLichThanhToanCls();
            o.HDGV.MaHDGV = MaHDGV;
            o.SelectNextPay();
            o.HDGV.Detail();
            spinSoTien.EditValue = o.SoTien;
            spinSoTien.Properties.MaxValue = decimal.Parse(o.SoTien.ToString());
            spinSoTien.Properties.MinValue = 0;
            spinThucThu.EditValue = 0;
            dateNgayThu.DateTime = DateTime.Now;
            lookUpLoaiTien.Properties.DataSource = o.HDGV.LoaiTien.Select();
            lookUpLoaiTien.EditValue = o.HDGV.LoaiTien.MaLoaiTien;
            lookUpLoaiTien.Tag = o.HDGV.LoaiTien.MaLoaiTien;
            o.HDGV.KhachHang.MaKH = MaKH;
            txtNguoiNop.Text = o.HDGV.KhachHang.GetCustomerPay();
            txtDiaChi.Text = o.HDGV.KhachHang.GetAddress();
            DotTT = o.DotTT;
            txtDienGiai.Text = "Thu tiền đợt " + o.DotTT + " căn hộ " + GetMaBDS() + ", HĐGV số: " + o.HDGV.SoPhieu + " ký ngày " + o.HDGV.NgayKy.ToString("dd/MM/yyyy");
        }

        void LoadData()
        {
            it.hdgvPhieuThuCls o = new it.hdgvPhieuThuCls(MaPT);
            txtSoPhieu.Text = o.SoPhieu;
            spinSoTien.EditValue = o.SoTien;
            spinThucThu.EditValue = o.SoTien;
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

        void LoadHinhThuc()
        {
            lookUpHinhThuc.Properties.DataSource = it.CommonCls.Table("HinhThucThanhToan_getAll");
            lookUpHinhThuc.ItemIndex = 0;
        }

        void LoadNganHang()
        {
            it.NganHangCls o = new it.NganHangCls();
            lookUpNganHang.Properties.DataSource = o.Select();
        }

        private void PhieuThu_frm_Load(object sender, EventArgs e)
        {
            LoadHinhThuc();
            LoadTaiKhoan();            
            if (MaPT != 0)
                LoadData();
            else
            {
                TaoSoPhieu();
                AddNew();

                spinThucThu.Properties.MinValue = 0;

                if (double.Parse(spinThucThu.EditValue.ToString()) > double.Parse(spinSoTien.EditValue.ToString()))
                    cmbOption.Properties.ReadOnly = false;
                else
                    cmbOption.Properties.ReadOnly = true;
            }
        }

        bool CheckNextPay()
        {
            it.hdgvLichThanhToanCls o = new it.hdgvLichThanhToanCls();
            o.HDGV.MaHDGV = MaHDGV;
            o.SoTien = double.Parse(spinThucThu.EditValue.ToString());
            o.SelectNextPay2();
            if (o.DotTT > 0)
                return true;
            else
                return false;
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (double.Parse(spinThucThu.EditValue.ToString()) > double.Parse(spinSoTien.EditValue.ToString()))
            {
                if (cmbOption.SelectedIndex == 0)
                {
                    if (!CheckNextPay())
                    {
                        DialogBox.Infomation("Khách hàng <" + txtNguoiNop.Text + "> không còn lịch thanh toán tiếp theo nên không thể chọn <Trừ vào đợt thanh toán tiếp theo>");
                        return;
                    }
                }
            }
            bool error = false;
            if (spinThucThu.Text == "0")
            {
                DialogBox.Infomation("Vui lòng nhập số tiền thực thu. Xin cảm ơn");
                spinThucThu.Focus();
                return;
            }
        doo:
            it.hdgvPhieuThuCls o = new it.hdgvPhieuThuCls();
            o.ChungTuGoc = txtChungTu.Text;
            o.DiaChi = txtDiaChi.Text;
            o.DienGiai = txtDienGiai.Text;
            o.NguoiNop = txtNguoiNop.Text;
            o.SoPhieu = txtSoPhieu.Text;
            o.SoTien = double.Parse(spinThucThu.EditValue.ToString());
            o.TKCo.MaTK = lookUpTKCo.EditValue.ToString();
            o.TKNo.MaTK = lookUpTKNo.EditValue.ToString();
            o.TyGia = double.Parse(lookUpLoaiTien.GetColumnValue("TyGia").ToString());
            o.LoaiTien.MaLoaiTien = byte.Parse(lookUpLoaiTien.EditValue.ToString());
            o.KhachHang.MaKH = MaKH;
            o.NgayThu = dateNgayThu.DateTime;
            o.NhanVien.MaNV = LandSoft.Library.Common.StaffID;
            o.GopVon = double.Parse(spinGonVon.EditValue.ToString());
            o.LaiSuat = double.Parse(spinLaiSuat.EditValue.ToString());
            o.ThueVAT = double.Parse(spinThueVAT.EditValue.ToString());
            o.TienSDDat = double.Parse(spinTienSDDat.EditValue.ToString());
            o.HinhThuc = lookUpHinhThuc.EditValue.ToString() == "0" ? false : true;
            try
            {
                o.MaNH = byte.Parse(lookUpNganHang.EditValue.ToString());
            }
            catch
            {
                o.MaNH = 0;
            }
            if (MaHDGV == 0)
                o.Lich.HDGV.MaHDGV = MaHDGV;

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
                    #region Tru vao dot sau hoac tra lai KH
                    if (double.Parse(spinThucThu.EditValue.ToString()) > double.Parse(spinSoTien.EditValue.ToString()))
                    {
                        if (cmbOption.SelectedIndex == 1)
                        {
                            MaPT = o.Insert();
                        doo2:
                            TaoSoPhieuChi();
                            it.hdgvPhieuChiCls objPC = new it.hdgvPhieuChiCls();
                            objPC.ChungTuGoc = txtChungTu.Text;
                            objPC.HDGV.MaHDGV = MaHDGV;
                            objPC.DiaChi = txtDiaChi.Text;
                            objPC.DienGiai = "Trả lại tiền dư cho khách hàng " + txtNguoiNop.Text;
                            objPC.NguoiNhan = txtNguoiNop.Text;
                            objPC.SoPhieu = SoPhieuChi;
                            objPC.SoTien = double.Parse(spinThucThu.EditValue.ToString()) - double.Parse(spinSoTien.EditValue.ToString());
                            objPC.TKCo.MaTK = lookUpTKCo.EditValue.ToString();
                            objPC.TKNo.MaTK = lookUpTKNo.EditValue.ToString();
                            objPC.KhachHang.MaKH = MaKH;
                            objPC.NgayChi = dateNgayThu.DateTime;
                            objPC.NhanVien.MaNV = LandSoft.Library.Common.StaffID;
                            try
                            {
                                objPC.Insert();
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message == "Cannot insert duplicate key row in object 'dbo.hdgvPhieuChi' with unique index 'IX_hdgvPhieuChi'.\r\nThe statement has been terminated.")
                                    goto doo2;
                                else
                                    error = true;
                            }
                        }
                        else
                        {
                            o.InsertMulti();
                        }
                    }
                    #endregion
                    else
                        o.Insert();
                }
                catch (Exception ex)
                {
                    error = true;
                    if (ex.Message.IndexOf("Cannot insert duplicate key row in object 'dbo.hdgvPhieuThu' with unique index 'IX_hdgvPhieuThu'") >= 0)
                    {
                        DialogBox.Infomation("Phiếu thu <" + txtSoPhieu.Text + "> đã tồn tại trong hệ thống. Hệ thống đã tạo tự động cho bạn số phiếu mới. Vui lòng kiểm tra lại, xin cảm ơn.");
                        txtSoPhieu.Focus();
                        return;
                    }                    
                }
            }

            if (!error)
            {
                IsUpdate = true;
                DialogBox.Infomation("Dữ liệu đã được lưu.");
                this.Close();
            }
        }

        private void spinThucThu_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit _TT = (SpinEdit)sender;
            spinThucThu.EditValue = _TT.EditValue;
            try
            {
                if (double.Parse(_TT.EditValue.ToString()) > double.Parse(spinSoTien.EditValue.ToString()))
                    cmbOption.Properties.ReadOnly = false;
                else
                    cmbOption.Properties.ReadOnly = true;
            }
            catch
            {
                cmbOption.Properties.ReadOnly = true;
                spinThucThu.EditValue = 0;
            }
        }

        private void lookUpHinhThuc_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpHinhThuc.ItemIndex == 1)
            {
                LoadNganHang();
                lookUpNganHang.ItemIndex = 0;
            }
            else
                lookUpNganHang.Properties.DataSource = null;
        }

        private void spinGonVon_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit _Spin = (SpinEdit)sender;
            try
            {
                spinThucThu.EditValue = double.Parse(_Spin.EditValue.ToString()) + double.Parse(spinLaiSuat.EditValue.ToString()) + double.Parse(spinTienSDDat.EditValue.ToString()) + double.Parse(spinThueVAT.EditValue.ToString());
            }
            catch
            {
                spinThucThu.EditValue = double.Parse(spinLaiSuat.EditValue.ToString()) + double.Parse(spinTienSDDat.EditValue.ToString()) + double.Parse(spinThueVAT.EditValue.ToString());
            }
        }

        private void spinThueVAT_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit _Spin = (SpinEdit)sender;
            try
            {
                spinThucThu.EditValue = double.Parse(_Spin.EditValue.ToString()) + double.Parse(spinLaiSuat.EditValue.ToString()) + double.Parse(spinTienSDDat.EditValue.ToString()) + double.Parse(spinGonVon.EditValue.ToString());
            }
            catch
            {
                spinThucThu.EditValue = double.Parse(spinLaiSuat.EditValue.ToString()) + double.Parse(spinTienSDDat.EditValue.ToString()) + double.Parse(spinGonVon.EditValue.ToString());
            }
        }

        private void spinLaiSuat_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit _Spin = (SpinEdit)sender;
            try
            {
                spinThucThu.EditValue = double.Parse(_Spin.EditValue.ToString()) + double.Parse(spinGonVon.EditValue.ToString()) + double.Parse(spinTienSDDat.EditValue.ToString()) + double.Parse(spinThueVAT.EditValue.ToString());
            }
            catch
            {
                spinThucThu.EditValue = double.Parse(spinGonVon.EditValue.ToString()) + double.Parse(spinTienSDDat.EditValue.ToString()) + double.Parse(spinThueVAT.EditValue.ToString());
            }
        }

        private void spinTienSDDat_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit _Spin = (SpinEdit)sender;
            try
            {
                spinThucThu.EditValue = double.Parse(_Spin.EditValue.ToString()) + double.Parse(spinLaiSuat.EditValue.ToString()) + double.Parse(spinGonVon.EditValue.ToString()) + double.Parse(spinThueVAT.EditValue.ToString());
            }
            catch
            {
                spinThucThu.EditValue = double.Parse(spinLaiSuat.EditValue.ToString()) + double.Parse(spinGonVon.EditValue.ToString()) + double.Parse(spinThueVAT.EditValue.ToString());
            }
        }
    }
}