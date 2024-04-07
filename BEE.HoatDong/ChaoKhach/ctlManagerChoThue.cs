using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq.SqlClient;
using BEE.ThuVien;
using BEEREMA;
using BEE.KhachHang;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data.Linq;

namespace BEE.HoatDong.ChaoKhach
{
    public partial class ctlManagerChoThue : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();
        public bool TypeID { get; set; }

        #region ctlManagerChoThue
        public ctlManagerChoThue()
        {
            InitializeComponent();

            paging1.PageRows = 10;
            paging1.PageClicked += new CongCu.Paging.PageClick(this.ToolStripButtonClick);
            paging1.CmbClicked += new CongCu.Paging.CmbClick(this.cmbPageRows_SelectedIndexChanged);

            //panelControl1.Height = 0;
        }
        #endregion

        #region ToolStripButtonClick
        private void ToolStripButtonClick(object sender, CongCu.PageClickEventHandler e)
        {
            //MessageBox.Show("Page " + e.SelectedPage.ToString());
            if (itemTrangThai.EditValue != "Chọn trạng thái" && itemTrangThai.EditValue != "")
            {
                Ban_Load();
                phanquyen();
                grvBan.BestFitColumns();
            }
        }
        #endregion

        #region cmbPageRows_SelectedIndexChanged
        private void cmbPageRows_SelectedIndexChanged(object sender, CongCu.PageClickEventHandler e)
        {
            //MessageBox.Show("Page " + e.SelectedPage.ToString());
            if (itemTrangThai.EditValue != "Chọn trạng thái" && itemTrangThai.EditValue != "")
            {
                Ban_Load();
                phanquyen();
                grvBan.BestFitColumns();
            }
        }
        #endregion

        #region LoadPermission
        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 175;
            DataTable tblAction = o.SelectBy();
            itmThem.Enabled = false;
            itemSua.Enabled = false;
            itemXoa.Enabled = false;
            itemExport.Enabled = false;
            itemImport.Enabled = false;
            itemMoBan.Enabled = false;
            itemUp.Enabled = false;
            itemNgungBan.Enabled = false;
            itemMoBan.Enabled = false;
            itemGiaoDich.Enabled = false;
            itemCauHinhTinh.Enabled = false;
            itemCauHinhTG.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            itmThem.Enabled = true;
                            break;
                        case 2:
                            itemSua.Enabled = true;
                            break;
                        case 3:
                            itemXoa.Enabled = true;
                            break;
                        case 13:
                            itemExport.Enabled = true;
                            break;
                        case 16:
                            itemImport.Enabled = true;
                            break;
                        case 74:
                            itemMoBan.Enabled = true;
                            break;
                        case 75:
                            itemNgungBan.Enabled = true;
                            break;
                        case 76://Nguoi dai dien
                            itemUp.Enabled = true;
                            break;
                        case 77://Nguoi gioi thieu
                            itemGiaoDich.Enabled = true;
                            break;
                        case 85://cấu hình tỉnh
                            itemCauHinhTinh.Enabled = true;
                            break;
                        case 86://cấu hình thời gian
                            itemCauHinhTG.Enabled = true;
                            break;
                    }
                }
            }
        }
        #endregion

        #region GetAccessData
        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 175);

            return o.SDB.SDBID;
        }
        #endregion

        #region Ban_Load
        private void Ban_Load()
        {
            var tuNgay = (DateTime?)itemTuNgay.EditValue ?? DateTime.Now;
            var denNgay = (DateTime?)itemDenNgay.EditValue ?? DateTime.Now;
            var strMaTT = (itemTrangThai.EditValue ?? "").ToString().Replace(" ", "");
            var arrMaTT = "," + strMaTT + ",";
            int MaNV = Common.StaffID;
            var wait = DialogBox.WaitingForm();
            try
            {
                if (itemTuNgay.EditValue == null || itemDenNgay.EditValue == null)
                {
                    gcBan.DataSource = null;
                    return;
                }

                var tinh = (itemTinh.EditValue + "").ToString().Replace(" ", "");
                var matinh = "," + tinh + ",";

                var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);

                var objt = db.crlHuyenQuanLies.Where(p => p.MaNV == Common.StaffID);
                var lockhuvuc = ",";
                foreach (var item in objt)
                {
                    lockhuvuc += item.MaHuyen + ",";
                }

                int TotalRecord = 0;

                gcBan.DataSource = null;

                switch (GetAccessData())
                {

                    case 1://Tat ca
                        db.CommandTimeout = 90000;
                        #region Tat ca

                        #region  if (obj.DienThoai == false)
                        if (obj.DienThoai == false)
                        {
                            gcBan.DataSource = db.mglbcBanChoThue_getByDate_Paging_Search
                                (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1, MaNV, true, matinh,
                                lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(),
                                txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue,
                                (DateTime?)dateThoiHanHD.EditValue, txtNguon.Text, txtTinhTrangHD.Text,
                                txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text,
                                txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue,
                                txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                )
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    DienThoai = Common.Right(p.DienThoai, 3),
                                    DiDong2 = Common.Right(p.DiDong2, 3),
                                    DiDong3 = Common.Right(p.DiDong3, 3),
                                    DiDong4 = Common.Right(p.DiDong4, 3)
                                }).ToList();

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1,
                                    MaNV, true, matinh, lockhuvuc, txtDiaChi.Text.Trim(),
                                    txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                    (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                    (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                    (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                    txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                    txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                    cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text

                                    ).ToList();
                                if (dtc.Count > 0)
                                {
                                    foreach (var item in dtc)
                                        TotalRecord = (int)item.sl;
                                }
                            }
                            catch (Exception ex)
                            { }
                            paging1.TotalRecords = TotalRecord;
                            paging1.RefreshPagination();


                        }
                        #endregion

                        #region else if (obj.DienThoai3Dau == false)
                        else if (obj.DienThoai3Dau == false)
                        {

                            gcBan.DataSource = db.mglbcBanChoThue_getByDate_Paging_Search
                                (tuNgay, denNgay, arrMaTT, false,
                                -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc, paging1.CurrentPage,
                                paging1.PageRows, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue,
                                (DateTime?)dateThoiHanHD.EditValue, txtNguon.Text, txtTinhTrangHD.Text,
                                txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text,
                                txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                )
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    DienThoai = Common.Right1(p.DienThoai, 3),
                                    DiDong2 = Common.Right1(p.DiDong2, 3),
                                    DiDong3 = Common.Right1(p.DiDong3, 3),
                                    DiDong4 = Common.Right1(p.DiDong4, 3)
                                }).ToList();

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1,
                                    MaNV, true, matinh, lockhuvuc, txtDiaChi.Text.Trim(),
                                    txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                    (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                    (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                    (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                    txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                    txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                    cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    ).ToList();
                                if (dtc.Count > 0)
                                {
                                    foreach (var item in dtc)
                                        TotalRecord = (int)item.sl;
                                }
                            }
                            catch (Exception ex)
                            { DialogBox.Error(ex.Message); }
                            paging1.TotalRecords = TotalRecord;
                            paging1.RefreshPagination();


                        }
                        #endregion
                        #region else if (obj.dienthoaian == false)
                        else if (obj.DienThoaiAn == false)
                        {

                            gcBan.DataSource = db.mglbcBanChoThue_getByDate_Paging_Search
                                (tuNgay, denNgay, arrMaTT, false,
                                -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc, paging1.CurrentPage,
                                paging1.PageRows, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue,
                                (DateTime?)dateThoiHanHD.EditValue, txtNguon.Text, txtTinhTrangHD.Text,
                                txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text,
                                txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                )
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    DienThoai = "",
                                    DiDong2 = "",
                                    DiDong3 = "",
                                    DiDong4 = ""
                                }).ToList();

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1,
                                    MaNV, true, matinh, lockhuvuc, txtDiaChi.Text.Trim(),
                                    txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                    (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                    (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                    (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                    txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                    txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                    cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    ).ToList();
                                if (dtc.Count > 0)
                                {
                                    foreach (var item in dtc)
                                        TotalRecord = (int)item.sl;
                                }
                            }
                            catch (Exception ex)
                            { DialogBox.Error(ex.Message); }
                            paging1.TotalRecords = TotalRecord;
                            paging1.RefreshPagination();


                        }
                        #endregion
                        #region else
                        else
                        {
                            gcBan.DataSource = db.mglbcBanChoThue_getByDate_Paging_Search
                                (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1, MaNV, true,
                                matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows,
                                txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text,
                                txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text,
                                (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                )
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    p.DienThoai,
                                    p.DiDong2,
                                    p.DiDong3,
                                    p.DiDong4
                                }).ToList();
                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1,
                                    MaNV, true, matinh, lockhuvuc, txtDiaChi.Text.Trim(),
                                    txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                    (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                    (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                    (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                    txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                    txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text
                                    , cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    ).ToList();
                                if (dtc.Count > 0)
                                {
                                    foreach (var item in dtc)
                                        TotalRecord = (int)item.sl;
                                }
                            }
                            catch (Exception ex)
                            { }
                            paging1.TotalRecords = TotalRecord;
                            paging1.RefreshPagination();


                        }
                        #endregion

                        #endregion
                        break;
                    case 2://Theo phong ban 
                        db.CommandTimeout = 90000;
                        #region phong ban

                        #region if (obj.DienThoai == false)
                        if (obj.DienThoai == false)
                        {
                            gcBan.DataSource = db.mglbcBanChoThue_getByDate_Paging_Search
                                (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID,
                                MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows,
                                txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text
                                , cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                )
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    p.MauNen,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    p.SoGiay,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    DienThoai = Common.Right(p.DienThoai, 3),
                                    DiDong2 = Common.Right(p.DiDong2, 3),
                                    DiDong3 = Common.Right(p.DiDong3, 3),
                                    DiDong4 = Common.Right(p.DiDong4, 3)
                                }).ToList();//.Where(p => p.IsBan.GetValueOrDefault() == true);

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID,
                                    MaNV, false, matinh, lockhuvuc, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                    txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                    txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                    txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text,
                                    (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                    cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    ).ToList();
                                if (dtc.Count > 0)
                                {
                                    foreach (var item in dtc)
                                        TotalRecord = (int)item.sl;
                                }
                            }
                            catch (Exception ex)
                            { }
                            paging1.TotalRecords = TotalRecord;
                            paging1.RefreshPagination();



                        }
                        #endregion

                        #region else if (obj.DienThoai3Dau == false)
                        else if (obj.DienThoai3Dau == false)
                        {
                            gcBan.DataSource = db.mglbcBanChoThue_getByDate_Paging_Search
                                (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV,
                                false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(),
                                txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text,
                                txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text,
                                txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text,
                                (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                )
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MauNen,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.MaTT,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    DienThoai = Common.Right1(p.DienThoai, 3),
                                    DiDong2 = Common.Right1(p.DiDong2, 3),
                                    DiDong3 = Common.Right1(p.DiDong3, 3),
                                    DiDong4 = Common.Right1(p.DiDong4, 3)
                                }).ToList();

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV,
                                    false, matinh, lockhuvuc, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                    txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                    txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                    txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text,
                                    (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                    cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    ).ToList();
                                if (dtc.Count > 0)
                                {
                                    foreach (var item in dtc)
                                        TotalRecord = (int)item.sl;
                                }
                            }
                            catch (Exception ex)
                            { }
                            paging1.TotalRecords = TotalRecord;
                            paging1.RefreshPagination();


                        }
                        #endregion
                        #region else if (obj.DienThoaian == false)
                        else if (obj.DienThoaiAn == false)
                        {
                            gcBan.DataSource = db.mglbcBanChoThue_getByDate_Paging_Search
                                (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV,
                                false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(),
                                txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text,
                                txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text,
                                txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text,
                                (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                )
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MauNen,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.MaTT,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    DienThoai = "",
                                    DiDong2 = "",
                                    DiDong3 = "",
                                    DiDong4 = ""
                                }).ToList();

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV,
                                    false, matinh, lockhuvuc, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                    txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                    txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                    txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text,
                                    (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                    cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    ).ToList();
                                if (dtc.Count > 0)
                                {
                                    foreach (var item in dtc)
                                        TotalRecord = (int)item.sl;
                                }
                            }
                            catch (Exception ex)
                            { }
                            paging1.TotalRecords = TotalRecord;
                            paging1.RefreshPagination();


                        }
                        #endregion
                        #region else
                        else
                        {
                            gcBan.DataSource = db.mglbcBanChoThue_getByDate_Paging_Search
                                (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV,
                                false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(),
                                txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue, txtNguon.Text, txtTinhTrangHD.Text,
                                txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text,
                                txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text
                                , cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                )
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    p.DienThoai,
                                    p.DiDong2,
                                    p.DiDong3,
                                    p.DiDong4
                                }).ToList();

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false,
                                    matinh, lockhuvuc, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text,
                                    txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                    txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                    txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text,
                                    (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                    cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    ).ToList();
                                if (dtc.Count > 0)
                                {
                                    foreach (var item in dtc)
                                        TotalRecord = (int)item.sl;
                                }
                            }
                            catch (Exception ex)
                            { }
                            paging1.TotalRecords = TotalRecord;
                            paging1.RefreshPagination();


                        }
                        #endregion

                        #endregion
                        break;
                    case 3://Theo nhom
                        db.CommandTimeout = 90000;
                        #region Theo nhom

                        #region  if (obj.DienThoai == false)
                        if (obj.DienThoai == false)
                        {
                            gcBan.DataSource = db.mglbcBanChoThue_getByDate_Paging_Search
                                (tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false,
                                matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(),
                                txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue, txtNguon.Text, txtTinhTrangHD.Text,
                                txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text,
                                txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text
                                , cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                )
                                .Select(p => new
                                {
                                    DiDong2 = Common.Right(p.DiDong2, 3),
                                    DiDong3 = Common.Right(p.DiDong3, 3),
                                    DiDong4 = Common.Right(p.DiDong4, 3),
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MauNen,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.MaTT,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    DienThoai = Common.Right(p.DienThoai, 3)
                                }).ToList();//.Where(p => p.IsBan.GetValueOrDefault() == true);

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false,
                                    matinh, lockhuvuc, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text,
                                    txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                    (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                    (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                    txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                    txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text
                                    , cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    ).ToList();
                                if (dtc.Count > 0)
                                {
                                    foreach (var item in dtc)
                                        TotalRecord = (int)item.sl;
                                }
                            }
                            catch (Exception ex)
                            { }
                            paging1.TotalRecords = TotalRecord;
                            paging1.RefreshPagination();


                        }
                        #endregion

                        #region else if (obj.DienThoai3Dau == false)
                        else if (obj.DienThoai3Dau == false)
                        {
                            gcBan.DataSource = db.mglbcBanChoThue_getByDate_Paging_Search
                                (tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc,
                                paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text,
                                txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text,
                                (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                )
                                .Select(p => new
                                {
                                    DiDong2 = Common.Right1(p.DiDong2, 3),
                                    DiDong3 = Common.Right1(p.DiDong3, 3),
                                    DiDong4 = Common.Right1(p.DiDong4, 3),
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MauNen,
                                    p.MaTT,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    DienThoai = Common.Right1(p.DienThoai, 3)
                                }).ToList();

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc,
                                    txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                    (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                    txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                    txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text,
                                    (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                    cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    ).ToList();
                                if (dtc.Count > 0)
                                {
                                    foreach (var item in dtc)
                                        TotalRecord = (int)item.sl;
                                }
                            }
                            catch (Exception ex)
                            { }
                            paging1.TotalRecords = TotalRecord;
                            paging1.RefreshPagination();


                        }
                        #endregion

                        #region else if (obj.Dienthoaian == false)
                        else if (obj.DienThoaiAn == false)
                        {
                            gcBan.DataSource = db.mglbcBanChoThue_getByDate_Paging_Search
                                (tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc,
                                paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text,
                                txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text,
                                (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                )
                                .Select(p => new
                                {
                                    DiDong2 = "",
                                    DiDong3 = "",
                                    DiDong4 = "",
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MauNen,
                                    p.MaTT,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    DienThoai = ""
                                }).ToList();

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc,
                                    txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                    (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                    txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                    txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text,
                                    (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                    cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    ).ToList();
                                if (dtc.Count > 0)
                                {
                                    foreach (var item in dtc)
                                        TotalRecord = (int)item.sl;
                                }
                            }
                            catch (Exception ex)
                            { }
                            paging1.TotalRecords = TotalRecord;
                            paging1.RefreshPagination();


                        }
                        #endregion
                        #region else
                        else
                        {
                            gcBan.DataSource = db.mglbcBanChoThue_getByDate_Paging_Search
                                (tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc,
                                paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text,
                                txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text,
                                (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                )
                                .Select(p => new
                                {

                                    p.DiDong2,
                                    p.DiDong3,
                                    p.DiDong4,
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    p.DienThoai
                                }).ToList();

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc,
                                    txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                    (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                    txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                    txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text,
                                    (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                    cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    ).ToList();
                                if (dtc.Count > 0)
                                {
                                    foreach (var item in dtc)
                                        TotalRecord = (int)item.sl;
                                }
                            }
                            catch (Exception ex)
                            { }
                            paging1.TotalRecords = TotalRecord;
                            paging1.RefreshPagination();



                        }
                        #endregion

                        #endregion
                        break;
                    case 4://Theo nhan vien
                        #region Theo nhan vien
                        db.CommandTimeout = 90000;
                        #region if (obj.DienThoai == false)
                        if (obj.DienThoai == false)
                        {
                            gcBan.DataSource = db.mglbcBanChoThue_getByDate_Paging_Search
                                (tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc,
                                paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text,
                                (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                )
                                .Select(p => new
                                {
                                    DiDong2 = Common.Right(p.DiDong2, 3),
                                    DiDong3 = Common.Right(p.DiDong3, 3),
                                    DiDong4 = Common.Right(p.DiDong4, 3),
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MauNen,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.MaTT,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    DienThoai = Common.Right(p.DienThoai, 3)
                                }).ToList();

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                    (tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false,
                                    matinh, lockhuvuc, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                    txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                    (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                    Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                    Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                    (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text,
                                    txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text,
                                    txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text,
                                    txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue,
                                    txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    ).ToList();
                                if (dtc.Count > 0)
                                {
                                    foreach (var item in dtc)
                                        TotalRecord = (int)item.sl;
                                }
                            }
                            catch (Exception ex)
                            { }
                            paging1.TotalRecords = TotalRecord;
                            paging1.RefreshPagination();



                        }
                        #endregion

                        #region  else if (obj.DienThoai3Dau == false)
                        else if (obj.DienThoai3Dau == false)
                        {
                            gcBan.DataSource = db.mglbcBanChoThue_getByDate_Paging_Search
                                (tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false,
                                matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(),
                                txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text,
                                txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text,
                                txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text,
                                txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue,
                                txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text
                                , cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                )
                                .Select(p => new
                                {
                                    DiDong2 = Common.Right1(p.DiDong2, 3),
                                    DiDong3 = Common.Right1(p.DiDong3, 3),
                                    DiDong4 = Common.Right1(p.DiDong4, 3),
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MauNen,
                                    p.MaTT,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    DienThoai = Common.Right1(p.DienThoai, 3)
                                }).ToList();

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                    (tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, matinh,
                                    lockhuvuc, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                    (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                    (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                    (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                    txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                    txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text
                                    , cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    ).ToList();
                                if (dtc.Count > 0)
                                {
                                    foreach (var item in dtc)
                                        TotalRecord = (int)item.sl;
                                }
                            }
                            catch (Exception ex)
                            { }
                            paging1.TotalRecords = TotalRecord;
                            paging1.RefreshPagination();



                        }
                        #endregion
                        #region  else if (obj.dienthoaian == false)
                        else if (obj.DienThoaiAn == false)
                        {
                            gcBan.DataSource = db.mglbcBanChoThue_getByDate_Paging_Search
                                (tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false,
                                matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(),
                                txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text,
                                txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text,
                                txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text,
                                txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue,
                                txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text
                                , cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                )
                                .Select(p => new
                                {
                                    DiDong2 = "",
                                    DiDong3 = "",
                                    DiDong4 = "",
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MauNen,
                                    p.MaTT,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    DienThoai = ""
                                }).ToList();

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                    (tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, matinh,
                                    lockhuvuc, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                    (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                    (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                    (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                    txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                    txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text
                                    , cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    ).ToList();
                                if (dtc.Count > 0)
                                {
                                    foreach (var item in dtc)
                                        TotalRecord = (int)item.sl;
                                }
                            }
                            catch (Exception ex)
                            { }
                            paging1.TotalRecords = TotalRecord;
                            paging1.RefreshPagination();



                        }
                        #endregion

                        #region else
                        else
                        {
                            gcBan.DataSource = db.mglbcBanChoThue_getByDate_Paging_Search
                                (tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc,
                                paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text
                                , cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                )
                                .Select(p => new
                                {

                                    p.DiDong2,
                                    p.DiDong3,
                                    p.DiDong4,
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    p.DienThoai
                                }).ToList();

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                    (tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc,
                                    txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                    (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                    (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                    (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue, txtNguon.Text,
                                    txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                    txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text,
                                    (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                    cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    ).ToList();
                                if (dtc.Count > 0)
                                {
                                    foreach (var item in dtc)
                                        TotalRecord = (int)item.sl;
                                }
                            }
                            catch (Exception ex)
                            { }
                            paging1.TotalRecords = TotalRecord;
                            paging1.RefreshPagination();



                        }
                        #endregion

                        #endregion
                        break;
                    default:

                        break;
                }

                //for (int i = 0; i < grvBan.RowCount; i++)
                //{
                //    string phiMG = grvBan.GetRowCellValue(i, "TyLeMG") != null ?
                //        string.Format("{0:#,0.##}", grvBan.GetRowCellValue(i, "TyLeMG")) :
                //        string.Format("{0:#,0.##}", grvBan.GetRowCellValue(i, "PhiMG"));
                //    grvBan.SetRowCellValue(i, "PhiMoiGioi", phiMG);
                //}
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }
        #endregion
        private void Ban_Edit()
        {
            frmEdit frm = new frmEdit();
            frm.MaBC = (int)grvBan.GetFocusedRowCellValue("MaBC");
            frm.ShowDialog();
            if (frm.IsSave)
            {
                Ban_Load();
            }
        }

        private void Ban_Delete()
        {
            var indexs = grvBan.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu đăng ký");
                return;
            }

            if (DialogBox.Question() == DialogResult.No) return;
            foreach (var i in indexs)
            {
                bool ck = (GetAccessData() == 1 || (int)grvBan.GetRowCellValue(i, "MaNVQL") == Common.StaffID) || ((int)grvBan.GetRowCellValue(i, "MaNVKD") == Common.StaffID);
                if (!ck)
                    return;
                mglbcBanChoThue objBC = db.mglbcBanChoThues.Single(p => p.MaBC == (int)grvBan.GetRowCellValue(i, "MaBC"));
                objBC.MaTTD = 1;
            }
            db.SubmitChanges();
            DialogBox.Infomation("Sản phẩm cho thuê đã chuyển sang mục chờ duyệt xóa");
            Ban_Load();
        }

        private void LocSanPham(mglbcBanChoThue objGD)
        {
            try
            {
                var objpq = db.mglmtPhanQuyens.Single(p => p.MaNV == Common.StaffID);
                if (objpq.TenCT == false)
                    colKhachHangMT.Visible = false;
                if (objpq.GhiChu == false)
                    colGhiChuMT.Visible = false;
                if (objpq.CanBoLV == false)
                    colNhanVienQLMT.Visible = false;
            }
            catch { }

            var arrlistSP = ",";
            var objNC = db.mglBCSanPhams.Where(p => p.MaSP == objGD.MaBC).Select(p => p.mglBCCongViec.MaCoHoiMT);
            arrlistSP += ",";
            //var obj = db.mglmtPhanQuyens.Single(p => p.MaNV == Common.StaffID);
            //if (obj.DienThoai == false)
            //{
            #region điện thoại 3 số cuối
            //var objLoc = (from mt in db.mglmtMuaThues
            //              join nvkd in db.NhanViens on mt.MaNVKD equals nvkd.MaNV
            //              join da in db.DuAns on mt.MaDA equals da.MaDA into du
            //              from da in du.DefaultIfEmpty()
            //              where (mt.IsMua == objGD.IsBan & mt.MaTT < 3)
            //              orderby mt.NgayDK descending
            //              select new
            //              {
            //                  #region mt
            //                  mt.MaTT,
            //                  mt.mglmtTrangThai.MauNen,
            //                  mt.mglmtTrangThai.TenTT,
            //                  mt.MaMT,
            //                  mt.SoDK,
            //                  ThuongHieu = mt.KhachHang.IsPersonal == true ? mt.KhachHang.MoHinh : mt.KhachHang.ThuongHieu,
            //                  mt.NgayCN,
            //                  //SoGiay= SqlMethods.DateDiffSecond(DateTime.Now,  SqlMethods. (mt.ThoiHan, mt.NgayDK),
            //                  //mt.SoGiay,
            //                  mt.MaNguon,
            //                  mt.MaCD,
            //                  mt.MaNVKT,
            //                  mt.MaNVKD,
            //                  mt.MaKH,
            //                  HoTenNVKD = nvkd.HoTen,
            //                  mt.IsMua,
            //                  TenNC = mt.IsMua == true ? "Cần mua" : "Cần thuê",
            //                  da.TenDA,
            //                  mt.GhiChu,
            //                  mt.KhuVuc,
            //                  mt.PhiMG,
            //                  mt.mglmtMuDichMT.MucDich,
            //                  mt.HuongCua,
            //                  mt.HuongBC,
            //                  mt.TienIch,
            //                  mt.LoaiTien.TenLoaiTien,
            //                  mt.BoiDam,
            //                  mt.NgayNhap,
            //                  PhongNgu = mt.PhNguTu,
            //                  mt.Duong,
            //                  //mt.SoTang,
            //                  PVS = mt.PVSTu,
            //                  //DienThoai = Common.Right(mt.KhachHang.DienThoaiCT, 3),
            //                  #endregion
            //                  mt.IsCanGoc,
            //                  mt.IsThangMay,
            //                  mt.IsTangHam,
            //                  mt.NgayDK,
            //                  mt.ThoiHan,
            //                  mt.TyLeHH,
            //                  mt.mglmtHuongs,
            //                  mt.mglmtHuongBCs,
            //                  mt.mglmtLoaiDuongs,
            //                  mt.mglmtPhapLies,
            //                  mt.mglmtHuyens,
            //                  //mt.MaDuong,
            //                  HoTenKH = (mt.MaNVKD == Common.StaffID || mt.MaNVKT == Common.StaffID || GetAccessData() == 1) == true ? mt.KhachHang.HoKH + " " + mt.KhachHang.TenKH : "",
            //                  DienThoai = Common.Right((mt.MaNVKD == Common.StaffID || mt.MaNVKT == Common.StaffID || GetAccessData() == 1) ? (mt.KhachHang.DiDong == null ? mt.KhachHang.DienThoaiCT : mt.KhachHang.DiDong) : "", 3),
            //                  //  KhoangGia = "<= " + string.Format("{0:#,0.##)", p.GiaDen),
            //                  //DienTich = ">= " + string.Format("{0:#,0.##}", p.DienTichTu),
            //                  //PhongNgu = ">= " + string.Format("{0}", p.mthNguTu),
            //                  //PVS = ">= " + string.Format("{0}", mt.PVSTu),
            //                  //KhoangGia=mt.GiaDen,
            //                  //DienTich=mt.DienTichTu,
            //                  //PhongNgu=mt.PhNguTu,
            //                  //PVS= mt.PVSTu,
            //                  //   HoTenNV = (mt.MaNVKD == Library.Common.StaffID || mt.MaNVKT == Library.Common.StaffID || GetAccessData() == 1) ? mt.NhanVien.HoTen : "",
            //                  HoTenNV = mt.NhanVien.HoTen,
            //                  mt.IsChinhChu,
            //                  mt.MaDA,
            //                  mt.DuongRongTu,
            //                  mt.DienTichTu,
            //                  GiaDen = mt.GiaDen * mt.LoaiTien.TyGia,
            //                  mt.IsDeOto,
            //                  mt.IsSanThuong,
            //                  mt.mglmtDuongs,
            //                  mt.IsThuongLuong,
            //                  mt.LauDen,
            //                  mt.LauTu,
            //                  mt.MaLBDS,
            //                  mt.PhNguTu,
            //                  mt.PVSTu,
            //                  mt.MatTienTu,
            //                  mt.MaTinh,
            //                  //mt.Duong.TenDuong,
            //                  mt.Tinh.TenTinh
            //              }).ToList().Where(mt => objNC.Contains(mt.MaMT) == false);
            //var objPQ = db.mglCaiDatTimKiems.FirstOrDefault(p => p.MaNV == Common.StaffID);
            //if (objPQ == null)
            //{
            //    DialogBox.Infomation("Bạn chưa cài đặt tiêu chí lọc sản phẩm");
            //    return;
            //}
            //if (objPQ == null)
            //{
            //    DialogBox.Infomation("Vui lòng cài đặt quy định lọc thông tin cho tài khoản này!");
            //    return;
            //}
            //if (objPQ.CanGoc.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.IsCanGoc == objGD.IsCanGoc).ToList();
            //if (objPQ.DienTich.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.DienTichTu <= objGD.DienTich).ToList();
            //if (objPQ.DuAn.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.MaDA == objGD.MaDA).ToList();
            //if (objPQ.DuongRong.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.DuongRongTu <= objGD.DuongRong).ToList();
            //if (objPQ.HuongCua.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.mglmtHuongs.Count == 0 || p.mglmtHuongs.Select(h => h.MaHuong).Contains(objGD.MaHuong)).ToList();
            //if (objPQ.HuongBanCong.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.mglmtHuongBCs.Count == 0 || p.mglmtHuongBCs.Select(h => h.MaHuong).Contains(objGD.HuongBanCong)).ToList();
            //try
            //{
            //    if (objPQ.KhoangGia.GetValueOrDefault())
            //        objLoc = objLoc.Where(p => p.GiaDen >= (objGD.ThanhTien * objGD.LoaiTien.TyGia)).ToList();
            //}
            //catch { }
            //if (objPQ.LoaiBDS.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.MaLBDS == objGD.MaLBDS).ToList();
            //if (objPQ.LoaiDuong.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.mglmtLoaiDuongs.Count == 0 || p.mglmtLoaiDuongs.Select(ld => ld.MaLD).Contains(objGD.MaLD)).ToList();
            //if (objPQ.OtoVao.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.IsDeOto == objGD.DauOto).ToList();
            //if (objPQ.PhapLy.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.mglmtPhapLies.Count == 0 || p.mglmtPhapLies.Select(pl => pl.MaPL).Contains(objGD.MaPL)).ToList();
            //if (objPQ.PhongNgu.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.PhNguTu <= objGD.PhongNgu).ToList();
            //if (objPQ.QuanHuyen.GetValueOrDefault())
            //    objLoc = objLoc = objLoc.Where(p => p.mglmtHuyens.Count == 0 || p.mglmtHuyens.Select(h => h.MaHuyen).Contains(objGD.MaHuyen)).ToList();
            //if (objPQ.SoTang.GetValueOrDefault())
            //    objLoc.Where(p => p.LauTu <= objGD.SoTang & (p.LauDen >= objGD.SoTang | p.LauDen == 0));
            //if (objPQ.ThangMay.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.IsThangMay == objGD.IsThangMay).ToList();
            //if (objPQ.TangHam.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.IsTangHam == objGD.TangHam).ToList();

            //if (objPQ.MatTien.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.MatTienTu == objGD.NgangXD).ToList();
            //if (objPQ.TenDuong.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.mglmtDuongs.Count == 0 || p.mglmtDuongs.Select(d => d.MaDuong).Contains(objGD.MaDuong)).ToList();
            //if (objPQ.Tinh.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.MaTinh == objGD.MaTinh).ToList();
            //gcMuaThue.DataSource = objLoc;
            #endregion
            //}
            //if (obj.DienThoai3Dau == false)
            //{
            #region điện thoại 3 số cuối và 3 đầu

            var objPQ = db.mglCaiDatTimKiems.FirstOrDefault(p => p.MaNV == Common.StaffID);
            if (objPQ == null)
            {
                DialogBox.Infomation("Bạn chưa cài đặt tiêu chí lọc sản phẩm");
                return;
            }
            if (objPQ == null)
            {
                DialogBox.Infomation("Vui lòng cài đặt quy định lọc thông tin cho tài khoản này!");
                return;
            }

            #region Code Linq
            //var objLoc = (from mt in db.mglmtMuaThues
            //                  join nvkd in db.NhanViens on mt.MaNVKD equals nvkd.MaNV
            //                  join da in db.DuAns on mt.MaDA equals da.MaDA into du
            //                  from da in du.DefaultIfEmpty()
            //                  where (mt.IsMua == objGD.IsBan & mt.MaTT < 3)
            //                  orderby mt.NgayDK descending
            //                  select new
            //                  {
            //                      #region mt
            //                      mt.MaTT,
            //                      mt.mglmtTrangThai.MauNen,
            //                      mt.mglmtTrangThai.TenTT,
            //                      mt.MaMT,
            //                      mt.SoDK,
            //                      ThuongHieu = mt.KhachHang.IsPersonal == true ? mt.KhachHang.MoHinh : mt.KhachHang.ThuongHieu,
            //                      mt.NgayCN,
            //                      //SoGiay= SqlMethods.DateDiffSecond(DateTime.Now,  SqlMethods. (mt.ThoiHan, mt.NgayDK),
            //                      //mt.SoGiay,
            //                      mt.MaNguon,
            //                      mt.MaCD,
            //                      mt.MaNVKT,
            //                      mt.MaNVKD,
            //                      mt.MaKH,
            //                      HoTenNVKD = nvkd.HoTen,
            //                      mt.IsMua,
            //                      TenNC = mt.IsMua == true ? "Cần mua" : "Cần thuê",
            //                      da.TenDA,
            //                      mt.GhiChu,
            //                      mt.KhuVuc,
            //                      mt.PhiMG,
            //                      mt.mglmtMuDichMT.MucDich,
            //                      mt.HuongCua,
            //                      mt.HuongBC,
            //                      mt.TienIch,
            //                      mt.LoaiTien.TenLoaiTien,
            //                      mt.BoiDam,
            //                      mt.NgayNhap,
            //                      PhongNgu = mt.PhNguTu,
            //                      mt.Duong,
            //                      //mt.SoTang,
            //                      PVS = mt.PVSTu,
            //                      //DienThoai = Common.Right(mt.KhachHang.DienThoaiCT, 3),
            //                      #endregion
            //                      mt.IsCanGoc,
            //                      mt.IsThangMay,
            //                      mt.IsTangHam,
            //                      mt.NgayDK,
            //                      mt.ThoiHan,
            //                      mt.TyLeHH,
            //                      mt.mglmtHuongs,
            //                      mt.mglmtHuongBCs,
            //                      mt.mglmtLoaiDuongs,
            //                      mt.mglmtPhapLies,
            //                      mt.mglmtHuyens,
            //                      //mt.MaDuong,
            //                      HoTenKH = (mt.MaNVKD == Common.StaffID || mt.MaNVKT == Common.StaffID || GetAccessData() == 1) == true ? mt.KhachHang.HoKH + " " + mt.KhachHang.TenKH : "",
            //                      DienThoai = Common.Right1((mt.MaNVKD == Common.StaffID || mt.MaNVKT == Common.StaffID || GetAccessData() == 1) ? (mt.KhachHang.DiDong == null ? mt.KhachHang.DienThoaiCT : mt.KhachHang.DiDong) : "", 3),
            //                      //  KhoangGia = "<= " + string.Format("{0:#,0.##)", p.GiaDen),
            //                      //DienTich = ">= " + string.Format("{0:#,0.##}", p.DienTichTu),
            //                      //PhongNgu = ">= " + string.Format("{0}", p.mthNguTu),
            //                      //PVS = ">= " + string.Format("{0}", mt.PVSTu),
            //                      //KhoangGia=mt.GiaDen,
            //                      //DienTich=mt.DienTichTu,
            //                      //PhongNgu=mt.PhNguTu,
            //                      //PVS= mt.PVSTu,
            //                      //   HoTenNV = (mt.MaNVKD == Library.Common.StaffID || mt.MaNVKT == Library.Common.StaffID || GetAccessData() == 1) ? mt.NhanVien.HoTen : "",
            //                      HoTenNV = mt.NhanVien.HoTen,
            //                      mt.IsChinhChu,
            //                      mt.MaDA,
            //                      mt.DuongRongTu,
            //                      mt.DienTichTu,
            //                      GiaDen = mt.GiaDen * mt.LoaiTien.TyGia,
            //                      mt.IsDeOto,
            //                      mt.IsSanThuong,
            //                      mt.mglmtDuongs,
            //                      mt.IsThuongLuong,
            //                      mt.LauDen,
            //                      mt.LauTu,
            //                      mt.MaLBDS,
            //                      mt.PhNguTu,
            //                      mt.PVSTu,
            //                      mt.MatTienTu,
            //                      mt.MaTinh,
            //                      //mt.Duong.TenDuong,
            //                      mt.Tinh.TenTinh
            //                  }).ToList().Where(mt => objNC.Contains(mt.MaMT) == false);
            #endregion

            var objLoc = db.mglmtMuaThue_KhopNhuCau_Paging
                        (
                            objGD.IsBan, 1, 10, arrlistSP,
                            objPQ.CanGoc,
                            objGD.IsCanGoc,
                            objPQ.DienTich,
                            objGD.DienTich,
                            objPQ.DuAn,
                            objGD.MaDA,
                            objPQ.DuongRong,
                            objGD.DuongRong,
                            objPQ.HuongCua,
                            objGD.MaHuong,
                            objPQ.HuongBanCong,
                            objGD.HuongBanCong,
                            objPQ.KhoangGia,
                            (objGD.ThanhTien * objGD.LoaiTien.TyGia),
                            objPQ.LoaiBDS,
                            objGD.MaLBDS,
                            objPQ.LoaiDuong,
                            objGD.MaLD,
                            objPQ.OtoVao,
                            objGD.DauOto,
                            objPQ.PhapLy,
                            objGD.MaPL,
                            objPQ.PhongNgu,
                            objGD.PhongNgu,
                            objPQ.QuanHuyen,
                            objGD.MaHuyen,
                            objPQ.SoTang,
                            objGD.SoTang,
                            objPQ.ThangMay,
                            objGD.IsThangMay,
                            objPQ.TangHam,
                            objGD.TangHam,
                            objPQ.TienIch,
                            objPQ.MatTien,
                            objGD.NgangXD,
                            objPQ.TenDuong,
                            objGD.MaDuong,
                            objPQ.Tinh,
                            objGD.MaTinh,
                            objPQ.PVS,
                            objGD.PhongVS
                        )
                        .Select(mt => new
                        {
                            #region mt
                            mt.MaTT,
                            mt.MauNen,
                            mt.TenTT,
                            mt.MaMT,
                            mt.SoDK,
                            mt.ThuongHieu,
                            mt.NgayCN,
                            //SoGiay= SqlMethods.DateDiffSecond(DateTime.Now,  SqlMethods. (mt.ThoiHan, mt.NgayDK),
                            //mt.SoGiay,
                            mt.MaNguon,
                            mt.MaCD,
                            mt.MaNVKT,
                            mt.MaNVKD,
                            mt.MaKH,
                            mt.HoTenNVKD,
                            mt.IsMua,
                            mt.TenNC,
                            mt.TenDA,
                            mt.GhiChu,
                            mt.KhuVuc,
                            mt.PhiMG,
                            mt.MucDich,
                            mt.HuongCua,
                            mt.HuongBC,
                            mt.TienIch,
                            mt.TenLoaiTien,
                            mt.BoiDam,
                            mt.NgayNhap,
                            PhongNgu = mt.PhNguTu,
                            mt.Duong,
                            //mt.SoTang,
                            PVS = mt.PVSTu,
                            //DienThoai = Common.Right(mt.KhachHang.DienThoaiCT, 3),
                            #endregion
                            #region Khop
                            mt.IsCanGoc,
                            mt.IsThangMay,
                            mt.IsTangHam,
                            mt.NgayDK,
                            mt.ThoiHan,
                            mt.TyLeHH,
                            //mt.MaDuong,
                            HoTenKH = (mt.MaNVKD == Common.StaffID || mt.MaNVKT == Common.StaffID || GetAccessData() == 1) == true ? mt.HoKH + " " + mt.TenKH : "",
                            DienThoai = Common.Right1((mt.MaNVKD == Common.StaffID || mt.MaNVKT == Common.StaffID || GetAccessData() == 1) ? (mt.DiDong == null ? mt.DienThoaiCT : mt.DiDong) : "", 3),
                            //  KhoangGia = "<= " + string.Format("{0:#,0.##)", p.GiaDen),
                            //DienTich = ">= " + string.Format("{0:#,0.##}", p.DienTichTu),
                            //PhongNgu = ">= " + string.Format("{0}", p.mthNguTu),
                            //PVS = ">= " + string.Format("{0}", mt.PVSTu),
                            //KhoangGia=mt.GiaDen,
                            //DienTich=mt.DienTichTu,
                            //PhongNgu=mt.PhNguTu,
                            //PVS= mt.PVSTu,
                            //   HoTenNV = (mt.MaNVKD == Library.Common.StaffID || mt.MaNVKT == Library.Common.StaffID || GetAccessData() == 1) ? mt.NhanVien.HoTen : "",
                            mt.HoTenNV,
                            mt.IsChinhChu,
                            mt.MaDA,
                            mt.DuongRongTu,
                            mt.DienTichTu,
                            mt.GiaDen,
                            mt.IsDeOto,
                            mt.IsSanThuong,
                            mt.IsThuongLuong,
                            mt.LauDen,
                            mt.LauTu,
                            mt.MaLBDS,
                            mt.PhNguTu,
                            mt.PVSTu,
                            mt.MatTienTu,
                            mt.MaTinh,
                            //mt.Duong.TenDuong,
                            mt.TenTinh
                            #endregion
                        }).ToList();


            //foreach (var i in objLoc)
            //{
            //    objLoc.Remove(i);
            //}

            #region Linq
            //if (objPQ.CanGoc.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.IsCanGoc == objGD.IsCanGoc).ToList();
            //if (objPQ.DienTich.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.DienTichTu <= objGD.DienTich).ToList();
            //if (objPQ.DuAn.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.MaDA == objGD.MaDA).ToList();
            //if (objPQ.DuongRong.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.DuongRongTu <= objGD.DuongRong).ToList();
            //if (objPQ.HuongCua.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.mglmtHuongs.Count == 0 || p.mglmtHuongs.Select(h => h.MaHuong).Contains(objGD.MaHuong)).ToList();
            //if (objPQ.HuongBanCong.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.mglmtHuongBCs.Count == 0 || p.mglmtHuongBCs.Select(h => h.MaHuong).Contains(objGD.HuongBanCong)).ToList();
            //try
            //{
            //    if (objPQ.KhoangGia.GetValueOrDefault())
            //        objLoc = objLoc.Where(p => p.GiaDen >= (objGD.ThanhTien * objGD.LoaiTien.TyGia)).ToList();
            //}
            //catch { }
            //if (objPQ.LoaiBDS.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.MaLBDS == objGD.MaLBDS).ToList();
            //if (objPQ.LoaiDuong.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.mglmtLoaiDuongs.Count == 0 || p.mglmtLoaiDuongs.Select(ld => ld.MaLD).Contains(objGD.MaLD)).ToList();
            //if (objPQ.OtoVao.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.IsDeOto == objGD.DauOto).ToList();
            //if (objPQ.PhapLy.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.mglmtPhapLies.Count == 0 || p.mglmtPhapLies.Select(pl => pl.MaPL).Contains(objGD.MaPL)).ToList();
            //if (objPQ.PhongNgu.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.PhNguTu <= objGD.PhongNgu).ToList();
            //if (objPQ.QuanHuyen.GetValueOrDefault())
            //    objLoc = objLoc = objLoc.Where(p => p.mglmtHuyens.Count == 0 || p.mglmtHuyens.Select(h => h.MaHuyen).Contains(objGD.MaHuyen)).ToList();
            //if (objPQ.SoTang.GetValueOrDefault())
            //    objLoc.Where(p => p.LauTu <= objGD.SoTang & (p.LauDen >= objGD.SoTang | p.LauDen == 0));
            //if (objPQ.ThangMay.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.IsThangMay == objGD.IsThangMay).ToList();
            //if (objPQ.TangHam.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.IsTangHam == objGD.TangHam).ToList();

            //if (objPQ.MatTien.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.MatTienTu == objGD.NgangXD).ToList();
            //if (objPQ.TenDuong.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.mglmtDuongs.Count == 0 || p.mglmtDuongs.Select(d => d.MaDuong).Contains(objGD.MaDuong)).ToList();
            //if (objPQ.Tinh.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.MaTinh == objGD.MaTinh).ToList();
            #endregion

            gcMuaThue.DataSource = objLoc;

            #endregion
            //}
            //else
            //{
            #region còn lại
            //var objLoc = (from mt in db.mglmtMuaThues
            //              join nvkd in db.NhanViens on mt.MaNVKD equals nvkd.MaNV
            //              join da in db.DuAns on mt.MaDA equals da.MaDA into du
            //              from da in du.DefaultIfEmpty()
            //              where (mt.IsMua == objGD.IsBan & mt.MaTT < 3)
            //              orderby mt.NgayDK descending
            //              select new
            //              {
            //                  #region mt
            //                  mt.MaTT,
            //                  mt.mglmtTrangThai.MauNen,
            //                  mt.mglmtTrangThai.TenTT,
            //                  mt.MaMT,
            //                  mt.SoDK,
            //                  ThuongHieu = mt.KhachHang.IsPersonal == true ? mt.KhachHang.MoHinh : mt.KhachHang.ThuongHieu,
            //                  mt.NgayCN,
            //                  //SoGiay= SqlMethods.DateDiffSecond(DateTime.Now,  SqlMethods. (mt.ThoiHan, mt.NgayDK),
            //                  //mt.SoGiay,
            //                  mt.MaNguon,
            //                  mt.MaCD,
            //                  mt.MaNVKT,
            //                  mt.MaNVKD,
            //                  mt.MaKH,
            //                  HoTenNVKD = nvkd.HoTen,
            //                  mt.IsMua,
            //                  TenNC = mt.IsMua == true ? "Cần mua" : "Cần thuê",
            //                  da.TenDA,
            //                  mt.GhiChu,
            //                  mt.KhuVuc,
            //                  mt.PhiMG,
            //                  mt.mglmtMuDichMT.MucDich,
            //                  mt.HuongCua,
            //                  mt.HuongBC,
            //                  mt.TienIch,
            //                  mt.LoaiTien.TenLoaiTien,
            //                  mt.BoiDam,
            //                  mt.NgayNhap,
            //                  PhongNgu = mt.PhNguTu,
            //                  mt.Duong,
            //                  //mt.SoTang,
            //                  PVS = mt.PVSTu,
            //                  //DienThoai = Common.Right(mt.KhachHang.DienThoaiCT, 3),
            //                  #endregion
            //                  mt.IsCanGoc,
            //                  mt.IsThangMay,
            //                  mt.IsTangHam,
            //                  mt.NgayDK,
            //                  mt.ThoiHan,
            //                  mt.TyLeHH,
            //                  mt.mglmtHuongs,
            //                  mt.mglmtHuongBCs,
            //                  mt.mglmtLoaiDuongs,
            //                  mt.mglmtPhapLies,
            //                  mt.mglmtHuyens,
            //                  //mt.MaDuong,
            //                  HoTenKH = (mt.MaNVKD == Common.StaffID || mt.MaNVKT == Common.StaffID || GetAccessData() == 1) == true ? mt.KhachHang.HoKH + " " + mt.KhachHang.TenKH : "",
            //                  DienThoai = (mt.MaNVKD == Common.StaffID || mt.MaNVKT == Common.StaffID || GetAccessData() == 1) ? (mt.KhachHang.DiDong == null ? mt.KhachHang.DienThoaiCT : mt.KhachHang.DiDong) : "",
            //                  //DienTich = ">= " + string.Format("{0:#,0.##}", p.DienTichTu),
            //                  //PhongNgu = ">= " + string.Format("{0}", p.mthNguTu),
            //                  //PVS = ">= " + string.Format("{0}", mt.PVSTu),
            //                  //KhoangGia=mt.GiaDen,
            //                  //DienTich=mt.DienTichTu,
            //                  //PhongNgu=mt.PhNguTu,
            //                  //PVS= mt.PVSTu,
            //                  //   HoTenNV = (mt.MaNVKD == Library.Common.StaffID || mt.MaNVKT == Library.Common.StaffID || GetAccessData() == 1) ? mt.NhanVien.HoTen : "",
            //                  HoTenNV = mt.NhanVien.HoTen,
            //                  mt.IsChinhChu,
            //                  mt.MaDA,
            //                  mt.DuongRongTu,
            //                  mt.DienTichTu,
            //                  GiaDen = mt.GiaDen * mt.LoaiTien.TyGia,
            //                  mt.IsDeOto,
            //                  mt.IsSanThuong,
            //                  mt.mglmtDuongs,
            //                  mt.IsThuongLuong,
            //                  mt.LauDen,
            //                  mt.LauTu,
            //                  mt.MaLBDS,
            //                  mt.PhNguTu,
            //                  mt.PVSTu,
            //                  mt.MatTienTu,
            //                  mt.MaTinh,
            //                  //mt.Duong.TenDuong,
            //                  mt.Tinh.TenTinh
            //              }).ToList().Where(mt => objNC.Contains(mt.MaMT) == false);

            //var objPQ = db.mglCaiDatTimKiems.FirstOrDefault(p => p.MaNV == Common.StaffID);
            //if (objPQ == null)
            //{
            //    DialogBox.Infomation("Bạn chưa cài đặt tiêu chí lọc sản phẩm");
            //    return;
            //}
            //if (objPQ == null)
            //{
            //    DialogBox.Infomation("Vui lòng cài đặt quy định lọc thông tin cho tài khoản này!");
            //    return;
            //}
            //if (objPQ.CanGoc.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.IsCanGoc == objGD.IsCanGoc).ToList();
            //if (objPQ.DienTich.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.DienTichTu <= objGD.DienTich).ToList();
            //if (objPQ.DuAn.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.MaDA == objGD.MaDA).ToList();
            //if (objPQ.DuongRong.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.DuongRongTu <= objGD.DuongRong).ToList();
            //if (objPQ.HuongCua.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.mglmtHuongs.Count == 0 || p.mglmtHuongs.Select(h => h.MaHuong).Contains(objGD.MaHuong)).ToList();
            //if (objPQ.HuongBanCong.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.mglmtHuongBCs.Count == 0 || p.mglmtHuongBCs.Select(h => h.MaHuong).Contains(objGD.HuongBanCong)).ToList();
            //try
            //{
            //    if (objPQ.KhoangGia.GetValueOrDefault())
            //        objLoc = objLoc.Where(p => p.GiaDen >= (objGD.ThanhTien * objGD.LoaiTien.TyGia)).ToList();
            //}
            //catch { }
            //if (objPQ.LoaiBDS.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.MaLBDS == objGD.MaLBDS).ToList();
            //if (objPQ.LoaiDuong.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.mglmtLoaiDuongs.Count == 0 || p.mglmtLoaiDuongs.Select(ld => ld.MaLD).Contains(objGD.MaLD)).ToList();
            //if (objPQ.OtoVao.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.IsDeOto == objGD.DauOto).ToList();
            //if (objPQ.PhapLy.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.mglmtPhapLies.Count == 0 || p.mglmtPhapLies.Select(pl => pl.MaPL).Contains(objGD.MaPL)).ToList();
            //if (objPQ.PhongNgu.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.PhNguTu <= objGD.PhongNgu).ToList();
            //if (objPQ.QuanHuyen.GetValueOrDefault())
            //    objLoc = objLoc = objLoc.Where(p => p.mglmtHuyens.Count == 0 || p.mglmtHuyens.Select(h => h.MaHuyen).Contains(objGD.MaHuyen)).ToList();
            //if (objPQ.SoTang.GetValueOrDefault())
            //    objLoc.Where(p => p.LauTu <= objGD.SoTang & (p.LauDen >= objGD.SoTang | p.LauDen == 0));
            //if (objPQ.ThangMay.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.IsThangMay == objGD.IsThangMay).ToList();
            //if (objPQ.TangHam.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.IsTangHam == objGD.TangHam).ToList();

            //if (objPQ.MatTien.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.MatTienTu == objGD.NgangXD).ToList();
            //if (objPQ.TenDuong.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.mglmtDuongs.Count == 0 || p.mglmtDuongs.Select(d => d.MaDuong).Contains(objGD.MaDuong)).ToList();
            //if (objPQ.Tinh.GetValueOrDefault())
            //    objLoc = objLoc.Where(p => p.MaTinh == objGD.MaTinh).ToList();
            //gcMuaThue.DataSource = objLoc;
            #endregion
            //}
        }

        private void TabPage_Load()
        {
            int? maBC = (int?)grvBan.GetFocusedRowCellValue("MaBC");

            if (maBC == null)
            {
                gcMuaThue.DataSource = null;
                gcNhatKy.DataSource = null;
                gcKhachMua.DataSource = null;
                gcKhachXem.DataSource = null;
                return;
            }

            switch (tabMain.SelectedTabPageIndex)
            {
                case 0:
                    #region Lich lam viec
                    var listNhatKy = (from p in db.mglbcNhatKyXuLies
                                      join nv in db.NhanViens on p.MaNVG equals nv.MaNV into nhanvien
                                      from nv in nhanvien.DefaultIfEmpty()
                                      join nv1 in db.NhanViens on p.MaNVN equals nv1.MaNV into nhanvien1
                                      from nv1 in nhanvien1.DefaultIfEmpty()
                                      join pt in db.PhuongThucXuLies on p.MaPT equals pt.MaPT into phuongthuc
                                      from pt in phuongthuc.DefaultIfEmpty()
                                      where p.MaBC == maBC
                                      orderby p.NgayXL descending

                                      select new
                                      {
                                          p.ID,
                                          p.NgayXL,
                                          p.TieuDe,
                                          p.MaTT,
                                          p.NoiDung,
                                          //p.PhuongThucXuLy.TenPT,
                                          p.MaNVG,
                                          HoTenNVG = nv.HoTen,
                                          HoTenNVN = nv1.HoTen,
                                          p.KetQua,
                                          pt.TenPT
                                      }).ToList();
                    gcNhatKy.DataSource = listNhatKy;
                    #endregion
                    break;
                case 1:
                    #region Nhu cau
                    if ((byte?)grvBan.GetFocusedRowCellValue("MaTT") != 0)
                    {
                        gcMuaThue.DataSource = null;
                        return;
                    }
                    var objGD = db.mglbcBanChoThues.Single(p => p.MaBC == maBC);
                    LocSanPham(objGD);
                    #endregion

                    break;
                case 2:
                    ctlTaiLieu1.FormID = 174;
                    ctlTaiLieu1.LinkID = (int?)grvBan.GetFocusedRowCellValue("MaBC");
                    ctlTaiLieu1.MaNV = Common.StaffID;
                    ctlTaiLieu1.TaiLieu_Load();
                    break;
                case 3:
                    if (Convert.ToInt32(grvBan.GetFocusedRowCellValue("MaTT")) == 3 | Convert.ToInt32(grvBan.GetFocusedRowCellValue("MaTT")) == 0)
                    {
                        gcKhachMua.DataSource = null;
                        return;
                    }
                    gcKhachMua.DataSource = db.mglgdGiaoDiches.Where(p => p.MaBC == maBC).Select(p => new
                    {
                        KhachHang = p.mglmtMuaThue.KhachHang.IsPersonal == true ? p.mglmtMuaThue.KhachHang.HoKH + " " + p.mglmtMuaThue.KhachHang.TenKH : p.mglmtMuaThue.KhachHang.TenCongTy,
                        p.mglmtMuaThue.KhachHang.DiDong,
                        p.mglmtMuaThue.KhachHang.DCLL,
                        p.mglmtMuaThue.SoDK,
                        p.mglmtMuaThue.NgayDK,
                        p.NhanVien.HoTen
                    });
                    break;
                case 4:
                    gcKhachXem.DataSource = db.mglBCSanPhams.Where(p => p.MaSP == maBC).Select(p => new
                    {
                        // p.mglBCCongViec.mglmtMuaThue.
                        KhachHang = p.mglBCCongViec.mglmtMuaThue.KhachHang.IsPersonal == true ? p.mglBCCongViec.mglmtMuaThue.KhachHang.HoKH + " " + p.mglBCCongViec.mglmtMuaThue.KhachHang.TenKH : p.mglBCCongViec.mglmtMuaThue.KhachHang.TenCongTy,
                        p.mglBCCongViec.mglmtMuaThue.KhachHang.DiDong,
                        p.mglBCCongViec.mglmtMuaThue.KhachHang.DCLL,
                        p.mglBCCongViec.mglmtMuaThue.SoDK,
                        p.mglBCCongViec.mglmtMuaThue.NgayDK,
                        p.mglBCCongViec.NhanVien.HoTen
                    });
                    break;
                case 5:
                    var objBC = db.mglbcBanChoThues.Single(p => p.MaBC == maBC);
                    txtGioiThieu.InnerHtml = objBC.GioiThieu;
                    break;
                case 6:
                    var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);

                    if (obj.DienThoai == false)
                    {
                        gridControlAvatar.DataSource = db.NguoiDaiDien_getByMaKH((int?)grvBan.GetFocusedRowCellValue("MaKH")).Select(mt => new
                        {
                            mt.MaNDD,
                            mt.MaKH,
                            mt.TenQD,
                            mt.TenQH,
                            mt.HoTen,
                            mt.STT,
                            mt.Email,
                            DTDD = Common.Right(mt.DTDD, 3),
                            DTCD = Common.Right(mt.DTCD, 3),
                            mt.DiaChiLL,
                            mt.DiaChiTT
                        }).ToList();
                    }
                    else if (obj.DienThoai3Dau == false)
                    {
                        gridControlAvatar.DataSource = db.NguoiDaiDien_getByMaKH((int?)grvBan.GetFocusedRowCellValue("MaKH")).Select(mt => new
                        {
                            mt.MaNDD,
                            mt.MaKH,
                            mt.TenQD,
                            mt.TenQH,
                            mt.HoTen,
                            mt.STT,
                            mt.Email,
                            DTDD = Common.Right1(mt.DTDD, 3),
                            DTCD = Common.Right1(mt.DTCD, 3),
                            mt.DiaChiLL,
                            mt.DiaChiTT
                        }).ToList();
                    }
                    else if (obj.DienThoaiAn == false)
                    {
                        gridControlAvatar.DataSource = db.NguoiDaiDien_getByMaKH((int?)grvBan.GetFocusedRowCellValue("MaKH")).Select(mt => new
                        {
                            mt.MaNDD,
                            mt.MaKH,
                            mt.TenQD,
                            mt.TenQH,
                            mt.HoTen,
                            mt.STT,
                            mt.Email,
                            DTDD = "",
                            DTCD = "",
                            mt.DiaChiLL,
                            mt.DiaChiTT
                        }).ToList();
                    }
                    else
                        gridControlAvatar.DataSource = db.NguoiDaiDien_getByMaKH((int?)grvBan.GetFocusedRowCellValue("MaKH")).ToList();
                    break;
                case 7:
                    #region ghi nhan
                    if (db.mglbcLichSus.Where(p => p.MaBC == maBC).Count() > 1)
                        gcGhiNhan.DataSource = (from ls in db.mglbcLichSus
                                                join nv in db.NhanViens on ls.MaNVS equals nv.MaNV
                                                where ls.MaBC == maBC
                                                select new
                                                {
                                                    ls.ID,
                                                    ls.TenDuong,
                                                    ls.SoNha,
                                                    ls.MatTien,
                                                    ls.DienTich,
                                                    ls.GiaTien,
                                                    ls.DacTrung,
                                                    ls.ThoiHanHD,
                                                    ls.TenKH,
                                                    ls.SoDienThoai,
                                                    ls.Email,
                                                    ls.DiaChi,
                                                    ls.ToaDo,
                                                    ls.CanBoLV,
                                                    nv.HoTen,
                                                    ls.NgaySua
                                                }).ToList();
                    #endregion
                    break;
                case 8:
                    gcChiTiet.DataSource = db.mglbcThongTinTTs.Where(p => p.MaBC == maBC);
                    break;
            }
        }

        void ThuTuCot()
        {
            var stt = db.mglbcThuTuCotCBs.OrderBy(p => p.STT).ToList();
            for (int i = 0; i < stt.Count; i++)
            {
                switch (stt[i].Cot)
                {
                    case "STT":
                        colSTT.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Ký hiệu":
                        colKyHieu.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Nhu cầu":
                        colNhuCau.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Loại BDS":
                        colLoaiDBS.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Trạng thái":
                        colTrangThai.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Số nhà":
                        colSoNha.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Khu vực":
                        colKhuVuc.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tên đường":
                        colTenDuong.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Dự án":
                        colDuAn.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Diện tích bán/Cho thuê":
                        colDienTichCT.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Số tầng bán/Cho thuê":
                        colSoTang.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Mặt tiền":
                        colMatTien.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Diện tích đất":
                        colDienTichDat.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Diện tích XD":
                        colDienTichXD.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Số tầng XD":
                        colSoTangXD.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tọa độ":
                        colKinhDo.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "P.Ngủ":
                        colPNgu.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Đơn vị thuê cũ":
                        colDonViThueCu.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Phòng VS":
                        colPhongVS.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Đơn vị đang thuê":
                        colDonViDangThue.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Thời gian hợp đồng":
                        colThoiHanHD.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Thời hạn BGMB":
                        colThoiGianBGMB.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Ghi chú":
                        colGhiChu.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Hướng của":
                        colHuongCua.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Hướng BC":
                        colHuongBC.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Giá bán":
                        colGiaChoThue.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tổng giá":
                        colTongGia.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Loại tiền":
                        colLoaiTien.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Loại đường":
                        colLoaiDuong.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Đường rộng":
                        colDuongRong.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tình trạng SP":
                        colTinhTrangSP.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Năm XD":
                        colNamXD.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Mặt tiền TT":
                        colMatTienTT.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Mặt sau TT":
                        colMatSauTT.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Dài TT":
                        colDaiTT.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Oto vào":
                        colOtoVao.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tâng hầm":
                        colTangHam.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Căn góc":
                        colCanGoc.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Thang máy":
                        colThangMay.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Thương lượng":
                        colThuongLuong.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tiện ích":
                        colTienIch.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Đặc trung":
                        colDacTrung.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Nguồn":
                        colNguon.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Pháp lý":
                        colPhapLy.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Phí mô giới":
                        colPhiMoGioi.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tình trạng HD":
                        colTinhTrangHD.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Ngày ĐK":
                        colNgayDK.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Khách hàng":
                        colKhachHang.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Số ĐK":
                        colSoDK.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Điện thoại":
                        colDienThoai.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Người đại diện":
                        colNguoiDD.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tên trung gian":
                        colTenTrungGian.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Nhân viên QL":
                        colNhanVienQL.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Nhân viên MG":
                        colNhanVienMG.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Ngày CN":
                        colNgayCN.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Ngày nhập":
                        colNgayNhap.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Video link":
                        colViedeo.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Ảnh":
                        colAnh.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tỉnh":
                        colTinh.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Huyện":
                        colHuyen.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Xã":
                        colXa.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Nhân viên nhập":
                        colNVNhap.VisibleIndex = stt[i].STT.Value;
                        break;

                    case "Điện thoại 2":
                        colDienThoai2.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Điện thoại 3":
                        colDienThoai3.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Điện thoại 4":
                        colDienThoai4.VisibleIndex = stt[i].STT.Value;
                        break;
                }
            }


        }

        void phanquyen()
        {
            try
            {
                var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);
                if (obj.SoNha == false)
                    colSoNha.Visible = false;
                if (obj.TenDuong == false)
                    colTenDuong.Visible = false;
                //if(obj.Tinh == false)
                //    col
                if (obj.MatTien == false)
                    colMatTien.Visible = false;
                if (obj.DienTich == false)
                    colDienTichCT.Visible = false;
                if (obj.GiaTien == false)
                    colTongGia.Visible = false;
                if (obj.DacTrung == false)
                    colDacTrung.Visible = false;
                if (obj.TenKH == false)
                    colKhachHang.Visible = false;
                //if (obj.DienThoaiAn == false)
                //    colDienThoai.Visible = false;
                if (obj.ToaDo == false)
                    colKinhDo.Visible = false;
                if (obj.CanBoLV == false)
                    colNhanVienQL.Visible = false;
            }
            catch { }

        }

        public void PhanQuyenThemSuaKH()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 9;
            DataTable tblAction = o.SelectBy();
            itemThemNLH.Enabled = false;
            itemSuaNLH.Enabled = false;
            itemXoaNLH.Enabled = false;
            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            itemThemNLH.Enabled = true;
                            break;
                        case 2:
                            itemSuaNLH.Enabled = true;
                            break;
                        case 3:
                            itemXoaNLH.Enabled = true;
                            break;

                    }
                }
            }
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {

            this.grvBan.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvBan_RowCellStyle);
            this.grvBan.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvBan_FocusedRowChanged);
            this.grvBan.DoubleClick += new System.EventHandler(this.grvBan_DoubleClick);

            List<ItemTrangThai> lst = new List<ItemTrangThai>();
            lst.Add(new ItemTrangThai() { MaTT = Convert.ToByte(100), TenTT = "<Tất cả>" });
            foreach (var p in db.mglbcTrangThais)
            {
                var obj = new ItemTrangThai() { MaTT = p.MaTT, TenTT = p.TenTT };
                lst.Add(obj);
            }
            lookTrangThai.DataSource = lst;
            chkTrangThai.DataSource = db.mglbcTrangThais;
            lookLoaiBDS.DataSource = db.LoaiBDs;
            lookHuong.DataSource = db.PhuongHuongs;
            lookLoaiDuong.DataSource = db.LoaiDuongs;
            lookPhapLy.DataSource = db.PhapLies;
            lookCapDo.DataSource = db.mglCapDos;
            lookNguon.DataSource = db.mglNguons;
            lookTinhTrangHD.DataSource = db.mglbcTrangThaiHDMGs;
            lookTinhTrang.DataSource = db.mglTinhTrangs;
            lkTrangthai.DataSource = db.mglbcTrangThais;
            lookDVTC.DataSource = lookDVDT.DataSource = db.KhachHangs.Select(p => new { p.MaKH, Name = p.IsPersonal == true ? p.HoKH + " " + p.TenKH : p.TenCongTy });
            chkTinh.DataSource = db.Tinhs;
            setFillter(db.mglbcLocTheoTinhs.FirstOrDefault().MaTinh);
            LoadPermission();

            phanquyen();
            PhanQuyenThemSuaKH();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(Convert.ToInt32(db.mglbcLocNgays.FirstOrDefault().MaBC));
            ThuTuCot();
            grvBan.BestFitColumns();

            cmbMatTien.SelectedIndex = 0;
            cmbDienTichDat.SelectedIndex = 0;
            cmbDienTichXD.SelectedIndex = 0;
            cmbSotangXD.SelectedIndex = 0;
            cmbDTChoThue.SelectedIndex = 0;
            cmbSoTangCT.SelectedIndex = 0;
            cmbDKGiaThue.SelectedIndex = 0;
            cmbTongGia.SelectedIndex = 0;

            cmbAnhBDS.SelectedIndex = 0;
            cmbToaDo.SelectedIndex = 0;
            cmbNgayNhap.SelectedIndex = 0;
            cmbNgayDK.SelectedIndex = 0;
            cmbNgayCN.SelectedIndex = 0;
            cmbThoiHanjHD.SelectedIndex = 0;
            cmbDKTGBGMB.SelectedIndex = 0;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (itemTrangThai.EditValue != "Chọn trạng thái" && itemTrangThai.EditValue != "")
            {
                paging1.CurrentPage = 1;
                Ban_Load();
                phanquyen();
                grvBan.BestFitColumns();
            }

        }

        private void itmThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmEdit frm = new frmEdit();
            frm.mucdich = false;
            frm.ShowDialog();
            if (frm.IsSave)
            {
                Ban_Load();
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBan.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu đăng ký");
                return;
            }
            Ban_Edit();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Ban_Delete();
        }

        private void grvBan_DoubleClick(object sender, EventArgs e)
        {
            if (grvBan.FocusedRowHandle < 0)
            {
                return;
            }

            Ban_Edit();
        }

        private void grvBan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Ban_Delete();
            }
        }

        public void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            //Ban_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            //Ban_Load();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void grvBan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TabPage_Load();
        }

        private void itemXuLy_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBan.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn giao dịch");
                return;
            }

            frmSend frm = new frmSend();
            frm.MaBC = (int)grvBan.GetFocusedRowCellValue("MaBC");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                TabPage_Load();
            }
        }

        private void itemXuLy_Sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvNhatKy.FocusedRowHandle < 0) return;

            frmSend frm = new frmSend();
            frm.ID = (int)grvNhatKy.GetFocusedRowCellValue("ID");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                TabPage_Load();
            }
        }

        private void itemNhatKy_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvNhatKy.FocusedRowHandle < 0) return;
            if (DialogBox.Question() == DialogResult.No) return;
            var objNK = db.mglbcNhatKyXuLies.Single(p => p.ID == (int)grvNhatKy.GetFocusedRowCellValue("ID"));
            db.mglbcNhatKyXuLies.DeleteOnSubmit(objNK);
            db.SubmitChanges();

            TabPage_Load();
        }

        private void itemNhatKy_TraLoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvNhatKy.FocusedRowHandle < 0) return;

            if ((int)grvBan.GetFocusedRowCellValue("MaNVKD") != Common.StaffID) return;

            frmReply frm = new frmReply();
            frm.ID = (int)grvNhatKy.GetFocusedRowCellValue("ID");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                TabPage_Load();
            }
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            TabPage_Load();
        }

        private void grvBan_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                else if (e.Column.FieldName == "TenTT")
                {
                    e.Appearance.BackColor = System.Drawing.Color.FromArgb((int)grvBan.GetRowCellValue(e.RowHandle, "MauNen"));
                }
            }
            catch { }
        }

        private void itemNC_GuiYeuCau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<int> listSP = new List<int>();
            var indexs = grvMuaThue.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn sản phẩm phù hợp để xử lý");
                return;
            }
            foreach (var i in indexs)
            {
                listSP.Add((int)grvBan.GetRowCellValue(i, "MaBC"));
            }
            using (var frm = new MGL.XuLy.frmEdit())
            {
                frm.MaCoHoiMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
                frm.ListSP = listSP;
                frm.ShowDialog();
            }
        }

        private void itemNC_GiaoDich_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMuaThue.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn nhu cầu");
                return;
            }

            if ((byte)grvBan.GetFocusedRowCellValue("MaTT") > 2)
            {
                DialogBox.Error("Sản phẩm đã giao dịch hoặc chưa mở bán");
                return;
            }

            int maBC = (int)grvBan.GetFocusedRowCellValue("MaBC");
            int maMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
            int rowCount = db.mglgdGiaoDiches.Where(p => p.MaBC == maBC & p.MaMT == maMT).Count();
            if (rowCount > 0)
            {
                DialogBox.Error("Sản phẩm đã được giao dịch đang chờ duyệt");
                return;
            }
            using (var frm = new MGL.GiaoDich.frmEdit())
            {
                frm.MaMT = maMT;
                frm.MaBC = maBC;
                frm.ShowDialog();
            }
        }

        private void itemUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs = grvBan.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn sản phẩm");
                return;
            }
            foreach (var i in indexs)
            {
                if ((int)grvBan.GetRowCellValue(i, "MaNVKD") == Common.StaffID)
                {
                    db.mglbcBanChoThues.Single(p => p.MaBC == (int)grvBan.GetRowCellValue(i, "MaBC")).NgayCN = DateTime.Now;
                }
            }
            db.SubmitChanges();

            Ban_Load();
        }

        private void itemMoBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs = grvBan.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn sản phẩm");
                return;
            }
            foreach (var i in indexs)
            {
                db.mglbcBanChoThues.Single(p => p.MaBC == (int)grvBan.GetRowCellValue(i, "MaBC")).MaTT = 0;
            }
            db.SubmitChanges();

            Ban_Load();
        }

        private void itemNgungBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs = grvBan.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn sản phẩm");
                return;
            }
            foreach (var i in indexs)
            {
                db.mglbcBanChoThues.Single(p => p.MaBC == (int)grvBan.GetRowCellValue(i, "MaBC")).MaTT = 0;
            }
            db.SubmitChanges();

            Ban_Load();
        }

        private void itemGiaoDich_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int? maMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
            if (maMT == null)
            {
                DialogBox.Error("Vui lòng chọn nhu cầu");
                return;
            }

            int maBC = (int)grvBan.GetFocusedRowCellValue("MaBC");
            int rowCount = db.mglgdGiaoDiches.Where(p => p.MaBC == maBC & p.MaMT == maMT & p.MaTT != 6).Count();
            if (rowCount > 0)
            {
                DialogBox.Error("Sản phẩm đã được giao dịch đang chờ duyệt");
                return;
            }
            GiaoDich.frmEdit frm = new MGL.GiaoDich.frmEdit();
            frm.MaMT = maMT.Value;
            frm.MaBC = maBC;
            frm.ShowDialog();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmImport frm = new frmImport();
            frm.ShowDialog();
            if (frm.IsSave)
                Ban_Load();
        }

        private void itemCapDo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //try
            //{
            //    int[] indexs = grvBan.GetSelectedRows();
            //    if (indexs.Length <= 0)
            //    {
            //        DialogBox.Error("Vui lòng chọn sản phẩm");
            //        return;
            //    }

            //    var frm = new frmCapDoChon();
            //    frm.ShowDialog();
            //    if (frm.MaCD == 0) return;

            //    foreach (var i in indexs)
            //    {
            //        db.mglbcBanChoThues.Single(p => p.MaBC == (int)grvBan.GetRowCellValue(i, "MaBC")).MaCD = frm.MaCD;
            //    }
            //    db.SubmitChanges();

            //    Ban_Load();
            //}
            //catch (Exception ex)
            //{
            //    DialogBox.Error(ex.Message);
            //}
        }

        private void itemNC_Xem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMuaThue.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Bạn vui lòng chọn khách hàng để xem chi tiết!");
                return;
            }
            if (grvMuaThue.GetFocusedRowCellValue("HoTenNV") == null)
            {
                DialogBox.Infomation("Bạn không có quyền xem thông tin khách hàng này!");
                return;
            }

            var objMT = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == (int)grvMuaThue.GetFocusedRowCellValue("MaMT"));
            using (var frm = new MGL.Mua.frmEdit())
            {
                if (objMT.MaNVKD != Common.StaffID && objMT.MaNVKT != Common.StaffID && Common.PerID != 1)
                {
                    frm.AllowSave = false;
                    frm.DislayContac = false;
                }
                frm.AllowSave = false;
                frm.DislayContac = false;
                frm.MaMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
                frm.ShowDialog();
            }
        }

        private void itemCaiDatTK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new MGL.frmCaiDatLoc())
            {
                frm.ShowDialog();
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            it.CommonCls.ExportExcel(gcBan);
        }

        private void itemSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvMuaThue.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Infomation("Bạn vui lòng chọn khách hàng để gửi Email!");
                return;
            }
            List<int> lstMaMT = new List<int>();
            foreach (var i in indexs)
            {
                lstMaMT.Add((int)grvMuaThue.GetRowCellValue(i, "MaMT"));
            }
            //if (grvMuaThue.FocusedRowHandle < 0)
            //{
            //    DialogBox.Infomation("Bạn vui lòng chọn khách hàng để gửi Email!");
            //    return;
            //}

            //var objMT = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == (int)grvMuaThue.GetFocusedRowCellValue("MaMT"));
            //using (var frm = new MGL.frmSend() { lstMaMT = lstMaMT })
            //{
            //    frm.ShowDialog();
            //}
        }

        private void itemChangeStatus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBan.FocusedRowHandle < 0)
                return;
            using (var frm = new frmXuLy() { MaMT = (int?)grvBan.GetFocusedRowCellValue("MaBC") })
            {
                frm.ShowDialog();
            }
        }

        private void itemCauHinhTinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmLocTinh())
            {
                frm.MainFormCT = this;
                frm.ShowDialog();
            }
        }

        public void setFillter(string tinh)
        {
            itemTinh.EditValue = tinh;
        }

        private void itemCauHinhTG_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmThoiGian())
            {
                frm.MainFormCT = this;
                frm.ShowDialog();
            }
        }

        private void itemThemNLH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var MaKH = (int)grvBan.GetFocusedRowCellValue("MaKH");
            NguoiDaiDien_frm frm = new NguoiDaiDien_frm();
            frm.MaKH = MaKH;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                TabPage_Load();
        }

        private void itemSuaNLH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridAvatar.GetFocusedRowCellValue("MaNDD") != null)
            {
                try
                {
                    NguoiDaiDien_frm frm = new NguoiDaiDien_frm();
                    frm.MaKH = int.Parse(gridAvatar.GetFocusedRowCellValue(colMaKHDD).ToString());
                    frm.MaNDD = (int)gridAvatar.GetFocusedRowCellValue("MaNDD");
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        TabPage_Load();
                }
                catch
                {
                    DialogBox.Infomation("[Người đại diện] này đã được sử dụng. Vui lòng kiểm tra lại.");
                }
            }
        }

        private void itemXoaNLH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridAvatar.GetFocusedRowCellValue(colMaKHDD) != null)
            {
                try
                {
                    if (DialogBox.Question("Bạn có chắc chắn muốn xóa [Người đại diện] này không?") == DialogResult.Yes)
                    {
                        it.NguoiDaiDienCls o = new it.NguoiDaiDienCls();
                        o.MaKH = int.Parse(gridAvatar.GetFocusedRowCellValue(colMaKHDD).ToString());
                        o.MaNDD = (int)gridAvatar.GetFocusedRowCellValue("MaNDD");
                        o.Delete();
                        gridAvatar.DeleteSelectedRows();
                    }
                }
                catch
                {
                    DialogBox.Infomation("[Người đại diện] này đã được sử dụng. Vui lòng kiểm tra lại.");
                }
            }
        }

        private void itemAnhBDS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBan.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn sản phẩm");
                return;
            }
            using (var frm = new frmAnhbds())
            {
                frm.MaBC = (int?)grvBan.GetFocusedRowCellValue("MaBC");
                frm.ShowDialog();
            }
        }

        private void itemSapXepTheoSN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (itemTrangThai.EditValue != "Chọn trạng thái" && itemTrangThai.EditValue != "")
            {
                var tuNgay = (DateTime?)itemTuNgay.EditValue ?? DateTime.Now;
                var denNgay = (DateTime?)itemDenNgay.EditValue ?? DateTime.Now;
                //var MaTT = (byte?)itemTrangThai.EditValue ?? Convert.ToByte(100);
                var strMaTT = (itemTrangThai.EditValue ?? "").ToString().Replace(" ", "");
                var arrMaTT = "," + strMaTT + ",";
                int MaNV = Common.StaffID;
                var wait = DialogBox.WaitingForm();
                try
                {
                    if (itemTuNgay.EditValue == null || itemDenNgay.EditValue == null)
                    {
                        gcBan.DataSource = null;
                        return;
                    }
                    var tinh = (itemTinh.EditValue + "").ToString().Replace(" ", "");
                    var matinh = "," + tinh + ",";

                    var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);

                    var objt = db.crlHuyenQuanLies.Where(p => p.MaNV == Common.StaffID);
                    var lockhuvuc = ",";
                    foreach (var item in objt)
                    {
                        lockhuvuc += item.MaHuyen + ",";
                    }

                    int TotalRecord = 0;

                    switch (GetAccessData())
                    {
                        case 1://Tat ca
                            #region Tat ca

                            #region  if (obj.DienThoai == false)
                            if (obj.DienThoai == false)
                            {
                                var data = db.mglbcBanChoThue_getByDate_Paging_Search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc,
                                    paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                    txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                    (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                    (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                    txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                    txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                    cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    )
                                    .Select(p => new
                                    {
                                        p.STT,
                                        p.MaBC,
                                        p.MauNen,
                                        p.TenTT,
                                        p.MaTT,
                                        VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                        AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                        p.SoGiay,
                                        p.ThoiHan,
                                        p.KyHieu,
                                        p.TenHuyen,
                                        p.TenTinh,
                                        p.TenXa,
                                        p.NamXayDung,
                                        p.TrangThaiHDMG,
                                        p.MaTinhTrang,
                                        p.HoTenNDD,
                                        p.HoTenNTG,
                                        p.HuongBanCong,
                                        p.NgangKV,
                                        p.DaiKV,
                                        p.SauKV,
                                        p.DauOto,
                                        p.TangHam,
                                        p.IsBan,
                                        p.TenNC,
                                        p.MaLBDS,
                                        p.HoTenKH,
                                        p.DiaChi,
                                        SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                        p.PhongVS,
                                        p.DienTichDat,
                                        p.DienTichXD,
                                        p.TenDuong,
                                        p.SoTangXD,
                                        p.ThoiGianHD,
                                        p.ThoiGianBGMB,
                                        p.GhiChu,
                                        p.GioiThieu,
                                        p.DonViDangThue,
                                        p.DonViThueCu,
                                        p.IsCanGoc,
                                        p.IsThangMay,
                                        p.TienIch,
                                        p.DacTrung,
                                        p.KinhDo,
                                        p.ViDo,
                                        p.ToaDo,
                                        p.TenLoaiTien,
                                        p.NgangXD,
                                        p.DaiXD,
                                        p.DienTich,
                                        p.DonGia,
                                        p.ThanhTien,
                                        p.ThuongLuong,
                                        p.PhongKhach,
                                        p.PhongNgu,
                                        p.PhongTam,
                                        p.SoTang,
                                        p.MaHuong,
                                        p.MaKH,
                                        p.PhiMoiGioi,
                                        p.TyLeMG,
                                        p.TyLeHH,
                                        p.ChinhChu,
                                        p.MaTinh,
                                        p.MaLD,
                                        p.DuongRong,
                                        p.MaPL,
                                        p.MaNVKD,
                                        p.MaNVQL,
                                        p.HoTenNVMG,
                                        p.HoTenNV,
                                        p.MaNguon,
                                        p.MaCD,
                                        p.NgayCN,
                                        p.NgayNhap,
                                        p.NgayDK,
                                        p.SoDK,
                                        p.DuAn,
                                        DienThoai = Common.Right(p.DienThoai, 3),
                                        DienThoai2 = Common.Right(p.DiDong2, 3),
                                        DienThoai3 = Common.Right(p.DiDong3, 3),
                                        DienThoai4 = Common.Right(p.DiDong4, 3)
                                    }).OrderBy(p => p.SoNhaaa).ToList();

                                try
                                {
                                    var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                        (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc,
                                        txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                        (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                        (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                        (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                        (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                        (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                        (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                        txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                        txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                        txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                        (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                        cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                        ).ToList();
                                    if (dtc.Count > 0)
                                    {
                                        foreach (var item in dtc)
                                            TotalRecord = (int)item.sl;
                                    }
                                }
                                catch (Exception ex)
                                { }
                                paging1.TotalRecords = TotalRecord;
                                paging1.RefreshPagination();

                                gcBan.DataSource = data;
                            }
                            #endregion

                            #region else if (obj.DienThoai3Dau == false)
                            else if (obj.DienThoai3Dau == false)
                            {
                                var data = db.mglbcBanChoThue_getByDate_Paging_Search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc,
                                    paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                    txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                    (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                    (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                    txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                    txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                    cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    )
                                    .Select(p => new
                                    {
                                        p.STT,
                                        p.MaBC,
                                        p.MauNen,
                                        p.TenTT,
                                        p.MaTT,
                                        VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                        AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                        p.SoGiay,
                                        p.ThoiHan,
                                        p.KyHieu,
                                        p.NamXayDung,
                                        p.TrangThaiHDMG,
                                        p.MaTinhTrang,
                                        p.HoTenNDD,
                                        p.HoTenNTG,
                                        p.HuongBanCong,
                                        p.NgangKV,
                                        p.DaiKV,
                                        p.SauKV,
                                        p.DauOto,
                                        p.TangHam,
                                        p.IsBan,
                                        p.TenNC,
                                        p.MaLBDS,
                                        p.TenHuyen,
                                        p.HoTenKH,
                                        p.DiaChi,
                                        p.TenTinh,
                                        p.TenXa,
                                        SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                        p.PhongVS,
                                        p.DienTichDat,
                                        p.DienTichXD,
                                        p.TenDuong,
                                        p.SoTangXD,
                                        p.ThoiGianHD,
                                        p.ThoiGianBGMB,
                                        p.GhiChu,
                                        p.GioiThieu,
                                        p.DonViDangThue,
                                        p.DonViThueCu,
                                        p.IsCanGoc,
                                        p.IsThangMay,
                                        p.TienIch,
                                        p.DacTrung,
                                        p.KinhDo,
                                        p.ViDo,
                                        p.ToaDo,
                                        p.TenLoaiTien,
                                        p.NgangXD,
                                        p.DaiXD,
                                        p.DienTich,
                                        p.DonGia,
                                        p.ThanhTien,
                                        p.ThuongLuong,
                                        p.PhongKhach,
                                        p.PhongNgu,
                                        p.PhongTam,
                                        p.SoTang,
                                        p.MaHuong,
                                        p.MaKH,
                                        p.PhiMoiGioi,
                                        p.TyLeMG,
                                        p.TyLeHH,
                                        p.ChinhChu,
                                        p.MaTinh,
                                        p.MaLD,
                                        p.DuongRong,
                                        p.MaPL,
                                        p.MaNVKD,
                                        p.MaNVQL,
                                        p.HoTenNVMG,
                                        p.HoTenNV,
                                        p.MaNguon,
                                        p.MaCD,
                                        p.NgayCN,
                                        p.NgayNhap,
                                        p.NgayDK,
                                        p.SoDK,
                                        p.DuAn,
                                        DienThoai = Common.Right1(p.DienThoai, 3),
                                        DienThoai2 = Common.Right1(p.DiDong2, 3),
                                        DienThoai3 = Common.Right1(p.DiDong3, 3),
                                        DienThoai4 = Common.Right1(p.DiDong4, 3)
                                    }).OrderBy(p => p.SoNhaaa).ToList();

                                try
                                {
                                    var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                        (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc,
                                        txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                        (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                        (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                        (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                        (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                        (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                        (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                        txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                        txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                        txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                        (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                        cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                        ).ToList();
                                    if (dtc.Count > 0)
                                    {
                                        foreach (var item in dtc)
                                            TotalRecord = (int)item.sl;
                                    }
                                }
                                catch (Exception ex)
                                { }
                                paging1.TotalRecords = TotalRecord;
                                paging1.RefreshPagination();

                                gcBan.DataSource = data;
                            }
                            #endregion

                            #region else
                            else
                            {
                                var data = db.mglbcBanChoThue_getByDate_Paging_Search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc,
                                    paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                    txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                    (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                    Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                    Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                    (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text,
                                    txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                    txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                    txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                    cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    )
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    p.DienThoai,
                                    p.DiDong2,
                                    p.DiDong3,
                                    p.DiDong4
                                }).OrderBy(p => p.SoNhaaa).ToList();

                                try
                                {
                                    var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                        (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc,
                                        txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                        (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                        (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                        (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                        (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                        (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                        (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                        txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                        txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                        txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                        (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                        cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                        ).ToList();
                                    if (dtc.Count > 0)
                                    {
                                        foreach (var item in dtc)
                                            TotalRecord = (int)item.sl;
                                    }
                                }
                                catch (Exception ex)
                                { }
                                paging1.TotalRecords = TotalRecord;
                                paging1.RefreshPagination();

                                gcBan.DataSource = data;
                            }
                            #endregion

                            #endregion
                            break;
                        case 2://Theo phong ban 
                            #region phong ban

                            #region if (obj.DienThoai == false)
                            if (obj.DienThoai == false)
                            {
                                var data = db.mglbcBanChoThue_getByDate_Paging_Search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false, matinh,
                                    lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(),
                                    txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                    (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                    Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                    Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                    (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                    txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue,
                                    (DateTime?)dateThoiHanHD.EditValue, txtNguon.Text, txtTinhTrangHD.Text,
                                    txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                    txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text,
                                    txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                    cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    )
                                    .Select(p => new
                                    {
                                        p.STT,
                                        p.MaBC,
                                        p.TenTT,
                                        p.MauNen,
                                        p.MaTT,
                                        VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                        AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                        p.SoGiay,
                                        p.ThoiHan,
                                        p.KyHieu,
                                        p.TenTinh,
                                        p.TenXa,
                                        p.NamXayDung,
                                        p.TrangThaiHDMG,
                                        p.MaTinhTrang,
                                        p.HoTenNDD,
                                        p.HoTenNTG,
                                        p.HuongBanCong,
                                        p.NgangKV,
                                        p.DaiKV,
                                        p.SauKV,
                                        p.DauOto,
                                        p.TangHam,
                                        p.IsBan,
                                        p.TenNC,
                                        p.MaLBDS,
                                        p.TenHuyen,
                                        p.HoTenKH,
                                        p.DiaChi,
                                        SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                        p.PhongVS,
                                        p.DienTichDat,
                                        p.DienTichXD,
                                        p.TenDuong,
                                        p.SoTangXD,
                                        p.ThoiGianHD,
                                        p.ThoiGianBGMB,
                                        p.GhiChu,
                                        p.GioiThieu,
                                        p.DonViDangThue,
                                        p.DonViThueCu,
                                        p.IsCanGoc,
                                        p.IsThangMay,
                                        p.TienIch,
                                        p.DacTrung,
                                        p.KinhDo,
                                        p.ViDo,
                                        p.ToaDo,
                                        p.TenLoaiTien,
                                        p.NgangXD,
                                        p.DaiXD,
                                        p.DienTich,
                                        p.DonGia,
                                        p.ThanhTien,
                                        p.ThuongLuong,
                                        p.PhongKhach,
                                        p.PhongNgu,
                                        p.PhongTam,
                                        p.SoTang,
                                        p.MaHuong,
                                        p.MaKH,
                                        p.PhiMoiGioi,
                                        p.TyLeMG,
                                        p.TyLeHH,
                                        p.ChinhChu,
                                        p.MaTinh,
                                        p.MaLD,
                                        p.DuongRong,
                                        p.MaPL,
                                        p.MaNVKD,
                                        p.MaNVQL,
                                        p.HoTenNVMG,
                                        p.HoTenNV,
                                        p.MaNguon,
                                        p.MaCD,
                                        p.NgayCN,
                                        p.NgayNhap,
                                        p.NgayDK,
                                        p.SoDK,
                                        p.DuAn,
                                        DienThoai = Common.Right(p.DienThoai, 3)
                                    }).OrderBy(p => p.SoNhaaa).ToList();//.Where(p => p.IsBan.GetValueOrDefault() == true);

                                try
                                {
                                    var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                        (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false,
                                        matinh, lockhuvuc, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text,
                                        txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                        (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                        (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                        (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                        (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                        (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                        txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                        txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                        txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                        (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                        cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                        ).ToList();
                                    if (dtc.Count > 0)
                                    {
                                        foreach (var item in dtc)
                                            TotalRecord = (int)item.sl;
                                    }
                                }
                                catch (Exception ex)
                                { }
                                paging1.TotalRecords = TotalRecord;
                                paging1.RefreshPagination();

                                gcBan.DataSource = data;

                            }
                            #endregion

                            #region else if (obj.DienThoai3Dau == false)
                            else if (obj.DienThoai3Dau == false)
                            {
                                var data = db.mglbcBanChoThue_getByDate_Paging_Search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false, matinh,
                                    lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(),
                                    txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                    (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                    Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                    Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                    (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text,
                                    txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text
                                    , txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text,
                                    txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue,
                                    txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    )
                            .Select(p => new
                            {
                                p.STT,
                                p.MaBC,
                                p.TenTT,
                                p.MauNen,
                                p.MaTT,
                                VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                p.SoGiay,
                                p.ThoiHan,
                                p.KyHieu,
                                p.NamXayDung,
                                p.TrangThaiHDMG,
                                p.MaTinhTrang,
                                p.HoTenNDD,
                                p.HoTenNTG,
                                p.HuongBanCong,
                                p.NgangKV,
                                p.TenTinh,
                                p.TenXa,
                                p.DaiKV,
                                p.SauKV,
                                p.DauOto,
                                p.TangHam,
                                p.IsBan,
                                p.TenNC,
                                p.MaLBDS,
                                p.TenHuyen,
                                p.HoTenKH,
                                p.DiaChi,
                                SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                p.PhongVS,
                                p.DienTichDat,
                                p.DienTichXD,
                                p.TenDuong,
                                p.SoTangXD,
                                p.ThoiGianHD,
                                p.ThoiGianBGMB,
                                p.GhiChu,
                                p.GioiThieu,
                                p.DonViDangThue,
                                p.DonViThueCu,
                                p.IsCanGoc,
                                p.IsThangMay,
                                p.TienIch,
                                p.DacTrung,
                                p.KinhDo,
                                p.ViDo,
                                p.ToaDo,
                                p.TenLoaiTien,
                                p.NgangXD,
                                p.DaiXD,
                                p.DienTich,
                                p.DonGia,
                                p.ThanhTien,
                                p.ThuongLuong,
                                p.PhongKhach,
                                p.PhongNgu,
                                p.PhongTam,
                                p.SoTang,
                                p.MaHuong,
                                p.MaKH,
                                p.PhiMoiGioi,
                                p.TyLeMG,
                                p.TyLeHH,
                                p.ChinhChu,
                                p.MaTinh,
                                p.MaLD,
                                p.DuongRong,
                                p.MaPL,
                                p.MaNVKD,
                                p.MaNVQL,
                                p.HoTenNVMG,
                                p.HoTenNV,
                                p.MaNguon,
                                p.MaCD,
                                p.NgayCN,
                                p.NgayNhap,
                                p.NgayDK,
                                p.SoDK,
                                p.DuAn,
                                DienThoai = Common.Right1(p.DienThoai, 3)
                            }).OrderBy(p => p.SoNhaaa).ToList();

                                try
                                {
                                    var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                        (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false, matinh,
                                        lockhuvuc, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                        (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                        Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                        Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                        (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue,
                                        (DateTime?)dateThoiHanHD.EditValue, txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                        txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text,
                                        txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                        cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                        ).ToList();
                                    if (dtc.Count > 0)
                                    {
                                        foreach (var item in dtc)
                                            TotalRecord = (int)item.sl;
                                    }
                                }
                                catch (Exception ex)
                                { }
                                paging1.TotalRecords = TotalRecord;
                                paging1.RefreshPagination();

                                gcBan.DataSource = data;
                            }
                            #endregion

                            #region else
                            else
                            {
                                var data = db.mglbcBanChoThue_getByDate_Paging_Search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false, matinh,
                                    lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(),
                                    txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                    (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                    Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                    Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                    (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text,
                                    txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                    txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                    txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                    cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    )
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    p.SoGiay,
                                    p.ThoiHan,
                                    p.KyHieu,
                                    p.HoTenNVN,
                                    p.NamXayDung,
                                    p.TrangThaiHDMG,
                                    p.MaTinhTrang,
                                    p.HoTenNDD,
                                    p.HoTenNTG,
                                    p.HuongBanCong,
                                    p.NgangKV,
                                    p.DaiKV,
                                    p.SauKV,
                                    p.DauOto,
                                    p.TangHam,
                                    p.IsBan,
                                    p.TenNC,
                                    p.MaLBDS,
                                    p.TenHuyen,
                                    p.TenTinh,
                                    p.TenXa,
                                    p.HoTenKH,
                                    p.DiaChi,
                                    SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViDangThue,
                                    p.DonViThueCu,
                                    p.IsCanGoc,
                                    p.IsThangMay,
                                    p.TienIch,
                                    p.DacTrung,
                                    p.KinhDo,
                                    p.ViDo,
                                    p.ToaDo,
                                    p.TenLoaiTien,
                                    p.NgangXD,
                                    p.DaiXD,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.ThuongLuong,
                                    p.PhongKhach,
                                    p.PhongNgu,
                                    p.PhongTam,
                                    p.SoTang,
                                    p.MaHuong,
                                    p.MaKH,
                                    p.PhiMoiGioi,
                                    p.TyLeMG,
                                    p.TyLeHH,
                                    p.ChinhChu,
                                    p.MaTinh,
                                    p.MaLD,
                                    p.DuongRong,
                                    p.MaPL,
                                    p.MaNVKD,
                                    p.MaNVQL,
                                    p.HoTenNVMG,
                                    p.HoTenNV,
                                    p.MaNguon,
                                    p.MaCD,
                                    p.NgayCN,
                                    p.NgayNhap,
                                    p.NgayDK,
                                    p.SoDK,
                                    p.DuAn,
                                    p.DienThoai
                                }).OrderBy(p => p.SoNhaaa).ToList();

                                try
                                {
                                    var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                        (tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false,
                                        matinh, lockhuvuc, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text,
                                        txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                        (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                        (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                        (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                        (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                        (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                        txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                        txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                        txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                        (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                        cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                        ).ToList();
                                    if (dtc.Count > 0)
                                    {
                                        foreach (var item in dtc)
                                            TotalRecord = (int)item.sl;
                                    }
                                }
                                catch (Exception ex)
                                { }
                                paging1.TotalRecords = TotalRecord;
                                paging1.RefreshPagination();

                                gcBan.DataSource = data;
                            }
                            #endregion

                            #endregion
                            break;
                        case 3://Theo nhom
                            #region Theo nhom

                            #region  if (obj.DienThoai == false)
                            if (obj.DienThoai == false)
                            {
                                var data = db.mglbcBanChoThue_getByDate_Paging_Search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false, matinh,
                                    lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(),
                                    txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                    (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                    Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                    Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                    (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                    txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue,
                                    (DateTime?)dateThoiHanHD.EditValue, txtNguon.Text, txtTinhTrangHD.Text,
                                    txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text,
                                    txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text,
                                    (DateTime?)dateNgayNhap.EditValue, (DateTime?)dateNgayDK.EditValue,
                                    txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text
                                    , cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    )
                                    .Select(p => new
                                    {
                                        p.STT,
                                        p.MaBC,
                                        p.TenTT,
                                        p.MauNen,
                                        p.MaTT,
                                        VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                        AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                        p.SoGiay,
                                        p.ThoiHan,
                                        p.KyHieu,
                                        p.NamXayDung,
                                        p.TrangThaiHDMG,
                                        p.MaTinhTrang,
                                        p.HoTenNDD,
                                        p.HoTenNTG,
                                        p.HuongBanCong,
                                        p.NgangKV,
                                        p.DaiKV,
                                        p.SauKV,
                                        p.DauOto,
                                        p.TangHam,
                                        p.IsBan,
                                        p.TenNC,
                                        p.MaLBDS,
                                        p.TenHuyen,
                                        p.HoTenKH,
                                        p.DiaChi,
                                        p.TenTinh,
                                        p.TenXa,
                                        SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                        p.PhongVS,
                                        p.DienTichDat,
                                        p.DienTichXD,
                                        p.TenDuong,
                                        p.SoTangXD,
                                        p.ThoiGianHD,
                                        p.ThoiGianBGMB,
                                        p.GhiChu,
                                        p.GioiThieu,
                                        p.DonViDangThue,
                                        p.DonViThueCu,
                                        p.IsCanGoc,
                                        p.IsThangMay,
                                        p.TienIch,
                                        p.DacTrung,
                                        p.KinhDo,
                                        p.ViDo,
                                        p.ToaDo,
                                        p.TenLoaiTien,
                                        p.NgangXD,
                                        p.DaiXD,
                                        p.DienTich,
                                        p.DonGia,
                                        p.ThanhTien,
                                        p.ThuongLuong,
                                        p.PhongKhach,
                                        p.PhongNgu,
                                        p.PhongTam,
                                        p.SoTang,
                                        p.MaHuong,
                                        p.MaKH,
                                        p.PhiMoiGioi,
                                        p.TyLeMG,
                                        p.TyLeHH,
                                        p.ChinhChu,
                                        p.MaTinh,
                                        p.MaLD,
                                        p.DuongRong,
                                        p.MaPL,
                                        p.MaNVKD,
                                        p.MaNVQL,
                                        p.HoTenNVMG,
                                        p.HoTenNV,
                                        p.MaNguon,
                                        p.MaCD,
                                        p.NgayCN,
                                        p.NgayNhap,
                                        p.NgayDK,
                                        p.SoDK,
                                        p.DuAn,
                                        DienThoai = Common.Right(p.DienThoai, 3)
                                    }).OrderBy(p => p.SoNhaaa).ToList();//.Where(p => p.IsBan.GetValueOrDefault() == true);

                                try
                                {
                                    var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                        (tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV,
                                        false, matinh, lockhuvuc, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                        txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                        (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                        Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                        Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                        (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text,
                                        txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue,
                                        (DateTime?)dateThoiHanHD.EditValue, txtNguon.Text, txtTinhTrangHD.Text,
                                        txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text,
                                        txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text,
                                        txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                        (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                        cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                        ).ToList();
                                    if (dtc.Count > 0)
                                    {
                                        foreach (var item in dtc)
                                            TotalRecord = (int)item.sl;
                                    }
                                }
                                catch (Exception ex)
                                { }
                                paging1.TotalRecords = TotalRecord;
                                paging1.RefreshPagination();

                                gcBan.DataSource = data;
                            }
                            #endregion

                            #region else if (obj.DienThoai3Dau == false)
                            else if (obj.DienThoai3Dau == false)
                            {
                                var data = db.mglbcBanChoThue_getByDate_Paging_Search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false, matinh,
                                    lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                    txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                    (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                    (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                    txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                    txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                    cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    )
                                    .Select(p => new
                                    {
                                        p.STT,
                                        p.MaBC,
                                        p.TenTT,
                                        p.MauNen,
                                        p.MaTT,
                                        VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                        AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                        p.SoGiay,
                                        p.ThoiHan,
                                        p.KyHieu,
                                        p.NamXayDung,
                                        p.TrangThaiHDMG,
                                        p.MaTinhTrang,
                                        p.HoTenNDD,
                                        p.HoTenNTG,
                                        p.HuongBanCong,
                                        p.NgangKV,
                                        p.DaiKV,
                                        p.SauKV,
                                        p.DauOto,
                                        p.TangHam,
                                        p.IsBan,
                                        p.TenNC,
                                        p.MaLBDS,
                                        p.TenHuyen,
                                        p.HoTenKH,
                                        p.DiaChi,
                                        p.TenTinh,
                                        p.TenXa,
                                        SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                        p.PhongVS,
                                        p.DienTichDat,
                                        p.DienTichXD,
                                        p.TenDuong,
                                        p.SoTangXD,
                                        p.ThoiGianHD,
                                        p.ThoiGianBGMB,
                                        p.GhiChu,
                                        p.GioiThieu,
                                        p.DonViDangThue,
                                        p.DonViThueCu,
                                        p.IsCanGoc,
                                        p.IsThangMay,
                                        p.TienIch,
                                        p.DacTrung,
                                        p.KinhDo,
                                        p.ViDo,
                                        p.ToaDo,
                                        p.TenLoaiTien,
                                        p.NgangXD,
                                        p.DaiXD,
                                        p.DienTich,
                                        p.DonGia,
                                        p.ThanhTien,
                                        p.ThuongLuong,
                                        p.PhongKhach,
                                        p.PhongNgu,
                                        p.PhongTam,
                                        p.SoTang,
                                        p.MaHuong,
                                        p.MaKH,
                                        p.PhiMoiGioi,
                                        p.TyLeMG,
                                        p.TyLeHH,
                                        p.ChinhChu,
                                        p.MaTinh,
                                        p.MaLD,
                                        p.DuongRong,
                                        p.MaPL,
                                        p.MaNVKD,
                                        p.MaNVQL,
                                        p.HoTenNVMG,
                                        p.HoTenNV,
                                        p.MaNguon,
                                        p.MaCD,
                                        p.NgayCN,
                                        p.NgayNhap,
                                        p.NgayDK,
                                        p.SoDK,
                                        p.DuAn,
                                        DienThoai = Common.Right1(p.DienThoai, 3)
                                    }).OrderBy(p => p.SoNhaaa).ToList();

                                try
                                {
                                    var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                        (tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false,
                                        matinh, lockhuvuc, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text,
                                        txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                        (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                        (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                        (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                        (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                        (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                        txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                        txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                        txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                        (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                        cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                        ).ToList();
                                    if (dtc.Count > 0)
                                    {
                                        foreach (var item in dtc)
                                            TotalRecord = (int)item.sl;
                                    }
                                }
                                catch (Exception ex)
                                { }
                                paging1.TotalRecords = TotalRecord;
                                paging1.RefreshPagination();

                                gcBan.DataSource = data;
                            }
                            #endregion

                            #region else
                            else
                            {
                                var data = db.mglbcBanChoThue_getByDate_Paging_Search
                                    (tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc,
                                    paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                    txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                    (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                    (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                    txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                    txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                    cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    )
                                    .Select(p => new
                                    {
                                        p.STT,
                                        p.MaBC,
                                        p.MauNen,
                                        p.TenTT,
                                        VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                        p.MaTT,
                                        AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                        p.SoGiay,
                                        p.ThoiHan,
                                        p.KyHieu,
                                        p.HoTenNVN,
                                        p.NamXayDung,
                                        p.TrangThaiHDMG,
                                        p.MaTinhTrang,
                                        p.HoTenNDD,
                                        p.HoTenNTG,
                                        p.HuongBanCong,
                                        p.NgangKV,
                                        p.DaiKV,
                                        p.SauKV,
                                        p.DauOto,
                                        p.TangHam,
                                        p.IsBan,
                                        p.TenNC,
                                        p.MaLBDS,
                                        p.TenHuyen,
                                        p.TenTinh,
                                        p.TenXa,
                                        p.HoTenKH,
                                        p.DiaChi,
                                        SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                        p.PhongVS,
                                        p.DienTichDat,
                                        p.DienTichXD,
                                        p.TenDuong,
                                        p.SoTangXD,
                                        p.ThoiGianHD,
                                        p.ThoiGianBGMB,
                                        p.GhiChu,
                                        p.GioiThieu,
                                        p.DonViDangThue,
                                        p.DonViThueCu,
                                        p.IsCanGoc,
                                        p.IsThangMay,
                                        p.TienIch,
                                        p.DacTrung,
                                        p.KinhDo,
                                        p.ViDo,
                                        p.ToaDo,
                                        p.TenLoaiTien,
                                        p.NgangXD,
                                        p.DaiXD,
                                        p.DienTich,
                                        p.DonGia,
                                        p.ThanhTien,
                                        p.ThuongLuong,
                                        p.PhongKhach,
                                        p.PhongNgu,
                                        p.PhongTam,
                                        p.SoTang,
                                        p.MaHuong,
                                        p.MaKH,
                                        p.PhiMoiGioi,
                                        p.TyLeMG,
                                        p.TyLeHH,
                                        p.ChinhChu,
                                        p.MaTinh,
                                        p.MaLD,
                                        p.DuongRong,
                                        p.MaPL,
                                        p.MaNVKD,
                                        p.MaNVQL,
                                        p.HoTenNVMG,
                                        p.HoTenNV,
                                        p.MaNguon,
                                        p.MaCD,
                                        p.NgayCN,
                                        p.NgayNhap,
                                        p.NgayDK,
                                        p.SoDK,
                                        p.DuAn,
                                        p.DienThoai
                                    }).OrderBy(p => p.SoNhaaa).ToList();


                                try
                                {
                                    var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                        (tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false,
                                        matinh, lockhuvuc, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                        txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue,
                                        (decimal?)spinDienTichDat.EditValue, (decimal?)spinDienTichXD.EditValue,
                                        Convert.ToInt32(spinSoTangXD.EditValue), (decimal?)spinDienTichCT.EditValue,
                                        Convert.ToInt32(spinSoTangCT.EditValue), (decimal?)spinGiaThue.EditValue,
                                        (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                        txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue,
                                        (DateTime?)dateThoiHanHD.EditValue, txtNguon.Text, txtTinhTrangHD.Text,
                                        txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                        txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text,
                                        txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                        (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text,
                                        cmbAnhBDS.Text, cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                        ).ToList();
                                    if (dtc.Count > 0)
                                    {
                                        foreach (var item in dtc)
                                            TotalRecord = (int)item.sl;
                                    }
                                }
                                catch (Exception ex)
                                { }
                                paging1.TotalRecords = TotalRecord;
                                paging1.RefreshPagination();

                                gcBan.DataSource = data;
                            }
                            #endregion

                            #endregion
                            break;
                        case 4://Theo nhan vien
                            #region Theo nhan vien

                            #region  if (obj.DienThoai == false)
                            if (obj.DienThoai == false)
                            {
                                var data = db.mglbcBanChoThue_getByDate_Paging_Search
                                    (tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc,
                                    paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                    txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                    (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                    (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                    txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                    txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                    txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                    cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    )
                                    .Select(p => new
                                    {
                                        p.STT,
                                        p.MaBC,
                                        p.TenTT,
                                        p.MauNen,
                                        p.MaTT,
                                        VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                        AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                        p.SoGiay,
                                        p.ThoiHan,
                                        p.KyHieu,
                                        p.NamXayDung,
                                        p.TrangThaiHDMG,
                                        p.MaTinhTrang,
                                        p.HoTenNDD,
                                        p.HoTenNTG,
                                        p.HuongBanCong,
                                        p.NgangKV,
                                        p.DaiKV,
                                        p.SauKV,
                                        p.DauOto,
                                        p.TangHam,
                                        p.IsBan,
                                        p.TenNC,
                                        p.MaLBDS,
                                        p.TenHuyen,
                                        p.HoTenKH,
                                        p.DiaChi,
                                        p.TenTinh,
                                        p.TenXa,
                                        SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                        p.PhongVS,
                                        p.DienTichDat,
                                        p.DienTichXD,
                                        p.TenDuong,
                                        p.SoTangXD,
                                        p.ThoiGianHD,
                                        p.ThoiGianBGMB,
                                        p.GhiChu,
                                        p.GioiThieu,
                                        p.DonViDangThue,
                                        p.DonViThueCu,
                                        p.IsCanGoc,
                                        p.IsThangMay,
                                        p.TienIch,
                                        p.DacTrung,
                                        p.KinhDo,
                                        p.ViDo,
                                        p.ToaDo,
                                        p.TenLoaiTien,
                                        p.NgangXD,
                                        p.DaiXD,
                                        p.DienTich,
                                        p.DonGia,
                                        p.ThanhTien,
                                        p.ThuongLuong,
                                        p.PhongKhach,
                                        p.PhongNgu,
                                        p.PhongTam,
                                        p.SoTang,
                                        p.MaHuong,
                                        p.MaKH,
                                        p.PhiMoiGioi,
                                        p.TyLeMG,
                                        p.TyLeHH,
                                        p.ChinhChu,
                                        p.MaTinh,
                                        p.MaLD,
                                        p.DuongRong,
                                        p.MaPL,
                                        p.MaNVKD,
                                        p.MaNVQL,
                                        p.HoTenNVMG,
                                        p.HoTenNV,
                                        p.MaNguon,
                                        p.MaCD,
                                        p.NgayCN,
                                        p.NgayNhap,
                                        p.NgayDK,
                                        p.SoDK,
                                        p.DuAn,
                                        DienThoai = Common.Right(p.DienThoai, 3)
                                    }).OrderBy(p => p.SoNhaaa).ToList();

                                try
                                {
                                    var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                        (tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc,
                                        txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                        (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                        (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                        (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                        (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                        (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                        (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                        txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                        txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                        txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                        (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text
                                        , cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                        ).ToList();
                                    if (dtc.Count > 0)
                                    {
                                        foreach (var item in dtc)
                                            TotalRecord = (int)item.sl;
                                    }
                                }
                                catch (Exception ex)
                                { }
                                paging1.TotalRecords = TotalRecord;
                                paging1.RefreshPagination();

                                gcBan.DataSource = data;
                            }
                            #endregion

                            #region else if (obj.DienThoai3Dau == false)
                            else if (obj.DienThoai3Dau == false)
                            {
                                var data = db.mglbcBanChoThue_getByDate_Paging_Search
                                    (tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc,
                                    paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                    txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                    txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue,
                                    (DateTime?)dateThoiHanHD.EditValue, txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text,
                                    txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                    txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text
                                    , cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    )
                                   .Select(p => new
                                   {
                                       p.STT,
                                       p.MaBC,
                                       p.TenTT,
                                       p.MauNen,
                                       p.MaTT,
                                       VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                       AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                       p.SoGiay,
                                       p.ThoiHan,
                                       p.KyHieu,
                                       p.NamXayDung,
                                       p.TrangThaiHDMG,
                                       p.MaTinhTrang,
                                       p.HoTenNDD,
                                       p.HoTenNTG,
                                       p.HuongBanCong,
                                       p.NgangKV,
                                       p.DaiKV,
                                       p.SauKV,
                                       p.DauOto,
                                       p.TangHam,
                                       p.IsBan,
                                       p.TenNC,
                                       p.MaLBDS,
                                       p.TenHuyen,
                                       p.HoTenKH,
                                       p.DiaChi,
                                       p.TenTinh,
                                       p.TenXa,
                                       SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                       p.PhongVS,
                                       p.DienTichDat,
                                       p.DienTichXD,
                                       p.TenDuong,
                                       p.SoTangXD,
                                       p.ThoiGianHD,
                                       p.ThoiGianBGMB,
                                       p.GhiChu,
                                       p.GioiThieu,
                                       p.DonViDangThue,
                                       p.DonViThueCu,
                                       p.IsCanGoc,
                                       p.IsThangMay,
                                       p.TienIch,
                                       p.DacTrung,
                                       p.KinhDo,
                                       p.ViDo,
                                       p.ToaDo,
                                       p.TenLoaiTien,
                                       p.NgangXD,
                                       p.DaiXD,
                                       p.DienTich,
                                       p.DonGia,
                                       p.ThanhTien,
                                       p.ThuongLuong,
                                       p.PhongKhach,
                                       p.PhongNgu,
                                       p.PhongTam,
                                       p.SoTang,
                                       p.MaHuong,
                                       p.MaKH,
                                       p.PhiMoiGioi,
                                       p.TyLeMG,
                                       p.TyLeHH,
                                       p.ChinhChu,
                                       p.MaTinh,
                                       p.MaLD,
                                       p.DuongRong,
                                       p.MaPL,
                                       p.MaNVKD,
                                       p.MaNVQL,
                                       p.HoTenNVMG,
                                       p.HoTenNV,
                                       p.MaNguon,
                                       p.MaCD,
                                       p.NgayCN,
                                       p.NgayNhap,
                                       p.NgayDK,
                                       p.SoDK,
                                       p.DuAn,
                                       DienThoai = Common.Right1(p.DienThoai, 3)
                                   }).OrderBy(p => p.SoNhaaa).ToList();

                                try
                                {
                                    var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                        (tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc,
                                        txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                        (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                        (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                        (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                        (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                        (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                        (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                        txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                        txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                        txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                        (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text
                                        , cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                        ).ToList();
                                    if (dtc.Count > 0)
                                    {
                                        foreach (var item in dtc)
                                            TotalRecord = (int)item.sl;
                                    }
                                }
                                catch (Exception ex)
                                { }
                                paging1.TotalRecords = TotalRecord;
                                paging1.RefreshPagination();

                                gcBan.DataSource = data;
                            }
                            #endregion

                            #region else
                            else
                            {
                                var data = db.mglbcBanChoThue_getByDate_Paging_Search
                                    (tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc,
                                    paging1.CurrentPage, paging1.PageRows, txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(),
                                    txtXa.Text, txtHuyen.Text, (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                    (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                    (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                    (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue, (DateTime?)dateNgayCN.EditValue,
                                    txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue,
                                    (DateTime?)dateThoiHanHD.EditValue, txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text,
                                    txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                    txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                    (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text
                                    , cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                    )
                                    .Select(p => new
                                    {
                                        p.STT,
                                        p.MaBC,
                                        p.MauNen,
                                        p.TenTT,
                                        VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                        p.MaTT,
                                        AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                        p.SoGiay,
                                        p.ThoiHan,
                                        p.KyHieu,
                                        p.HoTenNVN,
                                        p.NamXayDung,
                                        p.TrangThaiHDMG,
                                        p.MaTinhTrang,
                                        p.HoTenNDD,
                                        p.HoTenNTG,
                                        p.HuongBanCong,
                                        p.NgangKV,
                                        p.DaiKV,
                                        p.SauKV,
                                        p.DauOto,
                                        p.TangHam,
                                        p.IsBan,
                                        p.TenNC,
                                        p.MaLBDS,
                                        p.TenHuyen,
                                        p.TenTinh,
                                        p.TenXa,
                                        p.HoTenKH,
                                        p.DiaChi,
                                        SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                        p.PhongVS,
                                        p.DienTichDat,
                                        p.DienTichXD,
                                        p.TenDuong,
                                        p.SoTangXD,
                                        p.ThoiGianHD,
                                        p.ThoiGianBGMB,
                                        p.GhiChu,
                                        p.GioiThieu,
                                        p.DonViDangThue,
                                        p.DonViThueCu,
                                        p.IsCanGoc,
                                        p.IsThangMay,
                                        p.TienIch,
                                        p.DacTrung,
                                        p.KinhDo,
                                        p.ViDo,
                                        p.ToaDo,
                                        p.TenLoaiTien,
                                        p.NgangXD,
                                        p.DaiXD,
                                        p.DienTich,
                                        p.DonGia,
                                        p.ThanhTien,
                                        p.ThuongLuong,
                                        p.PhongKhach,
                                        p.PhongNgu,
                                        p.PhongTam,
                                        p.SoTang,
                                        p.MaHuong,
                                        p.MaKH,
                                        p.PhiMoiGioi,
                                        p.TyLeMG,
                                        p.TyLeHH,
                                        p.ChinhChu,
                                        p.MaTinh,
                                        p.MaLD,
                                        p.DuongRong,
                                        p.MaPL,
                                        p.MaNVKD,
                                        p.MaNVQL,
                                        p.HoTenNVMG,
                                        p.HoTenNV,
                                        p.MaNguon,
                                        p.MaCD,
                                        p.NgayCN,
                                        p.NgayNhap,
                                        p.NgayDK,
                                        p.SoDK,
                                        p.DuAn,
                                        p.DienThoai
                                    }).OrderBy(p => p.SoNhaaa).ToList();

                                try
                                {
                                    var dtc = db.mglbcBanChoThue_getByDate_Count_search
                                        (tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc,
                                        txtDiaChi.Text.Trim(), txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
                                        (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
                                        (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
                                        (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
                                        (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
                                        (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
                                        (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                        txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
                                        txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
                                        txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                        (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
                                        cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text
                                        ).ToList();
                                    if (dtc.Count > 0)
                                    {
                                        foreach (var item in dtc)
                                            TotalRecord = (int)item.sl;
                                    }
                                }
                                catch (Exception ex)
                                { }
                                paging1.TotalRecords = TotalRecord;
                                paging1.RefreshPagination();

                                gcBan.DataSource = data;

                            }
                            #endregion


                            #endregion
                            break;
                        default:
                            gcBan.DataSource = null;
                            break;
                    }
                }
                catch { }
                finally
                {
                    wait.Close();
                }
            }
        }

        private void itemVideo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBan.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn sản phẩm");
                return;
            }
            using (var frm = new frmVideo())
            {
                frm.MaBC = (int?)grvBan.GetFocusedRowCellValue("MaBC");
                frm.ShowDialog();
            }
        }

        private void itemTrangThai_EditValueChanged(object sender, EventArgs e)
        {
            //if (itemTrangThai.EditValue != "")
            //{
            //    var a = itemTrangThai.EditValue;
            //    byte? maTT = (byte?)itemTrangThai.EditValue;
            //    if (maTT == 1)
            //    {
            //        itemMoBan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //        itemNgungBan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //        itemUp.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //        itemGiaoDich.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //    }
            //    else
            //    {
            //        if (maTT == 2)
            //            itemMoBan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //        else
            //            itemMoBan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //        itemNgungBan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //        itemUp.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //        itemGiaoDich.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    }

            //    Ban_Load();
            //    phanquyen();
            //}
        }

        private void SearchBan()
        {
            if (itemTrangThai.EditValue != "Chọn trạng thái" && itemTrangThai.EditValue != "")
            {
                paging1.CurrentPage = 1;
                Ban_Load();
                phanquyen();
                grvBan.BestFitColumns();
            }
        }

        private void btnTimKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            popupControlContainer1.ShowPopup(barManager1, gcBan.PointToScreen(Point.Empty));
            //if (btnTimKiem.Caption== "Hiện thị tìm kiếm")
            //{
            //    panelControl1.Height = 180;
            //    btnTimKiem.Caption = "Ẩn tìm kiếm";
            //}
            //else
            //{
            //    panelControl1.Height = 0;
            //    btnTimKiem.Caption = "Hiện thị tìm kiếm";
            //}
        }

        private void linkToaDo_Click(object sender, EventArgs e)
        {
            var toado = grvBan.GetFocusedRowCellValue("ToaDo");
            if (toado != "")
                System.Diagnostics.Process.Start("https://www.google.com/maps/place/" + toado);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchBan();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDiaChi.EditValue = null;
            spinGiaThue.EditValue = 0;
            txtKhachHang.EditValue = null;
            dateNgayNhap.EditValue = null;
            txtTenDuong.EditValue = null;
            spinTongGia.EditValue = 0;
            txtDienThoai.EditValue = null;
            dateNgayDK.EditValue = null;
            txtXa.EditValue = null;
            dateNgayCN.EditValue = null;
            txtPhapLy.EditValue = null;
            txtNhanVienNhap.EditValue = null;
            txtHuyen.EditValue = null;
            txtDonViTC.EditValue = null;
            txtSoDK.EditValue = null;
            spinMatTien.EditValue = 0;
            txtDonViDT.EditValue = null;
            txtKyHieu.EditValue = null;
            spinDienTichDat.EditValue = 0;
            dateTGBGBM.EditValue = null;
            txtLoaiBDS.EditValue = null;
            spinDienTichXD.EditValue = 0;
            dateThoiHanHD.EditValue = null;
            txtToaDo.EditValue = null;
            spinSoTangXD.EditValue = 0;
            txtNguon.EditValue = null;
            txtNhanVienQL.EditValue = null;
            spinDienTichCT.EditValue = 0;
            txtTinhTrangHD.EditValue = null;
            txtNhanVienMQ.EditValue = null;
            spinSoTangCT.EditValue = 0;
            dateTGBGBMTo.EditValue = null;

            cmbMatTien.SelectedIndex = 0;
            cmbDienTichDat.SelectedIndex = 0;
            cmbDienTichXD.SelectedIndex = 0;
            cmbSotangXD.SelectedIndex = 0;
            cmbDTChoThue.SelectedIndex = 0;
            cmbSoTangCT.SelectedIndex = 0;
            cmbDKGiaThue.SelectedIndex = 0;
            cmbTongGia.SelectedIndex = 0;

            cmbAnhBDS.SelectedIndex = 0;
            cmbToaDo.SelectedIndex = 0;
            cmbNgayNhap.SelectedIndex = 0;
            cmbNgayDK.SelectedIndex = 0;
            cmbNgayCN.SelectedIndex = 0;
            cmbThoiHanjHD.SelectedIndex = 0;
            cmbDKTGBGMB.SelectedIndex = 0;




            //SearchBan();

        }

        private void cmbDKTGBGBM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDKTGBGMB.SelectedIndex == 0)
            {
                dateTGBGBMTo.Visible = false;
            }
            else if (cmbDKTGBGMB.SelectedIndex == 1)
            {
                dateTGBGBMTo.Visible = true;
            }
        }

        private void cmbNgayDK_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNgayDK.SelectedIndex == 0)
            {
                dateNgayDKTo.Visible = false;
            }
            else if (cmbNgayDK.SelectedIndex == 1)
            {
                dateNgayDKTo.Visible = true;
            }
        }

        private void cmbNgayCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNgayCN.SelectedIndex == 0)
            {
                dateNgayCNTo.Visible = false;
            }
            else if (cmbNgayCN.SelectedIndex == 1)
            {
                dateNgayCNTo.Visible = true;
            }
        }

        private void cmbThoiHanjHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbThoiHanjHD.SelectedIndex == 0)
            {
                dateThoiHanHDTo.Visible = false;
            }
            else if (cmbThoiHanjHD.SelectedIndex == 1)
            {
                dateThoiHanHDTo.Visible = true;
            }
        }

        private void cmbNgayNhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNgayNhap.SelectedIndex == 0)
            {
                dateNgayNhapTo.Visible = false;
            }
            else if (cmbNgayNhap.SelectedIndex == 1)
            {
                dateNgayNhapTo.Visible = true;
            }
        }

        private void btnCall_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
        }
        private void ClickToCall(string Phone)
        {
            var line = db.voipLineConfigs.Where(p => p.MaNV == Common.StaffID);

            if (line.Count() == 0)
            {
                DialogBox.Infomation("Tài khoản này chưa được cấp Line. Nên không thể thực hiện cuộc gọi!");
                return;
            }
            if (Phone == "")
            {
                DialogBox.Infomation("Khách hàng này chưa có số điện thoại, vui lòng kiểm tra lại!");
                return;
            }
            var pre = line.First().PreCall ?? "";
            var obj = line.First().LineNumber;
            //UCMClickToCall.ClickToCall((Asterisk.NET.Manager.ManagerConnection)astUCM.ManagerConnection, obj, string.Format("{0}{1}", pre, Phone));
            var objConfig = db.voipServerConfigs.FirstOrDefault();
            //PopupUCMThienVM.PopupUCMThienVM m = new PopupUCMThienVM.PopupUCMThienVM();
            //m.username = objConfig.UserName;
            //m.host = objConfig.Host;
            //m.port = 5038;
            //m.password = objConfig.Pass;
            //objConfig.Host, 5038, objConfig.UserName, objConfig.Pass

            PopupUCMThienVM.PopupUCMThienVM m = new PopupUCMThienVM.PopupUCMThienVM(objConfig.Host, 5038, objConfig.UserName, objConfig.Pass);
            if (m.xacthuc(objConfig.KeyConnect))
            {
                m.Clicktocall(string.Format("{0}{1}", pre, Phone), obj);
            }
            else
            {
                MessageBox.Show("Key kết nối tổng đài sai. Vui lòng kiểm tra lại");
            }

        }
        void KhaoSat(string sdt, string sdthidden, bool? isNLH, int? MaKH)
        {
            var db = new MasterDataContext();
            var objConfig = db.voipServerConfigs.FirstOrDefault();
            var objLine = db.voipLineConfigs.FirstOrDefault(p => p.MaNV == Common.StaffID);
            if (objLine == null)
            {
                DialogBox.Infomation("vui lòng cài đặt line để gọi đi, xin cảm ơn!");
                return;
            }
            var frC = new BEE.VOIPSETUP.Call.frmCall();
            frC.SDT = sdt;
            frC.isNLH = isNLH;
            frC.sdtHiden = sdthidden;
            frC.MaKH = MaKH;
            frC.MaBC = (int?)grvBan.GetFocusedRowCellValue("MaBC");
            // frC.ModuleID = 0;
            frC.line = objLine.LineNumber;
            frC.NhanVienTN = Common.StaffName;
            frC.LoaiCG = 0;
            frC.ShowDialog();
        }

        private void btnCall_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var makh = (int?)grvBan.GetFocusedRowCellValue("MaKH");
            var namecol = grvBan.FocusedColumn.Name;
            string sdt = "";
            string sdthidden = "";
            var objkh = db.KhachHangs.SingleOrDefault(p => p.MaKH == makh);
            objkh.ExEndCall = db.voipLineConfigs.SingleOrDefault(p => p.MaNV == Common.StaffID).LineNumber;
            db.SubmitChanges();
            switch (namecol)
            {
                case "colDienThoai":
                    sdt = objkh.DiDong;
                    sdthidden = grvBan.GetFocusedRowCellValue("DienThoai").ToString();
                    break;
                case "colDienThoai2":
                    sdt = objkh.DiDong2;
                    sdthidden = grvBan.GetFocusedRowCellValue("DiDong2").ToString();
                    break;
                case "colDienThoai3":
                    sdt = objkh.DiDong3;
                    sdthidden = grvBan.GetFocusedRowCellValue("DiDong3").ToString();
                    break;
                case "colDienThoai4":
                    sdt = objkh.DiDong4;
                    sdthidden = grvBan.GetFocusedRowCellValue("DiDong4").ToString();
                    break;
            }
            ClickToCall(sdt);
            KhaoSat(sdt, sdthidden, false, makh);
            try
            {

                //var phone = (sender as ButtonEdit).Text ?? "";
                //if (phone.Trim() == "") return;
                //ClickToCall(phone);
                //var line = db.voipLineConfigs.Where(p => p.MaNV == Common.StaffID);
                // var obj = line.First().LineNumber;
                // StartConnection(obj, line.First().PreCall + phone);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCallNLH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var makh = (int?)gridAvatar.GetFocusedRowCellValue("MaNDD");
            var namecol = gridAvatar.FocusedColumn.Name;
            string sdt = "";
            string sdthidden = "";
            var objkh = db.NguoiDaiDiens.SingleOrDefault(p => p.MaNDD == makh);
            switch (namecol)
            {
                case "colDTCD":
                    sdt = objkh.DTCD;
                    sdthidden = gridAvatar.GetFocusedRowCellValue("DTCD").ToString();
                    break;
                case "colDTĐ":
                    sdt = objkh.DTDD;
                    sdthidden = gridAvatar.GetFocusedRowCellValue("DTDD").ToString();
                    break;

            }

            if (sdt.Trim() == "") return;
            ClickToCall(sdt);
            KhaoSat(sdt, sdthidden, true, objkh.MaKH);
            try
            {

                //var phone = (sender as ButtonEdit).Text ?? "";
                //if (phone.Trim() == "") return;
                //ClickToCall(phone);
                //var line = db.voipLineConfigs.Where(p => p.MaNV == Common.StaffID);
                // var obj = line.First().LineNumber;
                // StartConnection(obj, line.First().PreCall + phone);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
