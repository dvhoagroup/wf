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

namespace BEE.HoatDong.MGL.Mua.GiaoDich
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();

        public ctlManager()
        {
            InitializeComponent();
            lookTrangThaiNK.DataSource = lookTrangThai.DataSource = db.mglgdTrangThais;

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 180;
            DataTable tblAction = o.SelectBy();
            // itemSua.Enabled = false;
            //  itemXoa.Enabled = false;
            itemDuyet.Enabled = false;
            itemKhongDuyet.Enabled = false;
            itemDaThuTien.Enabled = false;
            itemChuaThuTien.Enabled = false;
            itemTHuChuaDu.Enabled = false;
            itemGiaoDichThanhCong.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 2:
                            //  itemSua.Enabled = true;
                            break;
                        case 3:
                            //   itemXoa.Enabled = true;
                            break;
                        case 7://duyet thong tin
                            itemDuyet.Enabled = true;
                            itemKhongDuyet.Enabled = true;
                            break;
                        case 8://Xác nhận thu tiền
                            itemTHuChuaDu.Enabled = true;
                            itemChuaThuTien.Enabled = true;
                            break;
                        case 44:// xác nhận hoàn thành
                            itemGiaoDichThanhCong.Enabled = true;
                            break;
                    }
                }
            }
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 180);

            return o.SDB.SDBID;
        }

        void GiaoDich_Load()
        {

            var tuNgay = (DateTime?)itemTuNgay.EditValue ?? DateTime.Now;
            var denNgay = (DateTime?)itemDenNgay.EditValue ?? DateTime.Now;
            int MaNV = Common.StaffID;
            if (itemTuNgay.EditValue == null || itemDenNgay.EditValue == null)
            {
                itemDaKyHD.DataSource = null;
            }

            db = new MasterDataContext();
            itemDaKyHD.DataSource = (from gd in db.GiaoDichjs
                                     join mt in db.mglmtMuaThues on gd.MaMT equals mt.MaMT into mthue
                                     from mt in mthue.DefaultIfEmpty()
                                     join kh in db.KhachHangs on gd.MaKH equals kh.MaKH
                                     join nv in db.NhanViens on gd.MaNV equals nv.MaNV
                                     join p in db.mglbcBanChoThues on gd.MaBC equals p.MaBC into bt
                                     from p in bt.DefaultIfEmpty()
                                     join x in db.Xas on p.MaXa equals x.MaXa into xa
                                     from x in xa.DefaultIfEmpty()
                                     join h in db.Huyens on p.MaHuyen equals h.MaHuyen into huyen
                                     from h in huyen.DefaultIfEmpty()
                                     where SqlMethods.DateDiffDay(tuNgay, gd.NgayGD) >= 0 & SqlMethods.DateDiffDay(gd.NgayGD, denNgay) >= 0
                                     select new
                                     {
                                         gd.ID,
                                         gd.MaKH,
                                         Duyet = gd.Duyet == true ? "Đã duyệt" : "Chưa duyệt",
                                         gd.MaMT,
                                         gd.MaNV,
                                         gd.NgayGD,
                                         gd.SoPhieu,
                                         gd.SoTien,
                                         //  gd.TienCongTy,
                                         gd.HHNhanVien,
                                         gd.TienPhucLoi,
                                         //DaThuMG = db.getDaThuMG(gd.ID),
                                         DaChi =0,// db.getDaChiHoaHong(gd.ID),
                                         // ConLaiMG = gd.SoTien - db.getDaThuMG(gd.ID),
                                         PhaiChi = gd.mglgdHHNhanViens.Sum(t => t.SoTien) - 0,//db.getDaChiHoaHong(gd.ID),
                                         gd.NVKD,
                                         gd.NVVP,
                                         HoTenNV = nv.HoTen,
                                         gd.Nguon,
                                         gd.GhiChu,
                                         TienDV = 0,//db.getTienDV(gd.ID),
                                         HoaHong = gd.mglgdHHNhanViens.Sum(t => t.SoTien),
                                         TienCongTy = gd.SoTien - gd.mglgdHHNhanViens.Sum(t => t.SoTien),
                                         TenKH = kh.HoKH + " " + kh.TenKH,
                                         mt.SoDK,
                                         DiaChi = kh.NguyenQuan,// p.SoNha + ", " + p.TenDuong+", "+x.TenXa+", "+h.TenHuyen,
                                         p.SoNha,
                                         p.TenDuong,
                                         p.MaXa,
                                         p.MaHuyen,
                                     }).Distinct().AsEnumerable().Select((p, index) => new
                                         {
                                             STT = 1 + index,
                                             p.MaKH,
                                             p.MaMT,
                                             p.ID,
                                             p.HoTenNV,
                                             p.GhiChu,
                                             p.MaNV,
                                             p.NgayGD,
                                             p.TienDV,
                                             p.HoaHong,
                                             p.SoDK,
                                             p.Duyet,
                                             p.DaChi,
                                             p.PhaiChi,
                                             p.SoPhieu,
                                             p.HHNhanVien,
                                             p.TienCongTy,
                                             p.TienPhucLoi,
                                             p.NVKD,
                                             //p.DaThuMG,
                                             // p.ConLaiMG,
                                             p.SoTien,
                                             p.NVVP,
                                             p.Nguon,
                                             p.TenKH,
                                             p.DiaChi,
                                             p.SoNha,
                                             p.TenDuong,
                                             p.MaXa,
                                             p.MaHuyen,
                                         })
                                       .ToList();
            // switch (GetAccessData())
            //{
            //    case 1://Tat ca
            //        itemDaKyHD.DataSource = db.mglgdGiaoDich_GetInfo(tuNgay, denNgay, -1, -1, -1, MaNV, true);
            //        break;
            //    case 2://Theo phong ban 
            //        itemDaKyHD.DataSource = db.mglgdGiaoDich_GetInfo(tuNgay, denNgay, -1, -1, Common.DepartmentID, MaNV, false);
            //        break;
            //    case 3://Theo nhom
            //        itemDaKyHD.DataSource = db.mglgdGiaoDich_GetInfo(tuNgay, denNgay, -1, Common.GroupID, -1, MaNV, false);
            //        break;
            //    case 4://Theo nhan vienbreak;
            //        itemDaKyHD.DataSource = db.mglgdGiaoDich_GetInfo(tuNgay, denNgay, MaNV, -1, -1, MaNV, false);
            //        break;
            //    default:
            //        itemDaKyHD.DataSource = null;
            //        break;
            //}
            //gcGiaoDich.DataSource = db.mglgdGiaoDiches.Where(p =>
            //    SqlMethods.DateDiffDay((DateTime)itemTuNgay.EditValue, p.NgayGD) >= 0 &
            //    SqlMethods.DateDiffDay(p.NgayGD, (DateTime)itemDenNgay.EditValue) >= 0)
            //    .OrderBy(p => p.mglgdTrangThai.STT)
            //    .OrderByDescending(p => p.NgayGD)
            //    .AsEnumerable()
            //    .Select((p, index) => new
            //    {
            //        p.MaGD,
            //        STT = index + 1,
            //        p.SoGD,
            //        p.NgayGD,
            //        p.ThoiHan,
            //        p.TienCoc,
            //        p.MaTT,
            //        p.mglgdTrangThai.MauNen,
            //        p.mglbcBanChoThue.KyHieu,
            //        TenHT = p.mglbcBanChoThue.IsBan.Value ? "Bán" : "Cho thuê",
            //        p.mglbcBanChoThue.LoaiBD.TenLBDS,
            //        p.mglbcBanChoThue.DienTich,
            //        p.mglbcBanChoThue.DonGia,
            //        p.mglbcBanChoThue.ThanhTien,
            //        PhiMG = string.Format("{0:0.##}/{1:0.##}", p.TienMGBenBan, p.TienMGBenMua),
            //        HoTenKH1 = p.mglbcBanChoThue.MaNVKD == LandSoft.Library.Common.StaffID ?
            //            p.mglbcBanChoThue.KhachHang.HoKH + " " + p.mglbcBanChoThue.KhachHang.TenKH :
            //            "(nv) " + p.mglbcBanChoThue.NhanVien.HoTen,
            //        HoTenKH2 = p.mglmtMuaThue.MaNVKD == LandSoft.Library.Common.StaffID ?
            //                                p.mglmtMuaThue.KhachHang.HoKH + " " + p.mglmtMuaThue.KhachHang.TenKH :
            //                                "(nv) " + p.mglmtMuaThue.NhanVien.HoTen,
            //        HoTenNV1 = p.NhanVien.HoTen,
            //        HoTenNV2 = p.NhanVien1.HoTen,
            //        HoTenNV = p.NhanVien2.HoTen
            //    }).ToList();
        }

        private void GiaoDich_Edit()
        {
            var maTT = (int)grvGiaoDich.GetFocusedRowCellValue("ID");
            frmEdit frm = new frmEdit();
            frm.MaGD = (int)grvGiaoDich.GetFocusedRowCellValue("ID");

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                GiaoDich_Load();
            }
        }

        private void GiaoDich_Delete()
        {
            var indexs = grvGiaoDich.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu giao dịch");
                return;
            }
            if (DialogBox.Question() == DialogResult.No) return;
            foreach (var i in indexs)
            {
                GiaoDichj objGD = db.GiaoDichjs.Single(p => p.ID == (int)grvGiaoDich.GetRowCellValue(i, "ID"));
                db.GiaoDichjs.DeleteOnSubmit(objGD);
            }
            db.SubmitChanges();

            grvGiaoDich.DeleteSelectedRows();
        }

        private void TabPage_Load()
        {
            if (grvGiaoDich.FocusedRowHandle < 0)
            {
                gcNhatKy.DataSource = null;
                return;
            }

            int maGD = (int)grvGiaoDich.GetFocusedRowCellValue("ID");

            switch (tabMain.SelectedTabPageIndex)
            {
                case 0:
                    gcNhatKy.DataSource = db.mglgdNhatKyXuLies.Where(p => p.MaGD == maGD)
                        .OrderByDescending(p => p.NgayXL)
                        .AsEnumerable()
                        .Select((p, index) => new
                        {
                            p.ID,
                            STT = index + 1,
                            p.NgayXL,
                            p.MaTT,
                            p.DienGiai,
                            HoTenNV = p.NhanVien.HoTen
                        }).ToList();
                    break;
                case 1:
                    ctlTaiLieu1.FormID = 180;
                    ctlTaiLieu1.LinkID = (int?)grvGiaoDich.GetFocusedRowCellValue("MaGD");
                    ctlTaiLieu1.MaNV = Common.StaffID;
                    ctlTaiLieu1.TaiLieu_Load();
                    break;
                case 2:
                    gcNhanVien.DataSource = db.mglgdHHNhanViens.Where(p => p.MaGD == maGD);
                    break;
                case 3:
                    gcLichTT.DataSource = db.pgcPhieuThus.Where(p => p.MaPGC == maGD);
                    break;
                case 4:
                    gcPhieuChi.DataSource = db.pgcPhieuChis.Where(p => p.MaPGC == maGD);
                    break;
                case 5:
                    gcDichVu.DataSource = (from dv in db.DichVUs
                                           where dv.GDID == maGD
                                           select new
                                           {
                                               dv.TenDV,
                                               dv.SoLuong,
                                               dv.MaLDV,
                                               dv.ID,
                                               dv.GDID,
                                               dv.ThanhTien,
                                               DaThu = db.getDaThuDV(dv.ID),
                                               ConLai = dv.ThanhTien - db.getDaThuDV(dv.ID)
                                           }).ToList();
                    break;
            }
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            SetDate(0);
            lookNhanVien.DataSource = db.NhanViens;
            lkThu.DataSource = db.pgcLoaiPhieuThuChis.Where(p => p.IsPaid == true);
            lkLoaiChi.DataSource = db.pgcLoaiPhieuThuChis.Where(p => p.IsPaid == null);
            lkLoaiDV.DataSource = db.LoaiDichVus;
            LoadPermission();
            GetAccessData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GiaoDich_Load();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvGiaoDich.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu đăng ký");
                return;
            }
            GiaoDich_Edit();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GiaoDich_Delete();
        }

        private void grvGiaoDich_DoubleClick(object sender, EventArgs e)
        {
            if (grvGiaoDich.FocusedRowHandle < 0)
            {
                return;
            }

            GiaoDich_Edit();
        }

        private void grvGiaoDich_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                GiaoDich_Delete();
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
            GiaoDich_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            GiaoDich_Load();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void grvGiaoDich_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TabPage_Load();
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            TabPage_Load();
        }

        private void grvGiaoDich_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator & e.RowHandle >= 0)
            {
                //var cl = System.Drawing.Color.FromArgb((int)grvGiaoDich.GetRowCellValue(e.RowHandle, "MauNen"));
                //e.Graphics.FillRectangle(new SolidBrush(cl), e.Info.Bounds);
                //e.Handled = true;
            }
        }

        private void grvGiaoDich_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            //if (e.Column.FieldName == "TenHT")
            //{
            //    e.Appearance.ForeColor = grvGiaoDich.GetRowCellValue(e.RowHandle, "TenHT").ToString() == "Bán" ?
            //            System.Drawing.Color.Red : System.Drawing.Color.Black;
            //    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            //}
        }

        void Duyet(byte maTT)
        {
            //if (grvGiaoDich.FocusedRowHandle < 0)
            //{
            //    DialogBox.Error("Vui lòng chọn giao dịch");
            //    return;
            //}

            //frmDuyet frm = new frmDuyet();
            //frm.MaTT = maTT;
            //frm.MaGD = (int)grvGiaoDich.GetFocusedRowCellValue("MaGD");
            //frm.ShowDialog();
            //if (frm.DialogResult == DialogResult.OK)
            //    GiaoDich_Load();
        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(3);
        }

        private void itemKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(4);
            var objGD = db.mglgdGiaoDiches.FirstOrDefault(p => p.MaGD == (int)grvGiaoDich.GetFocusedRowCellValue("MaGD"));
            try
            {
                var objmt = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == objGD.MaMT);
                objmt.MaTT = 0;
                var obj = db.mglbcBanChoThues.First(p => p.MaBC == objGD.MaBC);
                obj.MaTT = 0;
                db.SubmitChanges();
            }
            catch
            { }
        }

        private void itemChuaThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(5);
        }

        private void itemGiaoDichThanhCong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(7);
            var objGD = db.mglgdGiaoDiches.FirstOrDefault(p => p.MaGD == (int)grvGiaoDich.GetFocusedRowCellValue("MaGD"));
            try
            {
                var objmt = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == objGD.MaMT);
                objmt.MaTT = 4;
                var obj = db.mglbcBanChoThues.First(p => p.MaBC == objGD.MaBC);
                obj.MaTT = 4;
                db.SubmitChanges();
            }
            catch
            { }
        }

        private void itemTHuChuaDu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(6);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(2);
        }

        private void itemDaKyHD_Click(object sender, EventArgs e)
        {

        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new Mua.GiaoDich.frmEdit();
            frm.ShowDialog();
        }

        private void itemPhieuThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var MaKH = (int?)grvGiaoDich.GetFocusedRowCellValue("MaKH");
            var ConLai = (decimal?)grvGiaoDich.GetFocusedRowCellValue("ConLaiMG");
            var ID = (int?)grvGiaoDich.GetFocusedRowCellValue("ID");
            var frm = new BEE.THUCHI.PhieuThu.frmEdit();
            frm.MaKH = MaKH;
            frm.MaLoai = 2;
            frm.TienThu = ConLai;
            frm.MaGD = ID;
            frm.ShowDialog();
        }

        private void itemPhieuChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            var MaKH = (int?)grvGiaoDich.GetFocusedRowCellValue("MaKH");
            var ConLai = (decimal?)grvGiaoDich.GetFocusedRowCellValue("PhaiChi");
            var ID = (int?)grvGiaoDich.GetFocusedRowCellValue("ID");
            var frm = new BEE.THUCHI.PhieuChi.frmEdit();
            frm.MaKH = MaKH;
            frm.TienThu = ConLai;
            frm.MaGD = ID;
            frm.ShowDialog();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ID = (int?)grvGiaoDich.GetFocusedRowCellValue("ID");
            var frn = new Mua.GiaoDich.frmDV();
            frn.MaGD = ID;
            frn.ShowDialog();
            TabPage_Load();
        }

        private void itemSuaPT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ID = (int?)grvLichTT.GetFocusedRowCellValue("MaPT");
            if (ID == null)
            {
                DialogBox.Error("Vui lòng chọn phiếu thu");
                return;
            }
            var frm = new BEE.THUCHI.PhieuThu.frmEdit();
            frm.ID = ID;
            frm.ShowDialog();
            TabPage_Load();
        }

        private void itemXoaPT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvLichTT.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu giao dịch");
                return;
            }
            if (DialogBox.Question() == DialogResult.No) return;
            foreach (var i in indexs)
            {
                try
                {
                    var obj = db.pgcPhieuThus.SingleOrDefault(p => p.MaPT == (int)grvLichTT.GetRowCellValue(i, "MaPT"));
                    db.pgcPhieuThus.DeleteOnSubmit(obj);
                }
                catch
                {
                }

            }
            db.SubmitChanges();

            grvLichTT.DeleteSelectedRows();
        }

        private void itemSuaPC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ID = (int?)gridView1.GetFocusedRowCellValue("MaPC");
            if (ID == null)
            {
                DialogBox.Error("Vui lòng chọn phiếu chi");
                return;
            }
            var frm = new BEE.THUCHI.PhieuChi.frmEdit();
            frm.ID = ID;
            frm.ShowDialog();
            TabPage_Load();
        }

        private void itemXoaPC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gridView1.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu giao dịch");
                return;
            }
            if (DialogBox.Question() == DialogResult.No) return;
            foreach (var i in indexs)
            {
                try
                {
                    var obj = db.pgcPhieuChis.SingleOrDefault(p => p.MaPC == (int)gridView1.GetRowCellValue(i, "MaPC"));
                    db.pgcPhieuChis.DeleteOnSubmit(obj);
                }
                catch
                {
                }

            }
            db.SubmitChanges();

            grvLichTT.DeleteSelectedRows();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var MaKH = (int?)grvGiaoDich.GetFocusedRowCellValue("MaKH");
            var ConLai = (decimal?)grvDichVu.GetFocusedRowCellValue("ConLai");
            var ID = (int?)grvGiaoDich.GetFocusedRowCellValue("ID");
            var MaDV = (int?)grvDichVu.GetFocusedRowCellValue("ID");
            var frm = new BEE.THUCHI.PhieuThu.frmEdit();
            frm.MaKH = MaKH;
            frm.MaDV = MaDV;
            frm.MaLoai = 3;
            frm.TienThu = ConLai;
            frm.MaGD = ID;
            frm.ShowDialog();
        }

        private void iteDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ID = (int?)grvGiaoDich.GetFocusedRowCellValue("ID");
            if (ID == null)
            {
                DialogBox.Error("Vui lòng chọn giao dịch");
                return;
            }
            var obj = db.GiaoDichjs.SingleOrDefault(p => p.ID == ID);
            obj.Duyet = true;
            db.SubmitChanges();
            DialogBox.Error("Dữ liệu đã được cập nhật");
            GiaoDich_Load();
        }
    }
}
