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
using System.Threading;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;

namespace BEE.HoatDong.MGL.Ban
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();
        mglbcBanChoThue objGD { get; set; }

        public ctlManager()
        {
            InitializeComponent();

            paging1.PageRows = 10;
            paging1.PageClicked += new CongCu.Paging.PageClick(this.ToolStripButtonClick);
            paging1.CmbClicked += new CongCu.Paging.CmbClick(this.cmbPageRows_SelectedIndexChanged);
        }

        #region ToolStripButtonClick
        private void ToolStripButtonClick(object sender, CongCu.PageClickEventHandler e)
        {
            //MessageBox.Show("Page " + e.SelectedPage.ToString());
            if (itemTrangThaicmb.EditValue != "Chọn trạng thái" && itemTrangThaicmb.EditValue != "")
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
            if (itemTrangThaicmb.EditValue != "Chọn trạng thái" && itemTrangThaicmb.EditValue != "")
            {
                Ban_Load();
                phanquyen();
                grvBan.BestFitColumns();
            }
        }
        #endregion

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 174;
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
            itemLocTinh.Enabled = false;
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
                            itemLocTinh.Enabled = true;
                            break;
                        case 86://cấu hình thời gian
                            itemCauHinhTG.Enabled = true;
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

        private void Ban_Load()
        {
            var tuNgay = (DateTime?)itemTuNgay.EditValue ?? DateTime.Now;
            var denNgay = (DateTime?)itemDenNgay.EditValue ?? DateTime.Now;
            //var MaTT = (byte?)itemTrangThai.EditValue ?? Convert.ToByte(100);
            var strMaTT = (itemTrangThaicmb.EditValue ?? "").ToString().Replace(" ", "");
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

                        #region if (obj.DienThoai == false)
                        if (obj.DienThoai == false)
                        {
                            var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    p.MaTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                    DiDong4 = Common.Right(p.DiDong4, 3),
                                });

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                            var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                    DienThoai = Common.Right(p.DienThoai, 3),
                                    DiDong2 = Common.Right(p.DiDong2, 3),
                                    DiDong3 = Common.Right(p.DiDong3, 3),
                                    DiDong4 = Common.Right(p.DiDong4, 3)
                                });


                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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

                            var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                            var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, Common.DepartmentID, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                 .Select(p => new
                                 {
                                     p.STT,
                                     p.MaBC,
                                     p.TenTT,
                                     p.MauNen,
                                     VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                     p.MaTT,
                                     p.SoGiay,
                                     AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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

                                 });

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, Common.DepartmentID, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                            var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, Common.DepartmentID, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MauNen,
                                    AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                    DienThoai = Common.Right(p.DienThoai, 3),
                                    DiDong2 = Common.Right(p.DiDong2, 3),
                                    DiDong3 = Common.Right(p.DiDong3, 3),
                                    DiDong4 = Common.Right(p.DiDong4, 3)

                                });

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, Common.DepartmentID, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                            var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, Common.DepartmentID, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, Common.DepartmentID, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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

                        #region if (obj.DienThoai == false)
                        if (obj.DienThoai == false)
                        {
                            var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                               .Select(p => new
                               {
                                   p.STT,
                                   p.MaBC,
                                   p.TenTT,
                                   VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                   p.MauNen,
                                   AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                   DienThoai = Common.Right(p.DienThoai, 3),
                                   DiDong2 = Common.Right(p.DiDong2, 3),
                                   DiDong3 = Common.Right(p.DiDong3, 3),
                                   DiDong4 = Common.Right(p.DiDong4, 3)

                               });//.Where(p => p.IsBan.GetValueOrDefault() == true);

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                            var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                               .Select(p => new
                               {
                                   p.STT,
                                   p.MaBC,
                                   p.TenTT,
                                   VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                   p.MauNen,
                                   p.MaTT,
                                   AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                   DiDong2 = Common.Right(p.DiDong2, 3),
                                   DiDong3 = Common.Right(p.DiDong3, 3),
                                   DiDong4 = Common.Right(p.DiDong4, 3)
                               });

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                            var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                               .Select(p => new
                               {
                                   p.STT,
                                   p.MaBC,
                                   p.MauNen,
                                   p.TenTT,
                                   VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                   p.MaTT,
                                   AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                   p.DiDong2,p.DiDong3,
                                   p.DiDong4
                               }).ToList();

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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

                        #region if (obj.DienThoai == false)
                        if (obj.DienThoai == false)
                        {
                            var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MauNen,
                                    AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                    DienThoai = Common.Right(p.DienThoai, 3),
                                     DiDong2 = Common.Right(p.DiDong2, 3),
                                    DiDong3 = Common.Right(p.DiDong3, 3),
                                    DiDong4 = Common.Right(p.DiDong4, 3)
                                });

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                            var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MauNen,
                                    p.MaTT,
                                    AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                    DiDong2 = Common.Right(p.DiDong2, 3),
                                    DiDong3 = Common.Right(p.DiDong3, 3),
                                    DiDong4 = Common.Right(p.DiDong4, 3)
                                });

                            try
                            {
                                var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                            var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                 .Select(p => new
                                 {
                                     p.STT,
                                     p.MaBC,
                                     p.MauNen,
                                     p.TenTT,
                                     VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                     p.MaTT,
                                     AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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

        private void Ban_EditV2()
        {
            bool ck = ((int)grvBan.GetFocusedRowCellValue("MaNVQL") == Common.StaffID) || ((int)grvBan.GetFocusedRowCellValue("MaNVKD") == Common.StaffID || GetAccessData() == 1);
            frmEdit frm = new frmEdit();
            frm.AllowSave = false;
            frm.DislayContac = ck;
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
                var objBC = db.mglbcBanChoThues.Single(p => p.MaBC == (int?)grvBan.GetRowCellValue(i, "MaBC"));
                objBC.MaTTD = 1;
            }
            db.SubmitChanges();
            DialogBox.Infomation("Đã chuyển sang danh sách chờ xóa");
            Ban_Load();
        }

        private void LocSanPham()
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


            var objNC = db.mglBCSanPhams.Where(p => p.MaSP == objGD.MaBC).Select(p => p.mglBCCongViec.MaCoHoiMT);
            var obj = db.mglmtPhanQuyens.Single(p => p.MaNV == Common.StaffID);
            if (obj.DienThoai == false)
            {
                #region điện thoại 3 số cuối
                var objLoc = (from mt in db.mglmtMuaThues
                              join nvkd in db.NhanViens on mt.MaNVKD equals nvkd.MaNV
                              join da in db.DuAns on mt.MaDA equals da.MaDA into du
                              from da in du.DefaultIfEmpty()
                              where (mt.IsMua == objGD.IsBan & mt.MaTT < 3)
                              orderby mt.NgayDK descending
                              select new
                              {
                                  #region mt
                                  mt.MaTT,
                                  mt.mglmtTrangThai.MauNen,
                                  mt.mglmtTrangThai.TenTT,
                                  mt.MaMT,
                                  mt.SoDK,
                                  ThuongHieu = mt.KhachHang.IsPersonal == true ? mt.KhachHang.MoHinh : mt.KhachHang.ThuongHieu,
                                  mt.NgayCN,
                                  //SoGiay= SqlMethods.DateDiffSecond(DateTime.Now,  SqlMethods. (mt.ThoiHan, mt.NgayDK),
                                  //mt.SoGiay,
                                  mt.MaNguon,
                                  mt.MaCD,
                                  mt.MaNVKT,
                                  mt.MaNVKD,
                                  mt.MaKH,
                                  HoTenNVKD = nvkd.HoTen,
                                  mt.IsMua,
                                  TenNC = mt.IsMua == true ? "Cần mua" : "Cần thuê",
                                  da.TenDA,
                                  mt.GhiChu,
                                  mt.KhuVuc,
                                  mt.PhiMG,
                                  mt.mglmtMuDichMT.MucDich,
                                  mt.HuongCua,
                                  mt.HuongBC,
                                  mt.TienIch,
                                  mt.LoaiTien.TenLoaiTien,
                                  mt.BoiDam,
                                  mt.NgayNhap,
                                  PhongNgu = mt.PhNguTu,
                                  mt.Duong,
                                  //mt.SoTang,
                                  PVS = mt.PVSTu,
                                  //DienThoai = Common.Right(mt.KhachHang.DienThoaiCT, 3),
                                  #endregion
                                  mt.IsCanGoc,
                                  mt.IsThangMay,
                                  mt.IsTangHam,
                                  mt.NgayDK,
                                  mt.ThoiHan,
                                  mt.TyLeHH,
                                  mt.mglmtHuongs,
                                  mt.mglmtHuongBCs,
                                  mt.mglmtLoaiDuongs,
                                  mt.mglmtPhapLies,
                                  mt.mglmtHuyens,
                                  //mt.MaDuong,
                                  HoTenKH = (mt.MaNVKD == Common.StaffID || mt.MaNVKT == Common.StaffID || GetAccessData() == 1) == true ? mt.KhachHang.HoKH + " " + mt.KhachHang.TenKH : "",
                                  DienThoai = Common.Right((mt.MaNVKD == Common.StaffID || mt.MaNVKT == Common.StaffID || GetAccessData() == 1) ? (mt.KhachHang.DiDong == null ? mt.KhachHang.DienThoaiCT : mt.KhachHang.DiDong) : "", 3),
                                  //  KhoangGia = "<= " + string.Format("{0:#,0.##)", p.GiaDen),
                                  //DienTich = ">= " + string.Format("{0:#,0.##}", p.DienTichTu),
                                  //PhongNgu = ">= " + string.Format("{0}", p.mthNguTu),
                                  //PVS = ">= " + string.Format("{0}", mt.PVSTu),
                                  //KhoangGia=mt.GiaDen,
                                  //DienTich=mt.DienTichTu,
                                  //PhongNgu=mt.PhNguTu,
                                  //PVS= mt.PVSTu,
                                  //   HoTenNV = (mt.MaNVKD == Library.Common.StaffID || mt.MaNVKT == Library.Common.StaffID || GetAccessData() == 1) ? mt.NhanVien.HoTen : "",
                                  HoTenNV = mt.NhanVien.HoTen,
                                  mt.IsChinhChu,
                                  mt.MaDA,
                                  mt.DuongRongTu,
                                  mt.DienTichTu,
                                  GiaDen = mt.GiaDen * mt.LoaiTien.TyGia,
                                  mt.IsDeOto,
                                  mt.IsSanThuong,
                                  mt.mglmtDuongs,
                                  mt.IsThuongLuong,
                                  mt.LauDen,
                                  mt.LauTu,
                                  mt.MaLBDS,
                                  mt.PhNguTu,
                                  mt.PVSTu,
                                  mt.MatTienTu,
                                  mt.MaTinh,
                                  //mt.Duong.TenDuong,
                                  mt.Tinh.TenTinh
                              }).ToList().Where(mt => objNC.Contains(mt.MaMT) == false);
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
                if (objPQ.CanGoc.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.IsCanGoc == objGD.IsCanGoc).ToList();
                if (objPQ.DienTich.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.DienTichTu <= objGD.DienTich).ToList();
                if (objPQ.DuAn.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.MaDA == objGD.MaDA).ToList();
                if (objPQ.DuongRong.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.DuongRongTu <= objGD.DuongRong).ToList();
                if (objPQ.HuongCua.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.mglmtHuongs.Count == 0 || p.mglmtHuongs.Select(h => h.MaHuong).Contains(objGD.MaHuong)).ToList();
                if (objPQ.HuongBanCong.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.mglmtHuongBCs.Count == 0 || p.mglmtHuongBCs.Select(h => h.MaHuong).Contains(objGD.HuongBanCong)).ToList();
                try
                {
                    if (objPQ.KhoangGia.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.GiaDen >= (objGD.ThanhTien * objGD.LoaiTien.TyGia)).ToList();
                }
                catch { }
                if (objPQ.LoaiBDS.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.MaLBDS == objGD.MaLBDS).ToList();
                if (objPQ.LoaiDuong.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.mglmtLoaiDuongs.Count == 0 || p.mglmtLoaiDuongs.Select(ld => ld.MaLD).Contains(objGD.MaLD)).ToList();
                if (objPQ.OtoVao.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.IsDeOto == objGD.DauOto).ToList();
                if (objPQ.PhapLy.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.mglmtPhapLies.Count == 0 || p.mglmtPhapLies.Select(pl => pl.MaPL).Contains(objGD.MaPL)).ToList();
                if (objPQ.PhongNgu.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.PhNguTu <= objGD.PhongNgu).ToList();
                if (objPQ.QuanHuyen.GetValueOrDefault())
                    objLoc = objLoc = objLoc.Where(p => p.mglmtHuyens.Count == 0 || p.mglmtHuyens.Select(h => h.MaHuyen).Contains(objGD.MaHuyen)).ToList();
                if (objPQ.SoTang.GetValueOrDefault())
                    objLoc.Where(p => p.LauTu <= objGD.SoTang & (p.LauDen >= objGD.SoTang | p.LauDen == 0));
                if (objPQ.ThangMay.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.IsThangMay == objGD.IsThangMay).ToList();
                if (objPQ.TangHam.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.IsTangHam == objGD.TangHam).ToList();

                if (objPQ.MatTien.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.MatTienTu == objGD.NgangXD).ToList();
                if (objPQ.TenDuong.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.mglmtDuongs.Count == 0 || p.mglmtDuongs.Select(d => d.MaDuong).Contains(objGD.MaDuong)).ToList();
                if (objPQ.Tinh.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.MaTinh == objGD.MaTinh).ToList();
                gcMuaThue.DataSource = objLoc;
                #endregion
            }
            if (obj.DienThoai3Dau == false)
            {
                #region điện thoại 3 số cuối và 3 đầu
                var objLoc = (from mt in db.mglmtMuaThues
                              join nvkd in db.NhanViens on mt.MaNVKD equals nvkd.MaNV
                              join da in db.DuAns on mt.MaDA equals da.MaDA into du
                              from da in du.DefaultIfEmpty()
                              where (mt.IsMua == objGD.IsBan & mt.MaTT < 3)
                              orderby mt.NgayDK descending
                              select new
                              {
                                  #region mt
                                  mt.MaTT,
                                  mt.mglmtTrangThai.MauNen,
                                  mt.mglmtTrangThai.TenTT,
                                  mt.MaMT,
                                  mt.SoDK,
                                  ThuongHieu = mt.KhachHang.IsPersonal == true ? mt.KhachHang.MoHinh : mt.KhachHang.ThuongHieu,
                                  mt.NgayCN,
                                  //SoGiay= SqlMethods.DateDiffSecond(DateTime.Now,  SqlMethods. (mt.ThoiHan, mt.NgayDK),
                                  //mt.SoGiay,
                                  mt.MaNguon,
                                  mt.MaCD,
                                  mt.MaNVKT,
                                  mt.MaNVKD,
                                  mt.MaKH,
                                  HoTenNVKD = nvkd.HoTen,
                                  mt.IsMua,
                                  TenNC = mt.IsMua == true ? "Cần mua" : "Cần thuê",
                                  da.TenDA,
                                  mt.GhiChu,
                                  mt.KhuVuc,
                                  mt.PhiMG,
                                  mt.mglmtMuDichMT.MucDich,
                                  mt.HuongCua,
                                  mt.HuongBC,
                                  mt.TienIch,
                                  mt.LoaiTien.TenLoaiTien,
                                  mt.BoiDam,
                                  mt.NgayNhap,
                                  PhongNgu = mt.PhNguTu,
                                  mt.Duong,
                                  //mt.SoTang,
                                  PVS = mt.PVSTu,
                                  //DienThoai = Common.Right(mt.KhachHang.DienThoaiCT, 3),
                                  #endregion
                                  mt.IsCanGoc,
                                  mt.IsThangMay,
                                  mt.IsTangHam,
                                  mt.NgayDK,
                                  mt.ThoiHan,
                                  mt.TyLeHH,
                                  mt.mglmtHuongs,
                                  mt.mglmtHuongBCs,
                                  mt.mglmtLoaiDuongs,
                                  mt.mglmtPhapLies,
                                  mt.mglmtHuyens,
                                  //mt.MaDuong,
                                  HoTenKH = (mt.MaNVKD == Common.StaffID || mt.MaNVKT == Common.StaffID || GetAccessData() == 1) == true ? mt.KhachHang.HoKH + " " + mt.KhachHang.TenKH : "",
                                  DienThoai = Common.Right1((mt.MaNVKD == Common.StaffID || mt.MaNVKT == Common.StaffID || GetAccessData() == 1) ? (mt.KhachHang.DiDong == null ? mt.KhachHang.DienThoaiCT : mt.KhachHang.DiDong) : "", 3),
                                  //  KhoangGia = "<= " + string.Format("{0:#,0.##)", p.GiaDen),
                                  //DienTich = ">= " + string.Format("{0:#,0.##}", p.DienTichTu),
                                  //PhongNgu = ">= " + string.Format("{0}", p.mthNguTu),
                                  //PVS = ">= " + string.Format("{0}", mt.PVSTu),
                                  //KhoangGia=mt.GiaDen,
                                  //DienTich=mt.DienTichTu,
                                  //PhongNgu=mt.PhNguTu,
                                  //PVS= mt.PVSTu,
                                  //   HoTenNV = (mt.MaNVKD == Library.Common.StaffID || mt.MaNVKT == Library.Common.StaffID || GetAccessData() == 1) ? mt.NhanVien.HoTen : "",
                                  HoTenNV = mt.NhanVien.HoTen,
                                  mt.IsChinhChu,
                                  mt.MaDA,
                                  mt.DuongRongTu,
                                  mt.DienTichTu,
                                  GiaDen = mt.GiaDen * mt.LoaiTien.TyGia,
                                  mt.IsDeOto,
                                  mt.IsSanThuong,
                                  mt.mglmtDuongs,
                                  mt.IsThuongLuong,
                                  mt.LauDen,
                                  mt.LauTu,
                                  mt.MaLBDS,
                                  mt.PhNguTu,
                                  mt.PVSTu,
                                  mt.MatTienTu,
                                  mt.MaTinh,
                                  //mt.Duong.TenDuong,
                                  mt.Tinh.TenTinh
                              }).ToList().Where(mt => objNC.Contains(mt.MaMT) == false);

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
                if (objPQ.CanGoc.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.IsCanGoc == objGD.IsCanGoc).ToList();
                if (objPQ.DienTich.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.DienTichTu <= objGD.DienTich).ToList();
                if (objPQ.DuAn.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.MaDA == objGD.MaDA).ToList();
                if (objPQ.DuongRong.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.DuongRongTu <= objGD.DuongRong).ToList();
                if (objPQ.HuongCua.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.mglmtHuongs.Count == 0 || p.mglmtHuongs.Select(h => h.MaHuong).Contains(objGD.MaHuong)).ToList();
                if (objPQ.HuongBanCong.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.mglmtHuongBCs.Count == 0 || p.mglmtHuongBCs.Select(h => h.MaHuong).Contains(objGD.HuongBanCong)).ToList();
                try
                {
                    if (objPQ.KhoangGia.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.GiaDen >= (objGD.ThanhTien * objGD.LoaiTien.TyGia)).ToList();
                }
                catch { }
                if (objPQ.LoaiBDS.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.MaLBDS == objGD.MaLBDS).ToList();
                if (objPQ.LoaiDuong.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.mglmtLoaiDuongs.Count == 0 || p.mglmtLoaiDuongs.Select(ld => ld.MaLD).Contains(objGD.MaLD)).ToList();
                if (objPQ.OtoVao.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.IsDeOto == objGD.DauOto).ToList();
                if (objPQ.PhapLy.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.mglmtPhapLies.Count == 0 || p.mglmtPhapLies.Select(pl => pl.MaPL).Contains(objGD.MaPL)).ToList();
                if (objPQ.PhongNgu.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.PhNguTu <= objGD.PhongNgu).ToList();
                if (objPQ.QuanHuyen.GetValueOrDefault())
                    objLoc = objLoc = objLoc.Where(p => p.mglmtHuyens.Count == 0 || p.mglmtHuyens.Select(h => h.MaHuyen).Contains(objGD.MaHuyen)).ToList();
                if (objPQ.SoTang.GetValueOrDefault())
                    objLoc.Where(p => p.LauTu <= objGD.SoTang & (p.LauDen >= objGD.SoTang | p.LauDen == 0));
                if (objPQ.ThangMay.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.IsThangMay == objGD.IsThangMay).ToList();
                if (objPQ.TangHam.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.IsTangHam == objGD.TangHam).ToList();

                if (objPQ.MatTien.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.MatTienTu == objGD.NgangXD).ToList();
                if (objPQ.TenDuong.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.mglmtDuongs.Count == 0 || p.mglmtDuongs.Select(d => d.MaDuong).Contains(objGD.MaDuong)).ToList();
                if (objPQ.Tinh.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.MaTinh == objGD.MaTinh).ToList();
                gcMuaThue.DataSource = objLoc;
                #endregion
            }
            else
            {
                #region còn lại
                var objLoc = (from mt in db.mglmtMuaThues
                              join nvkd in db.NhanViens on mt.MaNVKD equals nvkd.MaNV
                              join da in db.DuAns on mt.MaDA equals da.MaDA into du
                              from da in du.DefaultIfEmpty()
                              where (mt.IsMua == objGD.IsBan & mt.MaTT < 3)
                              orderby mt.NgayDK descending
                              select new
                              {
                                  #region mt
                                  mt.MaTT,
                                  mt.mglmtTrangThai.MauNen,
                                  mt.mglmtTrangThai.TenTT,
                                  mt.MaMT,
                                  mt.SoDK,
                                  ThuongHieu = mt.KhachHang.IsPersonal == true ? mt.KhachHang.MoHinh : mt.KhachHang.ThuongHieu,
                                  mt.NgayCN,
                                  //SoGiay= SqlMethods.DateDiffSecond(DateTime.Now,  SqlMethods. (mt.ThoiHan, mt.NgayDK),
                                  //mt.SoGiay,
                                  mt.MaNguon,
                                  mt.MaCD,
                                  mt.MaNVKT,
                                  mt.MaNVKD,
                                  mt.MaKH,
                                  HoTenNVKD = nvkd.HoTen,
                                  mt.IsMua,
                                  TenNC = mt.IsMua == true ? "Cần mua" : "Cần thuê",
                                  da.TenDA,
                                  mt.GhiChu,
                                  mt.KhuVuc,
                                  mt.PhiMG,
                                  mt.mglmtMuDichMT.MucDich,
                                  mt.HuongCua,
                                  mt.HuongBC,
                                  mt.TienIch,
                                  mt.LoaiTien.TenLoaiTien,
                                  mt.BoiDam,
                                  mt.NgayNhap,
                                  PhongNgu = mt.PhNguTu,
                                  mt.Duong,
                                  //mt.SoTang,
                                  PVS = mt.PVSTu,
                                  //DienThoai = Common.Right(mt.KhachHang.DienThoaiCT, 3),
                                  #endregion
                                  mt.IsCanGoc,
                                  mt.IsThangMay,
                                  mt.IsTangHam,
                                  mt.NgayDK,
                                  mt.ThoiHan,
                                  mt.TyLeHH,
                                  mt.mglmtHuongs,
                                  mt.mglmtHuongBCs,
                                  mt.mglmtLoaiDuongs,
                                  mt.mglmtPhapLies,
                                  mt.mglmtHuyens,
                                  //mt.MaDuong,
                                  HoTenKH = (mt.MaNVKD == Common.StaffID || mt.MaNVKT == Common.StaffID || GetAccessData() == 1) == true ? mt.KhachHang.HoKH + " " + mt.KhachHang.TenKH : "",
                                  DienThoai = (mt.MaNVKD == Common.StaffID || mt.MaNVKT == Common.StaffID || GetAccessData() == 1) ? (mt.KhachHang.DiDong == null ? mt.KhachHang.DienThoaiCT : mt.KhachHang.DiDong) : "",
                                  //DienTich = ">= " + string.Format("{0:#,0.##}", p.DienTichTu),
                                  //PhongNgu = ">= " + string.Format("{0}", p.mthNguTu),
                                  //PVS = ">= " + string.Format("{0}", mt.PVSTu),
                                  //KhoangGia=mt.GiaDen,
                                  //DienTich=mt.DienTichTu,
                                  //PhongNgu=mt.PhNguTu,
                                  //PVS= mt.PVSTu,
                                  //   HoTenNV = (mt.MaNVKD == Library.Common.StaffID || mt.MaNVKT == Library.Common.StaffID || GetAccessData() == 1) ? mt.NhanVien.HoTen : "",
                                  HoTenNV = mt.NhanVien.HoTen,
                                  mt.IsChinhChu,
                                  mt.MaDA,
                                  mt.DuongRongTu,
                                  mt.DienTichTu,
                                  GiaDen = mt.GiaDen * mt.LoaiTien.TyGia,
                                  mt.IsDeOto,
                                  mt.IsSanThuong,
                                  mt.mglmtDuongs,
                                  mt.IsThuongLuong,
                                  mt.LauDen,
                                  mt.LauTu,
                                  mt.MaLBDS,
                                  mt.PhNguTu,
                                  mt.PVSTu,
                                  mt.MatTienTu,
                                  mt.MaTinh,
                                  //mt.Duong.TenDuong,
                                  mt.Tinh.TenTinh
                              }).ToList().Where(mt => objNC.Contains(mt.MaMT) == false);

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
                if (objPQ.CanGoc.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.IsCanGoc == objGD.IsCanGoc).ToList();
                if (objPQ.DienTich.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.DienTichTu <= objGD.DienTich).ToList();
                if (objPQ.DuAn.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.MaDA == objGD.MaDA).ToList();
                if (objPQ.DuongRong.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.DuongRongTu <= objGD.DuongRong).ToList();
                if (objPQ.HuongCua.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.mglmtHuongs.Count == 0 || p.mglmtHuongs.Select(h => h.MaHuong).Contains(objGD.MaHuong)).ToList();
                if (objPQ.HuongBanCong.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.mglmtHuongBCs.Count == 0 || p.mglmtHuongBCs.Select(h => h.MaHuong).Contains(objGD.HuongBanCong)).ToList();
                try
                {
                    if (objPQ.KhoangGia.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.GiaDen >= (objGD.ThanhTien * objGD.LoaiTien.TyGia)).ToList();
                }
                catch { }
                if (objPQ.LoaiBDS.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.MaLBDS == objGD.MaLBDS).ToList();
                if (objPQ.LoaiDuong.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.mglmtLoaiDuongs.Count == 0 || p.mglmtLoaiDuongs.Select(ld => ld.MaLD).Contains(objGD.MaLD)).ToList();
                if (objPQ.OtoVao.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.IsDeOto == objGD.DauOto).ToList();
                if (objPQ.PhapLy.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.mglmtPhapLies.Count == 0 || p.mglmtPhapLies.Select(pl => pl.MaPL).Contains(objGD.MaPL)).ToList();
                if (objPQ.PhongNgu.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.PhNguTu <= objGD.PhongNgu).ToList();
                if (objPQ.QuanHuyen.GetValueOrDefault())
                    objLoc = objLoc = objLoc.Where(p => p.mglmtHuyens.Count == 0 || p.mglmtHuyens.Select(h => h.MaHuyen).Contains(objGD.MaHuyen)).ToList();
                if (objPQ.SoTang.GetValueOrDefault())
                    objLoc.Where(p => p.LauTu <= objGD.SoTang & (p.LauDen >= objGD.SoTang | p.LauDen == 0));
                if (objPQ.ThangMay.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.IsThangMay == objGD.IsThangMay).ToList();
                if (objPQ.TangHam.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.IsTangHam == objGD.TangHam).ToList();

                if (objPQ.MatTien.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.MatTienTu == objGD.NgangXD).ToList();
                if (objPQ.TenDuong.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.mglmtDuongs.Count == 0 || p.mglmtDuongs.Select(d => d.MaDuong).Contains(objGD.MaDuong)).ToList();
                if (objPQ.Tinh.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.MaTinh == objGD.MaTinh).ToList();
                gcMuaThue.DataSource = objLoc;
                #endregion
            }
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
                gridControlAvatar.DataSource = null;
                return;
            }

            switch (tabMain.SelectedTabPageIndex)
            {
                case 0:
                    #region Nhu cau
                    if ((byte?)grvBan.GetFocusedRowCellValue("MaTT") != 0 && (byte?)grvBan.GetFocusedRowCellValue("MaTT") != 2)
                    {
                        gcMuaThue.DataSource = null;
                        return;
                    }
                    objGD = db.mglbcBanChoThues.Single(p => p.MaBC == maBC);
                    LocSanPham();
                    #endregion
                    break;
                case 1:
                    #region Lich lam viec
                    var listNhatKy = db.mglbcNhatKyXuLies.Where(p => p.MaBC == maBC)
                        .OrderByDescending(p => p.NgayXL)
                        .AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.ID,
                            p.NgayXL,
                            p.TieuDe,
                            p.NoiDung,
                            TenPT = p.MaPT == null ? "" : p.PhuongThucXuLy.TenPT,
                            p.MaNVG,
                            HoTenNVG = p.NhanVien.HoTen,
                            HoTenNVN = p.NhanVien1.HoTen,
                            p.KetQua
                        }).ToList();
                    gcNhatKy.DataSource = listNhatKy;
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
                        KhachHang = (p.MaNV == Common.StaffID || p.MaNVBC == Common.StaffID || p.MaNVMT == Common.StaffID || Common.PerID == 1) ?
                       (p.mglmtMuaThue.KhachHang.IsPersonal == true ? p.mglmtMuaThue.KhachHang.HoKH + " " + p.mglmtMuaThue.KhachHang.TenKH : p.mglmtMuaThue.KhachHang.TenCongTy) : "",
                        DiDong = (p.MaNV == Common.StaffID || p.MaNVBC == Common.StaffID || p.MaNVMT == Common.StaffID || Common.PerID == 1) ? p.mglmtMuaThue.KhachHang.DiDong : "",
                        p.mglmtMuaThue.KhachHang.DCLL,
                        p.mglmtMuaThue.SoDK,
                        p.mglmtMuaThue.NgayDK,
                        p.NhanVien.HoTen
                    });
                    break;
                case 4:
                    gcKhachXem.DataSource = db.mglBCSanPhams.Where(p => p.MaSP == maBC).Select(p => new
                    {
                        KhachHang = (p.mglBCCongViec.MaNVBC == Common.StaffID || p.mglBCCongViec.MaNVMT == Common.StaffID || p.mglBCCongViec.MaNV == Common.StaffID || Common.PerID == 1) ?
                        p.mglBCCongViec.mglmtMuaThue.KhachHang.IsPersonal == true ? p.mglBCCongViec.mglmtMuaThue.KhachHang.HoKH + " " + p.mglBCCongViec.mglmtMuaThue.KhachHang.TenKH : p.mglBCCongViec.mglmtMuaThue.KhachHang.TenCongTy : "",
                        DiDong = (p.mglBCCongViec.MaNVBC == Common.StaffID || p.mglBCCongViec.MaNVMT == Common.StaffID || p.mglBCCongViec.MaNV == Common.StaffID || Common.PerID == 1) ? p.mglBCCongViec.mglmtMuaThue.KhachHang.DiDong : "",
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
                    it.NguoiDaiDienCls o = new it.NguoiDaiDienCls();
                    gridControlAvatar.DataSource = o.Select((int)grvBan.GetFocusedRowCellValue("MaKH"));
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
                    else
                        gcGhiNhan.DataSource = null;
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
                        colLoaiBDS.VisibleIndex = stt[i].STT.Value;
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
                        colDienTichBan.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Số tầng bán/Cho thuê":
                        colSoTangBan.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Mặt tiền":
                        colMatTien.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Diện tích đất":
                        colDienTichDat.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Diện tích XD":
                        colDienDichXD.VisibleIndex = stt[i].STT.Value;
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
                        colThoiHanBGMB.VisibleIndex = stt[i].STT.Value;
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
                        colGiaBan.VisibleIndex = stt[i].STT.Value;
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
                        colTienTich.VisibleIndex = stt[i].STT.Value;
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
                        colVideo.VisibleIndex = stt[i].STT.Value;
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
                    colDienTichBan.Visible = false;
                if (obj.GiaTien == false)
                    colTongGia.Visible = false;
                if (obj.DacTrung == false)
                    colDacTrung.Visible = false;
                if (obj.TenKH == false)
                    colKhachHang.Visible = false;
                if (obj.DienThoaiAn == false)
                    colDienThoai.Visible = false;
                if (obj.ToaDo == false)
                    colKinhDo.Visible = false;
                if (obj.CanBoLV == false)
                    colNhanVienQL.Visible = false;
            }
            catch { }

        }

        void DoRongCot()
        {
            colSoNha.OptionsColumn.FixedWidth = true;
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            this.grvBan.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvBan_RowCellStyle);
            this.grvBan.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvBan_FocusedRowChanged);
            this.grvBan.DoubleClick += new System.EventHandler(this.grvBan_DoubleClick);

            List<ItemTrangThai> lst = new List<ItemTrangThai>();
            lookTrangThai.DataSource = lst;
            // itemTrangThai.EditValue = Convert.ToByte(100);
            chkTrangThai.DataSource = db.mglbcTrangThais;
            lookLoiaBDSCM.DataSource = lookLoaiBDS.DataSource = db.LoaiBDs;
            lookHuong.DataSource = db.PhuongHuongs;
            lookLoaiDuong.DataSource = db.LoaiDuongs;
            lookPhapLy.DataSource = db.PhapLies;
            lookCapDo.DataSource = db.mglCapDos;
            lookNguonMT.DataSource = lookNguon.DataSource = db.mglNguons;
            lookTinhTrangHD.DataSource = db.mglbcTrangThaiHDMGs;
            lookTinhTrang.DataSource = db.mglTinhTrangs;
            lookKHTC.DataSource = lookMaKHDT.DataSource = db.KhachHangs.Select(p => new { p.MaKH, Name = p.IsPersonal == true ? p.HoKH + " " + p.TenKH : p.TenCongTy });
            LoadPermission();
            chkTinh.DataSource = db.Tinhs;
            setFillter(db.mglbcLocTheoTinhs.FirstOrDefault().MaTinh);
            phanquyen();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(Convert.ToInt32(db.mglbcLocNgays.FirstOrDefault().MaBC));
            ThuTuCot();
            grvBan.BestFitColumns();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (itemTrangThaicmb.EditValue != "Chọn trạng thái" && itemTrangThaicmb.EditValue != "")
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
            frm.mucdich = true;
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
            Ban_EditV2();
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
            if (e.RowHandle < 0) return;
            else if (e.Column.FieldName == "TenTT")
            {
                try
                {
                    e.Appearance.BackColor = System.Drawing.Color.FromArgb((int)grvBan.GetRowCellValue(e.RowHandle, "MauNen"));
                }
                catch { }
            }
        }

        private void itemNC_GuiYeuCau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<int> listSP = new List<int>();
            var indexs = grvBan.GetSelectedRows();
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
            using (var frm = new BEE.HoatDong.MGL.GiaoDich.frmEdit())
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
            GiaoDich.frmEdit frm = new BEE.HoatDong.MGL.GiaoDich.frmEdit();
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

        private void itemTrangThai_EditValueChanged(object sender, EventArgs e)
        {
            byte? maTT = (byte?)itemTrangThaicmb.EditValue;
            if (maTT == 1)
            {
                itemMoBan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemNgungBan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                itemUp.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                itemGiaoDich.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                if (maTT == 2)
                    itemMoBan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                else
                    itemMoBan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                itemNgungBan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemUp.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemGiaoDich.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

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
                frm.MaMT = objMT.MaMT;
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
            if (grvMuaThue.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Bạn vui lòng chọn khách hàng để gửi Email!");
                return;
            }
            using (var tt = new MGL.ThongTinGui())
            {
                tt.ID = 1;
                tt.ShowDialog();
            }

            string noidung = "";
            noidung += "Kính gửi" + Environment.NewLine;
            noidung += "Công ty cổ phần HoaLand gửi Anh thông tin mặt bằng phù hợp với mô hình F88: " + Environment.NewLine;
            var indexs = grvMuaThue.GetSelectedRows();
            foreach (var i in indexs)
            {
                var objBC = db.mglmtMuaThues.Single(p => p.MaMT == (int)grvMuaThue.GetRowCellValue(i, "MaMT"));
                var objTT = db.mglThongTinGuis.Single(p => p.ID == 1);
                //noidung += "Số nhà: " + objBC.SoNha + ", Tên đường: " + objBC.Duong.TenDuong + ", Phường/Xã: " + objBC.Xa.TenXa + ", Quận/Huyện: " + objBC.Huyen.TenHuyen +
                //    ", Tỉnh Thành Phố: " + objBC.Tinh.TenTinh + " ";
                if (objTT.MatTien == true)
                    noidung += " - Mặt tiền " + string.Format("{0:#,0.##}", objBC.MatTienTu) + " - " + string.Format("{0:#,0.##}", objBC.MatTienDien) + Environment.NewLine;
                if (objTT.DienTich == true)
                    noidung += " - Diện tích " + string.Format("{0:#,0.##}", objBC.DienTichTu) + "m " + "Số tầng: " + objBC.TangTu + " - " + objBC.TangDen + Environment.NewLine;
                if (objTT.TongGia == true)
                    noidung += " - Tổng Giá " + string.Format("{0:#,0.##}", objBC.GiaDen) + Environment.NewLine;
                if (objTT.GhiChu == true)
                    noidung += "Ghi chú " + objBC.GhiChu + Environment.NewLine;

            }
            noidung += "Thân trọng cảm ơn!:";
            var objMT = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == (int)grvMuaThue.GetFocusedRowCellValue("MaMT"));
            using (var frm = new BEE.HoatDong.MGL.frmSend() { objKH = objMT.KhachHang })
            {
                frm.noidung = noidung;
                frm.ShowDialog();
            }
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

        private void itemTrangThaicmb_EditValueChanged(object sender, EventArgs e)
        {
            phanquyen();
        }

        private void itemLocTinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmLocTinh())
            {
                frm.MainForm = this;
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
                frm.MainForm = this;
                frm.ShowDialog();
            }
            //it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            //objKBC.Initialize(cmbKyBaoCao);
            //SetDate(Convert.ToInt32(db.mglbcLocNgays.FirstOrDefault().MaBC));
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
            if (gridAvatar.GetFocusedRowCellValue("MaNDD") != null)
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

        private void itemLinkYoutobe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemAnhbds_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void itemSapXepSN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (itemTrangThaicmb.EditValue != "Chọn trạng thái" && itemTrangThaicmb.EditValue != "")
            {
                var tuNgay = (DateTime?)itemTuNgay.EditValue ?? DateTime.Now;
                var denNgay = (DateTime?)itemDenNgay.EditValue ?? DateTime.Now;
                //var MaTT = (byte?)itemTrangThai.EditValue ?? Convert.ToByte(100);
                var strMaTT = (itemTrangThaicmb.EditValue ?? "").ToString().Replace(" ", "");
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

                            #region if (obj.DienThoai == false)
                            if (obj.DienThoai == false)
                            {
                                var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                    .Select(p => new
                                    {
                                        p.STT,
                                        p.MaBC,
                                        p.MauNen,
                                        p.TenTT,
                                        p.MaTT,
                                        VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                        AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                        DienThoai = Common.Right(p.DienThoai, 3)
                                    }).OrderBy(p => p.SoNhaaa).ToList();

                                try
                                {
                                    var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                                var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                    .Select(p => new
                                    {
                                        p.STT,
                                        p.MaBC,
                                        p.MauNen,
                                        p.TenTT,
                                        p.MaTT,
                                        VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                        AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                    var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                                var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim()).Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                    var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, -1, MaNV, true, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                                var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, Common.DepartmentID, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                    .Select(p => new
                                    {
                                        p.STT,
                                        p.MaBC,
                                        p.TenTT,
                                        p.MauNen,
                                        p.MaTT,
                                        VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                        AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                    var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, Common.DepartmentID, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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

                            #region  else if (obj.DienThoai3Dau == false)
                            else if (obj.DienThoai3Dau == false)
                            {
                                var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, Common.DepartmentID, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                    .Select(p => new
                                    {
                                        p.STT,
                                        p.MaBC,
                                        p.TenTT,
                                        p.MauNen,
                                        p.MaTT,
                                        VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                        AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                    var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, Common.DepartmentID, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                                var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, Common.DepartmentID, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim()).Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                    var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, Common.DepartmentID, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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

                            #region if (obj.DienThoai == false)
                            if (obj.DienThoai == false)
                            {
                                var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                    .Select(p => new
                                    {
                                        p.STT,
                                        p.MaBC,
                                        p.TenTT,
                                        p.MauNen,
                                        p.MaTT,
                                        VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                        AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                    var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                                var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                    .Select(p => new
                                    {
                                        p.STT,
                                        p.MaBC,
                                        p.TenTT,
                                        p.MauNen,
                                        p.MaTT,
                                        VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                        AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                    var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                                var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim()).Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                    var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, -1, -1, Common.GroupID, -1, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                                var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                    .Select(p => new
                                    {
                                        p.STT,
                                        p.MaBC,
                                        p.TenTT,
                                        p.MauNen,
                                        p.MaTT,
                                        VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                        AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                    var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                                var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim())
                                    .Select(p => new
                                    {
                                        p.STT,
                                        p.MaBC,
                                        p.TenTT,
                                        p.MauNen,
                                        p.MaTT,
                                        VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                        AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                    var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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
                                var data = db.mglbcBanChoThue_getByDate_Paging(tuNgay, denNgay, arrMaTT, true, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc, paging1.CurrentPage, paging1.PageRows, txtSearch.Text.Trim()).Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MaTT,
                                    AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                                    var dtc = db.mglbcBanChoThue_getByDate_Count(tuNgay, denNgay, arrMaTT, true, MaNV, MaNV, -1, -1, MaNV, false, matinh, lockhuvuc, txtSearch.Text.Trim()).ToList();
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

                    //for (int i = 0; i < grvBan.RowCount; i++)
                    //{
                    //    string phiMG = grvBan.GetRowCellValue(i, "TyLeMG") != null ?
                    //        string.Format("{0:#,0.##}%", grvBan.GetRowCellValue(i, "TyLeMG")) :
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

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {

        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            if (itemTrangThaicmb.EditValue != "Chọn trạng thái" && itemTrangThaicmb.EditValue != "")
            {
                paging1.CurrentPage = 1;

                Ban_Load();
                phanquyen();
                grvBan.BestFitColumns();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (itemTrangThaicmb.EditValue != "Chọn trạng thái" && itemTrangThaicmb.EditValue != "")
            {
                paging1.CurrentPage = 1;

                Ban_Load();
                phanquyen();
                grvBan.BestFitColumns();
            }
        }

        private void linkToaDo_Click(object sender, EventArgs e)
        {
            var toado = grvBan.GetFocusedRowCellValue("ToaDo");
            if (toado != "")
                System.Diagnostics.Process.Start("https://www.google.com/maps/place/" + toado);
        }

        private void btnCall_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                var phone = (sender as ButtonEdit).Text ?? "";
                if (phone.Trim() == "") return;
                var line = db.voipLineConfigs.Where(p => p.MaNV == Common.StaffID);
                var obj = line.First().LineNumber;
                StartConnection(obj, line.First().PreCall + phone);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Connection_Closed()
        {
            //Deactivate chat UI; show login UI. 
            //this.Invoke((Action)(() => ChatPanel.Visible = false));
            // this.Invoke((Action)(() => ButtonSend.Enabled = false));
            // this.Invoke((Action)(() => StatusText.Text = "You have been disconnected."));
            // this.Invoke((Action)(() => SignInPanel.Visible = true));
        }
        private IHubProxy HubProxy { get; set; }
        const string ServerURI = "http://noibovoip.quanlykinhdoanh.com.vn:2222";
        private HubConnection Connection { get; set; }
        private async void ConnectVOIP()
        {
            Connection = new HubConnection(ServerURI);
            Connection.Closed += Connection_Closed;
            HubProxy = Connection.CreateHubProxy("MyHub");
            //Handle incoming event from server: use Invoke to write to console from SignalR's thread
            HubProxy.On<string, string>("ClickCall", (line, sdt) =>
                this.Invoke((Action)(() =>
                    MessageBox.Show(line + sdt)
                ))
            );
            try
            {
                Connection.Start();
            }
            catch
            {
                //StatusText.Text = "Unable to connect to server: Start server before connecting clients.";
                //No connection: Don't enable Send button or show chat UI
                return;
            }

        }
        public async Task StartConnection(string line, string sdt)
        {
            var connection = new HubConnection("http://hoaland.quanlykinhdoanh.com.vn:3333");
            var hub = connection.CreateHubProxy("MyHub");
            await connection.Start();
            await hub.Invoke("Click2Call", line, sdt);
        }

    }

    public class ItemTrangThai
    {
        public byte? MaTT { get; set; }
        public string TenTT { get; set; }
        public int? MauNen { get; set; }
    }
}
