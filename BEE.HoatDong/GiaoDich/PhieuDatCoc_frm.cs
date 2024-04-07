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
    public partial class PhieuDatCoc_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaGD1 = 0, MaGD2 = 0, MaPDC = 0, MaPGC = 0, MaKH1 = 0, MaKH2 = 0, MaNV1 = 0, MaNV2 = 0;
        public string MaBDS = "", SoPhieu = "", OldFileName = "";
        public bool IsDeposit = false, IsUpdate = false, Share1 = false, Share2 = false;
        bool error = false;
        public PhieuDatCoc_frm()
        {
            InitializeComponent();
        }

        void LoadBDS()
        {
            vGridControl1.DataSource = it.CommonCls.Table("BDS_Broker_getBy '" + MaBDS + "'");

            it.LoaiTienCls o = new it.LoaiTienCls();
            lookUpLoaiTien.Properties.DataSource = o.Select();
            lookUpLoaiTien.ItemIndex = 0;
        }

        void TaoSoPhieu()
        {
            SoPhieu = "";
            it.pdcPhieuDatCocCls o = new it.pdcPhieuDatCocCls();
            SoPhieu = txtSoPhieu.Text = o.TaoSoPhieu();
        }

        void LoadData()
        {
            it.pdcPhieuDatCocCls objPDC = new it.pdcPhieuDatCocCls(MaPDC);
            txtSoPhieu.Text = objPDC.SoPhieu;
            dateNgayKy.DateTime = objPDC.NgayKy;
            spinThoiHan.EditValue = objPDC.ThoiHan;
            btnFileAttach.Text = objPDC.FileAttach;
            spinSoTien.EditValue = objPDC.TienCoc;
            lookUpLoaiTien.Properties.DataSource = objPDC.PGC.LoaiTien.Select();
            lookUpLoaiTien.EditValue = objPDC.PGC.LoaiTien.MaLoaiTien;
            MaKH1 = objPDC.KhachHang.MaKH;
            MaKH2 = objPDC.KhachHang.MaKH;
            MaBDS = objPDC.MaBDS;
        }

        void LoadKhachHang()
        {
            it.KhachHangCls o = new it.KhachHangCls(MaKH1);
            txtHoTen.Text = o.HoKH + " " + o.TenKH;
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

        void LoadLichTT()
        {
            it.pgcPhieuGiuChoCls objPGC = new it.pgcPhieuGiuChoCls();
            gridControl1.DataSource = objPGC.LichThanhToan(MaPGC);
        }

        void LoadComfortable()
        {
            it.pdkgd_TienIchCls o = new it.pdkgd_TienIchCls();
            o.MaPGD = MaGD2;
            gridControlTienIch.DataSource = o.SelectBy();
        }

        private void BienBanDatCoc_frm_Load(object sender, EventArgs e)
        {
            LoadBDS();
            if (MaPDC != 0)
                LoadData();
            else
            {
                TaoSoPhieu();
                spinThoiHan.EditValue = 15;
                dateNgayKy.DateTime = DateTime.Now;
            }
            LoadLichTT();
            LoadKhachHang();
            LoadKhachHang2();
            LoadComfortable();

            if (!Share1)
            {
                pnlKhachHangA.Visible = true;
                LoadNhanVien();
            }
            if (!Share2)
            {
                pnlKhachHangB.Visible = true;
                LoadNhanVien2();
            }
        }

        void LoadNhanVien()
        {
            it.NhanVienCls o = new it.NhanVienCls(MaNV1);
            txtNhanVien1.Text = o.HoTen;
            txtDienThoaiNV1.Text = o.DienThoai;
            txtDiaChiNV2.Text = o.DiaChi;
        }

        void LoadNhanVien2()
        {
            it.NhanVienCls o = new it.NhanVienCls(MaNV2);
            txtNhanVien2.Text = o.HoTen;
            txtDienThoaiNV2.Text = o.DienThoai;
            txtDiaChiNV2.Text = o.DiaChi;
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

        private void picAddress2_DoubleClick(object sender, EventArgs e)
        {
            txtDiaChiLienHe.Text = txtThuongTru.Text;
            btnDCLH.Text = btnDCTT.Text;
            btnDCLH.Tag = btnDCTT.Tag;
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
            o.MaKH = MaKH1;
            o.Update4();
            txtHoTen.Properties.ReadOnly = true;
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

            UpdateCustomer();
            UpdateCustomer2();
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            
            if (!error)
            {
            doo:
                it.pdcPhieuDatCocCls o = new it.pdcPhieuDatCocCls();
                o.MaBDS = MaBDS;
                o.PGC.MaPGC = MaPGC;
                o.KhachHang.MaKH = MaKH1;
                o.NgayKy = dateNgayKy.DateTime.AddSeconds(100);
                o.SoPhieu = txtSoPhieu.Text;
                o.TienCoc = double.Parse(spinSoTien.EditValue.ToString());
                o.FileAttach = btnFileAttach.Text;
                o.ThoiHan = int.Parse(spinThoiHan.EditValue.ToString());
                o.NhanVienKT.MaNV = LandSoft.Library.Common.StaffID;
                o.DaiLy.MaDL = 0;
                o.NhanVienDL.MaNV = 0;
                o.GiaoDich1.MaGD = MaGD1;
                o.GiaoDich2.MaGD = MaGD2;

                if (MaPDC != 0)
                {
                    o.MaPDC = MaPDC;
                    o.NhanVienKD.MaNV = 0;
                    o.Update();
                }
                else
                {
                    try
                    {
                        if (o.Top1NotConfirm(false))
                        {
                            DialogBox.Infomation("Bất động sản này đã lập thỏa thuận đặt cọc và đang trong tình trạng chờ duyệt. Vui lòng chờ đến lượt giao dịch của bạn, xin cảm ơn.");
                            return;
                        }

                        o.NhanVienKD.MaNV = LandSoft.Library.Common.StaffID;
                        o.InsertJumb();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "Cannot insert duplicate key row in object 'dbo.pgcPhieuDatCoc' with unique index 'IX_pgcPhieuDatCoc'.\r\nThe statement has been terminated.")
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
                    //Cap nhat lich thanh toan
                    it.pgcLichThanhToanCls objLich;
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        objLich = new it.pgcLichThanhToanCls();
                        objLich.PGC.MaPGC = MaPGC;
                        objLich.NgayTT = DateTime.Parse(gridView1.GetRowCellValue(i, colNgayTT).ToString());
                        objLich.SoTien = double.Parse(gridView1.GetRowCellValue(i, colSoTien).ToString());
                        objLich.TuongUng = double.Parse(gridView1.GetRowCellValue(i, colTuongUng).ToString());
                        objLich.TyLeTT = byte.Parse(gridView1.GetRowCellValue(i, colTyLe).ToString());
                        objLich.ThueVAT = double.Parse(gridView1.GetRowCellValue(i, colThue).ToString());
                        objLich.TienSDDat = double.Parse(gridView1.GetRowCellValue(i, colTienSDDat).ToString());
                        objLich.DienGiai = gridView1.GetRowCellValue(i, colDienGiai).ToString();
                        objLich.DotTT = byte.Parse(gridView1.GetRowCellValue(i, colDotTT).ToString());
                        objLich.IsPay = false;
                        objLich.Insert();
                    }

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
            }
            Cursor.Current = currentCursor;
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
                frm.Directory = "httpdocs/upload/pdc";
                frm.ShowDialog();
                btnFileAttach.Text = frm.FileName;
            }
        }
    }
}