using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.HoatDong.MGL.Ban
{
    public partial class ctlManagerChoXoaCT : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();
        mglbcBanChoThue objGD { get; set; }

        public ctlManagerChoXoaCT()
        {
            InitializeComponent();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 21;
            DataTable tblAction = o.SelectBy();
            itemThem.Enabled = false;
            itemSua.Enabled = false;
            itemXoa.Enabled = false;
            subDuyet.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            itemThem.Enabled = true;
                            break;
                        case 2:
                            itemSua.Enabled = true;
                            break;
                        case 3:
                            itemXoa.Enabled = true;
                            break;
                        case 7:
                            subDuyet.Enabled = true;
                            break;
                    }
                }
            }
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 21);

            return o.SDB.SDBID;
        }

        private void Ban_Load()
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

                var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);

                switch (GetAccessData())
                {
                    case 1://Tat ca
                        #region Tat ca
                        if (obj.DienThoai == false)
                            gcBan.DataSource = db.mglbcBanChoThue_getByDateChoXoa(tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1, MaNV, true)
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    //VideoLink = p.VideoLink == "" ? p.VideoLink : BEE.HoatDong.Properties.Resources.calendar_date,
                                    p.MaTT,
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
                                    p.DonViThueCu,
                                    p.DonViDangThue,
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
                        else if (obj.DienThoai3Dau == false)
                            gcBan.DataSource = db.mglbcBanChoThue_getByDateChoXoa(tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1, MaNV, true)
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.MauNen,
                                    p.TenTT,
                                    p.MaTT,
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
                        else
                            gcBan.DataSource = db.mglbcBanChoThue_getByDateChoXoa(tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1, MaNV, true).ToList();
                        #endregion
                        break;
                    case 2://Theo phong ban 
                        #region phong ban
                        if (obj.DienThoai == false)
                            gcBan.DataSource = db.mglbcBanChoThue_getByDateChoXoa(tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false)
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    p.MauNen,
                                    p.MaTT,
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
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViThueCu,
                                    p.DonViDangThue,
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
                        else if (obj.DienThoai3Dau == false)
                            gcBan.DataSource = db.mglbcBanChoThue_getByDateChoXoa(tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false)
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    p.MauNen,
                                    p.MaTT,
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
                        else
                            db.mglbcBanChoThue_getByDateChoXoa(tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false).ToList();
                        #endregion
                        break;
                    case 3://Theo nhom
                        #region Theo nhom
                        if (obj.DienThoai == false)
                            gcBan.DataSource = db.mglbcBanChoThue_getByDateChoXoa(tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false)
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    p.MauNen,
                                    p.MaTT,
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
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViThueCu,
                                    p.DonViDangThue,
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
                        else if (obj.DienThoai3Dau == false)
                            gcBan.DataSource = db.mglbcBanChoThue_getByDateChoXoa(tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false)
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    p.MauNen,
                                    p.MaTT,
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
                        else
                            gcBan.DataSource = db.mglbcBanChoThue_getByDateChoXoa(tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false).ToList();//.Where(p => p.IsBan.GetValueOrDefault() == true);
                        #endregion
                        break;
                    case 4://Theo nhan vien
                        #region Theo nhan vien
                        if (obj.DienThoai == false)
                            gcBan.DataSource = db.mglbcBanChoThue_getByDateChoXoa(tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false)
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    p.MauNen,
                                    p.MaTT,
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
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViThueCu,
                                    p.DonViDangThue,
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
                        else if (obj.DienThoai3Dau == false)
                            gcBan.DataSource = db.mglbcBanChoThue_getByDateChoXoa(tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false)
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    p.MauNen,
                                    p.MaTT,
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
                                    p.PhongVS,
                                    p.DienTichDat,
                                    p.DienTichXD,
                                    p.TenDuong,
                                    p.SoTangXD,
                                    p.ThoiGianHD,
                                    p.ThoiGianBGMB,
                                    p.GhiChu,
                                    p.GioiThieu,
                                    p.DonViThueCu,
                                    p.DonViDangThue,
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
                        else
                            gcBan.DataSource = db.mglbcBanChoThue_getByDateChoXoa(tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false).ToList();//.Where(p => p.IsBan.GetValueOrDefault() == true);
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

        void Ban_Delete()
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

                if (db.mglbcThongTinTTs.Where(p => p.MaBC == (int?)grvBan.GetRowCellValue(i, "MaBC")).Count() > 0)
                {
                    var objTT = db.mglbcThongTinTTs.Where(p => p.MaBC == (int?)grvBan.GetRowCellValue(i, "MaBC"));
                    foreach (var item in objTT)
                    {
                        db.mglbcThongTinTTs.DeleteOnSubmit(item);
                    }
                    db.SubmitChanges();
                }

                mglbcBanChoThue objBC = db.mglbcBanChoThues.Single(p => p.MaBC == (int?)grvBan.GetRowCellValue(i, "MaBC"));
                db.mglbcBanChoThues.DeleteOnSubmit(objBC);
                grvBan.DeleteRow(i);
            }
            db.SubmitChanges();
            Ban_Load();
        }

        private void ctlManagerChoXoaCT_Load(object sender, EventArgs e)
        {
            chkTrangThai.DataSource = db.mglbcTrangThais;
            lookLoaiBDS.DataSource = db.LoaiBDs;
            lookHuong.DataSource = db.PhuongHuongs;
            lookLoaiDuong.DataSource = db.LoaiDuongs;
            lookPhapLy.DataSource = db.PhapLies;
            lookCapDo.DataSource = db.mglCapDos;
            lookNguon.DataSource = db.mglNguons;
            lookTinhTrangHD.DataSource = db.mglbcTrangThaiHDMGs;
            lookTinhTrang.DataSource = db.mglTinhTrangs;
            lookKHTC.DataSource = lookMaKHDT.DataSource = db.KhachHangs.Select(p => new { p.MaKH, Name = p.IsPersonal == true ? p.HoKH + " " + p.TenKH : p.TenCongTy });
            LoadPermission();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(4);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Ban_Load();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmEdit frm = new frmEdit();
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
            bool ck = ((int)grvBan.GetFocusedRowCellValue("MaNVQL") == Common.StaffID) || ((int)grvBan.GetFocusedRowCellValue("MaNVKD") == Common.StaffID || GetAccessData() == 1);
            frmEdit frm = new frmEdit();
            frm.AllowSave = ck;
            frm.DislayContac = ck;
            frm.MaBC = (int)grvBan.GetFocusedRowCellValue("MaBC");
            frm.ShowDialog();
            if (frm.IsSave)
            {
                Ban_Load();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Ban_Delete();
        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Ban_Delete();
        }

        private void itemKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                objBC.MaTTD = 2;
            }
            db.SubmitChanges();
            DialogBox.Infomation("Đã chuyển sang danh sách cần bán");
            Ban_Load();
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
            Ban_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            Ban_Load();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }
    }
}
