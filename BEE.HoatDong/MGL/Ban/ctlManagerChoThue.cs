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
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace BEE.HoatDong.MGL.Ban
{
    public partial class ctlManagerChoThue : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();
        public int TotalCount { get; set; }
        public bool TypeID { get; set; }
        public int MaNVKD { get; set; }
        public int MaNVQL { get; set; }
        public int MaNKD { get; set; }
        public int MaPB { get; set; }
        int? countData = 0;
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
            colGhiAm.Visible = false;
            colDongBoGhiAm.Visible = false;
            colDongBoThuCong.Visible = false;
            itemTaiFileGhiAm.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

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
                        case 93:
                            colGhiAm.Visible = true;
                            colDongBoGhiAm.Visible = true;
                            break;
                        case 94:
                            itemTaiFileGhiAm.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            break;
                        case 95:
                            colDongBoThuCong.Visible = true;
                            break;
                    }
                }
            }
        }
        void LoadPermissionLichSuLV()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 218;
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
                var strMaNKH = "," + (chkNhomKH.EditValue ?? "").ToString().Replace(" ", "") + ",";
                var strStatusNK = "," + (chkStatusCall.EditValue ?? "").ToString().Replace(" ", "") + ",";

                switch (GetAccessData())
                {

                    case 1://Tat ca
                        db.CommandTimeout = 90000;
                        #region Tat ca

                        #region  if (obj.DienThoai == false)
                        if (obj.DienThoai == false)
                        {
                            var data = db.mglbcBanChoThue_getByDate_Paging_Search
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                    VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
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
                                    DienThoai = Common.Right(p.DienThoai, 3), // không check thì ko dc xem 3 số 
                                    DiDong2 = Common.Right(p.DiDong2, 3),
                                    DiDong3 = Common.Right(p.DiDong3, 3),
                                    DiDong4 = Common.Right(p.DiDong4, 3),
                                    p.TenNKH,
                                    p.PhiMGCT,
                                    p.PhiMGDT,
                                    p.Color,
                                    p.CountSerch,
                                    p.TotalRows
                                }).ToList();
                            gcBan.DataSource = data;

                            var objTotal = data.FirstOrDefault();
                            countData = Convert.ToInt32(objTotal.CountSerch);
                            this.TotalCount = objTotal.TotalRows ?? 0;
                            Thread newthread = new Thread(CountV2);
                            newthread.Start();

                        }
                        #endregion

                        #region else if (obj.DienThoai3Dau == false)
                        else if (obj.DienThoai3Dau == false)
                        {

                            var obj2 = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == 93604);

                            var data = db.mglbcBanChoThue_getByDate_Paging_Search
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                    VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
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
                                    DiDong4 = Common.Right1(p.DiDong4, 3),
                                    p.TenNKH,
                                    p.PhiMGCT,
                                    p.PhiMGDT,
                                    p.Color,
                                    p.CountSerch,
                                    p.TotalRows
                                }).ToList();
                            gcBan.DataSource = data;


                            var objTotal = data.FirstOrDefault();
                            countData = (int?)objTotal.CountSerch;
                            this.TotalCount = objTotal.TotalRows ?? 0;
                            Thread newthread = new Thread(CountV2);
                            newthread.Start();


                        }
                        #endregion
                        #region else if (obj.dienthoaian == false)
                        else if (obj.DienThoaiAn == false)
                        {

                            var data = db.mglbcBanChoThue_getByDate_Paging_Search
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
                                    (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                        VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
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
                                        DiDong4 = "",
                                        p.TenNKH,
                                        p.PhiMGCT,
                                        p.PhiMGDT,
                                        p.Color,
                                        p.CountSerch,
                                        p.TotalRows
                                    }).ToList();

                            gcBan.DataSource = data;
                            var objTotal = data.FirstOrDefault();
                            countData = Convert.ToInt32(objTotal.CountSerch);
                            this.TotalCount = objTotal.TotalRows ?? 0;
                            Thread newthread = new Thread(CountV2);
                            newthread.Start();

                        }
                        #endregion
                        #region else
                        else
                        {
                            var data = db.mglbcBanChoThue_getByDate_Paging_Search
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
                                 (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                     VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
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
                                     p.DiDong4,
                                     p.TenNKH,
                                     p.PhiMGCT,
                                     p.PhiMGDT,
                                     p.Color,
                                     p.CountSerch,
                                     p.TotalRows
                                 }).ToList();

                            gcBan.DataSource = data;
                            var objTotal = data.FirstOrDefault();
                            countData = Convert.ToInt32(objTotal.CountSerch);
                            this.TotalCount = objTotal.TotalRows ?? 0;
                            Thread newthread = new Thread(CountV2);
                            newthread.Start();


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
                            var data = db.mglbcBanChoThue_getByDate_Paging_Search
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                    VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
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
                                    DiDong4 = Common.Right(p.DiDong4, 3),
                                    p.PhiMGCT,
                                    p.PhiMGDT,
                                    p.CountSerch,
                                    p.TotalRows,
                                }).ToList();


                            gcBan.DataSource = data;
                            var objTotal = data.FirstOrDefault();
                            countData = Convert.ToInt32(objTotal.CountSerch);
                            this.TotalCount = objTotal.TotalRows ?? 0;
                            Thread newthread = new Thread(CountV2);
                            newthread.Start();

                        }
                        #endregion

                        #region else if (obj.DienThoai3Dau == false)
                        else if (obj.DienThoai3Dau == false)
                        {
                            var data = db.mglbcBanChoThue_getByDate_Paging_Search
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
                                )
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MauNen,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
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
                                    DiDong4 = Common.Right1(p.DiDong4, 3),
                                    p.PhiMGCT,
                                    p.PhiMGDT,
                                    p.CountSerch,
                                    p.TotalRows
                                }).ToList();


                            gcBan.DataSource = data;
                            var objTotal = data.FirstOrDefault();
                            countData = Convert.ToInt32(objTotal.CountSerch);
                            this.TotalCount = objTotal.TotalRows ?? 0;
                            Thread newthread = new Thread(CountV2);
                            newthread.Start();
                        }
                        #endregion
                        #region else if (obj.DienThoaian == false)
                        else if (obj.DienThoaiAn == false)
                        {
                            var data = db.mglbcBanChoThue_getByDate_Paging_Search
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
                                )
                                .Select(p => new
                                {
                                    p.STT,
                                    p.MaBC,
                                    p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    p.MauNen,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
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
                                    DiDong4 = "",
                                    p.PhiMGCT,
                                    p.PhiMGDT,
                                    p.CountSerch,
                                    p.TotalRows
                                }).ToList();


                            gcBan.DataSource = data;
                            var objTotal = data.FirstOrDefault();
                            countData = Convert.ToInt32(objTotal.CountSerch);
                            this.TotalCount = objTotal.TotalRows ?? 0;
                            Thread newthread = new Thread(CountV2);
                            newthread.Start();

                        }
                        #endregion
                        #region else
                        else
                        {
                            var data = db.mglbcBanChoThue_getByDate_Paging_Search
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                    VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
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
                                    p.DiDong4,
                                    p.PhiMGCT,
                                    p.PhiMGDT,
                                    p.CountSerch,
                                    p.TotalRows
                                }).ToList();


                            gcBan.DataSource = data;
                            var objTotal = data.FirstOrDefault();
                            countData = Convert.ToInt32(objTotal.CountSerch);
                            this.TotalCount = objTotal.TotalRows ?? 0;
                            Thread newthread = new Thread(CountV2);
                            newthread.Start();

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
                            var data = db.mglbcBanChoThue_getByDate_Paging_Search
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
                                 (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                     VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
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
                                     p.PhiMGCT,
                                     p.PhiMGDT,
                                     p.CountSerch,
                                     p.TotalRows
                                 }).ToList();


                            gcBan.DataSource = data;
                            var objTotal = data.FirstOrDefault();
                            countData = Convert.ToInt32(objTotal.CountSerch);
                            this.TotalCount = objTotal.TotalRows ?? 0;
                            Thread newthread = new Thread(CountV2);
                            newthread.Start();

                        }
                        #endregion

                        #region else if (obj.DienThoai3Dau == false)
                        else if (obj.DienThoai3Dau == false)
                        {
                            var data = db.mglbcBanChoThue_getByDate_Paging_Search
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                    VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
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
                                    p.PhiMGCT,
                                    p.PhiMGDT,
                                    p.CountSerch,
                                    p.TotalRows
                                }).ToList();


                            gcBan.DataSource = data;
                            var objTotal = data.FirstOrDefault();
                            countData = Convert.ToInt32(objTotal.CountSerch);
                            this.TotalCount = objTotal.TotalRows ?? 0;
                            Thread newthread = new Thread(CountV2);
                            newthread.Start();
                        }
                        #endregion

                        #region else if (obj.Dienthoaian == false)
                        else if (obj.DienThoaiAn == false)
                        {
                            var data = db.mglbcBanChoThue_getByDate_Paging_Search
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                    VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
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
                                    p.PhiMGCT,
                                    p.PhiMGDT,
                                    p.CountSerch,
                                    p.TotalRows
                                }).ToList();


                            gcBan.DataSource = data;
                            var objTotal = data.FirstOrDefault();
                            countData = Convert.ToInt32(objTotal.CountSerch);
                            this.TotalCount = objTotal.TotalRows ?? 0;
                            Thread newthread = new Thread(CountV2);
                            newthread.Start();

                        }
                        #endregion
                        #region else
                        else
                        {
                            var data = db.mglbcBanChoThue_getByDate_Paging_Search
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                    VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
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
                                    p.PhiMGCT,
                                    p.PhiMGDT,
                                    p.CountSerch,
                                    p.TotalRows
                                }).ToList();


                            gcBan.DataSource = data;
                            var objTotal = data.FirstOrDefault();
                            countData = Convert.ToInt32(objTotal.CountSerch);
                            this.TotalCount = objTotal.TotalRows ?? 0;
                            Thread newthread = new Thread(CountV2);
                            newthread.Start();

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
                            var data = db.mglbcBanChoThue_getByDate_Paging_Search
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
                                 (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                     VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
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
                                     p.PhiMGCT,
                                     p.PhiMGDT,
                                     p.CountSerch,
                                     p.TotalRows
                                 }).ToList();


                            gcBan.DataSource = data;
                            var objTotal = data.FirstOrDefault();
                            countData = Convert.ToInt32(objTotal.CountSerch);
                            this.TotalCount = objTotal.TotalRows ?? 0;
                            Thread newthread = new Thread(CountV2);
                            newthread.Start();


                        }
                        #endregion

                        #region  else if (obj.DienThoai3Dau == false)
                        else if (obj.DienThoai3Dau == false)
                        {
                            var data = db.mglbcBanChoThue_getByDate_Paging_Search
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                    VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
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
                                    p.PhiMGCT,
                                    p.PhiMGDT,
                                    p.CountSerch,
                                    p.TotalRows
                                }).ToList();


                            gcBan.DataSource = data;
                            var objTotal = data.FirstOrDefault();
                            countData = Convert.ToInt32(objTotal.CountSerch);
                            this.TotalCount = objTotal.TotalRows ?? 0;
                            Thread newthread = new Thread(CountV2);
                            newthread.Start();

                        }
                        #endregion
                        #region  else if (obj.dienthoaian == false)
                        else if (obj.DienThoaiAn == false)
                        {
                            var data = db.mglbcBanChoThue_getByDate_Paging_Search
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                    VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
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
                                    p.PhiMGCT,
                                    p.PhiMGDT,
                                    p.CountSerch,
                                    p.TotalRows
                                }).ToList();


                            gcBan.DataSource = data;
                            var objTotal = data.FirstOrDefault();
                            countData = Convert.ToInt32(objTotal.CountSerch);
                            this.TotalCount = objTotal.TotalRows ?? 0;
                            Thread newthread = new Thread(CountV2);
                            newthread.Start();

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
                                txtDonViTC.Text, txtDonViDT.Text, (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
                                txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text, txtPhapLy.Text, txtSoDK.Text,
                                txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text, txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
                                (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text, cmbDKGiaThue.Text
                                , cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
                                cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
                                cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
                                cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
                                cmbNgayNhap.Text,
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                    VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
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
                                    p.PhiMGCT,
                                    p.PhiMGDT,
                                    p.CountSerch,
                                    p.TotalRows
                                }).ToList();


                            gcBan.DataSource = data;
                            var objTotal = data.FirstOrDefault();
                            countData = Convert.ToInt32(objTotal.CountSerch);
                            this.TotalCount = objTotal.TotalRows ?? 0;
                            Thread newthread = new Thread(CountV2);
                            newthread.Start();

                        }
                        #endregion

                        #endregion
                        break;
                    default:

                        break;
                }
            }
            catch (Exception ex)
            {
            }
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
            frm.Show();
            if (frm.IsSave)
            {
                Ban_Load();
            }
        }

        private void Ban_Delete()
        {
            try
            {

                var indexs = grvBan.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn phiếu đăng ký");
                    return;
                }

                if (DialogBox.Question() == DialogResult.No) return;
                db = new MasterDataContext();
                foreach (var i in indexs)
                {
                    bool ck = (GetAccessData() == 1 || (int)grvBan.GetRowCellValue(i, "MaNVQL") == Common.StaffID) || ((int)grvBan.GetRowCellValue(i, "MaNVKD") == Common.StaffID);
                    if (!ck)
                        return;
                    var objBC = db.mglbcBanChoThues.SingleOrDefault(p => p.MaBC == (int)grvBan.GetRowCellValue(i, "MaBC"));
                    objBC.MaTTD = 1;
                }
                db.SubmitChanges();
                DialogBox.Infomation("Sản phẩm cho thuê đã chuyển sang mục chờ duyệt xóa");
                Ban_Load();
            }
            catch (Exception ex)
            {

            }
        }
        public class itemLoai
        {
            public int? MaMT { get; set; }
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


            var objsp = (from sp in db.mglMTSanPhams
                         join cv in db.mglMTCongViecs on sp.MaCV equals cv.ID
                         where cv.MaBC == objGD.MaBC
                         select new itemLoai
                         {
                             MaMT = sp.MaMT
                         }
                                    ).ToList();


            foreach (var x in objsp)
            {
                if (objLoc.Where(p => p.MaMT == x.MaMT).ToList().Count() > 0)
                {
                    var objremove = objLoc.FirstOrDefault(p => p.MaMT == x.MaMT);
                    objLoc.Remove(objremove);
                }
            }


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
                    var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);

            switch (tabMain.SelectedTabPageIndex)
            {
                case 0:
                    #region Lich lam viec

                    if (obj.DienThoai == false)
                    {
                        var listNhatKy = (from p in db.mglbcNhatKyXuLies
                                          join nv in db.NhanViens on p.MaNVG equals nv.MaNV into nhanvien
                                          from nv in nhanvien.DefaultIfEmpty()
                                          join nv1 in db.NhanViens on p.MaNVN equals nv1.MaNV into nhanvien1
                                          from nv1 in nhanvien1.DefaultIfEmpty()
                                          join pt in db.PhuongThucXuLies on p.MaPT equals pt.MaPT into phuongthuc
                                          from pt in phuongthuc.DefaultIfEmpty()
                                          join t in db.bdsStatusCalls on p.MaTTGoi equals t.MaTT into ttt
                                          from tt in ttt.DefaultIfEmpty()
                                          where p.MaBC == maBC
                                          orderby p.NgayXL descending
                                          select new
                                          {
                                              p.ID,
                                              p.NgayXL,
                                              p.TieuDe,
                                              p.MaTT,
                                              p.NoiDung,
                                              p.MaNVG,
                                              HoTenNVG = nv.HoTen,
                                              HoTenNVN = nv1.HoTen,
                                              p.KetQua,
                                              pt.TenPT,
                                              p.StartDate,
                                              p.Enddate,
                                              StatusCall = tt.TenTT,
                                              MaTTBefore = p.MaTTBefore,
                                              p.FileRecord,
                                              isSyncFileRecord = p.FileRecord == null ? false : true,
                                              DiDong = Common.Right(p.DiDong, 3)
                                          }).ToList();
                        gcNhatKy.DataSource = listNhatKy;
                    }
                    else if (obj.DienThoai3Dau == false)
                    {
                        var listNhatKy = (from p in db.mglbcNhatKyXuLies
                                          join nv in db.NhanViens on p.MaNVG equals nv.MaNV into nhanvien
                                          from nv in nhanvien.DefaultIfEmpty()
                                          join nv1 in db.NhanViens on p.MaNVN equals nv1.MaNV into nhanvien1
                                          from nv1 in nhanvien1.DefaultIfEmpty()
                                          join pt in db.PhuongThucXuLies on p.MaPT equals pt.MaPT into phuongthuc
                                          from pt in phuongthuc.DefaultIfEmpty()
                                          join t in db.bdsStatusCalls on p.MaTTGoi equals t.MaTT into ttt
                                          from tt in ttt.DefaultIfEmpty()
                                          where p.MaBC == maBC
                                          orderby p.NgayXL descending
                                          select new
                                          {
                                              p.ID,
                                              p.NgayXL,
                                              p.TieuDe,
                                              p.MaTT,
                                              p.NoiDung,
                                              p.MaNVG,
                                              HoTenNVG = nv.HoTen,
                                              HoTenNVN = nv1.HoTen,
                                              p.KetQua,
                                              pt.TenPT,
                                              p.StartDate,
                                              p.Enddate,
                                              StatusCall = tt.TenTT,
                                              MaTTBefore = p.MaTTBefore,
                                              p.FileRecord,
                                              isSyncFileRecord = p.FileRecord == null ? false : true,
                                              DiDong = Common.Right1(p.DiDong, 3)
                                          }).ToList();
                        gcNhatKy.DataSource = listNhatKy;

                    }
                    else if (obj.DienThoaiAn == false)
                    {
                        var listNhatKy = (from p in db.mglbcNhatKyXuLies
                                          join nv in db.NhanViens on p.MaNVG equals nv.MaNV into nhanvien
                                          from nv in nhanvien.DefaultIfEmpty()
                                          join nv1 in db.NhanViens on p.MaNVN equals nv1.MaNV into nhanvien1
                                          from nv1 in nhanvien1.DefaultIfEmpty()
                                          join pt in db.PhuongThucXuLies on p.MaPT equals pt.MaPT into phuongthuc
                                          from pt in phuongthuc.DefaultIfEmpty()
                                          join t in db.bdsStatusCalls on p.MaTTGoi equals t.MaTT into ttt
                                          from tt in ttt.DefaultIfEmpty()
                                          where p.MaBC == maBC
                                          orderby p.NgayXL descending

                                          select new
                                          {
                                              p.ID,
                                              p.NgayXL,
                                              p.TieuDe,
                                              p.MaTT,
                                              p.NoiDung,
                                              p.MaNVG,
                                              HoTenNVG = nv.HoTen,
                                              HoTenNVN = nv1.HoTen,
                                              p.KetQua,
                                              pt.TenPT,
                                              p.StartDate,
                                              p.Enddate,
                                              StatusCall = tt.TenTT,
                                              MaTTBefore = p.MaTTBefore,
                                              p.FileRecord,
                                              isSyncFileRecord = p.FileRecord == null ? false : true,
                                              DiDong = ""
                                          }).ToList();
                        gcNhatKy.DataSource = listNhatKy;
                    }
                    else
                    {
                        var listNhatKy = (from p in db.mglbcNhatKyXuLies
                                          join nv in db.NhanViens on p.MaNVG equals nv.MaNV into nhanvien
                                          from nv in nhanvien.DefaultIfEmpty()
                                          join nv1 in db.NhanViens on p.MaNVN equals nv1.MaNV into nhanvien1
                                          from nv1 in nhanvien1.DefaultIfEmpty()
                                          join pt in db.PhuongThucXuLies on p.MaPT equals pt.MaPT into phuongthuc
                                          from pt in phuongthuc.DefaultIfEmpty()
                                          join t in db.bdsStatusCalls on p.MaTTGoi equals t.MaTT into ttt
                                          from tt in ttt.DefaultIfEmpty()
                                          where p.MaBC == maBC
                                          orderby p.NgayXL descending
                                          select new
                                          {
                                              p.ID,
                                              p.NgayXL,
                                              p.TieuDe,
                                              p.MaTT,
                                              p.NoiDung,
                                              p.MaNVG,
                                              HoTenNVG = nv.HoTen,
                                              HoTenNVN = nv1.HoTen,
                                              p.KetQua,
                                              pt.TenPT,
                                              p.StartDate,
                                              p.Enddate,
                                              StatusCall = tt.TenTT,
                                              MaTTBefore = p.MaTTBefore,
                                              p.FileRecord,
                                              isSyncFileRecord = p.FileRecord == null ? false : true,
                                              p.DiDong
                                          }).ToList();
                        gcNhatKy.DataSource = listNhatKy;

                    }
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
                            mt.DiaChiTT,
                            mt.HoTenNV,
                            mt.NgayNhap
                        }).ToList();
                    }
                    else if (obj.DienThoai3Dau == false)
                    {
                        var MaKH = (int?)grvBan.GetFocusedRowCellValue("MaKH");
                        gridControlAvatar.DataSource = db.NguoiDaiDien_getByMaKH(MaKH).Select(mt => new
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
                            mt.DiaChiTT,
                            mt.HoTenNV,
                            mt.NgayNhap
                        }).ToList();
                    }
                    else
                        gridControlAvatar.DataSource = db.NguoiDaiDien_getByMaKH((int?)grvBan.GetFocusedRowCellValue("MaKH")).ToList();
                    break;
                case 7:
                    #region ghi nhan
                    var count = db.mglbcHistoryChanges.Where(p => p.MaBC == maBC).Count();
                    if (count > 0)
                    {
                        switch (GetAccessDataHistory())
                        {
                            case 1://Tat ca
                                gcGhiNhan.DataSource = LichSuGhiNhan((int)maBC, -1, -1, -1, Common.StaffID, true);
                                break;
                            case 2://Theo phong ban 
                                gcGhiNhan.DataSource = LichSuGhiNhan((int)maBC, -1, -1, Common.DepartmentID, Common.StaffID, true);
                                break;
                            case 3://Theo nhom
                                gcGhiNhan.DataSource = LichSuGhiNhan((int)maBC, -1, Common.GroupID, -1, Common.StaffID, true);
                                break;
                            case 4://Theo nhan vien
                                gcGhiNhan.DataSource = LichSuGhiNhan((int)maBC, Common.StaffID, -1, -1, Common.StaffID, true);
                                break;
                            default:
                                gcGhiNhan.DataSource = null;
                                break;
                        }
                    }
                    else
                    {
                        gcGhiNhan.DataSource = null;
                    }
                    //(from ls in db.mglbcLichSus
                    //                                                                                         join nv in db.NhanViens on ls.MaNVS equals nv.MaNV
                    //                                                                                         where ls.MaBC == maBC
                    //                                                                                         select new
                    //                                                                                         {
                    //                                                                                             ls.ID,
                    //                                                                                             ls.TenDuong,
                    //                                                                                             ls.SoNha,
                    //                                                                                             ls.MatTien,
                    //                                                                                             ls.DienTich,
                    //                                                                                             ls.GiaTien,
                    //                                                                                             ls.DacTrung,
                    //                                                                                             ls.ThoiHanHD,
                    //                                                                                             ls.TenKH,
                    //                                                                                             ls.SoDienThoai,
                    //                                                                                             ls.Email,
                    //                                                                                             ls.DiaChi,
                    //                                                                                             ls.ToaDo,
                    //                                                                                             ls.CanBoLV,
                    //                                                                                             nv.HoTen,
                    //                                                                                             ls.NgaySua
                    //                                                                                         }).ToList();
                    #endregion
                    break;
                case 8:
                    gcChiTiet.DataSource = db.mglbcThongTinTTs.Where(p => p.MaBC == maBC);
                    break;

                case 9:
                    gcDaChao.DataSource = db.mglmtMuaThue_List_byID(maBC).ToList();
                    break;
                    //case 10:
                    //    gcDaQuanTam.DataSource = db.mglmtMuaThue_List_byID(maBC, 6).ToList();
                    //    break;
                    //case 11:
                    //    gcDaXem.DataSource = db.mglmtMuaThue_List_byID(maBC, 2).ToList();
                    //    break;

                    //case 12:
                    //    gcDaGapDP.DataSource = db.mglmtMuaThue_List_byID(maBC, 3).ToList();
                    //    break;
                    //case 13:
                    //    gcDaDuaKhGapNhau.DataSource = db.mglmtMuaThue_List_byID(maBC, 7).ToList();
                    //    break;
                    //case 14:
                    //    gcDangSuaHD.DataSource = db.mglmtMuaThue_List_byID(maBC, 8).ToList();
                    //    break;
                    //case 15:
                    //    gcDaKyHd.DataSource = db.mglmtMuaThue_List_byID(maBC, 9).ToList();
                    //    break;
                    //case 16:
                    //    gcDatCoc.DataSource = db.mglmtMuaThue_List_byID(maBC, 4).ToList();
                    //    break;
                    //case 17:
                    //    gcChoThuPhi.DataSource = db.mglmtMuaThue_List_byID(maBC, 5).ToList();
                    //    break;
                    //case 18:
                    //    gcDaThuPhi.DataSource = db.mglmtMuaThue_List_byID(maBC, 10).ToList();
                    //    break;

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
                        colSttLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Ký hiệu":
                        colKyHieu.VisibleIndex = stt[i].STT.Value;
                        colKyHieuLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Nhu cầu":
                        colNhuCau.VisibleIndex = stt[i].STT.Value;
                        colNhuCauLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Loại BDS":
                        colLoaiDBS.VisibleIndex = stt[i].STT.Value;
                        colLoaiBDSLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Trạng thái":
                        colTrangThai.VisibleIndex = stt[i].STT.Value;
                        colTrangThaiLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Số nhà":
                        colSoNha.VisibleIndex = stt[i].STT.Value;
                        colSoNhaLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Khu vực":
                        colKhuVuc.VisibleIndex = stt[i].STT.Value;
                        colKhuVucLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tên đường":
                        colTenDuong.VisibleIndex = stt[i].STT.Value;
                        colDuongLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Dự án":
                        colDuAn.VisibleIndex = stt[i].STT.Value;
                        colDuAnLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Diện tích bán/Cho thuê":
                        colDienTichCT.VisibleIndex = stt[i].STT.Value;
                        colDtChoThueLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Số tầng bán/Cho thuê":
                        colSoTang.VisibleIndex = stt[i].STT.Value;
                        colSoTangLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Mặt tiền":
                        colMatTien.VisibleIndex = stt[i].STT.Value;
                        colMatTienLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Diện tích đất":
                        colDienTichDat.VisibleIndex = stt[i].STT.Value;
                        colDtDatLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Diện tích XD":
                        colDienTichXD.VisibleIndex = stt[i].STT.Value;
                        colDtXsLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Số tầng XD":
                        colSoTangXD.VisibleIndex = stt[i].STT.Value;
                        colSoTangXDLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tọa độ":
                        colKinhDo.VisibleIndex = stt[i].STT.Value;
                        colToaDoLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "P.Ngủ":
                        colPNgu.VisibleIndex = stt[i].STT.Value;
                        colPNguLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Đơn vị thuê cũ":
                        colDonViThueCu.VisibleIndex = stt[i].STT.Value;
                        colDvThueCuLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Phòng VS":
                        colPhongVS.VisibleIndex = stt[i].STT.Value;
                        colPVsLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Đơn vị đang thuê":
                        colDonViDangThue.VisibleIndex = stt[i].STT.Value;
                        colDvDangThueLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Thời gian hợp đồng":
                        colThoiHanHD.VisibleIndex = stt[i].STT.Value;
                        colThoiHanHDLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Thời hạn BGMB":
                        colThoiGianBGMB.VisibleIndex = stt[i].STT.Value;
                        colThoiGianBGLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Ghi chú":
                        colGhiChu.VisibleIndex = stt[i].STT.Value;
                        colGhiChuLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Hướng của":
                        colHuongCua.VisibleIndex = stt[i].STT.Value;
                        colHuongCuaLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Hướng BC":
                        colHuongBC.VisibleIndex = stt[i].STT.Value;
                        colHuongBcLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Giá bán":
                        colGiaChoThue.VisibleIndex = stt[i].STT.Value;
                        colGiaCTLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tổng giá":
                        colTongGia.VisibleIndex = stt[i].STT.Value;
                        colTongGiaLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Loại tiền":
                        colLoaiTien.VisibleIndex = stt[i].STT.Value;
                        colLoaiTienLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Loại đường":
                        colLoaiDuong.VisibleIndex = stt[i].STT.Value;
                        colLoaiDuongLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Đường rộng":
                        colDuongRong.VisibleIndex = stt[i].STT.Value;
                        colDuongRongLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tình trạng SP":
                        colTinhTrangSP.VisibleIndex = stt[i].STT.Value;
                        colTinhTrangSpLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Năm XD":
                        colNamXD.VisibleIndex = stt[i].STT.Value;
                        colNamXDLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Mặt tiền TT":
                        colMatTienTT.VisibleIndex = stt[i].STT.Value;
                        colMatTienTTLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Mặt sau TT":
                        colMatSauTT.VisibleIndex = stt[i].STT.Value;
                        colMatSauTTLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Dài TT":
                        colDaiTT.VisibleIndex = stt[i].STT.Value;
                        colDaiTTLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Oto vào":
                        colOtoVao.VisibleIndex = stt[i].STT.Value;
                        colOtoVaoLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tâng hầm":
                        colTangHam.VisibleIndex = stt[i].STT.Value;
                        colTangHamLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Căn góc":
                        colCanGoc.VisibleIndex = stt[i].STT.Value;
                        colCanGocLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Thang máy":
                        colThangMay.VisibleIndex = stt[i].STT.Value;
                        colThangMayLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Thương lượng":
                        colThuongLuong.VisibleIndex = stt[i].STT.Value;
                        colThuongLuongLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tiện ích":
                        colTienIch.VisibleIndex = stt[i].STT.Value;
                        colTienIchLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Đặc trung":
                        colDacTrung.VisibleIndex = stt[i].STT.Value;
                        colDacTrungLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Nguồn":
                        colNguon.VisibleIndex = stt[i].STT.Value;
                        colNguonLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Pháp lý":
                        colPhapLy.VisibleIndex = stt[i].STT.Value;
                        colPhapLyLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Phí mô giới":
                        colPhiMoGioi.VisibleIndex = stt[i].STT.Value;
                        colPhiMgLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tình trạng HD":
                        colTinhTrangHD.VisibleIndex = stt[i].STT.Value;
                        colTinhTrangHdLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Ngày ĐK":
                        colNgayDK.VisibleIndex = stt[i].STT.Value;
                        colNgayDKLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Khách hàng":
                        colKhachHang.VisibleIndex = stt[i].STT.Value;
                        colKhachHangLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Số ĐK":
                        colSoDK.VisibleIndex = stt[i].STT.Value;
                        colSoDKLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Điện thoại":
                        colDienThoai1.VisibleIndex = stt[i].STT.Value;
                        colDienThoaiLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Người đại diện":
                        colNguoiDD.VisibleIndex = stt[i].STT.Value;
                        colNguoiDaiDienLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tên trung gian":
                        colTenTrungGian.VisibleIndex = stt[i].STT.Value;
                        colTrungGianLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Nhân viên QL":
                        colNhanVienQL.VisibleIndex = stt[i].STT.Value;
                        colNvQlLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Nhân viên MG":
                        colNhanVienMG.VisibleIndex = stt[i].STT.Value;
                        colNvMgLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Ngày CN":
                        colNgayCN.VisibleIndex = stt[i].STT.Value;
                        colNgayCNLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Ngày nhập":
                        colNgayNhap.VisibleIndex = stt[i].STT.Value;
                        //colNgayNhapLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Video link":
                        colViedeo.VisibleIndex = stt[i].STT.Value;
                        colVideoLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Ảnh":
                        colAnh.VisibleIndex = stt[i].STT.Value;
                        colAnhLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Tỉnh":
                        colTinh.VisibleIndex = stt[i].STT.Value;
                        colTinhLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Huyện":
                        colHuyen.VisibleIndex = stt[i].STT.Value;
                        colKhuVucLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Xã":
                        colXa.VisibleIndex = stt[i].STT.Value;
                        colXaLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Nhân viên nhập":
                        colNVNhap.VisibleIndex = stt[i].STT.Value;
                        //colNvNhapLs.VisibleIndex = stt[i].STT.Value;
                        break;

                    case "Điện thoại 2":
                        colDienThoai2.VisibleIndex = stt[i].STT.Value;
                        colDienThoai2Ls.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Điện thoại 3":
                        colDienThoai3.VisibleIndex = stt[i].STT.Value;
                        colDienThoai3Ls.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Điện thoại 4":
                        colDienThoai4.VisibleIndex = stt[i].STT.Value;
                        colDienThoai4Ls.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Video":
                        colVideoBDS.VisibleIndex = stt[i].STT.Value;
                        colVideoBdsLs.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Nhóm KH":
                        colNhomKh.VisibleIndex = stt[i].STT.Value;
                        break;
                    case "Phí MG cần thu":
                        colPhiMGCT.VisibleIndex = stt[i].STT.Value;
                        break;
                }
            }
            colNhanVienSuaLs.VisibleIndex = stt.Count + 1;
            colNgaySuaLs.VisibleIndex = stt.Count + 2;

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
            GetHuyen();
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
            chkStatusCall.Properties.DataSource = db.bdsStatusCalls;
            chkNhomKH.Properties.DataSource = db.NhomKHs.Select(p => new { p.MaNKH, p.TenNKH });
            setFillter(db.mglbcLocTheoTinhs.FirstOrDefault().MaTinh);
            LoadPermission();
            LoadPermissionLichSuLV();

            phanquyen();
            PhanQuyenThemSuaKH();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(Convert.ToInt32(db.mglbcLocNgays.FirstOrDefault().MaBC));
            ThuTuCot();
            itemTuNgay.EditValue = new DateTime(2012, 5, 28);
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
            cboDateCall.SelectedIndex = 0;
            cboVideo.SelectedIndex = 0;
        }

        public string lockhuvuc = ",";
        public void GetHuyen()
        {
            var objt = db.crlHuyenQuanLies.Where(p => p.MaNV == Common.StaffID);

            foreach (var item in objt)
            {
                lockhuvuc += item.MaHuyen + ",";
            }
        }

        //public void count()
        //{
        //    using (MasterDataContext db2 = new MasterDataContext())
        //    {
        //        db2.CommandTimeout = 0;
        //        var tinh = (itemTinh.EditValue + "").ToString().Replace(" ", "");
        //        var matinh = "," + tinh + ",";

        //        int TotalRecord = 0;
        //        var tuNgay = (DateTime?)itemTuNgay.EditValue ?? DateTime.Now;
        //        var denNgay = (DateTime?)itemDenNgay.EditValue ?? DateTime.Now;
        //        var strMaTT = (itemTrangThai.EditValue ?? "").ToString().Replace(" ", "");
        //        var arrMaTT = "," + strMaTT + ",";
        //        int MaNV = Common.StaffID;
        //        try
        //        {
        //            var dtc = db2.mglbcBanChoThue_getByDate_Count_search
        //                              (tuNgay, denNgay, arrMaTT, false, this.MaNVKD, this.MaNVQL, this.MaNVKD, this.MaPB,
        //                              MaNV, true, matinh, lockhuvuc, txtDiaChi.Text.Trim(),
        //                              txtTenDuong.Text.Trim(), txtXa.Text, txtHuyen.Text,
        //                              (decimal?)spinMatTien.EditValue, (decimal?)spinDienTichDat.EditValue,
        //                              (decimal?)spinDienTichXD.EditValue, Convert.ToInt32(spinSoTangXD.EditValue),
        //                              (decimal?)spinDienTichCT.EditValue, Convert.ToInt32(spinSoTangCT.EditValue),
        //                              (decimal?)spinGiaThue.EditValue, (decimal?)spinTongGia.EditValue,
        //                              (DateTime?)dateNgayCN.EditValue, txtDonViTC.Text, txtDonViDT.Text,
        //                              (DateTime?)dateTGBGBM.EditValue, (DateTime?)dateThoiHanHD.EditValue,
        //                              txtNguon.Text, txtTinhTrangHD.Text, txtKhachHang.Text, txtDienThoai.Text,
        //                              txtPhapLy.Text, txtSoDK.Text, txtKyHieu.Text, txtLoaiBDS.Text, txtToaDo.Text,
        //                              txtNhanVienQL.Text, txtNhanVienMQ.Text, (DateTime?)dateNgayNhap.EditValue,
        //                              (DateTime?)dateNgayDK.EditValue, txtNhanVienNhap.Text, cmbAnhBDS.Text,
        //                              cmbDKGiaThue.Text, cmbDKTGBGMB.Text, (DateTime?)dateTGBGBMTo.EditValue, cmbTongGia.Text,
        //                          cmbDienTichDat.Text, cmbDienTichXD.Text, cmbSotangXD.Text, cmbDTChoThue.Text, cmbSoTangCT.Text, cmbMatTien.Text,
        //                          cmbNgayCN.Text, (DateTime?)dateNgayCNTo.EditValue, cmbNgayDK.Text, (DateTime?)dateNgayDKTo.EditValue,
        //                          cmbThoiHanjHD.Text, (DateTime?)dateThoiHanHDTo.EditValue,
        //                          cmbNgayNhap.Text,
        //                          (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue

        //                              ).ToList();
        //            if (dtc.Count > 0)
        //            {
        //                foreach (var item in dtc)
        //                    TotalRecord = (int)item.sl;
        //            }
        //            paging1.TotalRecords = TotalRecord;
        //            if (paging1.InvokeRequired)
        //            {
        //                paging1.Invoke(new MethodInvoker(paging1.RefreshPagination));
        //            }

        //        }
        //        catch
        //        {

        //        }




        //    }

        //}
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

        public void CountV2()
        {
            paging1.TotalRecords = this.TotalCount;
            paging1.TotalData = (int)countData;
            // paging1.totto
            if (paging1.InvokeRequired)
            {
                paging1.Invoke(new MethodInvoker(paging1.RefreshPagination));
            }

        }
        private void itmThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmEdit frm = new frmEdit();
            frm.mucdich = false;
            frm.Show();
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

            frm.MaTT = (byte?)grvBan.GetFocusedRowCellValue("MaTT");
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
            frm.MaBC = (int)grvBan.GetFocusedRowCellValue("MaBC");
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
                else if (e.Column.FieldName == "TenNKH")
                {
                    var color = grvBan.GetRowCellValue(e.RowHandle, "Color");
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

            var objkh = db.KhachHangs.SingleOrDefault(p => p.MaKH == (int?)grvMuaThue.GetFocusedRowCellValue("MaKH"));

            var objMT = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == (int)grvMuaThue.GetFocusedRowCellValue("MaMT"));
            using (var frm = new MGL.frmSend() { objKH = objkh })
            {
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

                    var strStatusNK = "," + (chkStatusCall.EditValue ?? "").ToString().Replace(" ", "") + ",";
                    var strMaNKH = "," + (chkNhomKH.EditValue ?? "").ToString().Replace(" ", "") + ",";

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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                        DienThoai4 = Common.Right(p.DiDong4, 3),
                                        p.CountSerch,
                                        p.TotalRows
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                        DienThoai4 = Common.Right1(p.DiDong4, 3),
                                        p.CountSerch,
                                        p.TotalRows
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                    p.DiDong4,
                                    p.CountSerch,
                                    p.TotalRows
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                        DienThoai = Common.Right(p.DienThoai, 3),
                                        p.CountSerch,
                                        p.TotalRows
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                DienThoai = Common.Right1(p.DienThoai, 3),
                                p.CountSerch,
                                p.TotalRows
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                    p.CountSerch,
                                    p.TotalRows
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                        DienThoai = Common.Right(p.DienThoai, 3),
                                        p.CountSerch,
                                        p.TotalRows
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                        DienThoai = Common.Right1(p.DienThoai, 3),
                                        p.CountSerch,
                                        p.TotalRows
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                        p.CountSerch,
                                        p.TotalRows
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                        DienThoai = Common.Right(p.DienThoai, 3),
                                        p.CountSerch,
                                        p.TotalRows
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
                                (DateTime?)dateNgayNhapTo.EditValue, cmbToaDo.Text, cboVideo.Text, strMaNKH, strStatusNK, cboDateCall.Text, (DateTime?)dateFromCall.EditValue, (DateTime?)dateToCall.EditValue
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
            popupControlContainer1.Visible = false;


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
            cboDateCall.SelectedIndex = 0;
            dateFromCall.EditValue = null;
            dateToCall.EditValue = null;

            chkStatusCall.SetEditValue("");
            chkStatusCall.EditValue = null;
            chkNhomKH.EditValue = null;
            chkNhomKH.SetEditValue("");
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
            var startDate = db;
            var frC = new BEE.VOIPSETUP.Call.frmCall();
            frC.SDT = sdt;
            frC.isNLH = isNLH;
            frC.sdtHiden = sdthidden;
            frC.MaKH = MaKH;
            frC.MaBC = (int?)grvBan.GetFocusedRowCellValue("MaBC");
            // frC.ModuleID = 0;
            frC.line = objLine.LineNumber;
            frC.NhanVienTN = Common.StaffName;
            frC.LoaiCG = 0; // 0 gọi ra
            frC.StartDate = (DateTime)db.getDate();

            frC.Show();
        }

        private void btnCall_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var makh = (int?)grvBan.GetFocusedRowCellValue("MaKH");
            var namecol = grvBan.FocusedColumn.Name;
            string sdt = "";
            string sdthidden = "";
            var objkh = db.KhachHangs.SingleOrDefault(p => p.MaKH == makh);
            objkh.ExEndCall = db.voipLineConfigs.SingleOrDefault(p => p.MaNV == Common.StaffID)?.LineNumber;
            db.SubmitChanges();
            switch (namecol)
            {
                case "colDienThoai1":
                    sdt = objkh.DiDong;
                    sdthidden = grvBan.GetFocusedRowCellValue("DienThoai")?.ToString();
                    break;
                case "colDienThoai2":
                    sdt = objkh.DiDong2;
                    sdthidden = grvBan.GetFocusedRowCellValue("DiDong2")?.ToString();
                    break;
                case "colDienThoai3":
                    sdt = objkh.DiDong3;
                    sdthidden = grvBan.GetFocusedRowCellValue("DiDong3")?.ToString();
                    break;
                case "colDienThoai4":
                    sdt = objkh.DiDong4;
                    sdthidden = grvBan.GetFocusedRowCellValue("DiDong4")?.ToString();
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
        public void clickChaoKhach(int? maloai)
        {
            switch (maloai)
            {
                case 1: // gửi email

                    break;
                case 2: // gọi điện
                    break;
                case 3: // gửi tin nhắn
                    break;
                case 4: // khác
                    break;
            }

        }

        List<int> lstChon = new List<int>();
        List<int> lstChonDX = new List<int>();
        List<int> lstChonDP = new List<int>();
        List<int> lstChonDC = new List<int>();
        List<int> lstChonTP = new List<int>();
        List<int> lstChonDatCoc = new List<int>();

        private void itemCKHSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvMuaThue.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Infomation("Bạn vui lòng chọn khách hàng để gửi Email!");
                return;
            }

            //var objkh = db.KhachHangs.SingleOrDefault(p => p.MaKH == (int?)grvMuaThue.GetFocusedRowCellValue("MaKH"));

            var objMT = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == (int)grvMuaThue.GetFocusedRowCellValue("MaMT"));
            //using (var frm = new MGL.frmSend() { objKH = objkh })
            //{
            //    frm.ShowDialog();
            //}

            var objKH = db.KhachHangs.SingleOrDefault(p => p.MaKH == (int?)grvMuaThue.GetFocusedRowCellValue("MaKH"));
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
            int j = 0;

            //var indexs = grvBDS.GetSelectedRows();

            // lấy thông tin khách hàng cần gửi
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

            var objBC = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == (int)grvBan.GetFocusedRowCellValue("MaBC"));
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
                if (!string.IsNullOrEmpty(mailcf.Logo2))
                    noidung += "<img src='" + mailcf.Logo2 + "'/>";
            }
            catch
            {

            }
            using (var frm = new BEE.HoatDong.MGL.frmSend() { objKH = objKH })
            {
                frm.noidung = noidung;
                frm.ShowDialog();
            }

            // lưu dữ liệu
            try
            {
                var objmgbccv = new mglMTCongViec();
                objmgbccv.MaBC = (int?)grvBan.GetFocusedRowCellValue("MaBC");
                objmgbccv.MaNV = Common.StaffID;
                objmgbccv.NgayNhap = DateTime.Now;
                objmgbccv.DienGiai = "Gửi Mail";
                objmgbccv.MaTT = 1;

                db.mglMTCongViecs.InsertOnSubmit(objmgbccv);
                db.SubmitChanges();
                var itemsave = new mglMTSanPham();
                var id = objMT.MaMT;
                itemsave.MaMT = id;
                itemsave.MaCV = objmgbccv.ID;
                itemsave.MaTT = 1;
                itemsave.MaHT = 1; // gửi mail
                itemsave.MaLoai = 1; // chào khách hàng
                db.mglMTSanPhams.InsertOnSubmit(itemsave);
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

        private void itemCKHGoiDien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                var makh = (int?)grvMuaThue.GetFocusedRowCellValue("MaKH");
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
            try
            {
                var objmgbccv = new mglMTCongViec();
                objmgbccv.MaBC = (int?)grvBan.GetFocusedRowCellValue("MaBC");
                objmgbccv.MaNV = Common.StaffID;
                objmgbccv.NgayNhap = DateTime.Now;
                objmgbccv.DienGiai = "Gọi điện";
                objmgbccv.MaTT = 1;
                db.mglMTCongViecs.InsertOnSubmit(objmgbccv);
                db.SubmitChanges();
                var itemsave = new mglMTSanPham();
                var id = (int?)grvMuaThue.GetFocusedRowCellValue("MaMT");
                itemsave.MaMT = id;
                itemsave.MaCV = objmgbccv.ID;
                itemsave.MaTT = 1;
                itemsave.MaHT = 3; // goi dien
                itemsave.MaLoai = 1; // chào khách hàng
                db.mglMTSanPhams.InsertOnSubmit(itemsave);
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

        private void itemCKHGuiSMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvMuaThue.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Infomation("Bạn vui lòng chọn khách hàng để gửi Email!");
                return;
            }

            //var objkh = db.KhachHangs.SingleOrDefault(p => p.MaKH == (int?)grvMuaThue.GetFocusedRowCellValue("MaKH"));

            var objMT = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == (int)grvMuaThue.GetFocusedRowCellValue("MaMT"));
            //using (var frm = new MGL.frmSend() { objKH = objkh })
            //{
            //    frm.ShowDialog();
            //}

            var objKH = db.KhachHangs.SingleOrDefault(p => p.MaKH == (int?)grvMuaThue.GetFocusedRowCellValue("MaKH"));
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
            int j = 0;

            //var indexs = grvBDS.GetSelectedRows();

            // lấy thông tin khách hàng cần gửi
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

            var objBC = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == (int)grvBan.GetFocusedRowCellValue("MaBC"));
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
                if (!string.IsNullOrEmpty(mailcf.Logo2))
                    noidung += "<img src='" + mailcf.Logo2 + "'/>";
            }
            catch
            {

            }
            using (var frm = new BEE.HoatDong.MGL.frmSendSMS() { objKH = objKH })
            {
                frm.noidung = noidung;
                frm.ShowDialog();
            }

            // lưu dữ liệu
            try
            {
                var objmgbccv = new mglMTCongViec();
                objmgbccv.MaBC = (int?)grvBan.GetFocusedRowCellValue("MaBC");
                objmgbccv.MaNV = Common.StaffID;
                objmgbccv.NgayNhap = DateTime.Now;
                objmgbccv.DienGiai = "Gửi SMS";
                objmgbccv.MaTT = 1;

                db.mglMTCongViecs.InsertOnSubmit(objmgbccv);
                db.SubmitChanges();
                var itemsave = new mglMTSanPham();
                var id = objMT.MaMT;
                itemsave.MaMT = id;
                itemsave.MaCV = objmgbccv.ID;
                itemsave.MaTT = 1;
                itemsave.MaHT = 2; // gửi mail
                itemsave.MaLoai = 1; // chào khách hàng
                db.mglMTSanPhams.InsertOnSubmit(itemsave);
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

        private void itmCKHKhac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                var objmgbccv = new mglMTCongViec();
                objmgbccv.MaBC = (int?)grvBan.GetFocusedRowCellValue("MaBC");
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
                db.mglMTSanPhams.InsertOnSubmit(itemsave);

                db.SubmitChanges();
                var itemother = new mglLichSuOtherMT();
                itemother.MaMT = (int)grvMuaThue.GetFocusedRowCellValue("MaMT");
                itemother.MaNV = Common.StaffID;
                itemother.MaCV = objmgbccv.ID;
                itemother.NgayNhap = DateTime.Now;
                itemother.NoiDung = frm.NoiDung;
                db.mglLichSuOtherMTs.InsertOnSubmit(itemother);
                db.SubmitChanges();
            }
        }

        private void itemDaXem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // chuyển từ mặt bằng đã chào qua đã xem
            var indexs = grvDaChao.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (DialogBox.Question("Bạn có muốn chuyển thông tin không") == DialogResult.No) return;

            var id = (int?)grvDaChao.GetFocusedRowCellValue("ID");
            var maMT = (int?)grvDaChao.GetFocusedRowCellValue("MaMT");
            var maLoai = Convert.ToByte(grvDaChao.GetFocusedRowCellValue("MaLoai"));
            var maLoaiSau = maLoai == 10 ? (byte)10 : (byte)(maLoai + 1);

            ChuyenTrangThai(maLoai, maLoaiSau, id, maMT);
        }

        private void itemChuyenDaGapDamPhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // chuyển từ mặt bằng đã chào qua đã xem
            var indexs = grvDaXem.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (DialogBox.Question("Bạn có muốn chuyển thông tin không") == DialogResult.No) return;
            var id = (int?)grvDaXem.GetFocusedRowCellValue("ID");
            var maMT = (int?)grvDaXem.GetFocusedRowCellValue("MaMT");

            ChuyenTrangThai(2, 3, id, maMT);
        }

        private void itmChuyenDaDatCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvDaGapDP.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }
            if (DialogBox.Question("Bạn có muốn chuyển thông tin không") == DialogResult.No) return;
            var id = (int?)grvDaGapDP.GetFocusedRowCellValue("ID");
            var maMT = (int?)grvDaGapDP.GetFocusedRowCellValue("MaMT");

            ChuyenTrangThai(3, 4, id, maMT);
        }

        private void itemChuyenChoThuPhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // chuyển từ mặt bằng đã chào qua đã xem
            var indexs = grvDacCoc.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (DialogBox.Question("Bạn có muốn chuyển thông tin không") == DialogResult.No) return;
            var id = (int?)grvDacCoc.GetFocusedRowCellValue("ID");
            var maMT = (int?)grvDacCoc.GetFocusedRowCellValue("MaMT");

            ChuyenTrangThai(4, 5, id, maMT);
        }

        private void itemCKHSendMailCH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvBan.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Infomation("Bạn vui lòng chọn nhu cầu để gửi Email!");
                return;
            }

            var arrMaBC = new List<int>();
            var arrMaMT = new List<int>();
            foreach (var i in indexs)
            {
                var objBC = (int)grvBan.GetRowCellValue(i, "MaBC");
                arrMaBC.Add(objBC);

                // var maMT = (int)grvBan.GetRowCellValue(i, "MaMT");
                //  arrMaMT.Add(maMT);
            }


            using (var frm = new MGL.Mua.frmMatch())
            {
                frm.arrMaBC = arrMaBC;
                frm.ShowDialog();
            }
        }

        private void itemHTChaoHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvBan.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Infomation("Bạn vui lòng chọn nhu cầu để gửi Email!");
                return;
            }

            var arrMaBC = new List<int>();
            var arrMaMT = new List<int>();
            foreach (var i in indexs)
            {
                var objBC = (int)grvBan.GetRowCellValue(i, "MaBC");
                arrMaBC.Add(objBC);
            }


            using (var frm = new MGL.Mua.frmMatch())
            {
                frm.arrMaBC = arrMaBC;
                frm.ShowDialog();
            }
        }

        public List<ItemLichSuGhiNhan> LichSuGhiNhan(int mabc, int MaNV, int MaNKD, int MaPB, int MaNVXem, bool isPer)
        {
            return db.mglLichSuGhiNhan(mabc, MaNV, MaNKD, MaPB, MaNVXem, isPer)
                                .Select(p => new ItemLichSuGhiNhan
                                {
                                    STT = p.STT,
                                    MauNen = p.MauNen,
                                    TenTT = p.TenTT,
                                    VideoLink = (p.VideoLink == null || p.VideoLink == "") ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    MaTT = p.MaTT,
                                    AnhBDS = p.Anh == "Không có ảnh" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources._58_image,
                                    VideoBDS = p.Video == "Không có video" ? BEE.HoatDong.Properties.Resources.white : BEE.HoatDong.Properties.Resources.youtube,
                                    SoGiay = p.SoGiay,
                                    ThoiHan = p.ThoiHan,
                                    KyHieu = p.KyHieu,
                                    NamXayDung = p.NamXayDung,
                                    TrangThaiHDMG = p.TrangThaiHDMG,
                                    TenTrangThaiHDMG = p.TenTrangThaiHDMG,
                                    HoTenNDD = p.HoTenNDD,
                                    HoTenNTG = p.HoTenNTG,
                                    HuongBanCong = p.HuongBanCong,
                                    TenHuongBC = p.TenHuongBC,
                                    TenPL = p.TenPL,
                                    TenTinhTrangSP = p.TenTinhTrangSP,
                                    TenLD = p.TenLD,
                                    NgangKV = p.NgangKV,
                                    DaiKV = p.DaiKV,
                                    SauKV = p.SauKV,
                                    DauOto = p.DauOto,
                                    TangHam = p.TangHam,
                                    IsBan = p.IsBan,
                                    TenNC = p.TenNC,
                                    TenHuyen = p.TenHuyen,
                                    TenTinh = p.TenTinh,
                                    TenXa = p.TenXa,
                                    HoTenKH = p.HoTenKH,
                                    DiaChi = p.DiaChi,
                                    SoNhaaa = Common.SoNhaNEW((p.DiaChi)),
                                    SoNha = p.SoNha,
                                    TenLBDS = p.TenLBDS,
                                    PhongVS = p.PhongVS,
                                    DienTichDat = p.DienTichDat,
                                    DienTichXD = p.DienTichXD,
                                    TenDuong = p.TenDuong,
                                    SoTangXD = p.SoTangXD,
                                    ThoiGianHD = p.ThoiGianHD,
                                    ThoiGianBGMB = p.ThoiGianBGMB,
                                    GhiChu = p.GhiChu,
                                    GioiThieu = p.GioiThieu,
                                    DonViDangThue = p.DonViDangThue,
                                    DonViThueCu = p.DonViThueCu,
                                    IsCanGoc = p.IsCanGoc,
                                    IsThangMay = p.IsThangMay,
                                    TienIch = p.TienIch,
                                    DacTrung = p.DacTrung,
                                    KinhDo = p.KinhDo,
                                    ViDo = p.ViDo,
                                    ToaDo = p.ToaDo,
                                    TenLoaiTien = p.TenLoaiTien,
                                    NgangXD = p.MatTien,
                                    DaiXD = p.DaiXD,
                                    DienTich = p.DienTich,
                                    DonGia = p.DonGia,
                                    ThanhTien = p.ThanhTien,
                                    ThuongLuong = p.ThuongLuong,
                                    PhongKhach = p.PhongKhach,
                                    PhongNgu = p.PhongNgu,
                                    PhongTam = p.PhongTam,
                                    SoTang = p.SoTang,
                                    MaKH = p.MaKH,
                                    PhiMoiGioi = p.PhiMoiGioi,
                                    TyLeMG = p.TyLeMG,
                                    TyLeHH = p.TyLeHH,
                                    ChinhChu = p.ChinhChu,
                                    DuongRong = p.DuongRong,
                                    MaNVKD = p.MaNVKD,
                                    MaNVQL = p.MaNVQL,
                                    HoTenNVMG = p.HoTenNVMG,
                                    HoTenNV = p.HoTenNV,
                                    NgayCN = p.NgayCN,
                                    NgayDK = p.NgayDK,
                                    SoDK = p.SoDK,
                                    DuAn = p.DuAn,
                                    DienThoai = p.DienThoai,
                                    DiDong2 = p.DiDong2,
                                    DiDong3 = p.DiDong3,
                                    DiDong4 = p.DiDong4,
                                    HoTen = p.HoTen,
                                    HoTenNVS = p.HoTenNVS,
                                    NgaySua = p.NgaySua
                                }).ToList();
        }

        int GetAccessDataHistory()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 215);

            return o.SDB.SDBID;
        }

        private void itemDaQuanTam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvDaQuanTam.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (DialogBox.Question("Bạn có muốn chuyển thông tin không") == DialogResult.No) return;
            var id = (int?)grvDaQuanTam.GetFocusedRowCellValue("ID");
            var maMT = (int?)grvDaQuanTam.GetFocusedRowCellValue("MaMT");

            ChuyenTrangThai(6, 2, id, maMT);
        }

        private void itemGapKH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvDaDuaKHGapNhau.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (DialogBox.Question("Bạn có muốn chuyển thông tin không") == DialogResult.No) return;
            var id = (int?)grvDaDuaKHGapNhau.GetFocusedRowCellValue("ID");
            var maMT = (int?)grvDaDuaKHGapNhau.GetFocusedRowCellValue("MaMT");

            ChuyenTrangThai(7, 8, id, maMT);
        }

        private void itemDangSuaHD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvDangSuaHD.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (DialogBox.Question("Bạn có muốn chuyển thông tin không") == DialogResult.No) return;
            var id = (int?)grvDangSuaHD.GetFocusedRowCellValue("ID");
            var maMT = (int?)grvDangSuaHD.GetFocusedRowCellValue("MaMT");

            ChuyenTrangThai(8, 9, id, maMT);
        }

        private void itemDaKyHD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvDaKyHd.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (DialogBox.Question("Bạn có muốn chuyển thông tin không") == DialogResult.No) return;
            var id = (int?)grvDaKyHd.GetFocusedRowCellValue("ID");
            var maMT = (int?)grvDaKyHd.GetFocusedRowCellValue("MaMT");

            ChuyenTrangThai(9, 4, id, maMT);
        }

        private void itemDaThuPhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvDaThuPhi.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (DialogBox.Question("Bạn có muốn chuyển thông tin không") == DialogResult.No) return;
            var id = (int?)grvDaThuPhi.GetFocusedRowCellValue("ID");
            var maMT = (int?)grvDaThuPhi.GetFocusedRowCellValue("MaMT");

            ChuyenTrangThai(10, 10, id, maMT);
        }

        private void itemChoThuPhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvChoThuPhi.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (DialogBox.Question("Bạn có muốn chuyển thông tin không") == DialogResult.No) return;
            var id = (int?)grvChoThuPhi.GetFocusedRowCellValue("ID");
            var maMT = (int?)grvChoThuPhi.GetFocusedRowCellValue("MaMT");

            ChuyenTrangThai(5, 10, id, maMT);
        }
        private void ChuyenTrangThai(byte pMaLoaiTruoc = 0, byte pMaLoai = 0, int? Id = 0, int? maMt = 0)
        {
            var frm = new frmChuyenDoiMT();
            frm.MaLoaiTruoc = pMaLoaiTruoc;
            frm.MaLoai = pMaLoai;
            frm.ShowDialog();
            if (frm.isSave == true)
            {
                var obj = db.mglMTSanPhams.SingleOrDefault(p => p.ID == Id);
                if (pMaLoaiTruoc == 1)
                {
                    obj.StartDate = DateTime.Now;
                }
                obj.UpdateDate = DateTime.Now;
                obj.MaLoai = frm.MaLoai;

                var objBC = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == (int)grvBan.GetFocusedRowCellValue("MaBC"));
                if (objBC != null)
                {

                    if (frm.MaLoai == 10 && (objBC.PhiMG - (obj.PhiMGDTBan == null ? 0 : obj.PhiMGDTBan)) > 0)
                    {
                        DialogBox.Warning("Bạn không thể chuyển trạng thái [Đã thu phí] khi chưa thu đủ phí");
                        return;
                    }
                }

                var objls = new mglLichSuXuLyGiaoDich_MT();
                objls.MaNV = Common.StaffID;
                objls.NoiDung = frm.NoiDung;
                objls.MaMT = maMt;
                objls.MaLoai = frm.MaLoai;
                objls.MaMTCV = Id;
                objls.Title = frm.TieuDe;
                objls.MaLoaiTruoc = frm.MaLoaiTruoc;
                objls.NgayXL = frm.NgayXL;
                objls.MaPT = frm.MaPT;
                db.mglLichSuXuLyGiaoDich_MTs.InsertOnSubmit(objls);

                db.SubmitChanges();
                DialogBox.Infomation("Thao thác thành công");
            }
        }

        private void itemGallery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBan.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu đăng ký");
                return;
            }
            var id = (int)grvBan.GetFocusedRowCellValue("MaBC");
            //var objcount = db.mglbcAnhbds.Where(p => p.MaBC == id).ToList();
            //if(objcount.Count <= 0)
            //{
            //    DialogBox.Error("Bất động sản không tồn tại hình ảnh, vui lòng kiểm tra lại");
            //    return;
            //}
            try
            {
                var frm = new PhotoViewer.Form2();
                frm.MaBC = id;
                frm.ShowDialog();
                frm.Dispose();
                Thread thrd = new Thread(Delete);
                thrd.IsBackground = true;
                thrd.Start();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);

            }


        }
        public static void Delete()
        {


            System.IO.DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\Images");
            foreach (FileInfo file in di.GetFiles())
            {
                try
                {

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    file.Delete();
                }
                catch (Exception ex)
                {

                }


            }

        }

        public class itemRecordIncall
        {
            public string type { get; set; }
            public DateTime? timespan { get; set; }
            public string phone { get; set; }
            public string line { get; set; }
            public string Source { get; set; }

        }
        public class itemRecordOut
        {
            public string type { get; set; }
            public DateTime? timespan { get; set; }
            public string line { get; set; }
            public string phone { get; set; }
            public string Source { get; set; }

        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        private void btnGhiAm_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            var id = (int?)grvNhatKy.GetFocusedRowCellValue("ID");

            try
            {
                var FileRecord = (sender as ButtonEdit).Text ?? "";
                if (FileRecord.Trim() == "") // sẽ đồng bộ dữ liệu
                {



                    var objn = (from nk in db.mglbcNhatKyXuLies
                                join bc in db.mglbcBanChoThues on nk.MaBC equals bc.MaBC
                                join kh in db.KhachHangs on bc.MaKH equals kh.MaKH
                                join nv in db.NhanViens on nk.MaNVG equals nv.MaNV
                                join line in db.voipLineConfigs on nv.MaNV equals line.MaNV
                                where nk.ID == id
                                select new
                                {
                                    nk.DiDong,
                                    line.LineNumber,
                                    nk.StartDate,
                                    nk.Enddate,
                                    nk.LoaiCG,
                                    nk.NgayXL

                                }
                                ).FirstOrDefault();

                    if (objn != null)
                    {
                        var objconfig = db.tblConfigs.FirstOrDefault(p => p.TypeID == 3);

                        var datepath = string.Format("{0:yyyy-MM}", objn.NgayXL);
                        string ftppath = objconfig.FtpUrl + datepath;
                        FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftppath);
                        ftpRequest.Credentials = new NetworkCredential(objconfig.FtpUser, it.CommonCls.GiaiMa(objconfig.FtpPass));
                        //ftpRequest.Timeout = Timeout.Infinite;
                        //ftpRequest.KeepAlive = true;
                        ftpRequest.Timeout = 60000;
                        ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                        FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                        StreamReader streamReader = new StreamReader(response.GetResponseStream());
                        List<itemRecordIncall> directoriesIn = new List<itemRecordIncall>();

                        List<itemRecordOut> directoriesOut = new List<itemRecordOut>();
                        string line = streamReader.ReadLine();

                        DateTime timelq = (DateTime)objn.StartDate;
                        while (!string.IsNullOrEmpty(line))
                        {

                            if (line.Contains("6501") == false)
                            {
                                try
                                {
                                    var arrayfile = line.Replace(datepath + "/", "").Split('-');
                                    // kiểm tra định dạnh file


                                    if (arrayfile[3].Replace(".wav", "").Length > 9 && objn.LoaiCG == 0) // số điện thoải phải > 9, cuộc gọi ra
                                    {
                                        DateTime time = UnixTimeStampToDateTime(Convert.ToDouble(arrayfile[1])); // 

                                        TimeSpan tp = (DateTime)objn.StartDate - time;
                                        if (time.Year == timelq.Year && time.Month == timelq.Month && time.Day == timelq.Day) // dung ngay
                                        {
                                            try
                                            {
                                                var itemfile = new itemRecordOut();
                                                itemfile.type = arrayfile[0]; // 
                                                itemfile.timespan = time;
                                                itemfile.line = arrayfile[2]; // 
                                                itemfile.phone = arrayfile[3].Replace(".wav", ""); // 
                                                itemfile.Source = line;
                                                directoriesOut.Add(itemfile);
                                            }
                                            catch (Exception ex)
                                            {

                                            }

                                        }



                                    }

                                    else if (arrayfile[3].Replace(".wav", "").Length <= 4 && objn.LoaiCG == null)  // gọi vào line ở cuối 
                                    {
                                        DateTime time = UnixTimeStampToDateTime(Convert.ToDouble(arrayfile[1])); // 
                                        TimeSpan tp = (DateTime)objn.StartDate - time;
                                        var t = tp.Days;
                                        var tday = (int)tp.TotalDays;
                                        //var a = arrayfile[2];
                                        //if(a == "0919263686")
                                        //{
                                        //    var c = "";
                                        //}

                                        if (time.Year == timelq.Year && time.Month == timelq.Month && time.Day == timelq.Day) // dung ngay
                                        {
                                            try
                                            {
                                                var itemfile = new itemRecordIncall();
                                                itemfile.type = arrayfile[0]; // 
                                                itemfile.timespan = UnixTimeStampToDateTime(Convert.ToDouble(arrayfile[1])); // 
                                                itemfile.phone = arrayfile[2]; // 
                                                itemfile.line = arrayfile[3].Replace(".wav", ""); // 
                                                itemfile.Source = line;
                                                directoriesIn.Add(itemfile);
                                            }
                                            catch (Exception ex)
                                            {

                                            }

                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                            line = streamReader.ReadLine();
                        }
                        streamReader.Close();
                        // kết thúc lặp
                        var objresultRecordout = new List<itemRecordOut>();
                        var objresultRecordIn = new List<itemRecordIncall>();
                        var objnkupdate = db.mglbcNhatKyXuLies.FirstOrDefault(p => p.ID == id);
                        if (objn.LoaiCG == 0)
                        {
                            //    SqlMethods.DateDiffDay((DateTime)itemTuNgay.EditValue, p.NgayGD) >= 0 &
                            //    SqlMethods.DateDiffDay(p.NgayGD, (DateTime)itemDenNgay.EditValue) >= 0)
                            objresultRecordout = directoriesOut.Where(p => p.phone == objn.DiDong && p.line == objn.LineNumber).ToList();
                            objresultRecordout = objresultRecordout.Where(p => SqlMethods.DateDiffMinute(objn.StartDate, p.timespan) >= 0 & SqlMethods.DateDiffMinute(p.timespan, objn.Enddate) >= 0).ToList();
                            if (objresultRecordout.Count > 0)
                            {
                                var path = objresultRecordout.FirstOrDefault().Source;
                                objnkupdate.FileRecord = path;
                                db.SubmitChanges();

                                // open file media

                                var url = path;
                                var frm = new frmViewMedia();
                                frm.filepath = url;
                                frm.ShowDialog();

                            }
                            else
                            {
                                DialogBox.Error("Không tồn tại file ghi âm, vui lòng kiểm tra lại");
                                return;
                            }

                        }
                        else
                        {
                            objresultRecordIn = directoriesIn.Where(p => p.phone == objn.DiDong && p.line == objn.LineNumber).ToList();
                            objresultRecordIn = objresultRecordIn.Where(p => SqlMethods.DateDiffMinute(Convert.ToDateTime(objn.StartDate).AddSeconds(-15), p.timespan) >= 0 & SqlMethods.DateDiffSecond(p.timespan, objn.Enddate) >= 0).ToList();

                            if (objresultRecordIn.Count > 0)
                            {
                                objnkupdate.FileRecord = objresultRecordIn.OrderByDescending(p => p.timespan).FirstOrDefault().Source;
                                db.SubmitChanges();
                            }
                            else
                            {
                                DialogBox.Error("Không tồn tại file ghi âm, vui lòng kiểm tra lại");
                                return;
                            }
                        }
                        // cập nhật vào db xong

                    }// objn


                }

                else
                {
                    // lấy luôn file và open
                    // open file media

                    var frm = new frmViewMedia();
                    frm.filepath = FileRecord;
                    frm.ShowDialog();


                }
            }
            catch (Exception exx)
            {
                DialogBox.Error("lỗi: " + exx.Message);
                return;

            }


        }

        private void itemTaiFileGhiAm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var filepath = grvNhatKy.GetFocusedRowCellValue("FileRecord");
                if (filepath == null || filepath == "")
                {
                    DialogBox.Error("Không tồn tại file ghi âm, vui lòng kiểm tra lại");
                    return;
                }

                var objconfig = db.tblConfigs.FirstOrDefault(p => p.TypeID == 3);
                string ftppath = objconfig.FtpUrl + filepath;
                string user = objconfig.FtpUser;
                string pass = it.CommonCls.GiaiMa(objconfig.FtpPass);

                WebClient client = new WebClient();
                client.Credentials = new NetworkCredential(user, pass);


                SaveFileDialog savefile = new SaveFileDialog();
                savefile.Title = "Lưu file ghi âm";
                savefile.Filter = "Wav Files|*.wav";
                savefile.ShowDialog();
                if (savefile.FileName != "")
                {
                    try
                    {
                        client.DownloadFile(ftppath, savefile.FileName);
                        DialogBox.Infomation("Hoàn thành tải file");
                    }
                    catch (Exception ex)
                    {

                        DialogBox.Error("Lỗi, vui lòng thử lại:" + ex.Message);
                    }

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grvDaChao_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                if (e.Column.FieldName == "TenTTGD")
                {
                    var color = grvDaChao.GetRowCellValue(e.RowHandle, "Color");
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

        private void cboDateCall_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDateCall.SelectedIndex == 1)
            {
                dateToCall.EditValue = string.Empty;
                dateToCall.Visible = true;
            }
            else
            {
                dateToCall.Visible = false;
            }
        }

        private void gcBan_Click(object sender, EventArgs e)
        {

        }

        private void btnAsyncManual_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string dd = "";
            var id = (int?)grvNhatKy.GetFocusedRowCellValue("ID");
            try
            {
                dd = grvNhatKy.GetFocusedRowCellValue("DiDong").ToString();

            }
            catch { }

            if (id == null || id == 0)
            {
                DialogBox.Error("Vui lòng chọn nhật ký xử lý");
                return;
            }
            VOIPSETUP.Record.frmRecordFTP frm = new VOIPSETUP.Record.frmRecordFTP();
            frm.id = id;
            frm.didong = dd ?? "";
            frm.Show();


        }

        private void itemAnhS3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBan.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn bất động sản");
                return;
            }
            var id = (int)grvBan.GetFocusedRowCellValue("MaBC");
            try
            {
                var frm = new PhotoViewer.Form3S3();
                frm.MaBC = id;
                frm.ShowDialog();


            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);

            }
        }
    }
}
#region ItemLichSuGhiNhan
public class ItemLichSuGhiNhan
{
    public int MaBC { get; set; }

    public System.Nullable<long> STT { get; set; }

    public System.Nullable<int> MauNen { get; set; }

    public string TenTT { get; set; }

    public System.Nullable<byte> MaTT { get; set; }

    public System.Nullable<int> SoGiay { get; set; }

    public string ThoiHan { get; set; }

    public string KyHieu { get; set; }

    public System.Nullable<int> NamXayDung { get; set; }

    public System.Nullable<byte> TrangThaiHDMG { get; set; }

    public System.Nullable<byte> MaTinhTrang { get; set; }

    public Bitmap VideoLink { get; set; }

    public string HoTenNDD { get; set; }

    public string HoTenNTG { get; set; }

    public System.Nullable<short> HuongBanCong { get; set; }

    public System.Nullable<decimal> NgangKV { get; set; }

    public System.Nullable<decimal> DaiKV { get; set; }

    public System.Nullable<decimal> SauKV { get; set; }

    public System.Nullable<bool> DauOto { get; set; }

    public System.Nullable<bool> TangHam { get; set; }

    public string DienThoai { get; set; }

    public string DiDong2 { get; set; }

    public string DiDong3 { get; set; }

    public string DiDong4 { get; set; }

    public System.Nullable<bool> IsBan { get; set; }

    public string TenNC { get; set; }

    public System.Nullable<short> MaLBDS { get; set; }

    public string TenHuyen { get; set; }

    public string TenTinh { get; set; }

    public string TenXa { get; set; }

    public string HoTenKH { get; set; }

    public System.Nullable<byte> PhongVS { get; set; }

    public System.Nullable<decimal> DienTichDat { get; set; }

    public System.Nullable<decimal> DienTichXD { get; set; }

    public System.Nullable<int> SoTangXD { get; set; }

    public System.Nullable<System.DateTime> ThoiGianHD { get; set; }

    public System.Nullable<System.DateTime> ThoiGianBGMB { get; set; }

    public string GhiChu { get; set; }

    public string GioiThieu { get; set; }

    public string DonViThueCu { get; set; }

    public string DonViDangThue { get; set; }

    public System.Nullable<bool> IsCanGoc { get; set; }

    public System.Nullable<bool> IsThangMay { get; set; }

    public string TienIch { get; set; }

    public string KinhDo { get; set; }

    public string ViDo { get; set; }

    public string TenLoaiTien { get; set; }

    public System.Nullable<decimal> NgangXD { get; set; }

    public System.Nullable<decimal> DaiXD { get; set; }

    public System.Nullable<decimal> DonGia { get; set; }

    public System.Nullable<decimal> ThanhTien { get; set; }

    public System.Nullable<bool> ThuongLuong { get; set; }

    public System.Nullable<short> MaHuyen { get; set; }

    public System.Nullable<byte> PhongKhach { get; set; }

    public System.Nullable<byte> PhongNgu { get; set; }

    public System.Nullable<byte> PhongTam { get; set; }

    public System.Nullable<byte> SoTang { get; set; }

    public System.Nullable<short> MaHuong { get; set; }

    public System.Nullable<int> MaKH { get; set; }

    public System.Nullable<decimal> PhiMoiGioi { get; set; }

    public System.Nullable<decimal> TyLeMG { get; set; }

    public System.Nullable<decimal> TyLeHH { get; set; }

    public System.Nullable<bool> ChinhChu { get; set; }

    public System.Nullable<byte> MaTinh { get; set; }

    public string HoTenNVN { get; set; }

    public System.Nullable<short> MaLD { get; set; }

    public System.Nullable<decimal> DuongRong { get; set; }

    public System.Nullable<short> MaPL { get; set; }

    public System.Nullable<int> MaNVKD { get; set; }

    public System.Nullable<int> MaNVQL { get; set; }

    public string HoTenNVMG { get; set; }

    public string HoTenNV { get; set; }

    public System.Nullable<short> MaNguon { get; set; }

    public System.Nullable<short> MaCD { get; set; }

    public System.Nullable<System.DateTime> NgayCN { get; set; }

    public System.Nullable<System.DateTime> NgayNhap { get; set; }

    public System.Nullable<System.DateTime> NgayDK { get; set; }

    public string SoDK { get; set; }

    public string DuAn { get; set; }

    public string TenLBDS { get; set; }

    public string HuongCua { get; set; }

    public string TenHuongBC { get; set; }

    public string TenLD { get; set; }

    public string TenPL { get; set; }

    public string TenNguon { get; set; }

    public string TenTrangThaiHDMG { get; set; }

    public string TenTinhTrangSP { get; set; }

    public Bitmap AnhBDS { get; set; }

    public string TrangThaiToaDo { get; set; }

    public string Video { get; set; }

    public int ID { get; set; }

    public string TenDuong { get; set; }

    public string SoNha { get; set; }

    public System.Nullable<decimal> MatTien { get; set; }

    public System.Nullable<decimal> DienTich { get; set; }

    public System.Nullable<decimal> GiaTien { get; set; }

    public string DacTrung { get; set; }

    public System.Nullable<System.DateTime> ThoiHanHD { get; set; }

    public string TenKH { get; set; }

    public string SoDienThoai { get; set; }

    public string Email { get; set; }

    public string DiaChi { get; set; }

    public string ToaDo { get; set; }

    public string CanBoLV { get; set; }

    public string HoTen { get; set; }

    public System.Nullable<System.DateTime> NgaySua { get; set; }
    public string SoNhaaa { get; set; }
    public Bitmap VideoBDS { get; set; }
    public string HoTenNVS { get; set; }

}
#endregion
