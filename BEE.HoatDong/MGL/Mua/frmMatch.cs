using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using BEEREMA;
using System.Linq;

namespace BEE.HoatDong.MGL.Mua
{
    public partial class frmMatch : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public List<int> arrMaBC;
        public frmMatch()
        {
            InitializeComponent();
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 177);

            return o.SDB.SDBID;
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

        private void frmMatch_Load(object sender, EventArgs e)
        {
            List<ItemTrangThai> lst = new List<ItemTrangThai>();
            lst.Add(new ItemTrangThai() { MaTT = Convert.ToByte(100), TenTT = "<Tất cả>" });
            foreach (var p in db.mglbcTrangThais)
            {
                var obj = new ItemTrangThai() { MaTT = p.MaTT, TenTT = p.TenTT };
                lst.Add(obj);
            }
            chkTrangThai.DataSource = db.mglbcTrangThais;

            lookCapDo.DataSource = db.mglCapDos;
            //LoadPermission();
            phanquyen();
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(4);
            itemTuNgay.EditValue = new DateTime(2012, 5, 28);
            grvMuaThue.BestFitColumns();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (itemTrangThai.EditValue != "Trạng thái" && itemTrangThai.EditValue != "")
            {
                db = new MasterDataContext();
                MuaThue_Load();
                //phanquyen();
                grvMuaThue.BestFitColumns();
            }
        }

        void MuaThue_Load()
        {
            var tuNgay = (DateTime?)itemTuNgay.EditValue ?? new DateTime(2012, 5, 28);
            var denNgay = (DateTime?)itemDenNgay.EditValue ?? DateTime.Now;
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
                                    mt.MoHinh,
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
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, -1, -1, -1, -1, MaNV, true, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.MoHinh,
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
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.MoHinh,
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
                                }).ToList();
                        else if (obj.DienThoai3Dau == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, -1, -1, -1, Common.DepartmentID, MaNV, false, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.MoHinh,
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
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.MoHinh,
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
                                }).ToList();
                        else if (obj.DienThoai3Dau == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.ThuongHieu,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.MoHinh,
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
                                }).ToList();
                        else
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false, Common.StaffID);//.Where(p => p.IsMua.GetValueOrDefault() == true);
                        #endregion
                        break;
                    case 4://Theo nhan vien
                        #region Theo nhan vien
                        if (obj.DienThoai == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.MaMT,
                                    mt.MoHinh,
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
                                }).ToList();//.Where(p => p.IsMua.GetValueOrDefault() == true);
                        else if (obj.DienThoai3Dau == false)
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, MaNV, MaNV, -1, -1, MaNV, false, Common.StaffID)
                                .Select(mt => new
                                {
                                    mt.STT,
                                    mt.MaTT,
                                    //SLKhop = Khop(mt),
                                    mt.MauNen,
                                    mt.TenTT,
                                    mt.ThuongHieu,
                                    mt.MaMT,
                                    mt.MoHinh,
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
                                }).ToList();
                        else
                            gcMuaThue.DataSource = db.mglmtMuaThue_getByDate(tuNgay, denNgay, arrMaTT, false, -1, -1, Common.GroupID, -1, MaNV, false, Common.StaffID);//.Where(p => p.IsMua.GetValueOrDefault() == true);
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

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            phanquyen();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            phanquyen();
        }

        private void btnSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvMuaThue.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Infomation("Bạn vui lòng chọn khách hàng để gửi Email!");
                return;
            }

            //mailConfig mailcf;
            //try
            //{
            //    mailcf = db.mailConfigs.Where(p => p.StaffID == Common.StaffID).FirstOrDefault();
            //    if (mailcf == null)
            //    {
            //        DialogBox.Error("Bạn chưa cấu hình Email gửi, vui lòng kiểm tra lại");
            //        return;
            //    }
            //}
            //catch
            //{
            //    DialogBox.Error("Bạn chưa cấu hình Email gửi, vui lòng kiểm tra lại");
            //    return;
            //}

            var tt = new MGL.ThongTinGui();
            tt.ID = 1;
            tt.ShowDialog();

            var arrMaMT = new List<int>();
            var dicMail = new List<Dictionary<string, ThuVien.KhachHang>>();

            var lstMaKh = new List<BEE.ThuVien.KhachHang>();
            foreach (var i in grvMuaThue.GetSelectedRows())
            {
                if (!string.IsNullOrEmpty(grvMuaThue.GetRowCellValue(i, "MaKH").ToString()))
                {
                    lstMaKh.Add(db.KhachHangs.SingleOrDefault(p => p.MaKH == (int?)grvMuaThue.GetRowCellValue(i, "MaKH")));
                }
                var maMT = (int)grvMuaThue.GetRowCellValue(i, "MaMT");
                arrMaMT.Add(maMT);
            }

            foreach (var item in lstMaKh)
            {
                string noidung = "";
                int j = 1;
                var objKH = db.KhachHangs.SingleOrDefault(p => p.MaKH == item.MaKH);
                // lấy thông tin khách hàng cần gửi
                try
                {
                    //if (!string.IsNullOrEmpty(mailcf.Logo1))
                    //    noidung += "<img  src='" + mailcf.Logo1 + "'/>";

                    noidung += "<p><b>Kính gửi " + (objKH.IsPersonal == true ? objKH.HoKH + " " + objKH.TenKH : objKH.TenCongTy) + "</b></p>";
                    noidung += "<p>" + "Công ty cổ phần HoaLand gửi Anh thông tin mặt bằng phù hợp với mô hình <b>" + (objKH.IsPersonal == true ? objKH.MoHinh : objKH.ThuongHieu) + "</b>: " + "</p>";
                }
                catch
                {
                    DialogBox.Error("Bạn chưa cấu hình Email gửi, vui lòng kiểm tra lại");
                    return;
                }
                var objDic = new Dictionary<string, ThuVien.KhachHang>();
                foreach (var i in arrMaBC)
                {
                    var objBC = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == i);
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

                    try
                    {
                        //noidung += "<p>Trân trọng cảm ơn!</p>";
                        //if (!string.IsNullOrEmpty(mailcf.Logo2))
                        //    noidung += "<img src='" + mailcf.Logo2 + "'/>";
                    }
                    catch { }
                    j++;
                }
                objDic.Add(noidung, item);
                dicMail.Add(objDic);
            }
            var maNlh = (int?)grvAvatar.GetFocusedRowCellValue("MaNDD");
            using (var frm = new BEE.HoatDong.MGL.frmSend() { dicMail = dicMail, MaPT = 1 })
            {
                frm.ShowDialog();
                if (frm.isSend)
                {
                    // lưu dữ liệu
                    try
                    {
                        foreach (var item in arrMaBC)
                        {
                            int? idSPMT = null;
                            var existRecord = db.mglMTCongViecs.FirstOrDefault(p => p.MaBC == item);
                            if (existRecord != null)
                            {
                                existRecord.MaBC = item;
                                existRecord.MaNV = Common.StaffID;
                                existRecord.NgayNhap = DateTime.Now;
                                existRecord.DienGiai = "Gửi Mail";
                                existRecord.MaTT = 1;

                                var existSP = db.mglMTSanPhams.FirstOrDefault(p => p.MaMT == arrMaMT[0] && p.MaCV == existRecord.ID);
                                if (existSP == null)
                                {
                                    var itemsave = new mglMTSanPham();
                                    itemsave.MaMT = arrMaMT[0];
                                    itemsave.MaCV = existRecord.ID;
                                    itemsave.MaTT = 1;
                                    itemsave.MaHT = 1; // gửi mail
                                    itemsave.MaLoai = 1; // chào khách hàng
                                    itemsave.MaNLH = maNlh;
                                    itemsave.StartDate = DateTime.Now;
                                    itemsave.UpdateDate = DateTime.Now;
                                    itemsave.MaNV = Common.StaffID;
                                    db.mglMTSanPhams.InsertOnSubmit(itemsave);
                                    db.SubmitChanges();
                                    idSPMT = itemsave.ID;
                                }
                                else
                                {
                                    existSP.UpdateDate = DateTime.Now;
                                    existSP.MaNV = Common.StaffID;
                                    idSPMT = existSP.ID;
                                }
                            }
                            else
                            {
                                var objmgbccv = new mglMTCongViec();
                                objmgbccv.MaBC = item;
                                objmgbccv.MaNV = Common.StaffID;
                                objmgbccv.NgayNhap = DateTime.Now;
                                objmgbccv.DienGiai = "Gửi Mail";
                                objmgbccv.MaTT = 1;

                                db.mglMTCongViecs.InsertOnSubmit(objmgbccv);
                                db.SubmitChanges();

                                var itemsave = new mglMTSanPham();
                                itemsave.MaMT = arrMaMT[0];
                                itemsave.MaCV = objmgbccv.ID;
                                itemsave.MaTT = 1;
                                itemsave.MaHT = 1; // gửi mail
                                itemsave.MaLoai = 1; // chào khách hàng
                                itemsave.MaNLH = maNlh;
                                itemsave.StartDate = DateTime.Now;
                                itemsave.MaNV = Common.StaffID;
                                itemsave.UpdateDate = DateTime.Now;
                                db.mglMTSanPhams.InsertOnSubmit(itemsave);
                                db.SubmitChanges();
                                idSPMT = itemsave.ID;
                            }
                            var objls = new mglLichSuXuLyGiaoDich_MT();
                            objls.MaNV = Common.StaffID;
                            objls.NoiDung = "";
                            objls.MaBC = item;
                            objls.MaMT = arrMaMT[0];
                            objls.MaLoai = 1;
                            objls.MaMTCV = idSPMT;
                            objls.Title = "Chào mặt bằng";
                            objls.NgayXL = DateTime.Now;
                         
                            objls.MaPT = 3;
                            db.mglLichSuXuLyGiaoDich_MTs.InsertOnSubmit(objls);

                            db.SubmitChanges();
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        db.Dispose();
                    }
                }
            }
        }

        private void btnSendSms_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvMuaThue.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Infomation("Bạn vui lòng chọn khách hàng để gửi Email!");
                return;
            }

            //mailConfig mailcf;
            //try
            //{
            //    mailcf = db.mailConfigs.Where(p => p.StaffID == Common.StaffID).FirstOrDefault();
            //    if (mailcf == null)
            //    {
            //        DialogBox.Error("Bạn chưa cấu hình Email gửi, vui lòng kiểm tra lại");
            //        return;
            //    }
            //}
            //catch
            //{
            //    DialogBox.Error("Bạn chưa cấu hình Email gửi, vui lòng kiểm tra lại");
            //    return;
            //}
            var tt = new MGL.ThongTinGui();
            tt.ID = 1;
            tt.ShowDialog();

            var arrMaMT = new List<int>();
            List<Dictionary<BEE.ThuVien.KhachHang, string>> dicMail = new List<Dictionary<ThuVien.KhachHang, string>>();

            var lstMaKh = new List<BEE.ThuVien.KhachHang>();
            foreach (var i in grvMuaThue.GetSelectedRows())
            {
                if (!string.IsNullOrEmpty(grvMuaThue.GetRowCellValue(i, "MaKH").ToString()))
                {
                    lstMaKh.Add(db.KhachHangs.SingleOrDefault(p => p.MaKH == (int?)grvMuaThue.GetRowCellValue(i, "MaKH")));
                }

                var maMT = (int)grvMuaThue.GetRowCellValue(i, "MaMT");
                arrMaMT.Add(maMT);
            }

            foreach (var item in lstMaKh)
            {
                string noidung = "";
                int j = 1;

                var objDic = new Dictionary<ThuVien.KhachHang, string>();
                var objKH = db.KhachHangs.FirstOrDefault(p => p.MaKH == item.MaKH);
                if (objDic.ContainsKey(objKH)) continue;

                // lấy thông tin khách hàng cần gửi
                try
                {
                    //if (!string.IsNullOrEmpty(mailcf.Logo1))
                    //    noidung += "<img  src='" + mailcf.Logo1 + "'/>";

                    noidung += "<p><b>Kính gửi " + (objKH.IsPersonal == true ? objKH.HoKH + " " + objKH.TenKH : objKH.TenCongTy) + "</b></p>";
                    noidung += "<p>" + "Công ty cổ phần HoaLand gửi Anh thông tin mặt bằng phù hợp với mô hình <b>" + (objKH.IsPersonal == true ? objKH.MoHinh : objKH.ThuongHieu) + "</b>: " + "</p>";
                }
                catch
                {

                    DialogBox.Error("Bạn chưa cấu hình Email gửi, vui lòng kiểm tra lại");
                    return;
                }

                foreach (var i in arrMaBC)
                {
                    var objBC = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == i);
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

                    try
                    {
                        noidung += "<p>Trân trọng cảm ơn!</p>";
                        //if (!string.IsNullOrEmpty(mailcf.Logo2))
                        //    noidung += "<img src='" + mailcf.Logo2 + "'/>";
                    }
                    catch
                    {

                    }
                    j++;
                }
                objDic.Add(objKH, noidung);
                dicMail.Add(objDic);
            }
            using (var frm = new BEE.HoatDong.MGL.frmSendSMS() { dicSms = dicMail })
            {
                frm.ShowDialog();
                if (frm.isDaGui == true)
                {
                    try
                    {
                        int recourd = 0;
                        foreach (var item in arrMaBC)
                        {
                            int? idSPMT = null;
                            var existRecord = db.mglMTCongViecs.FirstOrDefault(p => p.MaBC == item);
                            if (existRecord != null)
                            {
                                existRecord.NgayNhap = DateTime.Now;
                                db.SubmitChanges();
                                var existSP = db.mglMTSanPhams.FirstOrDefault(p => p.MaMT == arrMaMT[0] && p.MaCV == existRecord.ID);
                                if (existSP == null)
                                {
                                    var itemsave = new mglMTSanPham();
                                    itemsave.MaMT = arrMaMT[0];
                                    itemsave.MaCV = existRecord.ID;
                                    itemsave.MaTT = 1;
                                    itemsave.MaHT = 2; // gửi sms
                                    itemsave.MaLoai = 1; // chào khách hàng
                                    itemsave.MaNLH = (int?)grvAvatar.GetFocusedRowCellValue("MaNDD");
                                    itemsave.StartDate = DateTime.Now;
                                    itemsave.UpdateDate = DateTime.Now;
                                    itemsave.MaNV = Common.StaffID;
                                    db.mglMTSanPhams.InsertOnSubmit(itemsave);
                                    db.SubmitChanges();
                                    idSPMT = itemsave.ID;
                                }
                                else
                                {
                                    idSPMT = existSP.ID;
                                }
                            }
                            else
                            {
                                var objmgbccv = new mglMTCongViec();
                                objmgbccv.MaBC = item;
                                objmgbccv.MaNV = Common.StaffID;
                                objmgbccv.NgayNhap = DateTime.Now;
                                objmgbccv.DienGiai = "Gửi SMS";
                                objmgbccv.MaTT = 1;

                                db.mglMTCongViecs.InsertOnSubmit(objmgbccv);
                                db.SubmitChanges();
                                var itemsave = new mglMTSanPham();
                                var id = arrMaMT[recourd];
                                itemsave.MaMT = id;
                                itemsave.MaCV = objmgbccv.ID;
                                itemsave.MaTT = 1;
                                itemsave.MaHT = 2; // gửi sms
                                itemsave.MaLoai = 1; // chào khách hàng
                                itemsave.MaNLH = (int?)grvAvatar.GetFocusedRowCellValue("MaNDD");
                                itemsave.StartDate = DateTime.Now;
                                itemsave.UpdateDate = DateTime.Now;
                                itemsave.MaNV = Common.StaffID;
                                db.mglMTSanPhams.InsertOnSubmit(itemsave);
                                db.SubmitChanges();
                                idSPMT = itemsave.ID;
                            }

                            var objls = new mglLichSuXuLyGiaoDich_MT();
                            objls.MaNV = Common.StaffID;
                            objls.NoiDung = "";
                            objls.MaBC = item;
                            objls.MaMT = arrMaMT[recourd];
                            objls.MaLoai = 1;
                            //objls.MaMTCV = macv;
                            objls.MaMTCV = idSPMT;
                            objls.Title = "Chào mặt bằng";
                            objls.NgayXL = DateTime.Now;
                            objls.MaPT = 4;
                            db.mglLichSuXuLyGiaoDich_MTs.InsertOnSubmit(objls);

                            db.SubmitChanges();
                            recourd++;
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        db.Dispose();
                    }
                }
            }
        }

        private void btnCallPhone_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var makh = (int?)grvMuaThue.GetFocusedRowCellValue("MaKH");
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
            try
            {
                int macv;
                int? idSPMT = null;
                var existRecord = db.mglMTCongViecs.FirstOrDefault(p => p.MaBC == arrMaBC[0]);
                if (existRecord != null)
                {
                    macv = existRecord.ID;
                    existRecord.MaNV = Common.StaffID;
                    existRecord.NgayNhap = DateTime.Now;
                    existRecord.DienGiai = "Gọi điện";
                    existRecord.MaTT = 1;

                    var existSP = db.mglMTSanPhams.FirstOrDefault(p => p.MaMT == (int)grvMuaThue.GetFocusedRowCellValue("MaMT") && p.MaCV == macv);
                    if (existSP == null)
                    {
                        var itemsave = new mglMTSanPham();
                        itemsave.MaMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
                        itemsave.MaCV = macv;
                        itemsave.MaTT = 1;
                        itemsave.MaHT = 3; // gọi điện
                        itemsave.MaLoai = 1; // chào khách hàng
                        itemsave.MaNLH = (int?)grvAvatar.GetFocusedRowCellValue("MaNDD");
                        itemsave.StartDate = DateTime.Now;
                        itemsave.UpdateDate = DateTime.Now;
                        itemsave.MaNV = Common.StaffID;
                        db.mglMTSanPhams.InsertOnSubmit(itemsave);
                        db.SubmitChanges();
                        idSPMT = itemsave.ID;
                    }
                    else
                    {
                        existSP.UpdateDate = DateTime.Now;
                        existSP.MaNV = Common.StaffID;
                        idSPMT = existSP.ID;
                    }
                }
                else
                {
                    var objmgbccv = new mglMTCongViec();
                    objmgbccv.MaBC = arrMaBC[0];
                    objmgbccv.MaNV = Common.StaffID;
                    objmgbccv.NgayNhap = DateTime.Now;
                    objmgbccv.DienGiai = "Gọi điện";
                    objmgbccv.MaTT = 1;
                    db.mglMTCongViecs.InsertOnSubmit(objmgbccv);
                    db.SubmitChanges();

                    var itemsave = new mglMTSanPham();
                    var id = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
                    itemsave.MaMT = id;
                    itemsave.MaCV = objmgbccv.ID;
                    itemsave.MaTT = 1;
                    itemsave.MaHT = 3; // goi dien
                    itemsave.MaLoai = 1; // chào khách hàng
                    itemsave.MaNLH = (int?)grvAvatar.GetFocusedRowCellValue("MaNDD");
                    itemsave.StartDate = DateTime.Now;
                    itemsave.UpdateDate = DateTime.Now;
                    itemsave.MaNV = Common.StaffID;
                    db.mglMTSanPhams.InsertOnSubmit(itemsave);
                    db.SubmitChanges();
                    macv = objmgbccv.ID;
                    idSPMT = itemsave.ID;
                }
                var objls = new mglLichSuXuLyGiaoDich_MT();
                objls.MaNV = Common.StaffID;
                objls.NoiDung = "";
                objls.MaBC = arrMaBC[0];
                objls.MaMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
                objls.MaLoai = 1;
                objls.MaMTCV = idSPMT;
                objls.Title = "Chào mặt bằng";
                objls.NgayXL = DateTime.Now;
                objls.MaPT = 2;
                db.mglLichSuXuLyGiaoDich_MTs.InsertOnSubmit(objls);

                db.SubmitChanges();
            }
            catch
            {
            }
            finally
            {
                db.Dispose();
            }

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

            var objConfig = db.voipServerConfigs.FirstOrDefault();

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
            frC.MaBC = arrMaBC[0];
            // frC.ModuleID = 0;
            frC.line = objLine.LineNumber;
            frC.NhanVienTN = Common.StaffName;
            frC.LoaiCG = 0;
            //frC.ShowDialog();
        }

        private void btnKhac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvMuaThue.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Infomation("Bạn vui lòng chọn khách hàng để gửi Email!");
                return;
            }
            var frm = new frmOther();
            frm.ShowDialog();
            if (frm.isSave == true)
            {
                int recourd = 0;
                foreach (var item in arrMaBC)
                {
                    int macv;
                    int? idSPMT = null;
                    var existRecord = db.mglMTCongViecs.FirstOrDefault(p => p.MaBC == item);
                    if (existRecord != null)
                    {
                        macv = existRecord.ID;
                        existRecord.MaBC = item;
                        existRecord.MaNV = Common.StaffID;
                        existRecord.NgayNhap = DateTime.Now;
                      
                        existRecord.DienGiai = "Gửi Mail";
                        existRecord.MaTT = 1;
                        db.SubmitChanges();
                    }
                    else
                    {
                        var objmgbccv = new mglMTCongViec();
                        objmgbccv.MaBC = item;
                        objmgbccv.MaNV = Common.StaffID;
                        objmgbccv.NgayNhap = DateTime.Now;
                        objmgbccv.DienGiai = "Khác";
                        objmgbccv.MaTT = 1;


                        db.mglMTCongViecs.InsertOnSubmit(objmgbccv);
                        db.SubmitChanges();
                        var itemsave = new mglMTSanPham();

                        itemsave.MaMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
                        itemsave.MaCV = objmgbccv.ID;
                        itemsave.MaTT = 1;
                        itemsave.MaHT = 1; // gửi mail
                        itemsave.MaLoai = 1; // chào khách hàng
                        itemsave.MaNLH = (int?)grvAvatar.GetFocusedRowCellValue("MaNDD");
                        itemsave.StartDate = DateTime.Now;
                        itemsave.UpdateDate = DateTime.Now;
                        itemsave.MaNV = Common.StaffID;
                        db.mglMTSanPhams.InsertOnSubmit(itemsave);
                        db.SubmitChanges();
                        macv = objmgbccv.ID;
                        idSPMT = itemsave.ID;
                    }
                    var itemother = new mglLichSuOtherMT();
                    itemother.MaMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
                    itemother.MaNV = Common.StaffID;
                    itemother.MaCV = macv;
                    itemother.NgayNhap = DateTime.Now;
                    itemother.NgayXL = DateTime.Now;
                    itemother.NoiDung = frm.NoiDung;
                    db.mglLichSuOtherMTs.InsertOnSubmit(itemother);
                    db.SubmitChanges();
                    recourd++;
                }
            }
        }

        private void btnZalo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvMuaThue.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Infomation("Bạn vui lòng chọn khách hàng để gửi Email!");
                return;
            }

            //mailConfig mailcf;
            //try
            //{
            //    mailcf = db.mailConfigs.Where(p => p.StaffID == Common.StaffID).FirstOrDefault();
            //    if (mailcf == null)
            //    {
            //        DialogBox.Error("Bạn chưa cấu hình Email gửi, vui lòng kiểm tra lại");
            //        return;
            //    }
            //}
            //catch
            //{
            //    DialogBox.Error("Bạn chưa cấu hình Email gửi, vui lòng kiểm tra lại");
            //    return;
            //}

            var tt = new MGL.ThongTinGui();
            tt.ID = 1;
            tt.ShowDialog();

            var arrMaMT = new List<int>();
            var dicMail = new List<Dictionary<string, ThuVien.KhachHang>>();

            var lstMaKh = new List<BEE.ThuVien.KhachHang>();
            foreach (var i in grvMuaThue.GetSelectedRows())
            {
                if (!string.IsNullOrEmpty(grvMuaThue.GetRowCellValue(i, "MaKH").ToString()))
                {
                    lstMaKh.Add(db.KhachHangs.SingleOrDefault(p => p.MaKH == (int?)grvMuaThue.GetRowCellValue(i, "MaKH")));
                }
                var maMT = (int)grvMuaThue.GetRowCellValue(i, "MaMT");
                arrMaMT.Add(maMT);
            }


            foreach (var item in lstMaKh)
            {
                string noidung = "";
                int j = 1;
                var objKH = db.KhachHangs.SingleOrDefault(p => p.MaKH == item.MaKH);

                // lấy thông tin khách hàng cần gửi
                try
                {
                    //if (!string.IsNullOrEmpty(mailcf.Logo1))
                    //    noidung += "<img  src='" + mailcf.Logo1 + "'/>";

                    noidung += "<p><b>Kính gửi " + (objKH.IsPersonal == true ? objKH.HoKH + " " + objKH.TenKH : objKH.TenCongTy) + "</b></p>";
                    noidung += "<p>" + "Công ty cổ phần HoaLand gửi Anh thông tin mặt bằng phù hợp với mô hình <b>" + (objKH.IsPersonal == true ? objKH.MoHinh : objKH.ThuongHieu) + "</b>: " + "</p>";
                }
                catch
                {

                    DialogBox.Error("Bạn chưa cấu hình Email gửi, vui lòng kiểm tra lại");
                    return;
                }
                var objDic = new Dictionary<string, ThuVien.KhachHang>();
                foreach (var i in arrMaBC)
                {
                    var objBC = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == i);
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

                    try
                    {
                        //noidung += "<p>Trân trọng cảm ơn!</p>";
                        //if (!string.IsNullOrEmpty(mailcf.Logo2))
                        //    noidung += "<img src='" + mailcf.Logo2 + "'/>";
                    }
                    catch { }
                    j++;
                }
                objDic.Add(noidung, item);
                dicMail.Add(objDic);
            }

            using (var frm = new BEE.HoatDong.MGL.frmSend() { dicMail = dicMail, MaPT = 2 })
            {
                frm.ShowDialog();
                if (frm.isSend)
                {
                    // lưu dữ liệu
                    try
                    {

                        foreach (var item in arrMaBC)
                        {
                            int macv;
                            var existRecord = db.mglMTCongViecs.FirstOrDefault(p => p.MaBC == item);
                            if (existRecord != null)
                            {
                                existRecord.MaNV = Common.StaffID;
                                existRecord.NgayNhap = DateTime.Now;

                                var existSP = db.mglMTSanPhams.FirstOrDefault(p => p.MaMT == arrMaMT[0] && p.MaCV == existRecord.ID);
                                if (existSP == null)
                                {
                                    var itemsave = new mglMTSanPham();
                                    itemsave.MaMT = arrMaMT[0];
                                    itemsave.MaCV = existRecord.ID;
                                    itemsave.MaTT = 1;
                                    itemsave.MaHT = 5; // gọi điện
                                    itemsave.MaLoai = 1; // chào khách hàng
                                    itemsave.MaNLH = (int?)grvAvatar.GetFocusedRowCellValue("MaNDD");
                                    itemsave.StartDate = DateTime.Now;
                                    itemsave.UpdateDate = DateTime.Now;
                                    itemsave.MaNV = Common.StaffID;                                   
                                    db.mglMTSanPhams.InsertOnSubmit(itemsave);
                                    db.SubmitChanges();
                                    macv = itemsave.ID;
                                }
                                else
                                {
                                    existSP.UpdateDate = DateTime.Now;
                                    existSP.MaNV = Common.StaffID;
                                    db.SubmitChanges();
                                    macv = existSP.ID;
                                }
                            }
                            else
                            {
                                var objmgbccv = new mglMTCongViec();
                                objmgbccv.MaBC = item;
                                objmgbccv.MaNV = Common.StaffID;
                                objmgbccv.NgayNhap = DateTime.Now;
                                objmgbccv.DienGiai = "Gửi Zalo";
                                objmgbccv.MaTT = 1;

                                db.mglMTCongViecs.InsertOnSubmit(objmgbccv);
                                db.SubmitChanges();

                                var itemsave = new mglMTSanPham();
                                var id = arrMaMT[0];
                                itemsave.MaMT = id;
                                itemsave.MaCV = objmgbccv.ID;
                                itemsave.MaTT = 1;
                                itemsave.MaHT = 5; // gửi zalo
                                itemsave.MaLoai = 1; // chào khách hàng
                                itemsave.MaNLH = (int?)grvAvatar.GetFocusedRowCellValue("MaNDD");
                                itemsave.StartDate = DateTime.Now;
                                itemsave.UpdateDate = DateTime.Now;
                                itemsave.MaNV = Common.StaffID;
                                db.mglMTSanPhams.InsertOnSubmit(itemsave);
                                db.SubmitChanges();
                                macv = itemsave.ID;
                            }

                            var objls = new mglLichSuXuLyGiaoDich_MT();
                            objls.MaNV = Common.StaffID;
                            objls.NoiDung = "";
                            objls.MaBC = item;
                            objls.MaMT = arrMaMT[0];
                            objls.MaLoai = 1;
                            objls.MaMTCV = macv;
                            objls.Title = "Chào mặt bằng";
                            objls.NgayXL = DateTime.Now;
                            
                            objls.MaPT = 5;
                            db.mglLichSuXuLyGiaoDich_MTs.InsertOnSubmit(objls);

                            db.SubmitChanges();
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        db.Dispose();
                    }
                }
            }
        }

        private void TabPage_Load()
        {
            try
            {
                int? maMT = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
                if (maMT == null)
                {
                    gcAvatar.DataSource = null;
                    return;
                }

                switch (tabMain.SelectedTabPageIndex)
                {
                    case 0:
                        db = new MasterDataContext();
                        var obj = db.mglmtPhanQuyens.Single(p => p.MaNV == Common.StaffID);

                        if (obj.DienThoai == false)
                        {
                            gcAvatar.DataSource = db.NguoiDaiDien_getByMaKH((int?)grvMuaThue.GetFocusedRowCellValue("MaKH")).Select(mt => new
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
                            gcAvatar.DataSource = db.NguoiDaiDien_getByMaKH((int?)grvMuaThue.GetFocusedRowCellValue("MaKH")).Select(mt => new
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
                            gcAvatar.DataSource = db.NguoiDaiDien_getByMaKH((int?)grvMuaThue.GetFocusedRowCellValue("MaKH")).Select(mt => new
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
                            gcAvatar.DataSource = db.NguoiDaiDien_getByMaKH((int?)grvMuaThue.GetFocusedRowCellValue("MaKH")).ToList();
                        break;
                }
            }
            catch
            {
                gcAvatar.DataSource = null;
            }
        }

        private void grvMuaThue_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TabPage_Load();
        }
    }

}