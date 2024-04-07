using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using BEEREMA;

namespace BEE.HoatDong.KyGui
{
    public partial class KyGui_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaPKG = 0, MaKH =0, MaHDMB = 0;
        string SoPhieu = "";
        public bool IsUpdate = false;
        public string MaBDS = "", OldFileName = "";
        bool error = false;
        public KyGui_frm()
        {
            InitializeComponent();
        }

        void SetEnable(bool val)
        {
            btnLuu.Enabled = val;
            btnHoan.Enabled = val;
            btnThem.Enabled = !val;
            btnXoa.Enabled = !val;
            btnSua.Enabled = !val;
            btnIn.Enabled = !val;
            txtDiaChiLienHe.Enabled = val;
            txtDiDong.Enabled = val;
            txtDienThoaiCD.Enabled = val;
            txtEmail.Enabled = val;
            lookUpHDMB.Enabled = val;
            lookUpBDS.Enabled = val;
            txtNoiCap.Enabled = val;
            txtNoiSinh.Enabled = val;
            txtSoCMND.Enabled = val;
            txtThuongTru.Enabled = val;
            dateNgayCap.Enabled = val;
            dateNgayKy.Enabled = val;
            dateNgaySinh.Enabled = val;
            btnDCLH.Enabled = val;
            btnDCTT.Enabled = val;
            txtYeuCau.Enabled = val;
        }

        void LoadData()
        {
            it.pkgPhieuKyGuiCls o = new it.pkgPhieuKyGuiCls(MaPKG);
            txtSoPhieu.Text = o.SoPKG;
            txtYeuCau.Text = o.YeuCau;
            dateNgayKy.DateTime = o.NgayKy;
            LoadPKG();
            LoadBDS();
            try
            {
                lookUpBDS.EditValue = MaPKG + ":" + MaHDMB;
            }
            catch { }
            LoadKhachHang();
            try
            {
                lookUpHDMB.EditValue = MaPKG + ":" + MaHDMB;
            }
            catch { }
        }

        void LoadBDS()
        {
            it.BatDongSanCls o = new it.BatDongSanCls(MaBDS);
            o.TangNha.GetBlockID();
            txtTang.Text = o.TangNha.TenTangNha;
            txtBlock.Text = o.TangNha.Blocks.BlockName;
            txtLoaiBDS.Text = o.LoaiCH;
            txtDienTich.Text = o.DienTichChung + " m2";
            txtDienTich.Tag = o.DienTichChung;
            txtDonGia.Text = string.Format("{0:n2} {1}/{2}", o.GiaBan, o.LoaiTien.GetTenLoaiTien(), o.DonViTinh.GetTenDVT());
            txtDonGia.Tag = o.GiaBan;
            txtTongTien.Text = string.Format("{0:n2} {1}", o.DienTichChung * o.GiaBan, o.LoaiTien.GetTenLoaiTien());
            txtTongTien.Tag = o.DienTichChung * o.GiaBan;
        }

        void LoadKhachHang()
        {
            it.KhachHangCls o = new it.KhachHangCls(MaKH);
            txtSoCMND.Text = o.SoCMND;
            dateNgayCap.DateTime = o.NgayCap;
            dateNgaySinh.DateTime = o.NgaySinh;
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

        void LoadHDMB()
        {
            it.HopDongMuaBanCls o = new it.HopDongMuaBanCls();
            lookUpHDMB.Properties.DataSource = o.ListNotInPKG();
            lookUpBDS.Properties.DataSource = o.ListNotInPKG();
        }

        void LoadPKG()
        {
            it.pkgPhieuKyGuiCls o = new it.pkgPhieuKyGuiCls();
            lookUpHDMB.Properties.DataSource = o.ListInPKG();
            lookUpBDS.Properties.DataSource = o.ListInPKG();
        }

        private void KyGui_frm_Load(object sender, EventArgs e)
        {
            SetEnable(true);
            if (MaPKG != 0)
            {
                LoadData();
            }
            else
            {
                TaoSoPhieu();
                LoadHDMB();
                dateNgayKy.DateTime = DateTime.Now;
                if (MaHDMB != 0)
                {
                    lookUpHDMB.EditValue = MaPKG + ":" + MaHDMB;
                    lookUpBDS.EditValue = MaPKG + ":" + MaHDMB;
                    lookUpHDMB.Enabled = false;
                    lookUpBDS.Enabled = false;
                }
            }
        }

        void TaoSoPhieu()
        {
            SoPhieu = "";
            it.pkgPhieuKyGuiCls o = new it.pkgPhieuKyGuiCls();
            SoPhieu = txtSoPhieu.Text = o.TaoSoPhieu();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetEnable(true);
            LoadHDMB();
            TaoSoPhieu();
            dateNgayKy.DateTime = DateTime.Now;
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetEnable(true);
        }

        private void btnHoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetEnable(false);
        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MaPKG != 0)
            {
                PhieuKyGuiCls o = new PhieuKyGuiCls();
                o.Print(MaPKG);
            }
        }

        private void lookUpPhieuGC_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit _KH = (LookUpEdit)sender;
            MaKH = int.Parse(_KH.GetColumnValue("MaKH").ToString());
            MaHDMB = int.Parse(_KH.GetColumnValue("MaHDMB").ToString());
            MaBDS = _KH.GetColumnValue("MaBDS").ToString();
            MaPKG = int.Parse(_KH.GetColumnValue("MaPKG").ToString());
            if (MaPKG != 0)
                LoadData();
            else
            {
                LoadKhachHang();
                lookUpHDMB.EditValue = MaPKG + ":" + MaHDMB;
                LoadBDS();
                lookUpBDS.EditValue = MaPKG + ":" + MaHDMB;
            }
        }

        private void lookUpBDS_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit _BDS = (LookUpEdit)sender;
            MaHDMB = int.Parse(_BDS.GetColumnValue("MaHDMB").ToString());
            MaKH = int.Parse(_BDS.GetColumnValue("MaKH").ToString());
            MaBDS = _BDS.GetColumnValue("MaBDS").ToString();
            MaPKG = int.Parse(_BDS.GetColumnValue("MaPKG").ToString());
            
            if (MaPKG != 0)
                LoadData();
            else
            {
                LoadBDS();
                lookUpBDS.EditValue = MaPKG + ":" + MaHDMB;
                LoadKhachHang();
                lookUpHDMB.EditValue = MaPKG + ":" + MaHDMB;
            }
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            it.pkgPhieuKyGuiCls o = new it.pkgPhieuKyGuiCls();
            o.BDS.MaBDS = MaBDS;
            if (o.Top1NotConfirm())
            {
                DialogBox.Infomation("Bất động sản này đã lập phiếu ký gửi và đang trong tình trạng chờ duyệt. Vui lòng chờ đến lượt giao dịch của bạn, xin cảm ơn.");
                return;
            }

            if (dateNgayKy.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập ngày lập phiếu ký gửi. Xin cảm ơn.");
                dateNgayKy.Focus();
                return;
            }

            if (lookUpHDMB.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn khách hàng để lập phiếu ký gửi. Xin cảm ơn.");
                lookUpHDMB.Focus();
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

            if (lookUpBDS.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn bất động sản để lập phiếu ký gửi. Xin cảm ơn.");
                lookUpBDS.Focus();
                return;
            }
            
        doo:            
            o.KhachHang.MaKH = MaKH;
            o.NgayKy = dateNgayKy.DateTime;
            o.SoPKG = txtSoPhieu.Text;
            o.FileAttach = btnFileAttach.Text;
            o.NhanVien.MaNV = BEE.ThuVien.Common.StaffID;
            o.DaiLy.MaDL = 0;
            o.NVDL.MaNV = 0;
            o.YeuCau = txtYeuCau.Text;
            o.HDMB.MaHDMB = MaHDMB;

            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            
            if (MaPKG != 0)
            {
                o.MaPKG = MaPKG;
                o.Update();
            }
            else
            {
                try
                {
                    o.Template = it.CommonCls.Row("BieuMau_get 6")["NoiDung"].ToString();
                    o.Insert();
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Cannot insert duplicate key row in object 'dbo.pkgPhieuKyGui' with unique index 'IX_pkgPhieuKyGuipgcPhieuDatCoc'.\r\nThe statement has been terminated.")
                    {
                        TaoSoPhieu();
                        goto doo;
                    }
                    else
                        error = true;
                }
            }
            if (!error)
            {
                //Cap nhat thong tin khach hang
                it.KhachHangCls objKH = new it.KhachHangCls();
                objKH.MaKH = MaKH;
                objKH.NoiCap = txtNoiCap.Text;
                if (dateNgayCap.Text != "")
                    objKH.NgayCap = dateNgayCap.DateTime;
                if (dateNgaySinh.Text != "")
                    objKH.NgaySinh = dateNgaySinh.DateTime;
                objKH.NguyenQuan = txtNoiSinh.Text;
                objKH.SoCMND = txtSoCMND.Text;
                objKH.ThuongTru = txtThuongTru.Text;
                objKH.Xa.MaXa = int.Parse(btnDCTT.Tag.ToString());
                objKH.Xa2.MaXa = int.Parse(btnDCLH.Tag.ToString());
                objKH.Email = txtEmail.Text;
                objKH.DiaChi = txtDiaChiLienHe.Text;
                objKH.DiDong = txtDiDong.Text;
                objKH.DTCD = txtDienThoaiCD.Text;
                objKH.Update2();

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
            Cursor.Current = currentCursor;
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

        private void btnFileAttach_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OldFileName = btnFileAttach.Text;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                BEE.NghiepVuKhac.InsertImage_frm frm = new BEE.NghiepVuKhac.InsertImage_frm();
                frm.FileName = ofd.FileName;
                frm.Directory = "httpdocs/upload/pkg";
                frm.ShowDialog();
                btnFileAttach.Text = frm.FileName;
            }
        }
    }
}