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
using System.Collections;
using BEE.ThuVien;
using BEEREMA;
using BEE.KhachHang;

namespace BEE.HoatDong.MGL.Mua
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();

        public ctlManager()
        {
            InitializeComponent();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 176;
            DataTable tblAction = o.SelectBy();
            itmThem.Enabled = false;
            itemSua.Enabled = false;
            itemXoa.Enabled = false;
            itemExport.Enabled = false;
            // itemImport.Enabled = false;
            itemMoBan.Enabled = false;
            itemUp.Enabled = false;
            itemNgungBan.Enabled = false;
            itemMoBan.Enabled = false;
            itemGiaoDich.Enabled = false;

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
                            //  itemImport.Enabled = true;
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
                    }
                }
            }
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 176);

            return o.SDB.SDBID;
        }

        public int Khop(mglmtMuaThue_getByDateResult objMT1)
        {
            try
            {
                if ((byte?)objMT1.MaTT != 0 && (byte?)objMT1.MaTT != 2)
                {
                    return 0;
                }
                var objMT = db.mglmtMuaThues.Single(p => p.MaMT == objMT1.MaMT);
                var listSP = new List<int>();
                var objcv = db.mglBCCongViecs.Where(p => p.MaCoHoiMT == objMT.MaMT);
                foreach (var item in objcv)
                {
                    listSP.Add(item.mglBCSanPhams.Select(p => p.MaSP).FirstOrDefault().Value);
                }
                // int MaBC = objcv.mglBCSanPhams.Select(p => p.MaSP).FirstOrDefault().Value;
                var objLoc = (from p in db.mglbcBanChoThues
                              join tt in db.mglbcThongTinTTs on p.MaBC equals tt.MaBC
                              join nvmg in db.NhanViens on p.MaNVKD equals nvmg.MaNV
                              where (p.IsBan.GetValueOrDefault() == objMT.IsMua & (p.MaTT == 2 || p.MaTT == 0))
                              orderby p.NgayDK descending
                              select new
                              {
                                  #region bc
                                  p.mglbcTrangThai.MauNen,
                                  p.mglbcTrangThai.TenTT,
                                  VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                  p.MaTT,
                                  AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                  // p.SoGiay,
                                  p.NamXayDung,
                                  p.TrangThaiHDMG,
                                  p.MaTinhTrang,
                                  p.HoTenNDD,
                                  p.HoTenNTG,
                                  p.NgangKV,
                                  p.DaiKV,
                                  p.SauKV,
                                  p.IsBan,
                                  TenNC = p.IsBan == true ? "Cần bán" : "Cho thuê",
                                  p.Huyen.TenHuyen,
                                  p.Tinh.TenTinh,
                                  p.Xa.TenXa,
                                  SoNhaaa = Common.SoNhaNEW((p.SoNha)),
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
                                  p.TienIch,
                                  p.DacTrung,
                                  p.KinhDo,
                                  p.ViDo,
                                  p.ToaDo,
                                  p.LoaiTien.TenLoaiTien,
                                  p.DaiXD,
                                  p.DonGia,
                                  p.MaKH,
                                  PhiMoiGioi = p.PhiMG,
                                  p.TyLeMG,
                                  p.MaNVKD,
                                  p.MaNVQL,
                                  HoTenNVMG = nvmg.HoTen,
                                  p.MaNguon,
                                  p.MaCD,
                                  p.NgayCN,
                                  p.NgayNhap,
                                  p.DuAn,
                                  //DienThoai = Common.Right(p.DienThoai, 3),
                                  #endregion
                                  IsCanGoc = p.IsCanGoc.GetValueOrDefault(),
                                  IsThangMay = p.IsThangMay.GetValueOrDefault(),
                                  TangHam = p.TangHam.GetValueOrDefault(),
                                  p.MaBC,
                                  p.SoDK,
                                  p.NgayDK,
                                  p.ThoiHan,
                                  p.TyLeHH,
                                  HoTenKH = (p.MaNVKD == Common.StaffID || p.MaNVQL == Common.StaffID || GetAccessData() == 1) == true ? p.KhachHang.HoKH + " " + p.KhachHang.TenKH : "",
                                  DienThoai = (p.MaNVKD == Common.StaffID || p.MaNVQL == Common.StaffID || GetAccessData() == 1) ? (p.MaKH == null ? "" : p.KhachHang.DiDong == "" ? "" : p.KhachHang.DiDong) : "",
                                  p.KyHieu,
                                  p.DienTich,
                                  ThanhTien = (p.ThanhTien * p.LoaiTien.TyGia),
                                  DiaChi = GetAdress(p.MaBC),
                                  TenPL = p.PhapLy != null ? p.PhapLy.TenPL : "",
                                  p.PhongKhach,
                                  p.PhongNgu,
                                  p.PhongTam,
                                  p.SoTang,
                                  Dtich = tt.DienTich,
                                  GiaTien = tt.GiaTien,
                                  MatTien = tt.MatTien,
                                  Tang = tt.SoTang,
                                  p.LauSo,
                                  //HoTenNV = (p.MaNVKD == Library.Common.StaffID || p.MaNVQL == Library.Common.StaffID || GetAccessData() == 1) ? p.NhanVien.HoTen : "",
                                  HoTenNV = p.NhanVien.HoTen,
                                  p.ChinhChu,
                                  p.MaDA,
                                  p.DuongRong,
                                  p.MaLBDS,
                                  p.LoaiBD.TenLBDS,
                                  DauOto = p.DauOto.GetValueOrDefault(),
                                  p.ThuongLuong,
                                  p.HuongBanCong,
                                  p.MaHuong,
                                  p.MaLD,
                                  p.MaHuyen,
                                  p.MaPL,
                                  p.PhiMG,
                                  p.NgangXD,
                                  p.MaTinh,
                                  p.MaDuong
                              }).ToList().Where(p => listSP.Contains(p.MaBC) == false);
                var objPQ = db.mglCaiDatTimKiems.FirstOrDefault(p => p.MaNV == Common.StaffID);
                if (objPQ == null)
                {
                    return 0;
                }
                if (objPQ == null)
                {
                    return 0;
                }
                if (objPQ.CanGoc.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.IsCanGoc == objMT.IsCanGoc).ToList();
                if (objPQ.DienTich.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.DienTich >= objMT.DienTichTu || p.Dtich >= objMT.DienTichTu).ToList();
                if (objPQ.DuAn.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.MaDA == objMT.MaDA).ToList();
                if (objPQ.DuongRong.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.DuongRong >= objMT.DuongRongTu).ToList();
                if (objPQ.HuongCua.GetValueOrDefault())
                    objLoc = objLoc.Where(p => objMT.mglmtHuongs.Count == 0 || objMT.mglmtHuongs.Select(h => h.MaHuong).Contains(p.MaHuong)).ToList();
                if (objPQ.HuongBanCong.GetValueOrDefault())
                    objLoc = objLoc.Where(p => objMT.mglmtHuongBCs.Count == 0 || objMT.mglmtHuongBCs.Select(h => h.MaHuong).Contains(p.HuongBanCong)).ToList();
                try
                {
                    if (objPQ.KhoangGia.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.ThanhTien <= (objMT.GiaDen * objMT.LoaiTien.TyGia) || p.GiaTien <= (objMT.GiaDen * objMT.LoaiTien.TyGia)).ToList();
                }
                catch { }
                if (objPQ.LoaiBDS.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.MaLBDS == objMT.MaLBDS).ToList();
                if (objPQ.LoaiDuong.GetValueOrDefault())
                    objLoc = objLoc.Where(p => objMT.mglmtLoaiDuongs.Count == 0 || objMT.mglmtLoaiDuongs.Select(ld => ld.MaLD).Contains(p.MaLD)).ToList();
                if (objPQ.OtoVao.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.DauOto == objMT.IsDeOto).ToList();
                if (objPQ.PhapLy.GetValueOrDefault())
                    objLoc = objLoc.Where(p => objMT.mglmtPhapLies.Count == 0 || objMT.mglmtPhapLies.Select(pl => pl.MaPL).Contains(p.MaPL)).ToList();
                if (objPQ.PhongNgu.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.PhongNgu >= objMT.PhNguTu).ToList();
                if (objPQ.QuanHuyen.GetValueOrDefault())
                    objLoc = objLoc = objLoc.Where(p => objMT.mglmtHuyens.Count == 0 || objMT.mglmtHuyens.Select(h => h.MaHuyen).Contains(p.MaHuyen)).ToList();
                if (objPQ.SoTang.GetValueOrDefault())
                    objLoc.Where(p => p.SoTang >= objMT.TangTu & (p.SoTang <= objMT.TangDen | objMT.TangDen == 0) || p.Tang >= objMT.TangTu & (p.Tang <= objMT.TangDen | objMT.TangDen == 0));
                if (objPQ.ThangMay.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.IsThangMay == objMT.IsThangMay).ToList();
                if (objPQ.TangHam.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.TangHam == objMT.IsTangHam).ToList();
                //if (objPQ.TienIch.GetValueOrDefault())
                //    objLoc.Where(p => objMT.mglmtTienIches.Count == 0 | objMT.mglmtTienIches.Select(h => h.MaTI).Contains(p.mglbcTienIches.Select(p=>p.MaTI).ToList()));

                if (objPQ.MatTien.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.NgangXD == objMT.MatTienTu || p.MatTien == objMT.MatTienTu).ToList();
                if (objPQ.TenDuong.GetValueOrDefault())
                    objLoc = objLoc.Where(p => objMT.mglmtDuongs.Count == 0 || objMT.mglmtDuongs.Select(d => d.MaDuong).Contains(p.MaDuong)).ToList();
                if (objPQ.Tinh.GetValueOrDefault())
                    objLoc = objLoc.Where(p => p.MaTinh == objMT.MaTinh).ToList();

                return objLoc.Count();
            }
            catch { }
            return 0;
        }

        void MuaThue_Load()
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
                    gcMuaThue.DataSource = null;
                    return;
                }
                var obj = db.mglmtPhanQuyens.Single(p => p.MaNV == Common.StaffID);
                switch (GetAccessData())
                {
                    case 1://Tat ca
                        #region Tat ca
                        if (obj.DienThoai == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, -1, MaNV, true, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    ////SLKhop = Khop(mt),
                                    mt.DaDoc,
                                    mt.SoDK,
                                    mt.ThuongHieu,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.MaNguon,
                                    mt.MaCD,
                                    mt.MaNVKT,
                                    mt.MaNVKD,
                                    mt.HoTenNV,
                                    mt.MaKH,
                                    mt.HoTenKH,
                                    mt.HoTenNVKD,
                                    mt.IsMua,
                                    mt.TenNC,
                                    mt.MaLBDS,
                                    mt.TenDA,
                                    mt.TyLeHH,
                                    mt.DienTichTu,
                                    mt.GhiChu,
                                    mt.KhuVuc,
                                    mt.PhiMG,
                                    mt.MucDich,
                                    mt.HuongCua,
                                    mt.HuongBC,
                                    mt.TienIch,
                                    mt.TenLoaiTien,
                                    mt.BoiDam,
                                    mt.GiaDen,
                                    mt.KhoangGia,
                                    mt.NgayNhap,
                                    mt.PhongNgu,
                                    mt.Duong,
                                    mt.SoTang,
                                    mt.PVS,
                                    DienThoai = Common.Right(mt.DienThoai, 3)
                                }).ToList();
                        else if (obj.DienThoai3Dau == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, -1, MaNV, true, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    //SLKhop = Khop(mt),
                                    mt.DaDoc,
                                    mt.SoDK,
                                    mt.ThuongHieu,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.MaNguon,
                                    mt.MaCD,
                                    mt.MaNVKT,
                                    mt.MaNVKD,
                                    mt.HoTenNV,
                                    mt.MaKH,
                                    mt.HoTenKH,
                                    mt.HoTenNVKD,
                                    mt.IsMua,
                                    mt.TenNC,
                                    mt.MaLBDS,
                                    mt.TenDA,
                                    mt.TyLeHH,
                                    mt.DienTichTu,
                                    mt.GhiChu,
                                    mt.KhuVuc,
                                    mt.PhiMG,
                                    mt.MucDich,
                                    mt.HuongCua,
                                    mt.HuongBC,
                                    mt.TienIch,
                                    mt.TenLoaiTien,
                                    mt.BoiDam,
                                    mt.GiaDen,
                                    mt.KhoangGia,
                                    mt.NgayNhap,
                                    mt.PhongNgu,
                                    mt.Duong,
                                    mt.SoTang,
                                    mt.PVS,
                                    DienThoai = Common.Right(mt.DienThoai, 3)
                                }).ToList();
                        else
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, -1, MaNV, true, Common.StaffID);//.Where(p => p.IsMua.GetValueOrDefault() == true);
                        #endregion
                        break;
                    case 2://Theo phong ban 
                        #region Theo phong ban
                        if (obj.DienThoai == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, Common.DepartmentID, MaNV, false, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.SoDK,
                                    mt.ThuongHieu,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.MaNguon,
                                    mt.DaDoc,
                                    mt.MaCD,
                                    mt.MaNVKT,
                                    mt.MaNVKD,
                                    mt.HoTenNV,
                                    mt.MaKH,
                                    mt.HoTenKH,
                                    mt.HoTenNVKD,
                                    mt.IsMua,
                                    mt.TenNC,
                                    mt.MaLBDS,
                                    mt.TenDA,
                                    mt.TyLeHH,
                                    mt.DienTichTu,
                                    mt.GhiChu,
                                    mt.KhuVuc,
                                    mt.PhiMG,
                                    mt.MucDich,
                                    mt.HuongCua,
                                    mt.HuongBC,
                                    mt.TienIch,
                                    mt.TenLoaiTien,
                                    mt.BoiDam,
                                    mt.GiaDen,
                                    mt.KhoangGia,
                                    mt.NgayNhap,
                                    mt.PhongNgu,
                                    mt.Duong,
                                    mt.SoTang,
                                    mt.PVS,
                                    DienThoai = Common.Right(mt.DienThoai, 3)
                                });
                        else if (obj.DienThoai3Dau == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, Common.DepartmentID, MaNV, false, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.SoDK,
                                    mt.ThuongHieu,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.MaNguon,
                                    mt.MaCD,
                                    mt.MaNVKT,
                                    mt.MaNVKD,
                                    mt.HoTenNV,
                                    mt.MaKH,
                                    mt.HoTenKH,
                                    mt.DaDoc,
                                    mt.HoTenNVKD,
                                    mt.IsMua,
                                    mt.TenNC,
                                    mt.MaLBDS,
                                    mt.TenDA,
                                    mt.TyLeHH,
                                    mt.DienTichTu,
                                    mt.GhiChu,
                                    mt.KhuVuc,
                                    mt.PhiMG,
                                    mt.MucDich,
                                    mt.HuongCua,
                                    mt.HuongBC,
                                    mt.TienIch,
                                    mt.TenLoaiTien,
                                    mt.BoiDam,
                                    mt.GiaDen,
                                    mt.KhoangGia,
                                    mt.NgayNhap,
                                    mt.PhongNgu,
                                    mt.Duong,
                                    mt.SoTang,
                                    mt.PVS,
                                    DienThoai = Common.Right1(mt.DienThoai, 3)
                                });
                        else
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, true, -1, -1, -1, Common.DepartmentID, MaNV, false, Common.StaffID);//.Where(p => p.IsMua.GetValueOrDefault() == true);
                        #endregion
                        break;
                    case 3://Theo nhom
                        #region Theo nhom
                        if (obj.DienThoai == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, true, -1, -1, Common.GroupID, -1, MaNV, false, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.SoDK,
                                    mt.ThuongHieu,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.MaNguon,
                                    mt.MaCD,
                                    mt.MaNVKT,
                                    mt.MaNVKD,
                                    mt.HoTenNV,
                                    mt.DaDoc,
                                    mt.MaKH,
                                    mt.HoTenKH,
                                    mt.HoTenNVKD,
                                    mt.IsMua,
                                    mt.TenNC,
                                    mt.MaLBDS,
                                    mt.TenDA,
                                    mt.TyLeHH,
                                    mt.DienTichTu,
                                    mt.GhiChu,
                                    mt.KhuVuc,
                                    mt.PhiMG,
                                    mt.MucDich,
                                    mt.HuongCua,
                                    mt.HuongBC,
                                    mt.TienIch,
                                    mt.TenLoaiTien,
                                    mt.BoiDam,
                                    mt.GiaDen,
                                    mt.KhoangGia,
                                    mt.NgayNhap,
                                    mt.PhongNgu,
                                    mt.Duong,
                                    mt.SoTang,
                                    mt.PVS,
                                    DienThoai = Common.Right(mt.DienThoai, 3)
                                });
                        else if (obj.DienThoai3Dau == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, true, -1, -1, Common.GroupID, -1, MaNV, false, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.ThuongHieu,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.SoDK,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.MaNguon,
                                    mt.MaCD,
                                    mt.MaNVKT,
                                    mt.MaNVKD,
                                    mt.DaDoc,
                                    mt.HoTenNV,
                                    mt.MaKH,
                                    mt.HoTenKH,
                                    mt.HoTenNVKD,
                                    mt.IsMua,
                                    mt.TenNC,
                                    mt.MaLBDS,
                                    mt.TenDA,
                                    mt.TyLeHH,
                                    mt.DienTichTu,
                                    mt.GhiChu,
                                    mt.KhuVuc,
                                    mt.PhiMG,
                                    mt.MucDich,
                                    mt.HuongCua,
                                    mt.HuongBC,
                                    mt.TienIch,
                                    mt.TenLoaiTien,
                                    mt.BoiDam,
                                    mt.GiaDen,
                                    mt.KhoangGia,
                                    mt.NgayNhap,
                                    mt.PhongNgu,
                                    mt.Duong,
                                    mt.SoTang,
                                    mt.PVS,
                                    DienThoai = Common.Right1(mt.DienThoai, 3)
                                });
                        else
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, true, -1, -1, Common.GroupID, -1, MaNV, false, Common.StaffID);//.Where(p => p.IsMua.GetValueOrDefault() == true);
                        #endregion
                        break;
                    case 4://Theo nhan vien
                        #region Theo nhan vien
                        if (obj.DienThoai == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, true, MaNV, MaNV, -1, -1, MaNV, false, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.SoDK,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.ThuongHieu,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.MaNguon,
                                    mt.MaCD,
                                    mt.MaNVKT,
                                    mt.MaNVKD,
                                    mt.HoTenNV,
                                    mt.MaKH,
                                    mt.HoTenKH,
                                    mt.DaDoc,
                                    mt.HoTenNVKD,
                                    mt.IsMua,
                                    mt.TenNC,
                                    mt.MaLBDS,
                                    mt.TenDA,
                                    mt.TyLeHH,
                                    mt.DienTichTu,
                                    mt.GhiChu,
                                    mt.KhuVuc,
                                    mt.PhiMG,
                                    mt.MucDich,
                                    mt.HuongCua,
                                    mt.HuongBC,
                                    mt.TienIch,
                                    mt.TenLoaiTien,
                                    mt.BoiDam,
                                    mt.GiaDen,
                                    mt.KhoangGia,
                                    mt.NgayNhap,
                                    mt.PhongNgu,
                                    mt.Duong,
                                    mt.SoTang,
                                    mt.PVS,
                                    DienThoai = Common.Right(mt.DienThoai, 3)
                                });//.Where(p => p.IsMua.GetValueOrDefault() == true);
                        else if (obj.DienThoai3Dau == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, true, MaNV, MaNV, -1, -1, MaNV, false, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.ThuongHieu,
                                    mt.MaMT,
                                    mt.SoDK,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.MaNguon,
                                    mt.MaCD,
                                    mt.MaNVKT,
                                    mt.MaNVKD,
                                    mt.HoTenNV,
                                    mt.MaKH,
                                    mt.HoTenKH,
                                    mt.DaDoc,
                                    mt.HoTenNVKD,
                                    mt.IsMua,
                                    mt.TenNC,
                                    mt.MaLBDS,
                                    mt.TenDA,
                                    mt.TyLeHH,
                                    mt.DienTichTu,
                                    mt.GhiChu,
                                    mt.KhuVuc,
                                    mt.PhiMG,
                                    mt.MucDich,
                                    mt.HuongCua,
                                    mt.HuongBC,
                                    mt.TienIch,
                                    mt.TenLoaiTien,
                                    mt.BoiDam,
                                    mt.GiaDen,
                                    mt.KhoangGia,
                                    mt.NgayNhap,
                                    mt.PhongNgu,
                                    mt.Duong,
                                    mt.SoTang,
                                    mt.PVS,
                                    DienThoai = Common.Right1(mt.DienThoai, 3)
                                });
                        else
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, true, -1, -1, Common.GroupID, -1, MaNV, false, Common.StaffID);//.Where(p => p.IsMua.GetValueOrDefault() == true);
                        #endregion
                        break;
                    default:
                        gcMuaThue.DataSource = null;
                        break;
                }
                //for (int i = 0; i < grvMuaThue.RowCount; i++)
                //{
                //    string khoangGia = string.Format("{0:#,0.##} -> {1:#,0.##} {2}", grvMuaThue.GetRowCellValue(i, "GiaTu"),
                //        grvMuaThue.GetRowCellValue(i, "GiaDen"), grvMuaThue.GetRowCellValue(i, "TenLoaiTien"));
                //    grvMuaThue.SetRowCellValue(i, "KhoangGia", khoangGia);
                //    string dienTich = string.Format("{0:#,0.##} -> {1:#,0.##}",
                //        grvMuaThue.GetRowCellValue(i, "DienTichTu"), grvMuaThue.GetRowCellValue(i, "DienTichDen"));
                //    grvMuaThue.SetRowCellValue(i, "DienTich", dienTich);
                //}
            }
            catch { }
            finally
            {
                wait.Close();
            }

        }

        private void MuaThue_Edit()
        {
            using (var frm = new frmEdit())
            {
                frm.MaMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
                frm.ShowDialog();
                if (frm.IsSave)
                {
                    MuaThue_Load();
                }
            }

        }

        private void MuaThue_EditV2()
        {
            using (var frm = new frmEdit())
            {
                frm.MaMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
                frm.ShowDialog();
                if (frm.IsSave)
                {
                    MuaThue_Load();
                }
            }

        }

        private void MuaThue_Delete()
        {
            var indexs = grvMuaThue.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu đăng ký");
                return;
            }
            if (DialogBox.Question() == DialogResult.No) return;
            foreach (var i in indexs)
            {
                bool ck = (GetAccessData() == 1 || (int)grvMuaThue.GetRowCellValue(i, "MaNVKT") == Common.StaffID) || ((int)grvMuaThue.GetRowCellValue(i, "MaNVKD") == Common.StaffID);
                if (!ck)
                    return;
                mglmtMuaThue objMT = db.mglmtMuaThues.Single(p => p.MaMT == (int)grvMuaThue.GetRowCellValue(i, "MaMT") && p.MaTT == 0);
                objMT.MaTTD = 1;
            }
            db.SubmitChanges();
            DialogBox.Infomation("Đã chuyển sang danh sách chờ xóa");
            MuaThue_Load();
        }

        private string GetAdress(int? MaBC)
        {
            var obj = db.mglbcBanChoThues.First(p => p.MaBC == MaBC);
            var xa = obj.MaXa == null ? "" : db.Xas.FirstOrDefault(p => p.MaXa == obj.MaXa).TenXa;
            var huyen = obj.MaHuyen == null ? "" : db.Huyens.First(p => p.MaHuyen == obj.MaHuyen).TenHuyen;
            var tinh = obj.MaTinh == null ? "" : db.Tinhs.First(p => p.MaTinh == obj.MaTinh).TenTinh;
            string DiaChi = string.Format("{0} - {1} - {2} - {3} - {4}", obj.SoNha, obj.Duong.TenDuong, xa, huyen, tinh);
            return DiaChi;
        }

        private void LocSanPham(mglmtMuaThue objMT)
        {
            try
            {
                var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);
                if (obj.SoNha == false & obj.TenDuong == false)
                    colSoNha.Visible = false;
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
                if (obj.ToaDo == false)
                    colKinhDo.Visible = false;
                if (obj.CanBoLV == false)
                    colNhanVienQL.Visible = false;
            }
            catch { }
            try
            {
                var listSP = new List<int>();
                var objcv = db.mglBCCongViecs.Where(p => p.MaCoHoiMT == objMT.MaMT);
                foreach (var item in objcv)
                {
                    listSP.Add(item.mglBCSanPhams.Select(p => p.MaSP).FirstOrDefault().Value);
                }
                // int MaBC = objcv.mglBCSanPhams.Select(p => p.MaSP).FirstOrDefault().Value;
                var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);
                if (obj.DienThoai == false)
                {
                    #region 3 số đầu
                    var objLoc = (from p in db.mglbcBanChoThues
                                  join tt in db.mglbcThongTinTTs on p.MaBC equals tt.MaBC
                                  join nvmg in db.NhanViens on p.MaNVKD equals nvmg.MaNV
                                  where (p.IsBan.GetValueOrDefault() == objMT.IsMua & (p.MaTT == 2 || p.MaTT == 0))
                                  orderby p.NgayDK descending
                                  select new
                                  {
                                      #region bc
                                      p.mglbcTrangThai.MauNen,
                                      p.mglbcTrangThai.TenTT,
                                      VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                      p.MaTT,
                                      AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                      // p.SoGiay,
                                      p.NamXayDung,
                                      p.TrangThaiHDMG,
                                      p.MaTinhTrang,
                                      p.HoTenNDD,
                                      p.HoTenNTG,
                                      p.NgangKV,
                                      p.DaiKV,
                                      p.SauKV,
                                      p.IsBan,
                                      TenNC = p.IsBan == true ? "Cần bán" : "Cho thuê",
                                      p.Huyen.TenHuyen,
                                      p.Tinh.TenTinh,
                                      p.Xa.TenXa,
                                      SoNhaaa = Common.SoNhaNEW((p.SoNha)),
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
                                      p.TienIch,
                                      p.DacTrung,
                                      p.KinhDo,
                                      p.ViDo,
                                      p.ToaDo,
                                      p.LoaiTien.TenLoaiTien,
                                      p.DaiXD,
                                      p.DonGia,
                                      p.MaKH,
                                      PhiMoiGioi = p.PhiMG,
                                      p.TyLeMG,
                                      p.MaNVKD,
                                      p.MaNVQL,
                                      HoTenNVMG = nvmg.HoTen,
                                      p.MaNguon,
                                      p.MaCD,
                                      p.NgayCN,
                                      p.NgayNhap,
                                      p.DuAn,
                                      //DienThoai = Common.Right(p.DienThoai, 3),
                                      #endregion
                                      IsCanGoc = p.IsCanGoc.GetValueOrDefault(),
                                      IsThangMay = p.IsThangMay.GetValueOrDefault(),
                                      TangHam = p.TangHam.GetValueOrDefault(),
                                      p.MaBC,
                                      p.SoDK,
                                      p.NgayDK,
                                      p.ThoiHan,
                                      p.TyLeHH,
                                      HoTenKH = (p.MaNVKD == Common.StaffID || p.MaNVQL == Common.StaffID || GetAccessData() == 1) == true ? p.KhachHang.HoKH + " " + p.KhachHang.TenKH : "",
                                      DienThoai = Common.Right((p.MaNVKD == Common.StaffID || p.MaNVQL == Common.StaffID || GetAccessData() == 1) ? (p.MaKH == null ? "" : p.KhachHang.DiDong == "" ? "" : p.KhachHang.DiDong) : "", 3),
                                      p.KyHieu,
                                      p.DienTich,
                                      ThanhTien = (p.ThanhTien * p.LoaiTien.TyGia),
                                      DiaChi = GetAdress(p.MaBC),
                                      TenPL = p.PhapLy != null ? p.PhapLy.TenPL : "",
                                      p.PhongKhach,
                                      p.PhongNgu,
                                      p.PhongTam,
                                      p.SoTang,
                                      Dtich = tt.DienTich,
                                      GiaTien = tt.GiaTien,
                                      MatTien = tt.MatTien,
                                      Tang = tt.SoTang,
                                      p.LauSo,
                                      //HoTenNV = (p.MaNVKD == Library.Common.StaffID || p.MaNVQL == Library.Common.StaffID || GetAccessData() == 1) ? p.NhanVien.HoTen : "",
                                      HoTenNV = p.NhanVien.HoTen,
                                      p.ChinhChu,
                                      p.MaDA,
                                      p.DuongRong,
                                      p.MaLBDS,
                                      p.LoaiBD.TenLBDS,
                                      DauOto = p.DauOto.GetValueOrDefault(),
                                      p.ThuongLuong,
                                      p.HuongBanCong,
                                      p.MaHuong,
                                      p.MaLD,
                                      p.MaHuyen,
                                      p.MaPL,
                                      p.PhiMG,
                                      p.NgangXD,
                                      p.MaTinh,
                                      p.MaDuong
                                  }).ToList().Where(p => listSP.Contains(p.MaBC) == false);
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
                        objLoc = objLoc.Where(p => p.IsCanGoc == objMT.IsCanGoc).ToList();
                    if (objPQ.DienTich.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.DienTich >= objMT.DienTichTu || p.Dtich >= objMT.DienTichTu).ToList();
                    if (objPQ.DuAn.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.MaDA == objMT.MaDA).ToList();
                    if (objPQ.DuongRong.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.DuongRong >= objMT.DuongRongTu).ToList();
                    if (objPQ.HuongCua.GetValueOrDefault())
                        objLoc = objLoc.Where(p => objMT.mglmtHuongs.Count == 0 || objMT.mglmtHuongs.Select(h => h.MaHuong).Contains(p.MaHuong)).ToList();
                    if (objPQ.HuongBanCong.GetValueOrDefault())
                        objLoc = objLoc.Where(p => objMT.mglmtHuongBCs.Count == 0 || objMT.mglmtHuongBCs.Select(h => h.MaHuong).Contains(p.HuongBanCong)).ToList();
                    try
                    {
                        if (objPQ.KhoangGia.GetValueOrDefault())
                            objLoc = objLoc.Where(p => p.ThanhTien <= (objMT.GiaDen * objMT.LoaiTien.TyGia) || p.GiaTien <= (objMT.GiaDen * objMT.LoaiTien.TyGia)).ToList();
                    }
                    catch { }
                    if (objPQ.LoaiBDS.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.MaLBDS == objMT.MaLBDS).ToList();
                    if (objPQ.LoaiDuong.GetValueOrDefault())
                        objLoc = objLoc.Where(p => objMT.mglmtLoaiDuongs.Count == 0 || objMT.mglmtLoaiDuongs.Select(ld => ld.MaLD).Contains(p.MaLD)).ToList();
                    if (objPQ.OtoVao.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.DauOto == objMT.IsDeOto).ToList();
                    if (objPQ.PhapLy.GetValueOrDefault())
                        objLoc = objLoc.Where(p => objMT.mglmtPhapLies.Count == 0 || objMT.mglmtPhapLies.Select(pl => pl.MaPL).Contains(p.MaPL)).ToList();
                    if (objPQ.PhongNgu.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.PhongNgu >= objMT.PhNguTu).ToList();
                    if (objPQ.QuanHuyen.GetValueOrDefault())
                        objLoc = objLoc = objLoc.Where(p => objMT.mglmtHuyens.Count == 0 || objMT.mglmtHuyens.Select(h => h.MaHuyen).Contains(p.MaHuyen)).ToList();
                    if (objPQ.SoTang.GetValueOrDefault())
                        objLoc.Where(p => p.SoTang >= objMT.TangTu & (p.SoTang <= objMT.TangDen | objMT.TangDen == 0) || p.Tang >= objMT.TangTu & (p.Tang <= objMT.TangDen | objMT.TangDen == 0));
                    if (objPQ.ThangMay.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.IsThangMay == objMT.IsThangMay).ToList();
                    if (objPQ.TangHam.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.TangHam == objMT.IsTangHam).ToList();
                    //if (objPQ.TienIch.GetValueOrDefault())
                    //    objLoc.Where(p => objMT.mglmtTienIches.Count == 0 | objMT.mglmtTienIches.Select(h => h.MaTI).Contains(p.mglbcTienIches.Select(p=>p.MaTI).ToList()));

                    if (objPQ.MatTien.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.NgangXD == objMT.MatTienTu || Convert.ToByte(p.MatTien) == objMT.MatTienTu).ToList();
                    if (objPQ.TenDuong.GetValueOrDefault())
                        objLoc = objLoc.Where(p => objMT.mglmtDuongs.Count == 0 || objMT.mglmtDuongs.Select(d => d.MaDuong).Contains(p.MaDuong)).ToList();
                    if (objPQ.Tinh.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.MaTinh == objMT.MaTinh).ToList();
                    gcBDS.DataSource = objLoc;
                    #endregion 3 số đầu
                }
                else if (obj.DienThoai3Dau == false)
                {
                    #region 3 số đầu
                    var objLoc = (from p in db.mglbcBanChoThues
                                  join tt in db.mglbcThongTinTTs on p.MaBC equals tt.MaBC
                                  join nvmg in db.NhanViens on p.MaNVKD equals nvmg.MaNV
                                  where (p.IsBan.GetValueOrDefault() == objMT.IsMua & (p.MaTT == 2 || p.MaTT == 0))
                                  orderby p.NgayDK descending
                                  select new
                                  {
                                      #region bc
                                      p.mglbcTrangThai.MauNen,
                                      p.mglbcTrangThai.TenTT,
                                      VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                      p.MaTT,
                                      AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                      // p.SoGiay,
                                      p.NamXayDung,
                                      p.TrangThaiHDMG,
                                      p.MaTinhTrang,
                                      p.HoTenNDD,
                                      p.HoTenNTG,
                                      p.NgangKV,
                                      p.DaiKV,
                                      p.SauKV,
                                      p.IsBan,
                                      TenNC = p.IsBan == true ? "Cần bán" : "Cho thuê",
                                      p.Huyen.TenHuyen,
                                      p.Tinh.TenTinh,
                                      p.Xa.TenXa,
                                      SoNhaaa = Common.SoNhaNEW((p.SoNha)),
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
                                      p.TienIch,
                                      p.DacTrung,
                                      p.KinhDo,
                                      p.ViDo,
                                      p.ToaDo,
                                      p.LoaiTien.TenLoaiTien,
                                      p.DaiXD,
                                      p.DonGia,
                                      p.MaKH,
                                      PhiMoiGioi = p.PhiMG,
                                      p.TyLeMG,
                                      p.MaNVKD,
                                      p.MaNVQL,
                                      HoTenNVMG = nvmg.HoTen,
                                      p.MaNguon,
                                      p.MaCD,
                                      p.NgayCN,
                                      p.NgayNhap,
                                      p.DuAn,
                                      //DienThoai = Common.Right(p.DienThoai, 3),
                                      #endregion
                                      IsCanGoc = p.IsCanGoc.GetValueOrDefault(),
                                      IsThangMay = p.IsThangMay.GetValueOrDefault(),
                                      TangHam = p.TangHam.GetValueOrDefault(),
                                      p.MaBC,
                                      p.SoDK,
                                      p.NgayDK,
                                      p.ThoiHan,
                                      p.TyLeHH,
                                      HoTenKH = (p.MaNVKD == Common.StaffID || p.MaNVQL == Common.StaffID || GetAccessData() == 1) == true ? p.KhachHang.HoKH + " " + p.KhachHang.TenKH : "",
                                      DienThoai = Common.Right1((p.MaNVKD == Common.StaffID || p.MaNVQL == Common.StaffID || GetAccessData() == 1) ? (p.MaKH == null ? "" : p.KhachHang.DiDong == "" ? "" : p.KhachHang.DiDong) : "", 3),
                                      p.KyHieu,
                                      p.DienTich,
                                      ThanhTien = (p.ThanhTien * p.LoaiTien.TyGia),
                                      DiaChi = GetAdress(p.MaBC),
                                      TenPL = p.PhapLy != null ? p.PhapLy.TenPL : "",
                                      p.PhongKhach,
                                      p.PhongNgu,
                                      p.PhongTam,
                                      p.SoTang,
                                      Dtich = tt.DienTich,
                                      GiaTien = tt.GiaTien,
                                      MatTien = tt.MatTien,
                                      Tang = tt.SoTang,
                                      p.LauSo,
                                      //HoTenNV = (p.MaNVKD == Library.Common.StaffID || p.MaNVQL == Library.Common.StaffID || GetAccessData() == 1) ? p.NhanVien.HoTen : "",
                                      HoTenNV = p.NhanVien.HoTen,
                                      p.ChinhChu,
                                      p.MaDA,
                                      p.DuongRong,
                                      p.MaLBDS,
                                      p.LoaiBD.TenLBDS,
                                      DauOto = p.DauOto.GetValueOrDefault(),
                                      p.ThuongLuong,
                                      p.HuongBanCong,
                                      p.MaHuong,
                                      p.MaLD,
                                      p.MaHuyen,
                                      p.MaPL,
                                      p.PhiMG,
                                      p.NgangXD,
                                      p.MaTinh,
                                      p.MaDuong
                                  }).ToList().Where(p => listSP.Contains(p.MaBC) == false);
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
                        objLoc = objLoc.Where(p => p.IsCanGoc == objMT.IsCanGoc).ToList();
                    if (objPQ.DienTich.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.DienTich >= objMT.DienTichTu || p.Dtich >= objMT.DienTichTu).ToList();
                    if (objPQ.DuAn.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.MaDA == objMT.MaDA).ToList();
                    if (objPQ.DuongRong.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.DuongRong >= objMT.DuongRongTu).ToList();
                    if (objPQ.HuongCua.GetValueOrDefault())
                        objLoc = objLoc.Where(p => objMT.mglmtHuongs.Count == 0 || objMT.mglmtHuongs.Select(h => h.MaHuong).Contains(p.MaHuong)).ToList();
                    if (objPQ.HuongBanCong.GetValueOrDefault())
                        objLoc = objLoc.Where(p => objMT.mglmtHuongBCs.Count == 0 || objMT.mglmtHuongBCs.Select(h => h.MaHuong).Contains(p.HuongBanCong)).ToList();
                    try
                    {
                        if (objPQ.KhoangGia.GetValueOrDefault())
                            objLoc = objLoc.Where(p => p.ThanhTien <= (objMT.GiaDen * objMT.LoaiTien.TyGia) || p.GiaTien <= (objMT.GiaDen * objMT.LoaiTien.TyGia)).ToList();
                    }
                    catch { }
                    if (objPQ.LoaiBDS.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.MaLBDS == objMT.MaLBDS).ToList();
                    if (objPQ.LoaiDuong.GetValueOrDefault())
                        objLoc = objLoc.Where(p => objMT.mglmtLoaiDuongs.Count == 0 || objMT.mglmtLoaiDuongs.Select(ld => ld.MaLD).Contains(p.MaLD)).ToList();
                    if (objPQ.OtoVao.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.DauOto == objMT.IsDeOto).ToList();
                    if (objPQ.PhapLy.GetValueOrDefault())
                        objLoc = objLoc.Where(p => objMT.mglmtPhapLies.Count == 0 || objMT.mglmtPhapLies.Select(pl => pl.MaPL).Contains(p.MaPL)).ToList();
                    if (objPQ.PhongNgu.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.PhongNgu >= objMT.PhNguTu).ToList();
                    if (objPQ.QuanHuyen.GetValueOrDefault())
                        objLoc = objLoc = objLoc.Where(p => objMT.mglmtHuyens.Count == 0 || objMT.mglmtHuyens.Select(h => h.MaHuyen).Contains(p.MaHuyen)).ToList();
                    if (objPQ.SoTang.GetValueOrDefault())
                        objLoc.Where(p => p.SoTang >= objMT.TangTu & (p.SoTang <= objMT.TangDen | objMT.TangDen == 0) || p.Tang >= objMT.TangTu & (p.Tang <= objMT.TangDen | objMT.TangDen == 0));
                    if (objPQ.ThangMay.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.IsThangMay == objMT.IsThangMay).ToList();
                    if (objPQ.TangHam.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.TangHam == objMT.IsTangHam).ToList();
                    //if (objPQ.TienIch.GetValueOrDefault())
                    //    objLoc.Where(p => objMT.mglmtTienIches.Count == 0 | objMT.mglmtTienIches.Select(h => h.MaTI).Contains(p.mglbcTienIches.Select(p=>p.MaTI).ToList()));

                    if (objPQ.MatTien.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.NgangXD == objMT.MatTienTu || Convert.ToByte(p.MatTien) == objMT.MatTienTu).ToList();
                    if (objPQ.TenDuong.GetValueOrDefault())
                        objLoc = objLoc.Where(p => objMT.mglmtDuongs.Count == 0 || objMT.mglmtDuongs.Select(d => d.MaDuong).Contains(p.MaDuong)).ToList();
                    if (objPQ.Tinh.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.MaTinh == objMT.MaTinh).ToList();
                    gcBDS.DataSource = objLoc;
                    #endregion 3 số đầu
                }
                else
                {
                    #region 3 số đầu
                    var objLoc = (from p in db.mglbcBanChoThues
                                  join tt in db.mglbcThongTinTTs on p.MaBC equals tt.MaBC
                                  join nvmg in db.NhanViens on p.MaNVKD equals nvmg.MaNV
                                  where (p.IsBan.GetValueOrDefault() == objMT.IsMua & (p.MaTT == 2 || p.MaTT == 0))
                                  orderby p.NgayDK descending
                                  select new
                                  {
                                      #region bc
                                      p.mglbcTrangThai.MauNen,
                                      p.mglbcTrangThai.TenTT,
                                      VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                      p.MaTT,
                                      AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                      // p.SoGiay,
                                      p.NamXayDung,
                                      p.TrangThaiHDMG,
                                      p.MaTinhTrang,
                                      p.HoTenNDD,
                                      p.HoTenNTG,
                                      p.NgangKV,
                                      p.DaiKV,
                                      p.SauKV,
                                      p.IsBan,
                                      TenNC = p.IsBan == true ? "Cần bán" : "Cho thuê",
                                      p.Huyen.TenHuyen,
                                      p.Tinh.TenTinh,
                                      p.Xa.TenXa,
                                      SoNhaaa = Common.SoNhaNEW((p.SoNha)),
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
                                      p.TienIch,
                                      p.DacTrung,
                                      p.KinhDo,
                                      p.ViDo,
                                      p.ToaDo,
                                      p.LoaiTien.TenLoaiTien,
                                      p.DaiXD,
                                      p.DonGia,
                                      p.MaKH,
                                      PhiMoiGioi = p.PhiMG,
                                      p.TyLeMG,
                                      p.MaNVKD,
                                      p.MaNVQL,
                                      HoTenNVMG = nvmg.HoTen,
                                      p.MaNguon,
                                      p.MaCD,
                                      p.NgayCN,
                                      p.NgayNhap,
                                      p.DuAn,
                                      //DienThoai = Common.Right(p.DienThoai, 3),
                                      #endregion
                                      IsCanGoc = p.IsCanGoc.GetValueOrDefault(),
                                      IsThangMay = p.IsThangMay.GetValueOrDefault(),
                                      TangHam = p.TangHam.GetValueOrDefault(),
                                      p.MaBC,
                                      p.SoDK,
                                      p.NgayDK,
                                      p.ThoiHan,
                                      p.TyLeHH,
                                      HoTenKH = (p.MaNVKD == Common.StaffID || p.MaNVQL == Common.StaffID || GetAccessData() == 1) == true ? p.KhachHang.HoKH + " " + p.KhachHang.TenKH : "",
                                      DienThoai = (p.MaNVKD == Common.StaffID || p.MaNVQL == Common.StaffID || GetAccessData() == 1) ? (p.MaKH == null ? "" : p.KhachHang.DiDong == "" ? "" : p.KhachHang.DiDong) : "",
                                      p.KyHieu,
                                      p.DienTich,
                                      ThanhTien = (p.ThanhTien * p.LoaiTien.TyGia),
                                      DiaChi = GetAdress(p.MaBC),
                                      TenPL = p.PhapLy != null ? p.PhapLy.TenPL : "",
                                      p.PhongKhach,
                                      p.PhongNgu,
                                      p.PhongTam,
                                      p.SoTang,
                                      Dtich = tt.DienTich,
                                      GiaTien = tt.GiaTien,
                                      MatTien = tt.MatTien,
                                      Tang = tt.SoTang,
                                      p.LauSo,
                                      //HoTenNV = (p.MaNVKD == Library.Common.StaffID || p.MaNVQL == Library.Common.StaffID || GetAccessData() == 1) ? p.NhanVien.HoTen : "",
                                      HoTenNV = p.NhanVien.HoTen,
                                      p.ChinhChu,
                                      p.MaDA,
                                      p.DuongRong,
                                      p.MaLBDS,
                                      p.LoaiBD.TenLBDS,
                                      DauOto = p.DauOto.GetValueOrDefault(),
                                      p.ThuongLuong,
                                      p.HuongBanCong,
                                      p.MaHuong,
                                      p.MaLD,
                                      p.MaHuyen,
                                      p.MaPL,
                                      p.PhiMG,
                                      p.NgangXD,
                                      p.MaTinh,
                                      p.MaDuong
                                  }).ToList().Where(p => listSP.Contains(p.MaBC) == false);
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
                        objLoc = objLoc.Where(p => p.IsCanGoc == objMT.IsCanGoc).ToList();
                    if (objPQ.DienTich.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.DienTich >= objMT.DienTichTu || p.Dtich >= objMT.DienTichTu).ToList();
                    if (objPQ.DuAn.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.MaDA == objMT.MaDA).ToList();
                    if (objPQ.DuongRong.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.DuongRong >= objMT.DuongRongTu).ToList();
                    if (objPQ.HuongCua.GetValueOrDefault())
                        objLoc = objLoc.Where(p => objMT.mglmtHuongs.Count == 0 || objMT.mglmtHuongs.Select(h => h.MaHuong).Contains(p.MaHuong)).ToList();
                    if (objPQ.HuongBanCong.GetValueOrDefault())
                        objLoc = objLoc.Where(p => objMT.mglmtHuongBCs.Count == 0 || objMT.mglmtHuongBCs.Select(h => h.MaHuong).Contains(p.HuongBanCong)).ToList();
                    try
                    {
                        if (objPQ.KhoangGia.GetValueOrDefault())
                            objLoc = objLoc.Where(p => p.ThanhTien <= (objMT.GiaDen * objMT.LoaiTien.TyGia) || p.GiaTien <= (objMT.GiaDen * objMT.LoaiTien.TyGia)).ToList();
                    }
                    catch { }
                    if (objPQ.LoaiBDS.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.MaLBDS == objMT.MaLBDS).ToList();
                    if (objPQ.LoaiDuong.GetValueOrDefault())
                        objLoc = objLoc.Where(p => objMT.mglmtLoaiDuongs.Count == 0 || objMT.mglmtLoaiDuongs.Select(ld => ld.MaLD).Contains(p.MaLD)).ToList();
                    if (objPQ.OtoVao.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.DauOto == objMT.IsDeOto).ToList();
                    if (objPQ.PhapLy.GetValueOrDefault())
                        objLoc = objLoc.Where(p => objMT.mglmtPhapLies.Count == 0 || objMT.mglmtPhapLies.Select(pl => pl.MaPL).Contains(p.MaPL)).ToList();
                    if (objPQ.PhongNgu.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.PhongNgu >= objMT.PhNguTu).ToList();
                    if (objPQ.QuanHuyen.GetValueOrDefault())
                        objLoc = objLoc = objLoc.Where(p => objMT.mglmtHuyens.Count == 0 || objMT.mglmtHuyens.Select(h => h.MaHuyen).Contains(p.MaHuyen)).ToList();
                    if (objPQ.SoTang.GetValueOrDefault())
                        objLoc.Where(p => p.SoTang >= objMT.TangTu & (p.SoTang <= objMT.TangDen | objMT.TangDen == 0) || p.Tang >= objMT.TangTu & (p.Tang <= objMT.TangDen | objMT.TangDen == 0));
                    if (objPQ.ThangMay.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.IsThangMay == objMT.IsThangMay).ToList();
                    if (objPQ.TangHam.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.TangHam == objMT.IsTangHam).ToList();
                    //if (objPQ.TienIch.GetValueOrDefault())
                    //    objLoc.Where(p => objMT.mglmtTienIches.Count == 0 | objMT.mglmtTienIches.Select(h => h.MaTI).Contains(p.mglbcTienIches.Select(p=>p.MaTI).ToList()));

                    if (objPQ.MatTien.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.NgangXD == objMT.MatTienTu || Convert.ToByte(p.MatTien) == objMT.MatTienTu).ToList();
                    if (objPQ.TenDuong.GetValueOrDefault())
                        objLoc = objLoc.Where(p => objMT.mglmtDuongs.Count == 0 || objMT.mglmtDuongs.Select(d => d.MaDuong).Contains(p.MaDuong)).ToList();
                    if (objPQ.Tinh.GetValueOrDefault())
                        objLoc = objLoc.Where(p => p.MaTinh == objMT.MaTinh).ToList();
                    gcBDS.DataSource = objLoc;
                    #endregion
                }
            }
            catch { }
        }

        private void TabPage_Load()
        {
            try
            {
                int? maMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
                if (maMT == null)
                {
                    gcBDS.DataSource = null;
                    gcNhatKy.DataSource = null;
                    gcSanPham.DataSource = null;
                    gcSPXem.DataSource = null;
                    gridControlAvatar.DataSource = null;
                    return;
                }

                switch (tabMain.SelectedTabPageIndex)
                {
                    case 0:
                        #region Lich lam viec
                        var listNhatKy = db.mglmtNhatKyXuLies.Where(p => p.MaMT == maMT)
                            .OrderByDescending(p => p.NgayXL)
                            .Select((p) => new
                            {
                                p.ID,
                                p.NgayXL,
                                p.TieuDe,
                                p.NoiDung,
                                p.PhuongThucXuLy.TenPT,
                                p.MaNVG,
                                HoTenNVG = p.NhanVien.HoTen,
                                HoTenNVN = p.NhanVien1.HoTen,
                                p.KetQua
                            }).ToList();
                        gcNhatKy.DataSource = listNhatKy;
                        #endregion
                        break;
                    case 1:
                        #region San pham
                        if ((byte?)grvMuaThue.GetFocusedRowCellValue("MaTT") != 0 && (byte?)grvMuaThue.GetFocusedRowCellValue("MaTT") != 2)
                        {
                            gcBDS.DataSource = null;
                            return;
                        }
                        var objMT = db.mglmtMuaThues.Single(p => p.MaMT == maMT);
                        LocSanPham(objMT);
                        #endregion
                        break;
                    case 2:
                        gcSanPham.DataSource = db.mglgdGiaoDiches.Where(p => p.MaMT == maMT && p.MaTT == 7).Select(p => new
                        {
                            p.mglbcBanChoThue.SoDK,
                            p.mglbcBanChoThue.NgayDK,
                            HoTenKH = (p.MaNVBC == Common.StaffID || p.MaNVMT == Common.StaffID || p.MaNV == Common.StaffID || Common.PerID == 1) ? (p.mglbcBanChoThue.KhachHang.HoKH + " " + p.mglbcBanChoThue.KhachHang.TenKH) : "",
                            DienThoai = (p.MaNVBC == Common.StaffID || p.MaNVMT == Common.StaffID || p.MaNV == Common.StaffID || Common.PerID == 1) ? p.mglbcBanChoThue.MaKH == null ? "" : p.mglbcBanChoThue.KhachHang.DiDong == "" ? "" : p.mglbcBanChoThue.KhachHang.DiDong : "",
                            p.mglbcBanChoThue.KyHieu,
                            p.mglbcBanChoThue.DienTich,
                            p.mglbcBanChoThue.ThanhTien,
                            p.mglbcBanChoThue.LoaiBD.TenLBDS,
                            DiaChi = GetAdress(p.MaBC),
                            p.NhanVien.HoTen
                        });
                        break;
                    case 3:
                        gcSPXem.DataSource = db.mglBCSanPhams.Where(p => p.mglBCCongViec.MaCoHoiMT == maMT).Select(p => new
                        {
                            p.mglbcBanChoThue.SoDK,
                            p.mglbcBanChoThue.NgayDK,
                            HoTenKH = (p.mglBCCongViec.MaNVBC == Common.StaffID || p.mglBCCongViec.MaNVMT == Common.StaffID || p.mglBCCongViec.MaNV == Common.StaffID || Common.PerID == 1) ? (p.mglbcBanChoThue.KhachHang.HoKH + " " + p.mglbcBanChoThue.KhachHang.TenKH) : "",
                            DienThoai = (p.mglBCCongViec.MaNVBC == Common.StaffID || p.mglBCCongViec.MaNVMT == Common.StaffID || p.mglBCCongViec.MaNV == Common.StaffID || Common.PerID == 1) ? p.mglbcBanChoThue.MaKH == null ? "" : p.mglbcBanChoThue.KhachHang.DiDong == "" ? "" : p.mglbcBanChoThue.KhachHang.DiDong : "",
                            p.mglbcBanChoThue.KyHieu,
                            p.mglbcBanChoThue.DienTich,
                            p.mglbcBanChoThue.ThanhTien,
                            p.mglbcBanChoThue.LoaiBD.TenLBDS,
                            DiaChi = GetAdress(p.MaSP),
                            p.mglbcBanChoThue.NhanVien.HoTen
                        });
                        break;
                    case 4:
                        ctlTaiLieu1.FormID = 176;
                        ctlTaiLieu1.LinkID = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
                        ctlTaiLieu1.MaNV = Common.StaffID;
                        ctlTaiLieu1.TaiLieu_Load();
                        break;
                    case 5:
                        it.NguoiDaiDienCls o = new it.NguoiDaiDienCls();
                        gridControlAvatar.DataSource = o.Select((int)grvMuaThue.GetFocusedRowCellValue("MaKH"));
                        break;
                    case 6:
                        #region ghi nhan
                        if (db.mglmtLichSus.Where(p => p.MaMT == maMT).Count() > 1)
                            gcGhiNhan.DataSource = (from ls in db.mglmtLichSus
                                                    join nv in db.NhanViens on ls.MaNVS equals nv.MaNV
                                                    where ls.MaMT == maMT
                                                    select new
                                                    {
                                                        ls.ID,
                                                        ls.TenKH,
                                                        ls.SoDienThoai,
                                                        ls.Email,
                                                        ls.DiaChi,
                                                        ls.GhiChu,
                                                        ls.CanBoLV,
                                                        nv.HoTen,
                                                        ls.NgaySua
                                                    }).ToList();
                        else
                            gcGhiNhan.DataSource = null;
                        #endregion
                        break;
                }
            }
            catch { }
        }

        void ThuTuCot()
        {
            var stt = db.mglmtThuTuCotMTs.OrderBy(p => p.STT).ToList();
            for (int i = 0; i < stt.Count; i++)
            {
                switch (stt[i].Cot)
                {
                    case "STT":
                        colSTT.VisibleIndex = stt[i].STT.Value;
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
                    case "Mục đích":
                        colMucDich.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Khu vực":
                        colKhuVuc.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Diện tích":
                        colDienTich.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Khoảng giá":
                        colKhoangGia.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Loại tiền":
                        colLoaiTien.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Phòng ngủ":
                        colPhongNgu.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Phòng VS":
                        colPhongVS.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Hướng cửa":
                        colHuongCua.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Hướng BC":
                        colHuongBC.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tiện ích":
                        colTienIch.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Phí MG":
                        colPhiMG.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Nguồn":
                        colNguon.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Số ĐK":
                        colSoDK.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Ngày ĐK":
                        colNgayDK.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Khách hàng":
                        colKhachHang.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Điện thoại":
                        colDienThoai.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Nhân viên QL":
                        colNhanVienQL.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Nhân viên MG":
                        colNhanVienMG.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Ghi chú":
                        colGhiChu.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Ngày CN":
                        colNgayCN.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Ngày nhập":
                        colNgayNhap.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Đường":
                        colDuong.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Khớp nhu cầu":
                        colKhop.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Thương hiệu - Mô hình":
                        colThuongHieu.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Nhân viên nhập":
                        colNhanVienNhap.VisibleIndex = stt[i].STT.Value;
                        break;
                }
            }


        }

        void phanquyen()
        {
            try
            {
                var obj = db.mglmtPhanQuyens.Single(p => p.MaNV == Common.StaffID);
                if (obj.TenCT == false)
                    colKhachHang.Visible = false;
                if (obj.GhiChu == false)
                    colGhiChu.Visible = false;
                if (obj.CanBoLV == false)
                    colNhanVienQL.Visible = false;
                if (obj.DienThoaiAn == false)
                    colDienThoai.Visible = false;
            }
            catch { }

        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            List<ItemTrangThai> lst = new List<ItemTrangThai>();
            lst.Add(new ItemTrangThai() { MaTT = Convert.ToByte(100), TenTT = "<Tất cả>" });
            foreach (var p in db.mglbcTrangThais)
            {
                var obj = new ItemTrangThai() { MaTT = p.MaTT, TenTT = p.TenTT };
                lst.Add(obj);
            }
            lookTrangThai.DataSource = lst;
            chkTrangThai.DataSource = db.mglbcTrangThais;
            lookLoaiBDSBC.DataSource = lookLoaiBDS.DataSource = db.LoaiBDs;
            lookCapDo.DataSource = db.mglCapDos;
            lookNguonBC.DataSource = lookNguon.DataSource = db.mglNguons;
            LoadPermission();
            phanquyen();
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(4);
            ThuTuCot();
            grvMuaThue.BestFitColumns();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (itemTrangThai.EditValue != "Trạng thái" && itemTrangThai.EditValue != "")
            {
                db = new MasterDataContext();
                MuaThue_Load();
                phanquyen();
                grvMuaThue.BestFitColumns();
            }
        }

        private void itmThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmEdit frm = new frmEdit();
            frm.NhuCau = true;
            frm.ShowDialog();
            if (frm.IsSave)
            {
                MuaThue_Load();
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMuaThue.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu đăng ký");
                return;
            }
            MuaThue_Edit();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MuaThue_Delete();
        }

        private void grvMuaThue_DoubleClick(object sender, EventArgs e)
        {
            if (grvMuaThue.FocusedRowHandle < 0)
            {
                return;
            }

            MuaThue_EditV2();
        }

        private void grvMuaThue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                MuaThue_Delete();
            }
        }

        void SetDate(int index)
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
            //MuaThue_Load();
            phanquyen();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            //MuaThue_Load();
            phanquyen();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void grvMuaThue_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TabPage_Load();
        }

        private void itemXuLy_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMuaThue.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn giao dịch");
                return;
            }

            frmSend frm = new frmSend();
            frm.MaMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                db = new MasterDataContext();
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
                db = new MasterDataContext();
                TabPage_Load();
            }
        }

        private void itemNhatKy_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvNhatKy.FocusedRowHandle < 0) return;
            if (DialogBox.Question() == DialogResult.No) return;
            var objNK = db.mglmtNhatKyXuLies.Single(p => p.ID == (int)grvNhatKy.GetFocusedRowCellValue("ID"));
            db.mglmtNhatKyXuLies.DeleteOnSubmit(objNK);
            db.SubmitChanges();

            TabPage_Load();
        }

        private void itemNhatKy_TraLoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvNhatKy.FocusedRowHandle < 0) return;

            if ((int)grvMuaThue.GetFocusedRowCellValue("MaNVKD") != Common.StaffID) return;

            frmReply frm = new frmReply();
            frm.ID = (int)grvNhatKy.GetFocusedRowCellValue("ID");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                db = new MasterDataContext();
                TabPage_Load();
            }
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            TabPage_Load();
        }

        private void itemIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void grvMuaThue_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            try
            {
                if (e.Column.FieldName == "TenTT")
                {
                    e.Appearance.BackColor = System.Drawing.Color.FromArgb((int)grvMuaThue.GetRowCellValue(e.RowHandle, "MauNen"));
                }

                if ((int)grvMuaThue.GetRowCellValue(e.RowHandle, "SLKhop") > 0 && !(bool)grvMuaThue.GetRowCellValue(e.RowHandle, "DaDoc"))
                {
                    if ((bool)grvMuaThue.GetFocusedRowCellValue("DaDoc") || db.mglmtDaDocs.Where(p => p.MaMT == (int?)grvMuaThue.GetRowCellValue(e.RowHandle, "MaMT")).Count() > 0)
                    {

                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);

                    }
                    else
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }

            }
            catch { }
        }

        private void itemSP_GiaoDich_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMuaThue.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn nhu cầu");
                return;
            }

            if ((byte)grvMuaThue.GetFocusedRowCellValue("MaTT") > 2)
            {
                DialogBox.Error("Nhu cầu đã giao dịch hoặc đang khóa");
                return;
            }

            int maBC = (int)grvBDS.GetFocusedRowCellValue("MaBC");
            int maMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
            int rowCount = db.mglgdGiaoDiches.Where(p => p.MaBC == maBC & p.MaMT == maMT).Count();
            if (rowCount > 0)
            {
                DialogBox.Error("Sản phẩm đã được giao dịch đang chờ duyệt");
                return;
            }
            GiaoDich.frmEdit frm = new BEE.HoatDong.MGL.GiaoDich.frmEdit();
            frm.MaMT = maMT;
            frm.MaBC = maBC;
            frm.ShowDialog();
        }

        private void itemSP_GuiYeuCau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBDS.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn sản phẩm");
                return;
            }

            Ban.frmSend frm = new Ban.frmSend();
            frm.MaBC = (int)grvBDS.GetFocusedRowCellValue("MaBC");
            frm.ShowDialog();
        }

        private void itemUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs = grvMuaThue.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn sản phẩm");
                return;
            }
            foreach (var i in indexs)
            {
                if ((int)grvMuaThue.GetRowCellValue(i, "MaNVKD") == Common.StaffID)
                {
                    db.mglmtMuaThues.Single(p => p.MaMT == (int)grvMuaThue.GetRowCellValue(i, "MaMT")).NgayCN = DateTime.Now;
                }
            }
            db.SubmitChanges();

            MuaThue_Load();
        }

        private void itemMoBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs = grvMuaThue.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn sản phẩm");
                return;
            }
            foreach (var i in indexs)
            {
                db.mglmtMuaThues.Single(p => p.MaMT == (int)grvMuaThue.GetRowCellValue(i, "MaMT")).MaTT = 0;
            }
            db.SubmitChanges();

            MuaThue_Load();
        }

        private void itemNgungBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs = grvMuaThue.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn sản phẩm");
                return;
            }
            foreach (var i in indexs)
            {
                db.mglmtMuaThues.Single(p => p.MaMT == (int)grvMuaThue.GetRowCellValue(i, "MaMT")).MaTT = 0;
            }
            db.SubmitChanges();

            MuaThue_Load();
        }

        private void itemGiaoDich_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int? maBC = (int?)grvBDS.GetFocusedRowCellValue("MaBC");

            if (maBC == null)
            {
                DialogBox.Error("Vui lòng chọn sản phẩm");
                return;
            }

            int maMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
            int rowCount = db.mglgdGiaoDiches.Where(p => p.MaBC == maBC & p.MaMT == maMT & p.MaTT != 6).Count();
            if (rowCount > 0)
            {
                DialogBox.Error("Sản phẩm đã được giao dịch đang chờ duyệt");
                return;
            }
            GiaoDich.frmEdit frm = new BEE.HoatDong.MGL.GiaoDich.frmEdit();
            frm.MaMT = maMT;
            frm.MaBC = maBC.Value;
            frm.ShowDialog();
        }

        private void itemTrangThai_EditValueChanged(object sender, EventArgs e)
        {
            byte? maTT = (byte?)itemTrangThai.EditValue;
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

            MuaThue_Load();
            phanquyen();
        }

        private void itemCapDo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //try
            //{
            //    int[] indexs = grvMuaThue.GetSelectedRows();
            //    if (indexs.Length <= 0)
            //    {
            //        DialogBox.Error("Vui lòng chọn nhu cầu");
            //        return;
            //    }

            //    var frm = new frmCapDoChon();
            //    frm.ShowDialog();
            //    if (frm.MaCD == 0) return;

            //    foreach (var i in indexs)
            //    {
            //        db.mglmtMuaThues.Single(p => p.MaMT == (int)grvMuaThue.GetRowCellValue(i, "MaMT")).MaCD = frm.MaCD;
            //    }
            //    db.SubmitChanges();

            //    MuaThue_Load();
            //}
            //catch (Exception ex)
            //{
            //    DialogBox.Error(ex.Message);
            //}
        }

        private void itemSP_Xem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBDS.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Bạn vui lòng chọn sản phẩm để xem chi tiết!");
                return;
            }
            if (grvBDS.GetFocusedRowCellValue("HoTenNV") == null)
            {
                DialogBox.Infomation("Bạn không có quyền xem thông tin sản phẩm này!");
                return;
            }
            var objBC = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == (int)grvBDS.GetFocusedRowCellValue("MaBC"));
            using (var frm = new MGL.Ban.frmEdit())
            {
                if (objBC.MaNVQL != Common.StaffID && objBC.MaNVKD != Common.StaffID && Common.PerID != 1)
                {
                    frm.AllowSave = false;
                    frm.DislayContac = false;
                }
                frm.MaBC = (int)grvBDS.GetFocusedRowCellValue("MaBC");
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
            it.CommonCls.ExportExcel(gcMuaThue);
        }

        private void itemXuLy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<int> listSP = new List<int>();
            var indexs = grvBDS.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn sản phẩm phù hợp để xử lý");
                return;
            }
            foreach (var i in indexs)
            {
                listSP.Add((int)grvBDS.GetRowCellValue(i, "MaBC"));
            }
            using (var frm = new MGL.XuLy.frmEdit())
            {
                frm.MaCoHoiMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
                frm.ListSP = listSP;
                frm.ShowDialog();
            }
        }

        private void itemSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBDS.FocusedRowHandle < 0)
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
            var indexs = grvBDS.GetSelectedRows();
            foreach (var i in indexs)
            {
                var objBC = db.mglbcBanChoThues.Single(p => p.MaBC == (int)grvBDS.GetRowCellValue(i, "MaBC"));
                var objTT = db.mglThongTinGuis.Single(p => p.ID == 1);
                if (objTT.SoNha == true)
                    noidung += "Số nhà: " + objBC.SoNha + ", Tên đường: " + objBC.Duong.TenDuong + ", Phường/Xã: " + objBC.Xa.TenXa + ", Quận/Huyện: " + objBC.Huyen.TenHuyen +
                        ", Tỉnh Thành Phố: " + objBC.Tinh.TenTinh + Environment.NewLine;
                if (objTT.MatTien == true)
                    noidung += " - Mặt tiền " + string.Format("{0:#,0.##}", objBC.NgangXD) + "m " + Environment.NewLine;
                if (objTT.DienTich == true)
                    noidung += " - Diện tích " + string.Format("{0:#,0.##}", objBC.DienTichXD) + "m " + "Số tầng: " + objBC.SoTangXD + Environment.NewLine;
                if (objTT.TongGia == true)
                    noidung += " - Tổng Giá " + string.Format("{0:#,0.##}", objBC.ThanhTien) + Environment.NewLine;
                if (objTT.MoTa == true)
                    noidung += "Mô tả " + objBC.DacTrung + Environment.NewLine;
                if (objTT.GhiChu == true)
                    noidung += "Ghi chú " + objBC.GhiChu + Environment.NewLine;
                if (objTT.LinkAnh == true)
                    noidung += "Hình ảnh xem tại: " + objBC.LinkAnh + Environment.NewLine;
                if (objTT.LinkViTri == true)
                    noidung += "Định vị xem tại: " + objBC.ToaDo + Environment.NewLine;
            }
            noidung += "Thân trọng cảm ơn!:";
            var objMT = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == (int)grvBDS.GetFocusedRowCellValue("MaBC"));
            using (var frm = new BEE.HoatDong.MGL.frmSend() { objKH = objMT.KhachHang })
            {
                frm.noidung = noidung;
                frm.ShowDialog();
            }
        }

        private void itemChangeST_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMuaThue.FocusedRowHandle < 0)
                return;
            using (var frm = new frmXuLy() { MaMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT") })
            {
                frm.ShowDialog();
            }
        }

        private void itemThemNLH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var MaKH = (int)grvMuaThue.GetFocusedRowCellValue("MaKH");
            NguoiDaiDien_frm frm = new NguoiDaiDien_frm();
            frm.MaKH = MaKH;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                TabPage_Load();
        }

        private void itemSuaNLH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridAvatar.GetFocusedRowCellValue(colMaKHDD) != null)
            {
                try
                {
                    NguoiDaiDien_frm frm = new NguoiDaiDien_frm();
                    frm.MaKH = int.Parse(gridAvatar.GetFocusedRowCellValue(colMaKHDD).ToString());
                    frm.MaNDD = byte.Parse(gridAvatar.GetFocusedRowCellValue(colSTT2).ToString());
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
                        o.MaNDD = byte.Parse(gridAvatar.GetFocusedRowCellValue(colSTT2).ToString());
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

        private void grvMuaThue_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                try
                {
                    if (!(bool)grvMuaThue.GetFocusedRowCellValue("DaDoc"))
                    {
                        db.mglmtDaDocAdd((int)grvMuaThue.GetFocusedRowCellValue("MaMT"), Common.StaffID);

                    }
                }
                catch { }
            }
        }

        private void itemTrangThai_EditValueChanged_2(object sender, EventArgs e)
        {
            if (itemTrangThai.EditValue != "")
            {
                //byte? maTT = (byte?)itemTrangThai.EditValue;
                //if (maTT == 1)
                //{
                //    itemMoBan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //    itemNgungBan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                //    itemUp.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                //    itemGiaoDich.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                //}
                //else
                //{
                //    if (maTT == 2)
                //        itemMoBan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //    else
                //        itemMoBan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                //    itemNgungBan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //    itemUp.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //    itemGiaoDich.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //}
                MuaThue_Load();
                phanquyen();
            }

        }

        private void itemInport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmImport())
            {
                frm.ShowDialog();
            }
        }
    }
    public class ItemTrangThai
    {
        public byte? MaTT { get; set; }
        public string TenTT { get; set; }
    }
}
