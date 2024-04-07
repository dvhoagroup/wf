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
    public partial class ThanhLy_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaKH = 0, MaHDGV = 0, MaPGC = 0;
        public string HoTen = "", MaBDS = "", MaSo = "", SoPhieu = "", OldFileName = "";
        public bool IsUpdate = false;
        public ThanhLy_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ThanhLy_frm_Load(object sender, EventArgs e)
        {
            it.hdGopVonCls o = new it.hdGopVonCls(MaHDGV);
            txtBDS.Text = MaSo;
            txtBDS.Tag = o.BDS.MaBDS;
            o.KhachHang.MaKH = MaKH;
            txtKhachHang.Text = o.KhachHang.GetCustomerPay();
            //txtKhachHang.Tag = o.KhachHang.MaKH;
            txtSoPhieu.Text = o.SoPhieu;
            dateNgayKy.DateTime = DateTime.Now;
            spinGiaTriHD.EditValue = o.GiaTriHD;            
            spinSoTienGop.EditValue = o.Payment();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (dateNgayKy.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập ngày thanh lý hợp đồng góp vốn. Xin cảm ơn.");
                return;
            }

            it.hdgvThanhLyCls o = new it.hdgvThanhLyCls();
            o.FileAttach = btnFileAttach.Text;
            o.MaBDS = txtBDS.Tag.ToString();
            o.MaHDGV = MaHDGV;
            o.MaKH = MaKH;
            o.MaNV = LandSoft.Library.Common.StaffID;
            o.NoiDung = txtNoiDung.Text;
            o.NgayTL = DateTime.Now;
            o.LaiSuat = double.Parse(spinLaiSuatNH.EditValue.ToString());
            o.ThueTNCN = double.Parse(spinThueThuNhap.EditValue.ToString());
            o.SoTienGop = double.Parse(spinSoTienGop.EditValue.ToString());
            o.GiaTriHD = double.Parse(spinGiaTriHD.EditValue.ToString());
            o.GiaTriHoanTra = double.Parse(spinGiaTriHoanTra.EditValue.ToString());
            o.LoiNhuan = double.Parse(spinLoiNhuan.EditValue.ToString());
            o.LaiSuat2 = double.Parse(spinLaiSuat.EditValue.ToString());
            o.Insert();

            //Chuyen san tinh trang cho thanh ly
            it.hdgvQuaTrinhThucHienCls objHD = new it.hdgvQuaTrinhThucHienCls();
            objHD.MaHDGV = MaHDGV;
            objHD.DienGiai = txtNoiDung.Text;
            objHD.NhanVien.MaNV = LandSoft.Library.Common.StaffID;
            objHD.TinhTrang.MaTT = 6;
            objHD.Insert();

            if (OldFileName != btnFileAttach.Text.Trim())
            {
                //Xoa file cu
                it.FTPCls objFTP = new it.FTPCls();
                objFTP.Delete(OldFileName);
            }

            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã được lưu.");
            this.Close();
        }

        private void spinSoTienGop_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit _SP = (SpinEdit)sender;
            try
            {
                spinGiaTriHoanTra.EditValue = double.Parse(_SP.EditValue.ToString()) + double.Parse(spinLaiSuatNH.EditValue.ToString()) - double.Parse(spinThueThuNhap.EditValue.ToString());
            }
            catch
            {
                _SP.EditValue = 0;
                spinGiaTriHoanTra.EditValue = double.Parse(spinLaiSuatNH.EditValue.ToString()) - double.Parse(spinThueThuNhap.EditValue.ToString());
            }
        }

        private void spinLaiSuatNH_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit _SP = (SpinEdit)sender;
            try
            {
                spinGiaTriHoanTra.EditValue = double.Parse(spinSoTienGop.EditValue.ToString()) + double.Parse(_SP.EditValue.ToString()) - double.Parse(spinThueThuNhap.EditValue.ToString());
            }
            catch
            {
                _SP.EditValue = 0;
                spinGiaTriHoanTra.EditValue = double.Parse(spinSoTienGop.EditValue.ToString()) - double.Parse(spinThueThuNhap.EditValue.ToString());
            }
        }

        private void spinThueThuNhap_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit _SP = (SpinEdit)sender;
            try
            {
                spinGiaTriHoanTra.EditValue = double.Parse(spinSoTienGop.EditValue.ToString()) + double.Parse(spinLaiSuatNH.EditValue.ToString()) - double.Parse(_SP.EditValue.ToString());
            }
            catch
            {
                _SP.EditValue = 0;
                spinGiaTriHoanTra.EditValue = double.Parse(spinSoTienGop.EditValue.ToString()) + double.Parse(spinLaiSuatNH.EditValue.ToString());
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
                frm.FileName = ofd.FileName;
                frm.Directory = "httpdocs/upload/thanhlyhdgv";
                frm.ShowDialog();
                btnFileAttach.Text = frm.FileName;
            }
        }
    }
}