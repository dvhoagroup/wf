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

namespace BEE.HoatDong.MGL.Mua
{
    public partial class ctlManagerChoXoaCT : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();

        public ctlManagerChoXoaCT()
        {
            InitializeComponent();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 23;
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
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 23);

            return o.SDB.SDBID;
        }        

        void MuaThue_Load()
        {
            var tuNgay = (DateTime?)itemTuNgay.EditValue ?? DateTime.Now;
            var denNgay = (DateTime?)itemDenNgay.EditValue ?? DateTime.Now;
            var MaTT = (byte?)itemTrangThai.EditValue ?? Convert.ToByte(100);
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
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDateChoXoa(tuNgay, denNgay, MaTT, false, -1, -1, -1, -1, MaNV, true)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    //SLKhop = Khop(mt),
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
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDateChoXoa(tuNgay, denNgay, MaTT, false, -1, -1, -1, -1, MaNV, true)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    //SLKhop = Khop(mt),
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
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDateChoXoa(tuNgay, denNgay, MaTT, false, -1, -1, -1, -1, MaNV, true);//.Where(p => p.IsMua.GetValueOrDefault() == true);
                        #endregion
                        break;
                    case 2://Theo phong ban 
                        #region Theo phong ban
                        if (obj.DienThoai == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDateChoXoa(tuNgay, denNgay, MaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false)
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
                                });
                        else if (obj.DienThoai3Dau == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDateChoXoa(tuNgay, denNgay, MaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false)
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
                                    DienThoai = Common.Right1(mt.DienThoai, 3)
                                });
                        else
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDateChoXoa(tuNgay, denNgay, MaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false);//.Where(p => p.IsMua.GetValueOrDefault() == true);
                        #endregion
                        break;
                    case 3://Theo nhom
                        #region Theo nhom
                        if (obj.DienThoai == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDateChoXoa(tuNgay, denNgay, MaTT, false, -1, -1, Common.GroupID, -1, MaNV, false)
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
                                });
                        else if (obj.DienThoai3Dau == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDateChoXoa(tuNgay, denNgay, MaTT, false, -1, -1, Common.GroupID, -1, MaNV, false)
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
                                    DienThoai = Common.Right1(mt.DienThoai, 3)
                                });
                        else
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDateChoXoa(tuNgay, denNgay, MaTT, false, -1, -1, Common.GroupID, -1, MaNV, false);//.Where(p => p.IsMua.GetValueOrDefault() == true);
                        #endregion
                        break;
                    case 4://Theo nhan vien
                        #region Theo nhan vien
                        if (obj.DienThoai == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDateChoXoa(tuNgay, denNgay, MaTT, false, MaNV, MaNV, -1, -1, MaNV, false)
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
                                });//.Where(p => p.IsMua.GetValueOrDefault() == true);
                        else if (obj.DienThoai3Dau == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDateChoXoa(tuNgay, denNgay, MaTT, false, MaNV, MaNV, -1, -1, MaNV, false)
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
                                    DienThoai = Common.Right1(mt.DienThoai, 3)
                                });
                        else
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDateChoXoa(tuNgay, denNgay, MaTT, false, -1, -1, Common.GroupID, -1, MaNV, false);//.Where(p => p.IsMua.GetValueOrDefault() == true);
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
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                wait.Close();
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
                db.mglmtMuaThues.DeleteOnSubmit(objMT);
                grvMuaThue.DeleteRow(i);
            }            
            db.SubmitChanges();
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
            }
            catch { }

        }

        private void ctlManagerChoXoaCT_Load(object sender, EventArgs e)
        {
            List<ItemTrangThai> lst = new List<ItemTrangThai>();
            lst.Add(new ItemTrangThai() { MaTT = Convert.ToByte(100), TenTT = "<Tất cả>" });
            foreach (var p in db.mglbcTrangThais)
            {
                var obj = new ItemTrangThai() { MaTT = p.MaTT, TenTT = p.TenTT };
                lst.Add(obj);
            }
            lookTrangThai.DataSource = lst;
            itemTrangThai.EditValue = Convert.ToByte(100);
            lookLoaiBDS.DataSource = db.LoaiBDs;
            lookCapDo.DataSource = db.mglCapDos;
            lookNguon.DataSource = db.mglNguons;
            LoadPermission();
            phanquyen();
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(4);
            ThuTuCot();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MuaThue_Load();
            phanquyen();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmEdit frm = new frmEdit();
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
            using (var frm = new frmEdit())
            {
                bool ck = (GetAccessData() == 1 || (int)grvMuaThue.GetFocusedRowCellValue("MaNVKT") == Common.StaffID) || ((int)grvMuaThue.GetFocusedRowCellValue("MaNVKD") == Common.StaffID);
                frm.AllowSave = ck;
                frm.DislayContac = ck;
                frm.MaMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
                frm.ShowDialog();
                if (frm.IsSave)
                {
                    MuaThue_Load();
                }
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MuaThue_Delete();
        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MuaThue_Delete();
        }

        private void itemKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                objMT.MaTTD = 2;
            }
            db.SubmitChanges();
            DialogBox.Infomation("Đã chuyển sang danh sách sản phẩm cần mua");
            MuaThue_Load();
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

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            MuaThue_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            MuaThue_Load();
        }
    }
}
