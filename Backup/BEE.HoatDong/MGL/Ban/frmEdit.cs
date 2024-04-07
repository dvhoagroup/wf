using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Net.Mail;
using it;
using System.Threading;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.HoatDong.MGL.Ban
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? MaBC = 0;
        public bool IsSave;
        public int? MaKH { get; set; }

        private MasterDataContext db = new MasterDataContext();
        mglbcLichSu objLS = new mglbcLichSu();
        private mglbcBanChoThue objBC;
        private BEE.ThuVien.KhachHang objKH;
        public bool AllowSave = true;
        public bool DislayContac = true;
        public bool? mucdich { get; set; }

        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            lookNVMG.Properties.DataSource = lookNVQL.Properties.DataSource = db.NhanViens.Where(p => p.MaTinhTrang == 1).Select(p => new { p.MaNV, p.HoTen });
            lookTinh.Properties.DataSource = db.Tinhs;
            lookLoaiBDS.Properties.DataSource = db.LoaiBDs;
            lookHuong.Properties.DataSource = lookHuongBC.Properties.DataSource = db.PhuongHuongs;
            lookTrangThai.Properties.DataSource = db.mglbcTrangThais;
            lookTinhTrang.Properties.DataSource = db.mglTinhTrangs;
            lookPhapLy.Properties.DataSource = db.PhapLies;
            cmbTienIch.Properties.DataSource = db.TienIches;
            lookLoaiDuong.Properties.DataSource = db.LoaiDuongs;
            //  lookCapDo.Properties.DataSource = db.mglCapDos;
            lookDuong.Properties.DataSource = db.Duongs;
            lookNguon.Properties.DataSource = db.mglNguons;
            lookDuAn.Properties.DataSource = db.DuAns.Select(p => new { p.MaDA, p.TenDA });
            lookHDMG.Properties.DataSource = db.mglbcTrangThaiHDMGs;
            itemLuu.Enabled = itemXoa.Enabled = itemSua.Enabled = AllowSave;
            lookLoaiTien.Properties.DataSource = db.LoaiTiens;




            if (MaBC > 0)
            {

                objBC = db.mglbcBanChoThues.Single(p => p.MaBC == this.MaBC);
                #region Thong tin chung
                objBC.IsCanGoc = chkCanGoc.Checked;
                txtSoDK.EditValue = objBC.SoDK;
                dateNgayDK.EditValue = objBC.NgayDK;
                rdbLoaiTin.EditValue = objBC.IsBan;
                lookTrangThai.EditValue = objBC.MaTT;
                lookLoaiBDS.EditValue = objBC.MaLBDS;
                lookDuAn.EditValue = objBC.MaDA;
                txtKyHieu.EditValue = objBC.KyHieu;
                lookTinh.EditValue = objBC.MaTinh;
                lookHuyen.EditValue = objBC.MaHuyen;
                lookXa.EditValue = objBC.MaXa;
                //đồng ý cho share
                if (AllowSave)
                {
                    lookDuong.EditValue = objBC.MaDuong;
                    txtSoNha.Text = objBC.SoNha;
                }
                spinDienTich.EditValue = objBC.DienTich;
                spinDonGia.EditValue = objBC.DonGia;
                spinThanhTien.EditValue = objBC.ThanhTien;
                //   spinGiaGoc.EditValue = objBC.GiaGoc;
                lookTinhTrang.EditValue = objBC.MaTinhTrang;
                spinMG.EditValue = objBC.TyLeMG ?? 0;
                cmbCachTinhPhi.SelectedIndex = (int)(objBC.CachTinhPMG ?? 0);
                spinThanhTienMG.EditValue = objBC.PhiMG ?? 0;
                lookNVMG.EditValue = objBC.MaNVKD;
                lookNVQL.EditValue = objBC.MaNVQL;
                lookHDMG.EditValue = objBC.TrangThaiHDMG;
                #endregion

                #region Thong tin SP
                lookPhapLy.EditValue = objBC.MaPL;
                //Tien ich
                string tienIch = "";
                foreach (mglbcTienIch t in objBC.mglbcTienIches)
                    tienIch += t.MaTI + ", ";
                tienIch = tienIch.TrimEnd(' ').TrimEnd(',');
                cmbTienIch.SetEditValue(tienIch);
                lookHuong.EditValue = objBC.MaHuong;
                lookHuongBC.EditValue = objBC.HuongBanCong;
                //  txtTomTat.Text = objBC.TomTat;
                txtDacTrung.Text = objBC.DacTrung;
                // lookCapDo.EditValue = objBC.MaCD;
                spinNamXD.EditValue = objBC.NamXayDung;
                lookNguon.EditValue = objBC.MaNguon;
                lookLoaiDuong.EditValue = objBC.MaLD;
                spinDuongRong.EditValue = objBC.DuongRong;
                //
                //  spinPhongKHach.EditValue = objBC.PhongKhach;
                spinPhongNgu.EditValue = objBC.PhongNgu;
                // spinPhongTam.EditValue = objBC.PhongTam;
                spinPhongVS.EditValue = objBC.PhongVS;
                spinSoTang.EditValue = objBC.SoTang;
                spinMatTien.EditValue = objBC.NgangXD;
                spinDai.EditValue = objBC.DaiXD;
                spinMatSau.EditValue = objBC.SauXD;
                spinMatTienTT.EditValue = objBC.NgangKV;
                spinDaiTT.EditValue = objBC.DaiKV;
                spinMatSauTT.EditValue = objBC.SauKV;
                ckbCoTangHam.Checked = objBC.TangHam ?? false;
                chkThangMay.Checked = objBC.IsThangMay ?? false;
                chkCanGoc.Checked = objBC.IsCanGoc ?? false;
                //  ckbChinhChu.Checked = objBC.ChinhChu ?? false;
                //  ckbCoSanThuong.Checked = objBC.SanThuong ?? false;
                ckbDoOto.Checked = objBC.DauOto ?? false;
                ckbThuongLuong.Checked = objBC.ThuongLuong ?? false;
                txtToaDo.Text = objBC.ToaDo;
                #endregion

                #region Thong tin KH
                //Khach hang
                objKH = objBC.KhachHang;
                KhachHang_Load();
                txtHoTenNDD.EditValue = objBC.HoTenNDD;
                txtHoTenNTG.EditValue = objBC.HoTenNTG;
                #endregion

                #region thông tin mới
                spinDienTichDat.EditValue = objBC.DienTichDat;
                spinDientichtXD.EditValue = objBC.DienTichXD;
                spinSoTangXD.EditValue = objBC.SoTangXD;
                txtDonVITC.EditValue = objBC.DonViThueCu;
                txtDonViDT.EditValue = objBC.DonViDangThue;
                dateThoiHanHD.EditValue = objBC.ThoiGianHD;
                dateThoiGianBG.EditValue = objBC.ThoiGianBGMB;
                txtGhiChu.Text = objBC.GhiChu;
                txtGioiThieu.InnerHtml = objBC.GioiThieu;
                lookLoaiTien.EditValue = objBC.MaLT;
                #endregion
                LoadThongTinTT();
            }
            else
            {
                dateNgayDK.EditValue = DateTime.Now;
                lookNVQL.EditValue = lookNVMG.EditValue = Common.StaffID;
                spinMG.EditValue = null;
                //lookTrangThai.EditValue = 1;
                cmbCachTinhPhi.SelectedIndex = 0;
                lookHDMG.EditValue = 2;
                SetAddNew();
            }

            if (this.MaKH != null)
            {
                this.objKH = db.KhachHangs.SingleOrDefault(p => p.MaKH == this.MaKH);
                KhachHang_Load();
            }
            if(this.MaBC == null)
            {
                SetAddNew();
            }




        }

        void SetEnableControl(bool enabel)
        {
            grKhachHang.Enabled = enabel;
            grPhieu.Enabled = enabel;
            grSanPham.Enabled = enabel;
            itemDinhKem.Enabled = enabel;
            gcChiTiet.Enabled = enabel;
            //nut chuc nang
            itemThem.Enabled = !enabel;
            itemSua.Enabled = !enabel;
            itemLuu.Enabled = enabel;
            itemHoan.Enabled = enabel;
            //
            itemXoa.Enabled = this.MaBC != 0;
            itemIn.Enabled = this.MaBC != 0;
        }

        void SetAddNew()
        {
            objBC = new mglbcBanChoThue();
            this.MaBC = 0;
            string soDK = "", kyHieu = "";
            db.mglbcBanChoThue_TaoSoPhieu(ref soDK, ref kyHieu);
            txtSoDK.EditValue = soDK;
            itemDinhKem.Tag = null;
            //Khach hang
            if (this.MaKH == null)
                KhachHang_AddNew();
            //Bat dong san
            txtKyHieu.EditValue = kyHieu;
            lookLoaiBDS.EditValue = null;
            lookDuAn.EditValue = null;
            rdbLoaiTin.EditValue = mucdich;
            spinDienTich.EditValue = null;
            spinDonGia.EditValue = null;
            spinThanhTien.EditValue = null;
            lookTinh.EditValue = null;
            lookHuyen.EditValue = null;
            //Tien ich
            cmbTienIch.SetEditValue("");
            //
            spinPhongNgu.EditValue = null;
            spinDuongRong.EditValue = null;
            spinSoTang.EditValue = null;
            spinMatTienTT.EditValue = null;
            spinDaiTT.EditValue = null;
            spinMatSauTT.EditValue = null;
            spinMatTien.EditValue = null;
            spinDai.EditValue = null;
            spinMatSau.EditValue = null;
        }

        void LoadDaiDien()
        {
            if (objKH != null)
            {
                it.NguoiDaiDienCls o = new it.NguoiDaiDienCls();
                gridControlAvatar.DataSource = o.Select(objKH.MaKH);
            }
            else
            {
                gridControlAvatar.DataSource = null;
            }
        }

        void KhachHang_Load()
        {
            dateNgaySinh.EditValue = objKH.NgaySinh;
            txtThuongTru.EditValue = objKH.ThuongTru;
            txtNoiCap.EditValue = objKH.NoiCap;
            txtSoCMND.EditValue = objKH.SoCMND;
            dateNgayCap.EditValue = objKH.NgayCap;
            txtNoiSinh.EditValue = objKH.NguyenQuan;
            if (objKH.Xa != null)
            {
                btnDCTT.EditValue = string.Format("{0}, {1}, {2}", objKH.Xa.TenXa,
                    objKH.Xa.Huyen.TenHuyen, objKH.Xa.Huyen.Tinh.TenTinh);
            }
            txtDiaChiLienHe.EditValue = objKH.DiaChi;
            if (objKH.Xa1 != null)
            {
                btnDCLH.EditValue = string.Format("{0}, {1}, {2}", objKH.Xa1.TenXa,
                    objKH.Xa1.Huyen.TenHuyen, objKH.Xa1.Huyen.Tinh.TenTinh);
            }
            if (DislayContac)
            {
                txtHoTenKH.EditValue = objKH.HoKH + " " + objKH.TenKH;
                txtDienThoaiCD.EditValue = objKH.DienThoaiCT;
                txtDiDong.EditValue = objKH.DiDong;
                txtDiDong2.EditValue = objKH.DiDong2;
                txtDiDong3.Text = objKH.DiDong3;
                txtDiDong4.Text = objKH.DiDong4;
                txtEmail.EditValue = objKH.Email;

                objLS.SoDienThoai = txtDiDong.Text;
                if (txtEmail.EditValue != objKH.Email)
                    objLS.Email = txtEmail.Text;
                if (txtDiaChiLienHe.EditValue != objKH.DiaChi)
                    objLS.DiaChi = txtDiaChiLienHe.Text;
                try
                {
                    if (objKH.MaKH != objBC.MaKH)
                        objLS.TenKH = objKH.HoKH + " " + objKH.TenKH;
                }
                catch
                {

                }

                if (txtDiDong.EditValue != objKH.DiDong)
                    LoadDaiDien();
            }
        }

        private void KhachHang_AddNew()
        {
            objKH = new BEE.ThuVien.KhachHang();
            objKH.IsPersonal = true;
            txtHoTenKH.EditValue = null;
            txtSoCMND.EditValue = null;
            dateNgayCap.EditValue = null;
            txtNoiCap.EditValue = null;
            dateNgaySinh.EditValue = null;
            txtNoiSinh.EditValue = null;
            txtDienThoaiCD.EditValue = null;
            txtDiDong.EditValue = null;
            txtEmail.EditValue = null;
            txtThuongTru.EditValue = null;
            btnDCTT.EditValue = null;
            txtDiaChiLienHe.EditValue = null;
            btnDCLH.EditValue = null;
        }

        void LoadThongTinTT()
        {
            gcChiTiet.DataSource = objBC.mglbcThongTinTTs;
        }

        private void btnDCTT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            frm.MaXa = objKH.MaXa.GetValueOrDefault();
            frm.ShowDialog();
            if (frm.MaXa != 0)
            {
                objKH.Xa = db.Xas.Single(p => p.MaXa == frm.MaXa);
                btnDCTT.Text = frm.Result;
            }
        }

        private void btnDCLH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BEE.NghiepVuKhac.SelectPosition_frm frm = new BEE.NghiepVuKhac.SelectPosition_frm();
            frm.MaXa = objKH.MaXa2.GetValueOrDefault();
            frm.ShowDialog();
            if (frm.MaXa != 0)
            {
                objKH.Xa1 = db.Xas.Single(p => p.MaXa == frm.MaXa);
                btnDCLH.Text = frm.Result;
            }
        }

        private void picAddressKH_Click(object sender, EventArgs e)
        {
            txtDiaChiLienHe.Text = txtThuongTru.Text;
            btnDCLH.Text = btnDCTT.Text;
            objKH.Xa1 = objKH.Xa;
        }

        private void txtHoTenKH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    KhachHang.Find_frm frm = new KhachHang.Find_frm();
                    frm.ShowDialog();
                    if (frm.MaKH != 0)
                    {
                        objKH = db.KhachHangs.Single(p => p.MaKH == frm.MaKH);
                        KhachHang_Load();
                        return;
                    }
                    break;
                case 1:
                    KhachHang_AddNew();
                    KhachHang.KhachHang_frm frmkh = new KhachHang.KhachHang_frm();
                    frmkh.ShowDialog();
                    if (frmkh.MaKH != 0)
                    {
                        objKH = db.KhachHangs.Single(p => p.MaKH == frmkh.MaKH);
                        KhachHang_Load();
                        return;
                    }
                    break;
                    //case 2:
                    //    KhachHang_Edit();
                    //    break;
            }
        }

        private void lookTinh_EditValueChanged(object sender, EventArgs e)
        {
            if (lookTinh.EditValue == null)
                lookHuyen.Properties.DataSource = null;
            else
            {
                lookHuyen.Properties.DataSource = db.Huyens.Where(p => p.MaTinh == (byte)lookTinh.EditValue);
                lookHuyen.EditValue = null;
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetAddNew();
            SetEnableControl(true);
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetEnableControl(true);
        }

        private void ThreadSendMail()
        {
            var objCOmpany = db.Companies.FirstOrDefault();
            var objListMailTo = db.mglmMailDangKyNhans.Where(p => p.IsThemSP == true);
            if (objListMailTo == null)
                return;
            var objConfig = db.mailConfigs.FirstOrDefault(p => p.IsNoiBo == true);
            if (objConfig == null)
                return;
            foreach (var m in objListMailTo)
            {
                MailProviderCls objMail = new MailProviderCls();
                var objMailForm = new MailAddress(objConfig.Email, objConfig.Username);
                objMail.MailAddressFrom = objMailForm;
                var objMailTo = new MailAddress(m.NhanVien.Email, m.NhanVien.HoTen);
                objMail.MailAddressTo = objMailTo;
                objMail.SmtpServer = objConfig.Server;
                objMail.EnableSsl = objConfig.EnableSsl.Value;
                objMail.Port = objConfig.Port ?? 465;
                objMail.PassWord = EncDec.Decrypt(objConfig.Password);
                objMail.Subject = objCOmpany == null ? "Nhân viên (nhập mới/chỉnh sửa) BDS" : string.Format("Nhân viên công ty {0} (nhập mới/chỉnh sửa) sản phẩm.", objCOmpany.TenCT);
                string Content = string.Format(" Nhân viên xử lý : {0} \r\n Nội dung công việc: Thêm mới/chỉnh sửa sản phẩm  \r\n Số đăng ký: {1} \r\n Ký hiệu: {2} \r\n Ngày xử lý: {3:dd/MM/yyyy-hh:mm:ss tt}",
                    objBC.NhanVien.HoTen, objBC.SoDK, objBC.KyHieu, objBC.NgayCN);
                objMail.Content = Content;
                objMail.SendMailV3();
                Thread.Sleep(2);
            }
        }

        void Save()
        {

            #region Rang buoc
            if (txtSoDK.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số đăng ký");
                txtSoDK.Focus();
                return;
            }
            else
            {
                var count = db.mglbcBanChoThues.Where(p => p.SoDK == txtSoDK.Text.Trim() & p.MaBC != MaBC).Count();
                if (count > 0)
                {
                    DialogBox.Error("Số đăng ký <" + txtSoDK.Text + "> đã có trong hệ thống. Vui lòng nhập lại.");
                    txtSoDK.Focus();
                    return;
                }
            }

            if (dateNgayDK.Text == "")
            {
                DialogBox.Error("Vui lòng nhập ngày đăng ký");
                dateNgayDK.Focus();
                return;
            }
            if (lookLoaiBDS.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập loại BĐS!");
                lookLoaiBDS.Focus();
                return;
            }

            if (lookLoaiTien.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn loại tiền!");
                lookLoaiTien.Focus();
                return;
            }

            if (lookTrangThai.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn trạng thái!");
                lookTrangThai.Focus();
                return;
            }

            if (lookXa.EditValue == null)
            {
                DialogBox.Infomation("Vui lòng nhập xã phường!");
                lookXa.Focus();
                return;
            }
            if (lookDuong.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập tên đường");
                lookDuong.Focus();
                return;
            }
            if (txtSoNha.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số nhà!");
                txtSoNha.Focus();
                return;
            }
            if (spinDienTich.Value == 0)
            {
                DialogBox.Error("Vui lòng nhập diện tích!");
                spinDienTich.Focus();
                return;
            }
            if (spinThanhTien.Value == 0)
            {
                DialogBox.Error("Vui lòng hập thành tiền");
                spinThanhTien.Focus();
                return;
            }
            //if (lookHuong.EditValue == null)
            //{
            //    DialogBox.Error("Vui lòng nhập hướng cửa!");
            //    lookHuong.Focus();
            //    return;
            //}
            if (txtHoTenKH.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập khách hàng!");
                txtHoTenKH.Focus();
                return;
            }

            if (txtDiDong.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số điện thoại di dộng");
                txtDiDong.Focus();
                return;
            }
            if (txtKyHieu.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập [Ký hiệu] sản phẩm!");
                txtKyHieu.Focus();
                return;
            }
            else
            {
                var count = db.mglbcBanChoThues.Where(p => p.KyHieu == txtKyHieu.Text.Trim() & p.MaBC != MaBC).Count();
                if (count > 0)
                {
                    DialogBox.Error("Ký hiệu <" + txtKyHieu.Text + "> đã có trong hệ thống. Vui lòng nhập lại.");
                    txtKyHieu.Focus();
                    return;
                }
            }
            #endregion

            #region ghi lich su
            if (this.MaBC != 0)
            {
                if (txtSoNha.Text != objBC.SoNha)
                    objLS.SoNha = txtSoNha.Text;
                if ((int)lookDuong.EditValue != objBC.MaDuong)
                    objLS.TenDuong = lookDuong.Text;
                if ((decimal?)spinMatTien.EditValue != objBC.NgangXD)
                    objLS.MatTien = (decimal?)spinMatTien.EditValue;
                if ((decimal?)spinDienTich.EditValue != objBC.DienTich)
                    objLS.DienTich = (decimal?)spinDienTich.EditValue;
                if ((decimal?)spinThanhTien.EditValue != objBC.ThanhTien)
                    objLS.GiaTien = (decimal?)spinThanhTien.EditValue;
                if (txtDacTrung.Text != objBC.DacTrung)
                    objLS.DacTrung = txtDacTrung.Text;
                if ((DateTime?)dateThoiHanHD.EditValue != objBC.ThoiGianHD)
                    objLS.ThoiHanHD = (DateTime?)dateThoiHanHD.EditValue;
                if (txtToaDo.Text != objBC.ToaDo)
                    objLS.ToaDo = txtToaDo.Text;
                if ((int?)lookNVQL.EditValue != objBC.MaNVQL)
                    objLS.CanBoLV = lookNVQL.Text;
                if (txtSoNha.Text != objBC.SoNha || (int)lookDuong.EditValue != objBC.MaDuong || (decimal?)spinMatTien.EditValue != objBC.NgangXD ||
                    (decimal?)spinDienTich.EditValue != objBC.DienTich || (decimal?)spinThanhTien.EditValue != objBC.ThanhTien || txtDacTrung.Text != objBC.DacTrung
                    || (DateTime?)dateThoiHanHD.EditValue != objBC.ThoiGianHD || txtToaDo.Text != objBC.ToaDo || (int?)lookNVQL.EditValue != objBC.MaNVQL)
                {
                    objLS.MaBC = objBC.MaBC;
                    objLS.MaNVS = Common.StaffID;
                    objLS.NgaySua = DateTime.Now;
                    db.mglbcLichSus.InsertOnSubmit(objLS);
                }

                if ((bool)objBC.IsCanGoc != chkCanGoc.Checked || txtSoDK.Text != objBC.SoDK || (DateTime?)dateNgayDK.EditValue != objBC.NgayDK ||
               (bool?)rdbLoaiTin.EditValue != objBC.IsBan || (byte)lookTrangThai.EditValue != objBC.MaTT || (short?)lookLoaiBDS.EditValue != objBC.MaLBDS ||
               (int?)lookDuAn.EditValue != objBC.MaDA || txtKyHieu.EditValue != objBC.KyHieu || (byte?)lookTinh.EditValue != objBC.MaTinh ||
               (short?)lookHuyen.EditValue != objBC.MaHuyen || (int?)lookXa.EditValue != objBC.MaXa ||
               (int?)lookDuong.EditValue != objBC.MaDuong || txtSoNha.Text != objBC.SoNha || (decimal?)spinDienTich.EditValue != objBC.DienTich ||
               (decimal?)spinDonGia.EditValue != objBC.DonGia || (decimal?)spinThanhTien.EditValue != objBC.ThanhTien ||
               (byte?)lookTinhTrang.EditValue != objBC.MaTinhTrang || (decimal?)spinMG.EditValue != objBC.TyLeMG ||
               (decimal?)spinThanhTienMG.EditValue != objBC.PhiMG ||
               (int?)lookNVMG.EditValue != objBC.MaNVKD || (int?)lookNVQL.EditValue != objBC.MaNVQL ||
               (byte?)lookHDMG.EditValue != objBC.TrangThaiHDMG || (short?)lookPhapLy.EditValue != objBC.MaPL ||
               (short?)lookHuong.EditValue != objBC.MaHuong || (short?)lookHuongBC.EditValue != objBC.HuongBanCong ||
               txtDacTrung.Text != objBC.DacTrung || Convert.ToInt32(spinNamXD.EditValue) != objBC.NamXayDung ||
               (short?)lookNguon.EditValue != (short?)objBC.MaNguon || (short?)lookLoaiDuong.EditValue != objBC.MaLD ||
               (decimal?)spinDuongRong.EditValue != objBC.DuongRong || Convert.ToByte(spinPhongNgu.EditValue) != objBC.PhongNgu ||
               Convert.ToByte(spinPhongVS.EditValue) != objBC.PhongVS || Convert.ToByte(spinSoTang.EditValue) != objBC.SoTang ||
               (decimal?)spinMatTien.EditValue != objBC.NgangXD || (decimal?)spinDai.EditValue != objBC.DaiXD ||
               (decimal?)spinMatSau.EditValue != objBC.SauXD || (decimal?)spinMatTienTT.EditValue != objBC.NgangKV ||
               (decimal?)spinDaiTT.EditValue != objBC.DaiKV || (decimal?)spinMatSauTT.EditValue != objBC.SauKV ||
               ckbCoTangHam.Checked != objBC.TangHam || chkThangMay.Checked != objBC.IsThangMay ||
               chkCanGoc.Checked != objBC.IsCanGoc || ckbDoOto.Checked != objBC.DauOto ||
               ckbThuongLuong.Checked != objBC.ThuongLuong || txtToaDo.Text != objBC.ToaDo ||
               txtHoTenNDD.EditValue != objBC.HoTenNDD || txtHoTenNTG.EditValue != objBC.HoTenNTG ||
               (decimal?)spinDienTichDat.EditValue != objBC.DienTichDat || (decimal?)spinDientichtXD.EditValue != objBC.DienTichXD ||
               Convert.ToInt32(spinSoTangXD.EditValue) != objBC.SoTangXD || txtDonVITC.EditValue != objBC.DonViThueCu ||
               txtDonViDT.EditValue != objBC.DonViDangThue || (DateTime?)dateThoiHanHD.EditValue != objBC.ThoiGianHD ||
               (DateTime?)dateThoiGianBG.EditValue != objBC.ThoiGianBGMB || txtGhiChu.Text != objBC.GhiChu ||
               txtGioiThieu.InnerHtml != objBC.GioiThieu || (byte?)lookLoaiTien.EditValue != objBC.MaLT)
                    objBC.NgayCN = DateTime.Now;
            }

            #endregion

            #region Lưu thông tin chung
            objBC.SoDK = txtSoDK.Text;
            objBC.NgayDK = dateNgayDK.DateTime;
            objBC.IsBan = (bool)rdbLoaiTin.EditValue;
            objBC.MaTT = (byte?)lookTrangThai.EditValue;
            objBC.MaLBDS = (short?)lookLoaiBDS.EditValue;
            objBC.MaDA = (int?)lookDuAn.EditValue;
            objBC.KyHieu = txtKyHieu.Text;
            objBC.MaHuyen = (short?)lookHuyen.EditValue;
            objBC.MaTinh = (byte?)lookTinh.EditValue;
            objBC.MaXa = (int?)lookXa.EditValue;
            objBC.MaDuong = (int?)lookDuong.EditValue;
            objBC.SoNha = txtSoNha.Text.Trim();
            objBC.DienTich = (decimal?)spinDienTich.EditValue;
            objBC.DonGia = (decimal?)spinDonGia.EditValue;
            objBC.ThanhTien = (decimal?)spinThanhTien.EditValue;
            objBC.IsCanGoc = (bool?)chkCanGoc.Checked;
            objBC.MaTinhTrang = (byte?)lookTinhTrang.EditValue;
            objBC.TyLeMG = (decimal?)spinMG.EditValue;
            objBC.CachTinhPMG = Convert.ToInt16(cmbCachTinhPhi.SelectedIndex);
            objBC.PhiMG = (decimal?)spinThanhTienMG.EditValue;
            objBC.MaNVKD = (int?)lookNVMG.EditValue;
            objBC.MaNVQL = (int?)lookNVQL.EditValue;
            objBC.MaPL = (short?)lookPhapLy.EditValue;
            objBC.TrangThaiHDMG = (byte?)lookHDMG.EditValue;
            objBC.MaTTD = 2;
            //Tien ich
            objBC.mglbcTienIches.Clear();
            objBC.TienIch = cmbTienIch.Text;
            string[] ts = cmbTienIch.Properties.GetCheckedItems().ToString().Split(',');
            if (ts[0] != "")
            {
                foreach (var i in ts)
                {
                    mglbcTienIch objTI = new mglbcTienIch();
                    objTI.MaTI = byte.Parse(i);
                    objBC.mglbcTienIches.Add(objTI);
                }
            }
            objBC.MaHuong = (short?)lookHuong.EditValue;
            objBC.HuongBanCong = (short?)lookHuongBC.EditValue;
            objBC.DacTrung = txtDacTrung.Text;
            objBC.NamXayDung = Convert.ToInt32(spinNamXD.EditValue);
            objBC.MaNguon = (short?)lookNguon.EditValue;
            objBC.MaLD = (short?)lookLoaiDuong.EditValue;
            objBC.DuongRong = (decimal?)spinDuongRong.EditValue;
            objBC.PhongNgu = Convert.ToByte(spinPhongNgu.EditValue);
            objBC.PhongVS = Convert.ToByte(spinPhongVS.EditValue);
            objBC.SoTang = Convert.ToByte(spinSoTang.EditValue);
            objBC.NgangXD = (decimal?)spinMatTien.EditValue;
            objBC.DaiXD = (decimal?)spinDai.EditValue;
            objBC.SauXD = (decimal?)spinMatSau.EditValue;
            objBC.NgangKV = (decimal?)spinMatTienTT.EditValue;
            objBC.DaiKV = (decimal?)spinDaiTT.EditValue;
            objBC.SauKV = (decimal?)spinMatSauTT.EditValue;
            objBC.ThuongLuong = (bool?)ckbThuongLuong.Checked;
            objBC.DauOto = (bool?)ckbDoOto.Checked;
            objBC.TangHam = (bool?)ckbCoTangHam.Checked;
            objBC.IsCanGoc = (bool?)chkCanGoc.Checked;
            objBC.IsThangMay = (bool?)chkThangMay.Checked;
            //Khach hang
            string hoTenKH = txtHoTenKH.Text.Trim();
            objKH.HoKH = hoTenKH.LastIndexOf(' ') > 0 ? hoTenKH.Substring(0, hoTenKH.LastIndexOf(' ')) : "";
            objKH.TenKH = hoTenKH.Substring(hoTenKH.LastIndexOf(' ') + 1);
            objKH.SoCMND = txtSoCMND.Text;
            if (dateNgayCap.Text != "")
                objKH.NgayCap = dateNgayCap.DateTime;
            objKH.NoiCap = txtNoiCap.Text;
            if (dateNgaySinh.Text != "")
                objKH.NgaySinh = dateNgaySinh.DateTime;
            objKH.NguyenQuan = txtNoiSinh.Text;
            objKH.DienThoaiCT = txtDienThoaiCD.Text;
            objKH.DiDong = txtDiDong.Text;
            objKH.DiDong2 = txtDiDong2.Text;
            objKH.DiDong3 = txtDiDong3.Text;
            objKH.DiDong4 = txtDiDong4.Text;
            objKH.Email = txtEmail.Text;
            objKH.ThuongTru = txtThuongTru.Text;
            objKH.DiaChi = txtDiaChiLienHe.Text;
            objKH.MaNV = (int?)lookNVQL.EditValue;
            objBC.KhachHang = objKH;
            objBC.HoTenNDD = txtHoTenNDD.Text;
            objBC.HoTenNTG = txtHoTenNTG.Text;
            #region thông tin mới
            objBC.DienTichDat = (decimal?)spinDienTichDat.EditValue;
            objBC.DienTichXD = (decimal?)spinDientichtXD.EditValue;
            objBC.SoTangXD = Convert.ToInt32(spinSoTangXD.EditValue);
            objBC.DonViThueCu = txtDonVITC.Text;
            objBC.DonViDangThue = txtDonViDT.Text;
            objBC.ThoiGianHD = (DateTime?)dateThoiHanHD.EditValue;
            objBC.ThoiGianBGMB = (DateTime?)dateThoiGianBG.EditValue;
            objBC.GhiChu = txtGhiChu.Text;
            objBC.GioiThieu = txtGioiThieu.InnerHtml;
            var toado = txtToaDo.Text;
            objBC.ToaDo = toado;
            if (toado != "")
            {
                var nam = toado.Substring(0, 12);
                var bac = toado.Substring(toado.Length - 13);
                objBC.KinhDo = nam;
                objBC.ViDo = bac;
            }
            else
            {
                objBC.KinhDo = "";
                objBC.ViDo = "";
            }

            objBC.MaLT = (byte?)lookLoaiTien.EditValue;
            #endregion

            if (this.MaBC == 0)
            {

                var obCK = db.mglbcBanChoThues.Where(p => p.MaKH == (int?)objKH.MaKH && p.MaDuong == (int?)lookDuong.EditValue && p.SoNha == txtSoNha.Text.Trim()).ToList();
                if (obCK.Count > 0)
                {
                    DialogBox.Infomation("Bất động sản này đã tồn tại vui lòng kiểm tra lại [Tên đường], [Số nhà] và Tab [Khách hàng]");
                    return;
                }
                objBC.NgayNhap = DateTime.Now;
                objBC.MaNVN = Common.StaffID;
                db.mglbcBanChoThues.InsertOnSubmit(objBC);
            }
            db.SubmitChanges();
            if (this.MaBC == 0)
            {
                objLS.SoNha = txtSoNha.Text;
                objLS.TenDuong = lookDuong.Text;
                objLS.MatTien = (decimal?)spinMatTien.EditValue;
                objLS.DienTich = (decimal?)spinDienTich.EditValue;
                objLS.GiaTien = (decimal?)spinThanhTien.EditValue;
                objLS.DacTrung = txtDacTrung.Text;
                objLS.ThoiHanHD = (DateTime?)dateThoiHanHD.EditValue;
                objLS.ToaDo = txtToaDo.Text;
                objLS.CanBoLV = lookNVQL.Text;
                objLS.MaBC = objBC.MaBC;
                objLS.MaNVS = Common.StaffID;
                objLS.NgaySua = DateTime.Now;
                db.mglbcLichSus.InsertOnSubmit(objLS);
                objBC.NgayCN = DateTime.Now;
                db.SubmitChanges();
            }

            this.MaBC = objBC.MaBC;
            this.IsSave = true;
            SetEnableControl(false);
            #endregion
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private void itemHoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetEnableControl(false);
        }

        private void itemDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void spinDienTich_EditValueChanged(object sender, EventArgs e)
        {
            timerGiaBan.Enabled = false;
            timerGiaBan.Enabled = true;



        }

        private void TinhPhiMG()
        {
            if (cmbCachTinhPhi.SelectedIndex == null)
                return;
            switch (cmbCachTinhPhi.SelectedIndex)
            {
                case 0:
                    spinThanhTienMG.Value = spinThanhTien.Value * spinMG.Value / 100;
                    lbMoTa.Text = "Phí MG/%";
                    break;
                case 1:
                    spinThanhTienMG.Value = spinMG.Value;
                    lbMoTa.Text = "Phí MG/Khoản cố định";
                    break;
                case 2:
                    spinThanhTienMG.Value = spinThanhTien.Value * spinMG.Value;
                    lbMoTa.Text = "Phí MG/Tháng thuê";
                    break;
            }
        }

        private void cmbCachTinhPhi_SelectedIndexChanged(object sender, EventArgs e)
        {
            TinhPhiMG();
        }

        private void lookHuyen_EditValueChanged(object sender, EventArgs e)
        {
            if (lookHuyen.EditValue == null)
                lookXa.Properties.DataSource = null;
            else
            {
                lookXa.Properties.DataSource = db.Xas.Where(p => p.MaHuyen == (short?)lookHuyen.EditValue);
                lookXa.EditValue = null;
            }
        }

        private void spinThanhTien_EditValueChanged(object sender, EventArgs e)
        {
            timerThanhTien.Enabled = false;
            timerThanhTien.Enabled = true;
        }

        private void spinMG_EditValueChanged(object sender, EventArgs e)
        {
            TinhPhiMG();
        }

        private void rdbLoaiTin_EditValueChanged(object sender, EventArgs e)
        {
            if (rdbLoaiTin.SelectedIndex == 0)
            {
                lblDonGia.Text = "Giá bán/m2";
                lblDienTich.Text = "Diện tích bán(*)";
            }
            else
            {
                lblDonGia.Text = "Giá cho thuê/m2";
                lblDienTich.Text = "DT cho thuê(*)";
            }
        }

        private void frmEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if ((bool)objBC.IsCanGoc != chkCanGoc.Checked || txtSoDK.Text != objBC.SoDK || (DateTime?)dateNgayDK.EditValue != objBC.NgayDK ||
                    (bool?)rdbLoaiTin.EditValue != objBC.IsBan || (byte)lookTrangThai.EditValue != objBC.MaTT || (short?)lookLoaiBDS.EditValue != objBC.MaLBDS ||
                    (int?)lookDuAn.EditValue != objBC.MaDA || txtKyHieu.EditValue != objBC.KyHieu || (byte?)lookTinh.EditValue != objBC.MaTinh ||
                    (short?)lookHuyen.EditValue != objBC.MaHuyen || (int?)lookXa.EditValue != objBC.MaXa ||
                    (int?)lookDuong.EditValue != objBC.MaDuong || txtSoNha.Text != objBC.SoNha || (decimal?)spinDienTich.EditValue != objBC.DienTich ||
                    (decimal?)spinDonGia.EditValue != objBC.DonGia || (decimal?)spinThanhTien.EditValue != objBC.ThanhTien ||
                    (byte?)lookTinhTrang.EditValue != objBC.MaTinhTrang || (decimal?)spinMG.EditValue != objBC.TyLeMG ||
                    (decimal?)spinThanhTienMG.EditValue != objBC.PhiMG ||
                    (int?)lookNVMG.EditValue != objBC.MaNVKD || (int?)lookNVQL.EditValue != objBC.MaNVQL ||
                    (byte?)lookHDMG.EditValue != objBC.TrangThaiHDMG || (short?)lookPhapLy.EditValue != objBC.MaPL ||
                    (short?)lookHuong.EditValue != objBC.MaHuong || (short?)lookHuongBC.EditValue != objBC.HuongBanCong ||
                    txtDacTrung.Text != objBC.DacTrung || Convert.ToInt32(spinNamXD.EditValue) != objBC.NamXayDung ||
                    (short?)lookNguon.EditValue != (short?)objBC.MaNguon || (short?)lookLoaiDuong.EditValue != objBC.MaLD ||
                    (decimal?)spinDuongRong.EditValue != objBC.DuongRong || Convert.ToByte(spinPhongNgu.EditValue) != objBC.PhongNgu ||
                    Convert.ToByte(spinPhongVS.EditValue) != objBC.PhongVS || Convert.ToByte(spinSoTang.EditValue) != objBC.SoTang ||
                    (decimal?)spinMatTien.EditValue != objBC.NgangXD || (decimal?)spinDai.EditValue != objBC.DaiXD ||
                    (decimal?)spinMatSau.EditValue != objBC.SauXD || (decimal?)spinMatTienTT.EditValue != objBC.NgangKV ||
                    (decimal?)spinDaiTT.EditValue != objBC.DaiKV || (decimal?)spinMatSauTT.EditValue != objBC.SauKV ||
                    ckbCoTangHam.Checked != objBC.TangHam || chkThangMay.Checked != objBC.IsThangMay ||
                    chkCanGoc.Checked != objBC.IsCanGoc || ckbDoOto.Checked != objBC.DauOto ||
                    ckbThuongLuong.Checked != objBC.ThuongLuong || txtToaDo.Text != objBC.ToaDo ||
                    txtHoTenNDD.EditValue != objBC.HoTenNDD || txtHoTenNTG.EditValue != objBC.HoTenNTG ||
                    (decimal?)spinDienTichDat.EditValue != objBC.DienTichDat || (decimal?)spinDientichtXD.EditValue != objBC.DienTichXD ||
                    Convert.ToInt32(spinSoTangXD.EditValue) != objBC.SoTangXD || txtDonVITC.EditValue != objBC.DonViThueCu ||
                    txtDonViDT.EditValue != objBC.DonViDangThue || (DateTime?)dateThoiHanHD.EditValue != objBC.ThoiGianHD ||
                    (DateTime?)dateThoiGianBG.EditValue != objBC.ThoiGianBGMB || txtGhiChu.Text != objBC.GhiChu ||
                    txtGioiThieu.InnerHtml != objBC.GioiThieu || (byte?)lookLoaiTien.EditValue != objBC.MaLT)
                {
                    if (DialogBox.Question("Bạn có muốn lưu không") == DialogResult.No) return;
                    Save();

                }

            }
            catch { }
        }

        private void lookLoaiTien_EditValueChanged(object sender, EventArgs e)
        {
            spinTyGia.EditValue = lookLoaiTien.GetColumnValue("TyGia");
        }

        private void timerGiaBan_Tick(object sender, EventArgs e)
        {
            timerGiaBan.Enabled = false;

            if (spinDonGia.Value == 0)
            {
                lbMoTa.Text = "Thương lượng";
            }
            else
            {
                var tiente = new it.TienTeCls();
                lbMoTa.Text = string.Format("{0}/m2", tiente.DocTienBangChu(Convert.ToInt64(spinDonGia.Value)));
            }

            if (spinThanhTien.Value == 0)
            {
                spinThanhTien.Value = spinDienTich.Value * spinDonGia.Value;
                lblKetQua.Text = "Kết quả đúng";
            }
            else
                lblKetQua.Text = "Kết quả sai";
        }

        private void timerThanhTien_Tick(object sender, EventArgs e)
        {
            timerThanhTien.Enabled = false;

            TinhPhiMG();

            if (spinDonGia.Value == 0 && spinDienTich.Value != 0)
            {
                spinDonGia.EditValue = spinThanhTien.Value / spinDienTich.Value;
                lblKetQua.Text = "Kết quả đúng";
            }
            else
                lblKetQua.Text = "Kết quả sai";
        }
    }
}