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

namespace BEE.HoatDong.MGL.Mua
{
    public partial class ctlManagerCanThue : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();
        List<int> lstChon = new List<int>();

        List<int> lstChonDX = new List<int>();
        List<int> lstChonDP = new List<int>();
        List<int> lstChonDC = new List<int>();
        List<int> lstChonTP = new List<int>();
        List<int> lstChonDatCoc = new List<int>();

        List<int> lstDaQuanTam = new List<int>();
        List<int> lstDaDuaKH = new List<int>();
        List<int> lstDangSuaHD = new List<int>();
        List<int> lstDaKyHD = new List<int>();
        List<int> lstDaThuPhi = new List<int>();
        public ctlManagerCanThue()
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

        void LoadPermissionLichSuLV()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 219;
            DataTable tblAction = o.SelectBy();
            itemXuLy_Them.Enabled = false;
            itemXuLy_Sua.Enabled = false;
            itemNhatKy_Xoa.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            itemXuLy_Them.Enabled = true;
                            break;
                        case 2:
                            itemXuLy_Sua.Enabled = true;
                            break;
                        case 3:
                            itemNhatKy_Xoa.Enabled = true;
                            break;
                    }
                }
            }
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

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 177);

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
                if (objLoc.Count() > 0)
                    objMT.BoiDam = true;
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
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1, MaNV, true, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.ThuongHieu,
                                    //SLKhop = Khop(mt),
                                    mt.SoDK,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.DaDoc,
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
                                    mt.MoHinh,
                                    DienThoai = Common.Right(mt.DienThoai, 3),
                                    DiDong2 = Common.Right(mt.DiDong2, 3),
                                    DiDong3 = Common.Right(mt.DiDong3, 3),
                                    DiDong4 = Common.Right(mt.DiDong4, 3),
                                }).ToList();
                        else if (obj.DienThoai3Dau == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1, MaNV, true, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.ThuongHieu,
                                    //SLKhop = Khop(mt),
                                    mt.SoDK,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.MaNguon,
                                    mt.MaCD,
                                    mt.DaDoc,
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
                                    mt.MoHinh,
                                    DienThoai = Common.Right(mt.DienThoai, 3),
                                    DiDong2 = Common.Right(mt.DiDong2, 3),
                                    DiDong3 = Common.Right(mt.DiDong3, 3),
                                    DiDong4 = Common.Right(mt.DiDong4, 3),
                                }).ToList();
                        else if (obj.DienThoaiAn == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1, MaNV, true, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.ThuongHieu,
                                    //SLKhop = Khop(mt),
                                    mt.SoDK,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.MaNguon,
                                    mt.MaCD,
                                    mt.DaDoc,
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
                                    mt.MoHinh,
                                    DienThoai = "",
                                    DiDong2 = "",
                                    DiDong3 = "",
                                    DiDong4 = "",
                                }).ToList();
                        else
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1, MaNV, true, Common.StaffID);//.Where(p => p.IsMua.GetValueOrDefault() == true);
                        #endregion
                        break;
                    case 2://Theo phong ban 
                        #region Theo phong ban
                        if (obj.DienThoai == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    mt.ThuongHieu,
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.SoDK,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.MaNguon,
                                    mt.MaCD,
                                    mt.DaDoc,
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
                                    mt.MoHinh,
                                    DienThoai = Common.Right(mt.DienThoai, 3),
                                    DiDong2 = Common.Right(mt.DiDong2, 3),
                                    DiDong3 = Common.Right(mt.DiDong3, 3),
                                    DiDong4 = Common.Right(mt.DiDong4, 3),
                                }).ToList();
                        else if (obj.DienThoai3Dau == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    mt.ThuongHieu,
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.SoDK,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.MaNguon,
                                    mt.MaCD,
                                    mt.DaDoc,
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
                                    mt.MoHinh,
                                    DienThoai = Common.Right(mt.DienThoai, 3),
                                    DiDong2 = Common.Right(mt.DiDong2, 3),
                                    DiDong3 = Common.Right(mt.DiDong3, 3),
                                    DiDong4 = Common.Right(mt.DiDong4, 3),
                                }).ToList();
                        else
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false, Common.StaffID);//.Where(p => p.IsMua.GetValueOrDefault() == true);
                        #endregion
                        break;
                    case 3://Theo nhom
                        #region Theo nhom
                        if (obj.DienThoai == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    mt.ThuongHieu,
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.SoDK,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.MaNguon,
                                    mt.MaCD,
                                    mt.DaDoc,
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
                                    mt.MoHinh,
                                    DienThoai = Common.Right(mt.DienThoai, 3),
                                    DiDong2 = Common.Right(mt.DiDong2, 3),
                                    DiDong3 = Common.Right(mt.DiDong3, 3),
                                    DiDong4 = Common.Right(mt.DiDong4, 3),
                                }).ToList();
                        else if (obj.DienThoai3Dau == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    mt.ThuongHieu,
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.SoDK,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.MaNguon,
                                    mt.MaCD,
                                    mt.DaDoc,
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
                                    mt.MoHinh,
                                    DienThoai = Common.Right(mt.DienThoai, 3),
                                    DiDong2 = Common.Right(mt.DiDong2, 3),
                                    DiDong3 = Common.Right(mt.DiDong3, 3),
                                    DiDong4 = Common.Right(mt.DiDong4, 3)
                                }).ToList();
                        else
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false, Common.StaffID);//.Where(p => p.IsMua.GetValueOrDefault() == true);
                        #endregion
                        break;
                    case 4://Theo nhan vien
                        #region Theo nhan vien
                        if (obj.DienThoai == false)
                        {
                            var obj1 = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, Common.StaffID)
                                   .Select(mt => new
                                   {
                                       mt.STT,
                                       mt.MaTT,
                                       mt.ThuongHieu,
                                       //SLKhop = Khop(mt),
                                       mt.MauNen,
                                       mt.TenTT,
                                       mt.MaMT,
                                       mt.SoDK,
                                       mt.NgayDK,
                                       mt.NgayCN,
                                       mt.SoGiay,
                                       mt.ThoiHan,
                                       mt.MaNguon,
                                       mt.MaCD,
                                       mt.DaDoc,
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
                                       mt.MoHinh,
                                       DienThoai = Common.Right(mt.DienThoai, 3),
                                       DiDong2 = Common.Right(mt.DiDong2, 3),
                                       DiDong3 = Common.Right(mt.DiDong3, 3),
                                       DiDong4 = Common.Right(mt.DiDong4, 3)
                                   }).ToList();//.Where(p => p.IsMua.GetValueOrDefault() == true);
                            gcMuaThue.DataSource = obj1;
                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            var obj1 = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    mt.ThuongHieu,
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.SoDK,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.MaNguon,
                                    mt.MaCD,
                                    mt.DaDoc,
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
                                    mt.MoHinh,
                                    DienThoai = Common.Right1(mt.DienThoai, 3),
                                    DiDong2 = Common.Right1(mt.DiDong2, 3),
                                    DiDong3 = Common.Right1(mt.DiDong3, 3),
                                    DiDong4 = Common.Right1(mt.DiDong4, 3)
                                }).ToList();
                            gcMuaThue.DataSource = obj1;
                        }
                        else if (obj.DienThoaiAn == false)
                        {
                            var obj1 = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    mt.ThuongHieu,
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.SoDK,
                                    mt.NgayDK,
                                    mt.NgayCN,
                                    mt.SoGiay,
                                    mt.ThoiHan,
                                    mt.MaNguon,
                                    mt.MaCD,
                                    mt.DaDoc,
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
                                    mt.MoHinh,
                                    DienThoai = "",
                                    DiDong2 = "",
                                    DiDong3 = "",
                                    DiDong4 = ""
                                }).ToList();
                            gcMuaThue.DataSource = obj1;
                        }
                        else
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, Common.StaffID).ToList();
                        #endregion
                        break;
                    default:
                        gcMuaThue.DataSource = null;
                        break;
                }
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
                bool ck = (GetAccessData() == 1 || (int)grvMuaThue.GetFocusedRowCellValue("MaNVKT") == Common.StaffID) || ((int)grvMuaThue.GetFocusedRowCellValue("MaNVKD") == Common.StaffID);
                frm.AllowSave = false;
                frm.DislayContac = ck;
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
                bool ck = ((int)grvMuaThue.GetRowCellValue(i, "MaNVKT") == Common.StaffID) || ((int)grvMuaThue.GetRowCellValue(i, "MaNVKD") == Common.StaffID);
                if (!ck)
                    return;
                mglmtMuaThue objMT = db.mglmtMuaThues.Single(p => p.MaMT == (int)grvMuaThue.GetRowCellValue(i, "MaMT"));
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
            string DiaChi = string.Format("{0} - {1} - {2} - {3} - {4}", obj.SoNha, obj.TenDuong, xa, huyen, tinh);
            return DiaChi;
        }

        private string GetAdress(string SoNha, string TenDuong, string xa, string huyen, string tinh)
        {
            string DiaChi = string.Format("{0} - {1} - {2} - {3} - {4}", SoNha, TenDuong, xa, huyen, tinh);
            return DiaChi;
        }

        private void LocSanPham(mglmtMuaThue objMT)
        {
            #region phan quyen hien thi cot xem
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
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            #endregion

            //
            try
            {
                lstChon.Clear();

                //var listSP = new List<int>();
                var arrlistSP = ",";
                //var objcv = db.mglBCCongViecs.Where(p => p.MaCoHoiMT == objMT.MaMT);
                //foreach (var item in objcv)
                //{
                //    listSP.Add(item.mglBCSanPhams.Select(p => p.MaSP).FirstOrDefault().Value);
                //    arrlistSP += item.mglBCSanPhams.Select(p => p.MaSP).FirstOrDefault().Value + ",";
                //}
                arrlistSP += ",";

                //// int MaBC = objcv.mglBCSanPhams.Select(p => p.MaSP).FirstOrDefault().Value;
                //var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);

                #region
                //if (obj.DienThoai == false)
                //{
                //    #region 3 số đầu
                //    //var objLoc = (from p in db.mglbcBanChoThues
                //    //              join tt in db.mglbcThongTinTTs on p.MaBC equals tt.MaBC
                //    //              join nvmg in db.NhanViens on p.MaNVKD equals nvmg.MaNV
                //    //              where (p.IsBan.GetValueOrDefault() == objMT.IsMua & (p.MaTT == 2 || p.MaTT == 0))
                //    //              orderby p.NgayDK descending
                //    //              select new
                //    //              {
                //    //                  #region bc
                //    //                  p.mglbcTrangThai.MauNen,
                //    //                  p.mglbcTrangThai.TenTT,
                //    //                  VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                //    //                  p.MaTT,
                //    //                  AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                //    //                  // p.SoGiay,
                //    //                  p.NamXayDung,
                //    //                  p.TrangThaiHDMG,
                //    //                  p.MaTinhTrang,
                //    //                  p.HoTenNDD,
                //    //                  p.HoTenNTG,
                //    //                  p.NgangKV,
                //    //                  p.DaiKV,
                //    //                  p.SauKV,
                //    //                  p.IsBan,
                //    //                  TenNC = p.IsBan == true ? "Cần bán" : "Cho thuê",
                //    //                  p.Huyen.TenHuyen,
                //    //                  p.Tinh.TenTinh,
                //    //                  p.Xa.TenXa,
                //    //                  SoNhaaa = Common.SoNhaNEW((p.SoNha)),
                //    //                  p.PhongVS,
                //    //                  p.DienTichDat,
                //    //                  p.DienTichXD,
                //    //                  p.TenDuong,
                //    //                  p.SoTangXD,
                //    //                  p.ThoiGianHD,
                //    //                  p.ThoiGianBGMB,
                //    //                  p.GhiChu,
                //    //                  p.GioiThieu,
                //    //                  p.DonViDangThue,
                //    //                  p.DonViThueCu,
                //    //                  p.TienIch,
                //    //                  p.DacTrung,
                //    //                  p.KinhDo,
                //    //                  p.ViDo,
                //    //                  p.ToaDo,
                //    //                  p.LoaiTien.TenLoaiTien,
                //    //                  p.DaiXD,
                //    //                  p.DonGia,
                //    //                  p.MaKH,
                //    //                  PhiMoiGioi = p.PhiMG,
                //    //                  p.TyLeMG,
                //    //                  p.MaNVKD,
                //    //                  p.MaNVQL,
                //    //                  HoTenNVMG = nvmg.HoTen,
                //    //                  p.MaNguon,
                //    //                  p.MaCD,
                //    //                  p.NgayCN,
                //    //                  p.NgayNhap,
                //    //                  p.DuAn,
                //    //                  //DienThoai = Common.Right(p.DienThoai, 3),
                //    //                  #endregion
                //    //                  IsCanGoc = p.IsCanGoc.GetValueOrDefault(),
                //    //                  IsThangMay = p.IsThangMay.GetValueOrDefault(),
                //    //                  TangHam = p.TangHam.GetValueOrDefault(),
                //    //                  p.MaBC,
                //    //                  p.SoDK,
                //    //                  p.NgayDK,
                //    //                  p.ThoiHan,
                //    //                  p.TyLeHH,
                //    //                  HoTenKH = (p.MaNVKD == Common.StaffID || p.MaNVQL == Common.StaffID || GetAccessData() == 1) == true ? p.KhachHang.HoKH + " " + p.KhachHang.TenKH : "",
                //    //                  DienThoai = Common.Right((p.MaNVKD == Common.StaffID || p.MaNVQL == Common.StaffID || GetAccessData() == 1) ? (p.MaKH == null ? "" : p.KhachHang.DiDong == "" ? "" : p.KhachHang.DiDong) : "", 3),
                //    //                  p.KyHieu,
                //    //                  p.DienTich,
                //    //                  ThanhTien = (p.ThanhTien * p.LoaiTien.TyGia),
                //    //                  DiaChi = GetAdress(p.MaBC),
                //    //                  TenPL = p.PhapLy != null ? p.PhapLy.TenPL : "",
                //    //                  p.PhongKhach,
                //    //                  p.PhongNgu,
                //    //                  p.PhongTam,
                //    //                  p.SoTang,
                //    //                  Dtich = tt.DienTich,
                //    //                  GiaTien = tt.GiaTien,
                //    //                  MatTien = tt.MatTien,
                //    //                  Tang = tt.SoTang,
                //    //                  p.LauSo,
                //    //                  //HoTenNV = (p.MaNVKD == Library.Common.StaffID || p.MaNVQL == Library.Common.StaffID || GetAccessData() == 1) ? p.NhanVien.HoTen : "",
                //    //                  HoTenNV = p.NhanVien.HoTen,
                //    //                  p.ChinhChu,
                //    //                  p.MaDA,
                //    //                  p.DuongRong,
                //    //                  p.MaLBDS,
                //    //                  p.LoaiBD.TenLBDS,
                //    //                  DauOto = p.DauOto.GetValueOrDefault(),
                //    //                  p.ThuongLuong,
                //    //                  p.HuongBanCong,
                //    //                  p.MaHuong,
                //    //                  p.MaLD,
                //    //                  p.MaHuyen,
                //    //                  p.MaPL,
                //    //                  p.PhiMG,
                //    //                  p.NgangXD,
                //    //                  p.MaTinh,
                //    //                  p.MaDuong
                //    //              }).ToList().Where(p => listSP.Contains(p.MaBC) == false);
                //    //var objPQ = db.mglCaiDatTimKiems.FirstOrDefault(p => p.MaNV == Common.StaffID);
                //    //if (objPQ == null)
                //    //{
                //    //    DialogBox.Infomation("Bạn chưa cài đặt tiêu chí lọc sản phẩm");
                //    //    return;
                //    //}
                //    //if (objPQ == null)
                //    //{
                //    //    DialogBox.Infomation("Vui lòng cài đặt quy định lọc thông tin cho tài khoản này!");
                //    //    return;
                //    //}
                //    //if (objPQ.CanGoc.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.IsCanGoc == objMT.IsCanGoc).ToList();
                //    //if (objPQ.DienTich.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.DienTich >= objMT.DienTichTu || p.Dtich >= objMT.DienTichTu).ToList();
                //    //if (objPQ.DuAn.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.MaDA == objMT.MaDA).ToList();
                //    //if (objPQ.DuongRong.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.DuongRong >= objMT.DuongRongTu).ToList();
                //    //if (objPQ.HuongCua.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => objMT.mglmtHuongs.Count == 0 || objMT.mglmtHuongs.Select(h => h.MaHuong).Contains(p.MaHuong)).ToList();
                //    //if (objPQ.HuongBanCong.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => objMT.mglmtHuongBCs.Count == 0 || objMT.mglmtHuongBCs.Select(h => h.MaHuong).Contains(p.HuongBanCong)).ToList();
                //    //try
                //    //{
                //    //    if (objPQ.KhoangGia.GetValueOrDefault())
                //    //        objLoc = objLoc.Where(p => p.ThanhTien <= (objMT.GiaDen * objMT.LoaiTien.TyGia) || p.GiaTien <= (objMT.GiaDen * objMT.LoaiTien.TyGia)).ToList();
                //    //}
                //    //catch { }
                //    //if (objPQ.LoaiBDS.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.MaLBDS == objMT.MaLBDS).ToList();
                //    //if (objPQ.LoaiDuong.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => objMT.mglmtLoaiDuongs.Count == 0 || objMT.mglmtLoaiDuongs.Select(ld => ld.MaLD).Contains(p.MaLD)).ToList();
                //    //if (objPQ.OtoVao.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.DauOto == objMT.IsDeOto).ToList();
                //    //if (objPQ.PhapLy.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => objMT.mglmtPhapLies.Count == 0 || objMT.mglmtPhapLies.Select(pl => pl.MaPL).Contains(p.MaPL)).ToList();
                //    //if (objPQ.PhongNgu.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.PhongNgu >= objMT.PhNguTu).ToList();
                //    //if (objPQ.QuanHuyen.GetValueOrDefault())
                //    //    objLoc = objLoc = objLoc.Where(p => objMT.mglmtHuyens.Count == 0 || objMT.mglmtHuyens.Select(h => h.MaHuyen).Contains(p.MaHuyen)).ToList();
                //    //if (objPQ.SoTang.GetValueOrDefault())
                //    //    objLoc.Where(p => p.SoTang >= objMT.TangTu & (p.SoTang <= objMT.TangDen | objMT.TangDen == 0) || p.Tang >= objMT.TangTu & (p.Tang <= objMT.TangDen | objMT.TangDen == 0));
                //    //if (objPQ.ThangMay.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.IsThangMay == objMT.IsThangMay).ToList();
                //    //if (objPQ.TangHam.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.TangHam == objMT.IsTangHam).ToList();
                //    ////if (objPQ.TienIch.GetValueOrDefault())
                //    ////    objLoc.Where(p => objMT.mglmtTienIches.Count == 0 | objMT.mglmtTienIches.Select(h => h.MaTI).Contains(p.mglbcTienIches.Select(p=>p.MaTI).ToList()));

                //    //if (objPQ.MatTien.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.NgangXD == objMT.MatTienTu || Convert.ToByte(p.MatTien) == objMT.MatTienTu).ToList();
                //    //if (objPQ.TenDuong.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => objMT.mglmtDuongs.Count == 0 || objMT.mglmtDuongs.Select(d => d.MaDuong).Contains(p.MaDuong)).ToList();
                //    //if (objPQ.Tinh.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.MaTinh == objMT.MaTinh).ToList();
                //    //gcBDS.DataSource = objLoc;
                //    #endregion 3 số đầu
                //}
                #endregion

                //else if (obj.DienThoai3Dau == false)
                {
                    #region Kiem tra tieu chi khop nhu cau
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

                    #endregion

                    #region 3 số đầu
                    #region code Linq
                    //var objLoc = (from p in db.mglbcBanChoThues
                    //              join tt in db.mglbcThongTinTTs on p.MaBC equals tt.MaBC
                    //              join nvmg in db.NhanViens on p.MaNVKD equals nvmg.MaNV
                    //              where (p.IsBan.GetValueOrDefault() == objMT.IsMua & (p.MaTT == 2 || p.MaTT == 0))
                    //              orderby p.NgayDK descending
                    //              select new
                    //              {
                    //                  #region bc
                    //                  p.mglbcTrangThai.MauNen,
                    //                  p.mglbcTrangThai.TenTT,
                    //                  VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                    //                  p.MaTT,
                    //                  AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                    //                  // p.SoGiay,
                    //                  p.NamXayDung,
                    //                  p.TrangThaiHDMG,
                    //                  p.MaTinhTrang,
                    //                  p.HoTenNDD,
                    //                  p.HoTenNTG,
                    //                  p.NgangKV,
                    //                  p.DaiKV,
                    //                  p.SauKV,
                    //                  p.IsBan,
                    //                  TenNC = p.IsBan == true ? "Cần bán" : "Cho thuê",
                    //                  p.Huyen.TenHuyen,
                    //                  p.Tinh.TenTinh,
                    //                  p.Xa.TenXa,
                    //                  SoNhaaa = Common.SoNhaNEW((p.SoNha)),
                    //                  p.PhongVS,
                    //                  p.DienTichDat,
                    //                  p.DienTichXD,
                    //                  p.TenDuong,
                    //                  p.SoTangXD,
                    //                  p.ThoiGianHD,
                    //                  p.ThoiGianBGMB,
                    //                  p.GhiChu,
                    //                  p.GioiThieu,
                    //                  p.DonViDangThue,
                    //                  p.DonViThueCu,
                    //                  p.TienIch,
                    //                  p.DacTrung,
                    //                  p.KinhDo,
                    //                  p.ViDo,
                    //                  p.ToaDo,
                    //                  p.LoaiTien.TenLoaiTien,
                    //                  p.DaiXD,
                    //                  p.DonGia,
                    //                  p.MaKH,
                    //                  PhiMoiGioi = p.PhiMG,
                    //                  p.TyLeMG,
                    //                  p.MaNVKD,
                    //                  p.MaNVQL,
                    //                  HoTenNVMG = nvmg.HoTen,
                    //                  p.MaNguon,
                    //                  p.MaCD,
                    //                  p.NgayCN,
                    //                  p.NgayNhap,
                    //                  p.DuAn,
                    //                  //DienThoai = Common.Right(p.DienThoai, 3),
                    //                  #endregion
                    //                  IsCanGoc = p.IsCanGoc.GetValueOrDefault(),
                    //                  IsThangMay = p.IsThangMay.GetValueOrDefault(),
                    //                  TangHam = p.TangHam.GetValueOrDefault(),
                    //                  p.MaBC,
                    //                  p.SoDK,
                    //                  p.NgayDK,
                    //                  p.ThoiHan,
                    //                  p.TyLeHH,
                    //                  HoTenKH = (p.MaNVKD == Common.StaffID || p.MaNVQL == Common.StaffID || GetAccessData() == 1) == true ? p.KhachHang.HoKH + " " + p.KhachHang.TenKH : "",
                    //                  DienThoai = Common.Right1((p.MaNVKD == Common.StaffID || p.MaNVQL == Common.StaffID || GetAccessData() == 1) ? (p.MaKH == null ? "" : p.KhachHang.DiDong == "" ? "" : p.KhachHang.DiDong) : "", 3),
                    //                  p.KyHieu,
                    //                  p.DienTich,
                    //                  ThanhTien = (p.ThanhTien * p.LoaiTien.TyGia),
                    //                  DiaChi = GetAdress(p.MaBC),
                    //                  TenPL = p.PhapLy != null ? p.PhapLy.TenPL : "",
                    //                  p.PhongKhach,
                    //                  p.PhongNgu,
                    //                  p.PhongTam,
                    //                  p.SoTang,
                    //                  Dtich = tt.DienTich,
                    //                  GiaTien = tt.GiaTien,
                    //                  MatTien = tt.MatTien,
                    //                  Tang = tt.SoTang,
                    //                  p.LauSo,
                    //                  //HoTenNV = (p.MaNVKD == Library.Common.StaffID || p.MaNVQL == Library.Common.StaffID || GetAccessData() == 1) ? p.NhanVien.HoTen : "",
                    //                  HoTenNV = p.NhanVien.HoTen,
                    //                  p.ChinhChu,
                    //                  p.MaDA,
                    //                  p.DuongRong,
                    //                  p.MaLBDS,
                    //                  p.LoaiBD.TenLBDS,
                    //                  DauOto = p.DauOto.GetValueOrDefault(),
                    //                  p.ThuongLuong,
                    //                  p.HuongBanCong,
                    //                  p.MaHuong,
                    //                  p.MaLD,
                    //                  p.MaHuyen,
                    //                  p.MaPL,
                    //                  p.PhiMG,
                    //                  p.NgangXD,
                    //                  p.MaTinh,
                    //                  p.MaDuong
                    //              }).ToList().Where(p => listSP.Contains(p.MaBC) == false);
                    #endregion

                    var objLoc = db.mglbcBanChoThue_KhopNhuCau_Paging
                        (
                            objMT.IsMua, 1, 10, arrlistSP,
                            objPQ.CanGoc,
                            objMT.IsCanGoc,
                            objPQ.DienTich,
                            objMT.DienTichTu,
                            objPQ.DuAn,
                            objMT.MaDA,
                            objPQ.DuongRong,
                            objMT.DuongRongTu,
                            objPQ.HuongCua,
                            objMT.MaMT,
                            objPQ.HuongBanCong,
                            objPQ.KhoangGia,
                            (objMT.GiaDen * objMT.LoaiTien.TyGia),
                            objPQ.LoaiBDS,
                            objMT.MaLBDS,
                            objPQ.LoaiDuong,
                            objPQ.OtoVao,
                            objMT.IsDeOto,
                            objPQ.PhapLy,
                            objPQ.PhongNgu,
                            objMT.PhNguTu,
                            objPQ.QuanHuyen,
                            objPQ.SoTang,
                            objMT.TangTu,
                            objMT.TangDen,
                            objPQ.ThangMay,
                            objMT.IsThangMay,
                            objPQ.TangHam,
                            objMT.IsTangHam,
                            objPQ.TienIch,
                            objPQ.MatTien,
                            objMT.MatTienTu,
                            objMT.MatTienDien,
                            objPQ.TenDuong,
                            objPQ.Tinh,
                            objMT.MaTinh,
                            objPQ.PVS,
                            objMT.PVSTu
                        )
                        .Select(p => new
                        {
                            #region bc
                            p.MauNen,
                            p.TenTT,
                            VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                            p.MaTT,
                            AnhBDS = p.AnhBDS == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
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
                            p.TenNC,
                            p.TenHuyen,
                            p.TenTinh,
                            p.TenXa,
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
                            p.TenLoaiTien,
                            p.DaiXD,
                            p.DonGia,
                            p.MaKH,
                            p.PhiMoiGioi,
                            p.TyLeMG,
                            p.MaNVKD,
                            p.MaNVQL,
                            p.HoTenNVMG,
                            p.MaNguon,
                            p.MaCD,
                            p.NgayCN,
                            p.NgayNhap,
                            p.DuAn,
                            //DienThoai = Common.Right(p.DienThoai, 3),
                            #endregion
                            #region Khop
                            p.IsCanGoc,
                            p.IsThangMay,
                            p.TangHam,
                            p.MaBC,
                            p.SoDK,
                            p.NgayDK,
                            p.ThoiHan,
                            p.TyLeHH,
                            HoTenKH = (p.MaNVKD == Common.StaffID || p.MaNVQL == Common.StaffID || GetAccessData() == 1) == true ? p.HoKH + " " + p.TenKH : "",
                            DienThoai = Common.Right1((p.MaNVKD == Common.StaffID || p.MaNVQL == Common.StaffID || GetAccessData() == 1) ? (p.MaKH == null ? "" : p.DiDong == "" ? "" : p.DiDong) : "", 3),
                            p.KyHieu,
                            p.DienTich,
                            p.ThanhTien,
                            DiaChi = GetAdress(p.SoNha, p.TenDuong, p.TenXa, p.TenHuyen, p.TenTinh),
                            p.TenPL,
                            p.PhongKhach,
                            p.PhongNgu,
                            p.PhongTam,
                            p.SoTang,
                            p.Dtich,
                            p.GiaTien,
                            p.MatTien,
                            p.Tang,
                            p.LauSo,
                            //HoTenNV = (p.MaNVKD == Library.Common.StaffID || p.MaNVQL == Library.Common.StaffID || GetAccessData() == 1) ? p.NhanVien.HoTen : "",
                            p.HoTenNV,
                            p.ChinhChu,
                            p.MaDA,
                            p.DuongRong,
                            p.MaLBDS,
                            p.TenLBDS,
                            p.DauOto,
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
                            #endregion
                        }).ToList();


                    #region linq
                    //if (objPQ.CanGoc.GetValueOrDefault())
                    //    objLoc = objLoc.Where(p => p.IsCanGoc == objMT.IsCanGoc).ToList();
                    //if (objPQ.DienTich.GetValueOrDefault())
                    //    objLoc = objLoc.Where(p => p.DienTich >= objMT.DienTichTu || p.Dtich >= objMT.DienTichTu).ToList();
                    //if (objPQ.DuAn.GetValueOrDefault())
                    //    objLoc = objLoc.Where(p => p.MaDA == objMT.MaDA).ToList();
                    //if (objPQ.DuongRong.GetValueOrDefault())
                    //    objLoc = objLoc.Where(p => p.DuongRong >= objMT.DuongRongTu).ToList();
                    //if (objPQ.HuongCua.GetValueOrDefault())
                    //    objLoc = objLoc.Where(p => objMT.mglmtHuongs.Count == 0 || objMT.mglmtHuongs.Select(h => h.MaHuong).Contains(p.MaHuong)).ToList();
                    //if (objPQ.HuongBanCong.GetValueOrDefault())
                    //    objLoc = objLoc.Where(p => objMT.mglmtHuongBCs.Count == 0 || objMT.mglmtHuongBCs.Select(h => h.MaHuong).Contains(p.HuongBanCong)).ToList();
                    //try
                    //{
                    //    if (objPQ.KhoangGia.GetValueOrDefault())
                    //        objLoc = objLoc.Where(p => p.ThanhTien <= (objMT.GiaDen * objMT.LoaiTien.TyGia) || p.GiaTien <= (objMT.GiaDen * objMT.LoaiTien.TyGia)).ToList();
                    //}
                    //catch { }
                    //if (objPQ.LoaiBDS.GetValueOrDefault())
                    //    objLoc = objLoc.Where(p => p.MaLBDS == objMT.MaLBDS).ToList();
                    //if (objPQ.LoaiDuong.GetValueOrDefault())
                    //    objLoc = objLoc.Where(p => objMT.mglmtLoaiDuongs.Count == 0 || objMT.mglmtLoaiDuongs.Select(ld => ld.MaLD).Contains(p.MaLD)).ToList();
                    //if (objPQ.OtoVao.GetValueOrDefault())
                    //    objLoc = objLoc.Where(p => p.DauOto == objMT.IsDeOto).ToList();
                    //if (objPQ.PhapLy.GetValueOrDefault())
                    //    objLoc = objLoc.Where(p => objMT.mglmtPhapLies.Count == 0 || objMT.mglmtPhapLies.Select(pl => pl.MaPL).Contains(p.MaPL)).ToList();
                    //if (objPQ.PhongNgu.GetValueOrDefault())
                    //    objLoc = objLoc.Where(p => p.PhongNgu >= objMT.PhNguTu).ToList();
                    //if (objPQ.QuanHuyen.GetValueOrDefault())
                    //    objLoc = objLoc = objLoc.Where(p => objMT.mglmtHuyens.Count == 0 || objMT.mglmtHuyens.Select(h => h.MaHuyen).Contains(p.MaHuyen)).ToList();
                    //if (objPQ.SoTang.GetValueOrDefault())
                    //    objLoc.Where(p => p.SoTang >= objMT.TangTu & (p.SoTang <= objMT.TangDen | objMT.TangDen == 0) || p.Tang >= objMT.TangTu & (p.Tang <= objMT.TangDen | objMT.TangDen == 0));
                    //if (objPQ.ThangMay.GetValueOrDefault())
                    //    objLoc = objLoc.Where(p => p.IsThangMay == objMT.IsThangMay).ToList();
                    //if (objPQ.TangHam.GetValueOrDefault())
                    //    objLoc = objLoc.Where(p => p.TangHam == objMT.IsTangHam).ToList();
                    ////if (objPQ.TienIch.GetValueOrDefault())
                    ////    objLoc.Where(p => objMT.mglmtTienIches.Count == 0 | objMT.mglmtTienIches.Select(h => h.MaTI).Contains(p.mglbcTienIches.Select(p=>p.MaTI).ToList()));

                    //if (objPQ.MatTien.GetValueOrDefault())
                    //    objLoc = objLoc.Where(p => p.NgangXD == objMT.MatTienTu || Convert.ToByte(p.MatTien) == objMT.MatTienTu).ToList();
                    //if (objPQ.TenDuong.GetValueOrDefault())
                    //    objLoc = objLoc.Where(p => objMT.mglmtDuongs.Count == 0 || objMT.mglmtDuongs.Select(d => d.MaDuong).Contains(p.MaDuong)).ToList();
                    //if (objPQ.Tinh.GetValueOrDefault())
                    //    objLoc = objLoc.Where(p => p.MaTinh == objMT.MaTinh).ToList();
                    #endregion

                    var objsp = (from sp in db.mglBCSanPhams
                                 join cv in db.mglBCCongViecs on sp.MaCV equals cv.ID
                                 where cv.MaCoHoiMT == objMT.MaMT
                                 select new itemLoai
                                 {
                                     MaBC = sp.MaSP
                                 }
                                     ).ToList();


                    foreach (var x in objsp)
                    {
                        if (objLoc.Where(p => p.MaBC == x.MaBC).ToList().Count() > 0)
                        {
                            var objremove = objLoc.FirstOrDefault(p => p.MaBC == x.MaBC);
                            objLoc.Remove(objremove);
                        }
                    }

                    gcBDS.DataSource = objLoc;
                    #endregion 3 số đầu
                }

                #region
                //else
                //{
                //    #region 3 số đầu
                //    //var objLoc = (from p in db.mglbcBanChoThues
                //    //              join tt in db.mglbcThongTinTTs on p.MaBC equals tt.MaBC
                //    //              join nvmg in db.NhanViens on p.MaNVKD equals nvmg.MaNV
                //    //              where (p.IsBan.GetValueOrDefault() == objMT.IsMua & (p.MaTT == 2 || p.MaTT == 0))
                //    //              orderby p.NgayDK descending
                //    //              select new
                //    //              {
                //    //                  #region bc
                //    //                  p.mglbcTrangThai.MauNen,
                //    //                  p.mglbcTrangThai.TenTT,
                //    //                  VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                //    //                  p.MaTT,
                //    //                  AnhBDS = db.mglbcAnhbds.Where(c => c.MaBC == p.MaBC).Count() == 0 ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                //    //                  // p.SoGiay,
                //    //                  p.NamXayDung,
                //    //                  p.TrangThaiHDMG,
                //    //                  p.MaTinhTrang,
                //    //                  p.HoTenNDD,
                //    //                  p.HoTenNTG,
                //    //                  p.NgangKV,
                //    //                  p.DaiKV,
                //    //                  p.SauKV,
                //    //                  p.IsBan,
                //    //                  TenNC = p.IsBan == true ? "Cần bán" : "Cho thuê",
                //    //                  p.Huyen.TenHuyen,
                //    //                  p.Tinh.TenTinh,
                //    //                  p.Xa.TenXa,
                //    //                  SoNhaaa = Common.SoNhaNEW((p.SoNha)),
                //    //                  p.PhongVS,
                //    //                  p.DienTichDat,
                //    //                  p.DienTichXD,
                //    //                  p.TenDuong,
                //    //                  p.SoTangXD,
                //    //                  p.ThoiGianHD,
                //    //                  p.ThoiGianBGMB,
                //    //                  p.GhiChu,
                //    //                  p.GioiThieu,
                //    //                  p.DonViDangThue,
                //    //                  p.DonViThueCu,
                //    //                  p.TienIch,
                //    //                  p.DacTrung,
                //    //                  p.KinhDo,
                //    //                  p.ViDo,
                //    //                  p.ToaDo,
                //    //                  p.LoaiTien.TenLoaiTien,
                //    //                  p.DaiXD,
                //    //                  p.DonGia,
                //    //                  p.MaKH,
                //    //                  PhiMoiGioi = p.PhiMG,
                //    //                  p.TyLeMG,
                //    //                  p.MaNVKD,
                //    //                  p.MaNVQL,
                //    //                  HoTenNVMG = nvmg.HoTen,
                //    //                  p.MaNguon,
                //    //                  p.MaCD,
                //    //                  p.NgayCN,
                //    //                  p.NgayNhap,
                //    //                  p.DuAn,
                //    //                  //DienThoai = Common.Right(p.DienThoai, 3),
                //    //                  #endregion
                //    //                  IsCanGoc = p.IsCanGoc.GetValueOrDefault(),
                //    //                  IsThangMay = p.IsThangMay.GetValueOrDefault(),
                //    //                  TangHam = p.TangHam.GetValueOrDefault(),
                //    //                  p.MaBC,
                //    //                  p.SoDK,
                //    //                  p.NgayDK,
                //    //                  p.ThoiHan,
                //    //                  p.TyLeHH,
                //    //                  HoTenKH = (p.MaNVKD == Common.StaffID || p.MaNVQL == Common.StaffID || GetAccessData() == 1) == true ? p.KhachHang.HoKH + " " + p.KhachHang.TenKH : "",
                //    //                  DienThoai = (p.MaNVKD == Common.StaffID || p.MaNVQL == Common.StaffID || GetAccessData() == 1) ? (p.MaKH == null ? "" : p.KhachHang.DiDong == "" ? "" : p.KhachHang.DiDong) : "",
                //    //                  p.KyHieu,
                //    //                  p.DienTich,
                //    //                  ThanhTien = (p.ThanhTien * p.LoaiTien.TyGia),
                //    //                  DiaChi = GetAdress(p.MaBC),
                //    //                  TenPL = p.PhapLy != null ? p.PhapLy.TenPL : "",
                //    //                  p.PhongKhach,
                //    //                  p.PhongNgu,
                //    //                  p.PhongTam,
                //    //                  p.SoTang,
                //    //                  Dtich = tt.DienTich,
                //    //                  GiaTien = tt.GiaTien,
                //    //                  MatTien = tt.MatTien,
                //    //                  Tang = tt.SoTang,
                //    //                  p.LauSo,
                //    //                  //HoTenNV = (p.MaNVKD == Library.Common.StaffID || p.MaNVQL == Library.Common.StaffID || GetAccessData() == 1) ? p.NhanVien.HoTen : "",
                //    //                  HoTenNV = p.NhanVien.HoTen,
                //    //                  p.ChinhChu,
                //    //                  p.MaDA,
                //    //                  p.DuongRong,
                //    //                  p.MaLBDS,
                //    //                  p.LoaiBD.TenLBDS,
                //    //                  DauOto = p.DauOto.GetValueOrDefault(),
                //    //                  p.ThuongLuong,
                //    //                  p.HuongBanCong,
                //    //                  p.MaHuong,
                //    //                  p.MaLD,
                //    //                  p.MaHuyen,
                //    //                  p.MaPL,
                //    //                  p.PhiMG,
                //    //                  p.NgangXD,
                //    //                  p.MaTinh,
                //    //                  p.MaDuong
                //    //              }).ToList().Where(p => listSP.Contains(p.MaBC) == false);
                //    //var objPQ = db.mglCaiDatTimKiems.FirstOrDefault(p => p.MaNV == Common.StaffID);
                //    //if (objPQ == null)
                //    //{
                //    //    DialogBox.Infomation("Bạn chưa cài đặt tiêu chí lọc sản phẩm");
                //    //    return;
                //    //}
                //    //if (objPQ == null)
                //    //{
                //    //    DialogBox.Infomation("Vui lòng cài đặt quy định lọc thông tin cho tài khoản này!");
                //    //    return;
                //    //}
                //    //if (objPQ.CanGoc.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.IsCanGoc == objMT.IsCanGoc).ToList();
                //    //if (objPQ.DienTich.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.DienTich >= objMT.DienTichTu || p.Dtich >= objMT.DienTichTu).ToList();
                //    //if (objPQ.DuAn.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.MaDA == objMT.MaDA).ToList();
                //    //if (objPQ.DuongRong.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.DuongRong >= objMT.DuongRongTu).ToList();
                //    //if (objPQ.HuongCua.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => objMT.mglmtHuongs.Count == 0 || objMT.mglmtHuongs.Select(h => h.MaHuong).Contains(p.MaHuong)).ToList();
                //    //if (objPQ.HuongBanCong.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => objMT.mglmtHuongBCs.Count == 0 || objMT.mglmtHuongBCs.Select(h => h.MaHuong).Contains(p.HuongBanCong)).ToList();
                //    //try
                //    //{
                //    //    if (objPQ.KhoangGia.GetValueOrDefault())
                //    //        objLoc = objLoc.Where(p => p.ThanhTien <= (objMT.GiaDen * objMT.LoaiTien.TyGia) || p.GiaTien <= (objMT.GiaDen * objMT.LoaiTien.TyGia)).ToList();
                //    //}
                //    //catch { }
                //    //if (objPQ.LoaiBDS.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.MaLBDS == objMT.MaLBDS).ToList();
                //    //if (objPQ.LoaiDuong.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => objMT.mglmtLoaiDuongs.Count == 0 || objMT.mglmtLoaiDuongs.Select(ld => ld.MaLD).Contains(p.MaLD)).ToList();
                //    //if (objPQ.OtoVao.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.DauOto == objMT.IsDeOto).ToList();
                //    //if (objPQ.PhapLy.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => objMT.mglmtPhapLies.Count == 0 || objMT.mglmtPhapLies.Select(pl => pl.MaPL).Contains(p.MaPL)).ToList();
                //    //if (objPQ.PhongNgu.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.PhongNgu >= objMT.PhNguTu).ToList();
                //    //if (objPQ.QuanHuyen.GetValueOrDefault())
                //    //    objLoc = objLoc = objLoc.Where(p => objMT.mglmtHuyens.Count == 0 || objMT.mglmtHuyens.Select(h => h.MaHuyen).Contains(p.MaHuyen)).ToList();
                //    //if (objPQ.SoTang.GetValueOrDefault())
                //    //    objLoc.Where(p => p.SoTang >= objMT.TangTu & (p.SoTang <= objMT.TangDen | objMT.TangDen == 0) || p.Tang >= objMT.TangTu & (p.Tang <= objMT.TangDen | objMT.TangDen == 0));
                //    //if (objPQ.ThangMay.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.IsThangMay == objMT.IsThangMay).ToList();
                //    //if (objPQ.TangHam.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.TangHam == objMT.IsTangHam).ToList();
                //    ////if (objPQ.TienIch.GetValueOrDefault())
                //    ////    objLoc.Where(p => objMT.mglmtTienIches.Count == 0 | objMT.mglmtTienIches.Select(h => h.MaTI).Contains(p.mglbcTienIches.Select(p=>p.MaTI).ToList()));

                //    //if (objPQ.MatTien.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.NgangXD == objMT.MatTienTu || Convert.ToByte(p.MatTien) == objMT.MatTienTu).ToList();
                //    //if (objPQ.TenDuong.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => objMT.mglmtDuongs.Count == 0 || objMT.mglmtDuongs.Select(d => d.MaDuong).Contains(p.MaDuong)).ToList();
                //    //if (objPQ.Tinh.GetValueOrDefault())
                //    //    objLoc = objLoc.Where(p => p.MaTinh == objMT.MaTinh).ToList();
                //    //gcBDS.DataSource = objLoc;
                //    #endregion
                //}
                #endregion
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }


        public class itemLoai
        {
            public int? MaBC { get; set; }
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

                        var listNhatKy = (from p in db.mglmtNhatKyXuLies
                                          join nv in db.NhanViens on p.MaNVG equals nv.MaNV into nhanvien
                                          from nv in nhanvien.DefaultIfEmpty()
                                          join nv1 in db.NhanViens on p.MaNVN equals nv1.MaNV into nhanvien1
                                          from nv1 in nhanvien1.DefaultIfEmpty()
                                          join pt in db.PhuongThucXuLies on p.MaPT equals pt.MaPT into phuongthuc
                                          from pt in phuongthuc.DefaultIfEmpty()
                                          where p.MaMT == maMT
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
                        var obj = db.mglmtPhanQuyens.Single(p => p.MaNV == Common.StaffID);

                        if (obj.DienThoai == false)
                        {
                            gridControlAvatar.DataSource = db.NguoiDaiDien_getByMaKH((int?)grvMuaThue.GetFocusedRowCellValue("MaKH")).Select(mt => new
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
                                mt.DiaChiTT,
                                mt.HoTenNV,
                                mt.NgayNhap
                            }).ToList();
                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            gridControlAvatar.DataSource = db.NguoiDaiDien_getByMaKH((int?)grvMuaThue.GetFocusedRowCellValue("MaKH")).Select(mt => new
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
                                mt.DiaChiTT,
                                mt.HoTenNV,
                                mt.NgayNhap
                            }).ToList();
                        }
                        else if (obj.DienThoaiAn == false)
                        {
                            gridControlAvatar.DataSource = db.NguoiDaiDien_getByMaKH((int?)grvMuaThue.GetFocusedRowCellValue("MaKH")).Select(mt => new
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
                                mt.DiaChiTT,
                                mt.HoTenNV,
                                mt.NgayNhap
                            }).ToList();
                        }
                        else
                            gridControlAvatar.DataSource = db.NguoiDaiDien_getByMaKH((int?)grvMuaThue.GetFocusedRowCellValue("MaKH")).ToList();
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
                        #endregion
                        break;

                    case 7:
                        gcMatBangDaChao.DataSource = db.mglbcBanChoThue_List_byID((int?)grvMuaThue.GetFocusedRowCellValue("MaMT"));
                        break;
                    case 17:                       
                        var count = db.mglmtHistoryChanges.Where(p => p.MaMT == maMT).Count();
                        if (count > 0)
                        {
                            switch (GetAccessDataHistory())
                            {
                                case 1://Tat ca
                                    gcHistoryChange.DataSource = db.mglLichSuGhiNhanMT(maMT, -1, -1, -1, Common.StaffID, true);
                                    break;
                                case 2://Theo phong ban 
                                    gcHistoryChange.DataSource = db.mglLichSuGhiNhanMT(maMT, -1, -1, Common.DepartmentID, Common.StaffID, true);
                                    break;
                                case 3://Theo nhom
                                    gcHistoryChange.DataSource = db.mglLichSuGhiNhanMT(maMT, -1, Common.GroupID, -1, Common.StaffID, true);
                                    break;
                                case 4://Theo nhan vien
                                    gcHistoryChange.DataSource = db.mglLichSuGhiNhanMT(maMT, Common.StaffID, -1, -1, Common.StaffID, true);
                                    break;
                                default:
                                    gcHistoryChange.DataSource = null;
                                    break;
                            }
                        }
                        else
                        {
                            gcHistoryChange.DataSource = null;
                        }
                        break;

                }
            }
            catch { }
        }

        int GetAccessDataHistory()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 228);

            return o.SDB.SDBID;
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
                    case "Điện thoại 2":
                        colDienThoai2.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Điện thoại 3":
                        colDienThoai3.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Điện thoại 4":
                        colDienThoai4.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Mô hình cũ":
                        colMoHinhCu.VisibleIndex = stt[i].STT.Value;
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
                //if (obj.DienThoaiAn == false)
                //    colDienThoai.Visible = false;
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
            chkTrangThai.DataSource = db.mglmtTrangThais;
            lookLoaiBDS.DataSource = db.LoaiBDs;
            lookHuong.DataSource = db.PhuongHuongs;
            lookLoaiDuong.DataSource = db.LoaiDuongs;
            lookPhapLy.DataSource = db.PhapLies;
            lookCapDo.DataSource = db.mglCapDos;
            lookNguon.DataSource = db.mglNguons;
            lookTinhTrangHD.DataSource = db.mglbcTrangThaiHDMGs;
            lookTinhTrang.DataSource = db.mglTinhTrangs;
            lkTrangThai.DataSource = db.mglmtTrangThais;
            LoadPermission(); LoadPermissionLichSuLV();
            Mail();
            phanquyen();
            PhanQuyenThemSuaKH();
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(4);
            ThuTuCot();

            itemTuNgay.EditValue = new DateTime(2012, 5, 28);
            grvMuaThue.BestFitColumns();
        }
        void Mail()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 197;
            DataTable tblAction = o.SelectBy();
            itemSendMail.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            itemSendMail.Enabled = true;
                            break;
                    }
                }
            }
        }
        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (itemTrangThai.EditValue != "Trạng thái" && itemTrangThai.EditValue != "")
            {
                db = new MasterDataContext();
                MuaThue_Load();
                phanquyen();
                // grvMuaThue.BestFitColumns();
            }
        }

        private void itmThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmEdit frm = new frmEdit();
            frm.NhuCau = false;
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
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            //MuaThue_Load();
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
            GiaoDich.frmEdit frm = new GiaoDich.frmEdit();
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
            GiaoDich.frmEdit frm = new GiaoDich.frmEdit();
            frm.MaMT = maMT;
            frm.MaBC = maBC.Value;
            frm.ShowDialog();
        }

        private void itemTrangThai_EditValueChanged(object sender, EventArgs e)
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

            //MuaThue_Load();
            //phanquyen();
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
                frm.MaBC = objBC.MaBC;
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

            if (lstChon.Count == 0)
            {
                DialogBox.Infomation("Bạn vui lòng chọn khách hàng để gửi Email!");
                return;
            }

            //if (grvBDS.FocusedRowHandle < 0)
            //{
            //    DialogBox.Infomation("Bạn vui lòng chọn khách hàng để gửi Email!");
            //    return;
            //}

            var tt = new MGL.ThongTinGui();
            tt.ID = 1;
            tt.ShowDialog();

            db = new MasterDataContext();

            int MaKH = (int)grvMuaThue.GetFocusedRowCellValue("MaKH");
            //int MaMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
            var objKH = db.KhachHangs.SingleOrDefault(o => o.MaKH == MaKH);
            //var objMT = db.mglmtMuaThues.SingleOrDefault(o => o.MaMT == MaMT);

            var mailcf = db.mailConfigs.Where(p => p.StaffID == Common.StaffID).FirstOrDefault();

            #region noi dung gui
            string noidung = "";
            //var indexs = grvBDS.GetSelectedRows();

            if (!string.IsNullOrEmpty(mailcf.Logo1))
                noidung += "<img  src='" + mailcf.Logo1 + "'/>";

            noidung += "<p><b>Kính gửi " + (objKH.IsPersonal == true ? objKH.HoKH + " " + objKH.TenKH : objKH.TenCongTy) + "</b></p>";
            noidung += "<p>" + "Công ty cổ phần HoaLand gửi Anh thông tin mặt bằng phù hợp với mô hình <b>" + (objKH.IsPersonal == true ? objKH.MoHinh : objKH.ThuongHieu) + "</b>: " + "</p>";
            int j = 0;
            foreach (var i in lstChon)
            {
                j++;
                var objBC = db.mglbcBanChoThues.Single(p => p.MaBC == (int)grvBDS.GetRowCellValue(i, "MaBC"));
                var objTT = db.mglThongTinGuis.Single(p => p.ID == tt.ID);

                #region check to hop dieu kien

                if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                    noidung += "<p><b>" + j + ". " + objBC.SoNha + "</b>" + "</p>";
                else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                    noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + "</b>" + "</p>";
                else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                    noidung += "<p><b>" + j + ". " + objBC.Xa.TenXa + "</b>" + "</p>";
                else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                    noidung += "<p><b>" + j + ". " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                    noidung += "<p><b>" + j + ". " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                    noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + "</b>" + "</p>";
                else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                    noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + "</b>" + "</p>";
                else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                    noidung += "<p><b>" + j + ". " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                    noidung += "<p><b>" + j + ". " + objBC.Huyen.TenHuyen + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                    noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + "</b>" + "</p>";
                else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                    noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                    noidung += "<p><b>" + j + ". " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen +
                        ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                    noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                    noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen +
                        ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                    noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen +
                        ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                    noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Xa.TenXa + "</b>" + "</p>";
                else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                    noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                    noidung += "<p><b>" + j + ". " + objBC.Xa.TenXa + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                    noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                    noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                    noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                    noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + "</b>" + "</p>";
                else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                    noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Huyen.TenHuyen + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                    noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Huyen.TenHuyen + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                    noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen +
                        ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                    noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Xa.TenXa + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                    noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                    noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                    noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Huyen.TenHuyen + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                    noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                    noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                    noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                #endregion

                if (objTT.MatTien == true)
                    noidung += "<p> - Mặt tiền: " + string.Format("{0:#,0.##}", objBC.NgangXD) + "m " + "</p>";

                if (objTT.DienTich == true && objTT.SoTang == true)
                    noidung += "<p> - Diện tích: " + string.Format("{0:#,0.##}", objBC.DienTich) + "m2 " + " x Số tầng: " + objBC.SoTang + "</p>";
                else if (objTT.DienTich == true && objTT.SoTang == false)
                    noidung += "<p> - Diện tích: " + string.Format("{0:#,0.##}", objBC.DienTich) + "m2 </p>";
                else if (objTT.DienTich == false && objTT.SoTang == true)
                    noidung += "<p> - Số tầng: " + objBC.SoTang + " </p> ";


                if (objTT.TongGia == true)
                    noidung += "<p> - Tổng Giá " + string.Format("{0:#,0.##}", objBC.ThanhTien) + " " + objBC.LoaiTien.TenLoaiTien + "</p>";
                if (objTT.MoTa == true)
                    noidung += "<p>Mô tả: " + objBC.DacTrung + "</p>";
                if (objTT.GhiChu == true)
                    noidung += "<p>Ghi chú: " + objBC.GhiChu + "</p>";
                if (objTT.LinkAnh == true)
                {
                    if (!string.IsNullOrEmpty(objBC.LinkAnh))
                        noidung += "<p>Hình ảnh xem tại: <a href='" + objBC.LinkAnh + "'>Click here</a></p>";
                    else
                        noidung += "<p>Hình ảnh xem tại: Không có link</p>";
                }
                if (objTT.LinkViTri == true)
                {
                    if (!string.IsNullOrEmpty(objBC.ToaDo))
                    {
                        noidung += "<p>Vị trí xem tại: <a href='https://www.google.com/maps/place/" + objBC.ToaDo.Replace("\"", "").Replace("'", " ") + "'>Click here</a></p>";
                    }
                    else
                        noidung += "<p>Vị trí xem tại: Không có link</p>";
                }
                //< a href = 'https://www.google.com/maps/place/20°59 33.8N 105°50 17.2E' > Click here </ a >
            }
            noidung += "<p>Trân trọng cảm ơn!</p>";
            if (!string.IsNullOrEmpty(mailcf.Logo2))
                noidung += "<img src='" + mailcf.Logo2 + "'/>";
            #endregion

            //var objMT = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == (int)grvBDS.GetFocusedRowCellValue("MaBC"));
            using (var frm = new BEE.HoatDong.MGL.frmSend() { objKH = objKH })
            {
                frm.noidung = noidung;
                frm.ShowDialog();
            }
        }

        private void itemChangeStatus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            if (gridAvatar.GetFocusedRowCellValue("MaKH") != null)
            {
                try
                {
                    NguoiDaiDien_frm frm = new NguoiDaiDien_frm();
                    frm.MaKH = (int)gridAvatar.GetFocusedRowCellValue("MaKH");
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
            if (gridAvatar.GetFocusedRowCellValue("MaKH") != null)
            {
                try
                {
                    if (DialogBox.Question("Bạn có chắc chắn muốn xóa [Người đại diện] này không?") == DialogResult.Yes)
                    {
                        it.NguoiDaiDienCls o = new it.NguoiDaiDienCls();
                        o.MaKH = (int)gridAvatar.GetFocusedRowCellValue("MaKH");
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

        private void itemTrangThai_EditValueChanged_1(object sender, EventArgs e)
        {
            //if (itemTrangThai.EditValue != "")
            //{
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
            //    MuaThue_Load();
            //    phanquyen();
            //}
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmImport())
            {
                frm.ShowDialog();
            }
        }

        private void linkToaDo_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void linkToaDo_Click(object sender, EventArgs e)
        {
            var toado = grvBDS.GetFocusedRowCellValue("ToaDo");
            if (toado != "")
                System.Diagnostics.Process.Start("https://www.google.com/maps/place/" + toado);
        }

        private void btnChon_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "chon")
            {
                var viewinfo = (DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo)grvBDS.GetViewInfo();
                var rowinfo = viewinfo.GetGridRowInfo(grvBDS.FocusedRowHandle);
                if (rowinfo == null)
                    return;
                var backcolor = rowinfo.Appearance.BackColor;
                if (backcolor == Color.Lime)
                {
                    if (lstChon.Contains(grvBDS.FocusedRowHandle))
                        lstChon.Remove(grvBDS.FocusedRowHandle);
                }
                else
                {
                    if (!lstChon.Contains(grvBDS.FocusedRowHandle))
                        lstChon.Add(grvBDS.FocusedRowHandle);
                }
            }
        }

        private void grvBDS_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (!grvBDS.IsDataRow(e.RowHandle))
                return;
            if (lstChon.Contains(e.RowHandle))
                e.Appearance.BackColor = Color.Lime;
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

            var frC = new BEE.VOIPSETUP.Call.frmCallCan();
            frC.SDT = sdt;
            frC.isNLH = isNLH;
            frC.MaKH = MaKH;
            frC.sdtHiden = sdthidden;
            frC.MaMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
            // frC.ModuleID = 0;
            frC.line = objLine.LineNumber;
            frC.NhanVienTN = Common.StaffName;
            frC.LoaiCG = 0;
            frC.ShowDialog();
        }
        private void btnCall_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var makh = (int?)grvMuaThue.GetFocusedRowCellValue("MaKH");
            var namecol = grvMuaThue.FocusedColumn.Name;
            string sdt = "";
            string sdthidden = "";
            var objkh = db.KhachHangs.SingleOrDefault(p => p.MaKH == makh);
            objkh.ExEndCall = db.voipLineConfigs.SingleOrDefault(p => p.MaNV == Common.StaffID).LineNumber;
            db.SubmitChanges();
            switch (namecol)
            {
                case "colDienThoai":
                    sdt = objkh.DiDong;
                    sdthidden = grvMuaThue.GetFocusedRowCellValue("DienThoai").ToString();
                    break;
                case "colDienThoai2":
                    sdt = objkh.DiDong2;
                    sdthidden = grvMuaThue.GetFocusedRowCellValue("DiDong2").ToString();
                    break;
                case "colDienThoai3":
                    sdt = objkh.DiDong3;
                    sdthidden = grvMuaThue.GetFocusedRowCellValue("DiDong3").ToString();
                    break;
                case "colDienThoai4":
                    sdt = objkh.DiDong4;
                    sdthidden = grvMuaThue.GetFocusedRowCellValue("DiDong4").ToString();
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


        private void itemEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db = new MasterDataContext();
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



            int MaKH = (int)grvMuaThue.GetFocusedRowCellValue("MaKH");
            //int MaMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
            var objKH = db.KhachHangs.SingleOrDefault(o => o.MaKH == MaKH);
            //var objMT = db.mglmtMuaThues.SingleOrDefault(o => o.MaMT == MaMT);
            mailConfig mailcf;
            try
            {
                mailcf = db.mailConfigs.Where(p => p.StaffID == Common.StaffID).FirstOrDefault();
                if (mailcf == null)
                {
                    DialogBox.Error("Bạn chưa cấu hình Email gửi, vui lòng kiểm tra lại");
                    return;
                }
            }
            catch
            {
                DialogBox.Error("Bạn chưa cấu hình Email gửi, vui lòng kiểm tra lại");
                return;
            }

            var tt = new MGL.ThongTinGui();
            tt.ID = 1;
            tt.ShowDialog();


            string noidung = "";
            //var indexs = grvBDS.GetSelectedRows();

            try
            {
                if (!string.IsNullOrEmpty(mailcf.Logo1))
                    noidung += "<img  src='" + mailcf.Logo1 + "'/>";

                noidung += "<p><b>Kính gửi " + (objKH.IsPersonal == true ? objKH.HoKH + " " + objKH.TenKH : objKH.TenCongTy) + "</b></p>";
                noidung += "<p>" + "Công ty cổ phần HoaLand gửi Anh thông tin mặt bằng phù hợp với mô hình <b>" + (objKH.IsPersonal == true ? objKH.MoHinh : objKH.ThuongHieu) + "</b>: " + "</p>";
            }
            catch
            {

                DialogBox.Error("Bạn chưa cấu hình Email gửi, vui lòng kiểm tra lại");
                return;
            }

            int j = 0;
            try
            {
                foreach (var i in lstChon)
                {
                    j++;
                    var objBC = db.mglbcBanChoThues.Single(p => p.MaBC == (int)grvBDS.GetRowCellValue(i, "MaBC"));
                    var objTT = db.mglThongTinGuis.Single(p => p.ID == 1);

                    #region check to hop dieu kien

                    if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.Xa.TenXa + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.Huyen.TenHuyen + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen +
                            ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen +
                            ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen +
                            ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Xa.TenXa + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.Xa.TenXa + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Huyen.TenHuyen + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Huyen.TenHuyen + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen +
                            ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Xa.TenXa + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Huyen.TenHuyen + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    #endregion

                    if (objTT.MatTien == true)
                        noidung += "<p> - Mặt tiền: " + string.Format("{0:#,0.##}", objBC.NgangXD) + "m " + "</p>";

                    if (objTT.DienTich == true && objTT.SoTang == true)
                        noidung += "<p> - Diện tích: " + string.Format("{0:#,0.##}", objBC.DienTich) + "m2 " + " x Số tầng: " + objBC.SoTang + "</p>";
                    else if (objTT.DienTich == true && objTT.SoTang == false)
                        noidung += "<p> - Diện tích: " + string.Format("{0:#,0.##}", objBC.DienTich) + "m2 </p>";
                    else if (objTT.DienTich == false && objTT.SoTang == true)
                        noidung += "<p> - Số tầng: " + objBC.SoTang + " </p> ";


                    if (objTT.TongGia == true)
                        noidung += "<p> - Tổng Giá " + string.Format("{0:#,0.##}", objBC.ThanhTien) + " " + objBC.LoaiTien.TenLoaiTien + "</p>";
                    if (objTT.MoTa == true)
                        noidung += "<p>Mô tả: " + objBC.DacTrung + "</p>";
                    if (objTT.GhiChu == true)
                        noidung += "<p>Ghi chú: " + objBC.GhiChu + "</p>";
                    if (objTT.LinkAnh == true)
                    {
                        if (!string.IsNullOrEmpty(objBC.LinkAnh))
                            noidung += "<p>Hình ảnh xem tại: <a href='" + objBC.LinkAnh + "'>Click here</a></p>";
                        else
                            noidung += "<p>Hình ảnh xem tại: Không có link</p>";
                    }
                    if (objTT.LinkViTri == true)
                    {
                        if (!string.IsNullOrEmpty(objBC.ToaDo))
                        {
                            noidung += "<p>Vị trí xem tại: <a href='https://www.google.com/maps/place/" + objBC.ToaDo.Replace("\"", "").Replace("'", " ") + "'>Click here</a></p>";
                        }
                        else
                            noidung += "<p>Vị trí xem tại: Không có link</p>";
                    }
                    //< a href = 'https://www.google.com/maps/place/20°59 33.8N 105°50 17.2E' > Click here </ a >
                }
            }
            catch
            {
            }

            try
            {
                noidung += "<p>Trân trọng cảm ơn!</p>";
                if (!string.IsNullOrEmpty(mailcf.Logo2))
                    noidung += "<img src='" + mailcf.Logo2 + "'/>";
            }
            catch
            {

            }




            //var objMT = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == (int)grvBDS.GetFocusedRowCellValue("MaBC"));
            using (var frm = new BEE.HoatDong.MGL.frmSend() { objKH = objKH })
            {
                frm.noidung = noidung;
                frm.ShowDialog();
            }


            // lưu dữ liệu

            var objmgbccv = new mglBCCongViec();
            objmgbccv.MaCoHoiMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
            objmgbccv.MaNV = Common.StaffID;
            objmgbccv.NgayNhap = DateTime.Now;
            objmgbccv.DienGiai = "Gửi Mail";
            objmgbccv.MaTT = 1;
            objmgbccv.MaNVXL = Common.StaffID;
            objmgbccv.NgayXuLy = DateTime.Now;
            db.mglBCCongViecs.InsertOnSubmit(objmgbccv);

            foreach (var x in lstChon)
            {
                var itemsave = new mglBCSanPham();
                var id = (int?)grvBDS.GetRowCellValue(x, "MaBC");
                itemsave.MaSP = id;
                itemsave.mglBCCongViec = objmgbccv;
                itemsave.MaTT = 1;
                itemsave.MaHT = 1; // gửi mail
                itemsave.MaLoai = 1; // chào khách hàng
                db.mglBCSanPhams.InsertOnSubmit(itemsave);

            }
            db.SubmitChanges();


        }

        private void itemSMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db = new MasterDataContext();
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



            int MaKH = (int)grvMuaThue.GetFocusedRowCellValue("MaKH");
            //int MaMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
            var objKH = db.KhachHangs.SingleOrDefault(o => o.MaKH == MaKH);
            //var objMT = db.mglmtMuaThues.SingleOrDefault(o => o.MaMT == MaMT);


            var tt = new MGL.ThongTinGui();
            tt.ID = 1;
            tt.ShowDialog();


            string noidung = "";
            //var indexs = grvBDS.GetSelectedRows();

            try
            {


                noidung += "<p><b>Kính gửi " + (objKH.IsPersonal == true ? objKH.HoKH + " " + objKH.TenKH : objKH.TenCongTy) + "</b></p>";
                noidung += "<p>" + "Công ty cổ phần HoaLand gửi Anh thông tin mặt bằng phù hợp với mô hình <b>" + (objKH.IsPersonal == true ? objKH.MoHinh : objKH.ThuongHieu) + "</b>: " + "</p>";
            }
            catch
            {

                DialogBox.Error("Bạn chưa cấu hình Email gửi, vui lòng kiểm tra lại");
                return;
            }

            int j = 0;
            try
            {
                foreach (var i in lstChon)
                {
                    j++;
                    var objBC = db.mglbcBanChoThues.Single(p => p.MaBC == (int)grvBDS.GetRowCellValue(i, "MaBC"));
                    var objTT = db.mglThongTinGuis.Single(p => p.ID == 1);

                    #region check to hop dieu kien

                    if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.Xa.TenXa + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.Huyen.TenHuyen + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen +
                            ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen +
                            ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen +
                            ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Xa.TenXa + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.Xa.TenXa + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Huyen.TenHuyen + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Huyen.TenHuyen + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen +
                            ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Xa.TenXa + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == false && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == true && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Huyen.TenHuyen + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == true && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Xa.TenXa + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == false && objTT.PhuongXa == true && objTT.QuanHuyen == true && objTT.TinhThanhPho == false)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Xa.TenXa + ", " + objBC.Huyen.TenHuyen + "</b>" + "</p>";
                    else if (objTT.SoNha == true && objTT.TenDuong == true && objTT.PhuongXa == false && objTT.QuanHuyen == false && objTT.TinhThanhPho == true)
                        noidung += "<p><b>" + j + ". " + objBC.SoNha + ", " + objBC.Duong.TenDuong + ", " + objBC.Tinh.TenTinh + "</b>" + "</p>";
                    #endregion

                    if (objTT.MatTien == true)
                        noidung += "<p> - Mặt tiền: " + string.Format("{0:#,0.##}", objBC.NgangXD) + "m " + "</p>";

                    if (objTT.DienTich == true && objTT.SoTang == true)
                        noidung += "<p> - Diện tích: " + string.Format("{0:#,0.##}", objBC.DienTich) + "m2 " + " x Số tầng: " + objBC.SoTang + "</p>";
                    else if (objTT.DienTich == true && objTT.SoTang == false)
                        noidung += "<p> - Diện tích: " + string.Format("{0:#,0.##}", objBC.DienTich) + "m2 </p>";
                    else if (objTT.DienTich == false && objTT.SoTang == true)
                        noidung += "<p> - Số tầng: " + objBC.SoTang + " </p> ";


                    if (objTT.TongGia == true)
                        noidung += "<p> - Tổng Giá " + string.Format("{0:#,0.##}", objBC.ThanhTien) + " " + objBC.LoaiTien.TenLoaiTien + "</p>";
                    if (objTT.MoTa == true)
                        noidung += "<p>Mô tả: " + objBC.DacTrung + "</p>";
                    if (objTT.GhiChu == true)
                        noidung += "<p>Ghi chú: " + objBC.GhiChu + "</p>";
                    if (objTT.LinkAnh == true)
                    {
                        if (!string.IsNullOrEmpty(objBC.LinkAnh))
                            noidung += "<p>Hình ảnh xem tại: <a href='" + objBC.LinkAnh + "'>Click here</a></p>";
                        else
                            noidung += "<p>Hình ảnh xem tại: Không có link</p>";
                    }
                    if (objTT.LinkViTri == true)
                    {
                        if (!string.IsNullOrEmpty(objBC.ToaDo))
                        {
                            noidung += "<p>Vị trí xem tại: <a href='https://www.google.com/maps/place/" + objBC.ToaDo.Replace("\"", "").Replace("'", " ") + "'>Click here</a></p>";
                        }
                        else
                            noidung += "<p>Vị trí xem tại: Không có link</p>";
                    }
                    //< a href = 'https://www.google.com/maps/place/20°59 33.8N 105°50 17.2E' > Click here </ a >
                }
            }
            catch
            {
            }

            bool? isDaGui;
            //var objMT = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == (int)grvBDS.GetFocusedRowCellValue("MaBC"));
            using (var frm = new BEE.HoatDong.MGL.frmSendSMS() { objKH = objKH })
            {
                frm.noidung = noidung;
                frm.ShowDialog();
                isDaGui = frm.isDaGui;
            }


            // lưu dữ liệu
            if (isDaGui == true)
            {
                var objmgbccv = new mglBCCongViec();
                objmgbccv.MaCoHoiMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
                objmgbccv.MaNV = Common.StaffID;
                objmgbccv.NgayNhap = DateTime.Now;
                objmgbccv.DienGiai = "Gửi SMS";
                objmgbccv.MaTT = 1;
                objmgbccv.MaNVXL = Common.StaffID;
                objmgbccv.NgayXuLy = DateTime.Now;
                db.mglBCCongViecs.InsertOnSubmit(objmgbccv);

                foreach (var x in lstChon)
                {
                    var itemsave = new mglBCSanPham();
                    var id = (int?)grvBDS.GetRowCellValue(x, "MaBC");
                    itemsave.MaSP = id;
                    itemsave.mglBCCongViec = objmgbccv;
                    itemsave.MaTT = 1;
                    itemsave.MaHT = 2; // gửi sms
                    itemsave.MaLoai = 1; // chào khách hàng
                    db.mglBCSanPhams.InsertOnSubmit(itemsave);

                }
                db.SubmitChanges();
            }


        }

        private void itemCall_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemOther_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lstChon.Count <= 0)
            {
                DialogBox.Error("Vui lòng chọn dữ liệu");
                return;
            }
            var frm = new frmOther();
            frm.ShowDialog();
            if (frm.isSave == true)
            {
                var objmgbccv = new mglBCCongViec();
                objmgbccv.MaCoHoiMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
                objmgbccv.MaNV = Common.StaffID;
                objmgbccv.NgayNhap = DateTime.Now;
                objmgbccv.DienGiai = "Khác";
                objmgbccv.MaTT = 1;
                objmgbccv.MaNVXL = Common.StaffID;
                objmgbccv.NgayXuLy = DateTime.Now;
                db.mglBCCongViecs.InsertOnSubmit(objmgbccv);

                foreach (var x in lstChon)
                {
                    var itemsave = new mglBCSanPham();
                    var id = (int?)grvBDS.GetRowCellValue(x, "MaBC");
                    itemsave.MaSP = id;
                    itemsave.mglBCCongViec = objmgbccv;
                    itemsave.MaTT = 1;
                    itemsave.MaHT = 4; // gửi sms
                    itemsave.MaLoai = 1; // chào khách hàng
                    db.mglBCSanPhams.InsertOnSubmit(itemsave);
                    db.SubmitChanges();
                    var itemother = new mglLichSuOther();
                    itemother.MaBCSP = itemsave.ID;
                    itemother.MaNV = Common.StaffID;
                    itemother.MaCV = objmgbccv.ID;
                    itemother.NgayNhap = DateTime.Now;
                    itemother.NoiDung = frm.NoiDung;
                    db.mglLichSuOthers.InsertOnSubmit(itemother);
                    db.SubmitChanges();

                }

            }



        }

        private void btnCallDC_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                var makh = (int?)grvBDS.GetFocusedRowCellValue("MaKH");
                var namecol = grvMuaThue.FocusedColumn.Name;
                string sdt = "";
                string sdthidden = "";
                var objkh = db.KhachHangs.SingleOrDefault(p => p.MaKH == makh);
                objkh.ExEndCall = db.voipLineConfigs.SingleOrDefault(p => p.MaNV == Common.StaffID).LineNumber;
                db.SubmitChanges();

                var phonehidden = (sender as ButtonEdit).Text ?? "";
                if (phonehidden.Trim() == "") return;
                try
                {
                    ClickToCall(objkh.DiDong);
                    KhaoSat(objkh.DiDong, sdthidden, false, makh);
                }
                catch { }

            }
            catch
            {
                DialogBox.Error("Vui lòng kiểm tra lại cấu hình line");
                return;

            }

            var objmgbccv = new mglBCCongViec();
            objmgbccv.MaCoHoiMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
            objmgbccv.MaNV = Common.StaffID;
            objmgbccv.NgayNhap = DateTime.Now;
            objmgbccv.DienGiai = "Gọi điện";
            objmgbccv.MaTT = 1;
            objmgbccv.MaNVXL = Common.StaffID;
            objmgbccv.NgayXuLy = DateTime.Now;
            db.mglBCCongViecs.InsertOnSubmit(objmgbccv);

            var itemsave = new mglBCSanPham();
            var id = (int?)grvBDS.GetFocusedRowCellValue("MaBC");
            itemsave.MaSP = id;
            itemsave.mglBCCongViec = objmgbccv;
            itemsave.MaTT = 1;
            itemsave.MaHT = 3; // gửi
            itemsave.MaLoai = 1; // chào khách hàng
            db.mglBCSanPhams.InsertOnSubmit(itemsave);
            db.SubmitChanges();

        }

        private void btnChonMBDC_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            if (e.Button.Tag.ToString() == "chonDC")
            {
                var viewinfo = (DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo)grvMatBangDaChao.GetViewInfo();
                var rowinfo = viewinfo.GetGridRowInfo(grvMatBangDaChao.FocusedRowHandle);
                if (rowinfo == null)
                    return;
                var backcolor = rowinfo.Appearance.BackColor;
                if (backcolor == Color.Lime)
                {
                    if (lstChonDC.Contains(grvMatBangDaChao.FocusedRowHandle))
                        lstChonDC.Remove(grvMatBangDaChao.FocusedRowHandle);
                }
                else
                {
                    if (!lstChonDC.Contains(grvMatBangDaChao.FocusedRowHandle))
                        lstChonDC.Add(grvMatBangDaChao.FocusedRowHandle);
                }
            }
        }

        private void grvMatBangDaChao_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (!grvMatBangDaChao.IsDataRow(e.RowHandle))
                return;
            if (lstChonDC.Contains(e.RowHandle))
                e.Appearance.BackColor = Color.Lime;
        }

        private void itemChuyenMatBangDaXem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lstChonDC.Count <= 0)
            {
                DialogBox.Error("Vui lòng chọn dữ liệu");
                return;
            }
            if (DialogBox.Question("Bạn có muốn chuyển thông tin không") == DialogResult.No) return;

            var maLoai = Convert.ToByte(grvMatBangDaChao.GetFocusedRowCellValue("MaLoai"));

            var frm = new frmChuyenDoi();
            frm.MaLoaiTruoc = maLoai;
            frm.ShowDialog();
            if (frm.isSave == true)
            {
                foreach (var x in lstChonDC)
                {
                    var id = (int?)grvMatBangDaChao.GetRowCellValue(x, "ID");
                    var obj = db.mglMTSanPhams.SingleOrDefault(p => p.ID == id);
                    obj.StartDate = DateTime.Now;
                    obj.UpdateDate = DateTime.Now;
                    obj.MaLoai = frm.MaLoai;

                    var objBC = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == (int)grvBDS.GetFocusedRowCellValue("MaMT"));
                    if (objBC != null)
                    {

                        if (frm.MaLoai == 10 && (objBC.PhiMG - (obj.PhiMGDTMua == null ? 0 : obj.PhiMGDTMua)) > 0)
                        {
                            DialogBox.Warning("Bạn không thể chuyển trạng thái [Đã thu phí] khi chưa thu đủ phí");
                            return;
                        }
                    }
                    int? maMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
                    var objls = new mglLichSuXuLyGiaoDich_MT();
                    objls.MaNV = Common.StaffID;
                    objls.NoiDung = frm.NoiDung;
                    objls.MaMT = maMT;
                    objls.MaLoai = frm.MaLoai;
                    objls.MaMTCV = id;
                    objls.Title = frm.TieuDe;
                    objls.MaLoaiTruoc = frm.MaLoaiTruoc;
                    objls.NgayXL = frm.NgayXL;
                    objls.MaPT = frm.MaPT;
                    db.mglLichSuXuLyGiaoDich_MTs.InsertOnSubmit(objls);

                    db.SubmitChanges();
                }
                DialogBox.Infomation("Thao thác thành công");
            }


        }

        private void grvMatBangDaXem2_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (!grvMatBangDaXem2.IsDataRow(e.RowHandle))
                return;
            if (lstChonDX.Contains(e.RowHandle))
                e.Appearance.BackColor = Color.Lime;
        }

        private void btnChonDX_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "chonDX")
            {
                var viewinfo = (DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo)grvMatBangDaXem2.GetViewInfo();
                var rowinfo = viewinfo.GetGridRowInfo(grvMatBangDaXem2.FocusedRowHandle);
                if (rowinfo == null)
                    return;
                var backcolor = rowinfo.Appearance.BackColor;
                if (backcolor == Color.Lime)
                {
                    if (lstChonDX.Contains(grvMatBangDaXem2.FocusedRowHandle))
                        lstChonDX.Remove(grvMatBangDaXem2.FocusedRowHandle);
                }
                else
                {
                    if (!lstChonDX.Contains(grvMatBangDaXem2.FocusedRowHandle))
                        lstChonDX.Add(grvMatBangDaXem2.FocusedRowHandle);
                }
            }
        }

        private void grvMatBangDaDamPhan_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (!grvMatBangDaDamPhan.IsDataRow(e.RowHandle))
                return;
            if (lstChonDP.Contains(e.RowHandle))
                e.Appearance.BackColor = Color.Lime;
        }

        private void btnChonDP_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "chonDP")
            {
                var viewinfo = (DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo)grvMatBangDaDamPhan.GetViewInfo();
                var rowinfo = viewinfo.GetGridRowInfo(grvMatBangDaDamPhan.FocusedRowHandle);
                if (rowinfo == null)
                    return;
                var backcolor = rowinfo.Appearance.BackColor;
                if (backcolor == Color.Lime)
                {
                    if (lstChonDP.Contains(grvMatBangDaDamPhan.FocusedRowHandle))
                        lstChonDP.Remove(grvMatBangDaDamPhan.FocusedRowHandle);
                }
                else
                {
                    if (!lstChonDP.Contains(grvMatBangDaDamPhan.FocusedRowHandle))
                        lstChonDP.Add(grvMatBangDaDamPhan.FocusedRowHandle);
                }
            }
        }

        private void grvMatBangDaDatCoc_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (!grvMatBangDaDatCoc.IsDataRow(e.RowHandle))
                return;
            if (lstChonDatCoc.Contains(e.RowHandle))
                e.Appearance.BackColor = Color.Lime;
        }

        private void btnChonDatCoc_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "chonDatCoc")
            {
                var viewinfo = (DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo)grvMatBangDaDatCoc.GetViewInfo();
                var rowinfo = viewinfo.GetGridRowInfo(grvMatBangDaDatCoc.FocusedRowHandle);
                if (rowinfo == null)
                    return;
                var backcolor = rowinfo.Appearance.BackColor;
                if (backcolor == Color.Lime)
                {
                    if (lstChonDatCoc.Contains(grvMatBangDaDatCoc.FocusedRowHandle))
                        lstChonDatCoc.Remove(grvMatBangDaDatCoc.FocusedRowHandle);
                }
                else
                {
                    if (!lstChonDatCoc.Contains(grvMatBangDaDatCoc.FocusedRowHandle))
                        lstChonDatCoc.Add(grvMatBangDaDatCoc.FocusedRowHandle);
                }
            }
        }

        private void itemMatBangGapDamPhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // chuyển từ mặt bằng đã chào qua đã xem

            if (lstChonDX.Count <= 0)
            {
                DialogBox.Error("Vui lòng chọn dữ liệu");
                return;
            }
            if (DialogBox.Question("Bạn có muốn chuyển thông tin không.") == DialogResult.No) return;

            var frm = new frmChuyenDoi();
            frm.MaLoaiTruoc = 2;
            frm.ShowDialog();

            if (frm.isSave == true)
            {
                foreach (var x in lstChonDX)
                {

                    var id = (int?)grvMatBangDaXem2.GetRowCellValue(x, "ID");
                    var obj = db.mglMTSanPhams.SingleOrDefault(p => p.ID == id);
                    obj.UpdateDate = DateTime.Now;
                    obj.MaLoai = frm.MaLoai; // mặt bằng đã gặp và đàm phán

                    int? maMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
                    var objls = new mglLichSuXuLyGiaoDich_MT();
                    objls.MaNV = Common.StaffID;
                    objls.NoiDung = frm.NoiDung;
                    objls.MaMT = maMT;
                    objls.MaLoai = frm.MaLoai;
                    objls.MaMTCV = id;
                    objls.Title = frm.TieuDe;
                    objls.MaLoaiTruoc = frm.MaLoaiTruoc;
                    objls.NgayXL = frm.NgayXL;
                    objls.MaPT = frm.MaPT;
                    db.mglLichSuXuLyGiaoDich_MTs.InsertOnSubmit(objls);

                    //var objls = new mglLichSuXuLyGiaoDich();
                    //objls.MaNV = Common.StaffID;
                    //objls.NoiDung = frm.NoiDung;
                    //objls.NgayXL = DateTime.Now;
                    //objls.MaLoai = frm.MaLoai;
                    //objls.MaLoaiTruoc = 2;
                    //objls.MaBCSP = id;
                    //db.mglLichSuXuLyGiaoDiches.InsertOnSubmit(objls);
                    db.SubmitChanges();
                }
            }


            DialogBox.Infomation("Thao thác thành công");

        }

        private void itemChuyenMBDatCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // chuyển từ mặt bằng đã chào qua đã xem

            if (lstChonDP.Count <= 0)
            {
                DialogBox.Error("Vui lòng chọn dữ liệu");
                return;
            }
            if (DialogBox.Question("Bạn có muốn chuyển thông tin không.") == DialogResult.No) return;
            var frm = new frmChuyenDoi();
            frm.MaLoaiTruoc = 4;
            frm.ShowDialog();

            if (frm.isSave == true)
            {
                foreach (var x in lstChonDP)
                {

                    var id = (int?)grvMatBangDaDamPhan.GetRowCellValue(x, "ID");
                    var obj = db.mglBCSanPhams.SingleOrDefault(p => p.ID == id);
                    obj.UpdateDate = DateTime.Now;
                    obj.MaLoai = frm.MaLoai; // mặt bằng đã đặt cọc

                    int? maMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
                    var objls = new mglLichSuXuLyGiaoDich_MT();
                    objls.MaNV = Common.StaffID;
                    objls.NoiDung = frm.NoiDung;
                    objls.MaMT = maMT;
                    objls.MaLoai = frm.MaLoai;
                    objls.MaMTCV = id;
                    objls.Title = frm.TieuDe;
                    objls.MaLoaiTruoc = frm.MaLoaiTruoc;
                    objls.NgayXL = frm.NgayXL;
                    objls.MaPT = frm.MaPT;
                    db.mglLichSuXuLyGiaoDich_MTs.InsertOnSubmit(objls);
                    db.SubmitChanges();
                }
                DialogBox.Infomation("Thao thác thành công");

            }


        }

        private void itemChuyenMatBangChoThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (lstChonDatCoc.Count <= 0)
            {
                DialogBox.Error("Vui lòng chọn dữ liệu");
                return;
            }
            if (DialogBox.Question("Bạn có muốn chuyển thông tin không.") == DialogResult.No) return;
            var frm = new frmChuyenDoi();
            frm.MaLoaiTruoc = 5;
            frm.ShowDialog();

            if (frm.isSave == true)
            {
                foreach (var x in lstChonDatCoc)
                {

                    var id = (int?)grvMatBangDaDatCoc.GetRowCellValue(x, "ID");
                    var obj = db.mglMTSanPhams.SingleOrDefault(p => p.ID == id);
                    obj.UpdateDate = DateTime.Now;
                    obj.MaLoai = frm.MaLoai; // chờ thanh toán 5

                    int? maMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
                    var objls = new mglLichSuXuLyGiaoDich_MT();
                    objls.MaNV = Common.StaffID;
                    objls.NoiDung = frm.NoiDung;
                    objls.MaMT = maMT;
                    objls.MaLoai = frm.MaLoai;
                    objls.MaMTCV = id;
                    objls.Title = frm.TieuDe;
                    objls.MaLoaiTruoc = frm.MaLoaiTruoc;
                    objls.NgayXL = frm.NgayXL;
                    objls.MaPT = frm.MaPT;
                    db.mglLichSuXuLyGiaoDich_MTs.InsertOnSubmit(objls);
                    db.SubmitChanges();
                }
                DialogBox.Infomation("Thao thác thành công");
            }

        }

        private void itemLichHenDaXem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            var id = (int?)grvMatBangDaXem2.GetFocusedRowCellValue("ID");
            var MaKH = (int?)grvMatBangDaXem2.GetFocusedRowCellValue("MaKH");
            string hotenKH = "";
            try
            {
                hotenKH = grvMatBangDaXem2.GetFocusedRowCellValue("HoTenKH").ToString();
            }
            catch
            {

            }
            var frm = new BEEREMA.CongViec.LichHen.AddNew_frm(null, MaKH, hotenKH);
            frm.MaBCSP = id;
            frm.MaLoai = 2;
            frm.ShowDialog();

        }

        private void itemLichHenDamPhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)grvMatBangDaXem2.GetFocusedRowCellValue("ID");
            var MaKH = (int?)grvMatBangDaXem2.GetFocusedRowCellValue("MaKH");
            string hotenKH = "";
            try
            {
                hotenKH = grvMatBangDaXem2.GetFocusedRowCellValue("HoTenKH").ToString();
            }
            catch
            {

            }
            var frm = new BEEREMA.CongViec.LichHen.AddNew_frm(null, MaKH, hotenKH);
            frm.MaBCSP = id;
            frm.MaLoai = 3;
            frm.ShowDialog();
        }
        private void itemLichHenDatCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)grvMatBangDaXem2.GetFocusedRowCellValue("ID");
            var MaKH = (int?)grvMatBangDaXem2.GetFocusedRowCellValue("MaKH");
            string hotenKH = "";
            try
            {
                hotenKH = grvMatBangDaXem2.GetFocusedRowCellValue("HoTenKH").ToString();
            }
            catch
            {

            }
            var frm = new BEEREMA.CongViec.LichHen.AddNew_frm(null, MaKH, hotenKH);
            frm.MaBCSP = id;
            frm.MaLoai = 4;
            frm.ShowDialog();
        }

        private void itemLichHenChoThuPhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)grvMatBangDaXem2.GetFocusedRowCellValue("ID");
            var MaKH = (int?)grvMatBangDaXem2.GetFocusedRowCellValue("MaKH");
            string hotenKH = "";
            try
            {
                hotenKH = grvMatBangDaXem2.GetFocusedRowCellValue("HoTenKH").ToString();
            }
            catch
            {

            }
            var frm = new BEEREMA.CongViec.LichHen.AddNew_frm(null, MaKH, hotenKH);
            frm.MaBCSP = id;
            frm.MaLoai = 5;
            frm.ShowDialog();
        }

        private void itemDaQuanTam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lstDaQuanTam.Count <= 0)
            {
                DialogBox.Error("Vui lòng chọn dữ liệu");
                return;
            }
            if (DialogBox.Question("Bạn có muốn chuyển thông tin không.") == DialogResult.No) return;
            var frm = new frmChuyenDoi();
            frm.MaLoaiTruoc = 6;
            frm.ShowDialog();

            if (frm.isSave == true)
            {
                foreach (var x in lstDaQuanTam)
                {

                    var id = (int?)grvDaQuanTam.GetRowCellValue(x, "ID");
                    var obj = db.mglMTSanPhams.SingleOrDefault(p => p.ID == id);
                    obj.UpdateDate = DateTime.Now;
                    obj.MaLoai = frm.MaLoai; // chờ thanh toán 

                    int? maMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
                    var objls = new mglLichSuXuLyGiaoDich_MT();
                    objls.MaNV = Common.StaffID;
                    objls.NoiDung = frm.NoiDung;
                    objls.MaMT = maMT;
                    objls.MaLoai = frm.MaLoai;
                    objls.MaMTCV = id;
                    objls.Title = frm.TieuDe;
                    objls.MaLoaiTruoc = frm.MaLoaiTruoc;
                    objls.NgayXL = frm.NgayXL;
                    objls.MaPT = frm.MaPT;
                    db.mglLichSuXuLyGiaoDich_MTs.InsertOnSubmit(objls);
                    db.SubmitChanges();
                }
                DialogBox.Infomation("Thao thác thành công");
            }
        }
        private void btnDaQuanTam_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "chonDC")
            {
                var viewinfo = (DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo)grvDaQuanTam.GetViewInfo();
                var rowinfo = viewinfo.GetGridRowInfo(grvDaQuanTam.FocusedRowHandle);
                if (rowinfo == null)
                    return;
                var backcolor = rowinfo.Appearance.BackColor;
                if (backcolor == Color.Lime)
                {
                    if (lstDaQuanTam.Contains(grvDaQuanTam.FocusedRowHandle))
                        lstDaQuanTam.Remove(grvDaQuanTam.FocusedRowHandle);
                }
                else
                {
                    if (!lstDaQuanTam.Contains(grvDaQuanTam.FocusedRowHandle))
                        lstDaQuanTam.Add(grvDaQuanTam.FocusedRowHandle);
                }
            }
        }
        private void grvDaQuanTam_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (!grvDaQuanTam.IsDataRow(e.RowHandle))
                return;
            if (lstDaQuanTam.Contains(e.RowHandle))
                e.Appearance.BackColor = Color.Lime;
        }
        private void itemDaDuaKhGap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lstDaDuaKH.Count <= 0)
            {
                DialogBox.Error("Vui lòng chọn dữ liệu");
                return;
            }
            if (DialogBox.Question("Bạn có muốn chuyển thông tin không.") == DialogResult.No) return;
            var frm = new frmChuyenDoi();
            frm.MaLoaiTruoc = 7;
            frm.ShowDialog();

            if (frm.isSave == true)
            {
                foreach (var x in lstDaDuaKH)
                {

                    var id = (int?)grvDaDuaKHGap.GetRowCellValue(x, "ID");
                    var obj = db.mglMTSanPhams.SingleOrDefault(p => p.ID == id);
                    obj.UpdateDate = DateTime.Now;
                    obj.MaLoai = frm.MaLoai;

                    int? maMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
                    var objls = new mglLichSuXuLyGiaoDich_MT();
                    objls.MaNV = Common.StaffID;
                    objls.NoiDung = frm.NoiDung;
                    objls.MaMT = maMT;
                    objls.MaLoai = frm.MaLoai;
                    objls.MaMTCV = id;
                    objls.Title = frm.TieuDe;
                    objls.MaLoaiTruoc = frm.MaLoaiTruoc;
                    objls.NgayXL = frm.NgayXL;
                    objls.MaPT = frm.MaPT;
                    db.mglLichSuXuLyGiaoDich_MTs.InsertOnSubmit(objls);
                    db.SubmitChanges();
                }
                DialogBox.Infomation("Thao thác thành công");
            }
        }
        private void btnDaKHDiGap_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "chonDC")
            {
                var viewinfo = (DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo)grvDaDuaKHGap.GetViewInfo();
                var rowinfo = viewinfo.GetGridRowInfo(grvDaDuaKHGap.FocusedRowHandle);
                if (rowinfo == null)
                    return;
                var backcolor = rowinfo.Appearance.BackColor;
                if (backcolor == Color.Lime)
                {
                    if (lstDaDuaKH.Contains(grvDaDuaKHGap.FocusedRowHandle))
                        lstDaDuaKH.Remove(grvDaDuaKHGap.FocusedRowHandle);
                }
                else
                {
                    if (!lstDaDuaKH.Contains(grvDaDuaKHGap.FocusedRowHandle))
                        lstDaDuaKH.Add(grvDaDuaKHGap.FocusedRowHandle);
                }
            }
        }
        private void grvDaDuaKHGap_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (!grvDaDuaKHGap.IsDataRow(e.RowHandle))
                return;
            if (lstDaDuaKH.Contains(e.RowHandle))
                e.Appearance.BackColor = Color.Lime;
        }
        private void itemDangSuaHD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lstDangSuaHD.Count <= 0)
            {
                DialogBox.Error("Vui lòng chọn dữ liệu");
                return;
            }
            if (DialogBox.Question("Bạn có muốn chuyển thông tin không.") == DialogResult.No) return;
            var frm = new frmChuyenDoi();
            frm.MaLoaiTruoc = 8;
            frm.ShowDialog();

            if (frm.isSave == true)
            {
                foreach (var x in lstDangSuaHD)
                {

                    var id = (int?)grvDangSuaHD.GetRowCellValue(x, "ID");
                    var obj = db.mglMTSanPhams.SingleOrDefault(p => p.ID == id);
                    obj.UpdateDate = DateTime.Now;
                    obj.MaLoai = frm.MaLoai;

                    int? maMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
                    var objls = new mglLichSuXuLyGiaoDich_MT();
                    objls.MaNV = Common.StaffID;
                    objls.NoiDung = frm.NoiDung;
                    objls.MaMT = maMT;
                    objls.MaLoai = frm.MaLoai;
                    objls.MaMTCV = id;
                    objls.Title = frm.TieuDe;
                    objls.MaLoaiTruoc = frm.MaLoaiTruoc;
                    objls.NgayXL = frm.NgayXL;
                    objls.MaPT = frm.MaPT;
                    db.mglLichSuXuLyGiaoDich_MTs.InsertOnSubmit(objls);
                    db.SubmitChanges();
                }
                DialogBox.Infomation("Thao thác thành công");
            }
        }
        private void btnDangSuaHD_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "chonDC")
            {
                var viewinfo = (DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo)grvDangSuaHD.GetViewInfo();
                var rowinfo = viewinfo.GetGridRowInfo(grvDangSuaHD.FocusedRowHandle);
                if (rowinfo == null)
                    return;
                var backcolor = rowinfo.Appearance.BackColor;
                if (backcolor == Color.Lime)
                {
                    if (lstDangSuaHD.Contains(grvDangSuaHD.FocusedRowHandle))
                        lstDangSuaHD.Remove(grvDangSuaHD.FocusedRowHandle);
                }
                else
                {
                    if (!lstDangSuaHD.Contains(grvDangSuaHD.FocusedRowHandle))
                        lstDangSuaHD.Add(grvDangSuaHD.FocusedRowHandle);
                }
            }
        }

        private void grvDangSuaHD_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (!grvMatBangDaDamPhan.IsDataRow(e.RowHandle))
                return;
            if (lstDangSuaHD.Contains(e.RowHandle))
                e.Appearance.BackColor = Color.Lime;
        }
        private void itemDaKyHD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lstDaKyHD.Count <= 0)
            {
                DialogBox.Error("Vui lòng chọn dữ liệu");
                return;
            }
            if (DialogBox.Question("Bạn có muốn chuyển thông tin không.") == DialogResult.No) return;
            var frm = new frmChuyenDoi();
            frm.MaLoaiTruoc = 9;
            frm.ShowDialog();

            if (frm.isSave == true)
            {
                foreach (var x in lstDaKyHD)
                {

                    var id = (int?)grvDaKyHD.GetRowCellValue(x, "ID");
                    var obj = db.mglMTSanPhams.SingleOrDefault(p => p.ID == id);
                    obj.UpdateDate = DateTime.Now;
                    obj.MaLoai = frm.MaLoai; // chờ thanh toán 5

                    int? maMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
                    var objls = new mglLichSuXuLyGiaoDich_MT();
                    objls.MaNV = Common.StaffID;
                    objls.NoiDung = frm.NoiDung;
                    objls.MaMT = maMT;
                    objls.MaLoai = frm.MaLoai;
                    objls.MaMTCV = id;
                    objls.Title = frm.TieuDe;
                    objls.MaLoaiTruoc = frm.MaLoaiTruoc;
                    objls.NgayXL = frm.NgayXL;
                    objls.MaPT = frm.MaPT;
                    db.mglLichSuXuLyGiaoDich_MTs.InsertOnSubmit(objls);
                    db.SubmitChanges();
                }
                DialogBox.Infomation("Thao thác thành công");
            }
        }

        private void btnDaKyHD_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "chonDC")
            {
                var viewinfo = (DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo)grvDaKyHD.GetViewInfo();
                var rowinfo = viewinfo.GetGridRowInfo(grvDaKyHD.FocusedRowHandle);
                if (rowinfo == null)
                    return;
                var backcolor = rowinfo.Appearance.BackColor;
                if (backcolor == Color.Lime)
                {
                    if (lstDaKyHD.Contains(grvDaKyHD.FocusedRowHandle))
                        lstDaKyHD.Remove(grvDaKyHD.FocusedRowHandle);
                }
                else
                {
                    if (!lstDaKyHD.Contains(grvDaKyHD.FocusedRowHandle))
                        lstDaKyHD.Add(grvDaKyHD.FocusedRowHandle);
                }
            }
        }

        private void grvDaKyHD_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (!grvDaKyHD.IsDataRow(e.RowHandle))
                return;
            if (lstDaKyHD.Contains(e.RowHandle))
                e.Appearance.BackColor = Color.Lime;
        }

        private void grvMatBangDaChao_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                if (e.Column.FieldName == "TenTTGD")
                {
                    var color = grvMatBangDaChao.GetRowCellValue(e.RowHandle, "Color");
                    if (color != null)
                    {
                        var colorArr = color.ToString().Split(',');
                        if (colorArr.Length > 1)
                        {
                            e.Appearance.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(colorArr[0].Trim()), Convert.ToInt32(colorArr[1].Trim()), Convert.ToInt32(colorArr[2].Trim()));
                        }
                        else
                        {
                            e.Appearance.BackColor = System.Drawing.Color.FromName(color.ToString());
                        }

                    }

                }
            }
            catch { }
        }

       
    }

}
