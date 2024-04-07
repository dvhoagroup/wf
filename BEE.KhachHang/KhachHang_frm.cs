using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;
using BEE.FTP;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class KhachHang_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaKH = 0;
        public bool IsUpdate = false;
        public bool IsPersonal = false;
        public string HoTenKH = "";
        bool Success = true;
        int MaDA = 0, BlockID = 0;
        short? MaHuyen { get; set; }
        short? MaHuyen2 { get; set; }
        byte? MaTinh { get; set; }
        byte? MaTinh2 { get; set; }
        byte STT = 0;
        MasterDataContext db;
        ThuVien.KhachHang objKH;
        public bool IsSave = true;

        public KhachHang_frm()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        void AddNew()
        {
            lookUpMucDich.Properties.DataSource = lookUpMucDich2.Properties.DataSource = from p in db.cdPurposes select new { PurposeID = p.ID, Name = p.Name };
            lookUpLevel.Properties.DataSource = lookUpLevel2.Properties.DataSource = from q in db.cdLevels select new { LevelID = q.ID, Name = q.Name };

            lookUpLoaiHinhKD.Properties.DataSource = db.LoaiHinhKDs;

            lookUpNgheNghiep.Properties.DataSource = db.NgheNghieps;

            lookUpNgheNghiep2.Properties.DataSource = db.NgheNghieps;

            lookUpNhomKH.Properties.DataSource = lookUpNhomKH2.Properties.DataSource = db.NhomKHs;

            lookUpQuyDanh.Properties.DataSource = lookUpQuyDanh2.Properties.DataSource = db.QuyDanhs;

            lookUpThoiDiemLH.Properties.DataSource = lookUpThoiDiemLH2.Properties.DataSource = db.ThoiDiemLienHes;

            lookDuAn.Properties.DataSource = lookDuAn2.Properties.DataSource = db.DuAns;

            lookLoaiBDS.Properties.DataSource = lookLoaiBDS2.Properties.DataSource = db.LoaiBDs;

            txtCodeDIP.Text = it.CommonCls.RandomString(12, true);
            txtCodeDIP2.Text = it.CommonCls.RandomString(12, true);

            dateNgayCap.Datetime = null;
            dateNgayCap2.Datetime = null;
            dateNgaySinh.Datetime = null;
            dateNgaySinh2.Datetime = null;
        }

        void LoadDataPersonal()
        {
            btnDongY.Enabled = IsSave;
            lookUpLoaiHinhKD.Properties.DataSource = db.LoaiHinhKDs;
            lookUpNgheNghiep.Properties.DataSource = db.NgheNghieps;
            lookUpNgheNghiep2.Properties.DataSource = db.NgheNghieps;
            lookUpNhomKH.Properties.DataSource = db.NhomKHs;
            lookUpNhomKH2.Properties.DataSource = db.NhomKHs;
            lookUpQuyDanh.Properties.DataSource = db.QuyDanhs;
            lookUpQuyDanh2.Properties.DataSource = db.QuyDanhs;
            lookUpThoiDiemLH.Properties.DataSource = db.ThoiDiemLienHes;
            lookUpThoiDiemLH2.Properties.DataSource = db.ThoiDiemLienHes;
            lookDuAn.Properties.DataSource = lookDuAn2.Properties.DataSource = db.DuAns;
            lookLoaiBDS.Properties.DataSource = lookLoaiBDS2.Properties.DataSource = db.LoaiBDs;

            lookUpMucDich.Properties.DataSource = lookUpMucDich2.Properties.DataSource = from p in db.cdPurposes select new { PurposeID = p.ID, Name = p.Name };
            lookUpLevel.Properties.DataSource = lookUpLevel2.Properties.DataSource = from q in db.cdLevels select new { LevelID = q.ID, Name = q.Name };


            objKH = db.KhachHangs.Single(p => p.MaKH == this.MaKH);
            lookUpNguonDen.EditValue = (short?)objKH.HowToKnowID;
            lookUpNguonDen2.EditValue = objKH.HowToKnowID;
            lookUpMucDich.EditValue = lookUpMucDich2.EditValue = objKH.PurposeID;

            lookUpLevel.EditValue = lookUpLevel2.EditValue = objKH.LevelID;

            try
            {
                //picLogo.Image = new System.Drawing.Bitmap(new System.IO.MemoryStream(new System.Net.WebClient().DownloadData(objKH.Logo)));
            }
            catch { }
            txtChucVu.Text = txtChucVu2.Text = objKH.ChucVu;
            txtDiaChi.Text = txtDiaChi2.Text = objKH.DiaChi;
            txtDiaChiCT.Text = objKH.DiaChiCT;
            txtDiDong.Text = objKH.DiDong;
            txtDienThoaiCT.Text = objKH.DienThoaiCT;
            // txtDTCD.Text = txtDTCD2.Text = objKH.DTCD;
            txtDienThoai2.Text = objKH.DiDong2;
            txtEmail.Text = txtEmail2.Text = objKH.Email;
            txtFax.Text = objKH.FaxCT;
            txtHoKH.Text = txtHoKH2.Text = HoTenKH = objKH.HoKH;
            txtDienThoai3.Text = objKH.DiDong3;
            txtDiDong4.Text = objKH.DiDong4;
            txtMaSoThueCT.Text = objKH.MaSoThueCT;
            txtNoiCap.Text = txtNoiCap2.Text = objKH.NoiCap;
            txtNguyenQuan.Text = txtNguyenQuan2.Text = objKH.NguyenQuanEN;
            txtSoCMND.Text = txtSoCMND2.Text = objKH.SoCMND;
            txtTenCT.Text = objKH.TenCongTy;
            txtTenThuongHieu.Text = objKH.ThuongHieu;
            txtMoHinh.Text = objKH.MoHinh;
            txtTenKH.Text = txtTenKH2.Text = objKH.TenKH;
            txtThuongTru.Text = txtThuongTru2.Text = objKH.ThuongTru;
            txtYahoo.Text = objKH.Yahoo;
            try
            {
                btnDCLH.Tag = objKH.Xa.MaXa;
            }
            catch { }
            try
            {
                btnDCLH2.Tag = objKH.Xa.MaXa;
            }
            catch { }
            txtSoGPKD.Text = objKH.SoGPKD;
            txtCodeDIP.Text = txtCodeDIP2.Text = objKH.CodeDIP;
            txtCodeSUN.Text = txtCodeSun2.Text = objKH.CodeSUN;
            checkCBCNV.EditValue = objKH.CBCNV;
            txtCongTy1.Text = objKH.CongTy1;
            try
            {
                btnDCLH.Tag = objKH.MaXa;
            }
            catch { }
            try
            {

                btnDCLH2.Tag = objKH.MaXa2;
            }
            catch { }
            try
            {

                btnDCTT.Tag = objKH.Xa.MaXa;
            }
            catch { }
            try
            {
                btnDCTT2.Tag = objKH.Xa.MaXa;
            }
            catch { }
            using (var dbs = new MasterDataContext())
            {
                var dcll = dbs.getDCLL(MaKH);
                try
                {
                    btnDCLH.Text = btnDCLH2.Text = dcll.Replace(txtDiaChi.Text, "");
                }
                catch { btnDCLH.Text = btnDCLH2.Text = dcll; }

                var dctt = dbs.getDCTT(MaKH);
                try
                {
                    btnDCTT.Text = btnDCTT2.Text = dctt.Replace(txtThuongTru.Text, "");
                }
                catch { btnDCTT.Text = btnDCTT2.Text = dctt; }
            }
            MaTinh = objKH.MaTinh;
            MaTinh2 = objKH.MaTinh2;
            MaHuyen = objKH.MaHuyen;
            MaHuyen2 = objKH.MaHuyen2;
            dateNgayCap.Datetime = dateNgayCap2.Datetime = objKH.NgayCap;
            dateNgaySinh.Datetime = dateNgaySinh2.Datetime = objKH.NgaySinh;
            lookUpLoaiHinhKD.EditValue = objKH.MaLHKD;
            lookUpNgheNghiep.EditValue = lookUpNgheNghiep2.EditValue = objKH.MaNN;
            lookUpNhomKH.EditValue = lookUpNhomKH2.EditValue = objKH.MaNKH;
            lookUpQuyDanh.EditValue = lookUpQuyDanh2.EditValue = objKH.MaQD;
            lookUpThoiDiemLH.EditValue = lookUpThoiDiemLH2.EditValue = objKH.MaTDLH;
            lookLoaiBDS.EditValue = lookLoaiBDS2.EditValue = objKH.MaLBDS;
            lookDuAn.EditValue = lookDuAn2.EditValue = objKH.MaDA;
            spinThanhVien.EditValue = spinThanhVien2.EditValue = objKH.SoThanhVien;
            spinMucThuNhap.EditValue = spinMucThuNhap2.EditValue = objKH.MucThuNhap;
            lookUpQuocTich.EditValue = objKH.MaQG;
            lookUpQuocGia2.EditValue = objKH.MaQG;
            txtSoDTKhanCap.Text = objKH.SoDTKhanCap;
            dateNgayCapGPKD.EditValue = objKH.NgayCapGDKKD;
            dateNgayCapGPKD2.EditValue = objKH.NgayCapGDKKD2;
            txtNoiCapGPKD.Text = objKH.NoiCapGDKKD;
            txtSoTaiKhoan.Text = objKH.SoTaiKhoan;
            lookUpNganHang.EditValue = objKH.MaNH;
            lookUpChiNhanh.EditValue = objKH.MaCN;
        }

        //void LoadImage()
        //{
        //    try
        //    {
        //        picLogo.Image = new System.Drawing.Bitmap(new System.IO.MemoryStream(new System.Net.WebClient().DownloadData(objTN.Logo)));
        //    }
        //    catch { }
        //}

        private void KhachHang_frm_Load(object sender, EventArgs e)
        {
            lookUpNguonDen.Properties.DataSource = db.mglNguons.Select(p => new { HowToKnowID = p.MaNguon, Name = p.TenNguon });
            lookUpNguonDen2.Properties.DataSource = db.mglNguons.Select(p => new { HowToKnowID = p.MaNguon, Name = p.TenNguon });

            var listQG = db.QuocGias.ToList();
            lookUpQuocTich.Properties.DataSource = listQG;
            lookUpQuocTich.ItemIndex = 0;
            lookUpQuocGia2.Properties.DataSource = listQG;
            lookUpQuocGia2.ItemIndex = 0;
            lookUpNganHang.Properties.DataSource = db.khNganHangs;


            if (BEE.NgonNgu.Language.LangID != 1)
                TranslateEN();
            if (MaKH != 0)
            {
                xtraTabControl1.SelectedTabPageIndex = 1;
                xtraTabControl1.SelectedTabPageIndex = 0;
                tabSubCompany.SelectedTabPageIndex = 1;
                tabSubCompany.SelectedTabPageIndex = 0;
                LoadDataPersonal();
                if (IsPersonal)
                    xtraTabControl1.SelectedTabPageIndex = 0;
                else
                    xtraTabControl1.SelectedTabPageIndex = 1;
            }
            else
            {
                xtraTabControl1.SelectedTabPageIndex = 1;
                xtraTabControl1.SelectedTabPageIndex = 0;
                tabSubCompany.SelectedTabPageIndex = 1;
                tabSubCompany.SelectedTabPageIndex = 0;
                objKH = new ThuVien.KhachHang();
                AddNew();
            }
        }

        void TranslateEN()
        {
            lblChiNhanh.Text = lblChiNhanh.Tag.ToString();
            lblChucVu.Text = lblChucVu.Tag.ToString();
            lblChucVuCT.Text = lblChucVuCT.Tag.ToString();
            lblDCLL.Text = lblDCLL.Tag.ToString();
            lblDCLLCT.Text = lblDCLLCT.Tag.ToString();
            lblDCTT.Text = lblDCTT.Tag.ToString();
            lblDCTTCT.Text = lblDCTTCT.Tag.ToString();
            lblDiaChi.Text = lblDiaChi.Tag.ToString();
            lblDiDong.Text = lblDiDong.Tag.ToString();
            lblDiDongCT.Text = lblDiDongCT.Tag.ToString();
            lblDienThoaiCT.Text = lblDienThoaiCT.Tag.ToString();
            lblDonViCongTac.Text = lblDonViCongTac.Tag.ToString();
            lblDTCD.Text = lblDTCD.Tag.ToString();
            lblDTCDCT.Text = lblDTCDCT.Tag.ToString();
            lblDuAn.Text = lblDuAn.Tag.ToString();
            lblDuAnCT.Text = lblDuAnCT.Tag.ToString();

            lblFirstName.Text = lblFirstName.Tag.ToString();
            lblQuyDanh2.Text = lblQuyDanh2.Tag.ToString();
            lblLastName.Text = lblLastName.Tag.ToString();
            lblTen2.Text = lblTen2.Tag.ToString();
            lblLoaiBDS.Text = lblLoaiBDS.Tag.ToString();
            lblLoaiBDSCT.Text = lblLoaiBDSCT.Tag.ToString();
            lblLoaiHinhKD.Text = lblLoaiHinhKD.Tag.ToString();
            lblMaSoThue.Text = lblMaSoThue.Tag.ToString();
            lblMaSoThueCT.Text = lblMaSoThueCT.Tag.ToString();
            lblMaSoThueCT2.Text = lblMaSoThueCT2.Tag.ToString();
            lblMucThuNhaCT.Text = lblMucThuNhaCT.Tag.ToString();
            lblMucThuNhap.Text = lblMucThuNhap.Tag.ToString();
            lblNganHang.Text = lblNganHang.Tag.ToString();
            lblNgayCap.Text = lblNgayCap.Tag.ToString();
            lblNgayCapCT.Text = lblNgayCapCT.Tag.ToString();
            lblNgayCapGPKD.Text = lblNgayCapGPKD.Tag.ToString();
            lblNgaySinh.Text = lblNgaySinh.Tag.ToString();
            lblNgaySinhCT.Text = lblNgaySinhCT.Tag.ToString();
            lblNgheNghiep.Text = lblNgheNghiep.Tag.ToString();
            lblNgheNghiepCT.Text = lblNgheNghiepCT.Tag.ToString();
            lblNhomKHCT.Text = lblNhomKHCT.Tag.ToString();
            lblNoiCap.Text = lblNoiCap.Tag.ToString();
            lblNoiCapCT.Text = lblNoiCapCT.Tag.ToString();
            lblNoiSinh.Text = lblNoiSinh.Tag.ToString();
            lblNoiSinh2.Text = lblNoiSinh2.Tag.ToString();
            lblQuocTich.Text = lblQuocTich.Tag.ToString();
            lblSanPham.Text = lblSanPham.Tag.ToString();
            lblSanPhamCT.Text = lblSanPhamCT.Tag.ToString();
            lblSLThanhVien.Text = lblSLThanhVien.Tag.ToString();
            lblSLThanhVienCT.Text = lblSLThanhVienCT.Tag.ToString();
            lblSoCMND.Text = lblSoCMND.Tag.ToString();
            lblSoCMNDCT.Text = lblSoCMNDCT.Tag.ToString();
            lblSoDTKhanCap.Text = lblSoDTKhanCap.Tag.ToString();
            lblSoGPKD.Text = lblSoGPKD.Tag.ToString();
            lblSoTaiKhoan.Text = lblSoTaiKhoan.Tag.ToString();
            lblTenCongTy.Text = lblTenCongTy.Tag.ToString();
            lblTenCongTyEN.Text = lblTenCongTyEN.Tag.ToString();
            lblThoiDiemLH.Text = lblThoiDiemLH.Tag.ToString();
            lblThoiDienLHCT.Text = lblThoiDienLHCT.Tag.ToString();
            lblTuyChon.Text = lblTuyChon.Tag.ToString();
            lblXHT_DCLL.Text = lblXHT_DCLL.Tag.ToString();
            lblXHT_DCLLCT.Text = lblXHT_DCLLCT.Tag.ToString();
            lblXHT_DCTT.Text = lblXHT_DCTT.Tag.ToString();
            lblXHT_DCTTCT.Text = lblXHT_DCTTCT.Tag.ToString();
            checkCBCNV.Text = checkCBCNV.Tag.ToString();

            tabCompany.Text = tabCompany.Tag.ToString();
            tabPersonal.Text = tabPersonal.Tag.ToString();
            tabSubCompany_General.Text = tabSubCompany_General.Tag.ToString();
            tabSubCompany_Representative.Text = tabSubCompany_Representative.Tag.ToString();

            this.Text = "Add new Customer";
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMaXa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCLH.Tag = frm.MaXa;
                btnDCLH.Text = frm.Result;
                MaHuyen2 = frm.MaHuyen;
                MaTinh2 = frm.MaTinh;
            }
        }

        private void btnMaXa2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCLH2.Tag = frm.MaXa;
                btnDCLH2.Text = frm.Result;
                MaHuyen2 = frm.MaHuyen;
                MaTinh2 = frm.MaTinh;
            }
        }

        private void lookUpNgheNghiep_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                NgheNghiep_frm frm = new NgheNghiep_frm();
                frm.ShowDialog();
                if (frm.IsUpdate)
                {
                    it.NgheNghiepCls o = new it.NgheNghiepCls();
                    lookUpNgheNghiep.Properties.DataSource = o.Select();
                    lookUpNgheNghiep2.Properties.DataSource = o.Select();
                }
            }
        }

        private void lookUpNhomKH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                NhomKH_frm frm = new NhomKH_frm();
                frm.ShowDialog();
                if (frm.IsUpdate)
                {
                    it.NhomKHCls o = new it.NhomKHCls();
                    lookUpNhomKH.Properties.DataSource = o.Select();
                    lookUpNhomKH2.Properties.DataSource = o.Select();
                }
            }
        }

        private void lookUpLoaiHinhKD_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                LoaiHinhKD_frm frm = new LoaiHinhKD_frm();
                frm.ShowDialog();
                if (frm.IsUpdate)
                {
                    it.LoaiHinhKDCls o = new it.LoaiHinhKDCls();
                    lookUpLoaiHinhKD.Properties.DataSource = o.Select();
                }
            }
        }

        private void lookUpNgheNghiep2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                NgheNghiep_frm frm = new NgheNghiep_frm();
                frm.ShowDialog();
                if (frm.IsUpdate)
                {
                    it.NgheNghiepCls o = new it.NgheNghiepCls();
                    lookUpNgheNghiep2.Properties.DataSource = o.Select();
                    lookUpNgheNghiep.Properties.DataSource = o.Select();
                }
            }
        }

        private void lookUpNhomKH2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                NhomKH_frm frm = new NhomKH_frm();
                frm.ShowDialog();
                if (frm.IsUpdate)
                {
                    it.NhomKHCls o = new it.NhomKHCls();
                    lookUpNhomKH2.Properties.DataSource = o.Select();
                    lookUpNhomKH.Properties.DataSource = o.Select();
                }
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            Success = false;
            if (xtraTabControl1.SelectedTabPageIndex == 0)
                SavePersonal();
            else
                SaveCompany();
            if (Success)
            {
                IsUpdate = true;
                DialogResult = System.Windows.Forms.DialogResult.OK;
                DialogBox.Save();
                this.Close();
            }
        }

        void SaveCompany()
        {
            #region ràng buộc
            if (txtTenCT.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập [Tên công ty], xin cảm ơn.\r\n\r\nPlease type [Company Name], thank!");
                tabSubCompany.SelectedTabPageIndex = 0;
                txtTenCT.Focus();
                return;
            }

            if (lookUpLoaiHinhKD.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn [Loại hình kinh doanh] của công ty, xin cảm ơn.\r\n\r\nPlease type [Type of Business], thank!");
                tabSubCompany.SelectedTabPageIndex = 0;
                lookUpLoaiHinhKD.Focus();
                return;
            }

            if (txtHoKH2.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập [Họ và tên đệm], xin cảm ơn.\r\n\r\nPlease type [Fist Name], thank!");
                tabSubCompany.SelectedTabPageIndex = 1;
                txtHoKH2.Focus();
                return;
            }

            if (txtTenKH2.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập [Tên], xin cảm ơn.\r\n\r\nPlease type [Last Name], thank!");
                tabSubCompany.SelectedTabPageIndex = 1;
                txtTenKH2.Focus();
                return;
            }

            var soGPKD = txtSoCMND.Text.Trim();
            if (soGPKD != "")
            {
                var count = db.KhachHangs.Where(p => p.SoGPKD == soGPKD & p.MaKH != objKH.MaKH).Count();
                if (count > 0)
                {
                    DialogBox.Error("Số SoGPKD [" + txtSoGPKD.Text + "] đã có trong hệ thống. Vui lòng nhập lại số SoGPKD. Xin cảm ơn.");
                    txtSoGPKD.Focus();
                    return;
                }
            }

            if (CheckAllPhoneExits(txtDienThoaiCT.Text.Trim()))
            {
                DialogBox.Error("Số điện thoại [" + txtDienThoaiCT.Text + "] đã có trong hệ thống. Vui lòng nhập lại, xin cảm ơn.");
                txtDienThoaiCT.Focus();
                return;
            }
            #endregion

            objKH.ChucVu = txtChucVu2.Text;
            objKH.DiaChi = txtDiaChi2.Text;
            objKH.ThuongTru = txtThuongTru2.Text;
            objKH.DienThoaiCT = txtDiDong.Text;
            objKH.DiDong2 = txtDienThoai2.Text;
            objKH.DiDong3 = txtDienThoai3.Text;
            objKH.DiDong4 = txtDiDong4.Text;

            objKH.DTCD = txtDTCD2.Text;
            objKH.Email = txtEmail2.Text;
            objKH.GhiChu = "";
            objKH.HoKH = txtHoKH2.Text.Trim();
            objKH.TenKH = txtTenKH2.Text.Trim();
            objKH.MaTDLH = (byte?)lookUpThoiDiemLH2.EditValue;
            objKH.CodeDIP = txtCodeDIP2.Text;
            objKH.CodeSUN = txtCodeSun2.Text;
            if (btnDCTT2.Text != "")
                objKH.MaXa = int.Parse(btnDCTT2.Tag.ToString());
            if (btnDCLH2.Text != "")
                objKH.MaXa2 = int.Parse(btnDCLH2.Tag.ToString());
            objKH.Yahoo = "";
            objKH.MaSoTTNCN = txtMaSoThue2.Text;
            objKH.NoiCap = txtNoiCap2.Text;
            if (dateNgayCap2.Datetime != null)
                objKH.NgayCap = dateNgayCap2.Datetime.Value;
            objKH.dd2 = dateNgayCap2.Ngay == "dd" ? "" : dateNgayCap2.Ngay;
            objKH.MM2 = dateNgayCap2.Thang == "MM" ? "" : dateNgayCap2.Thang;
            objKH.yyyy2 = dateNgayCap2.Nam == "yyyy" ? "" : dateNgayCap2.Nam;
            if (dateNgaySinh2.Datetime != null)
                objKH.NgaySinh = dateNgaySinh2.Datetime.Value;
            objKH.dd = dateNgaySinh2.Ngay == "dd" ? "" : dateNgaySinh2.Ngay;
            objKH.MM = dateNgaySinh2.Thang == "MM" ? "" : dateNgaySinh2.Thang;
            objKH.yyyy = dateNgaySinh2.Nam == "yyyy" ? "" : dateNgaySinh2.Nam;
            objKH.MaNN = (byte?)lookUpNgheNghiep2.EditValue;
            objKH.NguyenQuan = txtNguyenQuan2.Text;
            objKH.MaNKH = (byte?)lookUpNhomKH2.EditValue;
            objKH.MaQD = (byte?)lookUpQuyDanh2.EditValue;
            objKH.SoCMND = txtSoCMND2.Text;
            objKH.IsPersonal = false;
            objKH.IsAvatar = false;
            objKH.DiaChiCT = txtDiaChiCT.Text;
            objKH.DienThoaiCT = txtDienThoaiCT.Text;
            objKH.FaxCT = txtFax.Text;

            objKH.TenCongTy = txtTenCT.Text.Trim();
            objKH.ThuongHieu = txtTenThuongHieu.Text.Trim();
            objKH.MaSoThueCT = txtMaSoThueCT.Text;
            objKH.MaLHKD = (byte?)lookUpLoaiHinhKD.EditValue;
            objKH.SoGPKD = txtSoGPKD.Text;
            objKH.MaHuyen = MaHuyen;
            objKH.MaHuyen2 = MaHuyen2;
            objKH.MaTinh = MaTinh;
            objKH.MaTinh2 = MaTinh2;
            objKH.MucThuNhap = (decimal?)spinMucThuNhap2.EditValue;
            objKH.SoThanhVien = Convert.ToByte(spinThanhVien2.EditValue);
            objKH.SanPham = txtSanPham2.Text;
            objKH.SoDTKhanCap = "";
            objKH.QuocTich = "";
            objKH.NgayCapGDKKD = (DateTime?)dateNgayCapGPKD.EditValue;
            objKH.NgayCapGDKKD2 = (DateTime?)dateNgayCapGPKD2.EditValue;
            objKH.NoiCapGDKKD = txtNoiCapGPKD.Text.Trim();
            objKH.SoTaiKhoan = txtSoTaiKhoan.Text.Trim();
            objKH.MaNH = (int?)lookUpNganHang.EditValue;
            objKH.MaCN = (int?)lookUpChiNhanh.EditValue;
            objKH.MaDA = (int?)lookDuAn2.EditValue;
            objKH.MaLBDS = (short?)lookLoaiBDS2.EditValue;
            objKH.CodeDIP = txtCodeDIP2.Text.Trim();
            objKH.CodeSUN = txtCodeSun2.Text.Trim();
            objKH.MaQG = (int?)lookUpQuocGia2.EditValue;
            objKH.MaTT = (byte?)2;
            if ((short?)lookUpNguonDen2.EditValue > (short?)0)
            {
                objKH.HowToKnowID = (short?)lookUpNguonDen2.EditValue;
            }
            else
            {
                DialogBox.Error("Bạn chưa chọn nguồn đến!");
                return;
            }

            if (MaKH != 0)
            {
                Success = true;
            }
            else
            {
                try
                {
                    objKH.MaNV = Common.StaffID;
                    objKH.NgayDangKy = DateTime.Now;
                    db.KhachHangs.InsertOnSubmit(objKH);
                    Success = true;
                }
                catch (Exception)
                {
                    Success = false;
                }
            }
            db.SubmitChanges();
            this.MaKH = objKH.MaKH;

        }

        void SavePersonal()
        {
            #region ràng buộc
            if (txtHoKH.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập [Họ và tên đệm]. Xin cảm ơn.\r\n\r\nPlease type [Fist Name], thank!");
                txtHoKH.Focus();
                return;
            }

            if (txtTenKH.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập [Tên khách hàng]. Xin cảm ơn.\r\n\r\nPlease type [Last Name], thank!");
                txtTenKH.Focus();
                return;
            }

            var soCMND = txtSoCMND.Text.Trim();
            if (soCMND != "")
            {
                var count = db.KhachHangs.Where(p => p.SoCMND == soCMND & p.MaKH != objKH.MaKH).Count();
                if (count > 0)
                {
                    DialogBox.Error("Số CMND [" + txtSoCMND.Text + "] đã có trong hệ thống. Vui lòng nhập lại số CMND. Xin cảm ơn.");
                    txtSoCMND.Focus();
                    return;
                }
            }

            var diDong = txtDiDong.Text.Trim();
            if (diDong == "")
            {
                DialogBox.Infomation("Vui lòng nhập [Số điện thoại]. Xin cảm ơn.\r\n\r\nPlease type [Phone number], thank!");
                txtDiDong.Focus();
                return;
            }
            else
            {
                var exitPhone = CheckAllPhoneExits(diDong);
                if (exitPhone)
                {
                    DialogBox.Error("Số điện thoại [" + txtDiDong.Text + "] đã có trong hệ thống. Vui lòng nhập lại, xin cảm ơn.");
                    txtDiDong.Focus();
                    return;
                }
            }
            
            if (CheckAllPhoneExits(txtDienThoai2.Text.Trim()))
            {
                DialogBox.Error("Số điện thoại [" + txtDienThoai2.Text + "] đã có trong hệ thống. Vui lòng nhập lại, xin cảm ơn.");
                txtDienThoai2.Focus();
                return;
            }

            if (CheckAllPhoneExits(txtDienThoai3.Text.Trim()))
            {
                DialogBox.Error("Số điện thoại [" + txtDienThoai3.Text + "] đã có trong hệ thống. Vui lòng nhập lại, xin cảm ơn.");
                txtDienThoai2.Focus();
                return;
            }
            if (CheckAllPhoneExits(txtDiDong4.Text.Trim()))
            {
                DialogBox.Error("Số điện thoại [" + txtDiDong4.Text + "] đã có trong hệ thống. Vui lòng nhập lại, xin cảm ơn.");
                txtDiDong4.Focus();
                return;
            }

            var email = txtEmail.Text.Trim();
            if (email != "")
            {
                var count = db.KhachHangs.Where(p => p.Email == email & p.MaKH != objKH.MaKH).Count();
                if (count > 0)
                {
                    DialogBox.Error("Email [" + txtEmail.Text + "] đã có trong hệ thống. Vui lòng nhập lại, xin cảm ơn.");
                    txtEmail.Focus();
                    return;
                }
            }


            if (lookUpNhomKH.EditValue == null)
            {
                DialogBox.Infomation("Vui lòng chọn [Nhóm khách hàng], xin cảm ơn!");
                lookUpNhomKH.Focus();
                return;
            }

            #endregion

            //objKH.Logo = picLogo.Tag == null ? "" : picLogo.Tag.ToString();
            objKH.ChucVu = txtChucVu.Text;
            objKH.DiaChi = txtDiaChi.Text;
            objKH.ThuongTru = txtThuongTru.Text;
            objKH.DiDong = txtDiDong.Text;
            objKH.DTCD = "";
            objKH.DiDong2 = txtDienThoai2.Text.Trim();
            objKH.Email = txtEmail.Text;
            objKH.GhiChu = "";
            objKH.HoKH = txtHoKH.Text.Trim();
            objKH.TenKH = txtTenKH.Text.Trim();
            objKH.MaTDLH = (byte?)lookUpThoiDiemLH.EditValue;
            objKH.MoHinh = txtMoHinh.Text;
            objKH.DiDong4 = txtDiDong4.Text.Trim();

            if (btnDCTT.Text != "")
                objKH.Xa = db.Xas.First(p => p.MaXa == int.Parse(btnDCTT.Tag.ToString()));
            if (btnDCLH.Text != "")
                objKH.Xa1 = db.Xas.First(p => p.MaXa == int.Parse(btnDCTT.Tag.ToString()));
            objKH.Yahoo = txtYahoo.Text;
            objKH.DiDong3 = txtDienThoai3.Text.Trim();
            objKH.MaSoTTNCN = "";
            objKH.NoiCap = txtNoiCap.Text;
            if (dateNgayCap.Datetime != null)
                objKH.NgayCap = dateNgayCap.Datetime.Value;
            objKH.dd2 = dateNgayCap.Ngay == "dd" ? "" : dateNgayCap.Ngay;
            objKH.MM2 = dateNgayCap.Thang == "MM" ? "" : dateNgayCap.Thang;
            objKH.yyyy2 = dateNgayCap.Nam == "yyyy" ? "" : dateNgayCap.Nam;
            if (dateNgaySinh.Datetime != null)
                objKH.NgaySinh = dateNgaySinh.Datetime.Value;
            objKH.dd = dateNgaySinh.Ngay == "dd" ? "" : dateNgaySinh.Ngay;
            objKH.MM = dateNgaySinh.Thang == "MM" ? "" : dateNgaySinh.Thang;
            objKH.yyyy = dateNgaySinh.Nam == "yyyy" ? "" : dateNgaySinh.Nam;

            objKH.MaNN = (byte?)lookUpNgheNghiep.EditValue;
            objKH.NguyenQuan = txtNguyenQuan.Text;
            objKH.MaNKH = (byte?)lookUpNhomKH.EditValue;
            objKH.MaQD = (byte?)lookUpQuyDanh.EditValue;
            try
            {
                objKH.MaDA = (int?)lookDuAn.EditValue;
                objKH.MaLBDS = (short?)lookLoaiBDS.EditValue;
            }
            catch { }
            objKH.SoCMND = txtSoCMND.Text.Trim();
            objKH.IsPersonal = true;

            objKH.CBCNV = checkCBCNV.Checked;
            objKH.CongTy1 = txtCongTy1.Text;
            objKH.MaHuyen = (short?)MaHuyen;
            objKH.MaHuyen2 = (short?)MaHuyen2;
            objKH.MaTinh = MaTinh;
            objKH.MaTinh2 = MaTinh2;
            objKH.MucThuNhap = (decimal?)spinMucThuNhap.EditValue;
            objKH.SoThanhVien = Convert.ToByte(spinThanhVien.EditValue);
            objKH.SanPham = txtSanPham.Text;
            objKH.SoDTKhanCap = txtSoDTKhanCap.Text.Trim();
            objKH.QuocTich = "";
            objKH.CodeDIP = txtCodeDIP.Text.Trim();
            objKH.CodeSUN = txtCodeSUN.Text.Trim();
            objKH.MaQG = (int?)lookUpQuocTich.EditValue;
            objKH.MaTT = (byte?)2;
            if (lookUpNguonDen.ItemIndex != -1)
            {
                objKH.HowToKnowID = (short?)lookUpNguonDen.EditValue;
            }
            else
            {
                DialogBox.Error("Bạn chưa chọn nguồn đến!");
                return;
            }

            if (MaKH != 0)
            {
                Success = true;
            }
            else
            {
                objKH.MaNV = Common.StaffID;
                objKH.NgayDangKy = DateTime.Now;
                db.KhachHangs.InsertOnSubmit(objKH);
                Success = true;
            }
            db.SubmitChanges();
            this.MaKH = objKH.MaKH;
        }

        private void lookUpThoiDiemLH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                ThoiDiemLienLac_frm frm = new ThoiDiemLienLac_frm();
                frm.ShowDialog();
                if (frm.IsUpdate)
                {
                    it.ThoiDiemLienHeCls o = new it.ThoiDiemLienHeCls();
                    lookUpThoiDiemLH.Properties.DataSource = o.Select();
                    lookUpThoiDiemLH2.Properties.DataSource = o.Select();
                }
            }
        }

        private void lookUpThoiDiemLH2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                ThoiDiemLienLac_frm frm = new ThoiDiemLienLac_frm();
                frm.ShowDialog();
                if (frm.IsUpdate)
                {
                    it.ThoiDiemLienHeCls o = new it.ThoiDiemLienHeCls();
                    lookUpThoiDiemLH.Properties.DataSource = o.Select();
                    lookUpThoiDiemLH2.Properties.DataSource = o.Select();
                }
            }
        }

        private void chkAvatar_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnDCTT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCTT.Tag = frm.MaXa;
                btnDCTT.Text = frm.Result;
                MaHuyen = frm.MaHuyen;
                MaTinh = frm.MaTinh;
            }
        }

        private void btnDCTT2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            frm.ShowDialog();
            if (frm.Result != "")
            {
                btnDCTT2.Tag = frm.MaXa;
                btnDCTT2.Text = frm.Result;
                MaHuyen = frm.MaHuyen;
                MaTinh = frm.MaTinh;
            }
        }

        private void picAddress2_DoubleClick(object sender, EventArgs e)
        {
            txtDiaChi2.Text = txtThuongTru2.Text;
            btnDCLH2.Text = btnDCTT2.Text;
            btnDCLH2.Tag = btnDCTT2.Tag;
            MaTinh2 = MaTinh;
            MaHuyen2 = MaHuyen;
        }

        private void picAddress_DoubleClick(object sender, EventArgs e)
        {
            txtDiaChi.Text = txtThuongTru.Text;
            btnDCLH.Text = btnDCTT.Text;
            btnDCLH.Tag = btnDCTT.Tag;
            MaTinh2 = MaTinh;
            MaHuyen2 = MaHuyen;
        }

        private void lookUpNganHang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    lookUpChiNhanh.Properties.DataSource = db.khNganHangChiNhanhs.Where(p => p.MaNH == Convert.ToInt32(lookUpNganHang.EditValue)).ToList();
                }
            }
            catch { lookUpChiNhanh.Properties.DataSource = null; }
        }

        private void lookUpNganHang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                lookUpNganHang.EditValue = null;
                lookUpNganHang.ClosePopup();

                lookUpChiNhanh.EditValue = null;
                lookUpChiNhanh.ClosePopup();
            }
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
        }

        private void tabSubCompany_General_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picLogo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FTP.frmUploadFile())
            {
                if (frm.SelectFile(true))
                {
                    frm.Folder = "IMAGES/" + DateTime.Now.ToString("yyyy/MM/dd");
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //  o.Logo = frm.WebUrl;
                        picLogo.Tag = frm.WebUrl;
                        try
                        {
                            picLogo.Image = new System.Drawing.Bitmap(new System.IO.MemoryStream(new System.Net.WebClient().DownloadData(frm.WebUrl)));
                        }
                        catch { }
                    }
                }
            }
        }

        private void KhachHang_frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (txtHoKH.Text != objKH.HoKH || txtTenKH.Text != objKH.TenKH || txtChucVu.Text != objKH.ChucVu ||
                txtDiaChi.Text != objKH.DiaChi || txtDiDong.Text != objKH.DiDong || txtDienThoaiCT.Text != objKH.DienThoaiCT
                || txtSoCMND.Text != objKH.SoCMND || txtEmail.Text != objKH.Email)
            {
                if (DialogBox.Question("Bạn có muốn lưu không") == DialogResult.No) return;
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    SavePersonal();
                else
                    SaveCompany();
            }
        }

        private void txtDiDong_TextChanged(object sender, EventArgs e)
        {
            var text = txtDiDong.Text;
            if (text == null | text == "")
            {
                txtDiDong.BackColor = Color.White;
                btnDongY.Enabled = false;
            }
            else
            {
                btnDongY.Enabled = true;
            }


            if (text == " ")
            {
                DialogBox.Error("Không dùng khoảng trắng");
                txtDiDong.Focus();
                txtDiDong.BackColor = Color.Red;
                btnDongY.Enabled = false;

            }
            else
            {
                btnDongY.Enabled = true;
                txtDiDong.BackColor = Color.White;
            }

            var obj = db.KyTuDacBiets.Where(p => p.TenKT == text).ToList();
            if (obj.Count > 0)
            {
                DialogBox.Error("Số điện thoại không bao gồm ký tự: " + "[" + text + "]");
                txtDiDong.Focus();
                txtDiDong.BackColor = Color.Red;
                btnDongY.Enabled = false;

            }
            else
            {
                btnDongY.Enabled = true;
                txtDiDong.BackColor = Color.White;
            }


            checkDienThoai1(text);

        }

        public void checkDienThoai1(string text)
        {
            if (text.Contains(" "))
            {
                DialogBox.Error("Không dùng khoảng trắng");
                txtDiDong.Focus();
                txtDiDong.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            var obj = db.KyTuDacBiets.ToList();
            foreach (var i in obj)
            {
                if (text.Contains(i.TenKT))
                {
                    DialogBox.Error("Số điện thoại không bao gồm ký tự: " + "[" + i.TenKT + "]");
                    txtDiDong.Focus();
                    txtDiDong.BackColor = Color.Red;
                    btnDongY.Enabled = false;
                    return;

                }

            }
        }

        public void checkDienThoai2(string text)
        {
            if (text.Contains(" "))
            {
                DialogBox.Error("Không dùng khoảng trắng");
                txtDienThoai2.Focus();
                txtDienThoai2.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            var obj = db.KyTuDacBiets.ToList();
            foreach (var i in obj)
            {
                if (text.Contains(i.TenKT))
                {
                    DialogBox.Error("Số điện thoại không bao gồm ký tự: " + "[" + i.TenKT + "]");
                    txtDienThoai2.Focus();
                    txtDienThoai2.BackColor = Color.Red;
                    btnDongY.Enabled = false;
                    return;

                }

            }
        }
        public void checkDienThoai3(string text)
        {
            if (text.Contains(" "))
            {
                DialogBox.Error("Không dùng khoảng trắng");
                txtDienThoai3.Focus();
                txtDienThoai3.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            var obj = db.KyTuDacBiets.ToList();
            foreach (var i in obj)
            {
                if (text.Contains(i.TenKT))
                {
                    DialogBox.Error("Số điện thoại không bao gồm ký tự: " + "[" + i.TenKT + "]");
                    txtDienThoai3.Focus();
                    txtDienThoai3.BackColor = Color.Red;
                    btnDongY.Enabled = false;
                    return;

                }

            }
        }
        public void checkDienThoai4(string text)
        {
            if (text.Contains(" "))
            {
                DialogBox.Error("Không dùng khoảng trắng");
                txtDiDong4.Focus();
                txtDiDong4.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            var obj = db.KyTuDacBiets.ToList();
            foreach (var i in obj)
            {
                if (text.Contains(i.TenKT))
                {
                    DialogBox.Error("Số điện thoại không bao gồm ký tự: " + "[" + i.TenKT + "]");
                    txtDiDong4.Focus();
                    txtDiDong4.BackColor = Color.Red;
                    btnDongY.Enabled = false;
                    return;

                }

            }
        }
        public void checkDienThoaiKC(string text)
        {
            if (text.Contains(" "))
            {
                DialogBox.Error("Không dùng khoảng trắng");
                txtSoDTKhanCap.Focus();
                txtSoDTKhanCap.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            var obj = db.KyTuDacBiets.ToList();
            foreach (var i in obj)
            {
                if (text.Contains(i.TenKT))
                {
                    DialogBox.Error("Số điện thoại không bao gồm ký tự: " + "[" + i.TenKT + "]");
                    txtSoDTKhanCap.Focus();
                    txtSoDTKhanCap.BackColor = Color.Red;
                    btnDongY.Enabled = false;
                    return;

                }

            }
        }


        private void txtDienThoai2_TextChanged(object sender, EventArgs e)
        {
            var text = txtDienThoai2.Text;
            if (MaKH != 0 && string.IsNullOrEmpty(text))
            {
                return;
            }
            if (text == null | text == "")
            {
                txtDienThoai2.BackColor = Color.White;
                btnDongY.Enabled = false;
            }

            if (text == " ")
            {
                DialogBox.Error("Không dùng khoảng trắng");
                txtDienThoai2.Focus();
                txtDienThoai2.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            else
            {
                btnDongY.Enabled = true;
                txtDienThoai2.BackColor = Color.White;
            }

            var obj = db.KyTuDacBiets.Where(p => p.TenKT == text).ToList();
            if (obj.Count > 0)
            {
                DialogBox.Error("Số điện thoại không bao gồm ký tự: " + "[" + text + "]");
                txtDienThoai2.Focus();
                txtDienThoai2.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            else
            {
                btnDongY.Enabled = true;
                txtDienThoai2.BackColor = Color.White;
            }

            checkDienThoai2(text);
        }

        private void txtDienThoai3_TextChanged(object sender, EventArgs e)
        {
            var text = txtDienThoai3.Text;

            if (MaKH != 0 && string.IsNullOrEmpty(text))
            {
                return;
            }

            if (text == null | text == "")
            {
                txtDienThoai3.BackColor = Color.White;

            }

            if (text == " ")
            {
                DialogBox.Error("Không dùng khoảng trắng");
                txtDienThoai3.Focus();
                txtDienThoai3.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            else
            {
                btnDongY.Enabled = true;
                txtDienThoai3.BackColor = Color.White;
            }

            var obj = db.KyTuDacBiets.Where(p => p.TenKT == text).ToList();
            if (obj.Count > 0)
            {
                DialogBox.Error("Số điện thoại không bao gồm ký tự: " + "[" + text + "]");
                txtDienThoai3.Focus();
                txtDienThoai3.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            else
            {
                btnDongY.Enabled = true;
                txtDienThoai3.BackColor = Color.White;
            }
            checkDienThoai3(text);
        }

        private void txtDiDong4_TextChanged(object sender, EventArgs e)
        {
            var text = txtDiDong4.Text;

            if (MaKH != 0 && string.IsNullOrEmpty(text))
            {
                return;
            }

            if (text == null | text == "")
            {
                txtDiDong4.BackColor = Color.White;
            }

            if (text == " ")
            {
                DialogBox.Error("Không dùng khoảng trắng");
                txtDiDong4.Focus();
                txtDiDong4.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            else
            {
                btnDongY.Enabled = true;
                txtDiDong4.BackColor = Color.White;
            }

            var obj = db.KyTuDacBiets.Where(p => p.TenKT == text).ToList();
            if (obj.Count > 0)
            {
                DialogBox.Error("Số điện thoại không bao gồm ký tự: " + "[" + text + "]");
                txtDiDong4.Focus();
                txtDiDong4.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            else
            {
                btnDongY.Enabled = true;
                txtDiDong4.BackColor = Color.White;
            }
            checkDienThoai4(text);
        }

        private void txtSoDTKhanCap_TextChanged(object sender, EventArgs e)
        {
            var text = txtSoDTKhanCap.Text;

            if (MaKH != 0 && string.IsNullOrEmpty(text))
            {
                return;
            }

            if (text == null | text == "")
            {
                txtSoDTKhanCap.BackColor = Color.White;
            }

            if (text == " ")
            {
                DialogBox.Error("Không dùng khoảng trắng");
                txtSoDTKhanCap.Focus();
                txtSoDTKhanCap.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            else
            {
                btnDongY.Enabled = true;
                txtSoDTKhanCap.BackColor = Color.White;
            }

            var obj = db.KyTuDacBiets.Where(p => p.TenKT == text).ToList();
            if (obj.Count > 0)
            {
                DialogBox.Error("Số điện thoại không bao gồm ký tự: " + "[" + text + "]");
                txtSoDTKhanCap.Focus();
                txtSoDTKhanCap.BackColor = Color.Red;
                btnDongY.Enabled = false;
                return;
            }
            else
            {
                btnDongY.Enabled = true;
                txtSoDTKhanCap.BackColor = Color.White;
            }
            checkDienThoaiKC(text);
        }

        public bool CheckAllPhoneExits(string phone)
        {
            if (!string.IsNullOrEmpty(phone))
            {
                var exits = db.KhachHangs.Where(p => (p.DiDong == phone || p.DiDong2 == phone || p.DiDong3 == phone || p.DiDong4 == phone
                                                            || p.DienThoaiCT == phone) & p.MaKH != objKH.MaKH).Count();
                return exits > 0 ? true : false;
            }

            return false;
        }
    }
}