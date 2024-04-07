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
using BEE.HoatDong.MGL.Ban.Models.MessageQueue;
using RestSharp;
using Newtonsoft.Json;

namespace BEE.HoatDong.MGL.Ban
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? MaBC = 0;
        public bool IsSave;
        public bool IsNoti = false;
        public int? MaKH { get; set; }

        private MasterDataContext db = new MasterDataContext();
        mglbcLichSu objLS = new mglbcLichSu();
        private mglbcBanChoThue objBC;
        private BEE.ThuVien.KhachHang objKH;
        public bool AllowSave = true;
        public bool DislayContac = true;
        public bool? mucdich { get; set; }
        private int totalSeconds;
        public frmEdit()
        {
            InitializeComponent();
        }
        public void PhanQuyenThemSuaKH()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 9;
            DataTable tblAction = o.SelectBy();
            txtHoTenKH.Properties.Buttons[1].Visible = false;
            txtHoTenKH.Properties.Buttons[2].Visible = false;
            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            txtHoTenKH.Properties.Buttons[1].Visible = true;
                            break;
                        case 2:
                            txtHoTenKH.Properties.Buttons[2].Visible = true;
                            break;

                    }
                }
            }
        }
        private void frmEdit_Load(object sender, EventArgs e)
        {
            lookNVQL.Enabled = false;
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

            PhanQuyenThemSuaKH();

            if (MaBC > 0)
            {
                objBC = db.mglbcBanChoThues.Single(p => p.MaBC == this.MaBC);
                LoadPermission();
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
                txtSyncId.EditValue = objBC.syncId;
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
                lookNVQL.Enabled = true;
                SetAddNew();
            }

            if (this.MaKH != null)
            {
                this.objKH = db.KhachHangs.SingleOrDefault(p => p.MaKH == this.MaKH);
                KhachHang_Load();
            }
            if (this.MaBC == null)
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

        public void createKyHieu()
        {

            try
            {
                #region Gen số đăng ký cũ
                //DateTime now = DateTime.Now;
                //DateTime dstart = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                //DateTime dend = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);

                //var data = db.mglbcBanChoThues.Where(o => o.NgayNhap >= dstart && o.NgayNhap <= dend).OrderByDescending(o => o.NgayNhap).FirstOrDefault();
                //string sophieu = "";
                //if (data != null)
                //{
                //    if (string.IsNullOrEmpty(data.KyHieu))
                //    {
                //        sophieu = String.Format("{0:yyyyMMdd}", now) + "00001";

                //    }
                //    else
                //    {
                //        sophieu = NextID(data.KyHieu, String.Format("{0:yyyyMMdd}", now));
                //    }
                //}
                //else
                //{
                //    sophieu = String.Format("{0:yyyyMMdd}", now) + "00001";
                //}  
                #endregion

                var formatDate = db.ExecuteQuery<DateTime>("SELECT GETDATE()").FirstOrDefault();
                txtSoDK.Text = "DKBC-" + formatDate.ToString("yyyyMMddHHmmss");
                txtKyHieu.Text = "BC-" + formatDate.ToString("yyyyMMddHHmmss");



            }
            catch (Exception ex)
            {
            }
        }

        public string NextID(string lastID, string prefixID)
        {
            if (lastID == "")
            {
                return prefixID + "00001";  // fixwidth default
            }
            var nextID = long.Parse(lastID.Remove(0, prefixID.Length)) + 1;
            int lengthNumerID = lastID.Length - prefixID.Length;
            string zeroNumber = "";
            for (int i = 1; i <= lengthNumerID; i++)
            {
                if (nextID < Math.Pow(10, i))
                {
                    for (int j = 1; j <= lengthNumerID - i; i++)
                    {
                        zeroNumber += "0";
                    }
                    return prefixID + zeroNumber + nextID.ToString();
                }
            }
            return prefixID + nextID;

        }

        void SetAddNew()
        {
            objBC = new mglbcBanChoThue();
            createKyHieu();
            this.MaBC = 0;
            string soDK = "", kyHieu = "";
            //db.mglbcBanChoThue_TaoSoPhieu(ref soDK, ref kyHieu);
            //txtSoDK.EditValue = soDK;
            itemDinhKem.Tag = null;
            //Khach hang
            if (this.MaKH == null)
                KhachHang_AddNew();
            //Bat dong san
            //txtKyHieu.EditValue = kyHieu;
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

        public void PhanQuyenSDT()
        {
            int? sbid;
            if ((bool?)rdbLoaiTin.EditValue == true)
            {
                it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 174);
                sbid = o.SDB.SDBID;
            }
            else
            {
                it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 175);
                sbid = o.SDB.SDBID;

            }

            var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);
            switch (sbid)
            {
                case 1:
                    if (obj.DienThoai == false)
                    {
                        txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                        txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
                        txtDiDong4.Text = Common.Right(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoai3Dau == false)
                    {
                        txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                        txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
                        txtDiDong4.Text = Common.Right1(objKH.DiDong4, 3);
                    }
                    else
                    {
                        txtDiDong.Text = objKH.DiDong;
                        txtDiDong2.Text = objKH.DiDong2;
                        txtDiDong3.Text = objKH.DiDong3;
                        txtDiDong4.Text = objKH.DiDong4;

                    }
                    break;

                case 2:
                    if (obj.DienThoai == false)
                    {
                        txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                        txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
                        txtDiDong4.Text = Common.Right(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoai3Dau == false)
                    {
                        txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                        txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
                        txtDiDong4.Text = Common.Right1(objKH.DiDong4, 3);
                    }
                    else
                    {
                        txtDiDong.Text = objKH.DiDong;
                        txtDiDong2.Text = objKH.DiDong2;
                        txtDiDong3.Text = objKH.DiDong3;
                        txtDiDong4.Text = objKH.DiDong4;

                    }

                    break;
                case 3:
                    if (obj.DienThoai == false)
                    {
                        txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                        txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
                        txtDiDong4.Text = Common.Right(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoai3Dau == false)
                    {
                        txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                        txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
                        txtDiDong4.Text = Common.Right1(objKH.DiDong4, 3);
                    }
                    else
                    {
                        txtDiDong.Text = objKH.DiDong;
                        txtDiDong2.Text = objKH.DiDong2;
                        txtDiDong3.Text = objKH.DiDong3;
                        txtDiDong4.Text = objKH.DiDong4;

                    }

                    break;
                case 4:
                    if (obj.DienThoai == false)
                    {
                        txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                        txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
                        txtDiDong4.Text = Common.Right(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoai3Dau == false)
                    {
                        txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                        txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
                        txtDiDong4.Text = Common.Right1(objKH.DiDong4, 3);
                    }
                    else
                    {
                        txtDiDong.Text = objKH.DiDong;
                        txtDiDong2.Text = objKH.DiDong2;
                        txtDiDong3.Text = objKH.DiDong3;
                        txtDiDong4.Text = objKH.DiDong4;

                    }
                    break;



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
            PhanQuyenDienThoai();

            CheckDuplicate();
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
                    frm.LoaiNC = 1;
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
                case 2:
                    KhachHang.KhachHang_frm frmSKH = new KhachHang.KhachHang_frm();
                    frmSKH.MaKH = objKH.MaKH;
                    frmSKH.ShowDialog();
                    if (frmSKH.MaKH != 0)
                    {
                        objKH = db.KhachHangs.Single(p => p.MaKH == frmSKH.MaKH);
                        KhachHang_Load();
                        return;
                    }
                    break;
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
            CheckDuplicate();
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
            try
            {

                #region Rang buoc

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
                if (txtHoTenKH.Text == null || txtHoTenKH.Text == "")
                {
                    DialogBox.Error("Vui lòng nhập thông tin khách hàng");
                    txtHoTenKH.Focus();
                    return;
                }
                CheckDuplicate();

                #region ghi lich su
                if (this.MaBC != 0)
                {
                    UpdateLichSu();
                    objLS = new mglbcLichSu();
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

                objBC.NgayDK = dateNgayDK.DateTime;
                objBC.IsBan = (bool)rdbLoaiTin.EditValue;
                if (this.MaBC == 0 || this.MaBC == null)
                {
                    objBC.MaTT = (byte?)lookTrangThai.EditValue;
                }


                objBC.MaLBDS = (short?)lookLoaiBDS.EditValue;
                objBC.MaDA = (int?)lookDuAn.EditValue;
                //objBC.KyHieu = txtKyHieu.Text;
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

                objBC.KhachHang = objKH;



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
                    objBC.NgayNhap = DateTime.Now;
                    objBC.MaNVN = Common.StaffID;
                    createKyHieu();
                    objBC.SoDK = txtSoDK.Text;
                    objBC.KyHieu = txtKyHieu.Text;
                    objBC.NgayCN = DateTime.Now;

                    db.mglbcBanChoThues.InsertOnSubmit(objBC);
                }
                db.SubmitChanges();
                // update syncid
                try
                {
                    var objUpdate = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == objBC.MaBC);
                    if (objBC.syncId == null || objBC.syncId == "") // neu chua co id
                    {
                        objUpdate.syncId = objBC.syncId = "WF" + objBC.MaBC;
                    }

                    db.SubmitChanges();
                }
                catch (Exception ex)
                {

                }


                #region kafka
                // push data kafka
                var objrealestate = new mglbcBanChoThueModel();
                // khách hàng
                var objkhadd = new List<BEE.HoatDong.MGL.Ban.Models.MessageQueue.KhachHang>();
                var objitemkh = new BEE.HoatDong.MGL.Ban.Models.MessageQueue.KhachHang();
                objitemkh.HoTen = objBC.KhachHang.HoKH + " " + objBC.KhachHang.TenKH;
                objitemkh.DiDong = objKH.DiDong;
                objitemkh.MaKH = objKH.MaKH;
                objitemkh.DiDong2 = objKH.DiDong2;
                objitemkh.DiDong3 = objKH.DiDong3;
                objitemkh.DiDong4 = objKH.DiDong4;
                try
                {
                    objitemkh.AppMaKH = objKH.AppMaKH.ToString();  // thiếu trường
                }
                catch
                {

                }

                objkhadd.Add(objitemkh);
                objrealestate.KhachHang = objkhadd;
                // 
                objrealestate.MaBC = objBC.MaBC;
                objrealestate.MaTT = objBC.MaTT;
                objrealestate.IsBan = objBC.IsBan;
                // ảnh bds
                var lstimg = new List<mglbcAnhbdsModel>();
                foreach (var i in objBC.mglbcAnhbds)
                {
                    var objimgadd = new BEE.HoatDong.MGL.Ban.Models.MessageQueue.mglbcAnhbdsModel();
                    objimgadd.DuongDan = i.DuongDan;

                    if (i.IsS3 == true)
                        objimgadd.isS3 = 1;
                    else
                        objimgadd.isS3 = 0;
                    objimgadd.Position = i.Position;
                    objimgadd.Status = i.Status;
                    lstimg.Add(objimgadd);

                }
                objrealestate.AnhBDS = lstimg;


                // video

                var lstvideo = new List<mglbcVideobdsModel>();
                foreach (var i in db.mglbcVideobds.Where(p => p.MaBC == objBC.MaBC).ToList())
                {
                    var objvideo = new BEE.HoatDong.MGL.Ban.Models.MessageQueue.mglbcVideobdsModel();
                    objvideo.DuongDan = i.DuongDan;

                    if (i.IsS3 == true)
                        objvideo.isS3 = 1;
                    else
                        objvideo.isS3 = 0;
                    objvideo.ViTri = null;
                    objvideo.Position = i.Position;
                    objvideo.Status = i.Status;
                    lstvideo.Add(objvideo);

                }
                objrealestate.VideoBDS = lstvideo;

                // end video

                objrealestate.ViTri = objBC.ToaDo;
                objrealestate.SoNha = objBC.SoNha;
                objrealestate.TenDuong = lookDuong.Text;
                objrealestate.MaDuong = objBC.MaDuong;
                objrealestate.MaXa = objBC.MaXa;
                objrealestate.MaHuyen = objBC.MaHuyen;
                objrealestate.MaTinh = objBC.MaTinh;
                objrealestate.MaLbds = objBC.MaLBDS;
                objrealestate.ThanhTien = objBC.ThanhTien;
                objrealestate.DienTich = objBC.DienTich;
                objrealestate.SoTang = objBC.SoTang;
                objrealestate.NgangXD = objBC.NgangXD;
                objrealestate.MaHuong = objBC.MaHuong;
                objrealestate.DuongRong = objBC.DuongRong;
                objrealestate.MaTTNT = objBC.MaTTNT;
                objrealestate.PhongNgu = objBC.PhongNgu;
                objrealestate.PhongVs = objBC.PhongVS;
                objrealestate.SoTangXD = objBC.SoTangXD;
                objrealestate.PhongBep = objBC.PhongBep;
                objrealestate.PhongAn = objBC.PhongAn;
                objrealestate.PhongKhach = objBC.PhongKhach;
                objrealestate.DieuHoa = objBC.DieuHoa;
                objrealestate.NongLanh = objBC.NongLanh;
                objrealestate.IsThangMay = objBC.IsThangMay;
                objrealestate.TangHam = objBC.TangHam;
                objrealestate.MaSan = objBC.MaSan;
                objrealestate.CuaSo = objBC.CuaSo;
                objrealestate.isBanCong = objBC.isBanCong;
                objrealestate.isSan = objBC.isSan;
                objrealestate.isVuon = objBC.isVuon;
                objrealestate.MaDX = objBC.MaDX;
                objrealestate.KhoangCachDX = objBC.KhoangCachDX;
                objrealestate.DienTichDat = objBC.DienTichDat;
                objrealestate.DienTichXd = objBC.DienTichXD;
                objrealestate.DaiXD = objBC.DaiXD;
                objrealestate.SauXD = objBC.SauXD;
                objrealestate.SauKV = objBC.SauKV;
                objrealestate.NgangKV = objBC.NgangKV;
                objrealestate.MaLd = objBC.MaLD;
                objrealestate.TenLD = lookLoaiDuong.Text;
                objrealestate.PhapLyBDS = null;
                objrealestate.GhiChu = objBC.GhiChu;
                objrealestate.NamXayDung = objBC.NamXayDung;
                objrealestate.DaiKV = objBC.DaiKV; // sau thong thuy


                // phi moi gioi - bo sung
                objrealestate.CachTinhPMG = objBC.CachTinhPMG;
                objrealestate.TyLeMG = objBC.TyLeMG;
                objrealestate.PhiMG = objBC.PhiMG;

                try
                {
                    objrealestate.AppMaDT = objBC.MaNVKD.ToString();
                }
                catch
                {
                }

                objrealestate.MaBC = objBC.MaBC;
                objrealestate.syncId = objBC.syncId;

                try
                {
                    if (this.MaBC == 0 || this.MaBC == null)
                    {
                        objrealestate.AppMaBC = null;

                    }
                    else
                    {
                        objrealestate.AppMaBC = objBC.AppMaBC.ToString();

                    }
                }
                catch
                {
                }

                objrealestate.isUpdate = true;
                var client = new RestClient("http://api-gw.hoaland.com.vn:8085/api/RealEstate/sync-queue-realestate");
                //var client = new RestClient("http://192.168.1.5:8085/api/RealEstate/sync-queue-realestate");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(objrealestate);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                var log = new QueueHistory();
                log.CreateDate = DateTime.Now;
                log.Messger = body;
                log.Response = response.Content;
                db.QueueHistories.InsertOnSubmit(log);
                db.SubmitChanges();
                #endregion


                if (this.MaBC == 0)
                {
                    SaveLichSu(objBC);
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
                    db.SubmitChanges();
                }

                this.MaBC = objBC.MaBC;
                this.IsSave = true;
                SetEnableControl(false);
                #endregion


            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
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

            CheckDuplicate();
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

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            if (objBC.IsBan == true)
            {
                o.AccessData.Form.FormID = 174;
            }
            else
            {
                o.AccessData.Form.FormID = 175;
            }

            DataTable tblAction = o.SelectBy();


            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {

                        case 87://nhân viên quản lý
                            lookNVQL.Enabled = true;
                            break;

                    }
                }
            }
        }
        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 174);

            return o.SDB.SDBID;
        }
        public void PhanQuyenDienThoai()
        {
            if (objKH == null) return;
            int sbid;
            if ((bool?)rdbLoaiTin.EditValue == true)
            {
                it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 174);
                sbid = o.SDB.SDBID;
            }
            else
            {
                it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 175);
                sbid = o.SDB.SDBID;

            }

            var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);
            switch (sbid)
            {
                case 1:
                    if (obj.DienThoai == false)
                    {
                        txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                        txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
                        txtDiDong4.Text = Common.Right(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoai3Dau == false)
                    {
                        txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                        txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
                        txtDiDong4.Text = Common.Right1(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoaiAn == false)
                    {
                        txtDiDong.Text = "";
                        txtDiDong2.Text = "";
                        txtDiDong3.Text = "";
                        txtDiDong4.Text = "";
                    }
                    else
                    {
                        txtDiDong.Text = objKH.DiDong;
                        txtDiDong2.Text = objKH.DiDong2;
                        txtDiDong3.Text = objKH.DiDong3;
                        txtDiDong4.Text = objKH.DiDong4;

                    }
                    break;

                case 2:
                    if (obj.DienThoai == false)
                    {
                        txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                        txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
                        txtDiDong4.Text = Common.Right(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoai3Dau == false)
                    {
                        txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                        txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
                        txtDiDong4.Text = Common.Right1(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoaiAn == false)
                    {
                        txtDiDong.Text = "";
                        txtDiDong2.Text = "";
                        txtDiDong3.Text = "";
                        txtDiDong4.Text = "";
                    }
                    else
                    {
                        txtDiDong.Text = objKH.DiDong;
                        txtDiDong2.Text = objKH.DiDong2;
                        txtDiDong3.Text = objKH.DiDong3;
                        txtDiDong4.Text = objKH.DiDong4;

                    }

                    break;
                case 3:
                    if (obj.DienThoai == false)
                    {
                        txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                        txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
                        txtDiDong4.Text = Common.Right(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoai3Dau == false)
                    {
                        txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                        txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
                        txtDiDong4.Text = Common.Right1(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoaiAn == false)
                    {
                        txtDiDong.Text = "";
                        txtDiDong2.Text = "";
                        txtDiDong3.Text = "";
                        txtDiDong4.Text = "";
                    }
                    else
                    {
                        txtDiDong.Text = objKH.DiDong;
                        txtDiDong2.Text = objKH.DiDong2;
                        txtDiDong3.Text = objKH.DiDong3;
                        txtDiDong4.Text = objKH.DiDong4;

                    }

                    break;
                case 4:
                    if (obj.DienThoai == false)
                    {
                        txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                        txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
                        txtDiDong4.Text = Common.Right(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoai3Dau == false)
                    {
                        txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                        txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                        txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
                        txtDiDong4.Text = Common.Right1(objKH.DiDong4, 3);
                    }
                    else if (obj.DienThoaiAn == false)
                    {
                        txtDiDong.Text = "";
                        txtDiDong2.Text = "";
                        txtDiDong3.Text = "";
                        txtDiDong4.Text = "";
                    }
                    else
                    {
                        txtDiDong.Text = objKH.DiDong;
                        txtDiDong2.Text = objKH.DiDong2;
                        txtDiDong3.Text = objKH.DiDong3;
                        txtDiDong4.Text = objKH.DiDong4;

                    }
                    break;
            }
        }

        private void CheckDuplicate()
        {
            try
            {
                if (objKH.MaKH != 0 && lookDuong.EditValue != null && lookHuyen.EditValue != null && lookTinh.EditValue != null && !string.IsNullOrEmpty(txtSoNha.Text.Trim()))
                {
                    var lstBc = db.mglbcBanChoThues.Where(p => p.MaKH == (int?)objKH.MaKH && p.MaDuong == (int?)lookDuong.EditValue
                && p.MaHuyen == (short)lookHuyen.EditValue && p.MaTinh == (byte)lookTinh.EditValue).ToList();
                    if (MaBC != 0)
                    {
                        lstBc = lstBc.Where(p => p.MaBC != MaBC).ToList();
                    }

                    if (lstBc.Where(p => p.SoNha == txtSoNha.Text.Trim()).Count() > 0)
                    {
                        if (objKH.MaKH != 0 && lookDuong.EditValue != null && lookHuyen.EditValue != null && lookTinh.EditValue != null && !string.IsNullOrEmpty(txtSoNha.Text.Trim()))
                        {
                            itemLuu.Enabled = false;
                            //vẫn cho lưu
                            if (lstBc.Count > 0) DialogBox.Error("BDS này đã có trên hệ thống HOALAND");
                        }
                    }
                    else if (lstBc.Count > 0)
                    {
                        if (objKH.MaKH != 0 && lookDuong.EditValue != null && lookHuyen.EditValue != null && lookTinh.EditValue != null)
                        {
                            itemLuu.Enabled = true;
                            //Không cho lưu
                            DialogBox.Warning($"Số điện thoại này đã có BĐS trên tuyến đường {lookDuong.Text}, hãy kiếm tra kỹ lại [Số nhà]");
                        }
                    }
                }
            }
            catch { }
        }

        private void lookDuong_EditValueChanged(object sender, EventArgs e)
        {
            CheckDuplicate();
        }

        private void SaveLichSu(mglbcBanChoThue objBC)
        {
            var objLSAdd = new mglbcHistoryChange();
            objLSAdd.NgayDK = objBC.NgayDK;
            objLSAdd.IsBan = objBC.IsBan;
            objLSAdd.MaTT = (byte?)lookTrangThai.EditValue;
            objLSAdd.MaLBDS = (short?)lookLoaiBDS.EditValue;
            objLSAdd.MaDA = (int?)lookDuAn.EditValue;
            objLSAdd.TenHuyen = lookHuyen.Text;
            objLSAdd.TenTinh = lookTinh.Text;
            objLSAdd.TenXa = lookXa.Text;
            objLSAdd.TenDuong = lookDuong.Text;
            objLSAdd.SoNha = txtSoNha.Text.Trim();
            objLSAdd.DienTich = (decimal?)spinDienTich.EditValue;
            objLSAdd.DonGia = (decimal?)spinDonGia.EditValue;
            objLSAdd.ThanhTien = (decimal?)spinThanhTien.EditValue;
            objLSAdd.IsCanGoc = (bool?)chkCanGoc.Checked;
            objLSAdd.TenTinhTrang = lookTinhTrang.Text;
            objLSAdd.TyLeMG = (decimal?)spinMG.EditValue;
            objLSAdd.CachTinhPMG = Convert.ToInt16(cmbCachTinhPhi.SelectedIndex);
            objLSAdd.PhiMG = (decimal?)spinThanhTienMG.EditValue;
            objLSAdd.MaNVKD = (int?)lookNVMG.EditValue;
            objLSAdd.MaNVQL = (int?)lookNVQL.EditValue;
            objLSAdd.TenPL = lookPhapLy.Text;
            objLSAdd.TrangThaiHDMG = (byte?)lookHDMG.EditValue;
            objLSAdd.MaTTD = 2;

            objLSAdd.DienTichDat = (decimal?)spinDienTichDat.EditValue;
            objLSAdd.GhiChu = txtGhiChu.Text;
            objLSAdd.ToaDo = txtToaDo.Text;
            objLSAdd.ThoiGianBGMB = (DateTime?)dateThoiGianBG.EditValue;
            objLSAdd.ThoiGianHD = (DateTime?)dateThoiHanHD.EditValue;
            objLSAdd.DienTichXD = (decimal?)spinDientichtXD.EditValue;
            objLSAdd.SoTangXD = Convert.ToInt32(spinSoTangXD.EditValue);
            objLSAdd.DonViThueCu = txtDonVITC.Text;
            objLSAdd.DonViDangThue = txtDonViDT.Text;
            objLSAdd.GioiThieu = txtGioiThieu.InnerHtml;
            objLSAdd.KinhDo = objBC.KinhDo;
            objLSAdd.ViDo = objBC.ViDo;
            objLSAdd.MaLT = (byte?)lookLoaiTien.EditValue;
            objLSAdd.TienIch = cmbTienIch.Text;
            objLSAdd.SoDK = txtSoDK.Text;
            objLSAdd.KyHieu = txtKyHieu.Text;
            objLSAdd.NgayCN = objBC.NgayCN;


            if (this.MaBC == 0)
            {
                objLSAdd.TenHuong = lookHuong.Text;
                objLSAdd.HuongBanCong = (short?)lookHuongBC.EditValue;
                objLSAdd.DacTrung = txtDacTrung.Text;
                objLSAdd.NamXayDung = Convert.ToInt32(spinNamXD.EditValue);
                objLSAdd.TenNguon = lookNguon.Text;
                objLSAdd.TenLD = lookLoaiDuong.Text;
                objLSAdd.DuongRong = (decimal?)spinDuongRong.EditValue;
                objLSAdd.PhongNgu = Convert.ToByte(spinPhongNgu.EditValue);
                objLSAdd.PhongVS = Convert.ToByte(spinPhongVS.EditValue);
                objLSAdd.SoTang = Convert.ToByte(spinSoTang.EditValue);
                objLSAdd.NgangXD = (decimal?)spinMatTien.EditValue;
                objLSAdd.DaiXD = (decimal?)spinDai.EditValue;
                objLSAdd.SauXD = (decimal?)spinMatSau.EditValue;
                objLSAdd.NgangKV = (decimal?)spinMatTienTT.EditValue;
                objLSAdd.DaiKV = (decimal?)spinDaiTT.EditValue;
                objLSAdd.SauKV = (decimal?)spinMatSauTT.EditValue;
                objLSAdd.ThuongLuong = (bool?)ckbThuongLuong.Checked;
                objLSAdd.DauOto = (bool?)ckbDoOto.Checked;
                objLSAdd.TangHam = (bool?)ckbCoTangHam.Checked;
                objLSAdd.IsCanGoc = (bool?)chkCanGoc.Checked;
                objLSAdd.IsThangMay = (bool?)chkThangMay.Checked;

                objLSAdd.MaKH = objKH.MaKH;
                objLSAdd.MaNVS = Common.StaffID;
                objLSAdd.NgaySua = DateTime.Now;
                objLSAdd.MaBC = objBC.MaBC;
                db.mglbcHistoryChanges.InsertOnSubmit(objLSAdd);
                db.SubmitChanges();
            }
        }

        private void UpdateLichSu()
        {
            if (this.MaBC != 0)
            {
                var objLSNew = new mglbcHistoryChange();
                bool isChange = false;
                var objBC = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == MaBC);
                if (objBC.NgayDK != (DateTime)dateNgayDK.EditValue)
                {
                    isChange = true;
                    objLSNew.NgayDK = objBC.NgayDK;
                }
                if (objBC.IsBan != (bool)rdbLoaiTin.EditValue)
                {
                    isChange = true;
                    objLSNew.IsBan = objBC.IsBan;
                }
                if (objBC.MaTT != (byte?)lookTrangThai.EditValue)
                {
                    isChange = true;
                    objLSNew.MaTT = (byte?)lookTrangThai.EditValue;
                }
                if (objBC.MaLBDS != (short?)lookLoaiBDS.EditValue)
                {
                    isChange = true;
                    objLSNew.MaLBDS = (short?)lookLoaiBDS.EditValue;
                }
                if (objBC.MaDA != (int?)lookDuAn.EditValue)
                {
                    isChange = true;
                    objLSNew.MaDA = (int?)lookDuAn.EditValue;
                }
                if (objBC.MaHuyen != (short)lookHuyen.EditValue)
                {
                    isChange = true;
                    objLSNew.TenHuyen = lookHuyen.Text;
                }
                if (objBC.MaTinh != (byte)lookTinh.EditValue)
                {
                    isChange = true;
                    objLSNew.TenTinh = lookTinh.Text;
                }
                if (objBC.MaXa != (int)lookXa.EditValue)
                {
                    isChange = true;
                    objLSNew.TenXa = lookXa.Text;
                }
                if (objBC.MaDuong != (int)lookDuong.EditValue)
                {
                    isChange = true;
                    objLSNew.TenDuong = lookDuong.Text;
                }
                if (objBC.SoNha != txtSoNha.Text.Trim())
                {
                    isChange = true;
                    objLSNew.SoNha = txtSoNha.Text.Trim();
                }
                if (objBC.DienTich != (decimal?)spinDienTich.EditValue)
                {
                    isChange = true;
                    objLSNew.DienTich = (decimal?)spinDienTich.EditValue;
                }
                if (objBC.DonGia != (decimal?)spinDonGia.EditValue)
                {
                    isChange = true;
                    objLSNew.DonGia = (decimal?)spinDonGia.EditValue;
                }
                if (objBC.ThanhTien != (decimal?)spinThanhTien.EditValue)
                {
                    isChange = true;
                    objLSNew.ThanhTien = (decimal?)spinThanhTien.EditValue;
                }
                if (objBC.IsCanGoc != (bool?)chkCanGoc.Checked)
                {
                    isChange = true;
                    objLSNew.IsCanGoc = (bool?)chkCanGoc.Checked;
                }
                if (objBC.MaTinhTrang != (byte?)lookTinhTrang.EditValue)
                {
                    isChange = true;
                    objLSNew.TenTinhTrang = lookTinhTrang.Text;
                }
                if (objBC.TyLeMG != (decimal?)spinMG.EditValue)
                {
                    isChange = true;
                    objLSNew.TyLeMG = (decimal?)spinMG.EditValue;
                }
                if (objBC.CachTinhPMG != Convert.ToInt16(cmbCachTinhPhi.SelectedIndex))
                {
                    isChange = true;
                    objLSNew.CachTinhPMG = Convert.ToInt16(cmbCachTinhPhi.SelectedIndex);
                }
                if (objBC.PhiMG != (decimal?)spinThanhTienMG.EditValue)
                {
                    isChange = true;
                    objLSNew.PhiMG = (decimal?)spinThanhTienMG.EditValue;
                }
                if (objBC.MaNVKD != (int?)lookNVMG.EditValue)
                {
                    isChange = true;
                    objLSNew.MaNVKD = (int?)lookNVMG.EditValue;
                }
                if (objBC.MaNVQL != (int?)lookNVQL.EditValue)
                {
                    isChange = true;
                    objLSNew.MaNVQL = (int?)lookNVQL.EditValue;
                }
                if (objBC.MaPL != (short?)lookPhapLy.EditValue)
                {
                    isChange = true;
                    objLSNew.TenPL = lookPhapLy.Text;
                }
                if (objBC.TrangThaiHDMG != (byte?)lookHDMG.EditValue)
                {
                    isChange = true;
                    objLSNew.TrangThaiHDMG = (byte?)lookHDMG.EditValue;
                }

                if (objBC.DienTichDat != (decimal?)spinDienTichDat.EditValue)
                {
                    isChange = true;
                    objLSNew.DienTichDat = (decimal?)spinDienTichDat.EditValue;
                }
                if (objBC.GhiChu != txtGhiChu.Text)
                {
                    isChange = true;
                    objLSNew.GhiChu = txtGhiChu.Text;
                }
                if (objBC.ToaDo != txtToaDo.Text)
                {
                    isChange = true;
                    objLSNew.ToaDo = txtToaDo.Text;
                }
                if (objBC.ThoiGianBGMB != (DateTime?)dateThoiGianBG.EditValue)
                {
                    isChange = true;
                    objLSNew.ThoiGianBGMB = (DateTime?)dateThoiGianBG.EditValue;
                }
                if (objBC.SoTangXD != Convert.ToInt32(spinSoTangXD.EditValue))
                {
                    isChange = true;
                    objLSNew.SoTangXD = Convert.ToInt32(spinSoTangXD.EditValue);
                }
                if (objBC.ThoiGianHD != (DateTime?)dateThoiHanHD.EditValue)
                {
                    isChange = true;
                    objLSNew.ThoiGianHD = (DateTime?)dateThoiHanHD.EditValue;
                }
                if (objBC.DienTichXD != (decimal?)spinDientichtXD.EditValue)
                {
                    isChange = true;
                    objLSNew.DienTichXD = (decimal?)spinDientichtXD.EditValue;
                }
                if (objBC.DonViThueCu != txtDonVITC.Text)
                {
                    isChange = true;
                    objLSNew.DonViThueCu = txtDonVITC.Text;
                }
                if (objBC.DonViDangThue != txtDonViDT.Text)
                {
                    isChange = true;
                    objLSNew.DonViDangThue = txtDonViDT.Text;
                }
                if (objBC.GioiThieu != txtGioiThieu.InnerHtml)
                {
                    isChange = true;
                    objLSNew.GioiThieu = txtGioiThieu.InnerHtml;
                }
                if (objBC.KinhDo != objBC.KinhDo)
                {
                    isChange = true;
                    objLSNew.KinhDo = objBC.KinhDo;
                }
                if (objBC.ViDo != objBC.ViDo)
                {
                    isChange = true;
                    objLSNew.ViDo = objBC.ViDo;
                }
                if (objBC.MaLT != (byte?)lookLoaiTien.EditValue)
                {
                    isChange = true;
                    objLSNew.MaLT = (byte?)lookLoaiTien.EditValue;
                }
                if (objBC.TienIch != cmbTienIch.Text)
                {
                    isChange = true;
                    objLSNew.TienIch = cmbTienIch.Text;
                }
                if (objBC.KyHieu != txtKyHieu.Text)
                {
                    isChange = true;
                    objLSNew.KyHieu = txtKyHieu.Text;
                }

                if (objBC.MaHuong != (short?)lookHuong.EditValue)
                {
                    isChange = true;
                    objLSNew.TenHuong = lookHuong.Text;
                }
                if (objBC.HuongBanCong != (short?)lookHuongBC.EditValue)
                {
                    isChange = true;
                    objLSNew.HuongBanCong = (short?)lookHuongBC.EditValue;
                }
                if (objBC.DacTrung != txtDacTrung.Text)
                {
                    isChange = true;
                    objLSNew.DacTrung = txtDacTrung.Text;
                }
                if (objBC.NamXayDung != Convert.ToInt32(spinNamXD.EditValue))
                {
                    isChange = true;
                    objLSNew.NamXayDung = Convert.ToInt32(spinNamXD.EditValue);
                }
                if (objBC.MaNguon != (short?)lookNguon.EditValue)
                {
                    isChange = true;
                    objLSNew.TenNguon = lookNguon.Text;
                }
                if (objBC.MaLD != (short?)lookLoaiDuong.EditValue)
                {
                    isChange = true;
                    objLSNew.TenLD = lookLoaiDuong.Text;
                }
                if (objBC.DuongRong != (decimal?)spinDuongRong.EditValue)
                {
                    isChange = true;
                    objLSNew.DuongRong = (decimal?)spinDuongRong.EditValue;
                }
                if (objBC.PhongNgu != Convert.ToByte(spinPhongNgu.EditValue))
                {
                    isChange = true;
                    objLSNew.PhongNgu = Convert.ToByte(spinPhongNgu.EditValue);
                }
                if (objBC.PhongVS != Convert.ToByte(spinPhongVS.EditValue))
                {
                    isChange = true;
                    objLSNew.PhongVS = Convert.ToByte(spinPhongVS.EditValue);
                }
                if (objBC.SoTang != Convert.ToByte(spinSoTang.EditValue))
                {
                    isChange = true;
                    objLSNew.SoTang = Convert.ToByte(spinSoTang.EditValue);
                }
                if (objBC.NgangXD != (decimal?)spinMatTien.EditValue)
                {
                    isChange = true;
                    objLSNew.NgangXD = (decimal?)spinMatTien.EditValue;
                }
                if (objBC.DaiXD != (decimal?)spinDai.EditValue)
                {
                    isChange = true;
                    objLSNew.DaiXD = (decimal?)spinDai.EditValue;
                }
                if (objBC.SauXD != (decimal?)spinMatSau.EditValue)
                {
                    isChange = true;
                    objLSNew.SauXD = (decimal?)spinMatSau.EditValue;
                }
                if (objBC.NgangKV != (decimal?)spinMatTienTT.EditValue)
                {
                    isChange = true;
                    objLSNew.NgangKV = (decimal?)spinMatTienTT.EditValue;
                }
                if (objBC.DaiKV != (decimal?)spinDaiTT.EditValue)
                {
                    isChange = true;
                    objLSNew.DaiKV = (decimal?)spinDaiTT.EditValue;
                }
                if (objBC.SauKV != (decimal?)spinMatSauTT.EditValue)
                {
                    isChange = true;
                    objLSNew.SauKV = (decimal?)spinMatSauTT.EditValue;
                }
                if (objBC.ThuongLuong != (bool?)ckbThuongLuong.Checked)
                {
                    isChange = true;
                    objLSNew.ThuongLuong = (bool?)ckbThuongLuong.Checked;
                }
                if (objBC.DauOto != (bool?)ckbDoOto.Checked)
                {
                    isChange = true;
                    objLSNew.DauOto = (bool?)ckbDoOto.Checked;
                }
                if (objBC.TangHam != (bool?)ckbCoTangHam.Checked)
                {
                    isChange = true;
                    objLSNew.TangHam = (bool?)ckbCoTangHam.Checked;
                }
                if (objBC.IsCanGoc != (bool?)chkCanGoc.Checked)
                {
                    isChange = true;
                    objLSNew.IsCanGoc = (bool?)chkCanGoc.Checked;
                }
                if (objBC.IsThangMay != (bool?)chkThangMay.Checked)
                {
                    isChange = true;
                    objLSNew.IsThangMay = (bool?)chkThangMay.Checked;
                }
                if (objBC.MaKH != objKH.MaKH)
                {
                    isChange = true;
                    objLSNew.MaKH = objKH.MaKH;
                }
                if (isChange)
                {
                    objLSNew.MaNVS = Common.StaffID;
                    objLSNew.NgaySua = DateTime.Now;
                    objLSNew.MaBC = objBC.MaBC;
                    db.mglbcHistoryChanges.InsertOnSubmit(objLSNew);
                }
            }
        }

        private void txtSoNha_TextChanged(object sender, EventArgs e)
        {
            timerCheckSoNha.Stop();
            timerCheckSoNha.Enabled = true;
        }

        private void timerCheckSoNha_Tick(object sender, EventArgs e)
        {
            timerCheckSoNha.Stop();
            CheckDuplicate();
        }
    }
}